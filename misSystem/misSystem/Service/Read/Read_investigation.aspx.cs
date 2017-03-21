using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Read_investigation : System.Web.UI.Page
    {

        DataTable investigation = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {

            

            int id;
            id = int.Parse(Request["id"]);

            investigation = GlobalAnnounce.serviceDB.search_investigation(id);

            ID.Text = investigation.Rows[0]["servicelogID"].ToString();
            FMS.Text = investigation.Rows[0]["failuremors"].ToString();
            FPS.Text = investigation.Rows[0]["failurepartsn"].ToString();
            PE.Text = investigation.Rows[0]["pe"].ToString();

            id1.Text = investigation.Rows[0]["invesdetailed1"].ToString();
            id2.Text = investigation.Rows[0]["invesdetailed2"].ToString();
            id3.Text = investigation.Rows[0]["invesdetailed3"].ToString();
            id4.Text = investigation.Rows[0]["invesdetailed4"].ToString();
            id5.Text = investigation.Rows[0]["invesdetailed5"].ToString();
            id6.Text = investigation.Rows[0]["invesdetailed6"].ToString();
            id7.Text = investigation.Rows[0]["invesdetailed7"].ToString();

            FRCPC.Text = investigation.Rows[0]["frcpc"].ToString();
            Other.Text = investigation.Rows[0]["other"].ToString();
            recommended.Text = investigation.Rows[0]["recommended"].ToString();
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