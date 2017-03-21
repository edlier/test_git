using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics; //Debug.pring();
using Newtonsoft.Json;

namespace misSystem.QC
{
    public partial class QC_index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["SN"] != null)
                {
                    string str = "select * from qc_searchlist Left Join au_userinfo on qc_searchlist.Processer = au_userinfo.ID where SN = '" + Request.QueryString["SN"] + "';";
                    qc_grid.DataSource = GlobalAnnounce.db.GetDataTable(str);
                    qc_grid.DataBind();
                }

                process_dl.DataSource = GlobalAnnounce.db.GetDataTable("select * from qc_process");
                process_dl.DataValueField = "Id";
                process_dl.DataTextField = "Name";
                process_dl.DataBind();

                itemName_dl.DataSource = GlobalAnnounce.db.GetDataTable("select * from qc_item");
                itemName_dl.DataValueField = "Id";
                itemName_dl.DataTextField = "Name";
                itemName_dl.DataBind();

                model_dl.DataSource = GlobalAnnounce.db.GetDataTable("select * from qc_model");
                model_dl.DataValueField = "Id";
                model_dl.DataTextField = "Name";
                model_dl.DataBind();
            }
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            //int userId = 0;
            //if (Session[SessionString.userID] != null) { userId = (int)Session[SessionString.userID]; }
            //else { return; }
            string s = show_d.SelectedValue.Trim();            
            string str = "select * from qc_searchlist Left Join au_userinfo on qc_searchlist.Processer = au_userinfo.ID where ";
            if (s == "0") { str += "Status != 'Complete' AND "; }
            else if (s == "1") { str += "Status = 'Complete' AND "; }
            string process_no = process_dl.SelectedValue;
            string process_name = process_dl.SelectedItem.Text;
            if (process_name == "IQC")
            {
                string item_no = itemName_dl.SelectedValue;
                str += "Process_No = '" + process_no + "' AND Item_No = '" + item_no + "'";
            }
            else if (process_name == "FQC")
            {
                string model_no = model_dl.SelectedValue;
                str += "Process_No = '" + process_no + "' AND Model_No = '" + model_no + "'";
            }
            str += " ORDER by Date DESC;";
            Debug.WriteLine(str);
            qc_grid.DataSource = GlobalAnnounce.db.GetDataTable(str);  
          
            result_lab.Text = "";
            if (GlobalAnnounce.db.GetDataTable(str).Rows.Count == 0)
            {
                result_lab.Text = "Searching didn't match any records.";                
            }
            qc_grid.DataBind();
        }

        protected void process_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string process_name = process_dl.SelectedItem.Text;
            if (process_name == "IQC")
            {
                item_name_lab.Style.Add("display", "inline-block");
                itemName_dl.Style.Add("display", "inline-block");
                model_lab.Style.Add("display", "none");
                model_dl.Style.Add("display","none");
            }
            else if (process_name == "FQC")
            {
                item_name_lab.Style.Add("display", "none");
                itemName_dl.Style.Add("display", "none");
                model_lab.Style.Add("display", "inline-block");
                model_dl.Style.Add("display", "inline-block");
            }            
        }
                
        protected void qc_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int userId = 0;
            if (Session[SessionString.userID] != null) { userId = (int)Session[SessionString.userID]; }            

            for (int i = 0; i < qc_grid.Rows.Count; i++)
            {
                string status = ((Label)qc_grid.Rows[i].FindControl("status_lab")).Text;
                Button b = (Button)qc_grid.Rows[i].FindControl("edit_btn");
                int processer = 0;
                if (((Label)qc_grid.Rows[i].FindControl("processerId_lab")).Text != null &&
                    ((Label)qc_grid.Rows[i].FindControl("processerId_lab")).Text != "") { processer = int.Parse(((Label)qc_grid.Rows[i].FindControl("processerId_lab")).Text); }
              
                if (status == "Process") 
                {                    
                    b.Text = "Start";
                    b.Style.Add("display", "block");
                }
                else if (status == "Inprocess")
                {
                    b.Text = "Fill";
                    b.Style.Add("display", "block");
                    if (userId != processer || userId == 0) { b.Enabled = false; }
                }
                else
                { 
                    b.Style.Add("display", "none");
                    try
                    {
                        float fqty = float.Parse(((Label)qc_grid.Rows[i].FindControl("FQty_lab")).Text.Trim());
                        float qty = float.Parse(((Label)qc_grid.Rows[i].FindControl("Qty_lab")).Text.Trim());
                        float ratio = fqty / qty;
                        Debug.WriteLine(fqty.ToString() + " " + qty.ToString() + " " + ratio);
                        ((Label)qc_grid.Rows[i].FindControl("ratio_lab")).Text = ratio.ToString("P2");
                    }
                    catch(Exception ee){}
                }
            }
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {            
            if (Session[SessionString.userID] == null) 
            {
                Response.Write("<script>alert('You are not login yet.');");
                Response.Write("window.location.href='../Default.aspx';</script>");                
                return;
            }

            int rowIndex = int.Parse(((Button)sender).CommandArgument);
            string text = ((Button)sender).Text.Trim();
            Debug.WriteLine("clickBtn:" + text + " Index:" + rowIndex);

            GridViewRow r = qc_grid.Rows[rowIndex];
            string Id = ((Label)r.FindControl("id_lab")).Text,
                sn = ((Label)r.FindControl("sn_lab")).Text,
                PartNo = ((Label)r.FindControl("part_no_lab")).Text,
                ProcessNo = ((Label)r.FindControl("process_no_lab")).Text,
                ProcessType = ((Label)r.FindControl("process_name_lab")).Text,
                Catalogue = ((Label)r.FindControl("cata_name_lab")).Text,
                ItemName = ((Label)r.FindControl("item_name_lab")).Text,
                Vendor = ((Label)r.FindControl("vendor_lab")).Text,
                ManuBy = ((Label)r.FindControl("manuBy_lab")).Text,
                Qty = ((Label)r.FindControl("Qty_lab")).Text,
                Item = ((Label)r.FindControl("item_lab")).Text,
                Sampling = ((Label)r.FindControl("sampling_lab")).Text;

            //create json string
            var qc_detail_json = "{'Id':'" + Id + "','SN':'" + sn + "','PartNo':'" + PartNo + "','ProcessNo':'" + ProcessNo + "','ProcessType':'" + ProcessType + "','Catalogue':'" + Catalogue + "','ItemName':'" + ItemName + "','Vendor':'" + Vendor + "','ManuBy':'" + ManuBy + "','Qty':'" + Qty + "','Item':'" + Item + "','Sampling':'" + Sampling + "'}";
            dynamic json = Newtonsoft.Json.Linq.JValue.Parse(qc_detail_json);
            Session["qc_detail"] = json;
            Response.Redirect("QC_inprocess.aspx");
        }

    }
}