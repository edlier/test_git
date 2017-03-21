using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics; //Debug.pring();
using Newtonsoft.Json.Linq;

namespace misSystem.QC
{
    public partial class QC_inprocess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {                
                dynamic json = Session["qc_detail"];
                if (json != null)
                {
                    Debug.WriteLine(Session["qc_detail"].ToString());
                    if (json.ProcessType != "FQC")
                    {
                        sampling_lab.Text = ((double)(json.Sampling) * 100) + "%";
                        double s = Math.Round(((double)(json.Sampling) * (double)(json.Qty)), 0);
                        sampling_qty_lab.Text = s.ToString();
                        if ((double)(json.Sampling) != 1)
                        { sampling_pan.Visible = true; }
                        else { sampling_pan.Visible = false; }
                    }                    

                    id_lab.Text = json.Id;
                    sn_lab.Text = json.SN == "" ? "---" : json.SN;
                    part_no_lab.Text = json.PartNo == "" ? "---" : json.PartNo;
                    process_lab.Text = json.ProcessType == "" ? "---" : json.ProcessType;
                    catalogue_lab.Text = json.Catalogue == "" ? "---" : json.Catalogue;
                    item_name_lab.Text = json.ItemName == "" ? "---" : json.ItemName;
                    vender_lab.Text = json.Vendor == "" ? "---" : json.Vendor;
                    manuBy_lab.Text = json.ManuBy == "" ? "---" : json.ManuBy;
                    qty_lab.Text = json.Qty == "" ? "---" : json.Qty;
                    item_lab.Text = json.Item;

                    
                                        
                    string str = "select * from qc_rejection ";
                    str += "where Id = 0 OR (process_Id = " + json.ProcessNo + " AND RIGHT(Id,1) = '0') order by Id;";
                    if (json.ProcessType == "IQC")
                    {
                        for (int i = 0; i <= 24; i++) { iqc_hour_dl.Items.Add(i.ToString()); }
                        for (int i = 0; i <= 59; i++) { iqc_min_dl.Items.Add(i.ToString()); }
                        
                        iqc_reason_dl.DataSource = GlobalAnnounce.db.GetDataTable(str);
                        iqc_reason_dl.DataValueField = "Id";
                        iqc_reason_dl.DataTextField = "Name";
                        iqc_reason_dl.DataBind();
                    }
                    else if (json.ProcessType == "FQC")
                    {
                        for (int i = 0; i <= 24; i++) { fqc_hour_dl.Items.Add(i.ToString()); }
                        for (int i = 0; i <= 59; i++) { fqc_min_dl.Items.Add(i.ToString()); }
                        
                        fqc_reason_dl_1.DataSource = GlobalAnnounce.db.GetDataTable(str);
                        fqc_reason_dl_1.DataValueField = "Id";
                        fqc_reason_dl_1.DataTextField = "Name";
                        fqc_reason_dl_1.DataBind();
                    }
                }
            }
        }

        protected void vali_btn_Click(object sender, EventArgs e)
        {
            string type = process_lab.Text;

            if (type == "IQC")
            {
                b_pan.Visible = true;
                c_pan.Visible = false;
            }
            else if (type == "FQC")
            {
                b_pan.Visible = false;
                c_pan.Visible = true;
            }
            else
            {
                b_pan.Visible = false;
                c_pan.Visible = false;
            }

            int id = int.Parse(id_lab.Text.Trim()),
                item_relation = int.Parse(item_lab.Text.Trim());
            if (item_relation == 0)
            {
                int processer;
                processer = Convert.ToInt32(Session[SessionString.userID]);                
                string str = "insert into qc_list(Processer, Start_Time) values(" + processer + ", '" + DateTime.Now.ToString(@"yyyy\/MM\/dd H:mm:ss") + "');";
                str += " select LAST_INSERT_ID( );";
                Debug.WriteLine(str);
                int i = int.Parse(GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString());
                item_lab.Text = i.ToString();
                str = "update qc_temp_searchlist set Item_qc = " + i + " where Id = " + id + ";";
                GlobalAnnounce.db.GetDataTable(str);
                Debug.WriteLine(str);
            }

            vali_btn.Visible = false;
        }

        protected void fqc_reason_dl_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (fqc_reason_dl_1.SelectedValue.ToString() == "0")
            {
                fqc_iss_lab.Visible = false;
                fqc_iss_text.Visible = false;

                fqc_reason_dl_2.Items.Clear();
                return;
            }
            else
            {
                fqc_iss_lab.Visible = true;
                fqc_iss_text.Visible = true;
            }
            string id = fqc_reason_dl_1.SelectedValue.Substring(0, 2).ToString();
            Debug.WriteLine(id);            
            str = "select * from qc_rejection where LEFT(Id,2) = " + id + " AND RIGHT(Id,1) != '0' order by Id;";
            fqc_reason_dl_2.DataSource = GlobalAnnounce.db.GetDataTable(str);
            fqc_reason_dl_2.DataValueField = "Id";
            fqc_reason_dl_2.DataTextField = "Name";
            fqc_reason_dl_2.DataBind();
        }

        protected void iqc_reason_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iqc_reason_dl.SelectedValue.ToString() == "0")
            {
                iqc_iss_lab.Visible = false;
                iqc_iss_text.Visible = false;
            }
            else
            {
                iqc_iss_lab.Visible = true;
                iqc_iss_text.Visible = true;
            }
        }

        protected void iqc_submit_btn_Click(object sender, EventArgs e)
        {
            int processTime = 0;
            processTime = int.Parse(iqc_hour_dl.SelectedItem.Text) * 60 + int.Parse(iqc_min_dl.SelectedItem.Text);
            int fq = int.Parse(iqc_FQty_text.Text) * (int.Parse(sampling_lab.Text.Substring(0, sampling_lab.Text.Length - 1)) / 100), q = int.Parse(iqc_Qty_text.Text) * (int.Parse(sampling_lab.Text.Substring(0, sampling_lab.Text.Length - 1)) / 100);
            string str = "update qc_list set Process_Time = '" + processTime + "', End_Time = '" + DateTime.Now.ToString(@"yyyy\/MM\/dd H:mm:ss") + "', FQty = " + q + ", Qty_list = " + q + ", Rejection_No = " + iqc_reason_dl.SelectedValue + ", Issue_Detail = '" + iqc_iss_text.Text + "' where Item = '" + item_lab.Text + "';";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            Response.Redirect("QC_index.aspx?SN=" + sn_lab.Text);
        }

        protected void fqc_submit_btn_Click(object sender, EventArgs e)
        {
            int p;
            if (fqc_failure_rad.Checked) { p = 0; } else { p = 1; }
            int processTime = 0;
            processTime = int.Parse(fqc_hour_dl.SelectedItem.Text) * 60 + int.Parse(fqc_min_dl.SelectedItem.Text);
            if (processTime == 0)
            { fqc_time_lab.Text = "Time can't be 0."; fqc_time_lab.Visible = true; return; }
            else { fqc_time_lab.Text = " "; fqc_time_lab.Visible = false; }
            string d;
            if (fqc_reason_dl_1.SelectedValue == "0")
            { d = "0"; }
            else { d = fqc_reason_dl_2.SelectedValue; }

            string str = "update qc_list set Process_Time = '" + processTime + "', End_Time = '" + DateTime.Now.ToString(@"yyyy\/MM\/dd H:mm:ss") + "', Rejection_No = " + d + ", Issue_Detail = '" + fqc_iss_text.Text + "', fqc_passed = " + p + " where Item = '" + item_lab.Text + "';";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            Response.Redirect("QC_index.aspx?SN=" + sn_lab.Text);
        }
    }
}