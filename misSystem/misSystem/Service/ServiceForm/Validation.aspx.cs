﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Validation : System.Web.UI.Page
    {

        DataTable capa = new DataTable();
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

                rd1.Text = capa.Rows[0]["rd1"].ToString();
                rd2.Text = capa.Rows[0]["rd2"].ToString();
                rd3.Text = capa.Rows[0]["rd3"].ToString();

                cd1.Text = capa.Rows[0]["cd1"].ToString();
                cd2.Text = capa.Rows[0]["cd2"].ToString();
                cd3.Text = capa.Rows[0]["cd3"].ToString();



                ap1.Text = capa.Rows[0]["APredress1"].ToString();
                ap2.Text = capa.Rows[0]["APredress2"].ToString();
                ap3.Text = capa.Rows[0]["APredress3"].ToString();

                aprd1.Text = capa.Rows[0]["APRD1"].ToString();
                aprd2.Text = capa.Rows[0]["APRD2"].ToString();
                aprd3.Text = capa.Rows[0]["APRD3"].ToString();

                apdd1.Text = capa.Rows[0]["APDD1"].ToString();
                apdd2.Text = capa.Rows[0]["APDD2"].ToString();
                apdd3.Text = capa.Rows[0]["APDD3"].ToString();

                if (capa.Rows[0]["fmea"].ToString() == "0")
                {
                    rfno.Checked = true;
                    no.Text = capa.Rows[0]["fmeamsg"].ToString();
                }
                else
                {
                    rfyes.Checked = true;
                    yes.Text = capa.Rows[0]["fmeamsg"].ToString();
                }
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




                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "17")
                {
                    if (validation.Text == "")
                    {
                        Response.Write("<script>alert('未填寫!!');</script>");
                    }
                    else
                    {
                        GlobalAnnounce.serviceDB.insertvalidation(id, validation.Text, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(18, id);
                        Response.Write("<script>alert('Validation Saved!!');location.href='/Service/Status.aspx';</script>");
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