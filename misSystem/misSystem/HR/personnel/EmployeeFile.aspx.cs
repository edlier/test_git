using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using MisSystem_ClassLibrary;
using MisSystem_ClassLibrary.HR.personnel;
using MisSystem_ClassLibrary.HR;
using MisSystem_ClassLibrary.sys;

namespace misSystem.HR.personnel
{
    public partial class EmployeeFile : System.Web.UI.Page
    {
        public static CommonUtils PrgUtil = new CommonUtils();
        public static EmployeeDB myEmp = new EmployeeDB();
        public static SysCodeDB mySysC = new SysCodeDB();
        public static SysSetting mySysS = new SysSetting();
        public static NewRecuritDB myNew = new NewRecuritDB();
        string pathRel = string.Empty;
        string pathAbs = string.Empty;        

        private enum DFIdx_new
        {
            RowNumber = (Int32)0,
            Code = (Int32)1,
            FileYN = (Int32)2,
            checkYN = (Int32)3,
            codeN = (Int32)4,
            Descr = (Int32)5,
            FileName = (Int32)6,
            FileNameTxt = (Int32)7,
            commentTxt = (Int32)8,
            comment = (Int32)9,

            UpdUserID_h = (Int32)10,
            UpdUserIDN = (Int32)11,
            UpdDT_h = (Int32)12,
            Edit = (Int32)13
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //把預設按鈕指定給它
            this.Page.Form.DefaultButton = btn_def.UniqueID;
            //再把它隱藏起來
            btn_def.Style.Add("display", "none");

            DataTable dt_sysS = mySysS.search_SysSetting("PER");
            pathAbs = dt_sysS.Rows[0]["SettingSrting"].ToString(); //"http://192.168.168.47:8090/HR";
            pathRel = dt_sysS.Rows[1]["SettingSrting"].ToString(); //"C:/BPM/HR";

            if (!IsPostBack)
            {
                // 設定前網頁
                if (Request.UrlReferrer != null)
                {
                    ViewState["PreviousPageUrl"] = Request.UrlReferrer.ToString();
                }

                //status→ 1:new, 2:view
                if (Request.QueryString["emp"] != null && Request.QueryString["status"] != null)
                {
                    string status = Request.QueryString["status"].ToString();
                    string empN = Request.QueryString["emp"].ToString();
                    DataTable dt = myEmp.search_EmpDetail(empN);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lbl_empName.Text = dt.Rows[0]["EmpName"].ToString();
                        lbl_empNo.Text = dt.Rows[0]["EmpNo"].ToString();
                        lbl_inD.Text = DateTime.Parse(dt.Rows[0]["OnboardDT"].ToString()).ToString("yyyy/MM/dd");
                        hf_company.Value = dt.Rows[0]["CompanyID"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('不存在員工!!!'); location.href='EmployeeList.aspx';", true);
                    }

                    //需要再加上權限的問題
                    if (status == "1")
                    {                        
                        //setEnable(true,false);
                        setBtnVisible("New");
                        grid_new.Columns[(Int32)DFIdx_new.UpdDT_h].Visible = false;
                        grid_new.Columns[(Int32)DFIdx_new.UpdUserIDN].Visible = false;
                        grid_new.Columns[(Int32)DFIdx_new.Edit].Visible = false;
                        grid_new.Columns[(Int32)DFIdx_new.FileName].Visible = true;
                        grid_new.Columns[(Int32)DFIdx_new.FileNameTxt].Visible = false;
                        grid_new.Columns[(Int32)DFIdx_new.FileYN].Visible = true;
                        grid_new.Columns[(Int32)DFIdx_new.checkYN].Visible = false;
                        grid_new.Columns[(Int32)DFIdx_new.commentTxt].Visible = true;
                        grid_new.Columns[(Int32)DFIdx_new.comment].Visible = false;
                        cb_all.Visible = true;

                        DataTable dt2 = myNew.search_isExist(empN);
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('已創建員工繳交內容，無法再次新增!!!'); location.href='EmployeeFile.aspx?emp=" + empN + "&status=2&page=" + Request.QueryString["page"].ToString() + "&sEmp=" + Request.QueryString["sEmp"].ToString() + "&sSta=" + Request.QueryString["sSta"].ToString() + "';", true);
                        }
                        else
                        {
                            DataTable dt_new = mySysC.search_NEWRECRUIT();
                            dt_new.Columns.Add("Comment");
                            dt_new.Columns.Add("checkYN");
                            dt_new.Columns.Add("FileYN");
                            dt_new.Columns.Add("FileName");
                            dt_new.Columns.Add("htmlStr");
                            dt_new.Columns.Add("UpdUserName");
                            GlobalAnnounce.OtherFuction.BindDataToGrid(dt_new, grid_new);
                        }
                    }
                    else if (status == "2")
                    {
                        //setEnable(false, false);
                        setBtnVisible("View");
                                                
                        DataTable dt2 = myNew.search_isExist(empN);
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            setGridview();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('尚未創建員工繳交內容，請先新增!!!'); location.href='EmployeeFile.aspx?emp=" + empN + "&status=1&page=" + Request.QueryString["page"].ToString() + "&sEmp=" + Request.QueryString["sEmp"].ToString() + "&sSta=" + Request.QueryString["sSta"].ToString() + "';", true);
                        }



                        //string userid = Session[SessionString.userID].ToString();

                        //if (userid == "14")
                        //{
                        //    //panel_hr.Visible = false;
                        //    //panel_mis.Visible = true;
                        //}
                        //else
                        //{
                        //    //panel_hr.Visible = true;
                        //    //panel_mis.Visible = false;
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('URL錯誤!!!'); location.href='EmployeeFileList.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('URL錯誤!!!'); location.href='EmployeeFileList.aspx';", true);
                }
            }
        }

        private void setEnable(bool b_hr, bool b_mis)
        {
            //cb_1.Enabled = b_hr;
            //cb_2.Enabled = b_hr;
            //cb_3.Enabled = b_hr;
            //cb_4.Enabled = b_hr;
            //cb_5.Enabled = b_hr;
            //cb_6.Enabled = b_hr;
            //cb_7.Enabled = b_hr;
            //cb_8.Enabled = b_hr;
            //cb_9.Enabled = b_mis;

            //tb_1.ReadOnly = !b_hr;
            //tb_2.ReadOnly = !b_hr;
            //tb_3.ReadOnly = !b_hr;
            //tb_4.ReadOnly = !b_hr;
            //tb_5.ReadOnly = !b_hr;
            //tb_6.ReadOnly = !b_hr;
            //tb_7.ReadOnly = !b_hr;
            //tb_8.ReadOnly = !b_hr;
            //tb_9.ReadOnly = !b_mis;
        }

        //目前狀態(ex:View,Edit,New)
        private void setBtnVisible(string ss)
        {
            if (ss == "Edit")
            {
                imgbtnUpdate.Visible = true;
                imgbtnExit.Visible = true;
                //imgbtnEdit.Visible = false;
                imgbtnSave.Visible = false;
                //imgbtnSaveMore.Visible = false;
                btnReturn.Visible = false;
            }
            else if (ss == "New")
            {
                imgbtnSave.Visible = true;
                //imgbtnSaveMore.Visible = true;
                imgbtnExit.Visible = true;
                imgbtnUpdate.Visible = false;
                //imgbtnEdit.Visible = false;
                btnReturn.Visible = false;
            }
            else if (ss == "View")
            {
                //imgbtnEdit.Visible = true;
                btnReturn.Visible = true;
                imgbtnSave.Visible = false;
                //imgbtnSaveMore.Visible = false;
                imgbtnExit.Visible = false;
                imgbtnUpdate.Visible = false;
            }
        }

        protected void btn_def_Click(object sender, EventArgs e)
        {
            //什麼都不做，避免使用者誤按Enter用
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string page = Request.QueryString["page"] == string.Empty ? string.Empty : Request.QueryString["page"].ToString();
            string sEmp = Request.QueryString["sEmp"] == string.Empty ? string.Empty : Request.QueryString["sEmp"].ToString();
            string sSta = Request.QueryString["sSta"] == string.Empty ? string.Empty : Request.QueryString["sSta"].ToString();

            Response.Redirect("EmployeeFileList.aspx?page=" + page + "&sEmp=" + sEmp + "&sSta=" + sSta);
        }

        protected void grid_new_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grid_new.EditIndex = e.NewEditIndex;
            setGridview();
        }

        protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < grid_new.Rows.Count; i++)
            {
                Label code = (Label)grid_new.Rows[i].FindControl("lbl_code");
                Label codeN = (Label)grid_new.Rows[i].FindControl("lbl_codeN");
                CheckBox YN = (CheckBox)grid_new.Rows[i].FindControl("cb_YN1");
                if (YN.Checked == true)
                { YN.Text = "Y"; }
                else { YN.Text = "N"; }
                TextBox comment = (TextBox)grid_new.Rows[i].FindControl("tb_commentTxt");
                FileUpload file =(FileUpload)grid_new.Rows[i].FindControl("fup_file");
                string fileN = string.Empty;

                // 允許的檔案格式不同，除了照片外皆為掃描檔(.pdf)
                if (codeN.Text == "照片")
                {
                    PrgUtil.UploadFile(pathRel + "/EmpFile/" + codeN.Text, file, new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" }, 10100000, Label2, lbl_empNo.Text + "_" + codeN.Text);
                }
                else
                {
                    PrgUtil.UploadFile(pathRel + "/EmpFile/" + codeN.Text, file, new List<string> { ".pdf" }, 10100000, Label2, lbl_empNo.Text + "_" + codeN.Text);
                }

                if (file.HasFile == true)
                {
                    fileN = lbl_empNo.Text + "_" + codeN.Text + Path.GetExtension(file.FileName).ToLowerInvariant();
                }

                if (Label2.Text == "檔案上傳成功")
                {
                    myNew.insert_NewRecurit(Request.QueryString["emp"].ToString(), code.Text, YN.Text, comment.Text, fileN, Session[SessionString.userID].ToString(), "NOW()", hf_company.Value);
                }
                else
                {
                    myNew.insert_NewRecurit(Request.QueryString["emp"].ToString(), code.Text, YN.Text, comment.Text, "", Session[SessionString.userID].ToString(), "NOW()", hf_company.Value);
                }
                
                
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('新增成功!!!'); location.href='EmployeeFile.aspx?emp=" + Request.QueryString["emp"].ToString() + "&status=2&page=" + Request.QueryString["page"].ToString() + "&sEmp=" + Request.QueryString["sEmp"].ToString() + "&sSta=" + Request.QueryString["sSta"].ToString() + "'", true);
            //Response.Redirect("EmployeeFile.aspx?emp=" + Request.QueryString["emp"].ToString() + "&status=2");
        }

        protected void grid_new_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string code = ((Label)grid_new.Rows[e.RowIndex].FindControl("lbl_code")).Text;
            string codeN = ((Label)grid_new.Rows[e.RowIndex].FindControl("lbl_codeN")).Text;
            string YN = ((CheckBox)grid_new.Rows[e.RowIndex].FindControl("cb_edtYN")).Checked == true ? "Y" : "N";
            string comment = ((TextBox)grid_new.Rows[e.RowIndex].FindControl("tb_editcomment")).Text;
            string nid = ((Label)grid_new.Rows[e.RowIndex].FindControl("lbl_NID")).Text;
            FileUpload fileU = (FileUpload)grid_new.Rows[e.RowIndex].FindControl("fup_editFile");

            string serverDir = pathRel + "/EmpFile/" + codeN; //路徑
            string saveName = lbl_empNo.Text + "_" + codeN; //儲存檔名
            string extension = Path.GetExtension(fileU.FileName).ToLowerInvariant(); //副檔名
            string fileN = string.Empty;

            if (fileU.HasFile == true)
            {
                fileN = PrgUtil.repeatFileN(serverDir, saveName, extension); //實際儲存檔名(重覆檔名的問題)
            }

            // [允許的檔案格式不同，除了照片外皆為掃描檔(.pdf)
            if (codeN == "照片")
            {
                PrgUtil.UploadFile(serverDir, fileU, new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" }, 10100000, Label2, saveName);
            }
            else
            {
                PrgUtil.UploadFile(serverDir, fileU, new List<string> { ".pdf" }, 10100000, Label2, saveName);
            }

            if (Label2.Text == "檔案上傳成功")
            {
                myNew.update_NewRecurit(nid, YN, fileN, comment, Session[SessionString.userID].ToString(), "NOW()");
            }
            else
            {
                myNew.update_NewRecurit(nid, YN, "", comment, Session[SessionString.userID].ToString(), "NOW()");
            }

            //設定-1讓GridView離開編輯狀態
            grid_new.EditIndex = -1;
            setGridview();
        }

        protected void grid_new_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grid_new.EditIndex = -1;           
            setGridview();
        }

        private void setGridview()
        {
            DataTable dt_new = myNew.search_NewRecurit(Request.QueryString["emp"].ToString());
            dt_new.Columns.Add("checkYN");
            dt_new.Columns.Add("UpdUserName");
            dt_new.Columns.Add("htmlStr");
            foreach (DataRow dr in dt_new.Rows)
            {
                if (dr["FileYN"].ToString() == "Y")
                {
                    dr["checkYN"] = "v";
                }
                else
                {
                    dr["checkYN"] = "<span style=\"color: #ff0033\"><b>x</b></span>";
                }

                if (dr["UpdUserID"].ToString() != "0" && !string.IsNullOrEmpty(dr["UpdUserID"].ToString()))
                {
                    DataTable dt = myEmp.search_EmpName(dr["UpdUserID"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dr["UpdUserName"] = dt.Rows[0]["EmpName"].ToString();
                    }
                }
                else
                {
                    dr["UpdUserName"] = string.Empty;
                }
                dr["htmlStr"] = pathAbs + "/EmpFile/" + dr["CodeName"].ToString() + "/" + dr["FileName"].ToString();
            }
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt_new, grid_new);
        }

        protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["PreviousPageUrl"] != null)
            {
                //Response.Redirect(ViewState["PreviousPageUrl"].ToString());

                string page = Request.QueryString["page"] == string.Empty ? string.Empty : Request.QueryString["page"].ToString();
                string sEmp = Request.QueryString["sEmp"] == string.Empty ? string.Empty : Request.QueryString["sEmp"].ToString();
                string sSta = Request.QueryString["sSta"] == string.Empty ? string.Empty : Request.QueryString["sSta"].ToString();

                string[] spiltW = ViewState["PreviousPageUrl"].ToString().Split('?');
                Response.Redirect(spiltW[0] + "?page=" + page + "&sEmp=" + sEmp + "&sSta=" + sSta);
            }
        }

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_all.Checked)
            {
                for(int i=0;i<grid_new.Rows.Count;i++)
                {
                    CheckBox cb = (CheckBox)grid_new.Rows[i].FindControl("cb_YN1");
                    cb.Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < grid_new.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grid_new.Rows[i].FindControl("cb_YN1");
                    cb.Checked = false;
                }
            }
        }

    }
}