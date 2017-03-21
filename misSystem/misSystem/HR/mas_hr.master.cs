using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.HR
{
    public partial class mas_hr : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //把預設按鈕指定給它
            this.Page.Form.DefaultButton = btn_def.UniqueID;
            //再把它隱藏起來
            btn_def.Style.Add("display", "none");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //什麼都不做，避免使用者誤按Enter用

        }
        protected void btn_dailyReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("HR_checkReport_day.aspx");
        }

        protected void btn_monthlyReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("HR_checkReport_month.aspx");
        }

        protected void btn_singleUserMonthlyDT_Click(object sender, EventArgs e)
        {
            Response.Redirect("HR_checkReport_personal.aspx");
        }

        protected void btn_singleUserTakeDayOff_Click(object sender, EventArgs e)
        {
            Response.Redirect("HR_checkReport_memo.aspx");
        }

        protected void btn_insertEmployeeTakeOffDay_Click(object sender, EventArgs e)
        {
            Response.Redirect("HR_QuicklyImportDT.aspx");
        }


    }
}