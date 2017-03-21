using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Drawing;

namespace misSystem.regulatory
{
    public partial class regulatory_form_raar : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userID;
                string NoneMIS_AU = "../Default.aspx";
                string NoneLogin = "../account/login.aspx";
                //GlobalAnnounce.validateSession.validateMIS_AU(this.Page, NoneMIS_AU, NoneLogin);

                //顯示目前使用者
                userID = Convert.ToInt32(Session[SessionString.userID]);
                string writtenby = GlobalAnnounce.id.getName(userID);
                lbl_writtenby.Text = writtenby;

                //顯示目前日期
                DateTime dateNow = DateTime.Now;
                lbl_writtenDate.Text = dateNow.ToString("yyyy/MM/dd");

                //drop_resdept的選擇項
                DataTable dept = new DataTable();
                dept = GlobalAnnounce.regulatory_raar.resdeptOption();
                Depts1.DataSource = dept;
                Depts1.DataBind();
                Depts2.DataSource = dept;
                Depts2.DataBind();
                Depts3.DataSource = dept;
                Depts3.DataBind();
                Depts4.DataSource = dept;
                Depts4.DataBind();
                Depts5.DataSource = dept;
                Depts5.DataBind();
                Depts6.DataSource = dept;
                Depts6.DataBind();

                //顯示使用者的dept
                DataTable dep = GlobalAnnounce.regulatory_raar.getuserdept(userID);
                //string[] deps = GlobalAnnounce.regulatory_raar.getuserdept(userID);
                //if (deps[0] != "" || deps[1] != "")
                //{
                //     drop_resdept.Items.Add(new ListItem(deps[0],"1"));
                //     drop_resdept.Items.Add(new ListItem(deps[1],"2"));
                //}
                drop_resdept.DataSource = dep;
                //drop_resdept.Items.Add(new ListItem(dep.Rows[0]["des"].ToString(), dep.Rows[0]["id"].ToString()));
                drop_resdept.DataBind();

                //drop_fileNo的選擇項
                //drop_fileNo.DataSource = GlobalAnnounce.regulatory_raar.getuser_dcrasNo(userID);
                //drop_fileNo.DataBind();
                //Request("no");
                string no = Request.QueryString["no"];
                if (no != null)
                {
                    if (GlobalAnnounce.regulatory_raar.DCRNoIsExist(no).Rows.Count != 0)
                    {
                        lbl_fileno.Text = no;
                        drop_no.Visible = false;
                        //tb_fileno.Visible = false;
                    }
                    else
                    {
                        Response.Write("<script>alert('error');location.href='regulatory_form_DCR.aspx';</script>");
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "AlertMsg", "<script>alert(\'error assigned no\')</script>");
                        //Response.Redirect("regulatory_form_DCR.aspx");
                    }
                }
                else
                {
                    lbl_fileno.Visible = false;
                    if (GlobalAnnounce.regulatory_raar.getNoneRaarDCRNo().Rows.Count > 0)
                    {
                        drop_no.DataSource = GlobalAnnounce.regulatory_raar.getNoneRaarDCRNo();
                        drop_no.DataBind();
                    }
                    else
                        Response.Write("<script>alert('please fill in DCR');location.href='regulatory_form_DCR.aspx';</script>");
                }
            }
        }

        protected void btn_raar_submint_Click(object sender, EventArgs e)
        {
            string[] subject = new string[5];
            int[] dept = new int[5];
            int usrid = Convert.ToInt32(Session[SessionString.userID]);
            string resdept = drop_resdept.SelectedValue;

            subject[0] = tb_subject1.Text;
            subject[1] = tb_subject2.Text;
            subject[2] = tb_subject3.Text;
            subject[3] = tb_subject4.Text;
            subject[4] = tb_subject5.Text;

            dept[0] = int.Parse(Depts1.SelectedValue);
            dept[1] = int.Parse(Depts2.SelectedValue);
            dept[2] = int.Parse(Depts3.SelectedValue);
            dept[3] = int.Parse(Depts4.SelectedValue);
            dept[4] = int.Parse(Depts5.SelectedValue);


            if (dept[0] == 0)
            {
                lbl_err.Visible = true;
                tbl_dept.Rows[1].BorderColor = Color.Red;
                tbl_dept.Rows[1].BorderWidth = 2;
            }
            else
            {
                tbl_dept.Rows[1].BorderColor = Color.Black;
                tbl_dept.Rows[1].BorderWidth = 0;
            }

            if (subject[0] != null && dept[0] != 0 && resdept != "0")
            {
                int subjectid = GlobalAnnounce.regulatory_raar.insert_raarsubject(subject);
                int deptid = GlobalAnnounce.regulatory_raar.insert_raardept(dept);
                int raarid = GlobalAnnounce.regulatory_raar.insert_raar_form(usrid, subjectid, deptid, lbl_writtenDate.Text, resdept, drop_no.SelectedItem.ToString());

                GlobalAnnounce.regulatory_raar.update_documentchange(raarid, drop_no.SelectedValue);

                foreach (int i in dept)
                {
                    if(i != 0)
                        GlobalAnnounce.regulatory_raar.insert_raardeptcomment(i, raarid);
                }

                Response.Write("<script>alert('success');location.href='../check/unconfirmed_form.aspx'</script>");
            }
            
        }

        //public void setManager(string dept)
        //{
        //    DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(dept));
        //    string[] name = new string[3];
        //    for (int i = 0; i < manager.Rows.Count; i++)
        //    {
        //        name[i] = manager.Rows[i][0].ToString();
        //    }
        //    if (name[0] != null)
        //        lbl_manager1_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
        //    if (name[1] != null)
        //        lbl_manager1_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
        //    if (name[2] != null)
        //        lbl_manager1_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
        //}

        protected void Depts1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(Depts1.SelectedValue) != 0)
            {
                DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(Depts1.SelectedValue));
                string[] name = new string[3];
                for (int i = 0; i < manager.Rows.Count; i++)
                {
                    name[i] = manager.Rows[i][0].ToString();
                }
                if (name[0] != null)
                    lbl_manager1_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
                if (name[1] != null)
                    lbl_manager1_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
                if (name[2] != null)
                    lbl_manager1_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
            }
            else
            {
                lbl_manager1_1.Text = "";
                lbl_manager1_2.Text = "";
                lbl_manager1_3.Text = "";
            }
                
        }

        protected void Depts2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(Depts2.SelectedValue) != 0)
            {
                DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(Depts2.SelectedValue));
                string[] name = new string[3];
                for (int i = 0; i < manager.Rows.Count; i++)
                {
                    name[i] = manager.Rows[i][0].ToString();
                }
                if (name[0] != null)
                    lbl_manager2_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
                if (name[1] != null)
                    lbl_manager2_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
                if (name[2] != null)
                    lbl_manager2_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
            }
            else
            {
                lbl_manager2_1.Text = "";
                lbl_manager2_2.Text = "";
                lbl_manager2_3.Text = "";
            }
        }

        protected void Depts3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(Depts3.SelectedValue) != 0)
            {
                DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(Depts3.SelectedValue));
                string[] name = new string[3];
                for (int i = 0; i < manager.Rows.Count; i++)
                {
                    name[i] = manager.Rows[i][0].ToString();
                }
                if (name[0] != null)
                    lbl_manager3_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
                if (name[1] != null)
                    lbl_manager3_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
                if (name[2] != null)
                    lbl_manager3_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
            }
            else
            {
                lbl_manager3_1.Text = "";
                lbl_manager3_2.Text = "";
                lbl_manager3_3.Text = "";
            }
        }

        protected void Depts4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(Depts4.SelectedValue) != 0)
            {
                DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(Depts4.SelectedValue));
                string[] name = new string[3];
                for (int i = 0; i < manager.Rows.Count; i++)
                {
                    name[i] = manager.Rows[i][0].ToString();
                }
                if (name[0] != null)
                    lbl_manager4_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
                if (name[1] != null)
                    lbl_manager4_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
                if (name[2] != null)
                    lbl_manager4_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
            }
            else
            {
                lbl_manager4_1.Text = "";
                lbl_manager4_2.Text = "";
                lbl_manager4_3.Text = "";
            }
        }

        protected void Depts5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(Depts5.SelectedValue) != 0)
            {
                DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(Depts5.SelectedValue));
                string[] name = new string[3];
                for (int i = 0; i < manager.Rows.Count; i++)
                {
                    name[i] = manager.Rows[i][0].ToString();
                }
                if (name[0] != null)
                    lbl_manager5_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
                if (name[1] != null)
                    lbl_manager5_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
                if (name[2] != null)
                    lbl_manager5_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
            }
            else
            {
                lbl_manager5_1.Text = "";
                lbl_manager5_2.Text = "";
                lbl_manager5_3.Text = "";
            }
        }

        protected void Depts6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(Depts6.SelectedValue) != 0)
            {
                DataTable manager = GlobalAnnounce.regulatory_raar.getdeptManager(int.Parse(Depts6.SelectedValue));
                string[] name = new string[3];
                for (int i = 0; i < manager.Rows.Count; i++)
                {
                    name[i] = manager.Rows[i][0].ToString();
                }
                if (name[0] != null)
                    lbl_manager6_1.Text = GlobalAnnounce.id.getName(int.Parse(name[0]));
                if (name[1] != null)
                    lbl_manager6_2.Text = GlobalAnnounce.id.getName(int.Parse(name[1]));
                if (name[2] != null)
                    lbl_manager6_3.Text = GlobalAnnounce.id.getName(int.Parse(name[2]));
            }
            else
            {
                lbl_manager6_1.Text = "";
                lbl_manager6_2.Text = "";
                lbl_manager6_3.Text = "";
            }
        }
    }
}