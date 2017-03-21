using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MisSystem_ClassLibrary.HR.personnel;
using MisSystem_ClassLibrary.sys;

namespace misSystem.HR.personnel
{
    public partial class EmployeeFileList : System.Web.UI.Page
    {
        public static EmployeeDB myEmp = new EmployeeDB();
        public static SysCodeDB mySysC = new SysCodeDB();
        public static NewRecuritDB myNew = new NewRecuritDB();

        protected void Page_Load(object sender, EventArgs e)
        {
             //把預設按鈕指定給它
            this.Page.Form.DefaultButton = btn_def.UniqueID;
            //再把它隱藏起來
            btn_def.Style.Add("display", "none");

            if (!IsPostBack)
            {
                ucEmployee.setOriDropDownlist();
                // 設定前網頁
                if (Request.UrlReferrer != null)
                {
                    ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString();
                }

                if (Request.QueryString["page"] != null)
                {
                    string page = Request.QueryString["page"].ToString();
                    file_grid.PageIndex = int.Parse(page);

                    if (Request.QueryString["sEmp"] != null && Request.QueryString["sSta"] != null)
                    {
                        ucEmployee.setText(Request.QueryString["sEmp"].ToString());
                        dl_status.SelectedIndex = int.Parse(Request.QueryString["sSta"].ToString());

                        setGrid();
                        btn_search_Click(null, null);
                    }
                    else
                    {
                        DataTable dtCode = (DataTable)Session["DataTable"];

                        file_grid.DataSource = dtCode;
                        file_grid.DataBind();
                    }
                        
                }
                else
                {
                    setGrid();
                }
               
            }
        }

        private void setGrid()
        {
            DataTable dt = myEmp.search_EmpList("","","");
            dt.Columns.Add("YN");
            dt.Columns.Add("FileCount");

            DataTable dt_file = myNew.search_ALLNewRecurit();

            foreach (DataRow dr in dt.Rows)
            {
                DataRow[] foundRows;
                foundRows = dt_file.Select("EmpPID =" + dr["EmpPID"].ToString());
                int count = 0;
                if (foundRows != null && foundRows.Length > 0)
                {
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if (foundRows[i]["FileYN"].ToString() == "N")
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count = 10;
                }


                dr["FileCount"] = count;
                if (count > 0)
                {
                    dr["YN"] = "未齊全";
                }
                else
                {
                    dr["YN"] = "繳交齊全";
                }
            }


            GlobalAnnounce.OtherFuction.BindDataToGrid(dt, file_grid);
            Session["DataTable"] = dt;
            Session["PageTable"] = dt;
        }

        protected void btn_def_Click(object sender, EventArgs e)
        {
            //什麼都不做，避免使用者誤按Enter用
        }

        protected void file_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                if (e.Row.Cells[4].Text == "未齊全")
                {
                    e.Row.Cells[4].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[4].Text + "</b></span>";
                }
            }
        }

        protected void imgBtn_fileEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

            string pid = gvrow.Cells[(Int32)1].Text;
            string count = gvrow.Cells[(Int32)5].Text;
            string page = file_grid.PageIndex.ToString();

            if (count == "10")
            {
                Response.Redirect("EmployeeFile.aspx?emp=" + pid + "&status=1&page=" + page + "&sEmp=" + ucEmployee.getValue() + "&sSta=" + dl_status.SelectedIndex);
            }
            else
            {
                Response.Redirect("EmployeeFile.aspx?emp=" + pid + "&status=2&page=" + page + "&sEmp=" + ucEmployee.getValue() + "&sSta=" + dl_status.SelectedIndex);
            }            
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["DataTable"];
            DataTable dd = new DataTable();

            if (dl_status.SelectedIndex != 0) //是否有選擇狀態
            {
                string SelectStr = string.Empty;

                if (ucEmployee.getValue() != "***") //是否有選擇員工
                {
                    SelectStr = "YN ='" + dl_status.SelectedItem.Text + "' AND EmpNo=" + ucEmployee.getValue();
                }
                else
                {
                    SelectStr = "YN ='" + dl_status.SelectedItem.Text + "'";
                }

                DataRow[] foundRows;
                foundRows = dt.Select(SelectStr);

                foreach (DataColumn dc in dt.Columns)
                {
                    dd.Columns.Add(dc.ToString());
                }

                for (int i = 0; i < foundRows.Length; i++)
                {
                    dd.Rows.Add(foundRows[i].ItemArray);
                }

                GlobalAnnounce.OtherFuction.BindDataToGrid(dd, file_grid);
                Session["PageTable"] = dd;                
            }
            else
            {
                if (ucEmployee.getValue() != "***") //是否有選擇員工
                {
                    DataRow[] foundRows;
                    foundRows = dt.Select("EmpNo=" + ucEmployee.getValue());

                    foreach (DataColumn dc in dt.Columns)
                    {
                        dd.Columns.Add(dc.ToString());
                    }

                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        dd.Rows.Add(foundRows[i].ItemArray);
                    }

                    GlobalAnnounce.OtherFuction.BindDataToGrid(dd, file_grid);
                    Session["PageTable"] = dd;
                }
                else
                {
                    GlobalAnnounce.OtherFuction.BindDataToGrid(dt, file_grid);
                    Session["PageTable"] = dt;
                }              
            }

           
        }

        protected void file_grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[(Int32)1].Visible = false;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Session.Remove("DataTable");
            Session.Remove("PageTable");

                Response.Redirect("EmployeeList.aspx");
            
        }

        protected void file_grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            file_grid.PageIndex = e.NewPageIndex;
            DataTable dtCode = (DataTable)Session["PageTable"];
            file_grid.DataSource = dtCode;
            file_grid.DataBind(); 
        }
    }
}