using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Web.UI.HtmlControls;

namespace misSystem.check
{
    public partial class overview_dcr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setValue();
        }
        public void setValue() 
        {
            //if (!IsPostBack)
            //{
                if (Request.QueryString["id"] != null)
                {
                    int dcrid = int.Parse(Request.QueryString["id"]);
                    DataTable init = GlobalAnnounce.overview.getDCRinit(dcrid);
                    DataTable docNum = GlobalAnnounce.overview.getDCRdocNo(dcrid);
                    DataTable docName = GlobalAnnounce.overview.getDCRdocName(dcrid);
                    DataTable version = GlobalAnnounce.overview.getDCRversion(dcrid);
                    DataTable reason = GlobalAnnounce.overview.getDCRchangereason(dcrid);
                    DataTable affectdoc = GlobalAnnounce.overview.getaffectdoc(dcrid);
                    DataTable pdne = GlobalAnnounce.overview.getpdne(dcrid);
                    DataTable pcmc = GlobalAnnounce.overview.getpcmc(dcrid);

                    if (init.Rows.Count > 0)
                    {
                        lbl_model.Text = init.Rows[0]["productModel"].ToString();
                        lbl_ano.Text = init.Rows[0]["DCRassignedNo"].ToString();
                        lbl_pjname.Text = init.Rows[0]["RnDprojectName"].ToString();
                        lbl_by.Text = init.Rows[0]["changePerposedBy"].ToString();
                        lbl_adate.Text = init.Rows[0]["changePerposeDate"].ToString() == "" ? "" : DateTime.Parse(init.Rows[0]["changePerposeDate"].ToString()).ToString("yyyy-MM-dd");
                        lbl_depm.Text = init.Rows[0]["DepManagerID"].ToString() == "0" ? "" : GlobalAnnounce.id.getName(int.Parse(init.Rows[0]["DepManagerID"].ToString()));
                        lbl_pdate.Text = init.Rows[0]["DepManagerCheckDate"].ToString() == "" ? "" : DateTime.Parse(init.Rows[0]["DepManagerCheckDate"].ToString()).ToString("yyyy-MM-dd");
                        lbl_priority.Text = init.Rows[0]["priority"].ToString();
                    }

                    for (int i = 1; i <= docNum.Columns.Count; i++)
                    {
                        if (docNum.Rows[0][i - 1].ToString() != "")
                        {
                            TableRow tr = new TableRow();
                            TableCell tc_docnum = new TableCell();
                            TableCell tc_docname = new TableCell();
                            TableCell tc_nversion = new TableCell();
                            TableCell tc_oversion = new TableCell();
                            TableCell tc_reason = new TableCell();

                            tc_docnum.Text = docNum.Rows[0]["DCRDocNum" + i].ToString();
                            tc_docname.Text = docName.Rows[0]["DocName" + i].ToString();
                            tc_nversion.Text = version.Rows[0]["newVersion" + i].ToString() == "0" ? "" : version.Rows[0]["newVersion" + i].ToString();
                            tc_oversion.Text = version.Rows[0]["oldVersion" + i].ToString() == "0" ? "" : version.Rows[0]["oldVersion" + i].ToString();
                            tc_reason.Text = setreason(reason.Rows[0]["ChangeReason" + i].ToString(), reason.Rows[0]["remark"].ToString());

                            tr.Cells.Add(tc_docnum);
                            tr.Cells.Add(tc_docname);
                            tr.Cells.Add(tc_nversion);
                            tr.Cells.Add(tc_oversion);
                            tr.Cells.Add(tc_reason);

                            tb_doc.Rows.Add(tr);
                        }
                    }
                    if (affectdoc.Rows.Count > 0)
                    {
                        for (int i = 1; i <= affectdoc.Columns.Count - 2; i++)
                        {
                            string affectitem = affectdoc.Rows[0][i].ToString();
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            if (affectitem == "True")
                            {
                                
                                if (i == 14)
                                    li.InnerText = GlobalAnnounce.overview.getaffectdes(i) + " : " + affectdoc.Rows[0][15].ToString();//lbox_affect.Items.Add(new ListItem(GlobalAnnounce.overview.getaffectdes(i) + " : " + affectdoc.Rows[0][15].ToString()));
                                else
                                    li.InnerText = GlobalAnnounce.overview.getaffectdes(i);//lbox_affect.Items.Add(new ListItem(GlobalAnnounce.overview.getaffectdes(i)));
                                affect.Controls.Add(li);
                            }
                            
                        }
                    }

                    if (pdne.Rows.Count > 0)
                    {
                        Label[] arr = { lbl_cem, lbl_510, safe, stand };
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (pdne.Rows[0][i+1].ToString() == "1")
                                arr[i].Text = "Yes";
                            else if (pdne.Rows[0][i+1].ToString() == "0")
                                arr[i].Text = "No";
                        }
                        
                        manager.Text = GlobalAnnounce.id.getName(int.Parse(pdne.Rows[0]["manager"].ToString()));
                        date.Text = DateTime.Parse(pdne.Rows[0]["date"].ToString()).ToString("yyyy/MM/dd");
                    }

                    if (pcmc.Rows.Count > 0)
                    {
                        Label[] arr = { lbl_ph, lbl_po, lbl_sh, lbl_so, lbl_fh, lbl_fo };
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (pcmc.Rows[0][i + 1].ToString() != "")
                                arr[i].Text = pcmc.Rows[0][i + 1].ToString();
                        }

                        if (pcmc.Rows[0]["StockDealReviseStocks"].ToString() == "1")
                            lbl_stock.Text = "Revise stocks Date 修改庫存日期 " + DateTime.Parse(pcmc.Rows[0]["StockDealReviseStocksDate"].ToString()).ToString("yyyy/MM/dd");
                        else if (pcmc.Rows[0]["StockDealUsedStocks"].ToString() == "1")
                            lbl_stock.Text = "Used Stocks 庫存品報廢 " + DateTime.Parse(pcmc.Rows[0]["StockDealUsedStocksDate"].ToString()).ToString("yyyy/MM/dd");
                        else if (pcmc.Rows[0]["StockDealDepleteOfRP"].ToString() == "1")
                            lbl_stock.Text = "Deplete of remaining printout 至庫存用完,自然切換, New Issue Applied Date 預定使用本版日期 " + DateTime.Parse(pcmc.Rows[0]["StockDealDepleteOfRPDate"].ToString()).ToString("yyyy/MM/dd");

                        pcmcm.Text = GlobalAnnounce.id.getName(int.Parse(pcmc.Rows[0]["PCMCmanagerID"].ToString()));
                        pcmcd.Text = DateTime.Parse(pcmc.Rows[0]["PCMCmanagerCheckDate"].ToString()).ToString("yyyy/MM/dd");
                    }
                }
            //}
        }
        protected string setreason(string reason, string remark)
        {
            string final = "";
            switch (reason)
            {
                case "0":
                    break;
                case "8":
                    final = reason + " , " + remark;
                    break;
                case "9":
                    final = reason + " , " + remark;
                    break;
                case "10":
                    final = reason + " , " + remark;
                    break;
                default:
                    final = reason;
                    break;
            }
            return final;
        }
        
    }
}