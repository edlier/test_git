using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class InvestigationS : System.Web.UI.Page
    {
        DataTable servicelog = new DataTable();
        DataTable cpname = new DataTable();
        DataTable flow = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);
            if (!IsPostBack)
            {
                FMOS.DataSource = GlobalAnnounce.serviceDB.getpartno();
                FMOS.DataBind();

                FPS.DataSource = GlobalAnnounce.serviceDB.getpartsn();
                FPS.DataBind();
            }

            try
            {



                int id;
                id = int.Parse(Request["id"]);

                servicelog = GlobalAnnounce.serviceDB.search_slog(id);
                cpname = GlobalAnnounce.serviceDB.search_company(servicelog.Rows[0]["companyID"].ToString());


                ID.Text = servicelog.Rows[0]["ID"].ToString();
                productsn.Text = servicelog.Rows[0]["productSN"].ToString();
                describec.Text = servicelog.Rows[0]["describec"].ToString();
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

                if (PE.Text == "")
                {
                    check = false;
                }



                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "4")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertirs(id, FMOS.Text, FPS.Text, PE.Text, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(5, id);

                        Response.Write("<script>alert('Investigation Report(Service) Saved!!');location.href='/Service/Status.aspx';</script>");

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