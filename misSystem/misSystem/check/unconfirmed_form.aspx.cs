using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace misSystem.regulatory
{
    public partial class unconfirmed_form : System.Web.UI.Page
    {
        DataTable dt_2 = new DataTable();
        DataTable dt_3 = new DataTable();
        DataTable dt_4 = new DataTable();
        DataTable dt_5 = new DataTable();
        DataTable dt_6 = new DataTable();
        DataTable dt_7 = new DataTable();
        DataTable dt_8 = new DataTable();
        //GridView[] gridview = { (GridView)GridView1 };

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../Default.aspx";
            string NoneLogin = "../account/login.aspx";
            //GlobalAnnounce.validateSession.validateMIS_AU(this.Page, NoneMIS_AU, NoneLogin);

            dt_2 = GlobalAnnounce.unconfirmed.getvalue_dcr(2);
            dt_3 = GlobalAnnounce.unconfirmed.getvalue_raar(3);
            dt_4 = GlobalAnnounce.unconfirmed.getvalue_raar(4);
            dt_5 = GlobalAnnounce.unconfirmed.getvalue_raar(5);
            dt_6 = GlobalAnnounce.unconfirmed.getvalue_dcr(6);
            dt_7 = GlobalAnnounce.unconfirmed.getvalue_dcr(7);
            dt_8 = GlobalAnnounce.unconfirmed.getvalue_dcr(8);

            //GridView1.DataSource = dt_3;
            //GridView1.DataBind();
            //GridView2.DataSource = dt_4;
            //GridView2.DataBind();
            //GridView3.DataSource = dt_5;
            //GridView3.DataBind();
            //GridView4.DataSource = dt_6;
            //GridView4.DataBind();
            //GridView5.DataSource = dt_7;
            //GridView5.DataBind();
            //GridView6.DataSource = dt_8;
            //GridView6.DataBind();

            int[] array1 = {dt_2.Rows.Count,dt_3.Rows.Count,dt_4.Rows.Count};            
            int[] array2 = {dt_6.Rows.Count,dt_7.Rows.Count,dt_8.Rows.Count,dt_5.Rows.Count};

            //View1 Content
            for (int i = 0; i < array1.Max(); i++)
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                TableCell tc2 = new TableCell();
                TableCell tc3 = new TableCell();

                HtmlGenericControl a1 = new HtmlGenericControl("a");
                HtmlGenericControl a2 = new HtmlGenericControl("a");
                HtmlGenericControl a3 = new HtmlGenericControl("a");
                
                try
                { 
                    a1.InnerText = dt_2.Rows[i]["id"].ToString();
                    a1.Attributes.Add("href", "check_raarmg.aspx?id=" + dt_2.Rows[i]["id"].ToString());
                    tc1.Controls.Add(a1);  
                }  
                catch(Exception ee){}
                try
                { 
                    a2.InnerText = dt_3.Rows[i]["id"].ToString();
                    a2.Attributes.Add("href", "check_raa.aspx?id=" + dt_3.Rows[i]["id"].ToString());
                    tc2.Controls.Add(a2);
                }
                catch (Exception ee) { }
                try
                {
                    a3.InnerText = dt_4.Rows[i]["id"].ToString();
                    a3.Attributes.Add("href", "check_raafinal.aspx?id=" + dt_4.Rows[i]["id"].ToString());
                    tc3.Controls.Add(a3);
                }
                catch (Exception ee) { }
                

                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                tr.Cells.Add(tc3);
                

                table1.Rows.Add(tr);
            }

            //View2 Content
            for (int i = 0; i < array2.Max(); i++)
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                TableCell tc2 = new TableCell();
                TableCell tc3 = new TableCell();
                TableCell tc4 = new TableCell();

                HtmlGenericControl a1 = new HtmlGenericControl("a");
                HtmlGenericControl a2 = new HtmlGenericControl("a");
                HtmlGenericControl a3 = new HtmlGenericControl("a");
                HtmlGenericControl a4 = new HtmlGenericControl("a");
                
                try
                {
                    a1.InnerText = dt_5.Rows[i]["id"].ToString();
                    a1.Attributes.Add("href", "check_dcrmg.aspx?id=" + dt_5.Rows[i]["id"].ToString());
                    tc1.Controls.Add(a1);
                }
                catch (Exception ee) { }

                try
                {
                    a2.InnerText = dt_6.Rows[i]["id"].ToString();
                    a2.Attributes.Add("href", "check_pdne.aspx?id=" + dt_6.Rows[i]["id"].ToString());
                    tc2.Controls.Add(a2);
                }
                catch (Exception ee) { }

                try
                {
                    a3.InnerText = dt_7.Rows[i]["id"].ToString();
                    a3.Attributes.Add("href", "check_pcmc.aspx?id=" + dt_7.Rows[i]["id"].ToString());
                    tc3.Controls.Add(a3);
                }
                catch (Exception ee) { }

                try
                {
                    a4.InnerText = dt_8.Rows[i]["id"].ToString();
                    a4.Attributes.Add("href", "check_src.aspx?id=" + dt_8.Rows[i]["id"].ToString());
                    tc4.Controls.Add(a4);
                }
                catch (Exception ee) { }

                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                tr.Cells.Add(tc3);
                tr.Cells.Add(tc4);

                table2.Rows.Add(tr);
            }
        }
        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    dt_3 = GlobalAnnounce.unconfirmed.getvalue_raar(2);
        //    GridView1.DataSource = dt_3;
        //    GridView1.DataBind();
        //}
        //protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView2.PageIndex = e.NewPageIndex;
        //    dt_4 = GlobalAnnounce.unconfirmed.getvalue_raar(3);
        //    GridView2.DataSource = dt_4;
        //    GridView2.DataBind();
        //}
        //protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView3.PageIndex = e.NewPageIndex;
        //    dt_5 = GlobalAnnounce.unconfirmed.getvalue_raar(4);
        //    GridView3.DataSource = dt_5;
        //    GridView3.DataBind();
        //}
        //protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView4.PageIndex = e.NewPageIndex;
        //    dt_6 = GlobalAnnounce.unconfirmed.getvalue_dcr(5);
        //    GridView4.DataSource = dt_6;
        //    GridView4.DataBind();
        //}
        //protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView5.PageIndex = e.NewPageIndex;
        //    dt_7 = GlobalAnnounce.unconfirmed.getvalue_dcr(6);
        //    GridView5.DataSource = dt_7;
        //    GridView5.DataBind();
        //}
        //protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView6.PageIndex = e.NewPageIndex;
        //    dt_8 = GlobalAnnounce.unconfirmed.getvalue_dcr(7);
        //    GridView6.DataSource = dt_8;
        //    GridView6.DataBind();
        //}
    }
}