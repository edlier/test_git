using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace misSystem.UserControl
{
    public partial class uc_all_employee : System.Web.UI.UserControl
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public string getText()
        {
            return all_employee_dl.SelectedItem.Text;
        }

        public string getValue()
        {
            return all_employee_dl.SelectedItem.Value;
        }
        
        public string setValue(string value)
        {
            try
            {
                all_employee_dl.SelectedValue = value;
            }
            catch (Exception ee)
            {
                all_employee_dl.SelectedValue = "***";
            }

            return all_employee_dl.SelectedItem.Value;
        }

        public void setText(string value)
        {
            search_workNum_text.Text = setValue(value);   
        }

        protected void search_workNum_text_TextChanged(object sender, EventArgs e)
        {
            search_workNum_text.Text = setValue(search_workNum_text.Text);   
        }

        protected void all_employee_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            search_workNum_text.Text = getValue();
        }

        public void changeDropdownlist(int i, string time)//i ->>> daily :1, monthly:2, personal & memo:3 
        {
            //string[] tmpT =time.Split('-');//日期 [0]:年 [1]:月份
            //string sql = "select 0 as WorkerNum, 0 as EnFName, 0 as ChiName, '***' as Name, 0 as type";
            //sql += " UNION";
            string sql = "select c.EmpNo, c.EnFName, c.ChiName, CONCAT(c.EmpNo,',',c.ChiName,', ',c.EnFName) as Name, h.type from hr_per_employee c";
            //sql += " join au_userinfo u on(u.WorkerNum=c.WorkerNum)";
            sql += " left join hr_specialperson h on(c.EmpNo=h.workerNum)";
            sql += "";
            //就職日&離職日 1=日期 2.3=月份 
            if (i == 1)
            { sql += " WHERE c.OnboardDT<='" + time + "' AND (c.ResignationDT >= '" + time + "' OR c.ResignationDT<=>NULL)"; }
            else if (i == 2 || i == 3)
            {
                sql += " WHERE (c.OnboardDT<='" + time + "-31' AND (c.ResignationDT >= '" + time + "-1' OR c.ResignationDT <=> NULL) )";
            }
            //是否出現不用打卡    
            if (i == 1) { sql += " AND ((h.type = 2) OR (h.type = 1 AND h.isDel ='O') OR (h.type = 3)   OR h.type<=>NULL)"; }
            else if (i == 2) { sql += " AND ((h.type = 2) OR (h.type = 1 AND h.isDel ='O') OR (h.type = 3 AND h.isDel ='O')   OR h.type<=>NULL)"; }
            sql += " GROUP BY EmpNo ORDER BY EmpNo ASC";
            System.Data.DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            GlobalAnnounce.OtherFuction.Drop_SetValueAndText(all_employee_dl, "EmpNo", "Name");
            GlobalAnnounce.OtherFuction.BindDataToDrop(dt, all_employee_dl, "***");
			//all_employee_dl.DataSource = dt;
            //all_employee_dl.DataValueField = "WorkerNum";
            //all_employee_dl.DataTextField = "Name";
            //all_employee_dl.DataBind();

            search_workNum_text.Text = getValue();
        }

        public void setDrop(Boolean b)
        {
            all_employee_dl.Enabled = b;
            search_workNum_text.Enabled = b;
        }

        public void setOriDropDownlist()
        {
            string sql = "select c.EmpNo, c.EnFName, c.ChiName, CONCAT(c.EmpNo,',',c.ChiName,', ',c.EnFName) as Name from hr_per_employee c"
                + " WHERE c.Status='ONSERVICE' AND c.ResignationDT <=> NULL  ORDER BY EmpNo ASC;";
            System.Data.DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            GlobalAnnounce.OtherFuction.Drop_SetValueAndText(all_employee_dl, "EmpNo", "Name");
            GlobalAnnounce.OtherFuction.BindDataToDrop(dt, all_employee_dl, "***");
            //all_employee_dl.DataSource = dt;
            //all_employee_dl.DataValueField = "WorkerNum";
            //all_employee_dl.DataTextField = "Name";
            //all_employee_dl.DataBind();

            search_workNum_text.Text = getValue(); 
        }

    }
}