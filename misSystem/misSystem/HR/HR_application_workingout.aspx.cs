using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics; //Debug.pring();
using System.Data;

namespace misSystem.HR
{
    public partial class HR_application_out : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HR_class.prevent_cache(); //防止回上一頁，表單重load

            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }
                
                workNum_lab.Text = HR_class.getWorkerNum(Session["check_workerNum"], Session[SessionString.userID].ToString());

                string str = "select workerNum, missysID from au_userinfo where workerNum = " + workNum_lab.Text + ";";
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["missysID"].ToString();
                start_date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                start_time_text.Text = "8:30";
                end_time_text.Text = "17:30";

                if (Session["check_sn"] != null) //若session存在，表示審核
                {
                    form_panel.Enabled = false;

                    string sn = Session["check_sn"].ToString();
                    string ap = Session["check_ap"].ToString();

                    str = "select * from hr_application_workingout where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    DateTime dd;
                    dd = DateTime.Parse(dt.Rows[0]["start_time"].ToString());
                    start_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                    start_time_text.Text = dd.ToString(@"HH : mm");
                    dd = DateTime.Parse(dt.Rows[0]["end_time"].ToString());
                    end_time_text.Text = dd.ToString(@"HH : mm");
                    content_text.Text = dt.Rows[0]["content"].ToString().Trim();
                    ViewState["status"] = dt.Rows[0]["status"].ToString();

                    ViewState["check_sn"] = Session["check_sn"].ToString();

                    Session.Remove("check_sn");
                    Session.Remove("check_ap");
                    Session.Remove("check_workerNum");

                    send_btn.Visible = false;
                    pass_btn.Visible = true;
                    fail_btn.Visible = true;
                }
                else if (Request.QueryString["SN"] != null) //若get值存在，表示要修改或刪除表單
                {
                    string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
                    str = "select * from hr_application_workingout where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    DateTime dd;
                    dd = DateTime.Parse(dt.Rows[0]["start_time"].ToString());
                    start_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                    start_time_text.Text = dd.ToString(@"HH : mm");
                    dd = DateTime.Parse(dt.Rows[0]["end_time"].ToString());
                    end_time_text.Text = dd.ToString(@"HH : mm");
                    content_text.Text = dt.Rows[0]["content"].ToString().Trim();

                    send_btn.Visible = false;
                    edit_btn.Visible = true;
                    delete_btn.Visible = true;
                }
            }
        }

        protected void send_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; } //資料防呆

            DateTime start, end;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(start_date_text.Text + " " + end_time_text.Text + ":00");

            int status = HR_class.getStatus(Session["auDep5"].ToString());
            string str = "insert into hr_application_workingout(workerNum, start_time, end_time, content, status) values(" + workNum_lab.Text + ",'" + start.ToString(@"yyyy\/MM\/dd H:mm:ss") + "','" + end.ToString(@"yyyy\/MM\/dd H:mm:ss") + "','" + content_text.Text + "', " + status + ");";
            str += " select LAST_INSERT_ID( );";
            Debug.WriteLine(str);
            string sn = GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=" + HR_class.encodeBase64("2") + "';", true);
        }

        public bool checkInput()
        {
            //資料防呆
            bool flag = true;

            bool ff = HR_class.checkDate(start_date_text, start_time_text, start_date_text, end_time_text, hours_lab, datetime_lab);
            if (ff)
            { datetime_lab.Visible = false; }
            else
            {
                datetime_lab.Visible = true;
                flag = false;
            }

            if (content_text.Text.Trim() == "")
            {
                content_lab.Text = "Content can't be empty.";
                content_lab.Visible = true;
                flag = false;
            }
            else { content_lab.Visible = false; }

            return flag;
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; }

            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            DateTime start, end;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(start_date_text.Text + " " + end_time_text.Text + ":00");

            string str = "update hr_application_workingout set start_time = '" + start.ToString(@"yyyy\/MM\/dd H:mm:ss") + "', end_time = '" + end.ToString(@"yyyy\/MM\/dd H:mm:ss") + "', content = '" + content_text.Text + "' where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=2';", true);
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_workingout set isDelete = 1 where SN = " + sn + ";";
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=-1&type=2';", true);
        }

        protected void start_time_text_TextChanged(object sender, EventArgs e)
        {
            DateTime start, end, am830, pm530;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(start_date_text.Text + " " + end_time_text.Text + ":00");
            am830 = Convert.ToDateTime(start_date_text.Text + " 8:30:00");
            pm530 = Convert.ToDateTime(start_date_text.Text + " 17:30:00");
            if (DateTime.Compare(start, am830) < 0 || DateTime.Compare(start, end) > 0)
            { start_time_text.Text = "8:30"; }
            if (DateTime.Compare(end, am830) < 0 || DateTime.Compare(end, pm530) > 0)
            { end_time_text.Text = "17:30"; }

            bool flag = HR_class.checkDate(start_date_text, start_time_text, start_date_text, end_time_text, hours_lab, new Label());
            if (flag) { datetime_lab.Visible = false; }
        }

        protected void content_text_TextChanged(object sender, EventArgs e)
        {
            if (content_text.Text.Trim() != "") { content_lab.Visible = false; }
        }

        protected void pass_btn_Click(object sender, EventArgs e)
        {
            string sn = ViewState["check_sn"].ToString();
            int status =int.Parse(ViewState["status"].ToString());

            //審核
            string sql = "update HR_application_workingout ";
            switch (status)
            {
                case (int)HR_class.STATUS.Department_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.Administration_Employee + " "; break;
                case (int)HR_class.STATUS.Administration_Manager:
                case (int)HR_class.STATUS.Administration_Employee:
                    sql += "set status=" + (int)HR_class.STATUS.Pass + " "; break;
            }
            sql += "where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is passed.'); window.location.href='HR_check.aspx';", true);
        }

        protected void fail_btn_Click(object sender, EventArgs e)
        {
            //審核不通過
            string sn = ViewState["check_sn"].ToString();
            string sql = "update HR_application_workingout set status = " + (int)HR_class.STATUS.Fail + " where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.'); window.location.href='HR_check.aspx';", true);
        
        }

    }
}