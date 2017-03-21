using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


// 有保留DROP的選擇，快速輸入的時候可以更改 註解使用
// 另外一種需要直接輸入時間來INSERT


namespace misSystem.HR
{
	public partial class HR_QuicklyImportDT : System.Web.UI.Page
	{
		DataTable dt_statusList;
		protected void Page_Load(object sender, EventArgs e)
		{
			//dt_statusList = GlobalAnnounce.dataSearching.search_leaveReasonList();
			if (!Page.IsPostBack)
			{
				drop_leaveReason.DataSource = GlobalAnnounce.dataSearching.search_leaveReasonList();
				drop_leaveReason.DataBind();

				uc_all_employee.setOriDropDownlist();
                string Year = DateTime.Now.ToString("yyyy");
                int thisYearInt = Convert.ToInt32(Year);
                int lastYearInt = thisYearInt -1;
				//drop_year.
                drop_year.Items.Add(new ListItem(Year, Year));
                drop_year.Items.Add(new ListItem(lastYearInt.ToString(), lastYearInt.ToString()));


				try
				{
					if (Request.QueryString["wn"] != null) //若get值存在，需顯示指定人員及時間的資料
					{
						string date = Request.QueryString["date"];
						int m = int.Parse((date.Split('-'))[1]), d = int.Parse((date.Split('-'))[2]);
						tb_AMonth.Text = m.ToString();
						tb_BMonth.Text = m.ToString();
						tb_ADay.Text = d.ToString();
						tb_BDay.Text = d.ToString();

						string workerNum = HR_class.decodeBase64(Request.QueryString["wn"]);

						try { uc_all_employee.setText(workerNum); }
						catch (Exception ee) { }                        
					}
					else
					{
						string Month = DateTime.Now.ToString("MM");
						tb_AMonth.Text = Month;
						tb_BMonth.Text = Month;
					}
				}
				catch (Exception ee)
				{
					Response.Redirect("HR_QuicklyImportDT.aspx"); 
				}

			}
			//GlobalAnnounce.OtherFuction.BindDataToDrop(dt_statusList, drop_leaveReason);
		}

		protected void btn_submit_Click(object sender, EventArgs e)
		{
			//string stratTime;
			//if(drop_time.SelectedValue=="0"){
			//    stratTime=" 08:30:00";
			//}
			//else{
			//    stratTime=" 13:00:00";
			//}
			if (tb_startTime.Text == "") { tb_startTime.Text = "08:30"; }
			if (tb_endTime.Text == "") { tb_endTime.Text = "17:30"; }


			//string endDateTime = "2016-" + tb_AMonth.Text + "-" + tb_ADay.Text + " 17:30:00";
            string Year = drop_year.SelectedValue.ToString();

            string endDateTime = Year + "-" + tb_BMonth.Text + "-" + tb_BDay.Text + " " + tb_endTime.Text + ":00";
			//string startDateTime = "2016-" + tb_AMonth.Text + "-" + tb_ADay.Text + stratTime;
            string startDateTime = Year + "-" + tb_AMonth.Text + "-" + tb_ADay.Text + " " + tb_startTime.Text + ":00";
			GlobalAnnounce.dataSearching.insert_leaveData(uc_all_employee.getValue(), drop_leaveReason.SelectedValue, startDateTime, endDateTime,tb_reason.Text);
			Response.Redirect("HR_QuicklyImportDT.aspx");
		}

		protected void tb_ADay_TextChanged(object sender, EventArgs e)
		{
			tb_BDay.Text = tb_ADay.Text;
		}

        protected void tb_AMonth_TextChanged(object sender, EventArgs e)
        {
            tb_BMonth.Text = tb_AMonth.Text;
        }
	}
}