using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class ServiceReport : System.Web.UI.Page
    {
        DataTable flow = new DataTable();
        DataTable status = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.MaintainScrollPositionOnPostBack = true;

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


                log.Text = "<a href='/Service/Read/Read_ServiceLog.aspx?id=" + id + "'>ServiceLog</a>";
                igr.Text = "<a href='/Service/Read/Read_investigation.aspx?id=" + id + "'>InvestigationReport</a>";
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


                int[] fe = new int[7];
                int oaa = 0, cm = 0, acmc = 0, replaced = 0;
                int ws = 0;


                if (fe1.Checked)
                {
                    fe[1] = 1;
                }
                else
                {
                    fe[1] = 0;
                }

                if (fe2.Checked)
                {
                    fe[2] = 1;
                }
                else
                {
                    fe[2] = 0;
                }
                if (fe3.Checked)
                {
                    fe[3] = 1;
                    if (fe3msg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    fe[3] = 0;
                    fe3msg.Text = "";
                }
                if (fe4.Checked)
                {
                    fe[4] = 1;
                }
                else
                {
                    fe[4] = 0;
                }
                if (fe5.Checked)
                {
                    fe[5] = 1;
                    if (fe5msg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    fe[5] = 0;
                    fe5msg.Text = "";
                }
                if (fe6.Checked)
                {
                    fe[6] = 1;
                    if (fe6msg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    fe[6] = 0;
                    fe6msg.Text = "";
                }





                if (at1.Checked)
                {
                    oaa = 1;
                    if (oamsg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    oaa = 0;
                    oamsg.Text = "";
                }

                if (at2.Checked)
                {
                    cm = 1;
                    if (cmmsg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    cm = 0;
                    cmmsg.Text = "";
                }
                if (at3.Checked)
                {
                    acmc = 1;
                    if (acmcmsg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    acmc = 0;
                    acmcmsg.Text = "";
                }
                if (at4.Checked)
                {
                    replaced = 1;
                    if (qty1.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    replaced = 0;
                }




                if (cw.Checked)
                {
                    ws = 0;
                }
                else if (ps.Checked)
                {
                    ws = 1;
                }

                if (pri.Text == "" || cf.Text == "" || dataLaborHr.Text == "" || dataLaborCost.Text == "" || dataTravelHr.Text == "" || dataTravelCost.Text == "")
                {
                    check = false;
                }



                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "7")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertservicereport(id, pri.Text, cf.Text, fe[1], fe[2], fe[3], fe3msg.Text, fe[4], fe[5], fe5msg.Text, fe[6], fe6msg.Text,
                        oaa, oamsg.Text, cm, cmmsg.Text, acmc, acmcmsg.Text, replaced,
                        "test", "test", "test", "test", "test",
                        qty1.Text, qty2.Text, qty3.Text, qty4.Text, qty5.Text,
                        dataLaborHr.Text, dataLaborCost.Text, labelLaborCost.Text, dataTravelHr.Text,
                        dataTravelCost.Text, labelTravelCost.Text, labelTotalCost.Text, ws,
                        Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(8, id);
                        Response.Write("<script>alert('ServiceReport Saved!!');location.href='/Service/Status.aspx';</script>");
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





        protected void opentable(object sender, EventArgs e)
        {
            if(at4.Checked == true)
            {
                Table1.Visible = true;
            }
            else
            {
                Table1.Visible = false;
                Table1.Rows.Clear();
            }
            
        }

        private void calLaborCost()
        {
            float tmpLaborCost = 0;
            float tmpLaborHr = 0;
            float.TryParse(dataLaborCost.Text, out tmpLaborCost);
            float.TryParse(dataLaborHr.Text, out tmpLaborHr);
            labelLaborCost.Text = String.Format("{0:F2}", tmpLaborCost * tmpLaborHr);
        }
        private void calTravelCost()
        {
            float tmpTravelCost = 0;
            float tmpTravelHr = 0;
            float.TryParse(dataTravelCost.Text, out tmpTravelCost);
            float.TryParse(dataTravelHr.Text, out tmpTravelHr);
            labelTravelCost.Text = String.Format("{0:F2}", tmpTravelCost * tmpTravelHr);
        }
        private void calTotalCost()
        {
            float tmpTravel = 0;
            float tmpLabor = 0;
            float.TryParse(labelTravelCost.Text, out tmpTravel);
            float.TryParse(labelLaborCost.Text, out tmpLabor);
            labelTotalCost.Text = String.Format("{0:F2}", tmpTravel + tmpLabor);
        }
        //文字改變,離開後計算
        protected void dataLaborHr_TextChanged(object sender, EventArgs e)
        {
            calLaborCost();
            calTotalCost();
        }
        //文字改變,離開後計算
        protected void dataLaborCost_TextChanged(object sender, EventArgs e)
        {
            calLaborCost();
            calTotalCost();
        }
        //文字改變,離開後計算
        protected void dataTravelHr_TextChanged(object sender, EventArgs e)
        {
            calTravelCost();
            calTotalCost();
        }
        //文字改變,離開後計算
        protected void dataTravelCost_TextChanged(object sender, EventArgs e)
        {
            calTravelCost();
            calTotalCost();
        }
    }
}