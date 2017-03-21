using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace misSystem.QC
{
    public partial class IPQC_Fill : System.Web.UI.Page
    {
        DataTable dt_QFailedReason;
        DataTable dt_QFType; 
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_QFType = GlobalAnnounce.QCList.search_QCFailedType();
            dt_QFailedReason = GlobalAnnounce.QCList.search_QCFReasonList();
            if (!IsPostBack)
            {
                DataTable dt_product = GlobalAnnounce.QCList.search_Product();
                
                GlobalAnnounce.OtherFuction.Drop_SetValueAndText(drop_product, "Id", "Name");
                GlobalAnnounce.OtherFuction.BindDataToDrop(dt_product, drop_product);

            }
        }

        protected void Radio_FOrNot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Radio_FOrNot.SelectedValue == "1")
            {
                GlobalAnnounce.OtherFuction.OpenLabel_VisibelEnabled(lbl_failedR);
                GlobalAnnounce.OtherFuction.OpenDrop_VisibelEnabled(drop_T);

                Panel1.CssClass = "showFuc";
                GlobalAnnounce.OtherFuction.Drop_SetValueAndText(drop_T, "id", "idDes");

                GlobalAnnounce.OtherFuction.BindDataToDrop(dt_QFType, drop_T);
            }
            else
            {
                Panel1.CssClass = "hideFuc";
                GlobalAnnounce.OtherFuction.CloseLabel_VisibelEnabled(lbl_failedR);
                GlobalAnnounce.OtherFuction.Close2Drop_VisibelEnabled(drop_T, drop_R);
            }
        }

        protected void drop_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalAnnounce.OtherFuction.OpenDrop_VisibelEnabled(drop_R);
            GlobalAnnounce.OtherFuction.Drop_SetValueAndText(drop_R, "id", "idDes");

            string expression = " TypeID = " + (((DropDownList)sender).SelectedValue);

            DataView dv = new DataView(dt_QFailedReason);
            dv.RowFilter = expression;
            GlobalAnnounce.OtherFuction.BindDataToDrop(dv, drop_R);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            DataTable dt_validateIPQC = GlobalAnnounce.QCList.search_IPQC_DuplicateSN(tb_SN.Text);
            if (tb_SN.Text == "")
            {
                GlobalAnnounce.OtherFuction.AlertDialog(this.Page, "Please Fill SN!");
            }
            else if (Radio_FOrNot.SelectedValue == "1" && drop_R.SelectedIndex == 0)
            {
                GlobalAnnounce.OtherFuction.AlertDialog(this.Page, "Please Choose Failed Reason!");
            }
            else if (Radio_FOrNot.SelectedValue == "1" && drop_T.SelectedIndex == 0)
            {
                GlobalAnnounce.OtherFuction.AlertDialog(this.Page, "Please Choose Failed Type!");
            }
            else if (drop_product.SelectedIndex == 0)
            {
                GlobalAnnounce.OtherFuction.AlertDialog(this.Page, "Please Choose Product!");
            }

            //Check Duplicate for SN
            else if (dt_validateIPQC.Rows.Count != 0)
            {
                GlobalAnnounce.OtherFuction.AlertDialog(this.Page, "SN is Duplicate! Please check the Data!");
            }
            else
            {
                int x = 0;
                if (Radio_FOrNot.SelectedValue == "1")
                {
                    x = 1;
                }
                else
                {
                    //沒有不良
                    x = 2;
                }
                GlobalAnnounce.QCList.insertIPQC_data(
                    tb_SN.Text,
                    drop_product.SelectedValue,
                    x.ToString(),
                    drop_R.SelectedValue,
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                     Convert.ToInt32(Session[SessionString.userID]).ToString()
                     );
                Response.Redirect(ResolveClientUrl("IPQC_List.aspx"));

            }
        }
    }
}