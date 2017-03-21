using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Read_ServiceLog : System.Web.UI.Page
    {

        DataTable servicelog = new DataTable();
        DataTable cpname = new DataTable();
        DataTable Complaint = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {

                int id;
                id = int.Parse(Request["id"]);



                        servicelog = GlobalAnnounce.serviceDB.search_slog(id);
                        cpname = GlobalAnnounce.serviceDB.search_company(servicelog.Rows[0]["companyID"].ToString());

                        name.Text = cpname.Rows[0]["customerName"].ToString();
                        ID.Text = servicelog.Rows[0]["ID"].ToString();
                        cp.Text = servicelog.Rows[0]["contactname"].ToString();
                        cpemail.Text = servicelog.Rows[0]["email"].ToString();
                        productsn.Text = servicelog.Rows[0]["productSN"].ToString();

                        Complaint = GlobalAnnounce.serviceDB.search_complaint(id);

                        if (Complaint.Rows[0]["c1"].ToString() == "1")
                        {
                            c1.Checked = true;
                        }
                        if (Complaint.Rows[0]["c2"].ToString() == "1")
                        {
                            c2.Checked = true;
                        }
                        if (Complaint.Rows[0]["c3"].ToString() == "1")
                        {
                            c3.Checked = true;
                        }
                        if (Complaint.Rows[0]["c4"].ToString() == "1")
                        {
                            c4.Checked = true;
                        }
                        if (Complaint.Rows[0]["c5"].ToString() == "1")
                        {
                            c5.Checked = true;
                        }
                        if (Complaint.Rows[0]["c6"].ToString() == "1")
                        {
                            c6.Checked = true;
                            c6msg.Text = Complaint.Rows[0]["c6msg"].ToString();
                        }
                        if (Complaint.Rows[0]["c7"].ToString() == "1")
                        {
                            c7.Checked = true;
                            c7other.Text = Complaint.Rows[0]["c7other"].ToString();
                        }
                        if (Complaint.Rows[0]["c8"].ToString() == "1")
                        {
                            c8.Checked = true;
                            c8mname.Text = Complaint.Rows[0]["c8mname"].ToString();
                            c8msn.Text = Complaint.Rows[0]["c8msn"].ToString();
                        }



                        if (servicelog.Rows[0]["creceivedby"].ToString() == "Fax")
                        {
                            Fax.Checked = true;
                        }
                        else if (servicelog.Rows[0]["creceivedby"].ToString() == "Email")
                        {
                            Email.Checked = true;
                        }
                        else if (servicelog.Rows[0]["creceivedby"].ToString() == "Phone")
                        {
                            Phone.Checked = true;
                        }
                        else if (servicelog.Rows[0]["creceivedby"].ToString() == "Other")
                        {
                            Other.Checked = true;
                            othermsg.Text = servicelog.Rows[0]["othermsg"].ToString();
                        }

                        describec.Text = servicelog.Rows[0]["describec"].ToString();

                        if (servicelog.Rows[0]["cr"].ToString() == "1" && servicelog.Rows[0]["rma"].ToString() == "1")
                        {
                            CR.Checked = true;
                            RMA.Checked = true;
                        }
                        else if (servicelog.Rows[0]["cr"].ToString() == "1" && servicelog.Rows[0]["rma"].ToString() == "0")
                        {
                            CR.Checked = true;
                            if (servicelog.Rows[0]["IR"].ToString() == "1")
                            {
                                IR.Visible = true;
                                iny.Visible = true;
                                inn.Visible = true;
                                iny.Checked = true;
                            }
                            else
                            {
                                IR.Visible = true;
                                inn.Visible = true;
                                iny.Visible = true;
                                inn.Checked = true;
                            }

                        }
                        else if (servicelog.Rows[0]["cr"].ToString() == "0" && servicelog.Rows[0]["rma"].ToString() == "1")
                        {
                            RMA.Checked = true;
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