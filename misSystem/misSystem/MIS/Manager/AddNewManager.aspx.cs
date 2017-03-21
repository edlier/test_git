using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.MIS.Manager
{
    public partial class AddNewManager : System.Web.UI.Page
    {
        DataTable dt_searchdept;
        DataTable dt_searchUser;
        DataTable dt_searchLevel;
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin, 11);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //'XX'型別 必須置於有 runat=server 的表單標記之中
        }
        protected void drop_UserInit(object sender, EventArgs e)
        {
            dt_searchUser = GlobalAnnounce.user.search_dropUserList();
            GlobalAnnounce.OtherFuction.BindDataToDrop(dt_searchUser, drop_UserList);
        }
        protected void drop_deptInit(object sender, EventArgs e)
        {
            dt_searchdept = GlobalAnnounce.user.searchdept();
            //GlobalAnnounce.OtherFuction.BindDataToDrop(dt_searchdept, drop_dept);

            drop_dept.DataSource = dt_searchdept;
            drop_dept.DataBind();
        }
        protected void drop_levelInit(object sender, EventArgs e)
        {
            dt_searchLevel = GlobalAnnounce.user.searchLevelList();
            GlobalAnnounce.OtherFuction.BindDataToDrop(dt_searchLevel,drop_level);
        }

        protected void btn_set_Click(object sender, EventArgs e)
        {
            string DeptID = "";
            if (Convert.ToInt32(drop_level.SelectedValue) > 3)
            {
                DeptID = "0";
            }
            else{
                DeptID = drop_dept.SelectedValue;
            }
            GlobalAnnounce.manager.insertau_managerlist(drop_UserList.SelectedValue, DeptID, drop_level.SelectedValue);
        }
        
    }
}