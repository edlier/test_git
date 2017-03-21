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
    public partial class HR_check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["check_ap"] = apply_dl.SelectedValue.Trim();

            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }

                string str = "select workerNum, missysID from au_userinfo where ID = " + Session[SessionString.userID] + ";";
                level_lab.Text = Session[SessionString.level].ToString();
                workNum_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["workerNum"].ToString();
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["missysID"].ToString();
                str = "select DepID from au_computerform where WorkerNum = " + workNum_lab.Text + ";";
                dep_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0][0].ToString();

                DateTime today = DateTime.Today;
                end_date_text.Text = today.ToString(@"yyyy-MM-dd");
                start_date_text.Text = today.AddDays(-20).ToString(@"yyyy-MM-dd");                
            }
        }

        void search_form(string type, string statusSql)
        {
            //依選擇的表單，串接各表單sql為依照登入者身份，顯示需審核表單
            string str = "";
            if (type == "leave")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, Descript, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_leave inner join hr_leave_reason on hr_application_leave.leave_reason = hr_leave_reason.ID, au_computerform where hr_application_leave.workerNum "; }
            else if (type == "overtime")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, location as Location, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, pay, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_overtime, au_computerform where hr_application_overtime.workerNum "; }
            else if (type == "workingout")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_workingout, au_computerform where hr_application_workingout.workerNum "; }
            else if (type == "equipment")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, equipment as Equipment, price, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_equipment, au_computerform where hr_application_equipment.workerNum "; }
            else if (type == "support")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, many as Many, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_support, au_computerform where (sup_dep_flag = " + dep_lab.Text + " or sup_dep_flag = 0) AND  hr_application_support.workerNum "; }
            else if (type == "transfer")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, au_audep.Description as Transfer, au_aulevel.Description as Position, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_transfer inner join au_audep on hr_application_transfer.transfer_dep = au_audep.id inner join au_aulevel on hr_application_transfer.position = au_aulevel.ID, au_computerform where (dep_turn = " + dep_lab.Text + " or dep_turn = 0) AND hr_application_transfer.workerNum "; }
            else if (type == "hire")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, apply_no as Apply_Num, au_aulevel.Description as Position, hr_hire_reason.description as Description, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_hire inner join hr_hire_reason on hr_application_hire.hire_reason = hr_hire_reason.ID inner join au_aulevel on hr_application_hire.position = au_aulevel.ID, au_computerform where hr_application_hire.workerNum "; }
            str += " = au_computerform.WorkerNum ";
            str += "AND isDelete = 0 ";            
            str += "AND (apply_time BETWEEN '" + start_date_text.Text + " 00:00:00' AND '" + end_date_text.Text + " 23:59:59') ";
            str += "AND " + statusSql + ";";
            Debug.WriteLine(str);

            hr_grid1.DataSource = GlobalAnnounce.db.GetDataTable(str);
            hr_grid1.DataBind();
            if (hr_grid1.Rows.Count == 0)
            { title_lab1.Visible = false; }
            else
            { 
                title_lab1.Visible = true;
            }
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            title_lab.Text = apply_dl.SelectedItem.ToString();
            title_lab.Visible = true;
            
            string str = "select au_managerlist.* from au_managerlist, au_userinfo "
                    + "where au_managerlist.userID = au_userinfo.ID "
                    + "AND au_userinfo.workerNum=" + workNum_lab.Text + ";";

            DataTable dt = GlobalAnnounce.db.GetDataTable(str);
            string statusStr = "(";
            //串接權限字串，為傳入search_form()，以查詢該審核表單
            foreach (DataRow r in dt.Rows)
            {
                switch (r["level"].ToString())
                {
                    case "1":
                        if (r["DepID"].ToString() == HR_class.Administration)
                        { statusStr += "status=" + (int)HR_class.STATUS.Administration_Employee + " or "; }
                        break;
                    case "3":
                        if (r["DepID"].ToString() == HR_class.Administration)//行政主管
                        { statusStr += "status=" + (int)HR_class.STATUS.Administration_Manager + " or "; }
                        //else { statusStr += "(status=" + (int)HR_class.STATUS.Department_Manager + " AND DepID=" + dep_lab.Text + ") or "; }
                        statusStr += "(status=" + (int)HR_class.STATUS.Department_Manager;

                        if (apply_dl.SelectedValue.Trim() == "transfer" || apply_dl.SelectedValue.Trim() == "support")
                        { statusStr += ") or "; }
                        else { statusStr += " AND DepID=" + dep_lab.Text + ") or "; }

                        break;
                    case "4":
                        statusStr += "status=" + (int)HR_class.STATUS.Vice_General_Manager + " or ";
                        break;
                    case "5":
                        statusStr += "status=" + (int)HR_class.STATUS.General_Manager + " or ";
                        break;
                    case "9":
                        statusStr += "status=" + (int)HR_class.STATUS.HR + " or ";
                        break;
                }
            }
            if (statusStr.Length > 4)
            { statusStr = statusStr.Remove(statusStr.Length - 4) + ")"; }
            Debug.WriteLine(statusStr);

            search_form(apply_dl.SelectedValue, statusStr);

            Session["check_ap"] = apply_dl.SelectedValue.Trim();
        }

        protected void hr_grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 4)
            {
                //隱藏後三欄gridview
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 3].Visible = false;
            }
        }

        protected void check_btn1_Click(object sender, EventArgs e)
        {
            //至審核頁

            //int rowIndex = int.Parse(((Button)sender).CommandArgument);
            string str = ((Button)sender).CommandArgument.ToString();
            Session["check_sn"] = (str.Split(','))[0];
            Session["check_workerNum"] = (str.Split(','))[1];
            Response.Redirect("HR_application_" + Session["check_ap"] + ".aspx");
        }
    }
}