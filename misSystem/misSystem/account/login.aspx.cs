using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.account
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (Session[SessionString.userID] != null)
            {
                Response.Redirect("../Default.aspx");
            }
        }
        protected void LogIn(object sender, EventArgs e)
        {
            //驗證過 email 和 密碼不是空值
            if (IsValid)
            {
                DataTable getUserInfo;
                getUserInfo = GlobalAnnounce.logindb.validateLogin((UserName.Text).ToString(), (Password.Text).ToString());

                if (getUserInfo.Rows.Count == 0)
                {
                    //寫LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOG

                    //跳出錯誤視窗
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wrong email or password');", true);
                }
                ////使用者 email 和 password 都輸入正確，把ID丟入session，並跳轉到Default
                else
                {
                    //寫LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOG


                    Session[SessionString.userID] = getUserInfo.Rows[0]["EmpID"];
                    //string auDep="auDep";
                    //for (int i = 1; i < 12; i++)
                    //{
                    //    if (Convert.ToInt32(getUserInfo.Rows[0][auDep+i.ToString()]) != 0)
                    //    {
                    //        Session[auDep+i.ToString()]=getUserInfo.Rows[0][auDep+i.ToString()];
                    //    }
                    //}
                    //Session[SessionString.auDep1] = getUserInfo.Rows[0]["auDep1"];
                    //Session[SessionString.auDep2] = getUserInfo.Rows[0]["auDep2"];
                    //Session[SessionString.auDep3] = getUserInfo.Rows[0]["auDep3"];
                    //Session[SessionString.auDep4] = getUserInfo.Rows[0]["auDep4"];
                    //Session[SessionString.auDep5] = getUserInfo.Rows[0]["auDep5"];
                    //Session[SessionString.auDep6] = getUserInfo.Rows[0]["auDep6"];
                    //Session[SessionString.auDep7] = getUserInfo.Rows[0]["auDep7"];
                    //Session[SessionString.auDep8] = getUserInfo.Rows[0]["auDep8"];
                    //Session[SessionString.auDep9] = getUserInfo.Rows[0]["auDep9"];
                    //Session[SessionString.auDep10] = getUserInfo.Rows[0]["auDep10"];
                    //Session[SessionString.auDep11] = getUserInfo.Rows[0]["auDep11"];
                    //Session[SessionString.level] = getUserInfo.Rows[0]["level"];
                    Response.Redirect("../Default.aspx");
                }
            }
        }
        protected void titleLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void titleMainPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}