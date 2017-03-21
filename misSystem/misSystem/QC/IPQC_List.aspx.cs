using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace misSystem.QC
{
    public partial class IPQC_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt_ipqcList = GlobalAnnounce.QCList.search_IPQCList();
            GlobalAnnounce.OtherFuction.BindDataToGrid(dt_ipqcList, grid_IPQC);

        }
    }
}