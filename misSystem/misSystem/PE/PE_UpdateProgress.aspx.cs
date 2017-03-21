using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.PE
{
    public partial class PE_UpdateProgress : System.Web.UI.Page
    {
        string catchId;
        protected void Page_Load(object sender, EventArgs e)
        {
            string NonePE_AU = "../JumpPage.aspx";
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NonePE_AU, NoneLogin,3);
            lbl_ShowTitle.Text = get_TaskName();
            lbl_minprog.Text = get_TaskProgress();
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            catchId = Request.Url.Query.Substring(4);
            //Response.Write(catchId); 
            get_descriptions();
            get_projectoutput();
        }
        private void get_descriptions()
        {
            string sqlstr = "select descriptionname"
                + " from  pe_descriptions"
                + " where taskID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lb_descriptions.Items.Add((i + 1).ToString() + ". " + (dt.Rows[i][0]).ToString());
            }
        }
        private void get_projectoutput()
        {
            string sqlstr = "select projectoutput"
                + " from  pe_projectoutput"
                + " where taskID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lb_enditems.Items.Add((i + 1).ToString() + ". " + (dt.Rows[i][0]).ToString());
            }
        }
        private string get_TaskName()
        {
            string sqlstr = "select itemname"
                + " from  pe_tasks"
                + " where ID="
                  + GlobalAnnounce.db.qo(catchId)
                  + " ";
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            string name = "";
            name = dt.Rows[0]["itemname"].ToString();
            return name;
        }
        private string get_TaskProgress()
        {
            string prognum = "";
            string sqlstr = "select  max(p.progress) as currentprog"
                + " from pe_progressdetail as p"
                + " where  p.taskID="
                  + GlobalAnnounce.db.qo(catchId);
            DataTable dt = new DataTable();
            dt = GlobalAnnounce.db.GetDataTable(sqlstr);
            prognum = dt.Rows[0]["currentprog"].ToString();
            return prognum;
        }
        protected void bt_messageclr_Click(object sender, EventArgs e)
        {
            if (lb_message.SelectedIndex != -1)
            {
                lb_message.Items.RemoveAt(lb_message.SelectedIndex);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('點選已新增的訊息列，進行編輯或移除！ Select a message to edit or remove!');", true);
            }
        }

        protected void bt_addmessage_Click(object sender, EventArgs e)
        {
            if (tb_message.Text == "" || tb_message.Text == " ")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('請輸入工作內容！ Please enter a description!');", true);
            }
            else
            {
                if (lb_message.Items.Count < 5)
                {
                    lb_message.Items.Add(tb_message.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('最多新增五筆 Maximum: 5');", true);
                }
            }
            tb_message.Text = "";
        }

        protected void bt_submit_Click(object sender, EventArgs e)
        {
            int prognumck = 0;
            int minprog = 0;
            string msg = "";
            bool inputchk = false;
            int.TryParse(tb_progress.Text, out prognumck);
            int.TryParse(lbl_minprog.Text, out minprog);
            if (prognumck <= minprog)
            {
                msg += "輸入的進度值要大於：" + minprog;
            }
            else if (lb_message.Items.Count == 0)
            {
                msg += "請輸入進度內容！ Please enter a message!";
            }
            else
            {
                inputchk = true;
            }
            if (inputchk)
            {
                string sqlstr = "insert into pe_progressdetail(taskID,date,progress,message";
              
                    for (int i = 2; i <= 5; i++)
                    {
                        sqlstr += ",message" + i;
                    }
                
                sqlstr += ") values("
                + GlobalAnnounce.db.qo(catchId) + ","
                + "NOW(),"
                + GlobalAnnounce.db.qo(tb_progress.Text);
                for (int j = 0; j < lb_message.Items.Count; j++)
                {
                    sqlstr += ", " + GlobalAnnounce.db.qo(lb_message.Items[j].ToString());
                }
                if (lb_message.Items.Count < 5)
                {
                    for (int k = lb_message.Items.Count; k < 5; k++)
                    {
                        sqlstr += ", " + GlobalAnnounce.db.qo("");
                    }
                }
                sqlstr += ")";

                GlobalAnnounce.db.InsertDataTable(sqlstr);
                int prognum = 0;
                int.TryParse(tb_progress.Text, out prognum);
                if (prognum > 0 && prognum < 100)
                {
                    sqlstr = "update pe_tasks set status='1' where ID="
                      + GlobalAnnounce.db.qo(catchId);
                }
                else if (prognum == 100)
                {
                    sqlstr = "update pe_tasks set status='2' where ID="
                     + GlobalAnnounce.db.qo(catchId);
                }
                GlobalAnnounce.db.InsertDataTable(sqlstr);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('操作完成！ Done!');", true);
                Response.Redirect("PE_ItemList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + msg + "');", true);
            }
        }

        protected void bt_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PE_ItemList.aspx");
        }

        protected void lb_message_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_message.Text = lb_message.SelectedValue;
         
        }

        protected void lb_descriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_descriptions.Text = lb_descriptions.SelectedValue;
        }

        protected void lb_enditems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_enditems.Text = lb_enditems.SelectedValue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int ch = lb_message.SelectedIndex;
            int lbindex = lb_message.Items.Count;
            string[] msg = new string[5];


            if (tb_message.Text != "" && lb_message.Items.Count != 0 && lb_message.SelectedIndex != -1)
            {
                for (int i = 0; i < lbindex; i++)
                {
                    msg[i] = lb_message.Items[i].ToString();
                }

                lb_message.Items.Clear();
                msg[ch] = tb_message.Text;
                for (int i = 0; i < lbindex; i++)
                {
                    lb_message.Items.Add(msg[i]);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('點選已新增的訊息列，進行編輯或移除！ Select a message to edit or remove!');", true);
            }
        }
    }
}