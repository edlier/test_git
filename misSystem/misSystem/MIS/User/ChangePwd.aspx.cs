using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.MIS.User
{
    public partial class ChangePwd : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = PageListString.twoJumpP;
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            //先檢查就密碼有沒有輸入正確
            DataTable userPWD =new DataTable();
            Boolean userOldPwdTF;
            userOldPwdTF=GlobalAnnounce.user.search_UserPwd(Session[SessionString.userID].ToString(), tb_oldpwd.Text);
            
            

            //再檢查兩次密碼是否輸入一致
        }
    }
}