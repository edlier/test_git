using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace misSystem.check
{
    public partial class check_raa : System.Web.UI.Page
    {
        DateTime dateNow = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                string raarid = Request.QueryString["id"].ToString();
                string audep = GlobalAnnounce.check_raa.getusrdept(Session[SessionString.userID].ToString());
                DataTable dtdep = new DataTable();
                DataTable dtsub = new DataTable();
                dtdep = GlobalAnnounce.check_raa.getdept(raarid);
                dtsub = GlobalAnnounce.check_raa.getsubject(raarid);

                a1.Attributes.Add("href", "overview_raa.aspx?id=" + raarid);

                for (int i = 0; i < dtsub.Columns.Count; i++)
                {
                    if (dtsub.Rows[0][i].ToString() != "")
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.InnerText = dtsub.Rows[0][i].ToString();
                        ul_subject.Controls.Add(li);
                    }
                }

                //for (int i = 0; i < dtdep.Columns.Count; i++)
                //{
                //    if (dtdep.Rows[0][i].ToString() != "0")
                //    {
                //        string deptdes = GlobalAnnounce.check_raa.getdeptdes(dtdep.Rows[0][i].ToString());
                //        ddl_dep.Items.Add(new ListItem(deptdes, dtdep.Rows[0][i].ToString()));
                //    }
                //}

                for (int i = 0; i < dtdep.Columns.Count; i++)
                {
                    if (audep == dtdep.Rows[0][i].ToString())
                    {
                        string deptdes = GlobalAnnounce.check_raa.getdeptdes(dtdep.Rows[0][i].ToString());
                        lbl_dep.Text = deptdes;
                        lbl_depindex.Text = dtdep.Rows[0][i].ToString();
                        break;
                    }
                }
                if(lbl_depindex.Text=="")
                    Response.Redirect("unconfirmed_form.aspx");
            //}
            //catch(Exception ex)
            //{
            //    Response.Write("<script>alert(" + ex + ");</script>");
            //}
            
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string raarid = Request.QueryString["id"].ToString();
            string dep = lbl_depindex.Text;
            string comment = tb_cmt.Text;
            string userid = Session[SessionString.userID].ToString();
            string date = dateNow.ToString("yyyy/MM/dd");
            DataTable Mcheck = new DataTable();
            bool mcheck = true;

            Mcheck = GlobalAnnounce.check_raa.insertComment(raarid, dep, comment, userid, date);
            for (int i = 0; i < Mcheck.Rows.Count; i++)
            {
                if (Mcheck.Rows[i][0].ToString() != "1")
                    mcheck = false;
            }
            if(mcheck)
                GlobalAnnounce.check_raa.updatestatus(Request.QueryString["id"].ToString(), "4");
            Response.Redirect("unconfirmed_form.aspx");
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("unconfirmed_form.aspx");
        }
    }
}