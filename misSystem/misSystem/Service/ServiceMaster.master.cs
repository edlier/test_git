using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.Service
{
    public partial class ServiceMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_allLog_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("ALLlog.aspx"));
        }

        protected void btn_FillLog_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("ServiceLog.aspx"));
        }

        protected void btn_pending_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("Status.aspx"));
        }

    }
}