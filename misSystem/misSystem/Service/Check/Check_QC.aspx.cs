using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Check_QC : System.Web.UI.Page
    {
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

                Label1.Text = "<a href='/Service/Read/Read_ServiceLog.aspx?id=" + id + "'>ServiceLog</a>";
                Label2.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";
                Label3.Text = "<a href='/Service/Read/Read_investigation.aspx?id=" + id + "'>InvestigationReport</a>";
                Label4.Text = "<a href='/Service/Read/Read_capa.aspx?id=" + id + "'>CAPA</a>";
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

                GlobalAnnounce.serviceDB.updateflow(19, id);



                Response.Write("<script>alert('Checked!!');location.href='/Service/Status.aspx';</script>");
            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='/Service/Status.aspx';</script>");
            }
        }
    }
}