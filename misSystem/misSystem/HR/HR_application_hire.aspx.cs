using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;

namespace misSystem.HR
{
    public partial class HR_application_hire : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HR_class.prevent_cache(); //防止回上一頁時，表單重load

            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }

                workNum_lab.Text = HR_class.getWorkerNum(Session["check_workerNum"], Session[SessionString.userID].ToString());

                string str = "select au_computerform.WorkerNum, au_computerform.EnFName, au_audep.id "
                    + "from au_computerform, au_audep "
                    + "where au_computerform.DepID = au_audep.id "
                    + "AND au_computerform.WorkerNum = " + workNum_lab.Text + ";";
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["EnFName"].ToString();
                string dep = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["id"].ToString();
                dep_no_lab.Text = dep;
                date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Debug.WriteLine(str);

                str = "select * from au_audep where id = " + dep + ";";
                HR_class.setDropdownlist(str, "Description", "id", dep_dl);
                prepare_no_text.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["many"].ToString();

                str = "select count(*) as many from au_computerform, au_audep "
                    + "where au_computerform.DepID = au_audep.id AND au_audep.id = " + dep + ";";
                now_no_text.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["many"].ToString();
                
                str = "select * from au_aulevel;";
                HR_class.setDropdownlist(str, "Description", "ID", position_dl);

                str = "select * from hr_hire_reason;";
                HR_class.setDropdownlist(str, "description", "id", hire_reason_rl);
                //hire_reason_rl.Items.Add(new ListItem("其它","0"));
                //hire_reason_rl.SelectedIndex = 0;

                if (Session["check_sn"] != null) //若session存在，表示審核
                {
                    form_panel.Enabled = false;

                    string sn = Session["check_sn"].ToString();
                    string ap = Session["check_ap"].ToString();
                    
                    str = "select * from hr_application_hire where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    dep_dl.SelectedValue = dt.Rows[0]["dep"].ToString().Trim();
                    position_dl.SelectedValue = dt.Rows[0]["position"].ToString().Trim();
                    date_text.Text = DateTime.Parse(dt.Rows[0]["in_time"].ToString()).ToString("yyyy-MM-dd");
                    apply_no_text.Text = dt.Rows[0]["apply_no"].ToString().Trim();
                    hire_reason_rl.SelectedValue = dt.Rows[0]["hire_reason"].ToString().Trim();
                    sex_dl.SelectedValue = dt.Rows[0]["sex"].ToString().Trim();
                    age_dl.SelectedValue = dt.Rows[0]["age"].ToString().Trim();
                    edu_dl.SelectedValue = dt.Rows[0]["edu"].ToString().Trim();
                    major_text.Text = dt.Rows[0]["major"].ToString().Trim();
                    skill_text.Text = dt.Rows[0]["skill"].ToString().Trim();
                    other_text.Text = dt.Rows[0]["other"].ToString().Trim();
                    content_text.Text = dt.Rows[0]["content"].ToString().Trim();
                    ViewState["status"] = dt.Rows[0]["status"].ToString();

                    if (int.Parse(ViewState["status"].ToString()) == (int)HR_class.STATUS.Administration_Employee)
                    {
                        //行政人員檢核時顯示 hr 填寫資料
                        hr_panel2.Visible = true;
                        way_lab.Text = dt.Rows[0]["way"].ToString().Trim();
                    }
                    else { hr_panel2.Visible = false; }

                    ViewState["check_sn"] = Session["check_sn"].ToString();

                    Session.Remove("check_sn");
                    Session.Remove("check_ap");
                    Session.Remove("check_workerNum");

                    send_btn.Visible = false;
                    pass_btn.Visible = true;
                    fail_btn.Visible = true;

                    if (int.Parse(ViewState["status"].ToString()) == (int)HR_class.STATUS.HR)
                    //HR人員檢核時 顯示應填資料
                    { hr_panel.Visible = true; fail_btn.Enabled = false; }
                    else { hr_panel.Visible = false; }
                }
                else if (Request.QueryString["SN"] != null) //若get值存在，表示要修改或刪除表單
                {
                    string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
                    str = "select * from hr_application_hire where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    dep_dl.SelectedValue = dt.Rows[0]["dep"].ToString().Trim();
                    position_dl.SelectedValue = dt.Rows[0]["position"].ToString().Trim();
                    date_text.Text = DateTime.Parse(dt.Rows[0]["in_time"].ToString()).ToString("yyyy-MM-dd");
                    apply_no_text.Text = dt.Rows[0]["apply_no"].ToString().Trim();
                    hire_reason_rl.SelectedValue = dt.Rows[0]["hire_reason"].ToString().Trim();
                    sex_dl.SelectedValue = dt.Rows[0]["sex"].ToString().Trim();
                    age_dl.SelectedValue = dt.Rows[0]["age"].ToString().Trim();
                    edu_dl.SelectedValue = dt.Rows[0]["edu"].ToString().Trim();
                    major_text.Text = dt.Rows[0]["major"].ToString().Trim();
                    skill_text.Text = dt.Rows[0]["skill"].ToString().Trim();
                    other_text.Text = dt.Rows[0]["other"].ToString().Trim();
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

            DateTime time = Convert.ToDateTime(date_text.Text);

            int status = HR_class.getStatus(Session["auDep5"].ToString());
            string str = @"
            insert into hr_application_hire(workerNum, dep, position, in_time, apply_no, hire_reason, sex, age, edu, major, skill, other, content, status)
            values(" + workNum_lab.Text + "," + dep_dl.SelectedValue.ToString() + "," + position_dl.SelectedValue + ",'" + time.ToString(@"yyyy\/MM\/dd") + "'," + apply_no_text.Text + "," + hire_reason_rl.SelectedValue.ToString() + ","
            + sex_dl.SelectedValue.ToString() + ",'" + age_dl.SelectedItem.ToString() + "','" + edu_dl.SelectedItem.ToString() + "','" + major_text.Text.Trim() + "','" + skill_text.Text.Trim() + "','" + other_text.Text.Trim() + "','" + content_text.Text.Trim() + "',1);";
            str = str.Replace("            ", string.Empty).Replace("\r\n", " ").Trim();
            //使用@連接字串時會有很多tab，雖不影響sql執行，為偵錯可讀性，最好將多餘tab刪除
            str += " select LAST_INSERT_ID( );";
            Debug.WriteLine(str);
            string sn = GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=" + HR_class.encodeBase64("6") + "';", true);
        }

        bool checkInput()
        {
            //資料防呆
            bool flag = true;
                        
            if (date_text.Text == "" )
            {
                date_lab.Text = "Date can't be empty.";
                date_lab.Visible = true;
                flag = false;
            }
            else { date_lab.Visible = false; }

            if (prepare_no_text.Text == "" || prepare_no_text.Text == "0")
            {
                prepare_no_lab.Text = "Field can't be empty or 0.";
                prepare_no_lab.Visible = true;
                flag = false;
            }
            else { prepare_no_lab.Visible = false; }

            if (apply_no_text.Text == "" || apply_no_text.Text == "0")
            {
                apply_no_lab.Text = "Field can't be empty or 0.";
                apply_no_lab.Visible = true;
                flag = false;
            }
            else { apply_no_lab.Visible = false; }

            if (major_text.Text == "")
            {
                major_lab.Text = "Major can't be empty.";
                major_lab.Visible = true;
                flag = false;
            }
            else { major_lab.Visible = false; }

            if (skill_text.Text == "")
            {
                skill_lab.Text = "Skill can't be empty.";
                skill_lab.Visible = true;
                flag = false;
            }
            else { skill_lab.Visible = false; }

            if (content_text.Text.Trim() == "")
            {
                content_lab.Text = "Content can't be empty.";
                content_lab.Visible = true;
                flag = false;
            }
            else { content_lab.Visible = false; }

            //if (hire_reason_rl.SelectedValue == "0")
            //{
            //    hire_reason_lab.Text = "Field can't be empty.";
            //    hire_reason_lab.Visible = true;
            //    flag = false;
            //}
            //else { hire_reason_lab.Visible = false; }

            if (edu_dl.SelectedValue == "0" && edu_other_text.Text == "")
            {
                edu_lab.Text = "Field can't be empty.";
                edu_lab.Visible = true;
                flag = false;
            }
            else { edu_lab.Visible = false; }

            return flag;
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; }

            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            DateTime time = Convert.ToDateTime(date_text.Text);
            string str = @"
            update hr_application_hire set dep = " + dep_dl.SelectedValue.ToString() + ", position =" + position_dl.SelectedValue + ", in_time ='" + time.ToString(@"yyyy\/MM\/dd") + @"',
            apply_no = " + apply_no_text.Text + ", hire_reason = " + hire_reason_rl.SelectedValue.ToString() + @"
            , sex = " + sex_dl.SelectedValue.ToString() + ", age = '" + age_dl.SelectedItem.ToString() + "', edu = '" + edu_dl.SelectedItem.ToString() + "', major = '" + major_text.Text + @"', 
            skill = '" + skill_text.Text + "', other = '" + other_text.Text + "', content = '" + content_text.Text + "' where SN = " + sn + ";";
            str = str.Replace("            ", string.Empty).Replace("\r\n", " ").Trim();
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been edited.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=6';", true);
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_hire set isDelete = 1 where SN = " + sn + ";";
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been deleted.'); window.location.href='HR_list.aspx?SN=-1&type=6';", true);
        }

        protected void date_text_TextChanged(object sender, EventArgs e)
        {
            if (date_text.Text != "") { date_lab.Visible = false; }
        }

        protected void prepare_no_text_TextChanged(object sender, EventArgs e)
        {
            try { 
                if (int.Parse(prepare_no_text.Text) > 0) { prepare_no_lab.Visible = false; }
                else if (int.Parse(prepare_no_text.Text) <= 0) { prepare_no_text.Text = "0"; }
            }
            catch (Exception ee) { }
        }

        protected void major_text_TextChanged(object sender, EventArgs e)
        {
            if (major_text.Text != "") { major_lab.Visible = false; }
        }

        protected void skill_text_TextChanged(object sender, EventArgs e)
        {
            if (skill_text.Text != "") { skill_lab.Visible = false; }
        }

        protected void content_text_TextChanged(object sender, EventArgs e)
        {
            if (content_text.Text != "") { content_lab.Visible = false; }
        }

        protected void edu_other_text_TextChanged(object sender, EventArgs e)
        {
            if (edu_dl.SelectedValue == "0" && edu_other_text.Text != "")
            { edu_lab.Visible = false; }
        }

        protected void apply_no_text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(apply_no_text.Text) > 0) { apply_no_lab.Visible = false; }
                else if (int.Parse(apply_no_text.Text) <= 0) { apply_no_text.Text = "0"; }
            }
            catch (Exception ee) { }
        }

        protected void pass_btn_Click(object sender, EventArgs e)
        {
            string sn = ViewState["check_sn"].ToString();
            int status = int.Parse(ViewState["status"].ToString());
            //審核
            string sql = "update HR_application_hire ";            
            switch (status)
            {
                case (int)HR_class.STATUS.Department_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.HR + " "; break;    
                case (int)HR_class.STATUS.HR:
                    sql += "set status=" + (int)HR_class.STATUS.Administration_Manager + " "; break;
                case (int)HR_class.STATUS.Administration_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.Pass + " "; break; 
            }
            sql += "where SN=" + sn + ";";

            if (status == (int)HR_class.STATUS.HR) //若為HR審核，需將填寫資料增加至原record
            {
                sql += "update HR_application_hire set way = '" + way_rl.SelectedItem.Text + "' "
                    + "where SN=" + sn + ";";
            }

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is passed.'); window.location.href='HR_check.aspx';", true);
        }

        protected void fail_btn_Click(object sender, EventArgs e)
        {
            //審核不通過
            string sn = ViewState["check_sn"].ToString();
            string sql = "update HR_application_hire set status = " + (int)HR_class.STATUS.Fail + " where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.'); window.location.href='HR_check.aspx';", true);
        }

        protected void now_situration_rl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (now_situration_rl.SelectedValue == "correct")
            { pass_btn.Enabled = true; fail_btn.Enabled = false; }
            else { pass_btn.Enabled = false; fail_btn.Enabled = true; }
        }
    }
}