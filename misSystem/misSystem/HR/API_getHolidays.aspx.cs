using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace misSystem.HR
{
    public partial class API_getHolidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //此頁為API，無畫面，用於setHolidays時呼叫，回傳已設定的holidays

            string y = Request.Form["year"].Trim();
            string str = "select DATE_FORMAT(Date,'%m/%d/%Y') as Date from HR_holidays where Year(Date) = '" + y + "';";
            string sJson = JsonConvert.SerializeObject(GlobalAnnounce.db.GetDataTable(str));

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write("{\"Holidays\":" + sJson + "}");
            Response.End(); 
        }
    }
}