using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.MIS.User
{
    public partial class UserDetail : System.Web.UI.Page
    {
        DataTable dt_auDepdes = new DataTable();
        int catchId;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            string NoneMIS_AU = PageListString.twoJumpP;
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin, 11);

            catchId = Convert.ToInt32(Request.Url.Query.Substring(4));

            dt_auDepdes = GlobalAnnounce.user.search_DepnDes(catchId);

            //先把控制項丟入
            CBList_Depts.DataSource = dt_auDepdes;
            CBList_Depts.DataBind();            

            string[] SingleUserInfo = new string[20];
            SingleUserInfo = GlobalAnnounce.user.searchSingleUserInfoDetail(catchId);

            //把Dep權限的  CheckBox預先勾選
            foreach (ListItem lst in CBList_Depts.Items)
            {
                for (int x = 0; x < 11; x++)
                    if (SingleUserInfo[x + 2] == "1")
                    {
                        CBList_Depts.Items[x].Selected = true;
                    }
            }
            CBList_Depts.Enabled = false;

            

            tb_misID.Text = SingleUserInfo[1].ToString();

            //把Status的  CheckBox預先勾選
            foreach (ListItem lst in CBList_accountStatus.Items)
            {
                for (int x = 0; x < 5;x++ )
                    if (SingleUserInfo[x+15] == "1")
                    {
                        CBList_accountStatus.Items[x].Selected = true;
                    }
            }

        }

        protected void btn_changeMISsys_pwd_Click(object sender, EventArgs e)
        {
            Response.Redirect(misSystem.PageListString.ChangePwd);
        }
    }
}