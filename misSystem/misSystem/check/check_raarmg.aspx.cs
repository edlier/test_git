using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace misSystem.check
{
    public partial class check_raarmg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string raarid = Request.QueryString["id"].ToString();
            DataTable dtsub = new DataTable();
            dtsub = GlobalAnnounce.check_raa.getsubject(raarid);

            lbl_sub1.Text = dtsub.Rows[0]["subject1"].ToString();
            lbl_sub2.Text = dtsub.Rows[0]["subject2"].ToString();
            lbl_sub3.Text = dtsub.Rows[0]["subject3"].ToString();
            lbl_sub4.Text = dtsub.Rows[0]["subject4"].ToString();
            lbl_sub5.Text = dtsub.Rows[0]["subject5"].ToString();

            //HtmlGenericControl a1 = new HtmlGenericControl("a");
            a1.Attributes.Add("href", "overview_raa.aspx?id=" + raarid);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            GlobalAnnounce.check_raa.updatestatus(Request.QueryString["id"].ToString(), "3");
            Response.Redirect("unconfirmed_form.aspx");
        }
    }
}