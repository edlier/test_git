using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.PE
{
    public partial class PE_AddaNewItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NonePE_AU = "../JumpPage.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NonePE_AU, NoneLogin, 3);
            //int []arr={3,4};
            //GlobalAnnounce.validateSession.validate_AU(this.Page, NonePE_AU, NoneLogin, arr);
        }

        protected void bt_submit_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool inputchk = false;
            if (tb_title.Text != "")
            {
                msg += "請輸入工作項目! Please enter a title!";
            }
            else if (tbddate.Text != "")
            {
                msg += "請輸入預計完成日期！ Please enter a due date!";
            }
            else if (lb_taskdesp.Items.Count != 0)
            {
                msg += "請輸入工作內容！ Please enter a task description!";
            }
            else if (lb_enditem.Items.Count != 0)
            {
                msg += "請輸入工作產出！ Please enter a project output!";
            }
            else
            {
                inputchk = true;
            }
            if (inputchk)
            {
                string sqlstr = "insert into pe_tasks(itemname,duedate,owner,status)"
               + "values("
               + GlobalAnnounce.db.qo(tb_title.Text) + ","
               + GlobalAnnounce.db.qo(tbddate.Text) + ","
                + GlobalAnnounce.db.qo(Session[SessionString.userID].ToString()) + ","
                + "'0')";

                GlobalAnnounce.db.InsertDataTable(sqlstr);

                for (int i = 0; i < lb_taskdesp.Items.Count; i++)
                {
                    sqlstr = "insert into pe_descriptions(taskID,descriptionname)"
                        + "values("
                        + GlobalAnnounce.db.qo(getTaskID()) + ","
                        + GlobalAnnounce.db.qo(lb_taskdesp.Items[i].ToString())
                        + ")";
                    GlobalAnnounce.db.InsertDataTable(sqlstr);
                }

                for (int i = 0; i < lb_enditem.Items.Count; i++)
                {
                    sqlstr = "insert into pe_projectoutput(taskID,projectoutput)"
                        + "values("
                        + GlobalAnnounce.db.qo(getTaskID()) + ","
                        + GlobalAnnounce.db.qo(lb_enditem.Items[i].ToString())
                        + ")";
                    GlobalAnnounce.db.InsertDataTable(sqlstr);
                }

                sqlstr = "insert into pe_progressdetail(taskID,date,progress,message,message2,message3,message4,message5)"
             + "values("
             + GlobalAnnounce.db.qo(getTaskID()) + ","
             + "NOW(),"
             + "'0',"
              + "'Created','','','','')";

                GlobalAnnounce.db.InsertDataTable(sqlstr);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('操作完成！ Done!');", true);
                tb_title.Text = "";
                tbddate.Text = "";
                lb_taskdesp.Items.Clear();
                lb_enditem.Items.Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請輸入工作內容！ Please enter a description!');", true);
            }
        }

        private string getTaskID()
        {
            string sqlstr = "select * from pe_tasks where itemname="
                + GlobalAnnounce.db.qo(tb_title.Text);
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            string taskID = "";
            try
            {
                taskID = dt.Rows[0]["ID"].ToString();
            }
            catch (Exception ex)
            {

            }
            return taskID;
        }

        protected void bt_adddescription_Click(object sender, EventArgs e)
        {
            if (tb_desp.Text == "" || tb_desp.Text == " ")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請輸入工作內容！ Please enter a description!');", true);
            }
            else
            {
                if (lb_taskdesp.Items.Count < 5)
                {
                    lb_taskdesp.Items.Add(tb_desp.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('最多新增五筆 Maximum: 5');", true);
                }
            }
            tb_desp.Text = "";
        }

        protected void bt_taskdespclr_Click(object sender, EventArgs e)
        {
            if (lb_taskdesp.SelectedIndex != -1)
            {
                lb_taskdesp.Items.RemoveAt(lb_taskdesp.SelectedIndex);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('點選已新增的訊息列，進行編輯或移除！ Select a message to edit or remove!');", true);
            }
        }

        protected void bt_addenditem_Click(object sender, EventArgs e)
        {
            if (tb_projoutput.Text == "" || tb_projoutput.Text == " ")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請輸入工作內容！ Please enter a description!');", true);
            }
            else
            {
                if (lb_enditem.Items.Count < 5)
                {
                    lb_enditem.Items.Add(tb_projoutput.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('最多新增五筆 Maximum: 5');", true);
                }
            }
            tb_projoutput.Text = "";
        }

        protected void bt_enditemclr_Click(object sender, EventArgs e)
        {
            if (lb_enditem.SelectedIndex != -1)
            {
                lb_enditem.Items.RemoveAt(lb_enditem.SelectedIndex);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('點選已新增的訊息列，進行編輯或移除！ Select a message to edit or remove!');", true);
            }
        }

        protected void bt_list_Click(object sender, EventArgs e)
        {
            Response.Redirect("PE_ItemList.aspx");
        }

        protected void lb_enditem_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_projoutput.Text = lb_enditem.SelectedValue;
        }

        protected void lb_taskdesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_desp.Text = lb_taskdesp.SelectedValue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int ch = lb_taskdesp.SelectedIndex;
            int lbindex = lb_taskdesp.Items.Count;
            string[] msg = new string[5];


            if (tb_desp.Text != "" && lb_taskdesp.Items.Count != 0 && lb_taskdesp.SelectedIndex!=-1)
            {
                for (int i = 0; i < lbindex; i++)
                {
                    msg[i] = lb_taskdesp.Items[i].ToString();
                }

                lb_taskdesp.Items.Clear();
                msg[ch] = tb_desp.Text;
                for (int i = 0; i < lbindex; i++)
                {
                    lb_taskdesp.Items.Add(msg[i]);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('點選已新增的訊息列，進行編輯或移除！ Select a message to edit or remove!');", true);
            }
        }

        protected void bt_editenditem_Click(object sender, EventArgs e)
        {
            int ch = lb_enditem.SelectedIndex;
            int lbindex =lb_enditem.Items.Count;
            string[] msg = new string[5];
           
            if (tb_projoutput.Text != "" && lb_enditem.Items.Count != 0 && lb_enditem.SelectedIndex!=-1)
            {
                for (int i = 0; i < lbindex; i++)
                {
                    msg[i] = lb_enditem.Items[i].ToString();
                }

                lb_enditem.Items.Clear();
                msg[ch] = tb_projoutput.Text;
                for (int i = 0; i < lbindex; i++)
                {
                    lb_enditem.Items.Add(msg[i]);
                }
            }else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('點選已新增的訊息列，進行編輯或移除！ Select a message to edit or remove!');", true);
            }
        }
    }
}