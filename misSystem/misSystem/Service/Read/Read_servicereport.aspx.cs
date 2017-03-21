using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Read_servicereport : System.Web.UI.Page
    {

        DataTable servicereport = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {
                int id;
                id = int.Parse(Request["id"]);

                servicereport = GlobalAnnounce.serviceDB.search_sreport(id);

                pri.Text = servicereport.Rows[0]["pr"].ToString();
                cf.Text = servicereport.Rows[0]["cf"].ToString();

                if (servicereport.Rows[0]["fe1"].ToString() == "1")
                {
                    fe1.Checked = true;
                }
                if (servicereport.Rows[0]["fe2"].ToString() == "1")
                {
                    fe2.Checked = true;
                }
                if (servicereport.Rows[0]["fe3"].ToString() == "1")
                {
                    fe3.Checked = true;
                    fe3msg.Text = servicereport.Rows[0]["fe3msg"].ToString();
                }
                if (servicereport.Rows[0]["fe4"].ToString() == "1")
                {
                    fe4.Checked = true;
                }
                if (servicereport.Rows[0]["fe5"].ToString() == "1")
                {
                    fe5.Checked = true;
                    fe5msg.Text = servicereport.Rows[0]["fe5msg"].ToString();
                }
                if (servicereport.Rows[0]["fe6"].ToString() == "1")
                {
                    fe6.Checked = true;
                    fe6msg.Text = servicereport.Rows[0]["fe6msg"].ToString();
                }



                if (servicereport.Rows[0]["oa"].ToString() == "1")
                {
                    at1.Checked = true;
                    oamsg.Text = servicereport.Rows[0]["oamsg"].ToString();
                }
                if (servicereport.Rows[0]["cm"].ToString() == "1")
                {
                    at2.Checked = true;
                    cmmsg.Text = servicereport.Rows[0]["cmmsg"].ToString();
                }
                if (servicereport.Rows[0]["acmc"].ToString() == "1")
                {
                    at3.Checked = true;
                    acmcmsg.Text = servicereport.Rows[0]["acmcmsg"].ToString();
                }

                if (servicereport.Rows[0]["replaced"].ToString() == "1")
                {
                    at4.Checked = true;
                    if (servicereport.Rows[0]["cin1"].ToString() != "0")
                    {
                        pn1.Text = servicereport.Rows[0]["cin1"].ToString();
                        qty1.Text = servicereport.Rows[0]["qty1"].ToString();
                    }
                    if (servicereport.Rows[0]["cin2"].ToString() != "0")
                    {
                        pn2.Text = servicereport.Rows[0]["cin2"].ToString();
                        qty2.Text = servicereport.Rows[0]["qty2"].ToString();
                    }
                    if (servicereport.Rows[0]["cin3"].ToString() != "0")
                    {
                        pn3.Text = servicereport.Rows[0]["cin3"].ToString();
                        qty3.Text = servicereport.Rows[0]["qty3"].ToString();
                    }
                    if (servicereport.Rows[0]["cin4"].ToString() != "0")
                    {
                        pn4.Text = servicereport.Rows[0]["cin4"].ToString();
                        qty4.Text = servicereport.Rows[0]["qty4"].ToString();
                    }
                    if (servicereport.Rows[0]["cin5"].ToString() != "0")
                    {
                        pn5.Text = servicereport.Rows[0]["cin5"].ToString();
                        qty5.Text = servicereport.Rows[0]["qty5"].ToString();
                    }
                }
                else
                {
                    Table1.Visible = false;
                }



                dataLaborHr.Text = servicereport.Rows[0]["lh"].ToString();
                dataLaborCost.Text = servicereport.Rows[0]["lc"].ToString();
                labelLaborCost.Text = servicereport.Rows[0]["lt"].ToString();
                dataTravelHr.Text = servicereport.Rows[0]["th"].ToString();
                dataTravelCost.Text = servicereport.Rows[0]["tc"].ToString();
                labelTravelCost.Text = servicereport.Rows[0]["tt"].ToString();
                labelTotalCost.Text = servicereport.Rows[0]["totalcost"].ToString();

                if (servicereport.Rows[0]["workstatus"].ToString() == "0")
                {
                    cw.Checked = true;
                    ps.Checked = false;
                }
                else
                {
                    cw.Checked = false;
                    ps.Checked = true;
                }
            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='../ALLlog.aspx';</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> history.go(-2); </script>");
        }
    }
}