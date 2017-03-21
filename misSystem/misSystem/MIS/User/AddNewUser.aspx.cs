using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.MIS.User
{
    public partial class AddNewUser : System.Web.UI.Page
    {
        DataTable dt_searchdept;
        DataTable dt_auList;
        int[] totalcount = new int[12];
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = PageListString.twoJumpP;
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            dt_auList = GlobalAnnounce.user.auList_Option();
        }
        protected void drop_deptInit(object sender, EventArgs e)
        {
            dt_searchdept = GlobalAnnounce.user.searchdept();
            GlobalAnnounce.OtherFuction.BindDataToDrop(dt_searchdept,drop_dept);
        }
        protected void btn_generate_Click(object sender, EventArgs e)
        {
            if (tb_FirstName.Text != "" && tb_LastName.Text != "")
            {
                tb_mis_account.Text = tb_FirstName.Text + tb_LastName.Text;
                tb_Ad_Account.Text = tb_FirstName.Text + tb_LastName.Text;
                tb_email.Text = tb_FirstName.Text + tb_LastName.Text + "@lightmed.com";
                tb_skype.Text = "lightmed_" + tb_FirstName.Text + tb_LastName.Text;
                tb_skypeRegistrationEmail.Text = tb_email.Text;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('英文姓名沒有填寫');", true);
            }

        }
        protected void btn_nxt_Click(object sender, EventArgs e)
        {
            Panel_ACC.Visible = false;
            Panel_Sales.Visible = false;
            Panel_PDE.Visible = false;
            Panel_service.Visible = false;
            Panel_Administration.Visible = false;
            Panel_QC.Visible = false;
            Panel_PCMC.Visible = false;
            Panel_Production.Visible = false;
            Panel_Regulatory.Visible = false;
            Panel_Check.Visible = false;
            Panel_IT.Visible = false;

            btn_Save.Visible = true;


            totalcount = getAuthorizationOptions();

            for (int x = 1; x < 12; x++)
            {
                switch (totalcount[x])
                {
                    case 1:
                        Panel_ACC.Visible = true;
                        break;
                    case 2:
                        Panel_Sales.Visible = true;
                        break;
                    case 3:
                        depItemDetail(3, Panel_PDE, CheckList_PDE);
                        break;
                    case 4:
                        depItemDetail(4, Panel_service, CheckList_service);
                        break;
                    case 5:
                        depItemDetail(5, Panel_Administration, CheckList_Admin);
                        break;
                    case 6:
                        depItemDetail(6, Panel_QC, CheckList_QC);
                        break;
                    case 7:
                        depItemDetail(7, Panel_PCMC, CheckList_PCMC);
                        break;
                    case 8:
                        depItemDetail(8, Panel_Production, CheckList_Production);
                        break;
                    case 9:
                        depItemDetail(9, Panel_Regulatory, CheckList_Regulatory);
                        break;
                    case 10:
                        depItemDetail(10, Panel_Check, CheckList_Check);
                        break;
                    case 11:
                        Panel_IT.Visible = true;
                        break;


                }
            }
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (tb_mis_account.Text != "" && tb_mis_pwd.Text != "" && tb_workerNum.Text != "")
            {
                int strongOrnot=GlobalAnnounce.validateSession.checkPassword(tb_mis_pwd.Text);
                if (strongOrnot < 3)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('MIS sys Password is weak!!');", true);
                }
                else
                {
                    GlobalAnnounce.user.insertUserComputerForm(
                            tb_chineeseName.Text,
                            tb_FirstName.Text,
                            tb_LastName.Text,
                            drop_dept.SelectedValue,
                            tb_workerNum.Text,

                            datepicker.Text,
                            tb_Ad_Account.Text,
                            tb_Ad_pwd.Text,
                            tb_email.Text,
                            tb_email_pwd.Text,

                            tb_skype.Text,
                            tb_skype_pwd.Text,
                            tb_skypeRegistrationEmail.Text
                    );


                    int count = 100;
                    int[] select1 = new int[100];

                    totalcount = getAuthorizationOptions();

                    //insert資料進UserInfo
                    GlobalAnnounce.user.insertUserInfo(totalcount, tb_mis_account.Text, tb_mis_pwd.Text, (drop_level.SelectedValue).ToString(), tb_workerNum.Text);



                    for (int x = 1; x < 12; x++)
                    {

                        switch (totalcount[x])
                        {

                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                //↓  (CheckBoxList checkListName,int count,int minus)   count為陣列的總量
                                select1 = getSelectItemOfOptions(CheckList_PDE, count, 100);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_PDE);
                                break;
                            case 4:
                                select1 = getSelectItemOfOptions(CheckList_service, count, 200);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_service);
                                break;
                            case 5:
                                select1 = getSelectItemOfOptions(CheckList_Admin, count, 300);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_Admin);
                                break;
                            case 6:
                                select1 = getSelectItemOfOptions(CheckList_QC, count, 400);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_QC);
                                break;
                            case 7:
                                select1 = getSelectItemOfOptions(CheckList_PCMC, count, 500);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_PCMC);
                                break;
                            case 8:
                                select1 = getSelectItemOfOptions(CheckList_Production, count, 700);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_Production);
                                break;
                            case 9:
                                select1 = getSelectItemOfOptions(CheckList_Regulatory, count, 800);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_Regulatory);
                                break;
                            case 10:
                                select1 = getSelectItemOfOptions(CheckList_Check, count, 900);
                                getAlltheDataAndThrowToDatabase(totalcount[x], select1, CheckList_Check);
                                break;
                            case 11:
                                break;

                        }
                    }
                    Response.Redirect(PageListString.UserList);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('MIS data OR Worker Number is Empty');", true);
            }
                

        }




        //--------------------------------------------------------------
        // ---------------------- FUNCTION -----------------------
        //--------------------------------------------------------------
        private int[] getAuthorizationOptions()
        {
            foreach (ListItem lst in CB_au.Items)
            {
                if (lst.Selected == true)
                {
                    totalcount[Convert.ToInt32(lst.Value)] = Convert.ToInt32(lst.Value);
                }
            }
            return totalcount;
        }

        //選到多少個ITEM選項
        private int[] getSelectItemOfOptions(CheckBoxList checkListName, int count, int minus)
        {
            int[] xcount = new int[count];
            foreach (ListItem lst in checkListName.Items)
            {
                if (lst.Selected == true)
                {
                    xcount[Convert.ToInt32(lst.Value) - minus] = Convert.ToInt32(lst.Value);
                }
            }
            return xcount;
        }

        //全部的選項
        private int[] getAllOfOptions(CheckBoxList checkListName, int count)
        {
            int[] xcount = new int[count];
            int c = 1;
            foreach (ListItem lst in checkListName.Items)
            {
                xcount[c] = Convert.ToInt32(lst.Value);
                c++;
            }
            return xcount;
        }
        private void depItemDetail(int number, Panel Panel_dep, CheckBoxList checkboxlist_dep)
        {
            Panel_dep.Visible = true;
            checkboxlist_dep.Items.Clear();
            for (int i = 0; i < dt_auList.Rows.Count; i++)
            {
                // If auDepID == 過來的ID ，因為List是整張，需要再篩選
                if ((dt_auList.Rows[i]["auDepID"]).ToString() == number.ToString())
                {
                    checkboxlist_dep.Items.Add(new ListItem((dt_auList.Rows[i]["description"]).ToString(), (dt_auList.Rows[i]["num"]).ToString()));
                }
            }
        }
        private void getAlltheDataAndThrowToDatabase(int dep, int[] select, CheckBoxList checklist)
        {

            //先查詢剛剛丟入資料，新增使用者的ID為何
            string id = GlobalAnnounce.user.search_userIDAdd(tb_workerNum.Text);
            //All是從1開始計算的，1~100
            int[] selectAll1 = new int[100];
            int count = 100;

            selectAll1 = getAllOfOptions(checklist, count);
            GlobalAnnounce.user.insertAuDepData(dep, select, selectAll1, id);

        }

    }
}