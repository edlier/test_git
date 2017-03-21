using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.QC
{
    public partial class IQC_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt_IQCList = GlobalAnnounce.QCList.search_IQCList();
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt_IQCList, grid_IQCList);
        }

        protected void grid_IQCList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int i = 0;
                bool result = int.TryParse(e.Row.Cells[7].Text, out i);
                if (result == true && Convert.ToInt32(e.Row.Cells[7].Text)>0)
                {
                    e.Row.Cells[7].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[7].Text + "%</b></span>";
                }
                else
                {
                    e.Row.Cells[7].Text = e.Row.Cells[7].Text + "%";
                }
                if (Convert.ToInt32(e.Row.Cells[5].Text) < Convert.ToInt32(e.Row.Cells[4].Text))
                {
                    e.Row.Cells[5].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[5].Text + "</b></span>";
                }

            }
        }
    }
}