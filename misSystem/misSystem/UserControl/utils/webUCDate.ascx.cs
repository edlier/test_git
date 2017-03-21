using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using misSystem;
using MisSystem_ClassLibrary;

namespace misSystem.UserControl.utils
{
    public partial class webUCDate : System.Web.UI.UserControl
    {
        public static CommonUtils PrgUtil = new CommonUtils();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CalendarExtender1.Format = CommonUtils._psDateFormat;
                txtDate.Text = DateTime.Now.ToString(CommonUtils._psDateFormat);

                butDate.AlternateText = "Open Calendar，Click Date";

                //判斷IE版本決定是否加入CSS
                HttpBrowserCapabilities bc;
                bc = Request.Browser;
                PrgUtil.chkCalendar(bc, CalendarExtender1);
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Date
        {
            get
            {
                return txtDate.Text;
            }
            set
            {
                this.txtDate.Text = value;
            }
        }

        /// <summary>
        /// 設定日期欄位ReadOnly屬性
        /// </summary>
        /// <param name="bValue"></param>
        public void txtReadOnly(bool bValue)
        {
            this.txtDate.ReadOnly = bValue;
        }

        public bool BtnDateEnabled
        {
            get { return butDate.Enabled; }

            set
            {
                butDate.Enabled = value;
            }
        }        

        public bool DateReadOnly
        {
            get { return txtDate.ReadOnly; }

            set
            {
                txtDate.ReadOnly = value;
            }
        }

        /// <summary>
        /// 設定日期欄位Enable屬性
        /// </summary>
        /// <param name="bValue"></param>
        public void txtEnable(bool bValue)
        {
            this.txtDate.Enabled = bValue;
        }

        /// <summary>
        /// 設定日期背景色
        /// </summary>
        /// <param name="bkColor"></param>
        public void txtColor(System.Drawing.Color bkColor)
        {
            this.txtDate.BackColor = bkColor;
        }

        //加入日期變更觸發事件
        public event EventHandler TxtDate_TextChanged;
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (TxtDate_TextChanged != null)
            {
                TxtDate_TextChanged(this, e);
            }
        }

        /// <summary>
        /// 設定日期AutoPostBack值
        /// </summary>
        public bool AutoPostBack
        {
            set { txtDate.AutoPostBack = value; }
        }

        public bool DateEnabled
        {
            get { return txtDate.Enabled; }

            set
            {
                txtDate.Enabled = value;
            }
        }

        public FontUnit myFontUnitSize
        {
            get { return txtDate.Font.Size; }

            set
            {
                txtDate.Font.Size = value;
            }
        }
    
    }
}