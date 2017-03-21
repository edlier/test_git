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
    public partial class overview_raa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string raarid = Request.QueryString["id"].ToString();

                DataTable dtSubject = GlobalAnnounce.check_raa.getsubject(raarid);
                DataTable dtDept = GlobalAnnounce.check_raa.getdept(raarid);
                DataTable dtbasic = GlobalAnnounce.overview.getraabasic(raarid);

                if(dtbasic.Rows.Count>0)
                {
                    lbl_resdept.Text = dtbasic.Rows[0]["resDept"].ToString();
                    if (int.Parse(dtbasic.Rows[0]["fillby"].ToString()) != 0)
                        lbl_wrby.Text = GlobalAnnounce.id.getName(int.Parse(dtbasic.Rows[0]["fillby"].ToString()));
                    if (int.Parse(dtbasic.Rows[0]["managerID"].ToString()) != 0)
                        lbl_mg.Text = GlobalAnnounce.id.getName(int.Parse(dtbasic.Rows[0]["managerID"].ToString()));
                    lbl_no.Text = dtbasic.Rows[0]["raarNo"].ToString();
                }

                if (dtSubject.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSubject.Columns.Count; i++)
                    {
                        if (dtSubject.Rows[0][i].ToString() != "")
                        {
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            li.InnerText = dtSubject.Rows[0][i].ToString();
                            subject.Controls.Add(li);
                        }
                    }
                }

                if (dtDept.Rows.Count > 0)
                {
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
                            DataTable dtComt = GlobalAnnounce.check_raa.getcomment(raarid, dep);
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
                            table1.Rows.Add(tr);
                        }
                    }
                }
            }
        }
    }
}