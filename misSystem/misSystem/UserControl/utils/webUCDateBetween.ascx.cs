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
    public partial class webUCDateBetween : System.Web.UI.UserControl
    {
        public static CommonUtils PrgUtil = new CommonUtils();

        protected void Page_Init(object sender, EventArgs e)
        //protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CalendarExtender1.Format = CommonUtils._psDateFormat;
                CalendarExtender2.Format = CommonUtils._psDateFormat;
                txtDate1.Text = DateTime.Now.ToString(CommonUtils._psDateFormat);
                txtDate2.Text = DateTime.Now.ToString(CommonUtils._psDateFormat);

                butDate1.AlternateText = "Open Calendar，Click Date";
                butDate2.AlternateText = "Open Calendar，Click Date";
                //判斷IE版本決定是否加入CSS
                HttpBrowserCapabilities bc;
                bc = Request.Browser;
                PrgUtil.chkCalendar(bc, CalendarExtender1, CalendarExtender2);
            }
        }


        public string Date1
        {
            get
            {

                return txtDate1.Text;
            }
            set
            {
                this.txtDate1.Text = value;
            }
        }

        public string Date2
        {
            get
            {
                return txtDate2.Text;
            }
            set
            {
                this.txtDate2.Text = value;
            }
        }

        /// <summary>
        /// 日期欄位Enable設定
        /// </summary>
        private bool dateEnable;

        public bool DateEnable
        {
            set
            {
                txtDate1.Enabled = value;
                txtDate2.Enabled = value;
            }
        }

        //加入專案變更觸發事件
        public event EventHandler Date1_TextChanged;
        protected void txtDate1_TextChanged(object sender, EventArgs e)
        {
            if (Date1_TextChanged != null)
            {
                Date1_TextChanged(this, e);
            }
        }

        public event EventHandler Date2_TextChanged;
        protected void txtDate2_TextChanged(object sender, EventArgs e)
        {
            if (Date2_TextChanged != null)
            {
                Date2_TextChanged(this, e);
            }
        }

        /// <summary>
        /// 設定專案AutoPostBack值
        /// </summary>
        public bool AutoPostBack_Date1
        {
            set { txtDate1.AutoPostBack = value; }
        }

        public bool AutoPostBack_Date2
        {
            set { txtDate2.AutoPostBack = value; }
        }

    }
}