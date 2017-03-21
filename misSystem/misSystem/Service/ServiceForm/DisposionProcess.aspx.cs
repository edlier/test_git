using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service
{
    public partial class DisposionProcess : System.Web.UI.Page
    {
        DataTable psn = new DataTable();
        DataTable sff = new DataTable();
        DataTable flow = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../../../Default.aspx";
            string NoneLogin = "../../account/login.aspx";
            GlobalAnnounce.validateSession.validate_AU(this.Page, NoneMIS_AU, NoneLogin,11);

            try
            {
                int id;
                id = int.Parse(Request["id"]);

                psn = GlobalAnnounce.serviceDB.search_slog(id);
                sff = GlobalAnnounce.serviceDB.search_sff(id);

                productSN.Text = psn.Rows[0]["productSN"].ToString();

                servicereport.Text = "<a href='/Service/Read/Read_servicereport.aspx?id=" + id + "'>ServiceReport</a>";

                if (sff.Rows[0]["rrk"].ToString() == "1")
                {
                    rrk1.Checked = true;
                }
                else
                {
                    rrk2.Checked = true;
                }

                if (sff.Rows[0]["rrq"].ToString() == "1")
                {
                    rrq1.Checked = true;
                }
                else
                {
                    rrq2.Checked = true;
                }

                if (sff.Rows[0]["acttr"].ToString() == "1")
                {
                    acttrcheck.Checked = true;
                }


                if (sff.Rows[0]["fr1"].ToString() == "1")
                {
                    fr1.Checked = true;
                }

                if (sff.Rows[0]["fr2"].ToString() == "1")
                {
                    fr2.Checked = true;
                }

                if (sff.Rows[0]["fr3"].ToString() == "1")
                {
                    fr3.Checked = true;
                }

                if (sff.Rows[0]["fr4"].ToString() == "1")
                {
                    fr4.Checked = true;
                }

                if (sff.Rows[0]["fr5"].ToString() == "1")
                {
                    fr5.Checked = true;
                }

                if (sff.Rows[0]["fr6"].ToString() == "1")
                {
                    fr6.Checked = true;

                    fr6other.Text = sff.Rows[0]["fr6other"].ToString();
                }

                if (sff.Rows[0]["fr7"].ToString() == "1")
                {
                    fr7.Checked = true;
                }

                if (sff.Rows[0]["fr8"].ToString() == "1")
                {
                    fr82.Checked = true;
                    fr8dcr.Text = sff.Rows[0]["fr8dcr"].ToString();
                }
                else
                {
                    fr81.Checked = true;
                }

                if (sff.Rows[0]["vp"].ToString() == "1")
                {
                    vp2.Checked = true;
                    vpmsg.Text = sff.Rows[0]["vpmsg"].ToString();
                }
                else
                {
                    vp1.Checked = true;
                }
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

                string dp = "null";
                int cc = 0;

                if (dp1.Checked)
                {
                    dp = dp1.Text;
                }
                else if (dp2.Checked)
                {
                    dp = dp2.Text;
                }
                else if (dp3.Checked)
                {
                    dp = dp3.Text;
                }

                if (cc1.Checked)
                {
                    cc = 1;
                    if (chargeablemsg.Text == "")
                    {
                        check = false;
                    }
                }
                else if (cc2.Checked)
                {
                    cc = 0;
                    chargeablemsg.Text = "";
                }


                if (spi.Text == "" || outdate.Text == "" || tn.Text == "")
                {
                    check = false;
                }





                flow = GlobalAnnounce.serviceDB.search_flow(id);
                if (flow.Rows[0]["flow"].ToString() == "10")
                {
                    if (check == true)
                    {
                        GlobalAnnounce.serviceDB.insertdp(id, dp, outdate.Text, cc, chargeablemsg.Text, spi.Text, tn.Text, Session[SessionString.userID].ToString());
                        GlobalAnnounce.serviceDB.updateflow(11, id);

                        Response.Write("<script>alert('DisposionProcess Saved!!');location.href='/Service/Status.aspx';</script>");
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
        //日期的比較
        private bool compareTwoDate(DateTime selectD, DateTime compareD, int stateInt)//0是選的要大,1是選的要小
        {
            if (compareD == DateTime.MinValue)
                return true;
            else
            {
                if (stateInt == 0 && selectD > compareD)
                    return true;
                else if (stateInt == 1 && selectD < compareD)
                    return true;
                else
                    return false;
            }
        }
        //日期顯示到label,RequestDate
        protected void calendarRequestDate_SelectionChanged(object sender, EventArgs e)
        {

            outdate.Text = String.Format("{0:yyyy-MM-dd}", calendarRequestDate.SelectedDate);
            calendarRequestDate.Visible = false;
        }
    }
}