using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

using MisSystem_ClassLibrary;
using MisSystem_ClassLibrary.HR;
using MisSystem_ClassLibrary.HR.personnel;
using MisSystem_ClassLibrary.sys;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;


namespace misSystem.HR.personnel
{
    public partial class EmployeeDetail : System.Web.UI.Page
    {
        private static CommonUtils PrgUtil = new CommonUtils();
        private static EmployeeDB myEmp = new EmployeeDB();
        private static EducationDB myEdu = new EducationDB();
        private static ExperienceDB myExp = new ExperienceDB();
        private static ChangeDB myCha = new ChangeDB();
        private static NewRecuritDB myNew = new NewRecuritDB();
        private static SysCodeDB mySysC = new SysCodeDB();
        private static SysSetting mySysS = new SysSetting();

        private const string pwd = "4C494748544D4544434F5250524F524154494F4E406C696768746D65642E636F6D";
        static bool noneEmp = false;

        DateTime now = DateTime.Now;
        public static bool inDIsChange = false;
        public static bool updIsClick = false;
        //檔案上傳路徑
        string uploadPathAbs = string.Empty;//@"D:\BPM\HR\FileUploadDemo\";
        string uploadPathRel = string.Empty;//@"D:\BPM\HR\FileUploadDemo\";
        //string uploadPath = @"C:\Users\cherylnieh\Desktop\20161019\misSystem\misSystem\HR\FileUploadDemo\";

        private static string imgN = string.Empty;

        private enum DFIdx_edu
        {
            RowNumber = (Int32)0,
            ID_h = (Int32)1,
            EmpPID_h = (Int32)2,
            //SchoolID_h = (Int32)3,
            School = (Int32)3,
            //MajorID_h = (Int32)5,
            Major = (Int32)4,
            DegreeID_h = (Int32)5,
            Degree = (Int32)6,
            StartDT = (Int32)7,
            EndDT = (Int32)8,
            StatusID_h = (Int32)9,
            Status = (Int32)10,
            Comments = (Int32)11,
            UpdUserID_h = (Int32)12,
            UpdDT_h = (Int32)13,
            CompanyID_h = (Int32)14,
            Edit=(Int32)15
        }

        private enum DFIdx_exp
        {
            RowNumber = (Int32)0,
            ID_h = (Int32)1,
            EmpPID_h = (Int32)2,
            CompanyName = (Int32)3,
            Department = (Int32)4,
            Position = (Int32)5,
            Achievement = (Int32)6,
            StartDT = (Int32)7,
            EndDT = (Int32)8,
            Seniority = (Int32)9,
            LeavedRsnID_h = (Int32)10,
            LeavedRsn = (Int32)11,
            LeavedOtherRsn = (Int32)12,
            LeavedNote = (Int32)13,
            UpdUserID_h = (Int32)14,
            UpdDT_h = (Int32)15,
            CompanyID_h = (Int32)16,
            Edit=(Int32)17
        }

        private enum DFIdx_cha
        {
            RowNumber = (Int32)0,
            ID_h = (Int32)1,
            EmpPID_h = (Int32)2,
            BoardID = (Int32)3,
            DeptID_h = (Int32)4,
            Dept = (Int32)5,
            Position = (Int32)6,
            changeDT = (Int32)7,
            StatusID_h = (Int32)8,
            Status = (Int32)9,
            UpdUserID_h = (Int32)10,
            UpdDT_h = (Int32)11,
            CompanyID_h = (Int32)12
        }

        protected override void OnPreLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                ucCompany.myCodeHight = new Unit("22px");
                ucCompany.myCodeWidth = new Unit("150px");
                ucCompany.myCodeFontUnitSize = new FontUnit("11");
                ucCompany.myCodeBorderStyle = BorderStyle.None;

                ucCountry.CodeType = "COUNTRY";
                ucCountry.myCodeHight = new Unit("22px");
                ucCountry.myCodeWidth = new Unit("150px");
                ucCountry.myCodeFontUnitSize = new FontUnit("11");
                ucCountry.myCodeBorderStyle = BorderStyle.None;

                ucDept.myCodeHight = new Unit("22px");
                ucDept.myCodeWidth = new Unit("150px");
                ucDept.myCodeFontUnitSize = new FontUnit("11");
                ucDept.myCodeBorderStyle = BorderStyle.None;

                ucStatus.CodeType = "PERSTATUS";
                ucStatus.myCodeHight = new Unit("22px");
                ucStatus.myCodeWidth = new Unit("150px");
                ucStatus.myCodeFontUnitSize = new FontUnit("11");
                ucStatus.myCodeBorderStyle = BorderStyle.None;

                //ucEduSchool.CodeType = "EDUSCHOOL";
                //ucEduSchool.myCodeHight = new Unit("22px");
                //ucEduSchool.myCodeWidth = new Unit("150px");
                //ucEduSchool.myCodeFontUnitSize = new FontUnit("11");
                //ucEduSchool.myCodeBorderStyle = BorderStyle.None;

                //ucEduMajor.CodeType = "EDUMAJORSUBJECT";
                //ucEduMajor.myCodeHight = new Unit("22px");
                //ucEduMajor.myCodeWidth = new Unit("150px");
                //ucEduMajor.myCodeFontUnitSize = new FontUnit("11");
                //ucEduMajor.myCodeBorderStyle = BorderStyle.None;

                ucEduDegree.CodeType = "EDUDEGREE";
                ucEduDegree.myCodeHight = new Unit("22px");
                ucEduDegree.myCodeWidth = new Unit("150px");
                ucEduDegree.myCodeFontUnitSize = new FontUnit("11");
                ucEduDegree.myCodeBorderStyle = BorderStyle.None;

                ucEduStatus.CodeType = "EDUSTATUS";
                ucEduStatus.myCodeHight = new Unit("22px");
                ucEduStatus.myCodeWidth = new Unit("150px");
                ucEduStatus.myCodeFontUnitSize = new FontUnit("11");
                ucEduStatus.myCodeBorderStyle = BorderStyle.None;

                ucExpLeavedRsn.CodeType = "RESIGNREASON";
                ucExpLeavedRsn.myCodeHight = new Unit("22px");
                ucExpLeavedRsn.myCodeWidth = new Unit("150px");
                ucExpLeavedRsn.myCodeFontUnitSize = new FontUnit("11");
                ucExpLeavedRsn.myCodeBorderStyle = BorderStyle.None;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //把預設按鈕指定給它
            this.Page.Form.DefaultButton = btn_def.UniqueID;
            //再把它隱藏起來
            btn_def.Style.Add("display", "none");

            DataTable dt_sysS = mySysS.search_SysSetting("PER");
            uploadPathRel = dt_sysS.Rows[1]["SettingSrting"].ToString();//"C:/BPM/HR";
            uploadPathAbs = dt_sysS.Rows[0]["SettingSrting"].ToString(); //"http://192.168.168.47:8090/HR";

            ucDate2.AutoPostBack = true;
            ucDate2.TxtDate_TextChanged += new EventHandler(ucDate2_TxtDate_TextChanged);

            ucCompany.myAutoPostBack = true;
            ucCompany.myDDLCompany_SelectedIndexChanged += new EventHandler(ucCompany_SelectedIndexChanged);

            ucStatus.AutoPostBack = true;
            ucStatus.DDLCode_SelectedIndexChanged += new EventHandler(ucStatus_SelectedIndexChanged);

            ucExpLeavedRsn.AutoPostBack = true;
            ucExpLeavedRsn.DDLCode_SelectedIndexChanged += new EventHandler(ucExpLeavedRsn_SelectedIndexChanged);

            if (!IsPostBack)
            {                
                if (Request.QueryString["emp"] != null)
                {
                    lbl_seniority.Text = "目前年資:";
                    
                    DataTable dt;
                    //==========================================
                    //==========================================
                    /** 等資料完整後，記得將catch內的sql註解 **/
                    //==========================================
                    //==========================================
                    try
                    {
                        //string sql = "select EmpPID, EmpName, ChiName, EnFName, EnLName, DeptID, EmpNo, EmpCountry, OnboardDT, Status, PhoneNum, Sex, Birthday, ImgName, CompanyID FROM hr_per_employee WHERE EmpPID=" + Request.QueryString["emp"].ToString();
                        dt = myEmp.search_EmpDetail(Request.QueryString["emp"].ToString());
                        //tb_birthday.Text = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");
                        ucDate1.Date = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");
                    }
                    catch
                    {
                        string sql = "select EmpPID, EmpName, ChiName, EnFName, EnLName, DeptID, EmpNo, EmpCountry, InitSeniority, OnboardDT, Status, PhoneNum, Sex, Telephone, PerID, MarriageYN, ChildrenNum, BloodType, Height, Weight, Address, MailAddress, ImgName, CompanyID, ContactPerson, Relationship, ContactNum, ContactAddress, ResignationDT FROM hr_per_employee"
                            + " WHERE EmpPID=" + Request.QueryString["emp"].ToString();
                        dt = GlobalAnnounce.db.GetDataTable(sql);

                        ucDate1.Date = string.Empty;
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Session["empInfo"] = dt;

                        ucDept.DeptCompanyID = dt.Rows[0]["CompanyID"].ToString();

                        tbSetVisible(false, "Edit");
                        tbReadOnly(true);

                        noneEmp = false;
                    }
                    else
                    {
                        noneEmp = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('不存在員工!!!'); location.href='EmployeeList.aspx';", true);
                    }
                }
                else
                {
                    lbl_seniority.Text = "初始年資:";
                    tbSetEmpty();

                    //string sqlstr = "SELECT EmpPID, EmpNo FROM hr_per_employee ORDER BY EmpPID DESC";
                    DataTable dt_emp = myEmp.search_PIDNEmpNo();

                    if (dt_emp != null && dt_emp.Rows.Count > 0)
                    {
                        //tb_pid.Text = (int.Parse(dt_emp.Rows[0]["EmpPID"].ToString()) + 1).ToString();
                        hf_EmployeePID.Value = (int.Parse(dt_emp.Rows[0]["EmpPID"].ToString()) + 1).ToString();
                        int worknum = (int.Parse(dt_emp.Rows[0]["EmpNo"].ToString()) + 1) == 998 ? 999 : (int.Parse(dt_emp.Rows[0]["EmpNo"].ToString()) + 1);
                        tb_employeeNo.Text = (worknum).ToString();
                    }
                    else
                    {
                        //tb_pid.Text = "1";
                        hf_EmployeePID.Value = "1";
                        tb_employeeNo.Text = "1";
                    }

                    tbSetVisible(true, "Save");

                    string todayD = now.ToString("yyyy/MM/dd");
                    ucDate1.Date = string.Empty;
                    ucDate2.Date = todayD;
                    updIsClick = true;
                }

                // 設定前網頁
                if (Request.UrlReferrer != null)
                {
                    ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString();
                }

                ucDate3.Date = string.Empty;
                
                // The enctype attribute is missing on the first postback.
                // FileUpload 放在 UpdatePanel 內，第一次點擊 HasFile 都是 False
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (noneEmp == false)
                {
                    if (Request.QueryString["emp"] != null)
                    {
                        setEmpInfo((DataTable)Session["empInfo"]);
                        calculateSeniority(); //計算目前年資

                        updIsClick = false;

                        string resign = ((DataTable)Session["empInfo"]).Rows[0]["ResignationDT"] == null ? string.Empty : ((DataTable)Session["empInfo"]).Rows[0]["ResignationDT"].ToString();
                        string statusRe = ((DataTable)Session["empInfo"]).Rows[0]["Status"].ToString() == "RESIGNATION" ? ((DataTable)Session["empInfo"]).Rows[0]["Status"].ToString() : string.Empty;
                                                

                        if (!string.IsNullOrEmpty(resign) && !string.IsNullOrEmpty(statusRe))
                        {
                            DateTime resignDT = Convert.ToDateTime(resign);
                            //now = Convert.ToDateTime("2017/2/10");
                            
                            if (now.Year > resignDT.Year || (now.Year == resignDT.Year && now.Month > resignDT.Month))
                            {
                                imgbtnEdit.Visible = false;
                                ImgBtn_eduNew.Visible = false;
                                grid_edu.Columns[(Int32)DFIdx_edu.Edit].Visible = false;
                                imgBtn_expNew.Visible = false;
                                grid_exp.Columns[(Int32)DFIdx_exp.Edit].Visible = false;
                                imgbtn_leaveNew.Visible = false;
                            }
                            
                        }
                    }
                    else
                    {
                        tb_seniority.ReadOnly = false;
                    }
                }
            }
        }

        #region MultiView
        //學歷
        protected void Tab1_Click(object sender, EventArgs e)
        {
            lbl_eduMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {


                Tab1.CssClass = "Clicked";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 0;

                if (tb_employeeNo.Text != "")
                {
                    EduData();
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //經歷
        protected void Tab2_Click(object sender, EventArgs e)
        {
            lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Clicked";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 1;

                if (tb_employeeNo.Text != "")
                {       
                    ExpData();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //獎懲
        protected void Tab3_Click(object sender, EventArgs e)
        {
            //lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Clicked";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 2;

                if (tb_employeeNo.Text != "")
                { }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //異動
        protected void Tab4_Click(object sender, EventArgs e)
        {
            //lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Clicked";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 3;

                if (tb_employeeNo.Text != "")
                {
                    ChaData();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //教育訓練
        protected void Tab5_Click(object sender, EventArgs e)
        {
            //lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Clicked";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 4;

                if (tb_employeeNo.Text != "")
                { }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //考核
        protected void Tab6_Click(object sender, EventArgs e)
        {
            //lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Clicked";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 5;

                if (tb_employeeNo.Text != "")
                { }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //請假紀錄
        protected void Tab7_Click(object sender, EventArgs e)
        {
            //lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Clicked";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 6;

                if (tb_employeeNo.Text != "")
                {
                    //查詢出所有請假單及外出單
                    string sql = "select l.SN, l.workerNum, l.start_time, l.end_time, r.Descript as memo, l.status as s, '1' as type "
                        + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
                        + "where isDelete=0";
                    sql += " AND l.workerNum=" + tb_employeeNo.Text;
                    sql += " UNION "
                    + "select w.SN, w.workerNum, w.start_time, w.end_time, '外出', w.status as s, '2' as type "
                    + "from hr_application_workingout w "
                    + "where isDelete=0";
                    sql += " AND w.workerNum=" + tb_employeeNo.Text;
                    sql += " ORDER BY date(start_time),workerNum ASC;";

                    DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
                    dt.Columns.Add("Status");
                    dt.Columns.Add("time");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["s"].ToString() == "0") { dr["Status"] = "Pass"; }
                        else { dr["Status"] = "Not Pass"; }
                        dr["time"] = HR_class.getHours(Convert.ToDateTime(dr["start_time"].ToString()), Convert.ToDateTime(dr["end_time"].ToString()));
                    }

                    //dt.DefaultView.Sort = "start_time DESC";
                    GlobalAnnounce.OtherFuction.BindDataToGrid(dt, leave_grid);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //加班
        protected void Tab8_Click(object sender, EventArgs e)
        {
            //lbl_expMsg.Text = string.Empty;
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Clicked";
                Tab9.CssClass = "Initial";

                MainView.ActiveViewIndex = 7;

                if (tb_employeeNo.Text != "")
                { }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        //新人作業
        protected void Tab9_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["emp"] != null && updIsClick == false)
            {

                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                Tab5.CssClass = "Initial";
                Tab6.CssClass = "Initial";
                Tab7.CssClass = "Initial";
                Tab8.CssClass = "Initial";
                Tab9.CssClass = "Clicked";

                MainView.ActiveViewIndex = 8;

                if (tb_employeeNo.Text != "")
                {
                    //DataTable dt_new = myNew.search_isExist(hf_EmployeePID.Value);
                    //if (dt_new != null && dt_new.Rows.Count > 0)
                    //{
                    //    DataTable dt_file = new DataTable();
                    //    dt_file.Columns.Add("Notice");
                    //    dt_file.Columns.Add("LeaveProve");
                    //    dt_file.Columns.Add("JobProve");
                    //    dt_file.Columns.Add("Hospital");
                    //    dt_file.Columns.Add("perProve");
                    //    dt_file.Columns.Add("EduProve");
                    //    dt_file.Columns.Add("Pic");
                    //    dt_file.Columns.Add("Passbook");
                    //    dt_file.Columns.Add("seal");
                    //    dt_file.Columns.Add("account");

                    //    string tmp_YN = string.Empty;

                    //    foreach (DataRow dr in dt_new.Rows)
                    //    {
                    //        tmp_YN += dr["FileYN"].ToString() == "Y" ? "v," : "x,";
                    //    }

                    //    string[] spilt = tmp_YN.Split(',');
                    //    dt_file.Rows.Add(spilt[0], spilt[1], spilt[2], spilt[3], spilt[4], spilt[5], spilt[6], spilt[7], spilt[8], spilt[9]);
                    //    GlobalAnnounce.OtherFuction.BindDataToGrid(dt_file, file_grid);
                    //}
                    //else
                    //{
                    //    imgbtn_fileNew.Visible = true;
                    //}

                    DataTable dt_new=myNew.search_NewRecurit(hf_EmployeePID.Value);
                    if (dt_new != null && dt_new.Rows.Count > 0)
                    {
                        dt_new.Columns.Add("checkYN");

                        foreach (DataRow dr in dt_new.Rows)
                        {
                            dr["checkYN"] = dr["FileYN"].ToString() == "Y" ? "v" : "x";
                        }
                        GlobalAnnounce.OtherFuction.BindDataToGrid(dt_new, file_grid);
                        btn_fileView.Visible = true;
                    }
                    else
                    {
                        imgbtn_fileNew.Visible = true;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('尚未完成員工資料!');", true);
                MainView.ActiveViewIndex = -1;
            }
        }
        #endregion

        #region Fuction
        //到職日改變
        private void ucDate2_TxtDate_TextChanged(object sender, EventArgs e)
        {
            inDIsChange = true;
        }
        //公司改變
        private void ucCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucDept.DeptCompanyID = ucCompany.myCodeValue;
            ucDept.SetDDLCode();
        }
        //狀態改變
        private void ucStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucDate3.Date = string.Empty;
        }
        //離職原因改變
        void ucExpLeavedRsn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucExpLeavedRsn.CodeValue == "D001")
            {
                tb_LeavedOtherRsn.Visible = true;
            }
            else
            {
                tb_LeavedOtherRsn.Text = string.Empty;
                tb_LeavedOtherRsn.Visible = false;
            }
            mpe_exp.Show();
        }
                
        private void setEmpInfo(DataTable dt) //紀錄資料，取消修改時，可還原資料
        {
            tb_employeeName.Text = dt.Rows[0]["ChiName"].ToString(); //dt.Rows[0][2].ToString()
            tb_EnFName.Text = dt.Rows[0]["EnFName"].ToString();
            //tb_EnLName.Text = dt.Rows[0]["EnLName"].ToString();
            tb_employeeNo.Text = dt.Rows[0]["EmpNo"].ToString();
            hf_EmployeePID.Value = dt.Rows[0]["EmpPID"].ToString();
            //tb_pid.Text = dt.Rows[0]["EmpPID"].ToString();
            //tb_dept.Text = dt.Rows[0]["DeptID"].ToString();
            ucDept.myCodeValue = dt.Rows[0]["DeptID"].ToString();
            //tb_company.Text = dt.Rows[0]["CompanyID"].ToString();
            ucCompany.ddlCompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString();
            ucCountry.CodeValue = dt.Rows[0]["EmpCountry"].ToString();
            //tb_status.Text = dt.Rows[0]["Status"].ToString();
            ucStatus.CodeValue = dt.Rows[0]["Status"].ToString();
            if (dt.Rows[0]["Sex"].ToString() == "M")
            {
                rb_sex.SelectedIndex = 0;
            }
            else if (dt.Rows[0]["Sex"].ToString() == "F")
            {
                rb_sex.SelectedIndex = 1;
            }
            //tb_sex.Text = dt.Rows[0]["Sex"].ToString();

            //==========================================
            //==========================================
            /** 等資料完整後，記得將tb_birthday 的註解取消 **/
            //==========================================
            //==========================================
            ////tb_birthday.Text = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");
            //ucDate1.Date = DateTime.Parse(dt.Rows[0]["Birthday"].ToString()).ToString("yyyy/MM/dd");
            if (!string.IsNullOrEmpty(dt.Rows[0]["ImgName"].ToString()))
            {
                hf_imgN.Value = dt.Rows[0]["ImgName"].ToString();
                img.ImageUrl = uploadPathAbs + "/EmpImg/" + dt.Rows[0]["ImgName"].ToString();
            }
            else
            {
                hf_imgN.Value = string.Empty;
                img.ImageUrl = uploadPathAbs + "/EmpImg/user.png";
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["PhoneNum"].ToString()))
            {
                string str = strToDecrypt(dt.Rows[0]["PhoneNum"].ToString());
                string[] ttt = str.Split('-');
                tb_phone1.Text = ttt[0];
                tb_phone2.Text = ttt[1];
                tb_phone3.Text = ttt[2];
            }

            //tb_phone.Text = dt.Rows[0]["PhoneNum"].ToString();
            //tb_indate.Text = DateTime.Parse(dt.Rows[0]["OnboardDT"].ToString()).ToString("yyyy/MM/dd");
            ucDate2.Date = DateTime.Parse(dt.Rows[0]["OnboardDT"].ToString()).ToString("yyyy/MM/dd");
            tb_ID.Text = strToDecrypt(dt.Rows[0]["PerID"].ToString());
            if (dt.Rows[0]["MarriageYN"].ToString() == "N")
            {
                rb_marriage.SelectedIndex = 0;
            }
            else if (dt.Rows[0]["MarriageYN"].ToString() == "Y")
            {
                rb_marriage.SelectedIndex = 1;
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["Telephone"].ToString()))
            {
                string str = strToDecrypt(dt.Rows[0]["Telephone"].ToString());
                string[] ttt = str.Split('-');
                tb_telephone1.Text = ttt[0];
                tb_telephone2.Text = ttt[1];
                tb_telephone3.Text = ttt[2];
            }

            tb_AddressC.Text = strToDecrypt(dt.Rows[0]["MailAddress"].ToString());
            tb_Address.Text = strToDecrypt(dt.Rows[0]["Address"].ToString());
            tb_Children.Text = dt.Rows[0]["ChildrenNum"].ToString();
            tb_BloodType.Text = dt.Rows[0]["BloodType"].ToString();
            tb_Height.Text = dt.Rows[0]["Height"].ToString();
            tb_Weight.Text = dt.Rows[0]["Weight"].ToString();
            tb_ContactPer.Text = dt.Rows[0]["ContactPerson"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["ContactNum"].ToString()))
            {
                string str = strToDecrypt(dt.Rows[0]["ContactNum"].ToString());
                string[] ttt = str.Split('-');
                tb_ContactPhone1.Text = ttt[0];
                tb_ContactPhone2.Text = ttt[1];
                tb_ContactPhone3.Text = ttt[2];
            }
            tb_contactAddress.Text = strToDecrypt(dt.Rows[0]["ContactAddress"].ToString());
            tb_Relationship.Text = dt.Rows[0]["Relationship"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["ResignationDT"].ToString()))
            {
                ucDate3.Date = DateTime.Parse(dt.Rows[0]["ResignationDT"].ToString()).ToString("yyyy/MM/dd");
            }

        }

        private void tbSetEmpty()
        {
            tb_employeeName.Text = string.Empty;
            tb_EnFName.Text = string.Empty;
            //tb_EnLName.Text = string.Empty;
            tb_employeeNo.Text = string.Empty;
            //tb_pid.Text = string.Empty;
            hf_EmployeePID.Value = string.Empty;
            //tb_dept.Text = string.Empty;
            //tb_birthday.Text = string.Empty;
            //tb_company.Text = string.Empty;
            ucCompany.myCodeValue = "0";
            tb_seniority.Text = string.Empty;
            //tb_status.Text = string.Empty;
            rb_sex.SelectedIndex = -1;
            //tb_sex.Text = string.Empty;
            hf_imgN.Value = string.Empty;
            img.ImageUrl = string.Empty;
            imgN = string.Empty;
            tb_phone1.Text = string.Empty;
            tb_phone2.Text = string.Empty;
            tb_phone3.Text = string.Empty;
            rb_marriage.SelectedIndex = -1;
            tb_ID.Text = string.Empty;
            tb_telephone1.Text = string.Empty;
            tb_telephone2.Text = string.Empty;
            tb_telephone3.Text = string.Empty;
            tb_AddressC.Text = string.Empty;
            tb_Address.Text = string.Empty;
            tb_Children.Text = string.Empty;
            tb_BloodType.Text = string.Empty;
            tb_Height.Text = string.Empty;
            tb_Weight.Text = string.Empty;
            tb_ContactPer.Text = string.Empty;
            tb_ContactPhone1.Text = string.Empty;
            tb_ContactPhone2.Text = string.Empty;
            tb_ContactPhone3.Text = string.Empty;
            tb_contactAddress.Text = string.Empty;
            tb_Relationship.Text = string.Empty;
        }

        private void tbSetVisible(bool bb, string type)
        {
            //lbl_EnLName.Visible = bb;
            //tb_EnLName.Visible = bb;
            //lbl_position.Visible=bb;
            //tb_position.Visible = bb;
            //lbl_pid.Visible = bb;
            //tb_pid.Visible = bb;

            filMyFile.Visible = bb;
            btn_upload.Visible = bb;
            lbl_imgMsg.Visible = bb;
            //btn_del.Visible = bb;

            //lbl_seniority.Visible = !bb;
            //tb_seniority.Visible = !bb;

            imgbtnExit.Visible = bb;
            imgbtnNew.Visible = !bb;
            btnReturn.Visible = !bb;


            if (type == "Edit")
            {
                imgbtnEdit.Visible = !bb;
                imgbtnUpdate.Visible = bb;
                imgbtnSave.Visible = false;
                imgbtnSaveMore.Visible = false;
                //btn_pdf.Visible = !bb;
            }
            else if (type == "Save")
            {
                imgbtnUpdate.Visible = !bb;
                imgbtnSave.Visible = bb;
                imgbtnSaveMore.Visible = bb;
                //btn_pdf.Visible = !bb;
            }


        }

        private void tbReadOnly(bool bb)
        {

            tb_employeeName.ReadOnly = bb;
            //tb_company.ReadOnly = bb;
            ucCompany.myCodeEnabled = !bb;
            tb_EnFName.ReadOnly = bb;
            tb_phone1.ReadOnly = bb;
            tb_phone2.ReadOnly = bb;
            tb_phone3.ReadOnly = bb;
            rb_marriage.Enabled = !bb;
            tb_ID.ReadOnly = true;
            tb_telephone1.ReadOnly = bb;
            tb_telephone2.ReadOnly = bb;
            tb_telephone3.ReadOnly = bb;
            tb_AddressC.ReadOnly = bb;
            tb_Address.ReadOnly = bb;
            tb_Children.ReadOnly = bb;
            tb_BloodType.ReadOnly = bb;
            tb_Height.ReadOnly = bb;
            tb_Weight.ReadOnly = bb;
            tb_ContactPer.ReadOnly = bb;
            tb_ContactPhone1.ReadOnly = bb;
            tb_ContactPhone2.ReadOnly = bb;
            tb_ContactPhone3.ReadOnly = bb;
            tb_contactAddress.ReadOnly = bb;
            tb_Relationship.ReadOnly = bb;
            tb_seniority.ReadOnly = true;
            tb_employeeNo.ReadOnly = true;
            //tb_pid.ReadOnly = true;
            //tb_status.ReadOnly = bb;
            ucCountry.myCodeEnabled = !bb;
            ucStatus.myCodeEnabled = !bb;
            //tb_dept.ReadOnly = bb;
            ucDept.myCodeEnabled = !bb;
            rb_sex.Enabled = !bb;
            //tb_sex.ReadOnly = bb;
            //tb_birthday.ReadOnly = bb;
            ucDate1.txtReadOnly(bb);
            ucDate1.BtnDateEnabled = !bb;
            //tb_phone.ReadOnly = bb;
            //tb_indate.ReadOnly = bb;
            ucDate2.txtReadOnly(bb);
            ucDate2.BtnDateEnabled = !bb;
            ucDate3.txtReadOnly(bb);
            ucDate3.BtnDateEnabled = !bb;
        }

        private void EduData()
        {
            //string sql = "SELECT * FROM hr_per_education WHERE EmpPID=" + hf_EmployeePID.Value;
            DataTable dt = myEdu.search_EmpEduDetail(hf_EmployeePID.Value);
            //在資料表中加上欄位            
            dt.Columns.Add("Degree");
            dt.Columns.Add("Status");

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (!string.IsNullOrEmpty(dt.Rows[i]["SchoolID"].ToString()))
                    //{
                    //    DataTable dt_School = mySysC.search_EDUSCHOOL(dt.Rows[i]["SchoolID"].ToString());
                    //    dt.Rows[i]["School"] = dt_School.Rows[0]["CodeName"].ToString();
                    //}
                    //if (!string.IsNullOrEmpty(dt.Rows[i]["MajorID"].ToString()))
                    //{
                    //    DataTable dt_Major = mySysC.search_EDUMAJORSUBJECT(dt.Rows[i]["MajorID"].ToString());
                    //    dt.Rows[i]["Major"] = dt_Major.Rows[0]["CodeName"].ToString();
                    //}
                    if (!string.IsNullOrEmpty(dt.Rows[i]["DegreeID"].ToString()))
                    {
                        DataTable dt_Degree = mySysC.search_EDUDEGREE(dt.Rows[i]["DegreeID"].ToString());
                        dt.Rows[i]["Degree"] = dt_Degree.Rows[0]["CodeName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i]["StatusID"].ToString()))
                    {
                        DataTable dt_status = mySysC.search_EDUSTATUS(dt.Rows[i]["StatusID"].ToString());
                        dt.Rows[i]["Status"] = dt_status.Rows[0]["CodeName"].ToString();
                    }
                }
            }

            Session["dtEdu"] = dt;

            BindData(dt, grid_edu);
        }

        private void ExpData()
        {
            DataTable dt = myExp.search_EmpExpDetail(hf_EmployeePID.Value);
            //在資料表中加上欄位
            dt.Columns.Add("LeavedRsn");

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["LeavedRsnID"].ToString()))
                    {
                        DataTable dt_leaveRsn = mySysC.search_RESIGNREASON(dt.Rows[i]["LeavedRsnID"].ToString());
                        dt.Rows[i]["LeavedRsn"] = dt_leaveRsn.Rows[0]["CodeName"].ToString();
                    }
                }
            }

            Session["dtExp"] = dt;

            BindData(dt, grid_exp);
        }

        private void ChaData()
        {
            DataTable dt = myCha.search_EmpChaDetail(hf_EmployeePID.Value);
            //在資料表中加上欄位
            dt.Columns.Add("Dept");
            dt.Columns.Add("Status");

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["StatusID"].ToString()))
                    {
                        DataTable dt_status = mySysC.search_PERCHANGETYPE(dt.Rows[i]["StatusID"].ToString());
                        dt.Rows[i]["Status"] = dt_status.Rows[0]["CodeName"].ToString();
                    }
                }
            }

            Session["dtCha"] = dt;

            BindData(dt, grid_cha);
        }

        private void calculateSeniority()
        {
            //DateTime now = DateTime.Now;
            DateTime inDate = Convert.ToDateTime(ucDate2.Date); //就職日
            DateTime Date = now;
            //DateTime Date = Convert.ToDateTime("2016/01/01 12:00:00");
            if (!string.IsNullOrEmpty(ucDate3.Date))
            {
                Date = Convert.ToDateTime(ucDate3.Date); //離職日
            }
            else { }

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

            years = years + int.Parse(((DataTable)Session["empInfo"]).Rows[0]["InitSeniority"].ToString());
            tb_seniority.Text = years + "年" + months + "個月又" + days + "天";

        }

        private void delFile()
        {
            //lbl_imgMsg.Text = string.Empty;
            if (string.IsNullOrEmpty(imgN) && (DataTable)Session["empInfo"]==null)
            {

            }
            else if (string.IsNullOrEmpty(imgN) && !string.IsNullOrEmpty(((DataTable)Session["empInfo"]).Rows[0]["ImgName"].ToString()))
            {
                imgN = ((DataTable)Session["empInfo"]).Rows[0]["ImgName"].ToString();
                string file = uploadPathRel + "/EmpImg/" + imgN;
                // Delete a file by using File class static method...
                if (System.IO.File.Exists(file))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        string str = @uploadPathRel + "/EmpImg/delImg/";
                        str = str.Replace("/", "\\");
                        System.IO.File.Move(file, str + imgN);
                        lbl_imgMsg.Text = "檔案刪除成功";
                        img.ImageUrl = "";
                    }
                    catch (IOException ioe)
                    {
                        Console.WriteLine(ioe.Message);
                        return;
                    }
                }
            }
            else if (imgN != string.Empty && updIsClick == true)
            {
                if (imgN == string.Empty) { imgN = ((DataTable)Session["empInfo"]).Rows[0]["ImgName"].ToString(); }
                string file = uploadPathRel + "/EmpImg/" + imgN;
                // Delete a file by using File class static method...
                if (System.IO.File.Exists(file))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        string str = @uploadPathRel + "/EmpImg/delImg/";
                        str = str.Replace("/", "\\");
                        System.IO.File.Move(file, str + imgN);
                        lbl_imgMsg.Text = "檔案刪除成功";
                        img.ImageUrl = "";
                    }
                    catch (IOException ioe)
                    {
                        Console.WriteLine(ioe.Message);
                        return;
                    }
                }
            }
            else
            {
                lbl_imgMsg.Text = "未選擇檔案";
                return;
            }

            imgN = string.Empty;
        }

        private void BindData(DataTable dt, GridView gv)
        {
            if (dt != null && dt.Rows.Count > 0)
            { GlobalAnnounce.OtherFuction.BindDataToGrid(dt, gv); }
            else
            { EmptyBindGrid(dt, gv); }
        }

        private void EmptyBindGrid(DataTable dt, GridView gv)
        {
            dt.Constraints.Clear();
            foreach (DataColumn dc in dt.Columns)
            { dc.AllowDBNull = true; }

            // Add a blank row to the dataset
            dt.Columns[0].AllowDBNull = true;
            dt.Rows.Add(dt.NewRow());

            // Bind the DataSet to the GridView
            gv.DataSource = dt;
            gv.DataBind();
            //GlobalAnnounce.OtherFuction.BindDataToGrid(dt, gv);

            // Get the number of columns to know what the Column Span should be
            int columnCount = gv.Rows[0].Cells.Count;

            // Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv.Rows[0].Cells[0].Text = "No Data!";
            gv.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        #endregion

        #region EMP
        private bool chkField_emp()
        {
            lbl_Msg.Text = string.Empty;
            lbl_eduMsg.Text = string.Empty;
            lbl_expMsg.Text = string.Empty;
            lbl_chaMsg.Text = string.Empty;
            bool chkField = true;

            //=== 空白檢查 === 
            if (Request.QueryString["emp"] == null)
            {
                if (ucStatus.CodeSelectedIndex == 2)
                {
                    lbl_Msg.Text = "第一次新增不可為離職!";
                    chkField = false;
                    return chkField;
                }
            }

            if (string.IsNullOrEmpty(tb_employeeName.Text))
            {
                lbl_Msg.Text = "請輸入姓名!";
                chkField = false;
                return chkField;
            }

            if (ucCompany.myCodeSelectedIndex == 0)
            {
                lbl_Msg.Text = "請選擇公司!";
                chkField = false;
                return chkField;
            }

            if (ucCountry.CodeSelectedIndex == 0)
            {
                lbl_Msg.Text = "請選擇國家!";
                chkField = false;
                return chkField;
            }

            if (string.IsNullOrEmpty(ucDate1.Date))
            {
                lbl_Msg.Text = "請輸入生日!";
                chkField = false;
                return chkField;
            }

            if (rb_sex.SelectedItem == null)
            {
                lbl_Msg.Text = "請選擇性別!";
                chkField = false;
                return chkField;
            }

            if (string.IsNullOrEmpty(tb_AddressC.Text))
            {
                lbl_Msg.Text = "請輸入通訊地址!";
                chkField = false;
                return chkField;
            }

            //if (string.IsNullOrEmpty(tb_Address.Text))
            //{
            //    lbl_Msg.Text = "請輸入戶籍地址!";
            //    chkField = false;
            //    return chkField;
            //}

            if (string.IsNullOrEmpty(tb_ID.Text))
            {
                lbl_Msg.Text = "請輸入身分證字號!";
                chkField = false;
                return chkField;
            }

            //if (rb_marriage.SelectedItem == null)
            //{
            //    lbl_Msg.Text = "請選擇婚姻狀況!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_phone1.Text) || string.IsNullOrEmpty(tb_phone2.Text) || string.IsNullOrEmpty(tb_phone3.Text))
            //{
            //    lbl_Msg.Text = "請輸入手機號碼!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_telephone1.Text) || string.IsNullOrEmpty(tb_telephone2.Text) || string.IsNullOrEmpty(tb_telephone3.Text))
            //{
            //    lbl_Msg.Text = "請輸入市話號碼!";
            //    chkField = false;
            //    return chkField;
            //}

            if (ucDept.myCodeSelectedIndex == 0)
            {
                lbl_Msg.Text = "請選擇單位!";
                chkField = false;
                return chkField;
            }

            if (ucStatus.CodeSelectedIndex == 0)
            {
                lbl_Msg.Text = "請選擇狀態!";
                chkField = false;
                return chkField;
            }

            //if (string.IsNullOrEmpty(tb_Children.Text))
            //{
            //    lbl_Msg.Text = "請輸入兒女人數!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_BloodType.Text))
            //{
            //    lbl_Msg.Text = "請輸入血型!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_Height.Text))
            //{
            //    lbl_Msg.Text = "請輸入身高!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_Weight.Text))
            //{
            //    lbl_Msg.Text = "請輸入體重!";
            //    chkField = false;
            //    return chkField;
            //}

            DateTime indate = Convert.ToDateTime(ucDate2.Date);
            if (indate.Date > now.Date)
            {
                lbl_Msg.Text = "到職日不能大於今日!";
                chkField = false;
                return chkField;
            }

            //if (string.IsNullOrEmpty(tb_ContactPer.Text))
            //{
            //    lbl_Msg.Text = "請輸入連絡人!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_ContactPhone1.Text) || string.IsNullOrEmpty(tb_ContactPhone2.Text) || string.IsNullOrEmpty(tb_ContactPhone3.Text))
            //{
            //    lbl_Msg.Text = "請輸入連絡人電話!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_Relationship.Text))
            //{
            //    lbl_Msg.Text = "請輸入連絡人關係!";
            //    chkField = false;
            //    return chkField;
            //}

            //if (string.IsNullOrEmpty(tb_contactAddress.Text))
            //{
            //    lbl_Msg.Text = "請輸入連絡人地址!";
            //    chkField = false;
            //    return chkField;
            //}

            if (ucStatus.CodeSelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(ucDate3.Date))
                {
                    lbl_Msg.Text = "請輸入離職日期!";
                    chkField = false;
                    return chkField;
                }
                else
                {
                    DateTime outdate = Convert.ToDateTime(ucDate3.Date);
                    if (indate.Date > outdate.Date)
                    {
                        lbl_Msg.Text = "離職日不能早於到職日!";
                        chkField = false;
                        return chkField;
                    }
                }
            }
            else if (ucStatus.CodeSelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty(ucDate3.Date))
                {
                    lbl_Msg.Text = "離職日期應為空!";
                    chkField = false;
                    return chkField;
                }
            }

           

            return chkField;
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            delFile();

            bool temp = filMyFile.HasFiles;

            lbl_imgMsg.Text = string.Empty;
            PrgUtil.UploadFile(uploadPathRel + "/EmpImg/", filMyFile, new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" }, 5100000, lbl_imgMsg, Path.GetFileNameWithoutExtension(filMyFile.FileName));

            if (lbl_imgMsg.Text == "檔案上傳成功")
            {
                string filename = filMyFile.FileName;
                string serverFilePath = Path.Combine(uploadPathRel + "/EmpImg/", filename);
                string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
                string extension = Path.GetExtension(filename).ToLowerInvariant();
                int fileCount = 1;
                while (File.Exists(serverFilePath))
                {
                    // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
                    filename = string.Concat(fileNameOnly, "_", fileCount, extension);
                    serverFilePath = Path.Combine(uploadPathRel + "/EmpImg/", filename);
                    fileCount++;
                }
                if (fileCount > 2)
                {
                    imgN = string.Concat(fileNameOnly, "_", (fileCount - 2), extension);
                }
                else
                {
                    imgN = string.Concat(fileNameOnly, extension);
                }

                //預覽圖片
                HttpPostedFile myFile = filMyFile.PostedFile;
                Session["myFile"] = myFile;
                img.ImageUrl = "showImage.ashx";
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (ViewState["PreviousPageUrl"] != null)
            {
                string page = Request.QueryString["page"] == string.Empty ? string.Empty : Request.QueryString["page"].ToString();

                string[] spiltW = ViewState["PreviousPageUrl"].ToString().Split('?');
                //Response.Redirect(spiltW[0] + "?page=" + page);
                Response.Redirect("EmployeeList.aspx?page=" + page);

                Session.Remove("dtEdu");
                Session.Remove("empInfo");
            }
        }
        //protected void imgbtnReturn_Click(object sender, EvenImageClickEventArgstArgs e)
        //{
        //    if (ViewState["PreviousPageUrl"] != null)
        //    {
        //        Response.Redirect(ViewState["PreviousPageUrl"].ToString());

        //        Session.Remove("dtEdu");
        //        Session.Remove("empInfo");
        //    }
        //}

        protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
        {
            if (lbl_imgMsg.Text == "檔案刪除成功")
            {
                lbl_Msg.Text = "檔案已刪除無法取消!";
                return;
            }

            lbl_Msg.Text = string.Empty;
            lbl_eduMsg.Text = string.Empty;
            lbl_expMsg.Text = string.Empty;
            lbl_chaMsg.Text = string.Empty;

            delFile(); //取消新增，刪除已上傳檔案

            if (Request.QueryString["emp"] != null)
            {
                tbSetEmpty();
                tbSetVisible(false, "Edit");
                tbReadOnly(true);
                setEmpInfo((DataTable)Session["empInfo"]);
                updIsClick = false;
                imgN = string.Empty;
                lbl_imgMsg.Text = string.Empty;

                lbl_seniority.Text = "目前年資:";
            }
            else { Response.Redirect("EmployeeList.aspx"); }
        }

        protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (lbl_imgMsg.Text == string.Empty)
            {
                string imgName = ((DataTable)Session["empInfo"]).Rows[0]["ImgName"] == null ? string.Empty : ((DataTable)Session["empInfo"]).Rows[0]["ImgName"].ToString();
                if (!string.IsNullOrEmpty(imgName))
                {
                    
                }
                else
                {
                    if (filMyFile.HasFile)
                    {
                        lbl_Msg.Text = "請確認檔案是否上傳";
                        return;
                    }
                }
            }
            else if (lbl_imgMsg.Text != "檔案上傳成功")
            {
                lbl_Msg.Text = "請確認檔案是否上傳";
                return;
            }
            //==========================================
            //==========================================
            //** 等資料完整後，記得將註解內容復原 **//
            //==========================================
            //==========================================

            if (!chkField_emp())
            {
                return;
            }

            string empN = tb_employeeName.Text;
            string enFN = tb_EnFName.Text;
            string enLN = "";// tb_EnLName.Text;
            string dept = ucDept.myCodeValue;
            string country = ucCountry.CodeValue;
            string inDate = ucDate2.Date;
            string status = ucStatus.CodeValue;

            string phone = tb_phone1.Text + "-" + tb_phone2.Text + "-" + tb_phone3.Text;
            //==========================================
            phone = phone == "--" ? string.Empty : phone;
            //==========================================
            phone = strToEncrypt(phone);

            string sex = rb_sex.SelectedIndex == -1 ? string.Empty : rb_sex.SelectedValue.ToString();
            string birthday = ucDate1.Date;
            string company = ucCompany.myCodeValue;
            string pid = hf_EmployeePID.Value;
            string tele = tb_telephone1.Text + "-" + tb_telephone2.Text + "-" + tb_telephone3.Text;
            //==========================================
            tele = tele == "--" ? string.Empty : tele;
            //==========================================
            tele = strToEncrypt(tele);

            //==========================================
            /** 等資料完整後，記得將perID 註解，下方sql也要將perID去掉，身分證字號是不能修改的 **/
            //==========================================
            //==========================================
            string perID = tb_ID.Text;
            perID = strToEncrypt(perID);

            string marriage = rb_marriage.SelectedIndex == -1 ? string.Empty : rb_marriage.SelectedValue.ToString();
            string childN = tb_Children.Text == string.Empty ? string.Empty : tb_Children.Text;
            string bloodType=tb_BloodType.Text;
            string height = tb_Height.Text == string.Empty ? string.Empty : tb_Height.Text;
            string weight = tb_Weight.Text == string.Empty ? string.Empty : tb_Weight.Text;

            string address=tb_Address.Text;
            address = strToEncrypt(address);
            string MailAddress = tb_AddressC.Text;
            MailAddress = strToEncrypt(MailAddress);

            string ContactPerson = tb_ContactPer.Text == string.Empty ? string.Empty : tb_ContactPer.Text;
            string Relationship = tb_Relationship.Text == string.Empty ? string.Empty : tb_Relationship.Text;
            string ContactNum = tb_ContactPhone1.Text + "-" + tb_ContactPhone2.Text + "-" + tb_ContactPhone3.Text;
            //==========================================
            ContactNum = ContactNum == "--" ? string.Empty : ContactNum;
            //==========================================
            ContactNum = strToEncrypt(ContactNum);
            string ContactAddress = tb_contactAddress.Text == string.Empty ? string.Empty : tb_contactAddress.Text;
            ContactAddress = strToEncrypt(ContactAddress);

            string ResignationDT = ucDate3.Date;

            myEmp.update_EmpDetail(empN, enFN, enLN, dept, country, inDate, status, phone, sex, birthday, tele, perID, marriage, childN, bloodType, height, weight, address, MailAddress, Session[SessionString.userID].ToString(), imgN, company, pid, ContactPerson, Relationship, ContactNum, ContactAddress, ResignationDT); //身分證不會修改

            //因為目前資料未齊全，帶資料齊全後，將下方註解
            //================================================================================================
            string chaID = string.Empty;
            DataTable dt = new DataTable();
            dt = myCha.search_ChaID(hf_EmployeePID.Value, "ONBOARD");

            if ((dt == null || dt.Rows.Count == 0) && status == "ONSERVICE")
            {
                myCha.insert_ChaData(pid, "", dept, "", inDate, "ONBOARD", "0", "0", "1", Session[SessionString.userID].ToString(), company);
            }
            //================================================================================================

            //若更改到職日，則須修改異動紀錄
            if (inDIsChange == true)
            {
                dt = myCha.search_ChaID(hf_EmployeePID.Value, "ONBOARD");
                //if (dt != null || dt.Rows.Count > 0)
                //{
                chaID = dt.Rows[0]["ID"].ToString();
                myCha.update_ChaData(inDate, Session[SessionString.userID].ToString(), chaID);
                //}
            }

            if (status == "RESIGNATION")
            {
               DataTable dt_cha= myCha.search_EmpChaDetail(pid);
               if (dt_cha != null && dt_cha.Rows.Count > 0)
               {

                   myCha.insert_ChaData(pid, "", dept, "", now.Date.ToString(), "RESIGNATION", "0", "0", dt_cha.Rows[(dt_cha.Rows.Count - 1)]["SortOrder"].ToString(), Session[SessionString.userID].ToString(), company);
               }
               else
               {
                   myCha.insert_ChaData(pid, "", dept, "", now.Date.ToString(), "RESIGNATION", "0", "0", "1", Session[SessionString.userID].ToString(), company);
               }
            }
            //GlobalAnnounce.db.GetDataTable(sql);

            tbSetVisible(false, "Edit");
            tbReadOnly(true);

            if (status == "RESIGNATION")
            {
                imgbtnEdit.Visible = false;
            }

            //getTextBox();
            calculateSeniority();

            DataTable dt2 = myEmp.search_EmpDetail(Request.QueryString["emp"].ToString());
            Session["empInfo"] = dt2;
            setEmpInfo((DataTable)Session["empInfo"]);

            //到職日是否修改，改false
            inDIsChange = false;
            updIsClick = false;
            imgN = string.Empty;
            lbl_imgMsg.Text = string.Empty;


            //修改到職日後，現有gv不會更新
            //重新Load MultiView
            int ss = MainView.ActiveViewIndex;
            switch (ss)
            {
                case 0:
                    Tab1_Click(null, null);
                    break;
                case 1:
                    Tab2_Click(null, null);
                    break;
                case 2:
                    Tab3_Click(null, null);
                    break;
                case 3:
                    Tab4_Click(null, null);
                    break;
                case 4:
                    Tab5_Click(null, null);
                    break;
                case 5:
                    Tab6_Click(null, null);
                    break;
                case 6:
                    Tab7_Click(null, null);
                    break;
                case 7:
                    Tab8_Click(null, null);
                    break;
            }

            lbl_seniority.Text = "目前年資:";
            lbl_Msg.Text = string.Empty;
        }

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (lbl_imgMsg.Text == string.Empty)
            { }
            else if (lbl_imgMsg.Text != "檔案上傳成功")
            {
                lbl_Msg.Text = "請確認檔案是否上傳";
                return;
            }
           
            ImageButton saveImgBtn = sender as ImageButton;
            string saveID = saveImgBtn.ID;

            if (!chkField_emp())
            {
                return;
            }

            string empN = tb_employeeName.Text;
            string enFN = tb_EnFName.Text;
            string enLN = "";// tb_EnLName.Text;
            string dept = ucDept.myCodeValue;
            string empNo = tb_employeeNo.Text;
            string country = ucCountry.CodeValue;
            string inDate = ucDate2.Date;
            string status = ucStatus.CodeValue;
            
            string phone = tb_phone1.Text + "-" + tb_phone2.Text + "-" + tb_phone3.Text;
            phone = strToEncrypt(phone);

            string sex = rb_sex.SelectedItem.Value.ToString();
            string birthday = ucDate1.Date;
            string company = ucCompany.myCodeValue;
            string pid = hf_EmployeePID.Value;
            string initSen = tb_seniority.Text;

            string tele = tb_telephone1.Text + "-" + tb_telephone2.Text + "-" + tb_telephone3.Text;
            tele = strToEncrypt(tele);
            string perID = tb_ID.Text;
            perID = strToEncrypt(perID);

            string marriage = rb_marriage.SelectedItem.Value.ToString();
            string childN = tb_Children.Text;
            string bloodType = tb_BloodType.Text;
            string height = tb_Height.Text;
            string weight = tb_Weight.Text;

            string address = tb_Address.Text;
            address = strToEncrypt(address);
            string MailAddress = tb_AddressC.Text;
            MailAddress = strToEncrypt(MailAddress);
         

            string ContactPerson = tb_ContactPer.Text;
            string Relationship = tb_Relationship.Text;
            
            string ContactNum = tb_ContactPhone1.Text + "-" + tb_ContactPhone2.Text + "-" + tb_ContactPhone3.Text;
            ContactNum = strToEncrypt(ContactNum);
            string ContactAddress = tb_contactAddress.Text;
            ContactAddress = strToEncrypt(ContactAddress);
            
            string ResignationDT = ucDate3.Date;
            
            myEmp.insert_EmpDatail(empN, enFN, enLN, dept, empNo, country, initSen, inDate, status, phone, sex, birthday, tele, perID, marriage, childN, bloodType, height, weight, address, MailAddress, Session[SessionString.userID].ToString(), imgN, company, pid, ContactPerson, Relationship, ContactNum, ContactAddress, ResignationDT);

            //新人異動 (BoardID 可為空)
            myCha.insert_ChaData(pid, "", dept, "", inDate, "ONBOARD", initSen, "0", "1", Session[SessionString.userID].ToString(), company);
            
            if (saveID == "imgbtnSave")
            {
                Response.Redirect("EmployeeDetail.aspx?emp=" + hf_EmployeePID.Value);
            }
            else
            {
                imgbtnNew_Click(null, null);
            }

            imgN = string.Empty;
        }

        protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            lbl_seniority.Text = "初始年資:";

            if (Session["empInfo"] != null)
            {
                string initS = ((DataTable)Session["empInfo"]).Rows[0]["InitSeniority"] == null ? string.Empty : ((DataTable)Session["empInfo"]).Rows[0]["InitSeniority"].ToString();
                if (!string.IsNullOrEmpty(initS))
                {
                    tb_seniority.Text = initS;
                }
            }            
            else
            {
                DataTable dt = myEmp.search_EmpDetail(Request.QueryString["emp"].ToString());
                tb_seniority.Text = dt.Rows[0]["InitSeniority"].ToString();
                Session["empInfo"] = dt;
            }
            //tb_pid.Text = hf_EmployeePID.Value;
            tbSetVisible(true, "Edit");
            tbReadOnly(false);
            updIsClick = true;
            lbl_imgMsg.Text = string.Empty;
        }

        protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EmployeeDetail.aspx");
            //initNewEmp();
            //tbSetVisible(true, "Save");
            //tbReadOnly(false);
        }
        #endregion

        #region EDU
        private bool chkField_edu()
        {
            lbl_eduResult.Text = string.Empty;
            bool chkField = true;

            //=== 空白檢查 === 
            if (string.IsNullOrEmpty(tb_school.Text))
            {
                lbl_eduResult.Text = "請輸入學校!";
                chkField = false;
                return chkField;
            }

            if (string.IsNullOrEmpty(tb_major.Text))
            {
                lbl_eduResult.Text = "請輸入科系!";
                chkField = false;
                return chkField;
            }

            if (ucEduDegree.CodeSelectedIndex == 0)
            {
                lbl_eduResult.Text = "請選擇學位!";
                chkField = false;
                return chkField;
            }

            if (ucStatus.CodeSelectedIndex == 0)
            {
                lbl_eduResult.Text = "請選擇狀態!";
                chkField = false;
                return chkField;
            }

            DateTime startD = Convert.ToDateTime(ucEduStartDT.Date);
            if (startD.Date > now.Date)
            {
                lbl_eduResult.Text = "起始日期不能大於今日!";
                chkField = false;
                return chkField;
            }

            DateTime endD = Convert.ToDateTime(ucEduEndDT.Date);
            if (endD.Date > now.Date)
            {
                lbl_eduResult.Text = "結束日期不能大於今日!";
                chkField = false;
                return chkField;
            }

            return chkField;
        }

        protected void grid_edu_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    PrgUtil.AddSortIcon(e, gvCodeType); //對GridView抬頭增加排序的圖案 
                //}

                e.Row.Cells[(Int32)DFIdx_edu.ID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_edu.EmpPID_h].Visible = false;
                //e.Row.Cells[(Int32)DFIdx_edu.SchoolID_h].Visible = false;
                //e.Row.Cells[(Int32)DFIdx_edu.MajorID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_edu.DegreeID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_edu.StatusID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_edu.UpdUserID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_edu.UpdDT_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_edu.CompanyID_h].Visible = false;
            }
        }
        protected void grid_edu_Sorting(object sender, GridViewSortEventArgs e)
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
            string sort = "";

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
                case (Int32)DFIdx_edu.School:
                    name = "School";
                    break;
                case (Int32)DFIdx_edu.Major:
                    name = "Major";
                    break;
                case (Int32)DFIdx_edu.Degree:
                    name = "DegreeID";
                    break;
                case (Int32)DFIdx_edu.StartDT:
                    name = "StartDT";
                    break;
                case (Int32)DFIdx_edu.EndDT:
                    name = "EndDT";
                    break;
                case (Int32)DFIdx_edu.Status:
                    name = "StatusID";
                    break;
            }

            if (Session["dtEmp"] != null)
            {
                DataTable dt = (DataTable)Session["dtEdu"];
                dt.DefaultView.Sort = name + " " + sort;
                GlobalAnnounce.OtherFuction.BindDataToGrid(dt, grid_edu);
            }
        }
        protected void grid_edu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grid_edu.PageIndex = e.NewPageIndex;
            DataTable dtCode = (DataTable)Session["dtEdu"];
            grid_edu.DataSource = dtCode;
            grid_edu.DataBind();

        }
        private void setEduReadOnly(bool bb)
        {
            //hf_eduID.Value
            //tb_company.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.CompanyID_h].Text);
            //hf_ComapnyID.Value 
            tb_school.ReadOnly = bb;
            //ucEduSchool.myCodeEnabled = !bb;
            tb_major.ReadOnly = bb;
            //ucEduMajor.myCodeEnabled = !bb;
            //tb_degree.ReadOnly = bb;
            ucEduDegree.myCodeEnabled = !bb;
            ucEduStartDT.txtReadOnly(bb);
            ucEduStartDT.BtnDateEnabled = !bb;
            ucEduEndDT.txtReadOnly(bb);
            ucEduEndDT.BtnDateEnabled = !bb;
            //tb_startDT.ReadOnly = bb;
            //tb_endDT.ReadOnly = bb;
            ucEduStatus.myCodeEnabled = !bb;
            //tb_eduStatus.ReadOnly = bb;
            tb_eduComments.ReadOnly = bb;
        }
        protected void ImgBtn_eduNew_Click(object sender, ImageClickEventArgs e)
        {
            if (updIsClick == true)
            {
                lbl_eduMsg.Text = "請先完成員工資料!";
                this.mpe_edu.Hide();
            }
            else
            {
                lbl_eduResult.Text = string.Empty;

                hf_eduID.Value = string.Empty;
                //tb_CompanyCode.Text = tb_company.Text;hf_ComapnyID.Value
                //hf_CompanyID.Value = tb_company.Text;
                //ucEduSchool.CodeSelectedIndex = 0;
                //ucEduMajor.CodeSelectedIndex = 0;
                ucEduDegree.CodeSelectedIndex = 0;
                hf_CompanyID.Value = ucCompany.myCodeValue;
                tb_school.Text = string.Empty;
                tb_major.Text = string.Empty;
                //tb_degree.Text = string.Empty;
                ucEduStartDT.Date = string.Empty;
                ucEduEndDT.Date = string.Empty;
                //tb_startDT.Text = string.Empty;
                //tb_endDT.Text = string.Empty;
                ucEduStatus.CodeSelectedIndex = 0;
                //tb_eduStatus.Text = string.Empty;
                tb_eduComments.Text = string.Empty;

                //this.ModalPopupExtender1.Show();
                this.ImgBtn_eduEdit2.Visible = false;
                this.ImgBtn_eduSave.Visible = true;
                this.ImgBtn_eduUpdate.Visible = false;
                this.ImgBtn_eduSaveMore.Visible = true;

                this.mpe_edu.Show();
            }
        }
        protected void ImgBtn_eduEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (updIsClick == true)
            {
                lbl_eduMsg.Text = "請先完成員工資料!";
                this.mpe_edu.Hide();
            }
            else
            {
                lbl_eduResult.Text = string.Empty;

                ImageButton btndetails = sender as ImageButton;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

                hf_eduID.Value = gvrow.Cells[(Int32)DFIdx_edu.ID_h].Text;
                //tb_company.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.CompanyID_h].Text);
                hf_CompanyID.Value = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.CompanyID_h].Text);
                tb_school.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.School].Text);
                tb_major.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.Major].Text);
                ucEduDegree.CodeValue = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.DegreeID_h].Text);
                //tb_school.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.School].Text);
                //tb_major.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.Major].Text);
                //tb_degree.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.Degree].Text);
                //tb_startDT.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.StartDT].Text);
                ucEduStartDT.Date = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.StartDT].Text);
                //tb_endDT.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.EndDT].Text);
                ucEduEndDT.Date = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.EndDT].Text);
                ucEduStatus.CodeValue = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.StatusID_h].Text);
                //tb_eduStatus.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.StatusID_h].Text);
                tb_eduComments.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_edu.Comments].Text);

                setEduReadOnly(true);

                this.ImgBtn_eduEdit2.Visible = true;
                this.ImgBtn_eduSave.Visible = false;
                this.ImgBtn_eduUpdate.Visible = false;
                this.ImgBtn_eduSaveMore.Visible = false;
                //this.txtItemCode.ReadOnly = true;

                this.mpe_edu.Show();
            }
        }
        protected void ImgBtn_eduEdit2_Click(object sender, ImageClickEventArgs e)
        {

            setEduReadOnly(false);


            this.ImgBtn_eduEdit2.Visible = false;
            this.ImgBtn_eduSave.Visible = false;
            this.ImgBtn_eduUpdate.Visible = true;
            this.ImgBtn_eduSaveMore.Visible = false;

            this.mpe_edu.Show();
        }
        protected void ImgBtn_eduSave_Click(object sender, ImageClickEventArgs e)
        {            
            ImageButton imbeduSave = sender as ImageButton;
            string saveid = imbeduSave.ID;

            if (!chkField_edu())
            {
                this.mpe_edu.Show();
                return;
            }

            string pid = hf_EmployeePID.Value;
            string schoolID = tb_school.Text;
            string majorID = tb_major.Text;
            //string degreeID = ucEduDegree.CodeValue;
            //===============================================================
            string degreeID = ucEduDegree.CodeSelectedIndex == -1 ? string.Empty : ucEduDegree.CodeValue;
            //===============================================================
            string startD = ucEduStartDT.Date;
            string endD = ucEduEndDT.Date;
            string eduStatus = ucEduStatus.CodeValue;
            string eduComments = tb_eduComments.Text;
            string company = ucCompany.myCodeValue;
            //string sqlstr = "INSERT INTO hr_per_education(EmpPID, SchoolID, MajorID, DegreeID, StartDT, EndDT, Status, Comments, UpdUserID, UpdDT, CompanyID)";
            //sqlstr += " VALUES";
            //sqlstr += "(";
            //sqlstr += hf_EmployeePID.Value + ","
            //    + GlobalAnnounce.db.qo(tb_school.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_major.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_degree.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_startDT.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_endDT.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_eduStatus.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_eduComments.Text) + ","
            //    + Session[SessionString.userID].ToString() + ","
            //    + "NOW(),"
            //    + GlobalAnnounce.db.qo(tb_company.Text);
            //sqlstr += ")";

            myEdu.insert_EduData(pid, schoolID, majorID, degreeID, startD, endD, eduStatus, eduComments, Session[SessionString.userID].ToString(), company);

            //紀錄Session，為了sorting
            ////string sql = "SELECT * FROM hr_per_education WHERE EmpPID=" + hf_EmployeePID.Value;
            //DataTable dt = myEdu.search_EmpEduDetail(hf_EmployeePID.Value);

            ////目前沒資料才用ADD
            //dt.Columns.Add("School");
            //dt.Columns.Add("Major");
            //dt.Columns.Add("Degree");

            //Session["dtEdu"] = dt;

            //BindData(dt, grid_edu);

            EduData();

            if (saveid == "ImgBtn_eduSaveMore")
            {
                ImgBtn_eduNew_Click(null, null);
            }
        }
        protected void ImgBtn_eduUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (!chkField_edu())
            {
                this.mpe_edu.Show();
                return;
            }


            string eduID = hf_eduID.Value;
            string schoolID = tb_school.Text;
            string majorID = tb_major.Text;
            //string degreeID = ucEduDegree.CodeValue;
            //===============================================================
            string degreeID = ucEduDegree.CodeSelectedIndex == -1 ? string.Empty : ucEduDegree.CodeValue;
            //===============================================================
            string startD = ucEduStartDT.Date;
            string endD = ucEduEndDT.Date;
            string eduStatus = ucEduStatus.CodeValue;
            string eduComments = tb_eduComments.Text;
            string company = ucCompany.myCodeValue;

            //string sqlstr = "UPDATE hr_per_education";
            //sqlstr += " SET";
            //sqlstr += " SchoolID=" + GlobalAnnounce.db.qo(tb_school.Text) + ","
            //    + " MajorID=" + GlobalAnnounce.db.qo(tb_major.Text) + ","
            //    + " DegreeID=" + GlobalAnnounce.db.qo(tb_degree.Text) + ","
            //    + " StartDT=" + GlobalAnnounce.db.qo(tb_startDT.Text) + ","
            //    + " EndDT=" + GlobalAnnounce.db.qo(tb_endDT.Text) + ","
            //    + " Status=" + GlobalAnnounce.db.qo(tb_eduStatus.Text) + ","
            //    + " Comments=" + GlobalAnnounce.db.qo(tb_eduComments.Text) + ","
            //    + " UpdUserID=" + Session[SessionString.userID].ToString() + ","
            //    + " UpdDT=" + "NOW(),"
            //    + " CompanyID=" + GlobalAnnounce.db.qo(hf_CompanyID.Value);
            //sqlstr += " WHERE ID=" + hf_eduID.Value;

            myEdu.update_EduData(eduID, schoolID, majorID, degreeID, startD, endD, eduStatus, eduComments, Session[SessionString.userID].ToString(), company);

            //紀錄Session，為了sorting
            ////string sql = "SELECT * FROM hr_per_education WHERE EmpPID=" + hf_EmployeePID.Value;
            //DataTable dt = myEdu.search_EmpEduDetail(hf_EmployeePID.Value);

            ////目前沒資料才用ADD
            //dt.Columns.Add("School");
            //dt.Columns.Add("Major");
            //dt.Columns.Add("Degree");

            //Session["dtEdu"] = dt;

            //BindData(dt, grid_edu);

            EduData();
        }
        protected void ImgBtn_eduCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.mpe_edu.Hide();
        }
        #endregion

        #region EXP
        private bool chkField_exp()
        {
            lbl_expResult.Text = string.Empty;
            bool chkField = true;

            //=== 空白檢查 === 
            if (string.IsNullOrEmpty(tb_expCompanyN.Text))
            {
                lbl_expResult.Text = "請輸入公司名!";
                chkField = false;
                return chkField;
            }

            if (string.IsNullOrEmpty(tb_expPosition.Text))
            {
                lbl_expResult.Text = "請輸入職稱!";
                chkField = false;
                return chkField;
            }

            if (ucExpLeavedRsn.CodeSelectedIndex == 0)
            {
                lbl_expResult.Text = "請選擇離職原因!";
                chkField = false;
                return chkField;
            }

            DateTime startDate = Convert.ToDateTime(ucExpStartDT.Date);
            if (startDate.Date > now.Date)
            {
                lbl_expResult.Text = "開始日期不能大於今日!";
                chkField = false;
                return chkField;
            }

            DateTime endDate = Convert.ToDateTime(ucExpEndDT.Date);
            if (endDate.Date > now.Date)
            {
                lbl_expResult.Text = "結束日期不能大於今日!";
                chkField = false;
                return chkField;
            }

            //if (string.IsNullOrEmpty(tb_expPosition.Text))
            //{
            //    lbl_expResult.Text = "請輸入職稱!";
            //    chkField = false;
            //    return chkField;
            //}

            return chkField;
        }
        protected void grid_exp_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    PrgUtil.AddSortIcon(e, gvCodeType); //對GridView抬頭增加排序的圖案 
                //}

                e.Row.Cells[(Int32)DFIdx_exp.ID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_exp.EmpPID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_exp.LeavedRsnID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_exp.UpdUserID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_exp.UpdDT_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_exp.CompanyID_h].Visible = false;
            }
        }
        protected void grid_exp_Sorting(object sender, GridViewSortEventArgs e)
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
            string sort = "";

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
                case (Int32)DFIdx_exp.CompanyName:
                    name = "CompanyName";
                    break;
                case (Int32)DFIdx_exp.Department:
                    name = "Department";
                    break;
                case (Int32)DFIdx_exp.Position:
                    name = "Position";
                    break;
                case (Int32)DFIdx_exp.StartDT:
                    name = "StartDT";
                    break;
                case (Int32)DFIdx_exp.EndDT:
                    name = "EndDT";
                    break;
                case (Int32)DFIdx_exp.Seniority:
                    name = "Seniority";
                    break;
            }

            if (Session["dtExp"] != null)
            {
                DataTable dt = (DataTable)Session["dtExp"];
                dt.DefaultView.Sort = name + " " + sort;
                GlobalAnnounce.OtherFuction.BindDataToGrid(dt, grid_exp);
            }
        }
        private void setExpReadOnly(bool bb)
        {
            tb_expCompanyN.ReadOnly = bb;
            tb_expDept.ReadOnly = bb;
            tb_expPosition.ReadOnly = bb;
            ucExpStartDT.txtReadOnly(bb);
            ucExpStartDT.BtnDateEnabled = !bb;
            ucExpEndDT.txtReadOnly(bb);
            ucExpEndDT.BtnDateEnabled = !bb;
            tb_expSeniority.ReadOnly = bb;
            ucExpLeavedRsn.myCodeEnabled = !bb;
            //tb_LeavedRsn.ReadOnly = bb;
            tb_LeavedOtherRsn.ReadOnly = bb;
            tb_LeavedNote.ReadOnly = bb;
            tb_expAchievement.ReadOnly = bb;
        }
        protected void imgBtn_expNew_Click(object sender, ImageClickEventArgs e)
        {
            if (updIsClick == true)
            {
                lbl_expMsg.Text = "請先完成員工資料!";
                this.mpe_edu.Hide();
            }
            else
            {
                lbl_expResult.Text = string.Empty;

                hf_expID.Value = string.Empty;
                tb_expCompanyN.Text = string.Empty;
                hf_expCompanyID.Value = string.Empty;
                tb_expDept.Text = string.Empty;
                tb_expPosition.Text = string.Empty;
                ucExpStartDT.Date = string.Empty;
                ucExpEndDT.Date = string.Empty;
                //tb_expStartDT.Text = string.Empty;
                //tb_expEndDT.Text = string.Empty;
                tb_expSeniority.Text = string.Empty;
                ucExpLeavedRsn.CodeSelectedIndex = 0;
                //tb_LeavedRsn.Text = string.Empty;
                //hf_expLeavedRsnID.Value = string.Empty;
                tb_LeavedOtherRsn.Visible = false;
                tb_LeavedOtherRsn.Text = string.Empty;
                tb_LeavedNote.Text = string.Empty;
                tb_expAchievement.Text = string.Empty;


                this.ImgBtn_expEdit2.Visible = false;
                this.ImgBtn_expSave.Visible = true;
                this.ImgBtn_expUpdate.Visible = false;
                this.ImgBtn_expSaveMore.Visible = true;

                this.mpe_exp.Show();
            }
        }
        protected void imgBtn_expEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (updIsClick == true)
            {
                lbl_expMsg.Text = "請先完成員工資料!";
                this.mpe_edu.Hide();
            }
            else
            {
                lbl_eduResult.Text = string.Empty;

                ImageButton btndetails = sender as ImageButton;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

                hf_expID.Value = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.ID_h].Text);
                hf_expCompanyID.Value = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.CompanyID_h].Text);
                tb_expCompanyN.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.CompanyName].Text);
                tb_expDept.Text =  PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.Department].Text);
                tb_expPosition.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.Position].Text);
                ucExpStartDT.Date = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.StartDT].Text);
                ucExpEndDT.Date = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.EndDT].Text);
                tb_expSeniority.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.Seniority].Text);
                ucExpLeavedRsn.CodeValue = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.LeavedRsnID_h].Text);
                if (ucExpLeavedRsn.CodeValue == "D001")
                {
                    tb_LeavedOtherRsn.Visible = true;
                }
                //hf_expLeavedRsnID.Value = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.LeavedRsnID_h].Text);
                tb_LeavedOtherRsn.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.LeavedOtherRsn].Text);
                tb_LeavedNote.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.LeavedNote].Text);
                tb_expAchievement.Text = PrgUtil.getStr(gvrow.Cells[(Int32)DFIdx_exp.Achievement].Text);

                setExpReadOnly(true);

                this.ImgBtn_expEdit2.Visible = true;
                this.ImgBtn_expSave.Visible = false;
                this.ImgBtn_expUpdate.Visible = false;
                this.ImgBtn_expSaveMore.Visible = false;

                this.mpe_exp.Show();
            }
        }
        protected void ImgBtn_expEdit2_Click(object sender, ImageClickEventArgs e)
        {
            setExpReadOnly(false);

            this.ImgBtn_expEdit2.Visible = false;
            this.ImgBtn_expSave.Visible = false;
            this.ImgBtn_expUpdate.Visible = true;
            this.ImgBtn_expSaveMore.Visible = false;

            this.mpe_exp.Show();
        }
        protected void ImgBtn_expSave_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imbexpSave = sender as ImageButton;
            string saveid = imbexpSave.ID;

            if (!chkField_exp())
            {
                this.mpe_exp.Show();
                return;
            }
            //===============================================================
            //===============================================================
            //其他離職原因、離職備註、備註要加長度限制，因為sql欄位長度有限制
            //===============================================================
            //===============================================================
            string pid = hf_EmployeePID.Value;
            string companyN = tb_expCompanyN.Text;
            string department = tb_expDept.Text;
            string position = tb_expPosition.Text;
            string startD = ucExpStartDT.Date;
            string endD = ucExpEndDT.Date;
            string seniority = tb_expSeniority.Text;
            //string leavedRsnID = ucExpLeavedRsn.CodeValue;
            //===============================================================
            string leavedRsnID = ucExpLeavedRsn.CodeSelectedIndex == -1 ? string.Empty : ucExpLeavedRsn.CodeValue;
            //===============================================================
            string leavedOtherRsn = tb_LeavedOtherRsn.Text;
            string leavedNote = tb_LeavedNote.Text;
            string expAchievement = tb_expAchievement.Text;
            string company = ucCompany.myCodeValue;
            //string sqlstr = "INSERT INTO hr_per_experience(EmpPID, CompanyName, Department, Position, StartDT, EndDT, Seniority, LeavedRsnID, LeavedOtherRsn, LeavedNote, Comments, UpdUserID, UpdDT, CompanyID)";
            //sqlstr += " VALUES";
            //sqlstr += "(";
            //sqlstr += hf_EmployeePID.Value + ","
            //    + GlobalAnnounce.db.qo(tb_expCompanyN.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_expDept.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_expPosition.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_expStartDT.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_expEndDT.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_expSeniority.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_LeavedRsn.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_LeavedOtherRsn.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_LeavedNote.Text) + ","
            //    + GlobalAnnounce.db.qo(tb_expComments.Text) + ","
            //    + Session[SessionString.userID].ToString() + ","
            //    + "NOW(),"
            //    + GlobalAnnounce.db.qo(tb_company.Text);
            //sqlstr += ")";

            myExp.insert_ExpData(pid, companyN, department, position, startD, endD, seniority, leavedRsnID, leavedOtherRsn, leavedNote, expAchievement, Session[SessionString.userID].ToString(), company);

            //紀錄Session，為了sorting
            ////string sql = "SELECT * FROM hr_per_experience WHERE EmpPID=" + hf_EmployeePID.Value;
            //DataTable dt = myExp.search_EmpExpDetail(hf_EmployeePID.Value);

            ////目前沒資料才用ADD
            //dt.Columns.Add("LeavedRsn");

            //Session["dtExp"] = dt;

            //BindData(dt, grid_exp);

            ExpData();

            if (saveid == "ImgBtn_expSaveMore")
            {
                imgBtn_expNew_Click(null, null);
            }
        }
        protected void ImgBtn_expUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (!chkField_exp())
            {
                this.mpe_exp.Show();
                return;
            }
            //================================================================
            //================================================================
            // 其他離職原因、離職備註、備註要加長度限制，因為sql欄位長度有限制
            //================================================================
            //================================================================
            string expID = hf_expID.Value;
            string companyN = tb_expCompanyN.Text;
            string department = tb_expDept.Text;
            string position = tb_expPosition.Text;
            string startD = ucExpStartDT.Date;
            string endD = ucExpEndDT.Date;
            string seniority = tb_expSeniority.Text;
            //string leavedRsnID = ucExpLeavedRsn.CodeValue;
            //===============================================================
            string leavedRsnID = ucExpLeavedRsn.CodeSelectedIndex == -1 ? string.Empty : ucExpLeavedRsn.CodeValue;
            //===============================================================
            string leavedOtherRsn = tb_LeavedOtherRsn.Text;
            string leavedNote = tb_LeavedNote.Text;
            string expAchievement = tb_expAchievement.Text;
            string company = ucCompany.myCodeValue;
            //string sqlstr = "UPDATE hr_per_experience";
            //sqlstr += " SET";
            //sqlstr += " CompanyName=" + GlobalAnnounce.db.qo(tb_expCompanyN.Text) + ","
            //    + " Department=" + GlobalAnnounce.db.qo(tb_expDept.Text) + ","
            //    + " Position=" + GlobalAnnounce.db.qo(tb_expPosition.Text) + ","
            //    + " StartDT=" + GlobalAnnounce.db.qo(tb_expStartDT.Text) + ","
            //    + " EndDT=" + GlobalAnnounce.db.qo(tb_expEndDT.Text) + ","
            //    + " Seniority=" + GlobalAnnounce.db.qo(tb_expSeniority.Text) + ","
            //    + " LeavedRsnID=" + GlobalAnnounce.db.qo(tb_LeavedRsn.Text) + ","
            //    + " LeavedOtherRsn=" + GlobalAnnounce.db.qo(tb_LeavedOtherRsn.Text) + ","
            //    + " LeavedNote=" + GlobalAnnounce.db.qo(tb_LeavedNote.Text) + ","
            //    + " Comments=" + GlobalAnnounce.db.qo(tb_expComments.Text) + ","
            //    + " UpdUserID=" + Session[SessionString.userID].ToString() + ","
            //    + " UpdDT=" + "NOW()" + ","
            //    + " CompanyID=" + GlobalAnnounce.db.qo(hf_expCompanyID.Value);
            //sqlstr += " WHERE ID=" + hf_expID.Value;

            myExp.update_ExpData(expID, companyN, department, position, startD, endD, seniority, leavedRsnID, leavedOtherRsn, leavedNote, expAchievement, Session[SessionString.userID].ToString(), company);

            //紀錄Session，為了sorting
            ////string sql = "SELECT * FROM hr_per_experience WHERE EmpPID=" + hf_EmployeePID.Value;
            //DataTable dt = myExp.search_EmpExpDetail(hf_EmployeePID.Value);

            ////目前沒資料才用ADD
            //dt.Columns.Add("LeavedRsn");

            //Session["dtExp"] = dt;

            //BindData(dt, grid_exp);

            ExpData();
        }
        protected void ImgBtn_expCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.mpe_exp.Hide();
        }
        #endregion

        #region Leave
        protected void imgbtn_leaveNew_Click(object sender, ImageClickEventArgs e)
        {
            string empNo = HR_class.encodeBase64(tb_employeeNo.Text);
            string str_url = "../HR_QuicklyImportDT.aspx?&wn=" + empNo + "&date=" + now.Date.ToString("yyyy-MM-dd");

            //Response.Redirect(str_url);RegisterStartupScript
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test", "window.open('" + str_url + "'); ", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "window.open('" + str_url + "','_blank'); ", true);
        }
        #endregion

        #region CHANGE
        protected void grid_cha_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    PrgUtil.AddSortIcon(e, gvCodeType); //對GridView抬頭增加排序的圖案 
                //}

                e.Row.Cells[(Int32)DFIdx_cha.ID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_cha.EmpPID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_cha.DeptID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_cha.StatusID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_cha.UpdUserID_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_cha.UpdDT_h].Visible = false;
                e.Row.Cells[(Int32)DFIdx_cha.CompanyID_h].Visible = false;
            }
        }
        #endregion

        protected void btn_pdf_Click(object sender, EventArgs e)
        {
            //產生PDF檔
            //////////////////////////////////////////////////////////////////////////////////// 

            EmpolyeeDetail myEmpDetail = new EmpolyeeDetail();
            myEmpDetail.EmpPID = int.Parse(hf_EmployeePID.Value);
            myEmpDetail.EmpName = tb_employeeName.Text;
            myEmpDetail.ChiName = tb_employeeName.Text;
            myEmpDetail.EnFName = tb_EnFName.Text;
            myEmpDetail.EnLName = "";// tb_EnLName.Text;
            myEmpDetail.DeptID = int.Parse(ucDept.myCodeValue); 
            myEmpDetail.Dept = ucDept.myCodeText;
            myEmpDetail.EmpNo = int.Parse(tb_employeeNo.Text);
            myEmpDetail.EmpCountryID = ucCountry.CodeValue;
            myEmpDetail.EmpCountry = ucCountry.CodeText;
            myEmpDetail.Seniority =  tb_seniority.Text;
            myEmpDetail.OnboardDT = ucDate2.Date;
            myEmpDetail.Status = ucStatus.CodeValue;
            myEmpDetail.PhoneNum = tb_phone1.Text + "-" + tb_phone2.Text + "-" + tb_phone3.Text;
            myEmpDetail.Sex = rb_sex.SelectedValue;
            myEmpDetail.Birthday = ucDate1.Date;
            myEmpDetail.ImgName = hf_imgN.Value;
            myEmpDetail.CompanyID = int.Parse(ucCompany.myCodeValue);
            myEmpDetail.Company = ucCompany.myCodeText;
            myEmpDetail.Telephone = tb_telephone1.Text + "-" + tb_telephone2.Text + "-" + tb_telephone3.Text;
            myEmpDetail.PerID = tb_ID.Text;
            myEmpDetail.MarriageYN = rb_marriage.SelectedValue;
            myEmpDetail.ChildrenNum = int.Parse(tb_Children.Text);
            myEmpDetail.BloodType = tb_BloodType.Text;
            myEmpDetail.Height = tb_Height.Text + " cm";
            myEmpDetail.Weight = tb_Weight.Text + " kg";
            myEmpDetail.Address = tb_Address.Text;
            myEmpDetail.MailAddress = tb_AddressC.Text;
            myEmpDetail.ContactPerson = tb_ContactPer.Text;
            myEmpDetail.Relationship = tb_Relationship.Text;
            myEmpDetail.ContactNum = tb_ContactPhone1.Text + "-" + tb_ContactPhone2.Text + "-" + tb_ContactPhone3.Text;
            myEmpDetail.ContactAddress = tb_contactAddress.Text;    

            if (!string.IsNullOrEmpty(ucDate3.Date))
            {
                myEmpDetail.ResignationDT = ucDate3.Date;
            }
            string json = JsonConvert.SerializeObject(myEmpDetail);
            //downloadpdf(json);

            string URL = "~/HR/personnel/EmployeeDetailPdf.aspx?json=" + json;
            URL = Page.ResolveClientUrl(URL);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + URL + "','mywindow','menubar=0,resizable=1,width=auto,height=auto');", true);
        }

        //上傳檔案
        //path = 絕對位置, fup = FileUpload, allowedExtextsion = new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" }
        //maxFileSize = 5100000(5MB), msg = erroe mag Label
        //private void UploadFile(string path, FileUpload fup, List<string> allowedExtextsion, int maxFileSize, Label msg) //
        //{
        //    msg.Text = string.Empty;

        //    if (fup.HasFile == false)
        //    {
        //        msg.Text = "未選擇檔案";
        //        return;
        //    }

        //    // filMyFile.FileName 只有 "檔案名稱.副檔名"，並沒有 Client 端的完整理路徑
        //    string filename = fup.FileName;

        //    // 副檔名
        //    string extension = Path.GetExtension(filename).ToLowerInvariant();
        //    // 判斷是否為允許上傳的檔案副檔名
        //    //allowedExtextsion = new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" };
        //    if (allowedExtextsion.IndexOf(extension) == -1)
        //    {
        //        msg.Text = "不允許該檔案上傳";
        //        return;
        //    }

        //    // 限制檔案大小，限制為 5MB
        //    int filesize = fup.PostedFile.ContentLength;
        //    if (filesize > maxFileSize)
        //    {
        //        msg.Text = "檔案大小超過上限，該檔案無法上傳";
        //        return;
        //    }

        //    // 檢查 Server 上該資料夾是否存在，不存在就自動建立
        //    string serverDir = path;  //path = 絕對位置
        //    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

        //    // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
        //    // 使用 Path.Combine 來集合路徑的優點
        //    /** 以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
        //     *  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
        //     *  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
        //     *  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合**/
        //    string serverFilePath = Path.Combine(serverDir, filename);
        //    string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
        //    int fileCount = 1;
        //    while (File.Exists(serverFilePath))
        //    {
        //        // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
        //        filename = string.Concat(fileNameOnly, "_", fileCount, extension);
        //        serverFilePath = Path.Combine(serverDir, filename);
        //        fileCount++;
        //    }

        //    // 把檔案傳入指定的 Server 內路徑
        //    try
        //    {
        //        fup.SaveAs(serverFilePath);
        //        msg.Text = "檔案上傳成功";
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.Text = "檔案上傳失敗，";
        //        msg.Text += ex.Message;
        //    }
        //}

        protected void grid_exp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_exp.PageIndex = e.NewPageIndex;
            DataTable dtCode = (DataTable)Session["dtExp"];
            grid_exp.DataSource = dtCode;
            grid_exp.DataBind(); 
        }

        protected void btn_def_Click(object sender, EventArgs e)
        {
            //什麼都不做，避免使用者誤按Enter用
        }
        #region NEW
        protected void imgbtn_fileNew_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EmployeeFile.aspx?emp=" + hf_EmployeePID.Value + "&status=1");
        }

        protected void btn_fileView_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeFile.aspx?emp=" + hf_EmployeePID.Value + "&status=2");
        }

        protected void file_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    if (e.Row.Cells[i].Text == "x")
                //    {
                //        e.Row.Cells[i].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[i].Text + "</b></span>";
                //    }
                //}
                //if (Convert.ToInt32(e.Row.Cells[2].Text) > 0)
                //{
                //    e.Row.Cells[2].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[2].Text + "</b></span>";
                //}

                if (e.Row.Cells[3].Text == "x")
                {
                    e.Row.Cells[3].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[3].Text + "</b></span>";
                }
            }
        }

        protected void imgBtn_fileEdit_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EmployeeFile.aspx?emp=" + hf_EmployeePID.Value + "&status=2");
        }
        #endregion

        //================================================================
        //加密函式   
        private byte[] encrypt(string string_secretContent, string string_pwd)
        {
            //密碼轉譯一定都是用byte[] 所以把string都換成byte[]   
            byte[] byte_secretContent = Encoding.UTF8.GetBytes(string_secretContent);
            byte[] byte_pwd = Encoding.UTF8.GetBytes(string_pwd);
            //加解密函數的key通常都會有固定的長度 而使用者輸入的key長度不定 因此用hash過後的值當做key   
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_pwdMD5 = provider_MD5.ComputeHash(byte_pwd);
            //產生加密實體 如果要用其他不同的加解密演算法就改這裡(ex:AES)   
            RijndaelManaged provider_AES = new RijndaelManaged();
            ICryptoTransform encrypt_AES = provider_AES.CreateEncryptor(byte_pwdMD5, byte_pwdMD5);
            //output就是加密過後的結果   
            byte[] output = encrypt_AES.TransformFinalBlock(byte_secretContent, 0, byte_secretContent.Length);
            return output;
        }
        //解密函式   
        private string decrypt(byte[] byte_ciphertext, string string_pwd)
        {
            //密碼轉譯一定都是用byte[] 所以把string都換成byte[]   
            byte[] byte_pwd = Encoding.UTF8.GetBytes(string_pwd);
            //加解密函數的key通常都會有固定的長度 而使用者輸入的key長度不定 因此用hash過後的值當做key   
            MD5CryptoServiceProvider provider_MD5 = new MD5CryptoServiceProvider();
            byte[] byte_pwdMD5 = provider_MD5.ComputeHash(byte_pwd);
            //產生解密實體   
            RijndaelManaged provider_AES = new RijndaelManaged();
            ICryptoTransform decrypt_AES = provider_AES.CreateDecryptor(byte_pwdMD5, byte_pwdMD5);
            //string_secretContent就是解密後的明文   
            byte[] byte_secretContent = decrypt_AES.TransformFinalBlock(byte_ciphertext, 0, byte_ciphertext.Length);
            string string_secretContent = Encoding.UTF8.GetString(byte_secretContent);
            return string_secretContent;
        }
        //================================================================

        //================================================================
        //使用的加密
        private string strToEncrypt(string txt)
        {
            if (txt == String.Empty)
                return String.Empty;

            string newString = txt;
            byte[] b = encrypt(newString, pwd);
            string encryptStr = Convert.ToBase64String(b);
            return encryptStr;
        }
        //使用的解密
        private string strToDecrypt(string str)
        {
            //防呆   
            if (str == String.Empty)
                return String.Empty;
            //解密   
            byte[] b = Convert.FromBase64String(str);
            string newString = decrypt(b, pwd);
            //拆掉亂數   
            return newString;
        }

        


        ////地址使用的加密
        //private string strToEncryptAddress(string txt)
        //{
        //    if (txt == String.Empty)
        //        return String.Empty;
        //    Random r = new Random();
        //    int[] ramdon3 = new int[4];
        //    //確保TextBox1的長度符合要求   
        //    while (txt.Length < 50)
        //    {
        //        txt += " ";
        //    }
        //    //產生3組不同長度亂數   
        //    ramdon3[0] = r.Next(0, 9);
        //    ramdon3[1] = r.Next(10, 99);
        //    ramdon3[2] = r.Next(1000, 9999);
        //    ramdon3[3] = r.Next(100, 999);
        //    string newString = ramdon3[0] + txt.Substring(0, 8) + ramdon3[1] + txt.Substring(8, 15) + ramdon3[2] + txt.Substring(23, 11) + ramdon3[3] + txt.Substring(34, 16);
        //    byte[] b = encrypt(newString, pwd);
        //    string encryptStr = Convert.ToBase64String(b);
        //    return encryptStr;
        //}
        ////地址使用的解密
        //private string strToDecryptAddress(string str)
        //{
        //    //防呆   
        //    if (str == String.Empty)
        //        return String.Empty;
        //    //解密   
        //    byte[] b = Convert.FromBase64String(str);
        //    string newString = decrypt(b, pwd);
        //    //拆掉亂數   
        //    return newString.Substring(1, 8) + newString.Substring(11, 15) + newString.Substring(30, 11) + newString.Substring(44, 16);
        //}
        ////================================================================



        // 計算byte(中文字會算 2bytes)
        //private void countWords()
        //{
        //    byte[] byteStr = Encoding.GetEncoding("big5").GetBytes(TextBox1.Text); //把string轉為byte 
        //    Label8.Text = byteStr.Length.ToString(); //取byte的長度, 中文字就會算2碼了
        //}

    }
}