using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.check
{
    public partial class check_pcmc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void imgbtn_rsdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (date_rsselect.Visible == false)
        //        date_rsselect.Visible = true;
        //    else
        //        date_rsselect.Visible = false;
        //}

        //protected void date_rsselect_SelectionChanged(object sender, EventArgs e)
        //{
        //    lbl_rsdate.Text = String.Format("{0:yyyy-MM-dd}", date_rsselect.SelectedDate);
        //    date_rsselect.Visible = false;
        //}

        //protected void imgbtn_usdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (date_usselect.Visible == false)
        //        date_usselect.Visible = true;
        //    else
        //        date_usselect.Visible = false;
        //}

        //protected void date_usselect_SelectionChanged(object sender, EventArgs e)
        //{
        //    lbl_usdate.Text = String.Format("{0:yyyy-MM-dd}", date_usselect.SelectedDate);
        //    date_usselect.Visible = false;
        //}
        //protected void imgbtn_drpdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (date_drpselect.Visible == false)
        //        date_drpselect.Visible = true;
        //    else
        //        date_drpselect.Visible = false;
        //}

        //protected void date_drpselect_SelectionChanged(object sender, EventArgs e)
        //{
        //    lbl_drpdate.Text = String.Format("{0:yyyy-MM-dd}", date_drpselect.SelectedDate);
        //    date_drpselect.Visible = false;
        //}

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            int qpHand = int.Parse(tb_qpHand.Text);
            int qpOrder = int.Parse(tb_qpOrder.Text);
            int qsHand = int.Parse(tb_qsHand.Text);
            int qsOrder = int.Parse(tb_qsOrder.Text);
            int qfHand = int.Parse(tb_qfHand.Text);
            int qfOrder = int.Parse(tb_qfOrder.Text);
            int stock = int.Parse(rdlist_stock.SelectedValue);
            int revisestock = 0;
            int usedstock = 0;
            int deplete = 0;
            string revisestock_date = "";
            string usedstock_date = "";
            string deplete_date = "";

            switch(stock)
            {
                case 0:
                    revisestock = 1;
                    revisestock_date = lbl_stockdate.Text;
                    break;
                case 1:
                    usedstock = 1;
                    usedstock_date = lbl_stockdate.Text;
                    break;
                case 2:
                    deplete = 1;
                    deplete_date = lbl_stockdate.Text;
                    break;
            }

            int pcmcmid = int.Parse(Session[SessionString.userID].ToString());
            DateTime dateNow = DateTime.Now;
            string datenow = dateNow.ToString("yyyy/MM/dd");
            //int srStock,
            //string srStockdate,
            //int suStock,
            //string suStockdate,
            //int sdrp,
            //string sdrpdate,
            //int pcmcMid,
            //int pcmcMcheck,
            //string pcmcMcheckdate
            DataTable pcmcid = GlobalAnnounce.check_dcr.insertpcmc(qpHand,qpOrder,qsHand,qsOrder,qfHand,qfOrder,revisestock,revisestock_date,usedstock,usedstock_date,deplete,deplete_date,pcmcmid,1,datenow);
            GlobalAnnounce.check_dcr.update_pcmcid(pcmcid.Rows[0][0].ToString(), Request.QueryString["id"].ToString());
            GlobalAnnounce.check_dcr.updatestatus(Request.QueryString["id"].ToString(), "8");
            Response.Redirect("unconfirmed_form.aspx");
        }

        protected void rdlist_stock_SelectedIndexChanged(object sender, EventArgs e)
        {

            imgbtn_stockdate.Visible = true;
        }

        protected void cl_stockdate_SelectionChanged(object sender, EventArgs e)
        {
            lbl_stockdate.Text = String.Format("{0:yyyy-MM-dd}", cl_stockdate.SelectedDate);
            cl_stockdate.Visible = false;
        }

        protected void imgbtn_stockdate_Click(object sender, ImageClickEventArgs e)
        {
            if (cl_stockdate.Visible == false)
                cl_stockdate.Visible = true;
            else
                cl_stockdate.Visible = false;
        }
    }
}