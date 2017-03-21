using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace misSystem.QC
{
    public partial class QC_sampling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sn_text.Text = Request.QueryString["SN"];
                searchSN();
            }
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            searchSN();
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (Session[SessionString.userID] == null)
            {
                Response.Write("<script>alert('You are not login yet.');");
                Response.Write("window.location.href='../Default.aspx';</script>");
                return;
            }

            Response.Redirect("QC_set_sampling.aspx?SN=" + ((Button)sender).CommandArgument.ToString());
        }

        void searchSN()
        {
            string str = "select * from qc_searchlist where SN like '%" + sn_text.Text.Trim() + "%' AND Process_Name != 'FQC' AND Status = 'Process' ORDER by Date DESC;";
            Debug.WriteLine(str);
            result_lab.Text = "";
            if (GlobalAnnounce.db.GetDataTable(str).Rows.Count == 0)
            {
                result_lab.Text = "Searching didn't match any records.";
            }
            qc_grid.DataSource = GlobalAnnounce.db.GetDataTable(str);
            qc_grid.DataBind();
        }
    }
}