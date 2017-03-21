using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics; //Debug.pring();

namespace misSystem.HR
{
    public partial class HR_equipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HR_class.prevent_cache(); //防止回上一頁重load表單

            if (!IsPostBack)
            {                
                if (!HR_class.check_login(this.Page)) { return; }

                workNum_lab.Text = HR_class.getWorkerNum(Session["check_workerNum"], Session[SessionString.userID].ToString());

                string str = "select workerNum, missysID from au_userinfo where workerNum = " + workNum_lab.Text + ";";
                name_lab.Text = (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["missysID"].ToString();

                if (Session["check_sn"] != null) //若session存在表示是審核狀態
                {
                    form_panel.Enabled = false;

                    string sn = Session["check_sn"].ToString();
                    string ap = Session["check_ap"].ToString();
                   
                    str = "select * from hr_application_equipment where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    equipment_text.Text = dt.Rows[0]["equipment"].ToString().Trim();            
                    content_text.Text = dt.Rows[0]["content"].ToString().Trim();
                    price_text.Text = dt.Rows[0]["price"].ToString().Trim();
                    ViewState["status"] = dt.Rows[0]["status"].ToString();

                    file_panel.Visible = false;
                    pdf_panel.Visible = true;
                    ViewState["check_sn"] = Session["check_sn"].ToString();

                    Session.Remove("check_sn");
                    Session.Remove("check_ap");
                    Session.Remove("check_workerNum");
                    
                    send_btn.Visible = false;
                    pass_btn.Visible = true;
                    fail_btn.Visible = true;
                }
                else if (Request.QueryString["SN"] != null) //若get值存在，表示修改或刪除表單
                {
                    string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
                    str = "select * from hr_application_equipment where SN = " + sn + ";";
                    DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    equipment_text.Text = dt.Rows[0]["equipment"].ToString().Trim();
                    content_text.Text = dt.Rows[0]["content"].ToString().Trim();
                    price_text.Text = dt.Rows[0]["price"].ToString().Trim();

                    send_btn.Visible = false;
                    edit_btn.Visible = true;
                    delete_btn.Visible = true;
                }
            }
        }

        protected void send_btn_Click(object sender, EventArgs e)
        {            
            if (!checkInput()) { return; } //確認資料合理性
                     
            string insert_key = "";
            if (fileUpload())//上傳pdf，成功為true/失敗為false
            {
                string path = Server.MapPath("~/HR/PDF_equipment/");
                Debug.WriteLine(path);
                try
                {   
                    string str;
                    if (isFirstUpload_lab.Text == "true") //若此為true表示尚未新增表單資料，需新增
                    {
                        int status = HR_class.getStatus(Session["auDep5"].ToString());
                        str = "insert into hr_application_equipment(workerNum, equipment, content, price, status) values(" + workNum_lab.Text + ",'" + equipment_text.Text + "','" + content_text.Text + "','" + price_text.Text + "'," + status + ");";
                        str += " select LAST_INSERT_ID();";
                        Debug.WriteLine(str);
                        insert_key = GlobalAnnounce.db.GetDataTable(str).Rows[0][0].ToString();
                        upload_fl.SaveAs(path + insert_key + ".pdf");
                    }
                    else
                    {
                        //若不為true，則此label存的是已新增表單的SN
                        //發生於表單已新增，但PDF上傳可能不合法或失敗
                        //重新上傳pdf時，只需update原表單資料
                        insert_key = isFirstUpload_lab.Text; 
                        str = "update hr_application_equipment set equipment = '" + equipment_text.Text + "', content = '" + content_text.Text + "', price = '" + price_text.Text + "' where SN = " + insert_key + ";";
                        Debug.WriteLine(str);
                        GlobalAnnounce.db.InsertDataTable(str);
                        upload_fl.SaveAs(path + insert_key + ".pdf");
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been sent.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(insert_key) + "&type=" + HR_class.encodeBase64("3") + "';", true);
                }
                catch (Exception ee)
                {
                    //upload_lab.Text = ee.Message;
                    upload_lab.Text = "File Upload is failed. send again, please.";
                    upload_lab.Visible = true;
                    if (insert_key != "") { isFirstUpload_lab.Text = insert_key; }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.\nPleace sent again.')", true);
                }
            }
        }

        bool checkInput()
        {
            //資料防呆
            bool flag = true;

            if (equipment_text.Text == "")
            {
                equipment_lab.Text = "Apply Equipment can't be empty.";
                equipment_lab.Visible = true;
                flag = false;
            }
            else { equipment_lab.Visible = false; }

            if (content_text.Text.Trim() == "")
            {
                content_lab.Text = "Content can't be empty.";
                content_lab.Visible = true;
                flag = false;
            }
            else { content_lab.Visible = false; }

            if (price_text.Text.Trim() == "")
            {
                price_lab.Text = "Location can't be empty.";
                price_lab.Visible = true;
                flag = false;
            }
            else { price_lab.Visible = false; }

            return flag;
        }

        bool fileUpload()
        {
            //PDF上傳
            if (upload_fl.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(upload_fl.FileName).ToLower();
                string allowedExtension = ".pdf";
                if (fileExtension != allowedExtension)
                { upload_lab.Text = "Please upload PDF file."; upload_lab.Visible = true; }
                else { upload_lab.Visible = false; return true; }
            }
            return false;
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            string str = "update hr_application_equipment set isDelete = 1 where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been deleted.'); window.location.href='HR_list.aspx?SN=-1&type=3';", true);
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            if (!checkInput()) { return; }

            string path = Server.MapPath("~/HR/PDF_equipment/");
            string sn = HR_class.decodeBase64(Request.QueryString["SN"]);
            try
            {
                if (fileUpload())
                { upload_fl.SaveAs(path + sn + ".pdf"); }
            }
            catch (Exception ee)
            { upload_lab.Text = "Please upload PDF file."; }
            string str = "update hr_application_equipment set equipment = '" + equipment_text.Text + "', content = '" + content_text.Text + "', price = '" + price_text.Text + "' where SN = " + sn + ";";
            Debug.WriteLine(str);
            GlobalAnnounce.db.InsertDataTable(str);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application has been edited.'); window.location.href='HR_list.aspx?SN=" + HR_class.encodeBase64(sn) + "&type=3';", true);
        }

        protected void equipment_text_TextChanged(object sender, EventArgs e)
        {
            if (equipment_text.Text != "") { equipment_lab.Visible = false; }
        }

        protected void content_text_TextChanged(object sender, EventArgs e)
        {
            if (content_text.Text != "") { content_lab.Visible = false; }
        }

        protected void price_text_TextChanged(object sender, EventArgs e)
        {
            if (price_text.Text != "") { price_lab.Visible = false; }
        }

        protected void pass_btn_Click(object sender, EventArgs e)
        {
            string sn = ViewState["check_sn"].ToString();
            int status = int.Parse(ViewState["status"].ToString());
            
            string sql = "update HR_application_equipment ";
            //審核
            switch (status)
            {
                case (int)HR_class.STATUS.Department_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.Administration_Manager + " "; break;
                case (int)HR_class.STATUS.Administration_Manager:
                    sql += "set status=" + (int)HR_class.STATUS.Pass + " "; break;
            }
            sql += "where SN=" + sn + ";";
            
            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is passed.'); window.location.href='HR_check.aspx';", true);
        }

        protected void fail_btn_Click(object sender, EventArgs e)
        {
            //審核不通過
            string sn = ViewState["check_sn"].ToString();
            string sql = "update HR_application_equipment set status = " + (int)HR_class.STATUS.Fail + " where SN=" + sn + ";";

            Debug.WriteLine(sql);
            GlobalAnnounce.db.InsertDataTable(sql);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The application is failed.'); window.location.href='HR_check.aspx';", true);
        }
    }
}