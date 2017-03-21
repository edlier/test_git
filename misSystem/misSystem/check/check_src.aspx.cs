using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.check
{
    public partial class check_src : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if(Request.QueryString["id"] != null)
                {
                    int dcrid = int.Parse(Request.QueryString["id"]);
                    DataTable basic = GlobalAnnounce.check_dcr.getDCR_basic(dcrid);

                    try
                    {
                        Label1.Text = basic.Rows[0]["productModel"].ToString();
                        Label2.Text = basic.Rows[0]["DCRassignedNo"].ToString();
                        Label3.Text = basic.Rows[0]["RnDprojectName"].ToString();
                        Label4.Text = GlobalAnnounce.id.getName(int.Parse(basic.Rows[0]["changePerposedBy"].ToString()));
                        Label5.Text = DateTime.Parse(basic.Rows[0]["changePerposeDate"].ToString()).ToString("yyyy-MM-dd");
                        Label6.Text = basic.Rows[0]["priority"].ToString();
                    }
                    catch (Exception ee)
                    { }
                    rev.Attributes.Add("href", "overview_dcr.aspx?id=" + dcrid);
                }
                
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            GlobalAnnounce.check_dcr.updatestatus(Request.QueryString["id"].ToString(), "0");
            Response.Redirect("unconfirmed_form.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}