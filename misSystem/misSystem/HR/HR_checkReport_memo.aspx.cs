using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;

namespace misSystem.HR
{
    public partial class HR_checkReport_memo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HR_class.setSysTime();

                DateTime now = DateTime.Now;
                int now_year = int.Parse(now.Year.ToString());
                int now_month = int.Parse(now.Month.ToString());

                for (int i = now_year - 1; i <= (now_year); i++) //前一年至後三年
                {
                    year_dl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                for (int i = 1; i <= 12; i++)
                {
                    month_dl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                year_dl.SelectedValue = now_year.ToString();
                month_dl.SelectedValue = now_month.ToString();

                if (Session[SessionString.userID] != null)
                {
                    //string sql = "select c.EnFName from hr_per_employee c where c.EmpPID=" + Session[SessionString.userID].ToString();
                    name_lab.Text = (GlobalAnnounce.dataSearching.search_user(Session[SessionString.userID].ToString())).Rows[0][0].ToString();
                }

                uc_all_employee.changeDropdownlist(3, now_year + "-" + now_month);//day:1, month:2, personal & memo:3

                try
                {
                    if (Request.QueryString["wn"] != null) //若get值存在，需顯示指定時間及人員的資料
                    {
                        year_dl.SelectedValue = Request.QueryString["y"];
                        month_dl.SelectedValue = Request.QueryString["m"];

                        string workerNum = HR_class.decodeBase64(Request.QueryString["wn"]);
                        searchCheck(workerNum);
                        try { uc_all_employee.setText(workerNum); }
                        catch (Exception ee) { }
                    }
                }
                catch (Exception ee)
                { Response.Redirect("HR_checkReport_day.aspx"); }
            }
        }

        public string para = ""; //串連get字串
        void searchCheck(string workerNum)
        {
            //if (workerNum == "0") //必須選擇一名員工
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('請選擇員工');", true);
            //    return;
            //}

            //查詢出所有請假單及外出單
            string sql = "select l.SN, l.workerNum, l.start_time, l.end_time, r.Descript as memo, l.status as s, '1' as type "
                + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
                + "where year(l.start_time)='" + year_dl.SelectedValue + "' AND month(l.start_time)='" + month_dl.SelectedValue + "' AND isDelete=0";
            if (workerNum != "***") { sql += " AND l.workerNum=" + workerNum; }
            sql += " UNION "
            + "select w.SN, w.workerNum, w.start_time, w.end_time, '外出', w.status as s, '2' as type "
            + "from hr_application_workingout w "
            + "where year(w.start_time)='" + year_dl.SelectedValue + "' AND month(w.start_time)='" + month_dl.SelectedValue + "' AND isDelete=0";
            if (workerNum != "***") { sql += " AND w.workerNum=" + workerNum; }
            sql += " ORDER BY date(start_time),workerNum ASC;";

            DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            dt.Columns.Add("Status");
            dt.Columns.Add("encode_WorkerNum");
            dt.Columns.Add("Name");
            dt.Columns.Add("time");
            foreach (DataRow dr in dt.Rows)
            {
                dr["encode_WorkerNum"] = HR_class.encodeBase64(dr["workerNum"].ToString());
                if (dr["s"].ToString() == "0") { dr["Status"] = "Pass"; }
                else { dr["Status"] = "Not Pass"; }
                dr["Name"] = (GlobalAnnounce.dataSearching.search_worker(dr["workerNum"].ToString())).Rows[0][0].ToString();
                dr["time"] = HR_class.getHours(Convert.ToDateTime(dr["start_time"].ToString()), Convert.ToDateTime(dr["end_time"].ToString()));
            }

            para = "&y=" + year_dl.SelectedValue + "&m=" + month_dl.SelectedValue;
            checkinout_grid.DataSource = dt;
            checkinout_grid.DataBind();

            //sql = "select * "
            //    + "from au_computerform a inner join hr_checkinout c on a.WorkerNum=c.WorkerNum "
            //    + "AND time(c.CheckTime)> cast('" + HR_class.CheckIn + "' as time) "
            //    + "AND c.WorkerNum=" + workerNum + ";";
            //DataTable dt2 = GlobalAnnounce.db.GetDataTable(sql);
            //List<checkData> checkData = HR_class.getCheckDataList(dt2);

            //late_gv.DataSource = checkData;
            //late_gv.DataBind();

            //sql = "select  c.ChiName,c.EnLName from au_computerform c where c.WorkerNum=" + workerNum + ";";
            //workNum_lab.Text = workerNum;
            //search_name_lab.Text = (GlobalAnnounce.dataSearching.search_worker(workerNum)).Rows[0][0].ToString();
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            searchCheck(uc_all_employee.getValue());
        }

        protected void year_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            uc_all_employee.changeDropdownlist(3, time);//day:1, month:2, personal & memo:3
        }

        protected void month_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            uc_all_employee.changeDropdownlist(3, time);//day:1, month:2, personal & memo:3
        }

        protected void checkinout_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = checkinout_grid.Rows[index];

                Label snlab = (Label)row.FindControl("sn_lab");
                string sn = snlab.Text;
                Label wnlab = (Label)row.FindControl("wn_lab");
                string wn = wnlab.Text;
                Label memolab = (Label)row.FindControl("memo_lab");
                string memo = memolab.Text;
                Label startlab = (Label)row.FindControl("start");
                string start = startlab.Text;
                Label endlab = (Label)row.FindControl("end");
                string end = endlab.Text;
                Label statuslab = (Label)row.FindControl("status_lab");
                string status = statuslab.Text;
                if (status == "Pass")
                {
                    status = "0";
                }
                else //if (status == "Not Pass")
                {
                    status = "1";
                }
                Label typelab = (Label)row.FindControl("type_lab");
                string type = typelab.Text;

                DateTime now = DateTime.Now;
                string delT = now.ToString("yyyy-MM-dd hh:mm:ss");

                //string sql = "Update hr_checkinout SET isDel='O', updateID='" + Session[SessionString.userID].ToString() + "', updateTime='" + updateT + "' WHERE WorkerNum=" + workerNum + " and CheckTime='" + Request.QueryString["date"] + " " + time + "' and CheckType='" + status + "'";
                //string id=Session[SessionString.userID].ToString();
                //string checkTime = Request.QueryString["date"] + " " + time;
                GlobalAnnounce.dataSearching.update_leave(wn, start, end, status, type, delT, sn);
                searchCheck(uc_all_employee.getValue());
            }
        }
    }
}