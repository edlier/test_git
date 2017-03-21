using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics; //Debug.pring();

namespace misSystem.QC
{
    public partial class QC_set_sampling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                searchBySN(Request.QueryString["SN"]);
            }            
        }

        protected void check_btn_Click(object sender, EventArgs e)
        {            
            string str = "update qc_temp_searchlist set Sampling = " + float.Parse(sampling_text.Text.ToString()) / 100 + " where SN = '" + sn_hide_lab.Text + "';";
            Debug.WriteLine(str);

            GlobalAnnounce.db.InsertDataTable(str);
            Response.Redirect("QC_sampling.aspx?SN=" + sn_hide_lab.Text);
        }

        void searchBySN(string sn)
        {
            sn_hide_lab.Text = sn;
            string str = "select * from qc_searchlist where SN = '" + sn + "';";
            Debug.WriteLine(str);

            if (GlobalAnnounce.db.GetDataTable(str).Rows.Count == 0)
            {
                result_lab.Text = "Searching didn't match any records.";
                detail_pan.Visible = false;
                check_pan.Visible = false;
                old_sampling_pan.Visible = false;
                return;
            }

            detail_pan.Visible = true;

            DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                                  
            if (float.Parse(dt.Rows[0]["Sampling"].ToString()) != 0)
            {
                old_sampling_pan.Visible = true;
                sampling_lab.Text = float.Parse(dt.Rows[0]["Sampling"].ToString())*100 + "%";
            }
            else
            {
                old_sampling_pan.Visible = false;
            }
            sn_hide_lab.Text = dt.Rows[0]["SN"].ToString() == "" ? "---" : dt.Rows[0]["SN"].ToString();
            part_no_lab.Text = dt.Rows[0]["Part_No"].ToString() == "" ? "---" : dt.Rows[0]["Part_No"].ToString();
            process_lab.Text = dt.Rows[0]["Process_Name"].ToString() == "" ? "---" : dt.Rows[0]["Process_Name"].ToString();
            catalogue_lab.Text = dt.Rows[0]["Catalogue_Name"].ToString() == "" ? "---" : dt.Rows[0]["Catalogue_Name"].ToString();
            item_name_lab.Text = dt.Rows[0]["Item_Name"].ToString() == "" ? "---" : dt.Rows[0]["Item_Name"].ToString();
            vender_lab.Text = dt.Rows[0]["Vendor"].ToString() == "" ? "---" : dt.Rows[0]["Vendor"].ToString();
            manuBy_lab.Text = dt.Rows[0]["Manufacturer"].ToString() == "" ? "---" : dt.Rows[0]["Manufacturer"].ToString();
            qty_lab.Text = dt.Rows[0]["Qty"].ToString() == "" ? "---" : dt.Rows[0]["Qty"].ToString();
                
            check_pan.Visible = true;
        }
    }
}