using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class Check_redress : System.Web.UI.Page
    {

        DataTable capa = new DataTable();
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

                ID.Text = "ServiceLogID :" + id;
                log.Text = "<a href='/Service/Read/Read_ServiceLog.aspx?id=" + id + "'>ServiceLog</a>";
                sr.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";
                igr.Text = "<a href='/Service/Read/Read_investigation.aspx?id=" + id + "'>InvestigationReport</a>";

                capa = GlobalAnnounce.serviceDB.search_capa(id);

                ID.Text = "ServiceLogID :" + capa.Rows[0]["servicelogID"].ToString();

                if (capa.Rows[0]["dtctype"].ToString() == "Potential")
                {
                    dtc1.Checked = true;
                }
                else
                {
                    dtc2.Checked = true;
                }

                describec.Text = capa.Rows[0]["dtc"].ToString();
                aobrc.Text = capa.Rows[0]["aobrc"].ToString();

                ia1.Text = capa.Rows[0]["ia1"].ToString();
                ia2.Text = capa.Rows[0]["ia2"].ToString();
                ia3.Text = capa.Rows[0]["ia3"].ToString();

                rrd1.Text = capa.Rows[0]["rd1"].ToString();
                rrd2.Text = capa.Rows[0]["rd2"].ToString();
                rrd3.Text = capa.Rows[0]["rd3"].ToString();

                ccd1.Text = capa.Rows[0]["cd1"].ToString();
                ccd2.Text = capa.Rows[0]["cd2"].ToString();
                ccd3.Text = capa.Rows[0]["cd3"].ToString();
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
                int id;
                id = int.Parse(Request["id"]);

                bool check = false;

                if(cd1.Text != "")
                {
                    check = true;
                }

                if (ap1.Text == "" || cd1.Text == "")
                {
                    check = false;
                }


                if ((ap2.Text != "" && cd2.Text == "") || (ap2.Text == "" && cd2.Text != ""))
                {
                    check = false;
                }
                else if (ap2.Text == "" && cd2.Text == "")
                {
                    rd2.Text = "";
                }

                if ((ap3.Text != "" && cd3.Text == "") || (ap3.Text == "" && cd3.Text != ""))
                {
                    check = false;
                }
                else if (ap3.Text == "" && cd3.Text == "")
                {
                    rd3.Text = "";
                }






                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "15" && check == true)
                {
                    GlobalAnnounce.serviceDB.insertredress(id, ap1.Text, ap2.Text, ap3.Text, rd1.Text, rd2.Text, rd3.Text, cd1.Text, cd2.Text, cd3.Text);

                    GlobalAnnounce.serviceDB.updateflow(16, id);

                    Response.Write("<script>alert('Checked!!');location.href='/Service/Status.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('有資料未填!!');</script>");
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