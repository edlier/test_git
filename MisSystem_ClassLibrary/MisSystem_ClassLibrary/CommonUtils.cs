using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;


namespace MisSystem_ClassLibrary
{
    public class CommonUtils
    {
       
        public enum SelectValueType
        {
            listValue = 1,
            listText = 2,
        }

        public static string _psDateFormat = "yyyy/MM/dd";
        public static string _psDateTimeFormat = "yyyy/MM/dd hh:mm:ss";        
        public static string _psTimeFormat = "hh:mm:ss";
        public static int _piUploadFileSize = 2097152; // 2mb

        /// <summary>
        /// 產生Client端刪除詢問JavaScript
        /// </summary>
        /// <param name="clientFunctionName">Fucntion名稱</param>
        /// <param name="checkObjectName">檢查內容物件名稱</param>
        /// <param name="alertMsg">提示訊息：例：必須選擇刪除項目</param>
        /// <returns>JavaScript字串</returns>
        public StringBuilder getClientDelConfirm(string clientFunctionName, string checkObjectName, string alertMsg)
        {
            StringBuilder JS = new StringBuilder();
            JS.Append("function " + clientFunctionName);
            JS.Append(" { ");
            JS.Append("     var lsChkData = document.getElementById('" + checkObjectName + "').value; ");
            JS.Append("     if (lsChkData == '')  ");
            JS.Append("     { ");
            JS.Append("         alert(\"" + alertMsg + "\"); ");
            //JS.Append("         document.getElementById('" + checkObjectName + "').focus(); ");
            JS.Append("         return false; ");
            JS.Append("     } ");
            JS.Append("     return confirm('" + "確認刪除 [" + "' + lsChkData + '" + "] ?!'); ");
            JS.Append(" } ");

            return JS;
        }

        /// <summary>
        /// 傳入Y或N,回傳 是/否 
        /// </summary>
        /// <param name="fsData">傳入值(Y/N)</param>
        /// <returns>Return Yes/No</returns>
        public string getYesNoText(string fsData)
        {            

            string lsVal = "";

            switch (fsData.ToUpper())
            {
                case "Y":
                    lsVal = "Yes";
                    break;
                case "N":
                    lsVal = "No";
                    break;
                default:
                    lsVal = fsData;
                    break;
            }
            return lsVal;
        }

        public string getBoolToYN(bool b)
        {
            string lsVal = string.Empty;
            switch (b)
            {
                case true:
                    lsVal = "Y";
                    break;
                case false:
                    lsVal = "N";
                    break;
                default:
                    lsVal = "";
                    break;
            }
            return lsVal;
        }

        public bool getYNToBool(string YN)
        {
            bool lbVal = false;
            switch (YN)
            {
                case "Y":
                    lbVal = true;
                    break;
                case "N":
                    lbVal = false;
                    break;
                default:
                    lbVal = false;
                    break;
            }
            return lbVal;
        }

        public bool getYNToBool(Object lsData)
        {
            bool lbVal = false;
            if (lsData == System.DBNull.Value)
            {
                lbVal = false;
            }
            else
            {
                string YN = lsData.ToString();
                switch (YN)
                {
                    case "Y":
                        lbVal = true;
                        break;
                    case "N":
                        lbVal = false;
                        break;
                    default:
                        lbVal = false;
                        break;
                }
            }
            return lbVal;
        }
        

        /// <summary>
        /// 傳入性別F/M 
        /// </summary>
        /// <param name="fsData">傳入值(F/M)</param>
        /// <returns>回傳 男/女</returns>
        public string getCustSexText(string fsData)
        {            

            string lsVal = "";

            switch (fsData.ToUpper())
            {
                case "M":
                    lsVal = "Man";
                    break;
                case "F":
                    lsVal = "Female";
                    break;
                default:
                    lsVal = fsData;
                    break;

            }
            return lsVal;
        }

        /// <summary>
        /// 對GridView抬頭增加排序的圖案(SqlDataSource)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="GridView1"></param>
        public void AddSortIcon(GridViewRowEventArgs e, GridView GridView1)
        {
            foreach (TableCell MyHeader in e.Row.Cells) //對每一格      
            {
                if (MyHeader.HasControls())
                {
                    if (MyHeader.Controls[0] is LinkButton)//避免GridView中的CheckBox全選的錯誤
                    {
                        if (((LinkButton)MyHeader.Controls[0]).CommandArgument == GridView1.SortExpression)//是否為為排序欄位
                        {
                            if (GridView1.SortDirection == SortDirection.Ascending) //依排序方向加入箭號
                            {
                                MyHeader.Controls.Add(new LiteralControl("▽"));//可以換圖片"<img src='../images/btnDown.png' border='0'>"
                            }
                            else
                            {
                                MyHeader.Controls.Add(new LiteralControl("△"));
                            }
                        }
                        else
                        {
                            //MyHeader.Controls.Add(new LiteralControl(""));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 對GridView抬頭增加排序的圖案(DataTable)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="lsSortExpression"></param>
        /// <param name="lsSortDirection"></param>
        public void AddSortIcon(GridViewRowEventArgs e, string lsSortExpression, string lsSortDirection)
        {
            foreach (TableCell MyHeader in e.Row.Cells) //對每一格      
            {
                if (MyHeader.HasControls())
                {
                    if (MyHeader.Controls[0] is LinkButton)//避免GridView中的CheckBox全選的錯誤
                    {
                        if (((LinkButton)MyHeader.Controls[0]).CommandArgument == lsSortExpression)//是否為為排序欄位
                        {
                            if (lsSortDirection.ToUpper() == "ASC") //依排序方向加入箭號
                            {
                                MyHeader.Controls.Add(new LiteralControl("▽"));//可以換圖片"<img src='../images/btnDown.png' border='0'>"
                            }
                            else
                            {
                                MyHeader.Controls.Add(new LiteralControl("△"));
                            }
                        }
                        else
                        {
                            //MyHeader.Controls.Add(new LiteralControl(""));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 將Null資料，轉成空白資料 
        /// </summary>
        /// <param name="lsData">需轉換的資料</param>
        /// <returns></returns>
        public string getStr(Object lsData)
        {
            string lsRet = "";
            if (lsData == System.DBNull.Value)
            {
                lsRet = "";
            }
            else
            {
                //數字型態
                if (lsData is short || lsData is int || lsData is long || lsData is decimal)
                {
                    lsRet = lsData.ToString();
                    return lsRet;
                }
                else
                {
                    //文字
                    if (lsData.ToString().ToLower() == "&nbsp;")
                    {
                        lsRet = "";
                    }
                    else
                    {
                        lsRet = lsData.ToString().Replace("&nbsp;", "").Replace("&NBSP;", "");

                    }
                }
            }
            return lsRet;
        }

        /// <summary>
        /// 將傳入值轉換成數值傳出，如果是Null則回傳0
        /// </summary>
        /// <param name="fsData"></param>
        /// <returns></returns>
        public int getInt(object fsData)
        {
            if (fsData == DBNull.Value) { return 0; }

            int liVal;
            if (int.TryParse(fsData.ToString(), out liVal))
                return liVal;
            else
                return 0;
        }

        /// <summary>
        /// 將Null資料，轉成空白日期資料 
        /// Grace 調整，加入fsFormat參數 
        /// </summary>
        /// <param name="lsData">需轉換的資料</param>
        /// <param name="fsFormat"></param>
        /// <returns></returns>
        public string getDateFormat(object lsData, string fsFormat)
        {
            string functionReturnValue = null;
            if (lsData == System.DBNull.Value)
            {
                functionReturnValue = "";
            }
            else
            {
                if (lsData == null)
                {
                    functionReturnValue = "";
                }
                else
                {
                    if (lsData.ToString() == "")
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        DateTime ldDate;
                        bool lbRet = DateTime.TryParse(lsData.ToString(), out ldDate);
                        if (lbRet == false)
                        {
                            functionReturnValue = "";
                        }
                        else
                        {
                            if (fsFormat == "yyyy-MM-dd")
                            {
                                functionReturnValue = ldDate.ToString("yyyy-MM-dd");
                                //Judy upd for 2015/03/04, 調整判斷方式
                                //if (functionReturnValue == "1900-01-01" | functionReturnValue == "1900-1-1" | string.IsNullOrEmpty(functionReturnValue))
                                if (ldDate.ToString("yyyy-MM-dd") == "1900-01-01" || ldDate.ToString("yyyy-MM-dd") == "0000-00-00")
                                {
                                    functionReturnValue = "";
                                }

                            }
                            else
                            {
                                functionReturnValue = ldDate.ToString("yyyy/MM/dd");
                                //Judy upd for 2015/03/04, 調整判斷方式
                                //if (functionReturnValue == "1900/01/01" | functionReturnValue == "1900/1/1" | string.IsNullOrEmpty(functionReturnValue))
                                if (ldDate.ToString("yyyy/MM/dd") == "1900/01/01" || ldDate.ToString("yyyy/MM/dd") == "0000/00/00")
                                {
                                    functionReturnValue = "";
                                }
                            }
                        }
                    }
                }
            }
            return functionReturnValue;
        }

        public string getDateFormat(object lsData)
        {
            string functionReturnValue = null;
            if (lsData == System.DBNull.Value)
            {
                functionReturnValue = "";
            }
            else
            {
                if (lsData == null)
                {
                    functionReturnValue = "";
                }
                else
                {
                    if (lsData.ToString() == "")
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        DateTime ldDate;
                        bool lbRet = DateTime.TryParse(lsData.ToString(), out ldDate);
                        if (lbRet == false)
                        {
                            functionReturnValue = "";
                        }
                        else
                        {
                            if (_psDateFormat == "yyyy-MM-dd")
                            {
                                functionReturnValue = ldDate.ToString("yyyy-MM-dd");
                                //Judy upd for 2015/03/04, 調整判斷方式
                                //if (functionReturnValue == "1900-01-01" | functionReturnValue == "1900-1-1" | string.IsNullOrEmpty(functionReturnValue))
                                if (ldDate.ToString("yyyy-MM-dd") == "1900-01-01" || ldDate.ToString("yyyy-MM-dd") == "0000-00-00")
                                {
                                    functionReturnValue = "";
                                }

                            }
                            else
                            {
                                functionReturnValue = ldDate.ToString("yyyy/MM/dd");
                                //Judy upd for 2015/03/04, 調整判斷方式
                                //if (functionReturnValue == "1900/01/01" | functionReturnValue == "1900/1/1" | string.IsNullOrEmpty(functionReturnValue))
                                if (ldDate.ToString("yyyy/MM/dd") == "1900/01/01" || ldDate.ToString("yyyy/MM/dd") == "0000/00/00")
                                {
                                    functionReturnValue = "";
                                }
                            }
                        }
                    }
                }
            }
            return functionReturnValue;
        }
        /// <summary>
        /// 傳入日期資料，回傳日期 + 時間
        /// </summary>
        /// <param name="lsData">如果傳入值為1900/01/01，則回傳空白</param>
        /// <param name="fsFormat">值為"yyyy-MM-dd"或其他則視為"yyyy/MM/dd"</param>
        /// <returns></returns>
        public string getDateTimeFormat(object lsData, string fsFormat)
        {
            string functionReturnValue = null;
            if (lsData == System.DBNull.Value)
            {
                functionReturnValue = "";
            }
            else
            {
                if (lsData == null)
                {
                    functionReturnValue = "";
                }
                else
                {
                    if (lsData.ToString() == "")
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        DateTime ldDate;
                        bool lbRet = DateTime.TryParse((string)lsData, out ldDate);
                        if (lbRet == false)
                        {
                            functionReturnValue = "";
                        }
                        else
                        {
                            if (fsFormat == "yyyy-MM-dd")
                            {
                                functionReturnValue = ldDate.ToString("yyyy-MM-dd HH:mm:ss");
                                //Judy upd for 2015/03/04, 調整判斷方式
                                //if (functionReturnValue == "1900-01-01" | functionReturnValue == "1900-1-1" | string.IsNullOrEmpty(functionReturnValue))
                                if (ldDate.ToString("yyyy-MM-dd") == "1900-01-01" || ldDate.ToString("yyyy-MM-dd") == "0000-00-00")
                                {
                                    functionReturnValue = "";
                                }

                            }
                            else
                            {
                                functionReturnValue = ldDate.ToString("yyyy/MM/dd HH:mm:ss");
                                //Judy upd for 2015/03/04, 調整判斷方式
                                //if (functionReturnValue == "1900/01/01" | functionReturnValue == "1900/1/1" | string.IsNullOrEmpty(functionReturnValue))
                                if (ldDate.ToString("yyyy/MM/dd") == "1900/01/01" || ldDate.ToString("yyyy-MM-dd") == "0000/00/00")
                                {
                                    functionReturnValue = "";
                                }
                            }
                        }
                    }
                }
            }
            return functionReturnValue;
        }

        public string getDateTimeFormat(object lsData)
        {
            string functionReturnValue = null;
            if (lsData == System.DBNull.Value)
            {
                functionReturnValue = "";
            }
            else
            {
                if (lsData == null)
                {
                    functionReturnValue = "";
                }
                else
                {
                    if (lsData.ToString() == "")
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        DateTime ldDate;
                        bool lbRet = DateTime.TryParse((string)lsData, out ldDate);
                        if (lbRet == false)
                        {
                            functionReturnValue = "";
                        }
                        else
                        {
                            functionReturnValue = ldDate.ToString(_psDateTimeFormat);
                            //Judy upd for 2015/03/04, 調整判斷方式
                            //if (functionReturnValue == "1900/01/01" | functionReturnValue == "1900/1/1" | string.IsNullOrEmpty(functionReturnValue))
                            if (ldDate.ToString("yyyy/MM/dd") == "1900/01/01" || ldDate.ToString("yyyy-MM-dd") == "0000/00/00")
                            {
                                functionReturnValue = "";
                            }
                        }
                    }
                }
            }
            return functionReturnValue;
        }
 
        /// <summary>
        /// 將畫面資料做清空處理
        /// </summary>
        /// <param name="p"></param>
        public void ClearScreen(UpdatePanel p)
        {
            foreach (Control ctl in p.Controls)
            {
                foreach (Control ctlch in ctl.Controls)
                {
                    if (ctlch is TextBox) { ((TextBox)ctlch).Text = ""; continue; }
                    if (ctlch is DropDownList) { ((DropDownList)ctlch).SelectedIndex = -1; continue; }
                    if (ctlch is CheckBox) { ((CheckBox)ctlch).Checked = false; continue; }
                }
            }
        }

        public void ClearScreen(Panel p)
        {
            foreach (Control ctl in p.Controls)
            {
                foreach (Control ctlch in ctl.Controls)
                {
                    if (ctlch is TextBox) { ((TextBox)ctlch).Text = ""; continue; }
                    if (ctlch is DropDownList) { ((DropDownList)ctlch).SelectedIndex = -1; continue; }
                    if (ctlch is CheckBox) { ((CheckBox)ctlch).Checked = false; continue; }
                }
            }
        }


        /// <summary>
        /// 將依傳入值判斷是否為空白，如果是則顯示訊息 
        /// </summary>
        /// <param name="obj">檢查物件</param>
        /// <param name="Name">物件名稱</param>
        /// <param name="labMsg">檢查結果顯示至Label之物件名稱</param>
        /// <param name="OtherCheckFlag">欄位特別檢查
        /// DATE：日期檢查
        /// DATETIME：日期時間格式檢查
        /// TIME：時間格式檢查
        /// NUMERIC：數字檢查
        /// IDNO：身份證檢查
        /// EMAIL：Email格式檢查
        /// PHONE：電話格式檢查
        /// CODE：專案代碼輸入，英文、數字及減號
        /// </param>
        /// <returns></returns>
        public bool check_Null_Msg(object obj, string Name, Label labMsg, string OtherCheckFlag)
        {
            string lsData = "";
            bool lbRet = true;   

            //文字
            if (obj is TextBox)
            {
                lsData = ((TextBox)obj).Text.ToString().Trim();
            }

            if (obj is HiddenField)
            {
                lsData = ((HiddenField)obj).Value.ToString().Trim();
            }

            //Label
            if (obj is Label)
            {
                lsData = ((Label)obj).Text.ToString().Trim();
            }

            //下拉選項 
            if (obj is DropDownList)
            {
                lsData = ((DropDownList)obj).SelectedValue;
            }

            //string 
            if (obj is string)
            {
                lsData = (string)obj;
            }

            //=== 空白檢查 === 
            if (string.IsNullOrEmpty(lsData))
            {
                labMsg.Text = "「" + Name + "」Can not be empty！";
                lbRet = false;
            }

            //=============== 

            //=== 其他檢查 === 
            if (OtherCheckFlag != null & lbRet == true)
            {
                switch (OtherCheckFlag.ToUpper())
                {
                    case "DATE":
                        //日期檢查 
                        if (Information.IsDate(lsData) == false)
                        {
                            labMsg.Text = "「" + Name + "」Please enter『Date』Type！(Ex：2014/10/01)";
                            lbRet = false;
                        }

                        break;

                    case "DATETIME":
                        //日期時間檢查
                        DateTime dd;
                        if (!DateTime.TryParse(lsData, out dd))
                        {
                            labMsg.Text = "「" + Name + "」Please enter『DateTime』Type！(Ex：2014/10/01 15:10:00)";
                            lbRet = false;
                        }
                        break;

                    case "TIME":
                        //時間檢查
                        if (Information.IsDate(lsData) == false)
                        {
                            labMsg.Text = "「" + Name + "」Please enter『Time』Type！(Ex：15:10:00)";
                            lbRet = false;
                        }
                        break;

                    case "NUMERIC":
                        //數字 
                        if (Information.IsNumeric(lsData) == false)
                        {
                            labMsg.Text = "「" + Name + "」Please enter『Numeric』type！";
                            lbRet = false;
                        }

                        break;

                    case "EMAIL":
                        //EMail
                        if (!IsEmail(lsData))
                        {
                            labMsg.Text = "「" + Name + "」Format not correct!!(Ex:aaa@mail.com)";
                            lbRet = false;
                        }
                        break;

                    case "CODE":
                        //代碼輸入，只能輸入英文、數字及減號
                        //Regex regEngAndMath = new Regex("^[A-Za-z0-9_-]+$"); //Judy upd for 2014/09/16, 拿掉底線(名單自動匯入檔案會依底線做切割)
                        Regex regEngAndMath = new Regex("^[A-Za-z0-9-]+$");
                        if (!regEngAndMath.IsMatch(lsData))
                        {
                            labMsg.Text = "「" + Name + "」Format not correct!!(Only a-z、A-Z、0-9)";
                            lbRet = false;
                        }
                        break;
                }
            }
            //===============

            return lbRet;
        }

        /// <summary>
        /// 將依傳入值檢查格式，如果是空白則不檢查，有值再呼叫check_Null_Msg做檢核
        /// </summary>
        /// <param name="obj">檢查物件</param>
        /// <param name="Name">物件名稱</param>
        /// <param name="labMsg">檢查結果顯示至Label之物件名稱</param>
        /// <param name="OtherCheckFlag">欄位特別檢查
        /// DATE：日期檢查
        /// DATETIME：日期時間格式檢查
        /// NUMERIC：數字檢查
        /// IDNO：身份證檢查
        /// EMAIL：Email格式檢查
        /// PHONE：電話格式檢查
        /// CODE：專案代碼輸入，英文、數字及減號
        /// </param>
        /// <returns></returns>
        public bool check_Data_Msg(object obj, string Name, Label labMsg, string OtherCheckFlag)
        {
            string lsData = "";

            // TextBox 
            if (obj is TextBox)
            {
                lsData = ((TextBox)obj).Text.ToString().Trim();

            }

            // HiddenField 
            if (obj is HiddenField)
            {
                lsData = ((HiddenField)obj).Value.ToString().Trim();

            }

            // 下拉選項 
            if (obj is DropDownList)
            {
                lsData = ((DropDownList)obj).SelectedValue;
            }

            //string 
            if (obj is string)
            {
                lsData = (string)obj;
            }

            //=== 空白檢查 === 
            if (string.IsNullOrEmpty(lsData))
            {
                //空白不檢查
                return true;
            }
            else
            {
                return check_Null_Msg(obj, Name, labMsg, OtherCheckFlag);
            }
            //=============== 
        }

        /// <summary>
        /// 檢查EMail格式是否正確 
        /// </summary>
        /// <param name="str_Email">EMail資料</param>
        /// <returns></returns>
        public bool IsEmail(string fsEmail)
        {
            return Regex.IsMatch(fsEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 檢查Session是否存在
        /// </summary>
        /// <param name="myPage"></param>
        /// <param name="sysType"></param>
        /// <param name="obj"></param>
        public void CheckSession(Page myPage, object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                    ////myPage.ClientScript.RegisterClientScriptBlock(myPage.GetType(), "TimeOut", TimeOutMessage(), true);
                    myPage.Response.Redirect("~/Account/login");
            }
        }

        public string TimeOutMessage()
        {            
            string strMessage = null;

            strMessage = "alert('Sorry! You are idle too long. Please log on again.');";

            return strMessage;
        }

        /// <summary>
        /// 判斷日曆是否使用CSS
        /// </summary>
        /// <returns></returns>
        public void chkCalendar(HttpBrowserCapabilities bc, AjaxControlToolkit.CalendarExtender ce1, AjaxControlToolkit.CalendarExtender ce2)
        {
            float lfVersion = 0;
            float.TryParse(bc.Version, out lfVersion);

            if (lfVersion < 7)
            {
                ce1.CssClass = "";
                ce2.CssClass = "";
            }
        }

        /// <summary>
        /// 判斷日曆是否使用CSS
        /// </summary>
        /// <returns></returns>
        public void chkCalendar(HttpBrowserCapabilities bc, AjaxControlToolkit.CalendarExtender ce1)
        {
            float lfVersion = 0;
            float.TryParse(bc.Version, out lfVersion);

            if (lfVersion < 7)
            {
                ce1.CssClass = "";
            }
        }

        public int getDropDownListIndex(DropDownList ddl, string value, string text)
        {
            int liIdx = -1;

            for (int i = 0; i < ddl.Items.Count; i++)
            {
                string lsCompareData;
                string lsData;
                if (value != "")
                {
                    lsCompareData = ddl.Items[i].Value;
                    lsData = value;
                }
                else
                {
                    lsCompareData = ddl.Items[i].Text;
                    lsData = text;
                }
                if (lsCompareData.Equals(lsData))
                {
                    liIdx = i;
                    break;
                }
            }

            return liIdx;
        }

        public void SelectAllCheckBoxList(CheckBoxList cbl, bool bSelect)
        {
            if ((cbl.Items.Count > 0))
            {
                for (int i = 0; i <= cbl.Items.Count - 1; i++)
                {
                    cbl.Items[i].Selected = bSelect;
                }
            }
        }


        /// <summary>
        /// 取得CheckBoxList有勾選的Value或Text相加字串，各字串並以 ; 或 ' 隔開 
        /// </summary>
        /// <param name="cbl">CheckBoxList物件</param>
        /// <param name="ValueType">1表示Value，2表示Text</param>
        /// <param name="fsParam">'表示為了組SQL條件,結果如'A','B'，其他傳入用;做分隔</param>
        /// <returns></returns>
        public string GetCheckBoxListSelected(CheckBoxList cbl, Int16 ValueType, string fsParam)
        {
            string result = "";
            bool FirstChoice = true;

            if ((cbl.Items.Count > 0))
            {
                for (int i = 0; i <= cbl.Items.Count - 1; i++)
                {
                    if (cbl.Items[i].Selected)
                    {
                        if (!FirstChoice)
                        {
                            switch (fsParam)
                            {
                                case "'":
                                    //為了組SQL條件 
                                    result += "','";
                                    break;
                                default:
                                    result += ";";
                                    break;
                            }
                        }
                        else
                        {
                            if (fsParam == "'")
                            {
                                result += "'";
                            }
                            FirstChoice = false;
                        }

                        if (ValueType == Convert.ToInt16(SelectValueType.listValue))
                            result += cbl.Items[i].Value;
                        else
                            result += cbl.Items[i].Text;

                    }
                }
            }
            if (!string.IsNullOrEmpty(result) & fsParam == "'")
            {
                result += "'";
            }

            return result;
        }

        public DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable InsertRowAtEnd(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            return dt;
        }

        //傳入代碼值，傳回代碼名
        public string GetCodeName(DataTable dt, string codeValue)
        {
            string lsCodeName = string.Empty;            

            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return "Empty";
                }

                DataRow[] rows = dt.Select("[Code] = '" + codeValue + "' ");

                if (rows != null && rows.Length > 0)
                {
                    lsCodeName = rows[0]["CodeName"].ToString();
                }
            }
            catch (Exception ex)
            {
                lsCodeName = ex.Message;
            }

            return lsCodeName;
        }

        //傳入Key值，傳回代碼名
        public string GetSearchValue(DataTable dt, string queryName, string queryValue, string searchName)
        {
            string lsSearchValue = string.Empty;

            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return "Empty";
                }

                DataRow[] rows = dt.Select("[" + queryName + "] = '" + queryValue + "' ");

                if (rows != null && rows.Length > 0)
                {
                    lsSearchValue = rows[0]["searchName"].ToString();
                }
            }
            catch (Exception ex)
            {
                lsSearchValue = ex.Message;
            }

            return lsSearchValue;
        }

        //確認字串是否為有東西
        public bool checkNotNull(string strData)
        {
            bool result = false;
            if (strData != null && strData != "")
                result = true;
            return result;
        }
        //如果第一個是,扣掉
        public string transStrSubFstComma(string strTmpIn)
        {
            string strTmpOut = "";
            if (checkNotNull(strTmpIn))
            {
                if (strTmpIn[0] == ',')//如果出錯,外層要有try catch
                    strTmpOut = strTmpIn.Remove(0, 1);
                else
                    strTmpOut = strTmpIn;
            }
            return strTmpOut;
        }


        //上傳檔案
        //path = 絕對位置, fup = FileUpload, allowedExtextsion = new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" }
        //maxFileSize = 5100000(5MB), msg = erroe mag Label
        public void UploadFile(string path, FileUpload fup, List<string> allowedExtextsion, int maxFileSize, Label msg, string saveName) //
        {
            msg.Text = string.Empty;

            if (fup.HasFile == false)
            {
                msg.Text = "未選擇檔案";
                return;
            }

            // filMyFile.FileName 只有 "檔案名稱.副檔名"，並沒有 Client 端的完整理路徑
            string filename = fup.FileName;

            // 副檔名
            string extension = Path.GetExtension(filename).ToLowerInvariant();
            // 判斷是否為允許上傳的檔案副檔名
            //allowedExtextsion = new List<string> { ".jpg", ".bmp", ".gif", ".png", ".jpeg" };
            if (allowedExtextsion.IndexOf(extension) == -1)
            {
                msg.Text = "不允許該檔案上傳";
                return;
            }

            // 限制檔案大小，限制為 5MB
            int filesize = fup.PostedFile.ContentLength;
            if (filesize > maxFileSize)
            {
                msg.Text = "檔案大小超過上限，該檔案無法上傳";
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = path;  //path = 絕對位置
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            //// 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
            //// 使用 Path.Combine 來集合路徑的優點
            ///** 以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
            // *  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
            // *  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
            // *  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合**/
            //string serverFilePath = Path.Combine(serverDir, saveName + extension);
            ////string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
            //int fileCount = 1;
            //while (File.Exists(serverFilePath))
            //{
            //    // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
            //    filename = string.Concat(saveName, "_", fileCount, extension);
            //    serverFilePath = Path.Combine(serverDir, filename);
            //    fileCount++;
            //}

            filename = repeatFileN(serverDir, saveName, extension);
            string serverFilePath = Path.Combine(serverDir, filename);

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                fup.SaveAs(serverFilePath);
                msg.Text = "檔案上傳成功";
            }
            catch (Exception ex)
            {
                msg.Text = "檔案上傳失敗，";
                msg.Text += ex.Message;
            }
        }


        public string repeatFileN(string serverDir, string saveName, string extension)
        {
            string serverFilePath = Path.Combine(serverDir, saveName + extension);
            int fileCount = 1;
            string filename = saveName + extension;
            while (File.Exists(serverFilePath))
            {
                // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
                filename = string.Concat(saveName, "_", fileCount, extension);
                serverFilePath = Path.Combine(serverDir, filename);
                fileCount++;
            }

            return filename;
        }
    }


}
