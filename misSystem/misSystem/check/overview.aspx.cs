using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.check
{
    public partial class overview : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../Default.aspx";
            string NoneLogin = "../account/login.aspx";
            //GlobalAnnounce.validateSession.validateMIS_AU(this.Page, NoneMIS_AU, NoneLogin);

            dt = GlobalAnnounce.overview.get_dcr();
            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gridview1.PageIndex = e.NewPageIndex;
            dt = GlobalAnnounce.overview.get_dcr();
            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }
    }
}