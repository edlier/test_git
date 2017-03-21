using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using misSystem;
using MisSystem_ClassLibrary;
using MisSystem_ClassLibrary.sys;
using System.Drawing;

namespace misSystem.UserControl.sys
{
    public partial class webUCDDLCompany : System.Web.UI.UserControl
    {
        public static CommonUtils PrgUtil = new CommonUtils();        
        public static CompanyDB myCompanyDB = new CompanyDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetDDLCode();
            }
        }

        private void SetDDLCode()
        {

            ddlCompany.DataSource = myCompanyDB.search_DDLCompany();

            ddlCompany.DataTextField = "CompanyCode";
            ddlCompany.DataValueField = "CompanyPID";
            ddlCompany.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select--";
            li.Value = "";

            ddlCompany.Items.Insert(0, li);
            ddlCompany.SelectedIndex = 0;

            ddlCompany.Attributes.CssStyle.Add("margin-left", "5px");
            ddlCompany.Attributes.CssStyle.Add("margin-right", "5px");
            ddlCompany.Attributes.CssStyle.Add("text-align", "center");
        }

        // 加入專案變更觸發事件
        public event EventHandler myDDLCompany_SelectedIndexChanged;
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myDDLCompany_SelectedIndexChanged != null)
            {
                myDDLCompany_SelectedIndexChanged(this, e);
            }
        }

        /// <summary>
        /// 設定專案AutoPostBack值
        /// </summary>
        public bool myAutoPostBack
        {
            set { ddlCompany.AutoPostBack = value; }
        }

        //取得／設定專案值
        //private string country;
        public string myCodeValue
        {
            get { return ddlCompany.SelectedValue.ToString(); }

            set
            {
                ddlCompany.SelectedIndex = PrgUtil.getDropDownListIndex(ddlCompany, value, "");            
            }
        }

        public string myCodeText
        {
            get { return ddlCompany.SelectedItem.Text.ToString(); }

            set
            { ddlCompany.SelectedIndex = PrgUtil.getDropDownListIndex(ddlCompany, "", value); }
        }

        public int myCodeItemCount
        {
            get { return ddlCompany.Items.Count; }
        }

        public int myCodeSelectedIndex
        {
            get { return ddlCompany.SelectedIndex; }

            set
            {
                ddlCompany.SelectedIndex = value;
            }
        }

        public bool myCodeEnabled
        {
            get { return ddlCompany.Enabled; }

            set
            {
                ddlCompany.Enabled = value;
            }
        }

        public string setCssClass 
        {
            get { return ddlCompany.CssClass; }

            set
            {
                ddlCompany.CssClass = value;
            }
        }

        public bool CodeVisible
        {
            get { return ddlCompany.Visible; }

            set
            {
                ddlCompany.Visible = value;
            }
        }

        public string getNextCodeValue(int idx)
        {
            return ddlCompany.Items[idx].Value.ToString();
        }

        public Unit myCodeHight
        {
            get { return ddlCompany.Height; }

            set
            {
                //ex. value="250px"
                ddlCompany.Height = value;
            }
        }

        public Unit myCodeWidth
        {
            get { return ddlCompany.Width; }

            set
            {
                //ex. value = new unit("250px")
                ddlCompany.Width = value;
            }
        }

        public BorderStyle myCodeBorderStyle
        {
            get { return ddlCompany.BorderStyle; }

            set
            {
                ddlCompany.BorderStyle = value;
            }  
        }

        public Color myCodeBackColor
        {
            get { return ddlCompany.BackColor; }

            set
            {
                ddlCompany.BackColor = value;
            }
        }

        public FontUnit myCodeFontUnitSize 
        {
            get { return ddlCompany.Font.Size; }

            set
            {
                ddlCompany.Font.Size = value;
            }
        }

        public Color myCodeForeColor
        {
            get { return ddlCompany.ForeColor; }

            set
            {
                ddlCompany.ForeColor = value;
            }
        }
        
        public Unit myDDLHight
        {
            get { return ddlCompany.Height; }

            set
            {
                //ex. value="250px"
                ddlCompany.Height = value;
            }
        }

        public Unit myDDLWidth
        {
            get { return ddlCompany.Width; }

            set
            {
                //ex. value = new unit("250px")
                ddlCompany.Width = value;
            }
        }

        public BorderStyle myDDLBorderStyle
        {
            get { return ddlCompany.BorderStyle; }

            set
            {
                ddlCompany.BorderStyle = value;
            }
        }

        public Color myDDLBackColor
        {
            get { return ddlCompany.BackColor; }

            set
            {
                ddlCompany.BackColor = value;
            }
        }

        public FontUnit myDDLFontUnitSize
        {
            get { return ddlCompany.Font.Size; }

            set
            {
                ddlCompany.Font.Size = value;
            }
        }

        public Color myDDLForeColor
        {
            get { return ddlCompany.ForeColor; }

            set
            {
                ddlCompany.ForeColor = value;
            }
        }
        
    }
}