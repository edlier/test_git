using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.MIS.Manager
{
    public partial class ManagerList : System.Web.UI.Page
    {
        DataTable managerList;
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin, 11);
            if (!IsPostBack)
            {
                managerList = GlobalAnnounce.manager.search_ManagerList();
                grid_managerList.DataSource = managerList;
                grid_managerList.DataBind();
            }

        }


        protected void grid_managerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "myy")
            {
                Button BTN = (Button)e.CommandSource;    //先抓到這個按鈕（我們設定了CommandName）

                GridViewRow myRow = (GridViewRow)BTN.NamingContainer;
                // 從你按下 Button按鈕的時候，NamingContainer知道你按下的按鈕在GridView「哪一列」！

                Label LB = (Label)grid_managerList.Rows[myRow.DataItemIndex].FindControl("ID");
                //按下按鈕之後，這一列的列數（index）-- myRow.DataItemIndex
                GlobalAnnounce.manager.deleteManager(LB.Text);

                Response.Redirect(PageListString.ManagerList);
            }

        }


    }
}