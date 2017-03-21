using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace misSystem.HR
{
    public partial class HR_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HR_class.check_login(this.Page)) { return; }
                 
                string str = "select workerNum, missysID from au_userinfo where ID = " + Session[SessionString.userID] + ";";
                workNum_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["workerNum"].ToString();
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["missysID"].ToString();

                DateTime today = DateTime.Today;
                end_date_text.Text = today.ToString(@"yyyy-MM-dd");
                start_date_text.Text = today.AddDays(-20).ToString(@"yyyy-MM-dd");
                                
                if (Request.QueryString["SN"] != null && Request.QueryString["type"] != null)
                {
                    string sn, type, status;
                    sn = HR_class.decodeBase64(Request.QueryString["SN"]);
                    type = Request.QueryString["type"];
                    type = HR_class.decodeBase64(Request.QueryString["type"]);

                    string[] type_ls = { "leave", "overtime", "workingout", "equipment", "support", "transfer", "hire" };
                    apply_dl.SelectedValue = type_ls[int.Parse(type)];
                    search_form(type_ls[int.Parse(type)], sn, "uncheck");
                }
            }
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            search_form(apply_dl.SelectedValue, "-1", check_dl.SelectedValue);
        }

        void search_form(string type, string sn, string check)
        {
            string str = "";
            if (type == "leave")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, Descript, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status from hr_application_leave inner join hr_leave_reason on hr_application_leave.leave_reason = hr_leave_reason.ID, au_computerform where hr_application_leave.workerNum "; }
            else if (type == "overtime")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, location as Location, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, pay, status from hr_application_overtime, au_computerform where hr_application_overtime.workerNum "; }
            else if (type == "workingout")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status from hr_application_workingout, au_computerform where hr_application_workingout.workerNum "; }
            else if (type == "equipment")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, equipment as Equipment, price, status from hr_application_equipment, au_computerform where hr_application_equipment.workerNum "; }
            else if (type == "support")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, many as Many, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status from hr_application_support, au_computerform where hr_application_support.workerNum "; }
            else if (type == "transfer")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, au_audep.Description as Transfer, au_aulevel.Description as Position, status from hr_application_transfer inner join au_audep on hr_application_transfer.transfer_dep = au_audep.id inner join au_aulevel on hr_application_transfer.position = au_aulevel.ID, au_computerform where hr_application_transfer.workerNum "; }
            else if (type == "hire")
            { str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, apply_no as Apply_Num, au_aulevel.Description as Position, hr_hire_reason.description as Description, status from hr_application_hire inner join hr_hire_reason on hr_application_hire.hire_reason = hr_hire_reason.ID inner join au_aulevel on hr_application_hire.position = au_aulevel.ID, au_computerform where hr_application_hire.workerNum "; }
            str += " = au_computerform.WorkerNum ";
            str += "AND au_computerform.workerNum = " + workNum_lab.Text + " ";
            str += "AND isDelete = 0 ";
            if (sn != "-1") { str += "AND SN = " + sn + " "; }            
            str += "AND (apply_time BETWEEN '" + start_date_text.Text + " 00:00:00' AND '" + end_date_text.Text + " 23:59:59') ";
            if (check == "uncheck")
            { str += "AND (status = 1 or status = 4  or status = 7);"; }
            else if (check == "checking")
            { 
                str += "AND status != 0 AND status != 99 ";
                if (Session["audep5"].ToString() == HR_class.Administration)
                { str += "AND status !=4;"; }
                else { str += "AND status !=1;"; }
            }
            else if (check == "finish")
            { str += "AND status = 0;"; }
            else if (check == "fail")
            { str += "AND status = 99;"; }

            title_lab.Text = apply_dl.SelectedItem.ToString() + " 查詢";
            Debug.WriteLine(str);
            hr_grid.DataSource = GlobalAnnounce.db.GetDataTable(str);
            hr_grid.DataBind();            
        }
        
        protected void edit_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.encodeBase64(((Button)sender).CommandArgument.ToString());
            Response.Redirect("HR_application_" + apply_dl.SelectedValue.Trim() + ".aspx?SN=" + sn);
        }

        protected void hr_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 4)
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            }
        }
                
    }
}