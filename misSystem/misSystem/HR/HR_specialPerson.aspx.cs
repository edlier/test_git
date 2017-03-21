using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.HR
{
    public partial class HR_specialPerson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // HR_class 內有setDropdownlist可用   不過是傳sqlstr進去
            if (!IsPostBack)
            {
                #region DropDownList & datepicker設定資料
                //類別
                System.Data.DataTable dt = GlobalAnnounce.dataSearching.search_spcialPersonType();
                GlobalAnnounce.OtherFuction.Drop_SetValueAndText(type_dl, "id", "Descript");
                GlobalAnnounce.OtherFuction.BindDataToDrop(dt, type_dl,"***");

                //系統時間
                System.Data.DataTable dt2 = GlobalAnnounce.dataSearching.search_sysTime(1,"");
                //checkin
                GlobalAnnounce.OtherFuction.Drop_SetValueAndText(checkin_dl, "id", "time");
                GlobalAnnounce.OtherFuction.BindDataToDrop(dt2, checkin_dl, "***");
                //checkout
                GlobalAnnounce.OtherFuction.Drop_SetValueAndText(checkout_dl, "id", "time");
                GlobalAnnounce.OtherFuction.BindDataToDrop(dt2, checkout_dl, "***"); 

                DateTime now = DateTime.Now;
                date_text.Text = now.ToString("yyyy-MM-dd");
                #endregion

                showDataTable();
                uc_all_employee.setOriDropDownlist();
            }
        }

        void showDataTable()
        {
            DataTable dt3 = GlobalAnnounce.dataSearching.search_spePersonTable();
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt3, spePerson_grid);
        }

        protected void type_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type_dl.SelectedValue == "2")
            {
                dropPanel.Visible = true;
                tbPanel.Visible = false;
            }
            else if (type_dl.SelectedValue == "4")
            {
                dropPanel.Visible = false;
                tbPanel.Visible = true;
            }
            else
            {
                dropPanel.Visible = false;
                tbPanel.Visible = false;
                checkin_dl.SelectedIndex = 0;
                checkout_dl.SelectedIndex = 0;
            }
        }

        protected void submit_btn_Click(object sender, EventArgs e)
        {
            if (uc_all_employee.getValue() == "***")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請選擇員工!');", true); return;
            }
            else
            {
                //search_specialperson(int i,string data)  //i=1->data=workerNum, i=2->data=id
               DataTable dt= GlobalAnnounce.dataSearching.search_specialperson(1,uc_all_employee.getValue());
               if (dt.Rows.Count > 0)
               { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('員工已存在!'); window.location.href='HR_specialPerson.aspx';", true); return; }
            }

            if (type_dl.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請選擇類別!');", true); return;
            }

            if (type_dl.SelectedIndex == 2)
            {
                if (checkin_dl.SelectedIndex == 0 || checkout_dl.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請選擇 checkin 或 checkout 時間!');", true); return;
                }
            }

            if (type_dl.SelectedValue == "4") //離職
            {
                GlobalAnnounce.dataSearching.update_empResignation(uc_all_employee.getValue(), date_text.Text, Session[SessionString.userID].ToString());
                //GlobalAnnounce.dataSearching.update_userinfo(uc_all_employee.getValue(), date_text.Text);
            }
            else
            {
                GlobalAnnounce.dataSearching.insert_specialperson(uc_all_employee.getValue(), checkin_dl.SelectedValue, checkout_dl.SelectedValue, type_dl.SelectedValue);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Success!'); window.location.href='HR_specialPerson.aspx';", true);
        }

        protected void update_btn_Click(object sender, EventArgs e)
        {
            
            if (type_dl.SelectedIndex == 2)
            {
                if (checkin_dl.SelectedIndex == 0 || checkout_dl.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請選擇 checkin 或 checkout 時間!');", true); return;
                }
            }

            //search_specialperson(int i,string data)  //i=1->data=workerNum, i=2->data=id
            DataTable dt = GlobalAnnounce.dataSearching.search_specialperson(2, id2_hf.Value);//判定id是否還存在(可能已被刪除)
            if (dt.Rows.Count > 0)
            {
                if (type_dl.SelectedValue == "4") //離職
                {
                    DateTime now = DateTime.Now;
                    string delT = now.ToString("yyyy-MM-dd HH:mm:ss");

                    //update_specialperson(int id, string workerNum, string delT, string checkin, string checkout, string type)
                    //id->1:del 2:update
                    GlobalAnnounce.dataSearching.update_specialperson(1, id2_hf.Value, delT, "", "", "");
                    GlobalAnnounce.dataSearching.update_empResignation(uc_all_employee.getValue(), date_text.Text, Session[SessionString.userID].ToString());
                }
                else
                {
                    //update_specialperson(int id, string workerNum, string delT, string checkin, string checkout, string type)
                    //id->1:del 2:update
                    GlobalAnnounce.dataSearching.update_specialperson(2, id2_hf.Value, "", checkin_dl.SelectedValue, checkout_dl.SelectedValue, type_dl.SelectedValue);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Success!'); window.location.href='HR_specialPerson.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('資料不存在!請重新新增'); window.location.href='HR_specialPerson.aspx';", true);
            }
        }

        protected void insert_btn_Click(object sender, EventArgs e)
        {
            uc_all_employee.setDrop(true);

            Panel1.Visible = true;
            try { uc_all_employee.setText("***"); }
            catch (Exception ee) { }
            type_dl.SelectedIndex = 0;
            dropPanel.Visible = false;
            tbPanel.Visible = false;
            checkin_dl.SelectedIndex = 0;
            checkout_dl.SelectedIndex = 0;
            submitPanel.Visible = true;
            updatePanel.Visible = false;
        }

        //透過RowCommand來取得button的index 再抓取資料
        protected void checkinoutlog_grid_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            #region Delete
            if (e.CommandName == "del")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = spePerson_grid.Rows[index];

                HiddenField idhf = (HiddenField)row.FindControl("id_hf");
                id2_hf.Value = idhf.Value;
                //Label workerNumlab = (Label)row.FindControl("workerNum_lab");
                //string workerNum = workerNumlab.Text;

                DateTime now = DateTime.Now;
                string delT = now.ToString("yyyy-MM-dd HH:mm:ss");

                //update_specialperson(int id, string workerNum, string delT, string checkin, string checkout, string type)
                //id->1:del 2:update
                GlobalAnnounce.dataSearching.update_specialperson(1, id2_hf.Value, delT, "", "", "");
                showDataTable();
            }
            #endregion
            #region Edit
            else if (e.CommandName == "edi")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = spePerson_grid.Rows[index];

                HiddenField idhf = (HiddenField)row.FindControl("id_hf");
                id2_hf.Value = idhf.Value;
                Label workerNumlab = (Label)row.FindControl("workerNum_lab");
                try { uc_all_employee.setText(workerNumlab.Text); }
                catch (Exception ee) { }
                HiddenField typehf = (HiddenField)row.FindControl("type_hf");
                type_dl.SelectedValue = typehf.Value;                

                if (type_dl.SelectedValue == "2")
                {
                    HiddenField checkinhf = (HiddenField)row.FindControl("checkin_hf");
                    checkin_dl.SelectedValue = checkinhf.Value;
                    HiddenField checkouthf = (HiddenField)row.FindControl("checkout_hf");
                    checkout_dl.SelectedValue = checkouthf.Value;
                    dropPanel.Visible = true;
                    tbPanel.Visible = false;
                }
                else
                {
                    dropPanel.Visible = false;
                    tbPanel.Visible = false;
                    checkin_dl.SelectedIndex = 0;
                    checkout_dl.SelectedIndex = 0;
                }

                Panel1.Visible = true;
                submitPanel.Visible = false;
                updatePanel.Visible = true;


                uc_all_employee.setDrop(false);
            }
            #endregion
        }        
    }
}