using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using MisSystem_ClassLibrary.HR.personnel;

namespace misSystem.HR.personnel
{
    public partial class EmpPopupWindow : System.Web.UI.Page
    {
        public static EmployeeDB myEmp = new EmployeeDB();
        private static GridView grid;
        private static string name = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["type"] != null)
                {
                    string ss = Request.QueryString["type"].ToString();
                    DataTable dt;
                    switch (ss)
                    {
                        case "new":
                            break;
                        case "change":
                            break;
                        case "leave":
                            dt = myEmp.search_Leaved();
                            GlobalAnnounce.OtherFuction.BindDataToGrid(dt, grid_leave);
                            grid = grid_leave;
                            name = "離職清單";
                            break;
                        case "birthday":
                            dt = myEmp.search_QuarBirth(int.Parse(Request.QueryString["quar"].ToString()));
                            GlobalAnnounce.OtherFuction.BindDataToGrid(dt, grid_brithday);
                            grid = grid_brithday;
                            name = "壽星清單";
                            break;
                    }
                }
                
            }
        }

        private void download_excel(string text, GridView grid)
        {
            //下載excel
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + text + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //下載excel需override此function

            /*Tell the compiler that the control is rendered
             * explicitly by overriding the VerifyRenderingInServerForm event.*/
        }

        protected void btn_download_Click(object sender, EventArgs e)
        {
            download_excel(name, grid);
        }
    }
}