using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class ServiceFillForm : System.Web.UI.Page
    {
        DataTable psn = new DataTable();
        DataTable flow = new DataTable();

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

                productSN.Text = psn.Rows[0]["productSN"].ToString();

                servicereport.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";
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
                bool check = true;
                int id;
                id = int.Parse(Request["id"]);

                int[] fr = new int[13];
                int rrk = 0, rrq = 0, acttr = 0, vp = 0;


                if (rrk1.Checked)
                {
                    rrk = 1;
                }
                else if (rrk2.Checked)
                {
                    rrk = 0;
                }
                if (rrq1.Checked)
                {
                    rrq = 1;
                }
                else if (rrq2.Checked)
                {
                    rrq = 0;
                }
                if (acttrcheck.Checked)
                {
                    acttr = 1;
                }
                else
                {
                    acttr = 0;
                }


                if (fr1.Checked)
                {
                    fr[1] = 1;
                }
                else
                {
                    fr[1] = 0;
                }

                if (fr2.Checked)
                {
                    fr[2] = 1;
                }
                else
                {
                    fr[2] = 0;
                }
                if (fr3.Checked)
                {
                    fr[3] = 1;
                }
                else
                {
                    fr[3] = 0;
                }
                if (fr4.Checked)
                {
                    fr[4] = 1;
                }
                else
                {
                    fr[4] = 0;
                }
                if (fr5.Checked)
                {
                    fr[5] = 1;
                }
                else
                {
                    fr[5] = 0;
                }
                if (fr6.Checked)
                {
                    fr[6] = 1;
                    if (fr6other.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    fr[6] = 0;
                    fr6other.Text = "";
                }
                if (fr7.Checked)
                {
                    fr[7] = 1;
                }
                else
                {
                    fr[7] = 0;
                }
                if (fr81.Checked)
                {
                    fr[8] = 0;
                    fr8dcr.Text = "";
                }
                else if (fr82.Checked)
                {
                    fr[8] = 1;
                    if (fr8dcr.Text == "")
                    {
                        check = false;
                    }
                }
                if (vp1.Checked)
                {
                    vp = 0;
                    vpmsg.Text = "";
                }
                else if (vp2.Checked)
                {
                    vp = 1;
                    if (vpmsg.Text == "")
                    {
                        check = false;
                    }
                }




                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "9")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertservicefillform(id, rrk, rrq, acttr, fr[1], fr[2], fr[3], fr[4], fr[5], fr[6], fr6other.Text, fr[7], fr[8], fr8dcr.Text, vp, vpmsg.Text, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(10, id);
                        Response.Write("<script>alert('ServiceFillForm Saved!!');location.href='/Service/Status.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('有資料未填!!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('資料重複!!');</script>");
                }


            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='/Service/Status.aspx';</script>");
            }
            

        }
    }
}