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
using MisSystem_ClassLibrary.HR.personnel;

namespace misSystem.UserControl.sys
{
    public partial class webUCDDLDept : System.Web.UI.UserControl
    {
        public static CommonUtils PrgUtil = new CommonUtils();
        public static DeptDB myDeptDB = new DeptDB();

        private string _companyID = string.Empty;
        public string DeptCompanyID
        {
            get { return _companyID.ToString(); }

            set
            {
                _companyID = value;
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

            ddlDept.DataSource = myDeptDB.search_DeptByComapnyID(_companyID);

            ddlDept.DataTextField = "Description";
            ddlDept.DataValueField = "DeptPID";
            ddlDept.DataBind();

            ListItem li = new ListItem();
            li.Text = "-- Select --";
            li.Value = "";

            ddlDept.Items.Insert(0, li);
            ddlDept.SelectedIndex = 0;

            ddlDept.Attributes.CssStyle.Add("margin-left", "5px");
            ddlDept.Attributes.CssStyle.Add("margin-right", "5px");
            ddlDept.Attributes.CssStyle.Add("text-align", "center");
        }

        // 加入專案變更觸發事件
        public event EventHandler myDDLDept_SelectedIndexChanged;
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myDDLDept_SelectedIndexChanged != null)
            {
                myDDLDept_SelectedIndexChanged(this, e);
            }
        }

        /// <summary>
        /// 設定專案AutoPostBack值
        /// </summary>
        public bool myAutoPostBack
        {
            set { ddlDept.AutoPostBack = value; }
        }

        //取得／設定專案值
        //private string country;
        public string myCodeValue
        {
            get { return ddlDept.SelectedValue.ToString(); }

            set
            {
                ddlDept.SelectedIndex = PrgUtil.getDropDownListIndex(ddlDept, value, "");            
            }
        }

        public string myCodeText
        {
            get { return ddlDept.SelectedItem.Text.ToString(); }

            set
            { ddlDept.SelectedIndex = PrgUtil.getDropDownListIndex(ddlDept, "", value); }
        }

        public int myCodeItemCount
        {
            get { return ddlDept.Items.Count; }
        }

        public int myCodeSelectedIndex
        {
            get { return ddlDept.SelectedIndex; }

            set
            {
                ddlDept.SelectedIndex = value;
            }
        }

        public bool myCodeEnabled
        {
            get { return ddlDept.Enabled; }

            set
            {
                ddlDept.Enabled = value;
            }
        }

        public string setCssClass 
        {
            get { return ddlDept.CssClass; }

            set
            {
                ddlDept.CssClass = value;
            }
        }

        public bool myCodeVisible
        {
            get { return ddlDept.Visible; }

            set
            {
                ddlDept.Visible = value;
            }
        }

        public string getNextCodeValue(int idx)
        {
            return ddlDept.Items[idx].Value.ToString();
        }

        public Unit myCodeHight
        {
            get { return ddlDept.Height; }

            set
            {
                //ex. value="250px"
                ddlDept.Height = value;
            }
        }

        public Unit myCodeWidth
        {
            get { return ddlDept.Width; }

            set
            {
                //ex. value = new unit("250px")
                ddlDept.Width = value;
            }
        }

        public BorderStyle myCodeBorderStyle
        {
            get { return ddlDept.BorderStyle; }

            set
            {
                ddlDept.BorderStyle = value;
            }  
        }

        public Color myCodeBackColor
        {
            get { return ddlDept.BackColor; }

            set
            {
                ddlDept.BackColor = value;
            }
        }

        public FontUnit myCodeFontUnitSize 
        {
            get { return ddlDept.Font.Size; }

            set
            {
                ddlDept.Font.Size = value;
            }
        }

        public Color myCodeForeColor
        {
            get { return ddlDept.ForeColor; }

            set
            {
                ddlDept.ForeColor = value;
            }
        }
        
        public Unit myDDLHight
        {
            get { return ddlDept.Height; }

            set
            {
                //ex. value="250px"
                ddlDept.Height = value;
            }
        }

        public Unit myDDLWidth
        {
            get { return ddlDept.Width; }

            set
            {
                //ex. value = new unit("250px")
                ddlDept.Width = value;
            }
        }

        public BorderStyle myDDLBorderStyle
        {
            get { return ddlDept.BorderStyle; }

            set
            {
                ddlDept.BorderStyle = value;
            }
        }

        public Color myDDLBackColor
        {
            get { return ddlDept.BackColor; }

            set
            {
                ddlDept.BackColor = value;
            }
        }

        public FontUnit myDDLFontUnitSize
        {
            get { return ddlDept.Font.Size; }

            set
            {
                ddlDept.Font.Size = value;
            }
        }

        public Color myDDLForeColor
        {
            get { return ddlDept.ForeColor; }

            set
            {
                ddlDept.ForeColor = value;
            }
        }
        
    }
}