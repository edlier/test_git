using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.HR
{
    public partial class HR_IO_EditLog : System.Web.UI.Page
    {
        string workerNum = "";
        string id="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
           if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString();
                }
                if (Session[SessionString.userID] != null)
                {
                    id = Session[SessionString.userID].ToString();
                    //string sql = "select c.EnFName from au_userinfo u, au_computerform c where u.workerNum=c.WorkerNum and u.ID=" + Session[SessionString.userID].ToString();
                    name_lab.Text = (GlobalAnnounce.dataSearching.search_user(Session[SessionString.userID].ToString())).Rows[0][0].ToString();

                    if (Request.QueryString["wn"] != null) //若get值存在，需顯示指定人員及時間的資料
                    {
                        workerNum = HR_class.decodeBase64(Request.QueryString["wn"]);
                        id_lab.Text = workerNum;
                        //string sql2 = "select c.ChiName,c.EnLName from au_computerform c where c.WorkerNum=" + workerNum + ";";
                        name2_lab.Text = (GlobalAnnounce.dataSearching.search_worker(workerNum)).Rows[0][0].ToString();
                    }

                    date_lab.Text = Request.QueryString["date"];

                    //不能提前輸入打卡時間
                    DateTime now = DateTime.Now;
                    DateTime date = Convert.ToDateTime(date_lab.Text);
                    if (date > now) 
                    {

                        string date2 = Request.QueryString["date"];
                        int y = int.Parse((date2.Split('-'))[0]), m = int.Parse((date2.Split('-'))[1]);
                        string para = "&y=" + y.ToString() + "&m=" + m.ToString();
                        if (Request.QueryString["id"] == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('不能提前輸入!!!'); location.href='HR_checkReport_day.aspx';", true);
                        }
                        else if (Request.QueryString["id"] == "2")
                        {
                            string aa = "alert('不能提前輸入!!!');" + "location.href='";
                            
                            aa += "HR_checkReport_personal.aspx?wn=" + Request.QueryString["wn"] + para;
                            aa += "';";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage",aa, true);
                        }
                    }

                    searchCheck();
                }
            }

            if (cb_insert.Checked)
            {
                dropPanel.Visible = false;
                tbPanel.Visible = true;
            }
            else
            {
                dropPanel.Visible = true;
                tbPanel.Visible = false;
            }
        }

        void searchCheck()
        {
            //string sql = "select a.WorkerNum, c.CheckTime, c.CheckType from au_computerform a left join hr_checkinout c "
            //+ "on (a.WorkerNum=c.WorkerNum and date(c.CheckTime)='" + Request.QueryString["date"] + "')"
            //+ " where a.WorkerNum=" + id_lab.Text +" AND c.isDel='X'"+ " ORDER BY CheckTime ASC";
            //DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            DataTable dt = GlobalAnnounce.dataSearching.search_check(id_lab.Text, Request.QueryString["date"]);
            List<LogData> logData = new List<LogData>();
            //將上面查詢結果存至logData
            logData = (from DataRow dr in dt.Rows
                       where dr["EmpNo"].ToString() == id_lab.Text
                       select new LogData
                         {
                             Id = dr["Id"].ToString(),
                             WorkerNum = dr["EmpNo"].ToString(),
                             time = Convert.ToDateTime(dr["CheckTime"].ToString()).ToString(),
                             Status = dr["CheckType"].ToString()
                         }).ToList();

            SetLogData(ref logData);//設定Checkin/Checkout
            showLogData(ref logData);//顯示資料
        }

        void SetLogData(ref List<LogData> lcd)
        {
            foreach (var item in lcd)
            {
                if (item.Status == "I")
                {
                    item.Status = "Checkin";
                    item.transfer = "Transfer to Check-Out";
                }
                else
                {
                    item.Status = "Checkout";
                    item.transfer = "Transfer to Check-In";
                }
            }
        }

        void showLogData(ref List<LogData> lcd)
        {
            var show_cd = lcd.ToList();
            checkinoutlog_grid.DataSource = show_cd;
            checkinoutlog_grid.DataBind();
        }

        class LogData
        {
            public string Id { get; set; }
            public string WorkerNum { get; set; }
            private DateTime? _Time = null;
            public string time
            {
                get { return _Time != null ? ((DateTime)_Time).ToString("HH:mm:ss") : ""; }
                set { _Time = value == "" ? (DateTime?)null : Convert.ToDateTime(value.ToString()); }
            }
            public string Status { get; set; }
            public string transfer { get; set; }
        }

        protected void update_btn_Click(object sender, EventArgs e)
        {
            workerNum = id_lab.Text;
            string time = "";
            
            if (tbPanel.Visible == false)
            {
                time = drop_time.SelectedItem.Text;
            }
            else
            {
                if (time_tb.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('請輸入時間');", true);
                    return;
                }
                else { time = time_tb.Text; }
                
            }
                
            string status ="";
            if (checkinout_rbl.SelectedIndex == -1) 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請選擇Checkin / Checkout!');", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('');", true);
                return;
            }
            else if (checkinout_rbl.SelectedItem.Value == "Checkin")
            {
                status = "I"; checkinout_rbl.SelectedIndex = -1;
            }
            else if (checkinout_rbl.SelectedItem.Value == "Checkout")
            {
                status = "O"; checkinout_rbl.SelectedIndex = -1;
            }
            
            //string sql = "INSERT INTO hr_checkinout(WorkerNum, CheckTime, CheckType, isDel, status) VALUES (" + workerNum + ",'" + date_lab.Text + " " + time + "','" + status + "','X',1)";
            string checkTime = date_lab.Text + " " + time;
            GlobalAnnounce.dataSearching.insert_checkinout(1, workerNum, checkTime, status);
            
            searchCheck();
            time_tb.Text = "";
            drop_time.SelectedIndex = 0;
        }

        //透過RowCommand來取得button的index 再抓取資料
        protected void checkinoutlog_grid_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = checkinoutlog_grid.Rows[index];

                Label idlab = (Label)row.FindControl("id_lab");
                string checkId = idlab.Text;
                workerNum = id_lab.Text;
                Label timelab = (Label)row.FindControl("time_lab");
                string time = timelab.Text;
                Label statuslab = (Label)row.FindControl("status_lab");                
                string status = statuslab.Text;
                if (status == "Checkin")
                {
                    status = "I";
                }
                else //if (status == "Checkout")
                {
                    status = "O";
                }

                DateTime now = DateTime.Now;
                string updateT = now.ToString("yyyy-MM-dd HH:mm:ss");

                //string sql = "Update hr_checkinout SET isDel='O', updateID='" + Session[SessionString.userID].ToString() + "', updateTime='" + updateT + "' WHERE WorkerNum=" + workerNum + " and CheckTime='" + Request.QueryString["date"] + " " + time + "' and CheckType='" + status + "'";
                //string id=Session[SessionString.userID].ToString();
                string checkTime = Request.QueryString["date"] + " " + time;
                GlobalAnnounce.dataSearching.update_checkinout(Session[SessionString.userID].ToString(), updateT, checkId);
                searchCheck();
            }
            else if (e.CommandName == "trans")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = checkinoutlog_grid.Rows[index];

                Label idlab = (Label)row.FindControl("id_lab");
                string checkId = idlab.Text;
                workerNum = id_lab.Text;
                Label timelab = (Label)row.FindControl("time_lab");
                string time = timelab.Text;
                Label statuslab = (Label)row.FindControl("status_lab");
                string status = statuslab.Text;
                //string statusOld = "";

                if (status == "Checkin")
                {
                    //statusOld = "I";
                    status = "O";
                }
                else //if (status == "Checkout") 
                {
                    //statusOld = "O";
                    status = "I";
                }

                DateTime now = DateTime.Now;
                string updateT = now.ToString("yyyy-MM-dd HH:mm:ss");
                string checkTime = Request.QueryString["date"] + " " + time;

                //string sql = "Update hr_checkinout SET isDel='O', updateID='" + Session[SessionString.userID].ToString() 
                //    + "', updateTime='" + updateT + "' WHERE WorkerNum=" + workerNum + " and CheckTime='" 
                //    + Request.QueryString["date"] + " " + time + "' and CheckType='" + statusOld + "'";
                GlobalAnnounce.dataSearching.update_checkinout(Session[SessionString.userID].ToString(), updateT, checkId);

                //sql = "INSERT INTO hr_checkinout(WorkerNum, CheckTime, CheckType, isDel, status) VALUES ("
                //    + workerNum + ",'" + Request.QueryString["date"] + " " + time + "','" + status + "','X',2)";
                //dt = GlobalAnnounce.db.GetDataTable(sql);
                GlobalAnnounce.dataSearching.insert_checkinout(2, workerNum, checkTime, status);
                searchCheck();
            }
        }

        protected void prev_btn_Click(object sender, EventArgs e)
        {
            string date = Request.QueryString["date"];
            int y = int.Parse((date.Split('-'))[0]), m = int.Parse((date.Split('-'))[1]);
            string para = "&y=" + y.ToString() + "&m=" + m.ToString();
            if (Request.QueryString["id"] == "1")
            {
                if (ViewState["PreviousPageUrl"] != null)
                    Response.Redirect(ViewState["PreviousPageUrl"].ToString());
                //Response.Redirect("HR_checkReport_day.aspx");
            }
            else if (Request.QueryString["id"] == "2")
            {
                Response.Redirect("HR_checkReport_personal.aspx?wn=" + Request.QueryString["wn"] + para);
            }
        }
    }
}