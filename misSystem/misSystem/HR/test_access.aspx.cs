using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Newtonsoft.Json;

namespace misSystem.HR
{
    public partial class test_access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //setCheckInOut("5/1/2016 00:00:00", "5/31/2016 23:59:59");
            if (!IsPostBack)
            {

                string str = @"select hr_checkinout_temp_log.time from hr_checkinout_temp_log order by hr_checkinout_temp_log.Id desc limit 1;";
                string last_log = DateTime.Parse((GlobalAnnounce.db.GetDataTable(str)).Rows[0][0].ToString()).ToString(@"yyyy\/MM\/dd H:mm:ss");
                Label2.Text = last_log;               
            }
        }

        int setCheckInOut(string start, string end)
        {
            string result = HR_class.getCheckFromAccess(start, end);
            dynamic data = Newtonsoft.Json.Linq.JValue.Parse(result);

            string sqlstr = "insert into hr_checkinout_temp(WorkerNum, CheckTime, CheckType) values";

            int i = 0;
            foreach (dynamic d in data.CheckInOut)
            {
                i++;
                sqlstr += "(" + d.Badgenumber + ", '" + ((DateTime)(d.CHECKTIME)).ToString(@"yyyy\/MM\/dd H:mm:ss") + "', '" + d.CHECKTYPE + "'),";
                //string str = "WorkerNum:" + d.Badgenumber + ", CheckTime:" + ((DateTime)(d.CHECKTIME)).ToString(@"yyyy\/MM\/dd H:mm:ss") + ", Type:" + d.CHECKTYPE;            
            }
            sqlstr = sqlstr.Remove(sqlstr.Length - 1) + ";";
            sqlstr += "insert into hr_checkinout_temp_log(time, WorkerNum) values('" + end + "', 621);";

            Response.Write(sqlstr);
            //GlobalAnnounce.db.InsertDataTable(sqlstr);
            return i;
        }

        void getAllUser()
        {
            string result = HR_class.getAllUserFromAccess();
            result = result.Replace(@"\u0000", string.Empty).Replace(@"?", string.Empty);
            dynamic data = Newtonsoft.Json.Linq.JValue.Parse(result);

            string sqlstr = "insert into hr_employee_info(WorkerNum, Name) values";

            foreach (dynamic d in data.User)
            {
                sqlstr += "(" + d.Badgenumber + ", '" + d.Name + "'),";
            }
            sqlstr = sqlstr.Remove(sqlstr.Length - 1) + ";";

            Response.Write(sqlstr);
        }

        void getCheck(string date)
        {            
            var str = @"select hr_checkinout_temp.WorkerNum, hr_employee_info.Name, hr_checkinout_temp.CheckTime, hr_checkinout_temp.CheckType
            from hr_checkinout_temp, hr_employee_info
            where hr_checkinout_temp.WorkerNum = hr_employee_info.WorkerNum
            AND Date(hr_checkinout_temp.CheckTime) = '" + date + @"'
            order by hr_checkinout_temp.CheckTime;";
            Debug.WriteLine(str);
            GridView1.DataSource = GlobalAnnounce.db.GetDataTable(str);
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
            string now = DateTime.Now.ToString(@"yyyy\/MM\/dd H:mm:ss");
            Label3.Text = "已更新 " + Label2.Text+" 至 " + now + "，共 " + setCheckInOut(Label2.Text, now) + " 筆記錄";
            Label2.Text = now;
            }
            catch (Exception ee)
            {
                string str = "alert('" + ee.Message.ToString().Replace("'",@"""") + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            getCheck(TextBox1.Text);
        }
    }
}