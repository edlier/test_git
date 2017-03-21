using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;
using System.IO;
using MisSystem_ClassLibrary.HR.personnel;
using MisSystem_ClassLibrary.HR;


namespace misSystem.HR
{
    public partial class HR_checkReport_personal : System.Web.UI.Page
    {
        DataTable dt_leave;
        private static DeptDB myDept = new DeptDB();
        private static SysSetting mySysS = new SysSetting();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HR_class.setSysTime();

                DateTime now = DateTime.Now;
                int now_year = int.Parse(now.Year.ToString());
                int now_month = int.Parse(now.Month.ToString());

                for (int i = now_year - 1; i <= (now_year); i++) //前一年至後三年
                {
                    year_dl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                for (int i = 1; i <= 12; i++)
                {
                    month_dl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                year_dl.SelectedValue = now_year.ToString();
                month_dl.SelectedValue = now_month.ToString();

                string sql = "";

                if (Session[SessionString.userID] != null)
                {
                    sql = "select c.ChiName,c.EnFName, c.DeptID, m.level, m.DepID from hr_per_employee c left join au_managerlist m on(c.EmpPID=m.userID) where c.EmpPID=" + Session[SessionString.userID].ToString();
                    DataTable dt=GlobalAnnounce.db.GetDataTable(sql);
                    name_lab.Text = dt.Rows[0][0].ToString();
                    DataRow[] foundRows;
                    foundRows = dt.Select("level = 3 AND DepID = 5");

                    if (foundRows.Length>0)
                    { 
                        //有權限作彈性補回
                    }
                    else { checkinout_grid.Columns[10].Visible = false; }
                    
                }

                uc_all_employee.changeDropdownlist(3, now_year + "-" + now_month);//day:1, month:2, personal & memo:3

                try
                {
                    if (Request.QueryString["wn"] != null) //若get值存在，需顯示指定人員及時間的資料
                    {
                        year_dl.SelectedValue = Request.QueryString["y"];
                        month_dl.SelectedValue = Request.QueryString["m"];

                        string workerNum = HR_class.decodeBase64(Request.QueryString["wn"]); 
                        
                        try { uc_all_employee.setText(workerNum); }
                        catch (Exception ee) { }

                        searchCheck(workerNum);
                    }
                }
                catch(Exception ee)
                { Response.Redirect("HR_checkReport_day.aspx"); }

                coculateMonthDay();
            }
        }

        void searchCheck(string workerNum)
        {
            if (workerNum == "***") //需選擇一名員工
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('請選擇員工');", true);
                return;
            }

            //string sql = "select c.ChiName,c.EnLName, c.DeptID from hr_per_employee c where c.EmpNo=" + workerNum + ";";
            workNum_lab.Text = workerNum;
            //DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            DataTable dt = GlobalAnnounce.dataSearching.search_worker(workerNum);
            search_name_lab.Text = dt.Rows[0][0].ToString();
            depID_lab.Text = dt.Rows[0][2].ToString();

            //sql = "select a.WorkerNum, a.EnFName, a.ChiName, c.CheckTime, c.CheckType from au_computerform a inner join hr_checkinout c "
            //    + "on (a.WorkerNum=c.WorkerNum and "
            //    +"year(c.CheckTime)='" + year_dl.SelectedValue + "' and + month(c.CheckTime)='"+month_dl.SelectedValue+"' and "
            //    + "c.WorkerNum=" + workerNum + " and isDel='X');";

            DataTable dt_checkinout = GlobalAnnounce.dataSearching.search_PersonalCheckinout(year_dl.SelectedValue, month_dl.SelectedValue, workerNum);
            dt_leave = GlobalAnnounce.dataSearching.search_PersonalLeave(year_dl.SelectedValue, month_dl.SelectedValue, workerNum);
            List<checkData> checkData = new List<checkData>();

            checkData = HR_class.getCheckData(2, dt_checkinout, dt_leave); //將打卡資料、請假資料進行計算    

            List<monthCheckDate> monthCheckDate = new List<monthCheckDate>(); //繼承checkData
            
            setWholeMonth(checkData, ref monthCheckDate); //將該月應上班的每一天存入monthCheckDate
            //setCheckLeave(ref monthCheckDate, workNum_lab.Text); //將請假及外出資料存入monthCheckDate
            coculateTime(ref monthCheckDate); //計算時數
            showCheckData(ref monthCheckDate); //顯示資料

            Session["monthInOut_list"] = monthCheckDate;
        }

        //void setCheckLeave(ref List<monthCheckDate> cd, string workerNum)
        //{
        //    //查詢所有請假及外出單
        //    string sql = "select l.workerNum, r.Descript, l.start_time as start, l.end_time as end, date(l.start_time) as s_date, date(l.end_time) as e_date "
        //        + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
        //        + "where year(l.start_time)='" + year_dl.SelectedValue + "' and "
        //        + "month(l.start_time)='" + month_dl.SelectedValue + "' and "
        //        + "l.workerNum=" + workerNum + " "
        //        + "UNION "
        //        + "select w.workerNum, '外出' as Descripit, w.start_time as start, w.end_time as end, date(w.start_time) as s_date, date(w.end_time) as e_date "
        //        + "from hr_application_workingout w "
        //        + "where year(w.start_time)='" + year_dl.SelectedValue + "' and "
        //        + "month(w.start_time)='" + month_dl.SelectedValue + "' and "
        //        + "w.workerNum=" + workerNum + " "
        //        + " ORDER BY start ASC;";

        //    DataTable dt = GlobalAnnounce.db.GetDataTable(sql);

        //    //將請假資料存至memo
        //    foreach (var item in cd)
        //    {
        //        var i = (from DataRow dr in dt.Rows
        //                 where dr["workerNum"].ToString() == item.WorkerNum && HR_class.isBetween(Convert.ToDateTime(item.monthDate), Convert.ToDateTime(dr["s_date"].ToString()), Convert.ToDateTime(dr["e_date"].ToString()))
        //                 select dr).ToList();

        //        //系統時間
        //        DateTime am830 = Convert.ToDateTime(item.Date + " " + HR_class.CheckIn);
        //        DateTime am831 = Convert.ToDateTime(item.Date + " " + HR_class.Late);
        //        DateTime am900 = Convert.ToDateTime(item.Date + " " + HR_class.Absen_time);
        //        DateTime pm1200 = Convert.ToDateTime(item.Date + " " + HR_class.MidOut);
        //        DateTime pm100 = Convert.ToDateTime(item.Date + " " + HR_class.MidIn);
        //        DateTime pm530 = Convert.ToDateTime(item.Date + " " + HR_class.CheckOut);
        //        //計算遲到的請假時間
        //        DateTime leave_start = am830, leave_end = pm530;
        //        //打卡時間
        //        DateTime checkin = am830;
        //        DateTime checkout = pm530; 

        //        foreach (var ii in i)
        //        {
        //            if (item.CheckIn != "" || item.CheckOut != "")
        //            {
        //                checkin = Convert.ToDateTime(item.Date + " " + item.CheckIn.ToString());
        //                checkout = Convert.ToDateTime(item.Date + " " + item.CheckOut.ToString());
        //            }
        //            //請假時間
        //            DateTime start = Convert.ToDateTime(ii["start"]);
        //            DateTime end = Convert.ToDateTime(ii["end"]);

        //            if (ii == i[0])
        //            {
        //                item.memo = ii["Descript"].ToString();
        //                item.LeaveT = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
        //                leave_start = start; leave_end = end;
        //            }
        //            else
        //            {
        //                item.memo += "," + ii["Descript"].ToString();
        //                item.LeaveT2 = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
        //                leave_end = end;
        //            }
        //            //if (item.memo == "遲到")
        //            //{ item.memo += ", " + ii["Descript"].ToString(); }
        //            //else
        //            //{ item.memo = ii["Descript"].ToString(); }

        //            //item.memo = ii["Descript"].ToString();
        //        }

        //        double late = 0.0;
        //        if (DateTime.Compare(leave_start, am830) == 0) //請假開始時間等於8:30
        //        {
        //            if (DateTime.Compare(leave_end, pm530) == 0) { } //請整天假
        //            else if (DateTime.Compare(leave_end, pm1200) < 0) //請假結束時間在12:00前
        //            { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }
        //            else if (DateTime.Compare(leave_end, pm1200) >= 0 && DateTime.Compare(leave_end, pm100) <= 0) //請假結束時間在12:00~13:00
        //            { late = Math.Ceiling(checkin.Subtract(pm100).TotalMinutes); }
        //            else if (DateTime.Compare(leave_end, pm100) > 0) //請假結束時間在13:00後
        //            { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }
        //        }
        //        else if (DateTime.Compare(leave_start, am830) > 0)
        //        {
        //            late = Math.Ceiling(checkin.Subtract(am831).TotalMinutes);
        //        }

        //        if (late > 0)
        //        {
        //            item.memo += ",遲到";
        //        }
        //    }
        //}

        void setWholeMonth(List<checkData> cd, ref List<monthCheckDate> monthCheckDate)
        {              
            //將整個月資料設定至monthCheckData
            int y = int.Parse(year_dl.SelectedValue);
            int m = int.Parse(month_dl.SelectedValue);
            string date = year_dl.SelectedValue + "-" + (month_dl.SelectedValue.Length == 1 ? "0" + month_dl.SelectedValue : month_dl.SelectedValue) + "-";

            //查出所有holidays

            //string sql = "select h.Date from hr_holidays h where year(h.Date)='" + y.ToString() + "' and month(h.Date)='" + m.ToString() + "'";
            DataTable dt = GlobalAnnounce.dataSearching.search_holidays(y,m);

            for (int i = 1; i <= DateTime.DaysInMonth(y, m); i++) //該月每一天，除了holidays
            {
                //1~9號，補0
                string t_date = date + (i < 10 ? "0" + i.ToString() : i.ToString());
                string day = month_dl.SelectedValue + "/" + i.ToString();
                //ii 為當天的打卡資料
                var ii = (from item in cd
                          where item.Date == t_date
                          select item).ToList();

                string wn = workNum_lab.Text, name = search_name_lab.Text, c_date = "", checkin = "", checkout = "", type = "", memo = "", log = "", LeaveT = "", LeaveT2 = "", flexibleT = "";
                //t 為假日資料
                var t = (from DataRow dr in dt.Rows
                         where Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd") == t_date
                         select dr).ToList();
                //l 為請假資料
                var l = (from DataRow dr in dt_leave.Rows
                         where HR_class.isBetween(Convert.ToDateTime(t_date), Convert.ToDateTime(dr["s_date"].ToString()), Convert.ToDateTime(dr["e_date"].ToString()))
                         select dr).ToList();
                //v 為彈性補回次數
                var v = (from item in cd
                          where item.status == "3"
                          select item).ToList();

                if (t.Count > 0) { continue; } //count大於0表示該時段是假日，則此筆資料跳過

                if (ii.Count > 0) //當天有打卡資料
                {
                    c_date = ii[0].Date;
                    checkin = ii[0].CheckIn;
                    checkout = ii[0].CheckOut;
                    type = ii[0].CheckType;
                    memo = ii[0].memo;
                    log = ii[0].log;

                    DataTable dt_flextime = myDept.search_DeptFlextimeYN(int.Parse(depID_lab.Text)); // 抓系統參數 可以彈性補回的部門
                    if (dt_flextime != null && dt_flextime.Rows.Count > 0)
                    {
                        string flextimeYN = dt_flextime.Rows[0]["flexTimeYN"].ToString();
						DataTable dt_flexTimes = mySysS.search_SysSettingCode("ATT", "flexibleTimes"); // 抓系統參數 彈性補回次數
                        int flextimeCount = 0;
                        if (dt_flexTimes != null && dt_flexTimes.Rows.Count > 0)
                        {
                            flextimeCount = int.Parse(dt_flexTimes.Rows[0]["SettingSrting"].ToString());
                        }

                        if (flextimeYN == "Y" && ii[0].T >= (8 - ii[0].LeaveHour) && ii[0].memo.Contains("遲到") && v.Count < flextimeCount)
                        { flexibleT = "彈性補回"; }
                    }
                    
                    LeaveT = ii[0].LeaveT;
                    LeaveT2 = ii[0].LeaveT2;
                }
                else
                {
                    if (l.Count > 0) //有請假
                    {
                        foreach (var a in l)
                        {
                            DateTime start = Convert.ToDateTime(a["start"]);
                            DateTime end = Convert.ToDateTime(a["end"]);

                            if (a == l[0])
                            {
                                memo = a["Descript"].ToString();
                                //if (start.Date != end.Date)
                                //{
                                //    if (t_date == end.Date.ToString())
                                //    { LeaveT = "08:30~" + end.ToString("HH:mm"); }
                                //    else if (t_date == start.Date.ToString())
                                //    { LeaveT = start.ToString("HH:mm") + "~17:30"; }
                                //    else { LeaveT = "08:30~17:30"; }
                                //}
                                //else
                                //{ LeaveT = start.ToString("HH:mm") + "~" + end.ToString("HH:mm"); }
                                LeaveT = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
                            }
                            else
                            {
                                memo += "," + a["Descript"].ToString();
                                LeaveT2 = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
                            }
                        }
                        log = "修正";
                    }
                    else
                    {
                        memo = "整天未打卡"; log = "修正";
                    }
                }

                monthCheckDate.Add(new monthCheckDate
                {
                    monthDate = t_date,
                    day = day,
                    WorkerNum = wn,
                    EnFName = name,
                    Date = c_date,
                    CheckIn = checkin,
                    CheckOut = checkout,
                    CheckType = type,
                    memo = memo,
                    log = log,
                    flexibleT = flexibleT,
                    LeaveT = LeaveT,
                    LeaveT2 = LeaveT2
                });
            }
        }

        //Zak ver. coculateTime
        //void coculateTime(ref List<monthCheckDate> mcd)
        //{
        //    //計算mcd時間
        //    foreach (var item in mcd)
        //    {
        //        if (item.CheckIn == "" || item.CheckOut == "")
        //        { continue; }

        //        DateTime start = Convert.ToDateTime(item.monthDate + " " + item.CheckIn);
        //        DateTime end = Convert.ToDateTime(item.monthDate + " " + item.CheckOut);
        //        // IN=上班 MID_OUT=午休始 MID_OUT=午休止 OUT=下班
        //        DateTime IN = Convert.ToDateTime(item.monthDate + " " + HR_class.CheckIn);
        //        DateTime MID_OUT = Convert.ToDateTime(item.monthDate + " " + HR_class.MidOut);
        //        DateTime MID_IN = Convert.ToDateTime(item.monthDate + " " + HR_class.MidIn);
        //        DateTime OUT = Convert.ToDateTime(item.monthDate + " " + HR_class.CheckOut);

        //        TimeSpan ts;
        //        double h = 0;
        //        if (DateTime.Compare(start, IN) <= 0) // start <= IN
        //        {
        //            ts = end.Subtract(IN);  //8:30~end
        //            if (DateTime.Compare(end, MID_OUT) <= 0)
        //            { h = ts.Hours; }       //小於半天
        //            else if (DateTime.Compare(end, MID_OUT) > 0 && DateTime.Compare(end, MID_IN) <= 0)
        //            { h = 3; }              //半天 4
        //            else if (DateTime.Compare(end, MID_IN) > 0 & DateTime.Compare(end, OUT) < 0)
        //            { h = ts.Hours - 1; }   //大於半天(扣午休1小時)
        //            else if (DateTime.Compare(end, OUT) >= 0)
        //            { h = 8; }              //整天 8
        //        }
        //        else if (DateTime.Compare(end, OUT) >= 0) // start > IN & end <= OUT
        //        {
        //            ts = OUT.Subtract(start);   //start~1730
        //            if (DateTime.Compare(start, MID_OUT) <= 0)
        //            { h = ts.Hours - 1; }   //大於半天(扣午休1小時)
        //            else if (DateTime.Compare(start, MID_OUT) > 0 & DateTime.Compare(start, MID_IN) <= 0)
        //            { h = 4; }              //半天 4
        //            else if (DateTime.Compare(start, MID_IN) > 0)
        //            { h = ts.Hours; }       //小於半天
        //        }
        //        else // start > IN & end < OUT 且跨中午(扣午休1小時)
        //        {
        //            ts = end.Subtract(start);
        //            h = ts.Hours - 1;
        //        }

        //        int m = ts.Minutes;
        //        if (m >= 45) { h++; } //超過45分鐘算1小時
        //        else if (m >= 20) { h += 0.5; } //20~45分鐘算0.5小時

        //        //若超過8HR 則總時數以8HR計算
        //        if (h > 8)
        //        {
        //            h = 8;
        //        }
        //        item.time = h;
        //    }

        //    var sum = mcd.ToList().Select(s => s.time).Sum();
        //    time_lab.Text = sum.ToString();
        //}

        void coculateTime(ref List<monthCheckDate> mcd)
        {
            DataTable dt_spe = GlobalAnnounce.dataSearching.search_specialT();
            //計算mcd時間
            foreach (var item in mcd)
            {
                if (item.CheckIn == "" || item.CheckOut == "")
                { continue; }

                DateTime start = Convert.ToDateTime(item.monthDate + " " + item.CheckIn);
                DateTime end = Convert.ToDateTime(item.monthDate + " " + item.CheckOut);
                // IN=上班 MID_OUT=午休始 MID_OUT=午休止 OUT=下班
                DateTime IN = Convert.ToDateTime(item.monthDate + " " + HR_class.CheckIn);
                DateTime MID_OUT = Convert.ToDateTime(item.monthDate + " " + HR_class.MidOut);
                DateTime MID_IN = Convert.ToDateTime(item.monthDate + " " + HR_class.MidIn);
                DateTime OUT = Convert.ToDateTime(item.monthDate + " " + HR_class.CheckOut);
                DateTime SPE_IN = Convert.ToDateTime(item.monthDate + " " + HR_class.spe_in);
                DateTime SPE_OUT = Convert.ToDateTime(item.monthDate + " " + HR_class.spe_out);

                //特殊打卡時間的人
                //DataTable d = GlobalAnnounce.dataSearching.search_specialT(item.WorkerNum);
                Boolean isSPE = false;
                DataRow[] foundRows_spe;
                foundRows_spe = dt_spe.Select("workerNum =" + item.WorkerNum);

                if (foundRows_spe.Length > 0)
                {
                    SPE_IN = Convert.ToDateTime(item.monthDate + " " + foundRows_spe[0][7].ToString());
                    SPE_OUT = Convert.ToDateTime(item.monthDate + " " + foundRows_spe[0][8].ToString());
                    isSPE = true;
                }

                if (isSPE==true)
                {
                    #region 特殊打卡時間計算時數
                    TimeSpan ts;
                    double h = 0;                    
                    if (DateTime.Compare(start, SPE_IN) <= 0) // start <= SPE_IN
                    {
                        ts = end.Subtract(SPE_IN);  //9:00~end
                        if (DateTime.Compare(end, MID_OUT) <= 0)
                        { h = ts.Hours; }       //小於半天
                        else if (DateTime.Compare(end, MID_OUT) > 0 && DateTime.Compare(end, MID_IN) <= 0)
                        { h = 3; }              //半天 4
                        else if (DateTime.Compare(end, MID_IN) > 0 & DateTime.Compare(end, SPE_OUT) < 0)
                        { h = ts.Hours - 1; }   //大於半天(扣午休1小時)
                        else if (DateTime.Compare(end, SPE_OUT) >= 0)
                        { h = 8; }              //整天 8
                    }
                    else if (DateTime.Compare(end, SPE_OUT) >= 0) // start > SPE_IN & end >= SPE_OUT
                    {
                        ts = SPE_OUT.Subtract(start);   //start~1800
                        if (DateTime.Compare(start, MID_OUT) <= 0)
                        { h = ts.Hours - 1; }   //大於半天(扣午休1小時)
                        else if (DateTime.Compare(start, MID_OUT) > 0 & DateTime.Compare(start, MID_IN) <= 0)
                        { ts = SPE_OUT.Subtract(MID_IN); h = ts.Hours; }              //半天 4
                        else if (DateTime.Compare(start, MID_IN) > 0)
                        { h = ts.Hours; }       //小於半天
                    }
                    else // start > IN & end < OUT 且跨中午(扣午休1小時)
                    {
                        ts = end.Subtract(start);
                        if (DateTime.Compare(start, MID_OUT) <= 0) //start 在12:00前
                        {
                            if (DateTime.Compare(end, MID_IN) >= 0) //end 在13:00後
                            { h = ts.Hours - 1; }
                            else if (DateTime.Compare(end, MID_OUT) >= 0 && DateTime.Compare(end, MID_IN) < 0) //end 在12:00~13:00
                            { ts = MID_OUT.Subtract(start); h = ts.Hours; }
                            else if (DateTime.Compare(end, MID_OUT) < 0)  //end 在12:00前
                            { h = ts.Hours; }
                        }
                        else if (DateTime.Compare(start, MID_OUT) > 0 && DateTime.Compare(start, MID_IN) <= 0) //start 在12:00~13:00
                        {
                            ts = end.Subtract(MID_IN);
                            h = ts.Hours;
                        }
                        else if (DateTime.Compare(start, MID_IN) > 0) //start 在13:00後
                        { h = ts.Hours; }
                    }

                    int m = ts.Minutes;
                    if (m >= 45) { h++; } //超過45分鐘算1小時
                    else if (m >= 20) { h += 0.5; } //20~45分鐘算0.5小時

                    //若超過8HR 則總時數以8HR計算
                    if (h > 8)
                    { h = 8; }
                    else if (h < 0)
                    { h = 0; }
                    item.time = h;
                    #endregion
                }
                else
                {
                    #region 一般人員計算時數
                    TimeSpan ts;
                    double h = 0;
                    if (DateTime.Compare(start, IN) <= 0) // start <= IN
                    {
                        ts = end.Subtract(IN);  //8:30~end
                        if (DateTime.Compare(end, MID_OUT) <= 0)
                        { h = ts.Hours; }       //小於半天
                        else if (DateTime.Compare(end, MID_OUT) > 0 && DateTime.Compare(end, MID_IN) <= 0)
                        { h = 3; }              //半天 4
                        else if (DateTime.Compare(end, MID_IN) > 0 & DateTime.Compare(end, OUT) < 0)
                        { h = ts.Hours - 1; }   //大於半天(扣午休1小時)
                        else if (DateTime.Compare(end, OUT) >= 0)
                        { h = 8; }              //整天 8
                    }
                    else if (DateTime.Compare(end, OUT) >= 0) // start > IN & end >= OUT
                    {
                        ts = OUT.Subtract(start);   //start~1730
                        if (DateTime.Compare(start, MID_OUT) <= 0)
                        { h = ts.Hours - 1; }   //大於半天(扣午休1小時)
                        else if (DateTime.Compare(start, MID_OUT) > 0 & DateTime.Compare(start, MID_IN) <= 0)
                        { ts = OUT.Subtract(MID_IN); h = ts.Hours; }              //半天 4
                        else if (DateTime.Compare(start, MID_IN) > 0)
                        { h = ts.Hours; }       //小於半天
                    }
                    else // start > IN & end < OUT 且跨中午(扣午休1小時)
                    {
                        ts = end.Subtract(start);
                        if (DateTime.Compare(start, MID_OUT) <= 0) //start 在12:00前
                        {
                            if (DateTime.Compare(end, MID_IN) >= 0) //end 在13:00後
                            { h = ts.Hours - 1; }
                            else if (DateTime.Compare(end, MID_OUT) >= 0 && DateTime.Compare(end, MID_IN) < 0) //end 在12:00~13:00
                            { ts = MID_OUT.Subtract(start); h = ts.Hours; }
                            else if (DateTime.Compare(end, MID_OUT) < 0)  //end 在12:00前
                            { h = ts.Hours; }
                        }
                        else if (DateTime.Compare(start, MID_OUT) > 0 && DateTime.Compare(start, MID_IN) <= 0) //start 在12:00~13:00
                        {
                            ts = end.Subtract(MID_IN);
                            h = ts.Hours;
                        }
                        else if (DateTime.Compare(start, MID_IN) > 0) //start 在13:00後
                        { h = ts.Hours; }
                    }

                    int m = ts.Minutes;
                    if (m >= 45) { h++; } //超過45分鐘算1小時
                    else if (m >= 20) { h += 0.5; } //20~45分鐘算0.5小時

                    //若超過8HR 則總時數以8HR計算
                    if (h > 8)
                    { h = 8; }
                    else if (h < 0)
                    { h = 0; }
                    item.time = h;
                    #endregion
                }
            }

            var sum = mcd.ToList().Select(s => s.time).Sum();
            time_lab.Text = sum.ToString();
        }

        void showCheckData(ref List<monthCheckDate> mcd)
        {
            var show_mcd = mcd.ToList();
            try
            {
                if (uc_hr_memo_dl.getText() != "***") //memo 條件
                {
                    show_mcd = (from item in mcd
                                where item.memo.Contains(uc_hr_memo_dl.getText())
                                select item).ToList();
                }
            }
            catch (Exception ee) { }

           

            checkinout_grid.DataSource = show_mcd;
            checkinout_grid.DataBind(); 

            if (checkinout_grid.Rows.Count >= 1)
            { excel_btn.Visible = true; }
            else { excel_btn.Visible = false; }            
        }

        protected void excel_btn_Click(object sender, EventArgs e)
        {
            //下載excel

            string n = year_dl.SelectedValue + "-" + month_dl.SelectedValue + " " + workNum_lab.Text + "(" + search_name_lab.Text + ")";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + n + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            /*******************************************************/
            //將資料依需求存成datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("Date"); dt.Columns.Add("CheckIn");
            dt.Columns.Add("CheckOut"); dt.Columns.Add("Time"); dt.Columns.Add("Memo");
            DataRow dr;
            List<monthCheckDate> cd = (List<monthCheckDate>)Session["monthInOut_list"];
            foreach (var i in cd)
            {
                dr = dt.NewRow();
                dr["Date"] = i.monthDate; dr["CheckIn"] = i.CheckIn;
                dr["CheckOut"] = i.CheckOut; dr["Time"] = i.time; dr["Memo"] = i.memo;
                dt.Rows.Add(dr);
            }
            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();
            gv.Style.Add("font-size", "16pt");
            gv.Style.Add("text-align", "center");
            foreach (TableCell c in gv.HeaderRow.Cells)
            {
                c.BackColor = System.Drawing.ColorTranslator.FromHtml("#507CD1");
                c.ForeColor = System.Drawing.Color.White;
            }
            gv.RowStyle.HorizontalAlign = HorizontalAlign.Center;
            /*******************************************************/
            string str = "<table style='font-size:16pt;'><tr>"
                + "<td>WorkerNum:" + workNum_lab.Text + "</td>"
                + "<td>Name:" + search_name_lab.Text + "</td>"
                + "<td>TotalTime:" + time_lab.Text + "</td>"
                + "</tr><tr>"
                + "<td>應工作天數:" + days_lab.Text + "</td>"
                + "<td>應工作時數:" + hours_lab.Text + "</td>"
                + "<td></td>"
                + "</tr><tr><td colspan=3></td></tr></table></br>";
            Response.Write(str);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //下載excel需override此function

            /*Tell the compiler that the control is rendered
             * explicitly by overriding the VerifyRenderingInServerForm event.*/
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            searchCheck(uc_all_employee.getValue());

            coculateMonthDay();
        }

        void coculateMonthDay()
        {
            //計算出該月應上班天數及時數
            int year = int.Parse(year_dl.SelectedValue);
            int month = int.Parse(month_dl.SelectedValue);
            DataTable dt = GlobalAnnounce.dataSearching.search_holidays(year, month);
            //string sql = "select count(*) as holidays from hr_holidays where month(date)=" + month.ToString() + ";";
            int days = 0, hours = 0, holidays = 0;
            holidays = dt.Rows.Count;
            days = DateTime.DaysInMonth(year, month) - holidays;
            hours = days * 8;

            days_lab.Text = days.ToString();
            hours_lab.Text = hours.ToString();
        }

        public class monthCheckDate:checkData
        {
            public string monthDate { get; set; }
            public string day { get; set; }
            public double time { get; set; }
            public string flexibleT { get; set; }
        }

        protected void year_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            uc_all_employee.changeDropdownlist(3, time);//day:1, month:2, personal & memo:3
        }

        protected void month_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            uc_all_employee.changeDropdownlist(3, time);//day:1, month:2, personal & memo:3
        }

        //透過RowCommand來取得button的index 再抓取資料
        protected void checkinout_grid_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "flex")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = checkinout_grid.Rows[index];
                               
                string workerNum = workNum_lab.Text;
                Label datelab = (Label)row.FindControl("date_lab1");
                string date = datelab.Text;
                Label timelab = (Label)row.FindControl("inTime_lab");
                string time = timelab.Text;
                Label statuslab = (Label)row.FindControl("status_labs");

                DateTime now = DateTime.Now;
                string updateT = now.ToString("yyyy-MM-dd HH:mm:ss");
                string checkTime = date + " " + time;

                GlobalAnnounce.dataSearching.update_checkinout(Session[SessionString.userID].ToString(), updateT, workerNum, checkTime, "I");
                GlobalAnnounce.dataSearching.insert_checkinout(3, workerNum, checkTime, "I");

                searchCheck(workerNum);
            }
        }

        //protected void checkinout_grid_PreRender(object sender, EventArgs e)
        //{
        //    if (isAdminM == false)
        //    {
        //        checkinout_grid.Columns[10].Visible = false;
        //    }
        //    else
        //    {
        //        checkinout_grid.Columns[10].Visible = true;
        //    }
        //}
    }    
}