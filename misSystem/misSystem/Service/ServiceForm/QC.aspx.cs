using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class QC : System.Web.UI.Page
    {
        DataTable flowtype = new DataTable();
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

                int[] qc = new int[13];


                if (QC1.Checked)
                {
                    qc[1] = 1;
                }
                else
                {
                    qc[1] = 0;
                }

                if (QC2.Checked)
                {
                    qc[2] = 1;
                }
                else
                {
                    qc[2] = 0;
                }
                if (QC3.Checked)
                {
                    qc[3] = 1;
                }
                else
                {
                    qc[3] = 0;
                }
                if (QC4.Checked)
                {
                    qc[4] = 1;
                }
                else
                {
                    qc[4] = 0;
                }
                if (QC5.Checked)
                {
                    qc[5] = 1;
                    if (QC5accesories.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    qc[5] = 0;
                    QC5accesories.Text = "";
                }
                if (QC6.Checked)
                {
                    qc[6] = 1;
                }
                else
                {
                    qc[6] = 0;
                }
                if (QC7.Checked)
                {
                    qc[7] = 1;
                }
                else
                {
                    qc[7] = 0;
                }
                if (QC8.Checked)
                {
                    qc[8] = 1;
                }
                else
                {
                    qc[8] = 0;
                }
                if (QC9.Checked)
                {
                    qc[9] = 1;
                }
                else
                {
                    qc[9] = 0;
                }
                if (QC10.Checked)
                {
                    qc[10] = 1;
                }
                else
                {
                    qc[10] = 0;
                }
                if (QC11.Checked)
                {
                    qc[11] = 1;
                }
                else
                {
                    qc[11] = 0;
                }
                if (QC12.Checked)
                {
                    qc[12] = 1;
                    if (QC12other.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    qc[12] = 0;
                    QC12other.Text = "";
                }




                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "3")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertqc(id, qc[1], qc[2], qc[3], qc[4], qc[5], QC5accesories.Text, qc[6], qc[7], qc[8], qc[9], qc[10], qc[11], qc[12], QC12other.Text, Session[SessionString.userID].ToString());
                        flowtype = GlobalAnnounce.serviceDB.search_flowtype(id);
                        if (flowtype.Rows[0]["IR"].ToString() == "0")
                        {
                            GlobalAnnounce.serviceDB.updateflow(7, id);
                        }
                        else
                        {
                            GlobalAnnounce.serviceDB.updateflow(4, id);
                        }



                        Response.Write("<script>alert('QC Examine Saved!!');location.href='/Service/Status.aspx';</script>");
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