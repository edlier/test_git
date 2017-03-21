using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.QC
{
    public partial class QC : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneQC_AU = "../Default.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneQC_AU, NoneLogin, 6);
        }

        protected void btn_fillIQC_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("cnnSAPList.aspx"));
        }
        protected void btn_fillFQC_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("FQC_Fill.aspx"));
        }
        protected void btn_fQCList_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("FQC_List.aspx"));
        }

        protected void btn_IQCList_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("IQC_List.aspx"));
        }

        protected void btn_fillIPQC_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("IPQC_Fill.aspx"));
        }

        protected void btn_IPQCList_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveClientUrl("IPQC_List.aspx"));
        }

        //protected void btn_fillOPQC_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(ResolveClientUrl("OPQC.aspx"));
        //}
    }
}