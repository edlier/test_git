using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.QC
{
    public partial class FQC_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt_fqcList = GlobalAnnounce.QCList.search_FQCList();
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt_fqcList, grid_FQC);

        }
    }
}