using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.PE
{
    public partial class PE_ProgressDetail : System.Web.UI.Page
    {
        DataTable taskList = new DataTable();
        string catchId;
        protected void Page_Load(object sender, EventArgs e)
        {
            string NonePE_AU = "../JumpPage.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NonePE_AU, NoneLogin, 3);
            taskList = search_ProgList();
           
            lbl_ShowTitle.Text = get_TaskName();
            if (!IsPostBack)
            {
                ProgGrid(taskList);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            catchId = Request.Url.Query.Substring(4);
            //Response.Write(catchId); 
            get_descriptions();
            get_projectoutput();
        }
        private void get_descriptions()
        {
            string sqlstr = "select descriptionname"
                + " from  pe_descriptions"
                + " where taskID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                lb_descriptions.Items.Add((i+1).ToString()+". "+(dt.Rows[i][0]).ToString());
            }
        }
        private void get_projectoutput()
        {
            string sqlstr = "select projectoutput"
                + " from  pe_projectoutput"
                + " where taskID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lb_enditems.Items.Add((i + 1).ToString() + ". " + (dt.Rows[i][0]).ToString());
            }
        }
        private string get_TaskName()
        {
            string sqlstr = "select itemname"
                + " from  pe_tasks"
                + " where ID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            string name="";
            name = dt.Rows[0]["itemname"].ToString();
            return name;
        }

        private DataTable search_ProgList()
        {
            string sqlstr = "select p.*"
                + " from  pe_progressdetail as p"
                + " where p.taskID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            return dt;
        }

        private void ProgGrid(DataTable dt)
        { 
            DataTable tDt2 = new DataTable();
            //tDt2.Columns.Add("Date");           
            DataTable tDt = new DataTable();
            tDt.Columns.Add("日期／時間(Date/Time)");
            tDt.Rows.Add("進度(Progress, in %)");
            for (int k = 1; k <= 5; k++)
            {
                tDt.Rows.Add(k.ToString());               
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //以第一個欄位當新Table的欄位名稱
                tDt.Columns.Add(dt.Rows[i][2].ToString());
                //第二個欄位當該Row的值
                for (int j = 3; j < dt.Columns.Count; j++)
                {
                    //tDt.Rows.Add();
                    tDt.Rows[j-3][i+1] = dt.Rows[i][j];
                }
            }
            grid_showprogress.DataSource = tDt;
            grid_showprogress.DataBind();
        }
     
        protected void grid_showprogress_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void bt_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PE_ItemList.aspx");
        }

        protected void lb_descriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
           lbl_descriptions.Text= lb_descriptions.SelectedValue;
        }

        protected void lb_enditems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_enditems.Text=lb_enditems.SelectedValue;
        }
    }
}