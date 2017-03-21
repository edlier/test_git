using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class InvestigationPE : System.Web.UI.Page
    {
        DataTable investigation = new DataTable();
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

                log.Text = "<a href='/Service/Read/Read_ServiceLog.aspx?id=" + id + "'>ServiceLog</a>";
                investigation = GlobalAnnounce.serviceDB.search_investigation(id);


                FMS.Text = investigation.Rows[0]["failuremors"].ToString();
                FPS.Text = investigation.Rows[0]["failurepartsn"].ToString();
                PE.Text = investigation.Rows[0]["pe"].ToString();

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
                int id;
                bool check = true;
                id = int.Parse(Request["id"]);

                if (id1.Text == "")
                {
                    check = false;
                }

                if (recommended.Text == "")
                {
                    check = false;
                }


                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "5")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertirpe(id, id1.Text, id2.Text, id3.Text, id4.Text, id5.Text, id6.Text, id7.Text, "test", Other.Text, recommended.Text, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(6, id);

                        Response.Write("<script>alert('Investigation Report (PE) Saved!!');location.href='/Service/Status.aspx';</script>");
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