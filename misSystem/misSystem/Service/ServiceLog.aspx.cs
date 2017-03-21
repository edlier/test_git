using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.sevice
{
    public partial class ServiceLog : System.Web.UI.Page
    {

        string creceivedby;
        int crtype = 0, rmatype = 0, flowtype, IR;
        int[] compliant = new int[9];



        protected void Page_Load(object sender, EventArgs e)
        {

            string NoneMIS_AU = PageListString.oneJumpP;
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,4);
            if (!IsPostBack)
            {
                company.DataSource = GlobalAnnounce.customerDetail.getCommentAndPidDataTable();
                company.DataBind();
            }
            
        }



        protected void btn_set_Click(object sender, EventArgs e)
        {


            try
            {


                if (contactname.Text == "")
                {
                    Response.Write("<script>alert('Contact Person 未填!!');</script>");
                }
                if (cpemail.Text == "")
                {
                    Response.Write("<script>alert('Contact Person Email 未填!!');</script>");
                }
                if (productSN.Text == "")
                {
                    Response.Write("<script>alert('Product SN 未填!!');</script>");
                }


                if (!c1.Checked && !c2.Checked && !c3.Checked && !c4.Checked && !c5.Checked && !c6.Checked && !c7.Checked && !c8.Checked)
                {
                    Response.Write("<script>alert('Type of Complaint 一項都未填!!');</script>");
                }

                bool check = true;

                if (Fax.Checked)
                {
                    creceivedby = "Fax";
                }
                else if (Email.Checked)
                {
                    creceivedby = "Email";
                }
                else if (Phone.Checked)
                {
                    creceivedby = "Phone";
                }
                else
                {
                    creceivedby = "Other";
                    if (othermsg.Text == "")
                    {
                        Response.Write("<script>alert('Complaint Received Other message 未填!!');</script>");
                        check = false;
                    }
                }
                if (describec.Text == "")
                {
                    Response.Write("<script>alert('Describe the Complaint 未填!!');</script>");
                }

                if (CR.Checked && !RMA.Checked)
                {
                    if (iny.Checked == true)
                    {
                        IR = 1;
                    }
                    else
                    {
                        IR = 0;
                    }
                    crtype = 1;
                    rmatype = 0;
                    flowtype = 2;

                }
                else if (RMA.Checked && !CR.Checked)
                {
                    rmatype = 1;
                    crtype = 0;
                    flowtype = 3;
                    IR = 0;
                }
                else if (RMA.Checked && CR.Checked)
                {
                    rmatype = 1;
                    crtype = 1;
                    flowtype = 1;
                    IR = 1;
                }
                else if(!CR.Checked && !RMA.Checked)
                {
                    rmatype = 0;
                    crtype = 0;
                    flowtype = 0;
                    IR = 0;
                }



                if (c1.Checked)
                {
                    compliant[1] = 1;
                }
                else
                {
                    compliant[1] = 0;
                }
                if (c2.Checked)
                {
                    compliant[2] = 1;
                }
                else
                {
                    compliant[2] = 0;
                }
                if (c3.Checked)
                {
                    compliant[3] = 1;
                }
                else
                {
                    compliant[3] = 0;
                }
                if (c4.Checked)
                {
                    compliant[4] = 1;
                }
                else
                {
                    compliant[4] = 0;
                }
                if (c5.Checked)
                {
                    compliant[5] = 1;
                }
                else
                {
                    compliant[5] = 0;
                }
                if (c6.Checked)
                {
                    compliant[6] = 1;
                    if (c6msg.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    compliant[6] = 0;
                    c6msg.Text = "";
                }
                if (c7.Checked)
                {
                    compliant[7] = 1;
                    if (c7other.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    compliant[7] = 0;
                    c7other.Text = "";
                }
                if (c8.Checked)
                {
                    compliant[8] = 1;

                    if (c8mname.Text == "" || c8msn.Text == "")
                    {
                        check = false;
                    }
                }
                else
                {
                    compliant[8] = 0;
                    c8mname.Text = "";
                    c8msn.Text = "";
                }



                if (contactname.Text == "" || cpemail.Text == "" || productSN.Text == "" || describec.Text == "")
                {
                    check = false;
                }




                if (check == true)
                {

                    if(flowtype == 0)
                    {
                        GlobalAnnounce.serviceDB.insertSR(company.Text, contactname.Text, cpemail.Text, productSN.Text, 777, creceivedby, othermsg.Text, describec.Text, crtype, rmatype, IR, flowtype, 21, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.inserttoc(777, compliant[1], compliant[2], compliant[3], compliant[4], compliant[5], compliant[6], c6msg.Text, compliant[7], c7other.Text, compliant[8], c8mname.Text, c8msn.Text);
                        GlobalAnnounce.serviceDB.update();

                        Response.Write("<script>alert('ServiceLog Saved!!');location.href='Status.aspx';</script>");
                    }
                    else
                    {
                        GlobalAnnounce.serviceDB.insertSR(company.Text, contactname.Text, cpemail.Text, productSN.Text, 777, creceivedby, othermsg.Text, describec.Text, crtype, rmatype, IR, flowtype, 2, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.inserttoc(777, compliant[1], compliant[2], compliant[3], compliant[4], compliant[5], compliant[6], c6msg.Text, compliant[7], c7other.Text, compliant[8], c8mname.Text, c8msn.Text);
                        GlobalAnnounce.serviceDB.update();

                        Response.Write("<script>alert('ServiceLog Saved!!');location.href='Status.aspx';</script>");
                    }

                }
                else
                {
                    Response.Write("<script>alert('有資料未填!!');</script>");
                }
            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='Status.aspx';</script>");
            }

            
        }

        protected void CR_CheckedChanged(object sender, EventArgs e)
        {
            if(CR.Checked && !RMA.Checked)
            {
                irr.Visible = true;
            }
            else
            {
                irr.Visible = false;
                
            }
        }

        protected void RMA_CheckedChanged(object sender, EventArgs e)
        {
            if (RMA.Checked)
            {
                irr.Visible = false;
            }
            if (!RMA.Checked && CR.Checked)
            {
                irr.Visible = true;
            }
        }
        

    }
}