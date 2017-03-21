using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace misSystem.HR
{
    public partial class API_setHolidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //此頁為API，無畫面，用於setHolidays，設定holidyas呼叫

            string old_dates = Request.Form["old_dates"];
            string new_dates = Request.Form["new_dates"];
            string[] old_dates_array = new string[] { };
            string[] new_dates_array = new string[] { };
            if (old_dates != "")
            { old_dates_array = old_dates.Split(','); }
            if (new_dates != "")
            { new_dates_array = new_dates.Split(','); }

            //old_dates為要取消的假日，需於DB中刪除
            string str = "delete from hr_holidays where Date in ('0000-00-00',";
            foreach (string d in old_dates_array)
            {
                DateTime dd = DateTime.Parse(d);
                str += "'" + dd.ToString(@"yyyy\/MM\/dd") + "',";
            }
            str = str.Remove(str.Length - 1) + "); ";
            
            //new_dates為要增加的假日，新增至DB
            str += "insert into hr_holidays(Date) values";
            foreach (string d in new_dates_array)
            {
                DateTime dd = DateTime.Parse(d);
                str += "('" + dd.ToString(@"yyyy\/MM\/dd") + "'),";
            }
            str = str.Remove(str.Length - 1) + ";";

            GlobalAnnounce.db.InsertDataTable(str);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write("{\"Holidays\":\"Save Successfully\"}");
            Response.End(); 
        }
    }
}