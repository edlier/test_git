using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.check
{
    public partial class check_pdne : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                if (Session[SessionString.userID] != null)
                {
                    string usrid = Session[SessionString.userID].ToString();
                    string dcrid = Request.QueryString["id"].ToString();
                    if (rdbtn_ce.SelectedValue != "" && rdbtn_510.SelectedValue != "" && rdbtn_sf.SelectedValue != "" && rdbtn_st.SelectedValue != "")
                    {
                        int ce = int.Parse(rdbtn_ce.SelectedValue);
                        int foz = int.Parse(rdbtn_510.SelectedValue);
                        int sf = int.Parse(rdbtn_sf.SelectedValue);
                        int st = int.Parse(rdbtn_st.SelectedValue);
                        DateTime dateNow = DateTime.Now;
                        string date = dateNow.ToString("yyyy/MM/dd");
                        DataTable pdneid = GlobalAnnounce.check_dcr.insertpdne(ce, foz, sf, st, date, usrid);
                        GlobalAnnounce.check_dcr.update_pdneid(pdneid.Rows[0][0].ToString(), dcrid);
                        GlobalAnnounce.check_dcr.updatestatus(dcrid, "7");
                        Response.Redirect("unconfirmed_form.aspx");
                        
                    }
                    else
                        Response.Write("<script>alert('there has somethig you have answer')</script>");
                }
                else
                    Response.Redirect("unconfirmed_form.aspx");
            }
            
        }
    }
}