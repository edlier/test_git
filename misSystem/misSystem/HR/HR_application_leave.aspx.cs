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
    public partial class HR_application_leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HR_class.prevent_cache(); //防止回上一頁時，表單重load

            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }

                workNum_lab.Text = HR_class.getWorkerNum(Session["check_workerNum"], Session[SessionString.userID].ToString());

                string str = "select au_computerform.WorkerNum, au_computerform.EnFName, au_computerform.DepID "
                       + "from au_computerform "
                       + "where au_computerform.WorkerNum = " + workNum_lab.Text + ";";                              
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["EnFName"].ToString();
                string dep = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["DepID"].ToString();
                dep_no_lab.Text = dep;
                start_date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                end_date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                start_time_text.Text = "8:30";
                end_time_text.Text = "17:30";
                
                str = "select * from HR_leave_reason";
                HR_class.setDropdownlist(str,"Descript","Id",leave_rad);
                leave_rad.Items[1].Selected = true;

                str = "select * from au_audep where id = " + dep + ";";
                HR_class.setDropdownlist(str, "Description", "id", dep_dl);
                getSupDl();

                setUpForm();
            }
        }

        void setUpForm()
        {
            string str = "";

            if (Session["check_sn"] != null) //若session存在，表示審核
            {
                form_panel.Enabled = false;
                
                string sn = Session["check_sn"].ToString();
                string ap = Session["check_ap"].ToString();

                str = "select * from hr_application_leave where SN = " + sn + ";";
                DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                workNum_lab.Text = dt.Rows[0]["workerNum"].ToString();
                
                leave_rad.SelectedValue = dt.Rows[0]["leave_reason"].ToString().Trim();
                DateTime dd;
                dd = DateTime.Parse(dt.Rows[0]["start_time"].ToString());
                start_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                start_time_text.Text = dd.ToString(@"HH : mm");
                dd = DateTime.Parse(dt.Rows[0]["end_time"].ToString());
                end_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                end_time_text.Text = dd.ToString(@"HH : mm");
                content_text.Text = dt.Rows[0]["content"].ToString().Trim();
                string sup = dt.Rows[0]["proxy"].ToString();

                if (sup != "0")
                {
                    str = "SELECT * FROM au_computerform where WorkerNum = " + sup + ";";
                    string sup_dep = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["DepID"].ToString();
                    dep_dl.SelectedValue = sup_dep;
                    getSupDl();
                    employee_dl.SelectedValue = sup;
                }

                ViewState["status"] = dt.Rows[0]["status"].ToString();

                ViewState["check_sn"] = Session["check_sn"].ToString();

                Session.Remove("check_sn");
                Session.Remove("check_ap");
                Session.Remove("check_workerNum");

                if (ap == "proxy_leavl") //代理人審核
                { proxy_pass_btn.Visible = true; }
                else { pass_btn.Visible = true; }
                send_btn.Visible = false;                
                fail_btn.Visible = true;
            }
            else if (Request.QueryString["SN"] != null) //若get值存在，表非要修改為刪除表單
            {
                string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
                str = "select * from hr_application_leave where SN = " + sn + ";";
                DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                workNum_lab.Text = dt.Rows[0]["workerNum"].ToString();
                leave_rad.SelectedValue = dt.Rows[0]["leave_reason"].ToString().Trim();
                DateTime dd;
                dd = DateTime.Parse(dt.Rows[0]["start_time"].ToString());
                start_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                start_time_text.Text = dd.ToString(@"HH : mm");
                dd = DateTime.Parse(dt.Rows[0]["end_time"].ToString());
                end_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                end_time_text.Text = dd.ToString(@"HH : mm");
                content_text.Text = dt.Rows[0]["content"].ToString().Trim();
                string sup = dt.Rows[0]["proxy"].ToString();

                if (sup != "0")
                {
                    str = "SELECT * FROM au_computerform where WorkerNum = " + sup + ";";
                    string sup_dep = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["DepID"].ToString();
                    dep_dl.SelectedValue = sup_dep;
                    getSupDl();
                    employee_dl.SelectedValue = sup;
                }

                send_btn.Visible = false;
                edit_btn.Visible = true;
                delete_btn.Visible = true;
            }
        }

        void getSupDl()
        {
            //顯示代理人下拉選單
            if (dep_dl.SelectedValue.ToString() == "")
            { return; }
            string str = "select *, CONCAT( CAST( au_computerform.WorkerNum AS CHAR ) , ', ', au_computerform.EnFName ) AS fullName "
                + "from au_computerform, au_audep "
                + "where au_computerform.DepID = au_audep.id "
                + "AND DepID = " + dep_dl.SelectedValue.ToString() + " "
                + "AND au_computerform.WorkerNum != " + workNum_lab.Text + ";";

            HR_class.setDropdownlist(str, "fullName", "WorkerNum", employee_dl);
        }

        protected void send_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; } //資料防呆

            DateTime start, end;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(end_date_text.Text + " " + end_time_text.Text + ":00");

            int status = HR_class.getStatus(Session["auDep5"].ToString());
            string str = "insert into hr_application_leave(workerNum, leave_reason, start_time, end_time, content, proxy, status) values(" + workNum_lab.Text + "," + leave_rad.SelectedValue.ToString() + ",'" + start.ToString(@"yyyy\/MM\/dd H:mm:ss") + "','" + end.ToString(@"yyyy\/MM\/dd H:mm:ss") + "','" + content_text.Text + "','" + employee_dl.SelectedValue + "',7);";
            str += " select LAST_INSERT_ID( );";
            Debug.WriteLine(str);
            string sn = GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN="+HR_class.encodeBase64(sn)+"&type="+HR_class.encodeBase64("0")+"';", true);
        }

        bool checkInput()
        {
            //資料防呆
            bool flag = true;            

            bool ff = HR_class.checkDate(start_date_text, start_time_text, end_date_text, end_time_text, hours_lab, datetime_lab);
            if (ff)
            {
                DateTime start, end, am830, pm530;
                start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
                end = Convert.ToDateTime(end_date_text.Text + " " + end_time_text.Text + ":00");
                am830 = Convert.ToDateTime(start_date_text.Text + " 8:30:00");
                pm530 = Convert.ToDateTime(end_date_text.Text + " 17:30:00");
                if (DateTime.Compare(start, am830) >= 0 && DateTime.Compare(end, pm530) <= 0)
                { datetime_lab.Visible = false; }
                else
                {
                    datetime_lab.Text = "8:30~17:30";
                    datetime_lab.Visible = true;
                    flag = false;
                }                 
            }
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

            //if (employee_dl.SelectedValue == "")
            //{
            //    proxy_lab.Text = "Proxy can't be empty.";
            //    proxy_lab.Visible = true;
            //    flag = false;
            //}
            //else { proxy_lab.Visible = false; }

            return flag;
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; }

            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            DateTime start, end;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(end_date_text.Text + " " + end_time_text.Text + ":00");

            string str = "update hr_application_leave set leave_reason = " + leave_rad.SelectedValue.ToString() + ", start_time = '" + start.ToString(@"yyyy\/MM\/dd H:mm:ss") + "', end_time = '" + end.ToString(@"yyyy\/MM\/dd H:mm:ss") + "', content = '" + content_text.Text + "', proxy = " + employee_dl.SelectedValue + " where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been edited.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=0';", true);
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_leave set isDelete = 1 where SN = " + sn + ";";
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been deleted.'); window.location.href='HR_list.aspx?SN=-1&type=0';", true);
        }

        protected void start_date_text_TextChanged(object sender, EventArgs e)
        {
            //if (start_time_text.Text == "12:30")
            //{ start_time_text.Text = "12:00"; }
            //if (end_time_text.Text == "12:30")
            //{ end_time_text.Text = "13:00"; }

            if (start_time_text.Text == "12:30")
            { start_time_text.Text = "13:00"; }
            if (end_time_text.Text == "12:30")
            { end_time_text.Text = "12:00"; }

            DateTime start, end, am830, pm530;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(end_date_text.Text + " " + end_time_text.Text + ":00");

            am830 = Convert.ToDateTime(start_date_text.Text + " 8:30:00");
            pm530 = Convert.ToDateTime(start_date_text.Text + " 17:30:00");
            if (DateTime.Compare(start, am830) < 0 || DateTime.Compare(start, end) > 0)
            { start_time_text.Text = "8:30"; }
            am830 = Convert.ToDateTime(end_date_text.Text + " 8:30:00");
            pm530 = Convert.ToDateTime(end_date_text.Text + " 17:30:00");
            if (DateTime.Compare(end, am830) < 0 || DateTime.Compare(end, pm530) > 0)
            { end_time_text.Text = "17:30"; }

            bool flag = HR_class.checkDate(start_date_text, start_time_text, end_date_text, end_time_text, hours_lab, new Label());
            if (flag) { datetime_lab.Visible = false; }
        }

        protected void dep_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSupDl();
        }

        protected void content_text_TextChanged(object sender, EventArgs e)
        {
            if (content_text.Text.Trim() != "") { content_lab.Visible = false; }
        }

        protected void proxy_text_TextChanged(object sender, EventArgs e)
        {
            if (employee_dl.SelectedValue != "") { proxy_lab.Visible = false; }
        }

        protected void pass_btn_Click(object sender, EventArgs e)
        {
            string sn = ViewState["check_sn"].ToString();
            int status = int.Parse(ViewState["status"].ToString());
            
            //審想
            string sql = "update HR_application_leave ";
            switch (status)
            {
                case (int)HR_class.STATUS.Proxy:
                    sql += "set status=" + (int)HR_class.STATUS.Department_Manager + " "; break;
                case (int)HR_class.STATUS.Department_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.HR + " "; break;
                case (int)HR_class.STATUS.HR:
                    sql += "set status=" + (int)HR_class.STATUS.Administration_Manager + " "; break;
                case (int)HR_class.STATUS.Administration_Manager:
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
            string sql = "update HR_application_leave set status = " + (int)HR_class.STATUS.Fail + " where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.'); window.location.href='HR_check.aspx';", true);
        }
    }
}