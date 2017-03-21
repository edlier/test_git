using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session[SessionString.userID] == null)
            //{
            //    //Response.Redirect("../account/login.aspx");
            //    Response.Redirect(ResolveClientUrl("account/login.aspx"));
            //}
            string empID = string.Empty;

            empID = Request["empID"] == null ? string.Empty : Request["empID"].ToString();

            if (!string.IsNullOrEmpty(empID) && Session[SessionString.userID] == null)
            {
                Session[SessionString.userID] = empID;
            }
            else if (string.IsNullOrEmpty(empID) && Session[SessionString.userID] == null)
            {
                //Response.Redirect("../account/login.aspx");
                Response.Redirect(ResolveClientUrl("account/login.aspx"));
            }

        }
    }
}