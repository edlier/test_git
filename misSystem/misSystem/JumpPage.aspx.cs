using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem
{
    public partial class JumpPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Test", "<script>alert('You don't have the permission to access here!');location.href='Default.aspx';</script>");
            Response.Write("<script>alert('Permission Error!');location.href='Default.aspx'; </script>");
            //Response.Write("<script>alert('You don't have the permission to access here!');location.href='Default.aspx';</script>");
        }
    }
}