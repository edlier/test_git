using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

using MisSystem_ClassLibrary.HR.personnel;

namespace misSystem.HR.personnel
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        DateTime now = DateTime.Now;
        public static EmployeeDB myEmp = new EmployeeDB();

        private enum DFIdx_emp
        {
            Query = (Int32)0,
            RowNumber = (Int32)1,
            EmpPID_h = (Int32)2,
            EmpName = (Int32)4,
            DeptID_h = (Int32)5,
            Dept = (Int32)6,
            EmpNo = (Int32)3,
            EmpCountry_h = (Int32)7,
            OnBoardDT = (Int32)8,
            Seniority = (Int32)9,
            SenioritySort_h = (Int32)10,
            Edit = (Int32)11

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucCompany.myAutoPostBack = true;
            ucCompany.myDDLCompany_SelectedIndexChanged += new EventHandler(ucCompany_SelectedIndexChanged);

            ucDTBetween.AutoPostBack_Date1 = true;
            ucDTBetween.Date1_TextChanged += new EventHandler(ucDTBetween_Date1_TextChanged);

            if (!IsPostBack)
            {
                //bindStaData();
                //bindEmpData();

                // 設定前網頁
                if (Request.UrlReferrer != null)
                {
                    ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString();
                }

                if (Request.QueryString["page"] != null)
                {
                    string page = Request.QueryString["page"].ToString();
                    grid_emp.PageIndex = int.Parse(page);
                    if ((DataTable)Session["dtEmp"] == null)
                    {
                        bindEmpData();
                    }
                    DataTable dtCode = (DataTable)Session["dtEmp"];
                    grid_emp.DataSource = dtCode;
                    grid_emp.DataBind();
                }

                ucDTBetween.Date1 = string.Empty;
                ucDTBetween.Date2 = string.Empty;

                bindStaData();
                bindEmpData();
            }
        }

        void ucDTBetween_Date1_TextChanged(object sender, EventArgs e)
        {
            ucDTBetween.Date2 = ucDTBetween.Date1;
        }

        protected override void OnPreLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                // 載入 系統代碼群組
                ucChange.CodeType = "PERCHANGETYPE";
                ucChange.myCodeHight = new Unit("22px");
                ucChange.myCodeWidth = new Unit("150px");
                ucChange.myCodeFontUnitSize = new FontUnit("11");
                ucChange.myCodeBorderStyle = BorderStyle.None;

                ucCompany.myCodeHight = new Unit("22px");
                ucCompany.myCodeWidth = new Unit("150px");
                ucCompany.myCodeFontUnitSize = new FontUnit("11");
                ucCompany.myCodeBorderStyle = BorderStyle.None;

                ucDept.myCodeHight = new Unit("22px");
                ucDept.myCodeWidth = new Unit("150px");
                ucDept.myCodeFontUnitSize = new FontUnit("11");
                ucDept.myCodeBorderStyle = BorderStyle.None;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //ucCompany.myCodeValue = "1";
            //ucDTBetween.Date1 = string.Empty;
            //ucDTBetween.Date2 = string.Empty;
        }
        //公司改變
        private void ucCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucDept.DeptCompanyID = ucCompany.myCodeValue;
            ucDept.SetDDLCode();
        }
        
        public int quar = 1, nextQuar;
        void bindStaData()
        {
            //離職
            //string sqlLeave = "select EmpPID, LeavedDT from hr_per_employee"
            //    + " WHERE year(LeavedDT)=" + GlobalAnnounce.db.qo(now.Year.ToString())
            //    + " ORDER BY EmpPID";

            DataTable dt_leave = myEmp.search_Leaved();


            //計算季度(1,4,7,10)
            
            DateTime startQuarter = now.AddMonths(0 - (now.Month - 1) % 3).AddDays(1 - now.Day);
            int m = startQuarter.Month;
            switch (m)
            {
                case 1:
                    quar = 1;
                    break;
                case 4:
                    quar = 2;
                    break;
                case 7:
                    quar = 3;
                    break;
                case 10:
                    quar = 4;
                    break;
            }
            //string sqlQuar = "select EmpPID, QUARTER(Birthday) from hr_per_employee"
            //    + " WHERE QUARTER(Birthday)=" + quar
            //    + " ORDER BY EmpPID";
            DataTable dt_quar = myEmp.search_QuarBirth(quar);


            //下季
            if (quar == 4)
            {
                nextQuar = 1;
            }
            else
            {
                nextQuar = quar + 1;
            }

            DataTable dt_quar2 = myEmp.search_QuarBirth(nextQuar);


            DataTable dt2 = new DataTable();
            dt2.Columns.Add("newEmp", typeof(string));
            dt2.Columns.Add("change", typeof(string));
            dt2.Columns.Add("leave", typeof(string));
            dt2.Columns.Add("birthday", typeof(string));
            dt2.Columns.Add("nextbirthday", typeof(string));

            dt2.Rows.Add("0", "0", dt_leave.Rows.Count.ToString(), dt_quar.Rows.Count.ToString(), dt_quar2.Rows.Count.ToString());
            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["newEmp"] = "0";
            //    dr["change"] = "0";                
            //}
            
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt2, grid_sta);
        }

        void bindEmpData()
        {
            //string sql = "SELECT hr_per_employee.EmpPID, hr_per_employee.EmpName, hr_per_employee.DeptID, hr_per_employee.EmpNo, hr_per_employee.EmpCountry, hr_per_employee.OnboardDT, au_audep.Description as Dept FROM hr_per_employee";
            //sql += " JOIN au_audep on(hr_per_employee.DeptID = au_audep.id)";

            //int[,] xLeaveD = new int[18, 12]{{7,6,5,5,4,4,3,3,2,1,1,0},
            //        {7,7,7,7,7,7,7,7,7,7,7,7},{10,9,9,9,9,8,8,8,8,7,7,7},{10,10,10,10,10,10,10,10,10,10,10,10},{14,13,13,13,12,12,12,11,11,11,10,10},{14,14,14,14,14,14,14,14,14,14,14,14},{14,14,14,14,14,14,14,14,14,14,14,14},{14,14,14,14,14,14,14,14,14,14,14,14},{14,14,14,14,14,14,14,14,14,14,14,14},{15,14,14,14,14,14,14,14,14,14,14,14},{16,15,15,15,15,15,15,15,15,15,15,15},{17,16,16,16,16,16,16,16,16,16,16,16},{18,17,17,17,17,17,17,17,17,17,17,17},{19,18,18,18,18,18,18,18,18,18,18,18},{20,19,19,19,19,19,19,19,19,19,19,19},{21,20,20,20,20,20,20,20,20,20,20,20},{22,21,21,21,21,21,21,21,21,21,21,21},{23,22,22,22,22,22,22,22,22,22,22,22}};




            string dept = ucDept.myCodeValue.ToString();
            DataTable dt = myEmp.search_EmpList(dept,ucDTBetween.Date1,ucDTBetween.Date2);
            dt.Columns.Add("Seniority", typeof(string));
            dt.Columns.Add("SenioritySort", typeof(double));
            //dt.Columns.Add("sen", typeof(double));
            //dt.Columns.Add("mon", typeof(double));
            //dt.Columns.Add("days", typeof(double));

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string[] seniority = calculateSeniority(dr["OnboardDT"].ToString(), dr["InitSeniority"].ToString());
                    double dd = (int.Parse(seniority[0]) * 12 + int.Parse(seniority[1])) / 12.0;
                    dr["SenioritySort"] = dd;
                    dr["Seniority"] = seniority[0] + "年" + seniority[1] + "個月";
                    //dr["sen"] = (2016 - Convert.ToDateTime(dr["OnboardDT"].ToString()).Year);
                    //dr["mon"] = Convert.ToDateTime(dr["OnboardDT"].ToString()).Month;
                    //if ((int.Parse(dr["sen"].ToString()) - 1) < 0)
                    //{
                    //    dr["days"] = 0;
                    //}
                    //else
                    //{
                    //    dr["days"] = xLeaveD[(int.Parse(dr["sen"].ToString()) - 1), (int.Parse(dr["mon"].ToString()) - 1)];
                    //}
                }
            }
            
            Session["dtEmp"] = dt;
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt, grid_emp);
        }

        protected void grid_emp_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    PrgUtil.AddSortIcon(e, gvCodeType); //對GridView抬頭增加排序的圖案 
                //}

                //e.Row.Cells[(Int32)DFIdx_emp.ID_h].Visible = false;//ID
                e.Row.Cells[(Int32)DFIdx_emp.EmpPID_h].Visible = false;//PID
                e.Row.Cells[(Int32)DFIdx_emp.DeptID_h].Visible = false;//DeptID
                e.Row.Cells[(Int32)DFIdx_emp.EmpCountry_h].Visible = false;//Country
                e.Row.Cells[(Int32)DFIdx_emp.SenioritySort_h].Visible = false;//SenioritySort
                e.Row.Cells[(Int32)DFIdx_emp.Edit].Visible = false;//Edit Btn
            }
        }
        
        protected void grid_emp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "QRY":
                    // Convert the row index stored in the CommandArgument
                    // property to an Integer.
                    int index = Convert.ToInt32(e.CommandArgument)%10;

                    // Retrieve the row that contains the button clicked 
                    // by the user from the Rows collection.
                    GridViewRow row = grid_emp.Rows[index];
                    string pid = row.Cells[(Int32)DFIdx_emp.EmpPID_h].Text;
                    string page = grid_emp.PageIndex.ToString();
                    Response.Redirect("EmployeeDetail.aspx?emp=" + pid+"&page="+page);
                    break;
            }
        }

        protected void grid_emp_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = SortDirection.Descending;
            }
            for (int i = 0; i <= ((GridView)sender).Columns.Count - 1; i++)
            {
                ((GridView)sender).Columns[i].HeaderText = ((GridView)sender).Columns[i].HeaderText.Replace("▲", "");
                ((GridView)sender).Columns[i].HeaderText = ((GridView)sender).Columns[i].HeaderText.Replace("▼", "");
            }
            string ViewState_SortDirection = ViewState["SortDirection"].ToString();
            int Columns_i = 0;
            string sort = string.Empty;

            for (int i = 0; i <= ((GridView)sender).Columns.Count - 1; i++)
            {
                if (e.SortExpression == ((GridView)sender).Columns[i].SortExpression)
                {
                    Columns_i = i;
                    if (ViewState["SortDirection"].ToString() == SortDirection.Ascending.ToString())
                    {
                        e.SortDirection = SortDirection.Descending;
                        ((GridView)sender).Columns[i].HeaderText = ((GridView)sender).Columns[i].HeaderText + "▼";
                        ViewState["SortDirection"] = SortDirection.Descending;
                        sort = "DESC";
                    }
                    else
                    {
                        e.SortDirection = SortDirection.Ascending;
                        ((GridView)sender).Columns[i].HeaderText = ((GridView)sender).Columns[i].HeaderText + "▲";
                        ViewState["SortDirection"] = SortDirection.Ascending;
                        sort = "ASC";
                    }
                }                
            }

            string name = "";
            switch (Columns_i)
            {
                case (Int32)DFIdx_emp.EmpName:
                    name = "EmpName";
                    break;
                case (Int32)DFIdx_emp.Dept:
                    name = "Dept";
                    break;
                case (Int32)DFIdx_emp.EmpNo:
                    name = "EmpNo";
                    break;
                case (Int32)DFIdx_emp.Seniority:
                    name = "SenioritySort";
                    break;
                case (Int32)DFIdx_emp.OnBoardDT:
                    name = "OnboardDT";
                    break;
            }

            if (Session["dtEmp"] != null)
            {
                DataTable dt = (DataTable)Session["dtEmp"];
                dt.DefaultView.Sort = name + " " + sort;
                GlobalAnnounce.OtherFuction.BindDataToGrid(dt, grid_emp);
            }
        }

        protected void grid_emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_emp.PageIndex = e.NewPageIndex;
            DataTable dtCode = (DataTable)Session["dtEmp"];
            grid_emp.DataSource = dtCode;
            grid_emp.DataBind(); 
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Session.Remove("dtEmp");

            if (ViewState["PreviousPageUrl"] != null)
                Response.Redirect(ViewState["PreviousPageUrl"].ToString());
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EmployeeDetail.aspx");
        }

        private string[] calculateSeniority(string onBoardDate, string InitSeniority)
        {
            string[] seniority = new string[3];
            //DateTime now = DateTime.Now;
            DateTime inDate = Convert.ToDateTime(onBoardDate);
            DateTime Date = now;
            //DateTime Date = Convert.ToDateTime("2016/01/01 12:00:00");

            int years = Date.Year - inDate.Year;
            int months = 0;
            int days = 0;

            // Check if the last year, was a full year.
            if (Date < inDate.AddYears(years) && years != 0)
            {
                years--;
            }

            // Calculate the number of months.
            inDate = inDate.AddYears(years);

            if (inDate.Year == Date.Year)
            {
                months = Date.Month - inDate.Month;
            }
            else
            {
                months = (12 - inDate.Month) + Date.Month;
            }

            // Check if last month was a complete month.
            if (Date < inDate.AddMonths(months) && months != 0)
            {
                months--;
            }

            // Calculate the number of days.
            inDate = inDate.AddMonths(months);
            days = (Date - inDate).Days;

            years = years + int.Parse(InitSeniority);
            //seniority = years + "年" + months + "個月";
            seniority[0] = years.ToString();
            seniority[1] = months.ToString();
            seniority[2] = days.ToString();

            return seniority;
        }

        protected void btn_newEmp_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeFileList.aspx");
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            bindEmpData();
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            ucDTBetween.Date1 = string.Empty;
            ucDTBetween.Date2 = string.Empty;

            ucCompany.myCodeSelectedIndex = 0;
            ucDept.myCodeSelectedIndex = 0;

            bindEmpData();
        }

        

    }
}