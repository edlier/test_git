using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using misSystem;
using MisSystem_ClassLibrary;
using MisSystem_ClassLibrary.sys;


namespace misSystem.UserControl.sys
{
    public partial class webUCDDLSysCode : System.Web.UI.UserControl
    {
        public static CommonUtils PrgUtil = new CommonUtils();
        public static SysCodeDB myCodeDB = new SysCodeDB();

        private string _codeType = string.Empty;
        public string CodeType
        {
            get { return _codeType.ToString(); }

            set
            {
                _codeType = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetDDLCode();
            }
        }

        public void SetDDLCode()
        {

            ddlCode.DataSource = myCodeDB.search_DDLCode(_codeType);            

            ddlCode.DataTextField = "CodeName";
            ddlCode.DataValueField = "Code";
            ddlCode.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select--";
            li.Value = "";

            ddlCode.Items.Insert(0, li);
            ddlCode.SelectedIndex = 0;

            ddlCode.Attributes.CssStyle.Add("margin-left", "5px");
            ddlCode.Attributes.CssStyle.Add("margin-right", "5px");
            ddlCode.Attributes.CssStyle.Add("text-align", "center");
        }



        //加入專案變更觸發事件
        public event EventHandler DDLCode_SelectedIndexChanged;
        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLCode_SelectedIndexChanged != null)
            {
                DDLCode_SelectedIndexChanged(this, e);
            }
        }

        /// <summary>
        /// 設定專案AutoPostBack值
        /// </summary>
        public bool AutoPostBack
        {
            set { ddlCode.AutoPostBack = value; }
        }

        //取得／設定專案值
        //private string country;
        public string CodeValue
        {
            get { return ddlCode.SelectedValue.ToString(); }

            set
            {
                ddlCode.SelectedIndex = PrgUtil.getDropDownListIndex(ddlCode, value, "");            
            }
        }

        public string CodeText
        {
            get { return ddlCode.SelectedItem.Text.ToString(); }

            set
            { ddlCode.SelectedIndex = PrgUtil.getDropDownListIndex(ddlCode, "", value); }
        }

        public int CodeItemCount
        {
            get { return ddlCode.Items.Count; }
        }

        public int CodeSelectedIndex
        {
            get { return ddlCode.SelectedIndex; }

            set
            {
                ddlCode.SelectedIndex = value;
            }
        }

        public bool myCodeEnabled
        {
            get { return ddlCode.Enabled; }

            set
            {
                ddlCode.Enabled = value;
            }
        }

        public string setCssClass 
        {
            get { return ddlCode.CssClass; }

            set
            {
                ddlCode.CssClass = value;
            }
        }

        public bool CodeVisible
        {
            get { return ddlCode.Visible; }

            set
            {
                ddlCode.Visible = value;
            }
        }

        public string getNextCodeValue(int idx)
        {
            return ddlCode.Items[idx].Value.ToString();
        }

        public Unit myCodeHight
        {
            get { return ddlCode.Height; }

            set
            {
                //ex. value="250px"
                ddlCode.Height = value;
            }
        }

        public Unit myCodeWidth
        {
            get { return ddlCode.Width; }

            set
            {
                //ex. value = new unit("250px")
                ddlCode.Width = value;
            }
        }

        public BorderStyle myCodeBorderStyle
        {
            get { return ddlCode.BorderStyle; }

            set
            {                
                ddlCode.BorderStyle = value;
            }  
        }

        public Color myCodeBackColor
        {
            get { return ddlCode.BackColor; }

            set
            {
                ddlCode.BackColor = value;
            }
        }

        public FontUnit myCodeFontUnitSize 
        {
            get { return ddlCode.Font.Size; }

            set
            {
                ddlCode.Font.Size = value;
            }
        }

        public Color myCodeForeColor
        {
            get { return ddlCode.ForeColor; }

            set
            {
                ddlCode.ForeColor = value;
            }
        }
        
        public Unit myDDLHight
        {
            get { return ddlCode.Height; }

            set
            {
                //ex. value="250px"
                ddlCode.Height = value;
            }
        }

        public Unit myDDLWidth
        {
            get { return ddlCode.Width; }

            set
            {
                //ex. value = new unit("250px")
                ddlCode.Width = value;
            }
        }

        public BorderStyle myDDLBorderStyle
        {
            get { return ddlCode.BorderStyle; }

            set
            {
                ddlCode.BorderStyle = value;
            }
        }

        public Color myDDLBackGroundColor
        {
            get { return ddlCode.BackColor; }

            set
            {
                ddlCode.BackColor = value;
            }
        }

        public FontUnit myDDLFontUnitSize
        {
            get { return ddlCode.Font.Size; }

            set
            {
                ddlCode.Font.Size = value;
            }
        }

        public Color myDDLForeColor
        {
            get { return ddlCode.ForeColor; }

            set
            {
                ddlCode.ForeColor = value;
            }
        }
        
    }
}