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
    public partial class HR_application_support : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HR_class.prevent_cache(); //防止回上一頁，表單重load

            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }

                workNum_lab.Text = HR_class.getWorkerNum(Session["check_workerNum"], Session[SessionString.userID].ToString());

                string str = "select au_computerform.WorkerNum, EnFName, DepID, description "
                            + "from au_computerform, au_audep "
                            + "where au_computerform.WorkerNum = " + workNum_lab.Text + " AND DepID = au_audep.id";
                
                workNum_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["WorkerNum"].ToString();
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["EnFName"].ToString();
                dep_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["Description"].ToString();
                string dep = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["DepID"].ToString();
                now_dep_lab.Text = dep;
                start_date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                end_date_text.Text = DateTime.Now.ToString("yyyy-MM-dd");
                start_time_text.Text = "8:30";
                end_time_text.Text = "17:30";

                str = "select * from au_audep where id != " + dep + ";";
                HR_class.setDropdownlist(str, "Description", "id", dep_dl);
                getSupDl();

                str = "select * from hr_support_reason";
                HR_class.setDropdownlist(str, "description", "id", reason_dl);
                                
                if (Session["check_sn"] != null) //若session存在，表示審核
                {
                    form_panel.Enabled = false;

                    string sn = Session["check_sn"].ToString();
                    string ap = Session["check_ap"].ToString();
                    
                    str = "select * from hr_application_support where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    int many = int.Parse(dt.Rows[0]["many"].ToString().Trim());
                    DateTime dd;
                    dd = DateTime.Parse(dt.Rows[0]["start_time"].ToString());
                    start_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                    start_time_text.Text = dd.ToString(@"HH : mm");
                    dd = DateTime.Parse(dt.Rows[0]["end_time"].ToString());
                    end_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                    end_time_text.Text = dd.ToString(@"HH : mm");
                    reason_dl.SelectedValue = dt.Rows[0]["reason"].ToString().Trim();
                    ViewState["status"] = dt.Rows[0]["status"].ToString();

                    str = @"
                    select hr_application_support_detail.workerNum, au_computerform.EnFName
                    from hr_application_support_detail, au_computerform 
                    where au_computerform.WorkerNum = hr_application_support_detail.workerNum 
                    AND sup_SN = " + sn + ";";
                    str = str.Replace("                    ", string.Empty).Replace("\r\n", " ").Trim();
                    Debug.WriteLine(str);
                    dt = GlobalAnnounce.db.GetDataTable(str);
                    for (int i = 0; i < many; i++)
                    { sup_list.Items.Add(new ListItem(dt.Rows[i]["workerNum"].ToString() + ", " + dt.Rows[i]["EnFName"].ToString(), dt.Rows[i]["workerNum"].ToString())); }
                                        
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
                    str = "select * from hr_application_support where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    int many = int.Parse(dt.Rows[0]["many"].ToString().Trim());
                    DateTime dd;
                    dd = DateTime.Parse(dt.Rows[0]["start_time"].ToString());
                    start_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                    start_time_text.Text = dd.ToString(@"HH : mm");
                    dd = DateTime.Parse(dt.Rows[0]["end_time"].ToString());
                    end_date_text.Text = dd.ToString(@"yyyy-MM-dd");
                    end_time_text.Text = dd.ToString(@"HH : mm");
                    reason_dl.SelectedValue = dt.Rows[0]["reason"].ToString().Trim();

                    str = @"
                    select hr_application_support_detail.workerNum, au_computerform.EnFName
                    from hr_application_support_detail, au_computerform 
                    where au_computerform.WorkerNum = hr_application_support_detail.workerNum 
                    AND sup_SN = " + sn + ";";
                    str = str.Replace("                    ", string.Empty).Replace("\r\n", " ").Trim();
                    Debug.WriteLine(str);
                    dt = GlobalAnnounce.db.GetDataTable(str);
                    for (int i = 0; i < many; i++)
                    { sup_list.Items.Add(new ListItem(dt.Rows[i]["workerNum"].ToString() + ", " + dt.Rows[i]["EnFName"].ToString(), dt.Rows[i]["workerNum"].ToString())); }

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
            end = Convert.ToDateTime(end_date_text.Text + " " + end_time_text.Text + ":00");

            //int status = HR_class.getStatus(Session["auDep5"].ToString());
            int status = 1;
            string str = @"
            insert into hr_application_support(workerNum, many, start_time, end_time, reason, depId, sup_dep, sup_dep_flag, status)
            values(" + workNum_lab.Text + "," + sup_list.Items.Count + ",'" + start.ToString(@"yyyy\/MM\/dd H:mm:ss") + "','" + end.ToString(@"yyyy\/MM\/dd H:mm:ss") + "'," + reason_dl.SelectedValue + "," + now_dep_lab.Text + "," + dep_dl.SelectedValue + "," + now_dep_lab.Text + "," + status + @");
            select LAST_INSERT_ID( )";
            str = str.Replace("            ", string.Empty).Replace("\r\n", " ").Trim();
            Debug.WriteLine(str);
            int i = int.Parse(GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString());
            str = @"insert into hr_application_support_detail(sup_SN, workerNum) values";
            for (int ii = 0; ii < sup_list.Items.Count;ii++ )
            {
                str += @"(" + i + "," + sup_list.Items[ii].Value.Trim() + @"),";
            }
            str = str.Remove(str.Length - 1) + ";";
            str = str.Replace("            ", string.Empty).Replace("\r\n", " ").Trim();
            str += " select LAST_INSERT_ID( );";
            Debug.WriteLine(str);
            string sn = GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=" + HR_class.encodeBase64("4") + "';", true);
        }

        bool checkInput()
        {
            //資料防呆
            bool flag = true;

            bool ff = HR_class.checkDate(start_date_text, start_time_text, end_date_text, end_time_text, hours_lab, datetime_lab);
            if (ff)
            { datetime_lab.Visible = false; }
            else
            {
                datetime_lab.Visible = true;
                flag = false;
            }

            if (sup_list.Items.Count == 0)
            {
                sup_lab.Text = "Please, choose at least one person.";
                sup_lab.Visible = true;
                flag = false;
            }
            else { sup_lab.Visible = false; }
                        
            return flag;
        }

        void getSupDl()
        {
            //抓出該部門可協助人員

            //string str;
            //str = "select *, CONCAT( CAST( hr_employee_detail.WorkerNum AS CHAR ) ,  ', ', Name ) AS fullName from hr_employee_info, hr_employee_detail, deps where hr_employee_info.WorkerNum = hr_employee_detail.WorkerNum AND hr_employee_detail.Dep = deps.id AND";
            //str += " Dep = " + dep_dl.SelectedValue.ToString() + ";";
            //string str = "select *, CONCAT( CAST( au_computerform.WorkerNum AS CHAR ) , ', ', au_computerform.EnFName ) AS fullName "
            //    + "from au_computerform, au_audep "
            //    + "where au_computerform.DepID = au_audep.id "
            //    + "AND DepID = '" + dep_dl.SelectedValue.ToString() + "';";

            string str = "select c.ID, c.WorkerNum, CONCAT(CAST( c.WorkerNum AS CHAR ) , ', ', c.EnFName ) AS fullName "
                + "from au_computerform c, au_audep where c.DepID = au_audep.id "
                + "AND DepID = '" + dep_dl.SelectedValue.ToString() + "';";
            Debug.WriteLine(str);
            HR_class.setDropdownlist(str, "fullName", "WorkerNum", employee_dl);
        }

        protected void dep_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSupDl();
        }

        protected void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sup_list.Items.Count >= 5 || sup_list.Items.Contains(new ListItem(employee_dl.SelectedItem.ToString(), employee_dl.SelectedValue.ToString()))) { return; }
                sup_list.Items.Add(new ListItem(employee_dl.SelectedItem.ToString(), employee_dl.SelectedValue.ToString()));
                sup_lab.Visible = false;
            }
            catch (Exception ee) { return; }
        }

        protected void reset_btn_Click(object sender, EventArgs e)
        {
            sup_list.Items.Clear();
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; }

            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            DateTime start, end;
            start = Convert.ToDateTime(start_date_text.Text + " " + start_time_text.Text + ":00");
            end = Convert.ToDateTime(end_date_text.Text + " " + end_time_text.Text + ":00");

            string str = @"
            update hr_application_support
            set many = " + sup_list.Items.Count + @",
            start_time = '" + start.ToString(@"yyyy\/MM\/dd\/ H:mm:ss") + @"',
            end_time = '" + end.ToString(@"yyyy\/MM\/dd\/ H:mm:ss") + @"',
            reason = " + reason_dl.SelectedValue + " where SN = " + sn + ";";
            str = str.Replace("            ", string.Empty).Replace("\r\n", " ").Trim();            
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);

            str = "delete from hr_application_support_detail where sup_SN = " + sn + "; ";
            str += "insert into hr_application_support_detail(sup_SN, workerNum) values";
            for (int i = 0; i < sup_list.Items.Count; i++)
            {
                str += "(" + sn + "," + sup_list.Items[i].Value.Trim() + @"),";
            }
            str = str.Remove(str.Length - 1) + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been edited.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=4';", true);
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_support set isDelete = 1 where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been deleted.'); window.location.href='HR_list.aspx?SN=-1&type=4';", true);
        }

        protected void employee_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sup_list.Items.Count != 0)
            { sup_lab.Visible = false; }
        }

        protected void start_date_text_TextChanged(object sender, EventArgs e)
        {
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

        protected void pass_btn_Click(object sender, EventArgs e)
        {
            string sn = ViewState["check_sn"].ToString();
            int status = int.Parse(ViewState["status"].ToString());

            string str = "select origin_dep, sup_dep, sup_dep_flag from hr_application_support where SN = " + sn + ";";
            DataTable dt = GlobalAnnounce.db.GetDataTable(str);
            string origin_dep = dt.Rows[0]["origin_dep"].ToString();  //本身部門
            string dep_sup_flag = dt.Rows[0]["sup_dep_flag"].ToString();  //該審核主管部門
            string dep_sup = dt.Rows[0]["sup_dep"].ToString();  //支援部門

            //審核
            string sql = "update HR_application_support ";
            switch (status)
            {
                case (int)HR_class.STATUS.Department_Manager:
                    if (origin_dep == dep_sup_flag)
                    {
                        sql += "set sup_dep_flag = " + dep_sup + " ";
                    }
                    else { sql += "set sup_dep_flag = 0, status = " + (int)HR_class.STATUS.Vice_General_Manager + " "; }
                    break;
                case (int)HR_class.STATUS.Vice_General_Manager:
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
            string sql = "update HR_application_overtime set status = " + (int)HR_class.STATUS.Fail + " where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.'); window.location.href='HR_check.aspx';", true);
        }
    }
}