using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using System.IO;

namespace misSystem.HR
{
    public partial class HR_checkReport_day : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HR_class.setSysTime(); //設定上下班等時間

                DateTime now = DateTime.Now;
                date_text.Text = now.ToString("yyyy-MM-dd");

                string str = @"select hr_checkinout_log.time from hr_checkinout_log order by hr_checkinout_log.Id desc limit 1;";
                string last_log = DateTime.Parse((GlobalAnnounce.db.GetDataTable(str)).Rows[0][0].ToString()).ToString(@"yyyy\/MM\/dd H:mm:ss");
                last_log_lab.Text = last_log;

                uc_all_employee.changeDropdownlist(1, date_text.Text);//day:1, month:2, personal & memo:3

                if (Session[SessionString.userID] != null)
                {
                    string sql = "select c.ChiName,c.EnFName from hr_per_employee c where c.EmpPID=" + Session[SessionString.userID].ToString();
                    name_lab.Text = (GlobalAnnounce.db.GetDataTable(sql)).Rows[0][0].ToString();
                }
            }            
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            //string sql = "select a.WorkerNum, a.EnFName, a.ChiName, c.CheckTime, c.CheckType, c.isDel from au_computerform a ";
            //sql += " left join hr_checkinout c " + "on (a.WorkerNum=c.WorkerNum and date(c.CheckTime)='" + date_text.Text + "')";
            //sql += " join au_userinfo on(au_userinfo.WorkerNum=a.WorkerNum) ";
            //sql += " left join hr_specialperson h on(au_userinfo.WorkerNum=h.workerNum) ";

            //if (uc_all_employee.getValue() != "***")
            //{
            //    sql += " where a.WorkerNum=" + uc_all_employee.getValue() + " AND ";
            //}
            //else { sql += " WHERE"; }
            //string month = date_text.Text.Substring(5, 2);
            //sql += "  (c.isDel='X' OR c.isDel<=>NULL) AND ( (a.InaugurationDate<='" + date_text.Text + "' AND (au_userinfo.leavedDT >= '" + date_text.Text + "' OR au_userinfo.leavedDT<=>NULL))  AND ((h.type = 2) OR (h.type = 1 AND h.isDel ='O') OR (h.type = 3)   OR h.type<=>NULL) ) ";
            //sql += " ORDER BY WorkerNum, CheckTime ASC";

            DataTable dt_checkinout = GlobalAnnounce.dataSearching.search_DayCheckinout(date_text.Text, uc_all_employee.getValue());
            DataTable dt_leave = GlobalAnnounce.dataSearching.search_DayLeave(date_text.Text);

            List<checkData> checkData = new List<checkData>();

            checkData = HR_class.getCheckData(1,dt_checkinout, dt_leave); //將打卡資料、請假資料進行計算

            //setCheckLeave(ref checkData); //設定請假記錄
            showCheckData(ref checkData); //顯示資料

            Session["inout_list"] = checkData;
        }

        protected void checkinout_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ((GridView)sender).PageIndex = e.NewPageIndex;
            ((GridView)sender).DataSource = Session["inout_list"];
            ((GridView)sender).DataBind();
        }

        //void setCheckLeave(ref List<checkData> cd)
        //{
        //    string date = date_text.Text;
        //    //查出時間區段所有假單及外出單
        //    //string sql = "select l.workerNum, r.Descript, l.start_time as start, l.end_time as end "
        //    //    + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
        //    //    + "where ('" + date_text.Text + "' BETWEEN date(l.start_time) and date(l.end_time)) "
        //    //    + "UNION "
        //    //    + "select w.workerNum, '外出' as Descripit, w.start_time as start, w.end_time as end "
        //    //    + "from hr_application_workingout w "
        //    //    + "where ('" + date_text.Text + "' BETWEEN date(w.start_time) and date(w.end_time))"
        //    //    + " ORDER BY start ASC;";
        //    DataTable dt = GlobalAnnounce.dataSearching.search_DayLeave(date_text.Text);

        //    foreach (var item in cd)
        //    {
        //        //比對為記錄至memo
        //        var i = (from DataRow dr in dt.Rows
        //                 where dr["workerNum"].ToString() == item.WorkerNum
        //                 select dr).ToList();

        //        //系統時間
        //        DateTime am830 = Convert.ToDateTime(item.Date + " " + HR_class.CheckIn);
        //        DateTime am831 = Convert.ToDateTime(item.Date + " " + HR_class.Late);
        //        DateTime am900 = Convert.ToDateTime(item.Date + " " + HR_class.Absen_time);
        //        DateTime pm1200 = Convert.ToDateTime(item.Date + " " + HR_class.MidOut);
        //        DateTime pm100 = Convert.ToDateTime(item.Date + " " + HR_class.MidIn);
        //        DateTime pm530 = Convert.ToDateTime(item.Date + " " + HR_class.CheckOut);
        //        //計算遲到的請假時間
        //        DateTime leave_start = am830, leave_end = pm530;
        //        //打卡時間
        //        DateTime checkin = am830;
        //        DateTime checkout = pm530;

        //        foreach (var ii in i)
        //        {
        //            if (item.CheckIn != "" || item.CheckOut != "")
        //            {
        //                checkin = Convert.ToDateTime(item.Date + " " + item.CheckIn.ToString());
        //                checkout = Convert.ToDateTime(item.Date + " " + item.CheckOut.ToString());
        //            }
        //            //請假時間
        //            DateTime start = Convert.ToDateTime(ii["start"]);
        //            DateTime end = Convert.ToDateTime(ii["end"]);

        //            if (ii == i[0])
        //            {
        //                item.memo = ii["Descript"].ToString();
        //                item.LeaveT = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
        //                leave_start = start; leave_end = end;
        //            }
        //            else
        //            {
        //                item.memo += "," + ii["Descript"].ToString();
        //                item.LeaveT2 = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
        //                leave_end = end;
        //            }
        //        }

        //        double late = 0.0;
        //        if (DateTime.Compare(leave_start, am830) == 0) //請假開始時間等於8:30
        //        {
        //            if (DateTime.Compare(leave_end, pm530) == 0) { } //請整天假
        //            else if (DateTime.Compare(leave_end, pm1200) < 0) //請假結束時間在12:00前
        //            { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }
        //            else if (DateTime.Compare(leave_end, pm1200) >= 0 && DateTime.Compare(leave_end, pm100) <= 0) //請假結束時間在12:00~13:00
        //            { late = Math.Ceiling(checkin.Subtract(pm100).TotalMinutes); }
        //            else if (DateTime.Compare(leave_end, pm100) > 0) //請假結束時間在13:00後
        //            { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }
        //        }
        //        else if (DateTime.Compare(leave_start, am830) > 0)
        //        {
        //            late = Math.Ceiling(checkin.Subtract(am831).TotalMinutes);
        //        }

        //        if (late > 0)
        //        {
        //            item.memo += ",遲到";
        //        }

        //    }
        //}

        public string para = ""; //串連get字串(個人報表)
        public string para2 = ""; //串連get字串(請假紀錄、快速輸入請假)
        void showCheckData(ref List<checkData> cd)
        {
            string date = date_text.Text;
            int y = int.Parse((date.Split('-'))[0]), m = int.Parse((date.Split('-'))[1]);
            para = "&y=" + y.ToString() + "&m=" + m.ToString();
            para2 = "&date="+date_text.Text;

            var show_cd = cd.ToList();
            if (uc_hr_memo_dl.getText() != "***") //是否有選擇員工
            {
                show_cd = (from item in cd
                               where item.memo.Contains(uc_hr_memo_dl.getText())
                               select item).ToList();
            }

            checkinout_grid.DataSource = show_cd;
            checkinout_grid.DataBind();            
            
            if (checkinout_grid.Rows.Count >= 1)
            { excel_btn.Visible = true; }
            else { excel_btn.Visible = false; }
        }

        protected void excel_btn_Click(object sender, EventArgs e)
        {
            //下載excel檔
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + date_text.Text + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            DataTable dt = new DataTable();
            dt.Columns.Add("WorkerNum"); dt.Columns.Add("Name"); dt.Columns.Add("CheckIn");
            dt.Columns.Add("CheckOut"); dt.Columns.Add("Memo");
            DataRow dr;
            List<checkData> cd = (List<checkData>)Session["inout_list"];
            foreach (var i in cd)
            {
                dr = dt.NewRow();
                dr["WorkerNum"] = i.WorkerNum;
                dr["Name"] = i.ChiName; 
                dr["CheckIn"] = i.CheckIn;
                dr["CheckOut"] = i.CheckOut; 
                dr["Memo"] = i.memo;
                dt.Rows.Add(dr);
            }
            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            gv.Style.Add("font-size", "16pt");
            gv.Style.Add("text-align", "center");
            foreach (TableCell c in gv.HeaderRow.Cells)
            {
                c.BackColor = System.Drawing.ColorTranslator.FromHtml("#507CD1");
                c.ForeColor = System.Drawing.Color.White;
            }
            gv.RowStyle.HorizontalAlign = HorizontalAlign.Center;

            gv.RenderControl(htw);           
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //下載excel時，需override此funtion

            /*Tell the compiler that the control is rendered
             * explicitly by overriding the VerifyRenderingInServerForm event.*/
        }

        protected void get_check_btn_Click(object sender, EventArgs e)
        {
            //自打卡機抓取時間區段的打卡資料


            //HR_class.changeAccessFileDirToMIS();
            try
            {
                string now = DateTime.Now.ToString(@"yyyy\/MM\/dd H:mm:ss");
                int c = setCheckInOut(last_log_lab.Text, now);
                if (c == 0)
                { get_infor_lab.Text = "沒有新的打卡資料"; }
                else
                {
                    get_infor_lab.Text = "已更新 " + last_log_lab.Text + " 至 " + now + "/r/n共 " + c + " 筆記錄";
                    last_log_lab.Text = now;
                }
            }
            catch (Exception ee)
            {
                string str = "alert('" + ee.Message.ToString().Replace("'", @"""") + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
        }

        int setCheckInOut(string start, string end)
        {
            // checkinout 資料表待改，log的更新人待改

            //新增資料至checkInoOut
            //建議逐月新增，否則資料量過大造成delay

            string result = HR_class.getCheckFromAccess(start, end);
            dynamic data = Newtonsoft.Json.Linq.JValue.Parse(result);
            
            string sqlstr = "insert into hr_checkinout(WorkerNum, CheckTime, CheckType) values";

            int i = 0;
            foreach (dynamic d in data.CheckInOut)
            {
                i++;
                sqlstr += "(" + d.Badgenumber + ", '" + ((DateTime)(d.CHECKTIME)).ToString(@"yyyy\/MM\/dd H:mm:ss") + "', '" + d.CHECKTYPE + "'),";
                //string str = "WorkerNum:" + d.Badgenumber + ", CheckTime:" + ((DateTime)(d.CHECKTIME)).ToString(@"yyyy\/MM\/dd H:mm:ss") + ", Type:" + d.CHECKTYPE;            
            }

            if (i == 0) { return i; }

            sqlstr = sqlstr.Remove(sqlstr.Length - 1) + ";";
            //記錄log，上限時，workerNum為登入者
            sqlstr += "insert into hr_checkinout_log(time, WorkerNum) values('" + end + "', 621);";

            //Response.Write(sqlstr);
            GlobalAnnounce.db.InsertDataTable(sqlstr);
            return i; //回傳已新增筆數
        }

        protected void date_text_TextChanged(object sender, EventArgs e)
        {
            uc_all_employee.changeDropdownlist(1, date_text.Text);//day:1, month:2, personal & memo:3
        }

    }
}