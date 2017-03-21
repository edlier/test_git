using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.UserControl
{
    public partial class uc_memo_dl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = "select * from hr_leave_reason;";
                HR.HR_class.setDropdownlist(str, "Descript", "ID", memo_dl);
                memo_dl.Items.Insert(0, new ListItem("***"));
                memo_dl.Items.Insert(1, new ListItem("遲到"));
                memo_dl.Items.Insert(2, new ListItem("早退"));
                memo_dl.Items.Insert(3, new ListItem("整天未打卡"));
                memo_dl.Items.Insert(4, new ListItem("上班未打卡"));
                memo_dl.Items.Insert(5, new ListItem("下班未打卡"));
                memo_dl.Items.Insert(6, new ListItem("已彈性補回"));
                memo_dl.Items.Add(new ListItem("外出"));
            }
        }

        public string getText()
        {
            return memo_dl.SelectedItem.Text;
        }

        public string getValue()
        {
            return memo_dl.SelectedItem.Value;
        }
    }
}