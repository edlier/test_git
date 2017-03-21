using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Check_supervisor : System.Web.UI.Page
    {

        DataTable psn = new DataTable();
        DataTable sff = new DataTable();
        DataTable dp = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {


                int id;
                id = int.Parse(Request["id"]);

                psn = GlobalAnnounce.serviceDB.search_slog(id);
                sff = GlobalAnnounce.serviceDB.search_sff(id);
                dp = GlobalAnnounce.serviceDB.search_dp(id);

                productSN.Text = psn.Rows[0]["productSN"].ToString();

                servicereport.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";

                if (sff.Rows[0]["rrk"].ToString() == "1")
                {
                    rrk1.Checked = true;
                }
                else
                {
                    rrk2.Checked = true;
                }

                if (sff.Rows[0]["rrq"].ToString() == "1")
                {
                    rrq1.Checked = true;
                }
                else
                {
                    rrq2.Checked = true;
                }

                if (sff.Rows[0]["acttr"].ToString() == "1")
                {
                    acttrcheck.Checked = true;
                }


                if (sff.Rows[0]["fr1"].ToString() == "1")
                {
                    fr1.Checked = true;
                }

                if (sff.Rows[0]["fr2"].ToString() == "1")
                {
                    fr2.Checked = true;
                }

                if (sff.Rows[0]["fr3"].ToString() == "1")
                {
                    fr3.Checked = true;
                }

                if (sff.Rows[0]["fr4"].ToString() == "1")
                {
                    fr4.Checked = true;
                }

                if (sff.Rows[0]["fr5"].ToString() == "1")
                {
                    fr5.Checked = true;
                }

                if (sff.Rows[0]["fr6"].ToString() == "1")
                {
                    fr6.Checked = true;

                    fr6other.Text = sff.Rows[0]["fr6other"].ToString();
                }

                if (sff.Rows[0]["fr7"].ToString() == "1")
                {
                    fr7.Checked = true;
                }

                if (sff.Rows[0]["fr8"].ToString() == "1")
                {
                    fr82.Checked = true;
                    fr8dcr.Text = sff.Rows[0]["fr8dcr"].ToString();
                }
                else
                {
                    fr81.Checked = true;
                }

                if (sff.Rows[0]["vp"].ToString() == "1")
                {
                    vp2.Checked = true;
                    vpmsg.Text = sff.Rows[0]["vpmsg"].ToString();
                }
                else
                {
                    vp1.Checked = true;
                }


                if (dp.Rows[0]["dp"].ToString() == "Returned reworked")
                {
                    dp1.Checked = true;
                }
                else if (dp.Rows[0]["dp"].ToString() == "Restock")
                {
                    dp2.Checked = true;
                }
                else if (dp.Rows[0]["dp"].ToString() == "Disposed")
                {
                    dp3.Checked = true;
                }

                outdate.Text = dp.Rows[0]["outdate"].ToString();

                if (dp.Rows[0]["chargeable"].ToString() == "1")
                {
                    cc1.Checked = true;
                    chargeablemsg.Text = dp.Rows[0]["chargeablemsg"].ToString();
                }
                else
                {
                    cc2.Checked = true;
                }

                spi.Text = dp.Rows[0]["spino"].ToString();
                tn.Text = dp.Rows[0]["trackingno"].ToString();
            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='/Service/Status.aspx';</script>");
            }

        }
        protected void btn_set_Click(object sender, EventArgs e)
        {
            try
            {


                int id;
                id = int.Parse(Request["id"]);

                GlobalAnnounce.serviceDB.updateflow(12, id);



                Response.Write("<script>alert('Checked!!');location.href='/Service/Status.aspx';</script>");
            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='/Service/Status.aspx';</script>");
            }
        }
    }
}