using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class FMEA : System.Web.UI.Page
    {
        DataTable capa = new DataTable();
        DataTable flow = new DataTable();
        DataTable status = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {

                int id;
                id = int.Parse(Request["id"]);

                status = GlobalAnnounce.serviceDB.search_status(id);

                if (status.Rows[0]["IR"].ToString() == "0")
                {

                    IR.Visible = false;
                }

                ID.Text = "ServiceLogID :" + id;
                log.Text = "<a href='/Service/Read/Read_ServiceLog.aspx?id=" + id + "'>ServiceLog</a>";
                sr.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";
                igr.Text = "<a href='/Service/Read/Read_investigation.aspx?id=" + id + "'>InvestigationReport</a>";

                capa = GlobalAnnounce.serviceDB.search_capa(id);

                ID.Text = "ServiceLogID :" + capa.Rows[0]["servicelogID"].ToString();

                if (capa.Rows[0]["dtctype"].ToString() == "Potential")
                {
                    dtc1.Checked = true;
                }
                else
                {
                    dtc2.Checked = true;
                }

                describec.Text = capa.Rows[0]["dtc"].ToString();
                aobrc.Text = capa.Rows[0]["aobrc"].ToString();

                ia1.Text = capa.Rows[0]["ia1"].ToString();
                ia2.Text = capa.Rows[0]["ia2"].ToString();
                ia3.Text = capa.Rows[0]["ia3"].ToString();

                rrd1.Text = capa.Rows[0]["rd1"].ToString();
                rrd2.Text = capa.Rows[0]["rd2"].ToString();
                rrd3.Text = capa.Rows[0]["rd3"].ToString();

                ccd1.Text = capa.Rows[0]["cd1"].ToString();
                ccd2.Text = capa.Rows[0]["cd2"].ToString();
                ccd3.Text = capa.Rows[0]["cd3"].ToString();


                ap1.Text = capa.Rows[0]["APredress1"].ToString();
                ap2.Text = capa.Rows[0]["APredress2"].ToString();
                ap3.Text = capa.Rows[0]["APredress3"].ToString();

                rd1.Text = capa.Rows[0]["APRD1"].ToString();
                rd2.Text = capa.Rows[0]["APRD2"].ToString();
                rd3.Text = capa.Rows[0]["APRD3"].ToString();

                cd1.Text = capa.Rows[0]["APDD1"].ToString();
                cd2.Text = capa.Rows[0]["APDD2"].ToString();
                cd3.Text = capa.Rows[0]["APDD3"].ToString();

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
                bool check = true;
                int fmea = 0;
                string fmeamsg = "null";
                if (rfyes.Checked)
                {
                    if (yes.Text == "" || yes2.Text == "")
                    {
                        check = false;
                    }
                    else
                    {
                        fmeamsg = "FA-" + yes.Text + "-" + yes2.Text;
                        fmea = 1;
                    }

                }
                else if (rfno.Checked)
                {
                    if (no.Text == "")
                    {
                        check = false;
                    }
                    else
                    {
                        fmeamsg = no.Text;
                        fmea = 0;
                    }

                }



                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "16")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertfmea(id, fmea, fmeamsg, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(17, id);



                        Response.Write("<script>alert('FMEA Saved!!');location.href='/Service/Status.aspx';</script>");
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