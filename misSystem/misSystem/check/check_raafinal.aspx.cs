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
    public partial class check_raafinal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string raarid = Request.QueryString["id"].ToString();

            DataTable dtSubject = GlobalAnnounce.check_raa.getsubject(raarid);
            DataTable dtDept = GlobalAnnounce.check_raa.getdept(raarid);
            
            for (int i = 0; i < dtSubject.Columns.Count; i++)
            {
                if (dtSubject.Rows[0][i].ToString() != "")
                {  
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.InnerText = dtSubject.Rows[0][i].ToString();
                    ul_subject.Controls.Add(li);
                }
            }

            for (int i = 0; i < dtDept.Columns.Count; i++)
            {
                if (dtDept.Rows[0][i].ToString() != "0")
                {
                    TableRow tr = new TableRow();
                    TableCell tc1 = new TableCell();
                    TableCell tc2 = new TableCell();
                    TableCell tc3 = new TableCell();

                    tc1.Text = GlobalAnnounce.check_raa.getdeptdes(dtDept.Rows[0][i].ToString());
                    string dep = dtDept.Rows[0][i].ToString();
                    DataTable dtComt= GlobalAnnounce.check_raa.getcomment(raarid,dep);
                    GlobalAnnounce.unconfirmed.dateform(dtComt, "checkdate");

                    HtmlGenericControl ulc = new HtmlGenericControl("ul");
                    HtmlGenericControl uls = new HtmlGenericControl("ul");

                    if (dtComt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtComt.Rows.Count; j++)
                        {
                            HtmlGenericControl lic = new HtmlGenericControl("li");
                            lic.InnerText = dtComt.Rows[j][0].ToString();
                            ulc.Controls.Add(lic);
                            tc2.Controls.Add(ulc);
                            HtmlGenericControl lis = new HtmlGenericControl("li");
                            lis.InnerText = GlobalAnnounce.id.getName(int.Parse(dtComt.Rows[j][1].ToString())) + " , " + dtComt.Rows[j][3].ToString();
                            uls.Controls.Add(lis);
                            tc3.Controls.Add(uls);
                        }
                    }

                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tc2);
                    tr.Cells.Add(tc3);
                    tb1.Rows.Add(tr);
                }
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            string date = dateNow.ToString("yyyy/MM/dd");
            if (tb_comment.Text != "")
            {
                GlobalAnnounce.check_raa.updateConclusion(tb_comment.Text, date, Request.QueryString["id"].ToString());
                GlobalAnnounce.check_raa.updatestatus(Request.QueryString["id"].ToString(), "5");
                Response.Redirect("unconfirmed_form.aspx");
            }
            else
            {
                Response.Write("<script>alert('there has somethig you have answer')</script>");
            }
        }
    }
}