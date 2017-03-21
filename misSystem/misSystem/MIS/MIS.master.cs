using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.MIS
{
    public partial class MIS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_AddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("User/AddNewUser.aspx"));
        }

        protected void btn_Addmanager_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("Manager/AddNewManager.aspx"));
        }

        protected void btn_managerList_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("Manager/ManagerList.aspx"));
        }

        protected void btn_UserList_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("User/UserList.aspx"));
        }
    }
}