using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics; //Debug.pring();

namespace misSystem.HR
{
    public partial class HR_application_transfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HR_class.prevent_cache(); //防止回上一頁，表單重load

            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }
                
                workNum_lab.Text = HR_class.getWorkerNum(Session["check_workerNum"], Session[SessionString.userID].ToString());

                string str = "select au_computerform.*, au_audep.Description "
                + "from au_computerform, au_audep "
                + "where au_computerform.DepID=au_audep.id "
                + "AND au_computerform.WorkerNum=" + workNum_lab.Text + ";";

                DataRow dr = GlobalAnnounce.db.GetDataTable(str).Rows[0];
                name_lab.Text = dr["EnFName"].ToString();
                start_work_time_lab.Text = Convert.ToDateTime(dr["InaugurationDate"]).ToString("yyyy-MM-dd");
                
                //算年資
                int y = DateTime.Now.Year - Convert.ToDateTime(dr["InaugurationDate"].ToString()).Year;
                if (y == 0) { dur_lab.Text = "未滿一年"; }
                else { dur_lab.Text = y.ToString(); }

                now_dep_no.Text = dr["DepID"].ToString();
                now_dep_lab.Text = dr["Description"].ToString();

                y = DateTime.Now.Year - Convert.ToDateTime(dr["InaugurationDate"].ToString()).Year;
                if (y == 0) { now_dep_dur_lab.Text = "未滿一年"; }
                else { now_dep_dur_lab.Text = y.ToString(); }

                str = "select * from au_audep where id != " + now_dep_no.Text + ";";
                HR_class.setDropdownlist(str, "description", "id", transfer_dep_dl);

                str = "select * from au_aulevel;";
                HR_class.setDropdownlist(str, "Description", "ID", position_dl);

                str = "select * from hr_transfer_reason;";
                HR_class.setDropdownlist(str, "description", "id", reason_dl);

                if (Session["check_sn"] != null) //若session存在，表示審核
                {
                    form_panel.Enabled = false;

                    string sn = Session["check_sn"].ToString();
                    string ap = Session["check_ap"].ToString();

                    str = "select * from hr_application_transfer where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    experience_text.Text = dt.Rows[0]["experience"].ToString();
                    transfer_dep_dl.SelectedValue = dt.Rows[0]["transfer_dep"].ToString();
                    position_dl.SelectedValue = dt.Rows[0]["position"].ToString();
                    future_content_text.Text = dt.Rows[0]["content"].ToString();
                    reason_dl.SelectedValue = dt.Rows[0]["reason"].ToString();

                    ViewState["status"] = dt.Rows[0]["status"].ToString();

                    if (int.Parse(ViewState["status"].ToString()) == (int)HR_class.STATUS.HR)
                    {
                        hr_panel.Visible = true;
                        start_date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        end_date_text.Text = (DateTime.Now).AddDays(10).ToString("yyyy-MM-dd");
                        go_date_text.Text = (DateTime.Now).AddDays(20).ToString("yyyy-MM-dd");
                    }

                    if (int.Parse(ViewState["status"].ToString()) == (int)HR_class.STATUS.Vice_General_Manager
                        || int.Parse(ViewState["status"].ToString()) == (int)HR_class.STATUS.General_Manager)
                    {
                        //總副總檢核時顯示 hr 填寫資料
                        hr_panel2.Visible = true;
                        start_date_lab.Text = Convert.ToDateTime(dt.Rows[0]["start_transfer"]).ToString("yyyy-MM-dd");
                        end_date_lab.Text = Convert.ToDateTime(dt.Rows[0]["end_transfer"]).ToString("yyyy-MM-dd");
                        go_date_l.Text = Convert.ToDateTime(dt.Rows[0]["go_transfer"]).ToString("yyyy-MM-dd");
                    }
                    else { hr_panel2.Visible = false; }

                    ViewState["check_sn"] = Session["check_sn"].ToString();

                    Session.Remove("check_sn");
                    Session.Remove("check_ap");
                    Session.Remove("check_workerNum");

                    send_btn.Visible = false;
                    pass_btn.Visible = true;
                    fail_btn.Visible = true;
                }
                else if (Request.QueryString["SN"] != null) //若get值存在，表示修改或刪除表單
                {
                    string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
                    str = "select * from hr_application_transfer where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    experience_text.Text = dt.Rows[0]["experience"].ToString();
                    transfer_dep_dl.SelectedValue = dt.Rows[0]["transfer_dep"].ToString();
                    position_dl.SelectedValue = dt.Rows[0]["position"].ToString();
                    future_content_text.Text = dt.Rows[0]["content"].ToString();
                    reason_dl.SelectedValue = dt.Rows[0]["reason"].ToString();

                    send_btn.Visible = false;
                    edit_btn.Visible = true;
                    delete_btn.Visible = true;
                }
            }
        }
           
        protected void send_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; } //資料防呆

            int status = HR_class.getStatus(Session["auDep5"].ToString());
            string str = "insert into hr_application_transfer(workerNum, experience, origin_dep, transfer_dep, position, content, reason, dep_turn, status) values(" + workNum_lab.Text + ",'" + experience_text.Text + "'," + now_dep_no.Text + "," + transfer_dep_dl.SelectedValue.ToString() + "," + position_dl.SelectedValue + ",'" + future_content_text.Text + "'," + reason_dl.SelectedValue + "," + now_dep_no.Text + ", " + status + ");";
            str += " select LAST_INSERT_ID( );";
            Debug.WriteLine(str);
            string sn = GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=" + HR_class.encodeBase64("5") + "';", true);
        }

        bool checkInput()
        {
            //資料防呆
            bool flag = true;

            if (experience_text.Text.Trim() == "")
            {
                experience_lab.Text = "Experience can't be empty.";
                experience_lab.Visible = true;
                flag = false;
            }
            else { experience_lab.Visible = false; }
            if (future_content_text.Text.Trim() == "")
            {
                future_content_lab.Text = "Content can't be empty.";
                future_content_lab.Visible = true;
                flag = false;
            }
            else { future_content_lab.Visible = false; }

            return flag;
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; }

            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_transfer set experience = '"+experience_text.Text+"', origin_dep = "+now_dep_no.Text+", transfer_dep = "+transfer_dep_dl.SelectedValue.ToString()+", position = "+position_dl.SelectedValue+", content = '"+future_content_text.Text+"', reason = "+reason_dl.SelectedValue+" where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=5';", true);
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_transfer set isDelete = 1 where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=-1&type=5';", true);
        }

        protected void pass_btn_Click(object sender, EventArgs e)
        {
            string sn = ViewState["check_sn"].ToString();
            int status = int.Parse(ViewState["status"].ToString());

            string str = "select dep_turn from hr_application_transfer where SN = " + sn + ";";
            DataTable dt = GlobalAnnounce.db.GetDataTable(str);
            string depTurn = dt.Rows[0]["dep_turn"].ToString(); //要調職的代號

            //審核
            string sql = "update HR_application_transfer ";
            switch (status)
            {
                case (int)HR_class.STATUS.Department_Manager:
                case (int)HR_class.STATUS.Administration_Manager:
                    if (now_dep_no.Text == depTurn)
                    { 
                        sql += "set dep_turn=" + transfer_dep_dl.SelectedValue.ToString() + " ";
                        if (transfer_dep_dl.SelectedValue.ToString() == HR_class.Administration)
                        { sql += ", status=" + (int)HR_class.STATUS.Administration_Manager + " "; }
                        else { sql += ", status=" + (int)HR_class.STATUS.Department_Manager + " "; }
                    }
                    else if (transfer_dep_dl.SelectedValue.ToString() == depTurn)
                    { sql += "set status=" + (int)HR_class.STATUS.HR + ", dep_turn=0 "; }
                    break;
                case (int)HR_class.STATUS.HR:
                    {
                        if (!hr_check_date())
                        { return; }

                        sql += "set status=" + (int)HR_class.STATUS.Vice_General_Manager + " ";
                        sql += ", start_transfer='" + start_date_text.Text + "', end_transfer='" + end_date_text.Text + "', go_transfer='" + go_date_text.Text + "' ";
                        break;
                    } 
                case (int)HR_class.STATUS.Vice_General_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.General_Manager + " "; break;
                case (int)HR_class.STATUS.General_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.Pass + " "; break;
            }
            sql += "where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is passed.'); window.location.href='HR_check.aspx';", true);
        }

        bool hr_check_date()
        {
            //hr填寫資料防呆
            if (start_date_text.Text == " " || end_date_text.Text == " ")
            {
                datetime_lab.Text = "Date can't be empty.";
                datetime_lab.Visible = true;
                return false;
            }
            if (go_date_text.Text == " ")
            {
                go_date_lab.Text = "Date can't be empty.";
                go_date_lab.Visible = true;
                return false;
            }

            DateTime start = Convert.ToDateTime(start_date_text.Text + " 00:00:00");
            DateTime end = Convert.ToDateTime(end_date_text.Text + " 00:00:00");
            if (DateTime.Compare(start, end) <= 0)
            {
                return true;
            }
            else
            { datetime_lab.Text = "StartTime can't be later than EndTime"; datetime_lab.Visible = true; }
            return false;
        }

        protected void fail_btn_Click(object sender, EventArgs e)
        {
            //審核不通過
            string sn = ViewState["check_sn"].ToString();
            string sql = "update HR_application_transfer set status = " + (int)HR_class.STATUS.Fail + " where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.'); window.location.href='HR_check.aspx';", true);
        }

        protected void start_date_text_TextChanged(object sender, EventArgs e)
        {

            if (start_date_text.Text != " " || end_date_text.Text == " ")
            {
                DateTime start = Convert.ToDateTime(start_date_text.Text + " 00:00:00");
                DateTime end = Convert.ToDateTime(end_date_text.Text + " 00:00:00");
                if (DateTime.Compare(start, end) <= 0)
                {
                    datetime_lab.Visible = false;
                }
            }
        }

        protected void go_date_text_TextChanged(object sender, EventArgs e)
        {
            if (go_date_text.Text != " ")
            {
                go_date_lab.Visible = false;
            }
        }
    }
}