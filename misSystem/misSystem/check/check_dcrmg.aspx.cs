using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.check
{
    public partial class WebForm1 : overview_dcr
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setValue();
            pdne.Visible = false;
            pcmc.Visible = false;
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string dcrid = Request.QueryString["id"];
                DateTime dateNow = DateTime.Now;
                string date = dateNow.ToString("yyyy/MM/dd");
                int userID = Convert.ToInt32(Session[SessionString.userID]);

                GlobalAnnounce.check_dcr.updateMC(dcrid, date, userID);
                GlobalAnnounce.check_dcr.updatestatus(dcrid, "6");

                Response.Write("<script>alert('通過!');location.href='unconfirmed_form.aspx';</script>");
            }
        }
    }
}