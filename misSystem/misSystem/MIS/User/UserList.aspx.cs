using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.MIS.User
{
    public partial class UserList : System.Web.UI.Page
    {
        DataTable userList=new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            string NoneMIS_AU = PageListString.twoJumpP;
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);
            userList = GlobalAnnounce.user.search_UserList();
            if (!IsPostBack)
            {
                GlobalAnnounce.OtherFuction.BindDataToGrid(userList,grid_UserList);
            }
        }

        protected void btn_idSorting_Click(object sender, EventArgs e)
        {
            int selectedVaule = Convert.ToInt32(drop_select.SelectedValue);
            if (btn_idSorting.Text == "DESC")
            {
                int sorting = 2;
                doSwitchForSelect(selectedVaule, sorting);
                btn_idSorting.Text = "ASC";
            }
            else
            {
                int sorting = 1;
                doSwitchForSelect(selectedVaule, sorting);
                btn_idSorting.Text = "DESC";
            }
        }
        protected void drop_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedVaule=Convert.ToInt32(drop_select.SelectedValue);
            //  sorting =   1   = id ASC
            int sorting=1;

            doSwitchForSelect(selectedVaule, sorting);
        }

        //只需要Sort ID
        private void changeUserGridtoSort(string sortOrder)
        {
            DataView dv = new DataView(userList);
            dv.Sort = sortOrder;
            GlobalAnnounce.OtherFuction.BindDataToGrid(dv, grid_UserList);
        }

        //Select + Sorting
        private void changeUserGridtoSort(string expression, string sortOrder)
        {
            DataView dv = new DataView(userList);
            dv.Sort = sortOrder;
            dv.RowFilter = expression;
            GlobalAnnounce.OtherFuction.BindDataToGrid(dv, grid_UserList);
        }
        
        protected void doSwitchForSelect(int selectedValue, int sorting)
        {
            string expression;
            //  sorting =   1   = id ASC
            //  sorting =   2   = id DESC
            string sortfor = "";
            if (sorting == 1)
            {
                sortfor = "id asc";
            }
            else if (sorting==2)
            {
                sortfor = "id desc";
            }

            switch (selectedValue)
            {
                case 0:
                    changeUserGridtoSort(sortfor);
                    break;
                case 1:
                    expression = "AD = 1";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 2:
                    expression = "AD = 0";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 3:
                    expression = "email = 1";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 4:
                    expression = "email = 0";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 5:
                    expression = "skype = 1";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 6:
                    expression = "skype = 0";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 7:
                    expression = "leaved = 1";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 8:
                    expression = "leaved = 0";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 9:
                    expression = "misaccount = 1";
                    changeUserGridtoSort(expression, sortfor);
                    break;
                case 10:
                    expression = "misaccount = 0";
                    changeUserGridtoSort(expression, sortfor);
                    break;
            }
        }
    }
}