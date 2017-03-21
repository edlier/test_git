using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class ALLlog : System.Web.UI.Page
    {

        DataTable servicelogList = new DataTable();
        string log;

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = PageListString.oneJumpP;
            string NoneLogin = "../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,4);

            if (!IsPostBack)
            {
                servicelogList = GlobalAnnounce.serviceDB.search_servicelogList();

                
                servicelogList.Columns.Add("status");

                for (int i = 0; i < servicelogList.Rows.Count;i++)
                {
                    if (servicelogList.Rows[i]["flow"].ToString() == "1")
                    {
                        servicelogList.Rows[i]["status"] = "待填log";

                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "2")
                    {
                        servicelogList.Rows[i]["status"] = "Shipping Check";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "3")
                    {
                        servicelogList.Rows[i]["status"] = "品保檢驗";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "4")
                    {
                        servicelogList.Rows[i]["status"] = "Investigation Report(Service)";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "5")
                    {
                        servicelogList.Rows[i]["status"] = "Investigation Report(PE)";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "6")
                    {
                        servicelogList.Rows[i]["status"] = "PE manager reviewed";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "7")
                    {
                        servicelogList.Rows[i]["status"] = "Service Report";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "8")
                    {
                        servicelogList.Rows[i]["status"] = "Service Report 主管確認";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "9")
                    {
                        servicelogList.Rows[i]["status"] = "Service Fill Form";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "10")
                    {
                        servicelogList.Rows[i]["status"] = "Disposition Process";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "11")
                    {
                        servicelogList.Rows[i]["status"] = "Supervisor Check";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "12")
                    {
                        servicelogList.Rows[i]["status"] = "審查";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "13")
                    {
                        servicelogList.Rows[i]["status"] = "填寫CAPA資訊";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "14")
                    {
                        servicelogList.Rows[i]["status"] = "主管審核";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "15")
                    {
                        servicelogList.Rows[i]["status"] = "矯正措施";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "16")
                    {
                        servicelogList.Rows[i]["status"] = "修正失效模式分析";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "17")
                    {
                        servicelogList.Rows[i]["status"] = "驗證";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "18")
                    {
                        servicelogList.Rows[i]["status"] = "結案品保主管確認";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "19")
                    {
                        servicelogList.Rows[i]["status"] = "SRC主席 Close CAPA";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "20")
                    {
                        servicelogList.Rows[i]["status"] = "SRC主席 Close CR";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "21")
                    {
                        servicelogList.Rows[i]["status"] = "結案";
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "132")
                    {
                        servicelogList.Rows[i]["status"] = "(No CAPA)SRC結案審核 ";
                    }
                }
                    
                



                    grid_UserList.DataSource = servicelogList;
                grid_UserList.DataBind();
            }
        }
    }
}