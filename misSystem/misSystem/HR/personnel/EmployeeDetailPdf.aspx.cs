using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using MisSystem_ClassLibrary.HR;
using MisSystem_ClassLibrary.HR.personnel;
using MisSystem_ClassLibrary.sys;

using Newtonsoft.Json;

namespace misSystem.HR.personnel
{
    public partial class EmployeeDetailPdf : System.Web.UI.Page
    {
        private string _EmpJsonData = string.Empty;
        public static EducationDB myEdu = new EducationDB();
        public static ExperienceDB myExp = new ExperienceDB();
        public static SysSetting mySysS = new SysSetting();
        public static SysCodeDB mySysC = new SysCodeDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            _EmpJsonData = Request["json"] == null ? string.Empty : Request["json"].ToString();


            if (!IsPostBack)
            {
                //下載 Pdf
                downloadpdf(_EmpJsonData);
                ////Response.Write("<script language='javascript'>window.close();</script>");
            }
        }

        //下載 Pdf
        private void downloadpdf(string json)
        {
            //設定字型            
            ////string fontPath = System.Environment.GetEnvironmentVariable("windir") + @"\Fonts\KAIU.ttf";            
            ////BaseFont bfc = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            ////iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfc, 12f, iTextSharp.text.Font.NORMAL);            

            string returnMessage = string.Empty;

            ////string r1 = "□";
            ////string r2 = "■";
           
            EmpolyeeDetail EmpDetail = JsonConvert.DeserializeObject<EmpolyeeDetail>(json);

            EmpolyeeDetail empObj = new EmpolyeeDetail();

            empObj.EmpPID = EmpDetail.EmpPID;
            empObj.EmpName = EmpDetail.EmpName;
            empObj.ChiName =EmpDetail.ChiName;
            empObj.EnFName =EmpDetail.EnFName;
            empObj.EnLName = EmpDetail.EnLName;
            empObj.DeptID = EmpDetail.DeptID;
            empObj.Dept = EmpDetail.Dept;
            empObj.EmpNo = EmpDetail.EmpNo;
            empObj.EmpCountryID = EmpDetail.EmpCountryID;
            empObj.EmpCountry = EmpDetail.EmpCountry;
            empObj.Seniority = EmpDetail.Seniority;
            empObj.OnboardDT = EmpDetail.OnboardDT;
            empObj.Status = EmpDetail.Status;
            empObj.PhoneNum = EmpDetail.PhoneNum;
            empObj.Sex = EmpDetail.Sex;
            empObj.Birthday = EmpDetail.Birthday;
            empObj.ImgName = EmpDetail.ImgName;
            empObj.CompanyID = EmpDetail.CompanyID;
            empObj.Company = EmpDetail.Company;
            empObj.Telephone=EmpDetail.Telephone;
            empObj.PerID = EmpDetail.PerID;
            empObj.MarriageYN = EmpDetail.MarriageYN;
            empObj.ChildrenNum = EmpDetail.ChildrenNum;
            empObj.BloodType = EmpDetail.BloodType;
            empObj.Height = EmpDetail.Height;
            empObj.Weight = EmpDetail.Weight;
            empObj.Address = EmpDetail.Address;
            empObj.MailAddress = EmpDetail.MailAddress;
            empObj.ContactPerson = EmpDetail.ContactPerson;
            empObj.ContactNum = EmpDetail.ContactNum;
            empObj.Relationship = EmpDetail.Relationship;
            empObj.ContactAddress = EmpDetail.ContactAddress;
            empObj.ResignationDT = EmpDetail.ResignationDT;

            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyyMMddHHmmss");
            string pdfFileName = empObj.EmpNo + "_" + myDateString + ".pdf";
            //string pdfFullName = Server.MapPath("~") + "/hr/personnel/pdf/" + pdfFileName;
            string pdfFullName ="C:\\BPM\\HR\\pdf\\" + pdfFileName;


            /*
            //設定字型
            string fontPath = System.Environment.GetEnvironmentVariable("windir") + @"\Fonts\KAIU.ttf";
            string fontName = "標楷體";
            FontFactory.Register(fontPath);
            iTextSharp.text.Font fontContentText = FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, 10f);
            */
            //開啟pdf檔案
            Document document = new Document(iTextSharp.text.PageSize.A4, 10f, 10f, 20f, 50f);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFullName, FileMode.Create));

            //writer.ViewerPreferences = PdfWriter.HideMenubar | PdfWriter.HideToolbar;


            //Document doc = new Document(PageSize.A4, 50, 50, 80, 50); // 設定PageSize, Margin, left, right, top, bottom
            //MemoryStream ms = new MemoryStream();
            //PdfWriter pw = PdfWriter.GetInstance(doc, ms);

            ////    字型設定
            // 在PDF檔案內容中要顯示中文，最重要的是字型設定，如果沒有正確設定中文字型，會造成中文無法顯示的問題。
            // 首先設定基本字型：kaiu.ttf 是作業系統系統提供的標楷體字型，IDENTITY_H 是指編碼(The Unicode encoding with horizontal writing)，及是否要將字型嵌入PDF 檔中。
            // 再來針對基本字型做變化，例如Font Size、粗體斜體以及顏色等。當然你也可以採用其他中文字體字型。
            BaseFont bfChinese = BaseFont.CreateFont("C:\\Windows\\Fonts\\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font ChFont = new Font(bfChinese, 12);
            Font ChFont_green = new Font(bfChinese, 40, Font.NORMAL, BaseColor.GREEN);
            Font ChFont_msg = new Font(bfChinese, 12, Font.ITALIC, BaseColor.RED);
            Font ChFont_18f = new Font(bfChinese, 18, Font.NORMAL, BaseColor.BLACK);
            Font ChFont_16f = new Font(bfChinese, 16, Font.NORMAL, BaseColor.BLACK);
            Font ChFont_14f = new Font(bfChinese, 14, Font.NORMAL, BaseColor.BLACK);
            Font ChFont_12f = new Font(bfChinese, 12, Font.NORMAL, BaseColor.BLACK);
            Font ChFont_11f = new Font(bfChinese, 11, Font.NORMAL, BaseColor.BLACK);
            Font ChFont_10f = new Font(bfChinese, 10, Font.NORMAL, BaseColor.BLACK);

            // Setting Document properties e.g.
            // 1. Title
            // 2. Subject
            // 3. Keywords
            // 4. Creator
            // 5. Author
            // 6. Header
            document.AddTitle("人事資料卡");
            document.AddSubject("This is an Example 4 of Chapter 1 of Book 'iText in Action'");
            document.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
            document.AddCreator("BPM");
            document.AddAuthor("Me");
            document.AddHeader("Nothing", "No Header");

            writer.PageEvent = new EmpITextEvents();

            document.Open();
                        
            // 產生表格 -- START
            // 建立4個欄位表格之相對寬度
            PdfPTable pt = new PdfPTable(new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });  //預計切23格
            // 表格總寬
            pt.TotalWidth = 540f;
            pt.LockedWidth = true;
            // 塞入資料 -- START
            // 設定表頭
            PdfPCell R1_header_1 = new PdfPCell(new Phrase(""));
            R1_header_1.Colspan = 5;
            R1_header_1.Rowspan = 4;
            R1_header_1.Border = 0;
            R1_header_1.HorizontalAlignment = Element.ALIGN_CENTER;        // 表頭內文置中
            pt.AddCell(R1_header_1);

            string pic_icon = Server.MapPath("~/picture/icon.png");
            iTextSharp.text.Image icon = iTextSharp.text.Image.GetInstance(pic_icon);
            icon.ScaleToFit(35, 35);
            PdfPCell R1_header_2 = new PdfPCell(icon);
            R1_header_2.Colspan = 4;
            R1_header_2.Rowspan = 4;
            R1_header_2.Padding = 5f;
            R1_header_2.Border = 0;
            R1_header_2.HorizontalAlignment = Element.ALIGN_RIGHT;        
            pt.AddCell(R1_header_2);

            PdfPCell R1_header_3 = new PdfPCell(new Phrase("承賢科技股份有限公司", ChFont_16f));
            R1_header_3.Colspan = 9;
            R1_header_3.Rowspan = 2;
            R1_header_3.Border = 0;
            pt.AddCell(R1_header_3);

            PdfPCell R1_header_5 = new PdfPCell(new Phrase(""));
            R1_header_5.Colspan = 5;
            R1_header_5.Rowspan = 4;
            R1_header_5.Border = 0;
            pt.AddCell(R1_header_5);

            PdfPCell R2_header_4 = new PdfPCell(new Phrase("LIGHTMED CORPORATION", ChFont_16f));
            R2_header_4.Colspan = 9;
            R2_header_4.Rowspan = 2;
            R2_header_4.Border = 0;
            pt.AddCell(R2_header_4);

            PdfPCell R3_header_1 = new PdfPCell(new Phrase("   ", ChFont_12f));
            R3_header_1.Colspan = 23;
            R3_header_1.Border = 0;
            pt.AddCell(R3_header_1);

            //======================================================================================

            PdfPCell R1_EmpName_L = new PdfPCell(new Phrase("姓名", ChFont_12f));
            R1_EmpName_L.Colspan = 3;
            R1_EmpName_L.MinimumHeight = 20F;//设置表格的高度
            R1_EmpName_L.HorizontalAlignment = Element.ALIGN_CENTER;        // 表頭內文置中
            pt.AddCell(R1_EmpName_L);

            string ls_EmpName = empObj.EmpName;
            PdfPCell R1_EmpName_D = new PdfPCell(new Phrase(ls_EmpName, ChFont_12f));
            R1_EmpName_D.Colspan = 3;
            R1_EmpName_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R1_EmpName_D);

            PdfPCell R1_Birthday_L = new PdfPCell(new Phrase("出生年月日", ChFont_12f));
            R1_Birthday_L.Colspan = 3;
            R1_Birthday_L.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            pt.AddCell(R1_Birthday_L);

            string ls_Birthday = empObj.Birthday;
            PdfPCell R1_Birthday_D = new PdfPCell(new Phrase(ls_Birthday, ChFont_12f));
            R1_Birthday_D.Colspan = 3;
            R1_Birthday_D.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            pt.AddCell(R1_Birthday_D);

            //string ls_EmpNo = empObj.EmpNo.ToString();
            //PdfPCell R2_EmpNo_D = new PdfPCell(new Phrase(ls_EmpNo, ChFont_12f));
            //R2_EmpNo_D.Colspan = 2;
            //R2_EmpNo_D.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            //pt.AddCell(R2_EmpNo_D);

            PdfPCell R1_Country_L = new PdfPCell(new Phrase("籍貫", ChFont_12f));
            R1_Country_L.Colspan = 3;
            R1_Country_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R1_Country_L);

            string ls_Country = string.Empty;
            if (!string.IsNullOrEmpty(empObj.EmpCountryID))
            {
                ls_Country = empObj.EmpCountry;
            }
            PdfPCell R1_Country_D = new PdfPCell(new Phrase(ls_Country, ChFont_12f));
            R1_Country_D.Colspan = 4;
            R1_Country_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R1_Country_D);

            PdfPCell R1_Img;
            if (!string.IsNullOrEmpty(empObj.ImgName))
            {
                DataTable dt_sysS = mySysS.search_SysSettingCode("PER", "AbsolutePath");
                string path = dt_sysS.Rows[0]["SettingSrting"].ToString(); ;
                string ls_Img = path + "/EmpImg/" + empObj.ImgName;
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(ls_Img);
                jpg.ScaleToFit(98, 98);
                R1_Img = new PdfPCell(jpg);
            }
            else { R1_Img = new PdfPCell(); }
            R1_Img.Colspan = 4;
            R1_Img.Rowspan = 5;
            R1_Img.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R1_Img);

            //======================================================================================

            PdfPCell R2_Dept_L = new PdfPCell(new Phrase("單位", ChFont_12f));
            R2_Dept_L.Colspan = 3;
            R2_Dept_L.MinimumHeight = 20F;//设置表格的高度
            R2_Dept_L.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_Dept_L);

            string ls_DeptName = empObj.Dept;
            PdfPCell R2_DeptName_D = new PdfPCell(new Phrase(ls_DeptName, ChFont_12f));
            R2_DeptName_D.Colspan = 4;
            R2_DeptName_D.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_DeptName_D);

            PdfPCell R2_PerID_L = new PdfPCell(new Phrase("身分證字號", ChFont_12f));
            R2_PerID_L.Colspan = 3;
            R2_PerID_L.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_PerID_L);

            string ls_PerID = empObj.PerID;
            PdfPCell R2_PerID_D = new PdfPCell(new Phrase(ls_PerID, ChFont_12f));
            R2_PerID_D.Colspan = 5;
            R2_PerID_D.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_PerID_D);

            PdfPCell R2_Sex_L = new PdfPCell(new Phrase("性別", ChFont_12f));
            R2_Sex_L.Colspan = 2;
            R2_Sex_L.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_Sex_L);

            string ls_Sex = empObj.Sex;
            PdfPCell R2_Sex_D = new PdfPCell(new Phrase(ls_Sex, ChFont_12f));
            R2_Sex_D.Colspan = 2;
            R2_Sex_D.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_Sex_D);

            //======================================================================================

            PdfPCell R3_EmpNo_L = new PdfPCell(new Phrase("工號", ChFont_12f));
            R3_EmpNo_L.Colspan = 3;
            R3_EmpNo_L.MinimumHeight = 20F;
            R3_EmpNo_L.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EmpNo_L);

            string ls_EmpNo = empObj.EmpNo.ToString();
            PdfPCell R3_EmpNo_D = new PdfPCell(new Phrase(ls_EmpNo, ChFont_12f));
            R3_EmpNo_D.Colspan = 3;
            R3_EmpNo_D.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EmpNo_D);

            PdfPCell R3_inD_L = new PdfPCell(new Phrase("到職日期", ChFont_12f));
            R3_inD_L.Colspan = 3;
            R3_inD_L.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            pt.AddCell(R3_inD_L);

            string ls_inD = empObj.OnboardDT;
            PdfPCell R3_inD_D = new PdfPCell(new Phrase(ls_inD, ChFont_12f));
            R3_inD_D.Colspan = 3;
            R3_inD_D.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            pt.AddCell(R3_inD_D);

            PdfPCell R3_LeaveD_L = new PdfPCell(new Phrase("離職日期", ChFont_12f));
            R3_LeaveD_L.Colspan = 3;
            R3_LeaveD_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R3_LeaveD_L);

            string ls_outD;
            if (!string.IsNullOrEmpty(empObj.ResignationDT))
            {
                ls_outD = empObj.ResignationDT;
            }
            else
            { ls_outD = string.Empty; }
            PdfPCell R3_LeaveD_D = new PdfPCell(new Phrase(ls_outD, ChFont_12f));
            R3_LeaveD_D.Colspan = 4;
            R3_LeaveD_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R3_LeaveD_D);

            //======================================================================================

            PdfPCell R4_MailAddress_L = new PdfPCell(new Phrase("通訊地址", ChFont_12f));
            R4_MailAddress_L.Colspan = 3;
            R4_MailAddress_L.MinimumHeight = 20F;
            R4_MailAddress_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R4_MailAddress_L);

            string ls_MailAddress = empObj.MailAddress;
            PdfPCell R4_MailAddress_D = new PdfPCell(new Phrase(ls_MailAddress, ChFont_12f));
            R4_MailAddress_D.Colspan = 9;
            R4_MailAddress_D.PaddingLeft = 8f;
            //R4_MailAddress_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R4_MailAddress_D);

            PdfPCell R4_PhoneNum_L = new PdfPCell(new Phrase("手機號碼", ChFont_12f));
            R4_PhoneNum_L.Colspan = 3;
            R4_PhoneNum_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R4_PhoneNum_L);

            string ls_PhoneN = string.Empty;
            if (empObj.PhoneNum != "--")
            {
                ls_PhoneN = empObj.PhoneNum;
            }     
            PdfPCell R4_PhoneNum_D = new PdfPCell(new Phrase(ls_PhoneN, ChFont_12f));
            R4_PhoneNum_D.Colspan = 4;
            R4_PhoneNum_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R4_PhoneNum_D);

            //======================================================================================

            PdfPCell R5_Address_L = new PdfPCell(new Phrase("戶籍地址", ChFont_12f));
            R5_Address_L.Colspan = 3;
            R5_Address_L.MinimumHeight = 20F;
            R5_Address_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R5_Address_L);

            string ls_Address = empObj.Address;
            PdfPCell R5_Address_D = new PdfPCell(new Phrase(ls_Address, ChFont_12f));
            R5_Address_D.Colspan = 9;
            R5_Address_D.PaddingLeft = 8f;
            //R5_Address_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R5_Address_D);

            PdfPCell R5_PhoneNum_L = new PdfPCell(new Phrase("市話號碼", ChFont_12f));
            R5_PhoneNum_L.Colspan = 3;
            R5_PhoneNum_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R5_PhoneNum_L);

            string ls_TelePhone = string.Empty;
            if (empObj.Telephone != "--")
            {
                ls_TelePhone = empObj.Telephone;
            }     
            PdfPCell R5_PhoneNum_D = new PdfPCell(new Phrase(ls_TelePhone, ChFont_12f));
            R5_PhoneNum_D.Colspan = 4;
            R5_PhoneNum_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R5_PhoneNum_D);

            //======================================================================================

            PdfPCell R6_Blood_L = new PdfPCell(new Phrase("血型", ChFont_12f));
            R6_Blood_L.Colspan = 2;
            R6_Blood_L.MinimumHeight = 20F;
            R6_Blood_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Blood_L);

            string ls_Blood = empObj.BloodType;
            PdfPCell R6_Blood_D = new PdfPCell(new Phrase(ls_Blood, ChFont_12f));
            R6_Blood_D.Colspan = 2;
            R6_Blood_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Blood_D);

            PdfPCell R6_Height_L = new PdfPCell(new Phrase("身高", ChFont_12f));
            R6_Height_L.Colspan = 2;
            R6_Height_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Height_L);

            string ls_Height = empObj.Height;
            if (ls_Height.Contains("0") == true)
            {
                ls_Height = string.Empty;
            }
            PdfPCell R6_Height_D = new PdfPCell(new Phrase(ls_Height, ChFont_12f));
            R6_Height_D.Colspan = 2;
            R6_Height_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Height_D);

            PdfPCell R6_Weight_L = new PdfPCell(new Phrase("體重", ChFont_12f));
            R6_Weight_L.Colspan = 2;
            R6_Weight_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Weight_L);

            string ls_Weight = empObj.Weight;
            if (ls_Weight.Contains("0") == true)
            {
                ls_Weight = string.Empty;
            }
            PdfPCell R6_Weight_D = new PdfPCell(new Phrase(ls_Weight, ChFont_12f));
            R6_Weight_D.Colspan = 2;
            R6_Weight_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Weight_D);

            PdfPCell R6_Marriage_L = new PdfPCell(new Phrase("婚姻狀況", ChFont_12f));
            R6_Marriage_L.Colspan = 3;
            R6_Marriage_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Marriage_L);

            #region imgPdfPTable

            PdfPTable imgpt = new PdfPTable(new float[] { 1, 2, 1, 2 });  //預計切4格，1、2為寬度比例


            string pic_tick = Server.MapPath("~/picture/checked.png");
            iTextSharp.text.Image tick = iTextSharp.text.Image.GetInstance(pic_tick);
            tick.ScaleToFit(12, 12);
            string pic_frame = Server.MapPath("~/picture/box.png");
            iTextSharp.text.Image frame = iTextSharp.text.Image.GetInstance(pic_frame);
            frame.ScaleToFit(12, 12);
                       
            //打勾的框
            PdfPCell R6_Marriage_T = new PdfPCell(tick,true);
            R6_Marriage_T.Colspan = 1;
            R6_Marriage_T.Border = 0;
            R6_Marriage_T.PaddingTop = 2f;
            R6_Marriage_T.HorizontalAlignment = Element.ALIGN_RIGHT;
            //沒打勾的框
            PdfPCell R6_Marriage_F = new PdfPCell(frame, true);
            R6_Marriage_F.Colspan = 1;
            R6_Marriage_F.Border = 0;
            R6_Marriage_F.PaddingTop = 2f;
            R6_Marriage_F.HorizontalAlignment = Element.ALIGN_RIGHT;


            if (empObj.MarriageYN == "Y")
            {
                imgpt.AddCell(R6_Marriage_F);
            }
            else
            {
                imgpt.AddCell(R6_Marriage_T);
            }

            string ls_MarriageN = "未婚";//"□未婚  □已婚"
            PdfPCell R6_Marriage_N = new PdfPCell(new Phrase(ls_MarriageN, ChFont_12f));
            R6_Marriage_N.Colspan = 1;
            R6_Marriage_N.Border = 0;
            R6_Marriage_N.HorizontalAlignment = Element.ALIGN_LEFT;
            imgpt.AddCell(R6_Marriage_N);
            
            if (empObj.MarriageYN == "Y")
            { 
                imgpt.AddCell(R6_Marriage_T);
            }
            else
            {
                imgpt.AddCell(R6_Marriage_F);
            }
           
            string ls_MarriageY = "已婚";//"□未婚  □已婚"
            PdfPCell R6_Marriage_Y = new PdfPCell(new Phrase(ls_MarriageY, ChFont_12f));
            R6_Marriage_Y.Colspan = 1;
            R6_Marriage_Y.Border = 0;
            R6_Marriage_Y.HorizontalAlignment = Element.ALIGN_LEFT;
            imgpt.AddCell(R6_Marriage_Y);
            #endregion

            PdfPCell R6_Marriage_D = new PdfPCell(imgpt);
            R6_Marriage_D.Colspan = 4;
            R6_Marriage_D.Padding = 2f;
            //R6_Marriage_Y.HorizontalAlignment = Element.ALIGN_LEFT;
            pt.AddCell(R6_Marriage_D);

            PdfPCell R6_Dependents_L = new PdfPCell(new Phrase("兒女人數", ChFont_12f));
            R6_Dependents_L.Colspan = 3;
            R6_Dependents_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Dependents_L);

            string ls_Children = empObj.ChildrenNum.ToString();
            PdfPCell R6_Dependents_D = new PdfPCell(new Phrase(ls_Children, ChFont_12f));
            R6_Dependents_D.Colspan = 1;
            R6_Dependents_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R6_Dependents_D);

            //======================================================================================

            PdfPCell R7_ContactPer_L = new PdfPCell(new Phrase("連絡人", ChFont_12f));
            R7_ContactPer_L.Colspan = 3;
            R7_ContactPer_L.MinimumHeight = 20F;
            R7_ContactPer_L.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R7_ContactPer_L);

            string ls_ContactPer = empObj.ContactPerson;
            PdfPCell R7_ContactPer_D = new PdfPCell(new Phrase(ls_ContactPer, ChFont_12f));
            R7_ContactPer_D.Colspan = 3;
            R7_ContactPer_D.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R7_ContactPer_D);

            PdfPCell R7_Relationship_L = new PdfPCell(new Phrase("關係", ChFont_12f));
            R7_Relationship_L.Colspan = 3;
            R7_Relationship_L.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            pt.AddCell(R7_Relationship_L);

            string ls_Relationship = empObj.Relationship;
            PdfPCell R7_Relationship_D = new PdfPCell(new Phrase(ls_Relationship, ChFont_12f));
            R7_Relationship_D.Colspan = 3;
            R7_Relationship_D.HorizontalAlignment = Element.ALIGN_CENTER;    // 表頭內文置中
            pt.AddCell(R7_Relationship_D);

            PdfPCell R7_ContactPerPhone_L = new PdfPCell(new Phrase("電話號碼", ChFont_12f));
            R7_ContactPerPhone_L.Colspan = 3;
            R7_ContactPerPhone_L.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R7_ContactPerPhone_L);

            string ls_ContactNum = string.Empty;
            if (empObj.ContactNum != "--")
            {
                ls_ContactNum = empObj.ContactNum;
            }            
            PdfPCell R7_ContactPerPhone_D = new PdfPCell(new Phrase(ls_ContactNum, ChFont_12f));
            R7_ContactPerPhone_D.Colspan = 8;
            R7_ContactPerPhone_D.HorizontalAlignment = Element.ALIGN_CENTER;  // 表頭內文置中
            pt.AddCell(R7_ContactPerPhone_D);

            //======================================================================================

            PdfPCell R8_ContactPerAdd_L = new PdfPCell(new Phrase("地址", ChFont_12f));
            R8_ContactPerAdd_L.Colspan = 3;
            R8_ContactPerAdd_L.MinimumHeight = 20F;
            R8_ContactPerAdd_L.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R8_ContactPerAdd_L);

            string ls_ContactAddress = empObj.ContactAddress;
            PdfPCell R8_ContactPerAdd_D = new PdfPCell(new Phrase(ls_ContactAddress, ChFont_12f));
            R8_ContactPerAdd_D.Colspan = 20;
            R8_ContactPerAdd_D.PaddingLeft = 8f;
            //R8_ContactPerAdd_D.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R8_ContactPerAdd_D);

            //======================================================================================

            // 塞入資料 -- END
            document.Add(pt);

            //======================================================================================
            //======================================================================================
            //======================================================================================

            //因為格式不一致，所以需要再重新定義
            pt = new PdfPTable(new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });  //預計切23格
            // 表格總寬
            pt.TotalWidth = 540f;
            pt.LockedWidth = true;

            //======================================================================================

            PdfPCell R1_Edu = new PdfPCell(new Phrase("    ", ChFont_12f));
            R1_Edu.Colspan = 23;
            R1_Edu.Border = 0;
            pt.AddCell(R1_Edu);

            //======================================================================================

            PdfPCell R2_Edu = new PdfPCell(new Phrase("學   歷", ChFont_14f));
            R2_Edu.Colspan = 23;
            R2_Edu.MinimumHeight = 20F;
            R2_Edu.Border = 0;
            R2_Edu.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_Edu);

            //======================================================================================

            PdfPCell R3_EduNo = new PdfPCell(new Phrase("序號", ChFont_12f));
            R3_EduNo.Colspan = 2;
            R3_EduNo.MinimumHeight = 20F;
            R3_EduNo.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EduNo);

            PdfPCell R3_EduStartD = new PdfPCell(new Phrase("開始時間", ChFont_12f));
            R3_EduStartD.Colspan = 3;
            R3_EduStartD.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EduStartD);

            PdfPCell R3_EduEndD = new PdfPCell(new Phrase("結束時間", ChFont_12f));
            R3_EduEndD.Colspan = 3;
            R3_EduEndD.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EduEndD);

            PdfPCell R3_EduName = new PdfPCell(new Phrase("學校名稱", ChFont_12f));
            R3_EduName.Colspan = 7;
            R3_EduName.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EduName);

            PdfPCell R3_EduMajor = new PdfPCell(new Phrase("科系", ChFont_12f));
            R3_EduMajor.Colspan = 5;
            R3_EduMajor.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EduMajor);

            PdfPCell R3_EduDegree = new PdfPCell(new Phrase("學位", ChFont_12f));
            R3_EduDegree.Colspan = 3;
            R3_EduDegree.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_EduDegree);

            //======================================================================================

            DataTable dt_EduList = myEdu.search_EmpEduDetail(empObj.EmpPID.ToString());

            PdfPCell R_C_D = new PdfPCell();
            R_C_D.VerticalAlignment = Element.ALIGN_MIDDLE;

            if (dt_EduList != null && dt_EduList.Rows.Count > 0)
            {
                for (int n = 0; n < dt_EduList.Rows.Count; n++)
                {
                    string ls_EduNo = (n + 1).ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_EduNo, ChFont_12f));
                    R_C_D.Colspan = 2;
                    R_C_D.MinimumHeight = 20F;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_EduStartD = dt_EduList.Rows[n]["StartDT"]==null ? string.Empty : Convert.ToDateTime(dt_EduList.Rows[n]["StartDT"].ToString()).ToString("yyyy/MM");
                    R_C_D = new PdfPCell(new Phrase(ls_EduStartD, ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_EduEndD = dt_EduList.Rows[n]["EndDT"]==null ? string.Empty : Convert.ToDateTime(dt_EduList.Rows[n]["EndDT"].ToString()).ToString("yyyy/MM"); 
                    R_C_D = new PdfPCell(new Phrase(ls_EduEndD, ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    //string ls_EduSchool = dt_EduList.Rows[n]["School"].ToString();
                    //DataTable dt_School = mySysC.search_EDUSCHOOL(ls_EduSchoolID);
                    string ls_EduName = dt_EduList.Rows[n]["School"].ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_EduName, ChFont_12f));
                    R_C_D.Colspan = 7;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    //string ls_EduMajorID = dt_EduList.Rows[n]["MajorID"].ToString();
                    //DataTable dt_Major = mySysC.search_EDUMAJORSUBJECT(ls_EduMajorID);
                    string ls_EduMajor = dt_EduList.Rows[n]["Major"].ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_EduMajor, ChFont_12f));
                    R_C_D.Colspan = 5;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_EduDegreeID = dt_EduList.Rows[n]["DegreeID"].ToString();
                    DataTable dt_Degree = mySysC.search_EDUDEGREE(ls_EduDegreeID);
                    string ls_EduDegree = dt_Degree.Rows[0]["CodeName"].ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_EduDegree, ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);
                }
            }

            if (dt_EduList.Rows.Count < 5)
            {
                for (int i = dt_EduList.Rows.Count; i < 5; i++)
                {
                    string ls_EduNo = (i + 1).ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_EduNo, ChFont_12f));
                    R_C_D.Colspan = 2;
                    R_C_D.MinimumHeight = 20F;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 7;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 5;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);
                }
            }

            //======================================================================================

            PdfPCell R1_Exp = new PdfPCell(new Phrase("    ", ChFont_12f));
            R1_Exp.Colspan = 23;
            R1_Exp.Border = 0;
            pt.AddCell(R1_Exp);

            //======================================================================================

            PdfPCell R2_Exp = new PdfPCell(new Phrase("經   歷", ChFont_14f));
            R2_Exp.Colspan = 23;
            R2_Exp.MinimumHeight = 20F;
            R2_Exp.Border = 0;
            R2_Exp.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R2_Exp);
            
            //======================================================================================

            PdfPCell R3_ExpNo = new PdfPCell(new Phrase("序號", ChFont_12f));
            R3_ExpNo.Colspan = 2;
            R3_ExpNo.MinimumHeight = 20F;
            R3_ExpNo.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_ExpNo);

            PdfPCell R3_ExpStartD = new PdfPCell(new Phrase("開始時間", ChFont_12f));
            R3_ExpStartD.Colspan = 3;
            R3_ExpStartD.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_ExpStartD);

            PdfPCell R3_ExpEndD = new PdfPCell(new Phrase("結束時間", ChFont_12f));
            R3_ExpEndD.Colspan = 3;
            R3_ExpEndD.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_ExpEndD);

            PdfPCell R3_ExpName = new PdfPCell(new Phrase("公司名稱", ChFont_12f));
            R3_ExpName.Colspan = 7;
            R3_ExpName.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_ExpName);

            PdfPCell R3_ExpPosition = new PdfPCell(new Phrase("職稱", ChFont_12f));
            R3_ExpPosition.Colspan = 3;
            R3_ExpPosition.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_ExpPosition);

            PdfPCell R3_ExpLeavedRsn = new PdfPCell(new Phrase("離職原因", ChFont_12f));
            R3_ExpLeavedRsn.Colspan = 5;
            R3_ExpLeavedRsn.HorizontalAlignment = Element.ALIGN_CENTER;
            pt.AddCell(R3_ExpLeavedRsn);

            //======================================================================================

            DataTable dt_ExpList = myExp.search_EmpExpDetail(empObj.EmpPID.ToString());

            //PdfPCell R_C_D = new PdfPCell();
            //R_C_D.VerticalAlignment = Element.ALIGN_MIDDLE;

            if (dt_ExpList != null && dt_ExpList.Rows.Count > 0)
            {
                for (int n = 0; n < dt_ExpList.Rows.Count; n++)
                {
                    string ls_ExpNo = (n + 1).ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_ExpNo, ChFont_12f));
                    R_C_D.Colspan = 2;
                    R_C_D.MinimumHeight = 20F;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_ExpStartD = dt_ExpList.Rows[n]["StartDT"] == null ? string.Empty : Convert.ToDateTime(dt_ExpList.Rows[n]["StartDT"].ToString()).ToString("yyyy/MM");
                    R_C_D = new PdfPCell(new Phrase(ls_ExpStartD, ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_ExpEndD = dt_ExpList.Rows[n]["EndDT"] == null ? string.Empty : Convert.ToDateTime(dt_ExpList.Rows[n]["EndDT"].ToString()).ToString("yyyy/MM"); 
                    R_C_D = new PdfPCell(new Phrase(ls_ExpEndD, ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_ExpCompanyN = dt_ExpList.Rows[n]["CompanyName"].ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_ExpCompanyN, ChFont_12f));
                    R_C_D.Colspan = 7;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_ExpPosition = dt_ExpList.Rows[n]["Position"].ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_ExpPosition, ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    string ls_ExpLeavedRsnID = dt_ExpList.Rows[n]["LeavedRsnID"].ToString();
                    DataTable dt_leaveRsn = mySysC.search_RESIGNREASON(ls_ExpLeavedRsnID);
                    string ls_ExpLeavedRsn = dt_leaveRsn.Rows[0]["CodeName"].ToString();
                    if (ls_ExpLeavedRsn == "其他")
                    {
                        ls_ExpLeavedRsn += "(" + dt_ExpList.Rows[n]["LeavedOtherRsn"].ToString() + ")";
                    }
                    R_C_D = new PdfPCell(new Phrase(ls_ExpLeavedRsn, ChFont_12f));
                    R_C_D.Colspan = 5;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);
                }
            }

            if (dt_ExpList.Rows.Count < 5)
            {
                for (int i = dt_ExpList.Rows.Count; i < 5; i++)
                {
                    string ls_ExpNo = (i + 1).ToString();
                    R_C_D = new PdfPCell(new Phrase(ls_ExpNo, ChFont_12f));
                    R_C_D.Colspan = 2;
                    R_C_D.MinimumHeight = 20F;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 7;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 3;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);

                    R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                    R_C_D.Colspan = 5;
                    R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                    pt.AddCell(R_C_D);
                }
            }

            //======================================================================================
            
            document.Add(pt);

            //======================================================================================
            //換頁
            document.NewPage();

            PdfPTable pt2 = new PdfPTable(new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });  //預計切23格

            // 表格總寬
            pt2.TotalWidth = 540f;
            pt2.LockedWidth = true;

            //======================================================================================

            PdfPCell R1_header = new PdfPCell(new Phrase("    ", ChFont_14f));
            R1_header.Colspan = 23;
            R1_header.Border = 0;
            R1_header.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R1_header);

            //======================================================================================

            PdfPCell R2_header = new PdfPCell(new Phrase("    ", ChFont_14f));
            R2_header.Colspan = 23;
            R2_header.Border = 0;
            R2_header.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R2_header);

            //======================================================================================

            PdfPCell R3_header = new PdfPCell(new Phrase("    ", ChFont_12f));
            R3_header.Colspan = 23;
            R3_header.Border = 0;
            R3_header.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R3_header);

            //======================================================================================

            PdfPCell R1_Repu = new PdfPCell(new Phrase("獎懲紀錄", ChFont_14f));
            R1_Repu.Colspan = 23;
            R1_Repu.MinimumHeight = 20F;
            R1_Repu.Border = 0;
            R1_Repu.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R1_Repu);

            //======================================================================================

            PdfPCell R2_RepuNo = new PdfPCell(new Phrase("序號", ChFont_12f));
            R2_RepuNo.Colspan = 2;
            R2_RepuNo.MinimumHeight = 20F;
            R2_RepuNo.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R2_RepuNo);

            PdfPCell R2_RepuD = new PdfPCell(new Phrase("生效日期", ChFont_12f));
            R2_RepuD.Colspan = 3;
            R2_RepuD.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R2_RepuD);

            PdfPCell R2_RepuContents = new PdfPCell(new Phrase("獎懲事實摘要", ChFont_12f));
            R2_RepuContents.Colspan = 9;
            R2_RepuContents.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R2_RepuContents);

            PdfPCell R2_RepuType = new PdfPCell(new Phrase("獎懲結果", ChFont_12f));
            R2_RepuType.Colspan = 5;
            R2_RepuType.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R2_RepuType);

            PdfPCell R2_RepuSign = new PdfPCell(new Phrase("公文號", ChFont_12f));
            R2_RepuSign.Colspan = 4;
            R2_RepuSign.HorizontalAlignment = Element.ALIGN_CENTER;
            pt2.AddCell(R2_RepuSign);

            //======================================================================================

            for (int i = 0; i < 15; i++)
            {
                string ls_No = (i + 1).ToString();
                R_C_D = new PdfPCell(new Phrase(ls_No, ChFont_12f));
                R_C_D.Colspan = 2;
                R_C_D.MinimumHeight = 20F;
                R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                pt2.AddCell(R_C_D);

                R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                R_C_D.Colspan = 3;
                R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                pt2.AddCell(R_C_D);

                R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                R_C_D.Colspan = 9;
                R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                pt2.AddCell(R_C_D);

                R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                R_C_D.Colspan = 5;
                R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                pt2.AddCell(R_C_D);

                R_C_D = new PdfPCell(new Phrase("", ChFont_12f));
                R_C_D.Colspan = 4;
                R_C_D.HorizontalAlignment = Element.ALIGN_CENTER;
                pt2.AddCell(R_C_D);
            }

            //======================================================================================

            document.Add(pt2);

            //======================================================================================

            //關閉pdf檔
            document.Close();
            writer.Close();

            //string url = Server.MapPath("~") + "~/hr/personnel/pdf/" + pdfFileName;
            Response.Redirect("http://192.168.168.47:8090/HR/pdf/" + pdfFileName);
           
        }
    }

    public class EmpITextEvents : PdfPageEventHelper
    {

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate, footerTemplateText;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;


        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
                footerTemplateText = cb.CreateTemplate(50, 100);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);

            BaseFont bfChinese = BaseFont.CreateFont("C:\\Windows\\Fonts\\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font ChFont_9f = new Font(bfChinese, 9);

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            //////PdfPCell pdfCell1 = new PdfPCell();
            //////PdfPCell pdfCell2 = new PdfPCell(p1Header);
            //////PdfPCell pdfCell3 = new PdfPCell();

            String text = "Page " + writer.PageNumber + " of ";


            //Add paging to header
            {
                //////cb.BeginText();
                //////cb.SetFontAndSize(bf, 12);
                //////cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
                //////cb.ShowText(text);
                //////cb.EndText();
                //////float len = bf.GetWidthPoint(text, 12);
                //Adds "12" in Page 1 of 12
                //////cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
            }
            //Add paging to footer
            {
                //cb.BeginText();
                //cb.SetFontAndSize(bf, 9);
                //cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(20));
                //cb.ShowText(text);
                //cb.EndText();
                //float len = bf.GetWidthPoint(text, 9);
                //cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(20));

                //string txt = "簽核程序:申請人→單位/部門主管→行管部歸檔 (每日下午5點前提交)。";
                //cb.BeginText();
                //cb.SetFontAndSize(bfChinese, 9);
                //cb.SetTextMatrix(document.PageSize.GetLeft(20), document.PageSize.GetBottom(20));
                //cb.ShowText(txt);
                //cb.EndText();
                //float len2 = bf.GetWidthPoint(txt, 9);
                //cb.AddTemplate(footerTemplateText, document.PageSize.GetLeft(20) + len2, document.PageSize.GetBottom(20));
            }
            //Row 2
            //////PdfPCell pdfCell4 = new PdfPCell(new Phrase("Sub Header Description", baseFontNormal));

            //Row 3
            /*
            //////PdfPCell pdfCell5 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), baseFontBig));
            //////PdfPCell pdfCell6 = new PdfPCell();
            //////PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));
            */

            //set the alignment of all three cells and set border to 0
            //////pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            //////pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            //////pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            //////pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            //////pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            //////pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            //////pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;


            //////pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
            //////pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            //////pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
            ////pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            ////pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;


            //////pdfCell4.Colspan = 3;



            //////pdfCell1.Border = 0;
            //////pdfCell2.Border = 0;
            //////pdfCell3.Border = 0;
            //////pdfCell4.Border = 0;
            //////pdfCell5.Border = 0;
            //////pdfCell6.Border = 0;
            //////pdfCell7.Border = 0;


            //add all three cells into PdfTable
            //////pdfTab.AddCell(pdfCell1);
            //////pdfTab.AddCell(pdfCell2);
            //////pdfTab.AddCell(pdfCell3);
            //////pdfTab.AddCell(pdfCell4);
            //////pdfTab.AddCell(pdfCell5);
            //////pdfTab.AddCell(pdfCell6);
            //////pdfTab.AddCell(pdfCell7);

            pdfTab.TotalWidth = document.PageSize.Width - 20f;
            pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;


            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            //////cb.MoveTo(40, document.PageSize.Height - 100);
            //////cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            //////cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(20, document.PageSize.GetBottom(35));
            cb.LineTo(document.PageSize.Width - 20, document.PageSize.GetBottom(35));
            //cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            //////headerTemplate.BeginText();
            //////headerTemplate.SetFontAndSize(bf, 11);
            //////headerTemplate.SetTextMatrix(0, 0);
            //////headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            //////headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 9);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();

        }
    }
}