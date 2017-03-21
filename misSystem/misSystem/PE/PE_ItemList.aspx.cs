using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.PE
{
    public partial class PE_ItemList : System.Web.UI.Page
    {
        DataTable taskList = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string NonePE_AU = "../JumpPage.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NonePE_AU, NoneLogin,3);
            taskList = search_TaskList();
            if (!IsPostBack)
            {
                TaskGrid(taskList);
            }
        }
        private DataTable search_TaskList()//SQL join 語法
        {
            string sqlstr = "select t.*, u.missysID, max(p.progress) as currentprog"
                +" from pe_tasks as t, au_userinfo as u, pe_progressdetail as p"
                +" where t.owner=u.ID and t.ID=p.taskID and t.owner="
                  + GlobalAnnounce.db.qo(Session[SessionString.userID].ToString())
                  + " group by t.ID";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);           
            return dt;
        }
        //把資料丟入Gridview --->>>> "grid_tasklist"
        private void TaskGrid(DataTable dt)
        {
            grid_tasklist.DataSource = dt;
            grid_tasklist.DataBind();
        }
        protected void grid_tasklist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grid_tasklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(e.Row.Cells[2].Text)==0)
                {
                    e.Row.Cells[2].Text = "未開始Inactive";
                }
                else if (e.Row.Cells[2].Text == "1") 
                {
                    e.Row.Cells[2].Text = "已開始Active";
                }
                else if (e.Row.Cells[2].Text == "2")
                {
                    e.Row.Cells[2].Text = "已完成 Finished";
                    e.Row.Cells[6].Text = "";
                }
                //for (int i = 0; i < e.Row.Cells.Count; i++)
                //{                   
                //    string statusnum = DataBinder.Eval(e.Row.DataItem, "status").ToString();
                //    if (statusnum == "0")
                //    {
                //        e.Row.Cells[2].Text = "未開始 Inactive";
                //    }
                //    else if (statusnum == "1")
                //    {
                //        e.Row.Cells[2].Text = "已開始 Active";
                //    }
                //    else if (statusnum == "2")
                //    {
                //        e.Row.Cells[2].Text = "已完成 Finished";
                //        e.Row.Cells[6].Text = "";
                //    }
                //}
            }
        }
    }
}