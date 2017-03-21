using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.MIS
{
    public partial class MIS_main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU= "../Default.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

        }
    }
}