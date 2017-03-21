using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.regulatory
{
    public partial class regulatory_main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Form_DCR_Click(object sender, EventArgs e)
        {
            Response.Redirect("regulatory_form_DCR.aspx");
        }
    }
}