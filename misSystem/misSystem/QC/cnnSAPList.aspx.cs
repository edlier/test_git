using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
namespace misSystem.QC
{
    public partial class cnnSAPList : System.Web.UI.Page
    {
        DataTable dt_qc_SAP = new DataTable();
        DataTable dt_qc_missys= new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

            string NoneQC_AU = "../Default.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneQC_AU, NoneLogin,6);

            if (!IsPostBack)
            {
                //-------------------------IQC---------------------------
                //搜尋SAP在06倉 待檢驗的資料
                dt_qc_SAP = GlobalAnnounce.SQL_String.search_SAP_QCwaitForValidateList();

                //搜尋 mis system 的資料 (MySQL Local data)
                dt_qc_missys = GlobalAnnounce.QCList.search_mySQL_QCwaitForValidateList();
                Debug.WriteLine(dt_qc_missys.Rows.Count);
                //==========================================
                DataColumn column;
                column = new DataColumn();
                dt_qc_missys.Columns.Add("DocDate2");
                dt_qc_missys.Columns.Add("status2");

                //---------------------------------------------------------等修正
                //Compare two Data between SAP and MySQL
                #region MIS count < SAP count
                if (dt_qc_missys.Rows.Count < dt_qc_SAP.Rows.Count)
                {

                    for (int i = 0; i < dt_qc_SAP.Rows.Count; i++)
                    {
                        //Find duplicate data
                        string expression;
                        expression = " ItemCode LIKE '" + dt_qc_SAP.Rows[i]["ItemCode"]
                            + "' AND DocNum = " + GlobalAnnounce.db.qo(dt_qc_SAP.Rows[i]["DocNum"].ToString())
                            + " AND LineNum=" + GlobalAnnounce.db.qo(dt_qc_SAP.Rows[i]["LineNum"].ToString());

                        DataRow[] foundRows;

                        // Use the Select method to find all rows matching the filter.
                        foundRows = dt_qc_missys.Select(expression);
                        //Debug.WriteLine(foundRows.Length);
                        //If there isn't any data in MySQL
                        if (foundRows.Length == 0)
                        {
                            DataRow dr;
                            dr = dt_qc_missys.NewRow();

                            dr["DocNum"] = dt_qc_SAP.Rows[i]["DocNum"];
                            dr["ItemCode"] = dt_qc_SAP.Rows[i]["ItemCode"];
                            dr["Qty"] = dt_qc_SAP.Rows[i]["Quantity"];
                            dr["Dscription"] = dt_qc_SAP.Rows[i]["Dscription"];
                            dr["LineNum"] = dt_qc_SAP.Rows[i]["LineNum"];
                            dr["CardName"] = dt_qc_SAP.Rows[i]["CardName"];
                            dr["CardCode"] = dt_qc_SAP.Rows[i]["CardCode"];


                            dr["DocDate2"] = Convert.ToDateTime(dt_qc_SAP.Rows[i]["DocDate"]).ToString("yyyy-MM-dd");

                            dt_qc_missys.Rows.Add(dr);
                        }
                    }

                }
                #endregion
                #region MIS count >= SAP count
                else if (dt_qc_missys.Rows.Count >= dt_qc_SAP.Rows.Count)
                {
                    for (int i = 0; i < dt_qc_missys.Rows.Count; i++)
                    {
                        //find duplicate data
                        string expression;
                        expression = " ItemCode LIKE '" + dt_qc_SAP.Rows[i]["ItemCode"]
                            + "' AND DocNum = " + GlobalAnnounce.db.qo(dt_qc_SAP.Rows[i]["DocNum"].ToString())
                            + " AND LineNum=" + GlobalAnnounce.db.qo(dt_qc_SAP.Rows[i]["LineNum"].ToString());


                        DataRow[] foundRows;

                        // Use the Select method to find all rows matching the filter.

                        foundRows = dt_qc_missys.Select(expression);
                        if (foundRows.Length == 0)
                        {
                            DataRow dr;
                            dr = dt_qc_missys.NewRow();

                            dr["DocNum"] = dt_qc_SAP.Rows[i]["DocNum"];
                            dr["ItemCode"] = dt_qc_SAP.Rows[i]["ItemCode"];
                            dr["Qty"] = dt_qc_SAP.Rows[i]["Quantity"];
                            dr["Dscription"] = dt_qc_SAP.Rows[i]["Dscription"];
                            dr["DocDate"] = String.Format("{0:yyyy-MM-dd}", dt_qc_SAP.Rows[i]["DocDate"]);
                            dr["LineNum"] = dt_qc_SAP.Rows[i]["LineNum"];
                            //dr["Processer"] = "";
                            dt_qc_missys.Rows.Add(dr);
                        }
                    }
                    for (int i = 0; i < dt_qc_missys.Rows.Count; i++)
                    {
                        if (dt_qc_missys.Rows[i]["DocDate2"].ToString() == "")
                        {
                            dt_qc_missys.Rows[i]["DocDate2"] = Convert.ToDateTime(dt_qc_missys.Rows[i]["DocDate"]).ToString("yyyy-MM-dd");
                        }
                    }
                }
                #endregion

                #region 移除  Status=2
                Debug.WriteLine(dt_qc_missys.Rows.Count);
                for (int i = dt_qc_missys.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt_qc_missys.Rows[i]["status"].ToString() == "2")
                    {
                        dt_qc_missys.Rows.RemoveAt(i);
                    }
                }
                #endregion


                //Convert  time to the same format + Change the status from num to string
                #region Convert  time + status
                for (int i = 0; i < dt_qc_missys.Rows.Count; i++)
                {
                    if (dt_qc_missys.Rows[i]["DocDate2"].ToString() == "")
                    {
                        dt_qc_missys.Rows[i]["DocDate2"] = Convert.ToDateTime(dt_qc_missys.Rows[i]["DocDate"]).ToString("yyyy-MM-dd");
                    }
                    try
                    {
                        if (Convert.ToInt32(dt_qc_missys.Rows[i]["status"]) == 1)
                        {
                            dt_qc_missys.Rows[i]["status2"] = "Starting";
                        }
                    }
                    catch { }
                }
                #endregion
                
                GridView1.DataSource = dt_qc_missys;
                GridView1.DataBind();
                

                #region Change Button's Text
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Button b = (Button)GridView1.Rows[i].FindControl("btn_start");

                    if (((Label)GridView1.Rows[i].FindControl("status")).Text == "Starting")
                    {
                        b.Text = "Fill";
                    }
                    else
                    {
                        b.Text = "Start";
                    }
                }
                #endregion
            }
        }

        protected void btn_start_Click(object sender, EventArgs e)
        {
            int rowIndex = int.Parse(((Button)sender).CommandArgument);
            GridViewRow r = GridView1.Rows[rowIndex];

            //直接將Button1_Click事件中的sender參數轉換成按鈕
            Button Bu2 = (Button)sender;            

            //將Button轉換成GridViewRow就是您所點選的某一列
            GridViewRow GV_Row = (GridViewRow)Bu2.NamingContainer;



            //Debug.WriteLine(((HiddenField)GV_Row.Cells[3].FindControl("LineNum")).Value);
            //Debug.WriteLine(rowIndex);
            //Debug.WriteLine(GV_Row.DataItemIndex);
            


            //將資料導到下一個頁面

            Response.Redirect("cnnSAP_List_inProcess.aspx?DocNum=" + GV_Row.Cells[1].Text +
                "&ItemCode=" + GV_Row.Cells[2].Text +
                "&LineNum=" + ((HiddenField)GV_Row.Cells[3].FindControl("LineNum")).Value +
                "&status=" + ((Label)r.FindControl("status")).Text);
        }
    }
}