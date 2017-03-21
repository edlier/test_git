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
    public partial class HR_check_proxy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["check_ap"] = "proxy_leave"; //代理人審核狀態

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
            //串連sql指令，顯示代理人為使用者的表單
            string str = "";
            str = "select SN, au_computerform.EnFName as Name, DATE_FORMAT(apply_time,'%Y-%m-%d %H:%i') as Apply, Descript, DATE_FORMAT(start_time,'%Y-%m-%d %H:%i') as Start, DATE_FORMAT(end_time,'%Y-%m-%d %H:%i') as End, status, au_computerform.DepID, au_computerform.WorkerNum from hr_application_leave inner join hr_leave_reason on hr_application_leave.leave_reason = hr_leave_reason.ID, au_computerform where hr_application_leave.workerNum "; 
            str += " = au_computerform.WorkerNum ";
            str += "AND isDelete = 0 ";
            str += "AND (apply_time BETWEEN '" + start_date_text.Text + " 00:00:00' AND '" + end_date_text.Text + " 23:59:59') ";
            str += "AND " + statusSql + ";";
            Debug.WriteLine(str);

            hr_grid1.DataSource = GlobalAnnounce.db.GetDataTable(str);
            hr_grid1.DataBind();
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {            
            title_lab.Visible = true;

            string statusStr = "proxy=" + workNum_lab.Text + " AND status=" + (int)HR_class.STATUS.Proxy + " ";

            search_form("proxy_leave", statusStr);
        }

        protected void hr_grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 4)
            {
                //隱藏後3欄gridview
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;
                e.Row.Cells[e.Row.Cells.Count - 3].Visible = false;
            }
        }

        protected void check_btn1_Click(object sender, EventArgs e)
        {
            //int rowIndex = int.Parse(((Button)sender).CommandArgument);
            string str = ((Button)sender).CommandArgument.ToString();
            Session["check_sn"] = (str.Split(','))[0];
            Session["check_workerNum"] = (str.Split(','))[1];
            Response.Redirect("HR_application_leave.aspx");
        }
    }
}