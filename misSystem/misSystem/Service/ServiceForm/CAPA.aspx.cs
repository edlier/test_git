using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class CAPA : System.Web.UI.Page
    {

        DataTable flow = new DataTable();
        DataTable status = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {
                int id;
                id = int.Parse(Request["id"]);

                status = GlobalAnnounce.serviceDB.search_status(id);

                if (status.Rows[0]["IR"].ToString() == "0")
                {

                    IR.Visible = false;
                }
                Label2.Text = "<a href='/Service/Read/Read_ServiceLog.aspx?id=" + id + "'>ServiceLog</a>";
                Label3.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";
                Label4.Text = "<a href='/Service/Read/Read_investigation.aspx?id=" + id + "'>InvestigationReport</a>";
            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='/Service/Status.aspx';</script>");
            }

        }

        protected void btn_set_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = true;
                int id;
                id = int.Parse(Request["id"]);
                string dtc = "null";
                if (dtc1.Checked)
                {
                    dtc = dtc1.Text;
                }
                else if (dtc2.Checked)
                {
                    dtc = dtc2.Text;
                }




                if (describec.Text == "")
                {
                    check = false;
                }
                if (aobrc.Text == "")
                {
                    check = false;
                }
                if (ia1.Text == "" || cd1.Text == "")
                {
                    check = false;
                }


                if ((ia2.Text != "" && cd2.Text == "") || (ia2.Text == "" && cd2.Text != ""))
                {
                    check = false;
                }
                else if (ia2.Text == "" && cd2.Text == "")
                {
                    rd2.Text = "";
                }

                if ((ia3.Text != "" && cd3.Text == "") || (ia3.Text == "" && cd3.Text != ""))
                {
                    check = false;
                }
                else if (ia3.Text == "" && cd3.Text == "")
                {
                    rd3.Text = "";
                }


                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "13")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertcapa(id, dtc, describec.Text, aobrc.Text, ia1.Text, ia2.Text, ia3.Text, rd1.Text, rd2.Text, rd3.Text, cd1.Text, cd2.Text, cd3.Text, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(14, id);



                        Response.Write("<script>alert('CAPA Saved!!');location.href='/Service/Status.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('有資料未填!!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('資料重複!!');</script>");
                }


            }
            catch
            {
                Response.Write("<script>alert('ERROR!!');location.href='/Service/Status.aspx';</script>");
            }

            
        }

        //顯示或隱藏日曆,RequestDate
        protected void imgButtonRequestDate_Click(object sender, ImageClickEventArgs e)
        {
            if (calendarRequestDate.Visible == false)
                calendarRequestDate.Visible = true;
            else
                calendarRequestDate.Visible = false;
        }

        //日期顯示到label,RequestDate
        protected void calendarRequestDate_SelectionChanged(object sender, EventArgs e)
        {

            cd1.Text = String.Format("{0:yyyy-MM-dd}", calendarRequestDate.SelectedDate);
            calendarRequestDate.Visible = false;
        }





        //顯示或隱藏日曆,RequestDate
        protected void imgButtonRequestDate2_Click(object sender, ImageClickEventArgs e)
        {
            if (calendar1.Visible == false)
                calendar1.Visible = true;
            else
                calendar1.Visible = false;
        }

        //日期顯示到label,RequestDate
        protected void calendarRequestDate2_SelectionChanged(object sender, EventArgs e)
        {

            cd2.Text = String.Format("{0:yyyy-MM-dd}", calendar1.SelectedDate);
            calendar1.Visible = false;
        }




        //顯示或隱藏日曆,RequestDate
        protected void imgButtonRequestDate3_Click(object sender, ImageClickEventArgs e)
        {
            if (calendar2.Visible == false)
                calendar2.Visible = true;
            else
                calendar2.Visible = false;
        }

        //日期顯示到label,RequestDate
        protected void calendarRequestDate3_SelectionChanged(object sender, EventArgs e)
        {

            cd3.Text = String.Format("{0:yyyy-MM-dd}", calendar2.SelectedDate);
            calendar2.Visible = false;
        }
    }
}