using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;

namespace misSystem.HR
{
    public partial class HR_checkReport_month : System.Web.UI.Page
    {
        //List<reportCheckData> rcd = new List<reportCheckData>(); //最後要顯示的用報表資料
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HR_class.setSysTime(); //設定上下班打卡時間

                DateTime now = DateTime.Now;
                int now_year = int.Parse(now.Year.ToString());
                int now_month = int.Parse(now.Month.ToString());

                for (int i = now_year - 1; i <= (now_year + 0); i++) //前一年至後三年
                {
                    year_dl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                for (int i = 1; i <= 12; i++)
                {
                    month_dl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                year_dl.SelectedValue = now_year.ToString();
                month_dl.SelectedValue = now_month.ToString();

                if (Session[SessionString.userID] != null)
                {
                    string sql = "select c.ChiName,c.EnFName from hr_per_employee c where c.EmpPID=" + Session[SessionString.userID].ToString();
                    name_lab.Text = (GlobalAnnounce.db.GetDataTable(sql)).Rows[0][0].ToString();
                }

                uc_all_employee.changeDropdownlist(2, now_year+"-"+now_month);//day:1, month:2, personal & memo:3
            }
        }

        protected void search_btn_Click(object sender, EventArgs e)
        {
            searchCheck(); 
        }

        void searchCheck()
        {
            //查詢所有請假及外出單
            string sql; //= "select l.workerNum, l.start_time, l.end_time, r.Descript as memo, l.status as s "
            //    + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
            //    + "where year(l.start_time)='" + year_dl.SelectedValue + "' AND month(l.start_time)='" + month_dl.SelectedValue + "'  AND l.isDelete=0 "
            //    + "UNION "
            //    + "select w.workerNum, w.start_time, w.end_time, '外出', w.status "
            //    + "from hr_application_workingout w "
            //    + "where year(w.start_time)='" + year_dl.SelectedValue + "' AND month(w.start_time)='" + month_dl.SelectedValue + "'  AND w.isDelete=0 "
            //    +"order by start_time ASC;";

            //sql = "select l.workerNum, l.start_time, l.end_time, r.Descript as memo, l.status as s from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID where  l.isDelete=0 order by start_time ASC";
            //DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            DataTable dt = GlobalAnnounce.dataSearching.search_MonthLeave(year_dl.SelectedValue, month_dl.SelectedValue);
            List<leaveData> leaveData = new List<leaveData>();
            //將上面查詢結果存至leaveData
            leaveData = (from DataRow dr in dt.Rows
                         where dr["s"].ToString() == "0"
                         select new leaveData
                         {
                             WorkerNum = dr["workerNum"].ToString(),
                             Start = Convert.ToDateTime(dr["start_time"].ToString()),
                             End = Convert.ToDateTime(dr["end_time"].ToString()),
                             memo = dr["memo"].ToString()
                         }).ToList();
            //計算所有時間
            foreach (var item in leaveData)
            { item.time = HR_class.getHours(item.Start, item.End); }

            List<checkData> checkData = new List<checkData>();
            sql = "select c.CheckTime, c.CheckType, a.EmpNo, a.EnFName, a.ChiName, c.status "
                + "from hr_per_employee a left join hr_checkinout c on ( a.EmpNo=c.WorkerNum "
                + "AND year(c.CheckTime)='" + year_dl.SelectedValue + "' AND month(c.CheckTime)='" + month_dl.SelectedValue + "' "
                + "AND time(c.CheckTime)> cast('" + HR_class.Late + "' as time) AND c.isDel='X' )";
            //sql += " join au_userinfo on(au_userinfo.WorkerNum=a.EmpNo) ";
            sql += " left join hr_specialperson h on(a.EmpNo=h.workerNum) ";

            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            if (uc_all_employee.getValue() != "***")
            { sql += " WHERE a.EmpNo=" + uc_all_employee.getValue() + " "; }
            else
            {
                sql += " WHERE  ( a.OnboardDT<='" + time + "-31' AND (a.ResignationDT >= '" + time + "-1' OR a.ResignationDT<=>NULL) AND ((h.type = 2) OR (h.type = 1 AND h.isDel ='O') OR (h.type = 3 AND h.isDel ='O')   OR h.type<=>NULL)) ";
            }
            sql += " ";
            sql += " ORDER BY EmpNo ASC";
            dt = GlobalAnnounce.db.GetDataTable(sql);
            checkData = HR_class.getCheckDataList(dt); //取得打卡資料

            List<reportCheckData> rcd = new List<reportCheckData>(); //最後要顯示的用報表資料

            //未打卡的曠職
            List<workdayData> workdayData = new List<workdayData>();
            caculateDate(ref workdayData);
            caculateAbsent(ref workdayData, ref rcd, ref leaveData);

            //checkLate(ref rcd, leaveData, ref checkData); ////檢查遲到或曠職，並計算時間
            caculateLeave(ref leaveData, ref rcd); //將請假資料及時間加總至rcd            

            showCheckData(ref rcd); //顯示rcd
            showParttimeData();

            Session["rcd"] = rcd;
            //Session["ld"] = leaveData;
        }

        void checkLate(ref List<reportCheckData> rcd, List<leaveData> ld, ref List<checkData> cd)
        {
            
        }

        void caculateLeave(ref List<leaveData> ld, ref List<reportCheckData> rcd)
        {
            //記算請假時間為存入rcd
            foreach (var item in ld)
            {
                var i = (from ii in rcd
                         where ii.workerNum == item.WorkerNum
                         select ii).ToList();

                foreach (var ii in i)
                {
                    //d為天數計算方式，原本請假單算好的時間皆為小時
                    double d = 0.0;
                    if (item.time % 8 != 0)
                    {
                        d += Math.Floor(item.time / 8) + 0.5;
                    }
                    else { d += item.time / 8; }
                    //if (item.time == 8) { d += 1; }
                    //else if (item.time == 4.5) { d += 0.5; }//下半天
                    //else if (item.time == 3.5) { d += 0.5; }//上半天

                    switch (item.memo)
                    {
                        case "特休":
                            ii.xLeave += item.time;
                            break;
                        case "事假":
                            ii.perleave += item.time;
                            break;
                        case "病假":
                            ii.sickleave += item.time;
                            break;
                        case "喪假":
                            ii.bereavement += d;
                            break;
                        case "補休":
                            ii.compensatory += item.time;
                            break;
                        case "婚假":
                            ii.marriageleave += d;
                            break;
                        case "產假":
                            ii.maternity += d;
                            break;
                        case "公假":
                            ii.publicleave += item.time;
                            break;
                        case "公傷病假":
                            ii.xSickleave += d;
                            break;
                        case "陪產":
                            ii.paternity += d;
                            break;
                        case "留職停薪":
                            ii.nopayleave += d;
                            break;
                        case "出差":
                            ii.businesstrip += d;
                            break;
                        case "外出":
                            ii.workout += item.time;
                            break;
                        case "整天未打卡":
                            ii.absenteeism += 1;
                            break;
                        case "謀職假":
                            ii.Seekingleave += d;
                            break;
                    }
                }
            }            
        }

        void caculateDate(ref List<workdayData> wd)
        {
            int year = int.Parse(year_dl.SelectedValue);
            int month = int.Parse(month_dl.SelectedValue);
            int monthday = DateTime.DaysInMonth(year, month);//月份總天數

            DataTable dt = GlobalAnnounce.dataSearching.search_holidays(year, month);

            string day = year + "-" + (month < 10 ? "0" + month.ToString() : month.ToString()) + "-";

            for (int i = 1; i <= monthday; i++)
            {
                string w_date = day + (i < 10 ? "0" + i.ToString() : i.ToString());
                var t = (from DataRow dr in dt.Rows
                         where Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd") == w_date
                         select dr).ToList();

                if (t.Count > 0) { continue; } //count大於0表示該時段是假日，則此筆資料跳過

                wd.Add(new workdayData
                {
                    date = w_date
                });
            }
        }

        void caculateAbsent(ref List<workdayData> wd, ref List<reportCheckData> rcd, ref List<leaveData> ld)
        {
            #region 跨天假
            List<leaveData> ld2 = ld;
            var tmp = (from i in ld2
                       where i.Start.Date.ToString("yyyy/M/d") != i.End.Date.ToString("yyyy/M/d")
                       select i).ToList();
            /*原始跨天假資料 依照橫跨天數新增
             * ex: start:2016/8/12 08:00:00 end:2016/8/14 17:30:00  (原始資料不動  至下方再修改)
             *  insert start:2016/8/13 08:00:00 end:2016/8/13 17:30:00
             *  insert start:2016/8/14 08:00:00 end:2016/8/14 17:30:00*/
            foreach (var i in tmp) //跨天假
            {
                int count = i.End.Date.Subtract(i.Start.Date).Days;//天數

                for (int ii = 1; ii < (count + 1); ii++)
                {
                    if (ii != count)
                    {
                        ld2.Add(new leaveData
                        {
                            WorkerNum = i.WorkerNum.ToString(),
                            Start = Convert.ToDateTime(i.Start.Date.AddDays(ii).ToString("yyyy/M/d") + " " + HR_class.CheckIn),
                            End = Convert.ToDateTime(i.Start.Date.AddDays(ii).ToString("yyyy/M/d") + " " + HR_class.CheckOut)
                        });
                    }
                    else
                    {
                        ld2.Add(new leaveData
                        {
                            WorkerNum = i.WorkerNum.ToString(),
                            Start = Convert.ToDateTime(i.End.Date.ToString("yyyy/M/d") + " " + HR_class.CheckIn),
                            End = i.End
                        });
                    }
                }
            }
            #endregion

            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            DataTable dt;
            if (uc_all_employee.getValue() != "***")
            { dt = GlobalAnnounce.dataSearching.select_allEmployee(time, uc_all_employee.getValue()); }
            else
            { dt = GlobalAnnounce.dataSearching.select_allEmployee(time); }

            DataTable dt_spe = GlobalAnnounce.dataSearching.search_specialT(); //特殊時間打卡資料
            var e = (from DataRow dr in dt.Rows select dr).ToList(); //所有員工資料
            foreach (var dr in e)
            {
                #region 曠職 & 遲到
                int absenteeism = 0; double late = 0; int count = 0;

                foreach (var data in wd) //for (int i = 0; i < wd.Count; i++)
                {
                    //尚未上班日不應記為曠職
                    DateTime now = DateTime.Now;
                    if (DateTime.Compare(Convert.ToDateTime(data.date), now.Date) > 0) { break; }

                    string dd = data.date;
                    //打卡紀錄
                    DataTable dt2 = GlobalAnnounce.dataSearching.search_check(dr["EmpNo"].ToString(), data.date);
                    var d = (from DataRow dr2 in dt2.Rows select dr2).ToList();//應上班日比對打卡紀錄

                    //請假紀錄
                    var s_ld = (from i in ld2
                                where i.WorkerNum == dr["EmpNo"].ToString() && DateTime.Compare(Convert.ToDateTime(data.date), i.Start.Date) == 0
                                select i).ToList();

                    if (d.Count == 0)//沒有打卡紀錄
                    {
                        //就職日以前 OR 離職日以後，不應記為曠職
                        if (DateTime.Compare(Convert.ToDateTime(dr["OnboardDT"]), Convert.ToDateTime(data.date)) > 0)
                        { continue; }

                        if (dr["ResignationDT"].ToString() != "")
                        {
                            if (DateTime.Compare(Convert.ToDateTime(dr["ResignationDT"]), Convert.ToDateTime(data.date)) < 0)
                            { continue; }
                        }

                        if (s_ld.Count == 0) //沒請假
                        {
                            absenteeism += 1;
                        }
                    }
                    else
                    {
                        #region 遲到
                        //遲到
                        DateTime am830 = Convert.ToDateTime(data.date + " " + HR_class.CheckIn);
                        DateTime am831 = Convert.ToDateTime(data.date + " " + HR_class.Late);
                        DateTime am900 = Convert.ToDateTime(data.date + " " + HR_class.Absen_time);
                        DateTime pm1200 = Convert.ToDateTime(data.date + " " + HR_class.MidOut);
                        DateTime pm100 = Convert.ToDateTime(data.date + " " + HR_class.MidIn);
                        DateTime pm530 = Convert.ToDateTime(data.date + " " + HR_class.CheckOut);
                        DateTime SPE_IN = Convert.ToDateTime(data.date + " " + HR_class.spe_in);
                        DateTime SPE_OUT = Convert.ToDateTime(data.date + " " + HR_class.spe_out);
                        //請假時間
                        DateTime leave_start = am830, leave_end = pm530;

                        var ii = d[0];//打卡紀錄第一筆，即為checkin第一筆
                        if (ii["CheckType"].ToString() == "I")
                        {
                            DateTime checkin = Convert.ToDateTime(ii["CheckTime"].ToString());
                            //DateTime checkout = Convert.ToDateTime(item.Date + " " + item.CheckOut);
                            double tmpLate = 0;                            
                            if (ii["status"].ToString() == "3") { continue; } //跳過彈性補登

                            //請假資料的子資料，即上班打卡日期與請假日期相同
                            var s_ld2 = (from i in ld2
                                         where i.WorkerNum == dr["EmpNo"].ToString() &&
                                         DateTime.Compare(checkin.Date, i.Start.Date) == 0
                                         select i).ToList();

                            double x = Math.Ceiling(pm1200.Subtract(checkin).TotalMinutes);

                            //特殊打卡時間
                            //dt = GlobalAnnounce.dataSearching.search_specialT(dr["WorkerNum"].ToString());
                            Boolean isSPE = false;
                            DataRow[] foundRows_spe;
                            foundRows_spe = dt_spe.Select("workerNum =" + dr["EmpNo"].ToString());

                            if (foundRows_spe.Length > 0)
                            {
                                SPE_IN = Convert.ToDateTime(data.date + " " + foundRows_spe[0][7].ToString());
                                SPE_OUT = Convert.ToDateTime(data.date + " " + foundRows_spe[0][8].ToString());
                                isSPE = true;
                            }

                            if (isSPE == true)//特殊打卡時間
                            {
                                //DateTime SPE_IN = Convert.ToDateTime(data.date + " " + dt.Rows[0]["inT"].ToString());
                                //DateTime SPE_OUT = Convert.ToDateTime(data.date + " " + dt.Rows[0]["outT"].ToString());

                                if (DateTime.Compare(checkin, SPE_OUT) > 0) { continue; }

                                #region x>0
                                if (x > 0) //上午來
                                {
                                    if (s_ld2.Count != 0)//有請假
                                    {
                                        /*原始跨天假資料 改成第一天假資料
                                         * ex: start:2016/8/12 08:00:00 end:2016/8/13 17:30:00
                                         *  to start:2016/8/12 08:00:00 end:2016/8/12 17:30:00 */
                                        foreach (var i in s_ld2)
                                        {
                                            if (i.Start.Date != i.End.Date)
                                            { i.End = Convert.ToDateTime(i.Start.Date.ToString("yyyy/M/d") + " " + HR_class.CheckOut); }

                                            if (i == s_ld2[0])
                                            {
                                                leave_start = i.Start; leave_end = i.End;
                                            }
                                            else { leave_end = i.End; }
                                        }

                                        if (DateTime.Compare(checkin, leave_start) >= 0 && DateTime.Compare(checkin, leave_end) <= 0)
                                        {
                                            //準時在請假時間內打卡
                                        }
                                        else if (DateTime.Compare(checkin, leave_start) < 0) //打卡時間比請假開始時間早
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(SPE_IN).TotalMinutes);
                                        }
                                        else
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes);
                                        }
                                    }
                                    else
                                    {
                                        tmpLate = Math.Ceiling(checkin.Subtract(SPE_IN).TotalMinutes);
                                    }
                                }
                                #endregion
                                #region x<-60
                                else if (x < -60) //下午來
                                {
                                    if (s_ld2.Count != 0)//有請假
                                    {
                                        /*原始跨天假資料 改成第一天假資料
                                         * ex: start:2016/8/12 08:00:00 end:2016/8/13 17:30:00
                                         *  to start:2016/8/12 08:00:00 end:2016/8/12 17:30:00 */
                                        foreach (var i in s_ld2)
                                        {
                                            if (i.Start.Date != i.End.Date)
                                            { i.End = Convert.ToDateTime(i.Start.Date.ToString("yyyy/M/d") + " " + HR_class.CheckOut); }

                                            if (i == s_ld2[0])
                                            {
                                                leave_start = i.Start; leave_end = i.End;
                                            }
                                            else { leave_end = i.End; }
                                        }

                                        if (DateTime.Compare(checkin, leave_start) >= 0 && DateTime.Compare(checkin, leave_end) <= 0)
                                        {
                                            //準時在請假時間內打卡
                                        }
                                        else if (DateTime.Compare(leave_end, pm100) > 0)
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes);
                                        }
                                        else
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(pm100).TotalMinutes);
                                        }
                                    }
                                    else
                                    {
                                        tmpLate = Math.Ceiling(pm1200.Subtract(SPE_IN).TotalMinutes) + Math.Ceiling(checkin.Subtract(pm100).TotalMinutes);
                                    }
                                }
                                #endregion
                                #region -60<x<0
                                else if (x <= 0 && x >= -60)
                                {
                                    if (s_ld2.Count != 0)//有請假
                                    { }
                                    else
                                    {
                                        tmpLate = Math.Ceiling(pm1200.Subtract(SPE_IN).TotalMinutes);
                                    }
                                }
                                #endregion
                            }
                            else //一般人員
                            {
                                if (DateTime.Compare(checkin, pm530) > 0) { continue; }

                                #region x>0
                                if (x > 0) //上午來
                                {
                                    if (s_ld2.Count != 0)//有請假
                                    {

                                        /*原始跨天假資料 改成第一天假資料
                                         * ex: start:2016/8/12 08:00:00 end:2016/8/13 17:30:00
                                         *  to start:2016/8/12 08:00:00 end:2016/8/12 17:30:00 */
                                        foreach (var i in s_ld2)
                                        {
                                            if (i.Start.Date != i.End.Date)
                                            { i.End = Convert.ToDateTime(i.Start.Date.ToString("yyyy/M/d") + " " + HR_class.CheckOut); }

                                            if (i == s_ld2[0])
                                            {
                                                leave_start = i.Start; leave_end = i.End;
                                            }
                                            else { leave_end = i.End; }
                                        }

                                        if (DateTime.Compare(checkin, leave_start) >= 0 && DateTime.Compare(checkin, leave_end) <= 0)
                                        {
                                            //準時在請假時間內打卡
                                        }
                                        else if (DateTime.Compare(checkin, leave_start) < 0) //打卡時間比請假開始時間早
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(am831).TotalMinutes);
                                        }
                                        else
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes);
                                        }
                                    }
                                    else
                                    {
                                        tmpLate = Math.Ceiling(checkin.Subtract(am831).TotalMinutes);
                                    }
                                }
                                #endregion
                                #region x<-60
                                else if (x < -60) //下午來
                                {
                                    if (s_ld2.Count != 0)//有請假
                                    {
                                        /*原始跨天假資料 改成第一天假資料
                                         * ex: start:2016/8/12 08:00:00 end:2016/8/13 17:30:00
                                         *  to start:2016/8/12 08:00:00 end:2016/8/12 17:30:00 */
                                        foreach (var i in s_ld2)
                                        {
                                            if (i.Start.Date != i.End.Date)
                                            { i.End = Convert.ToDateTime(i.Start.Date.ToString("yyyy/M/d") + " " + HR_class.CheckOut); }

                                            if (i == s_ld2[0])
                                            {
                                                leave_start = i.Start; leave_end = i.End;
                                            }
                                            else { leave_end = i.End; }
                                        }

                                        if (DateTime.Compare(checkin, leave_start) >= 0 && DateTime.Compare(checkin, leave_end) <= 0)
                                        {
                                            //準時在請假時間內打卡
                                        }
                                        else if (DateTime.Compare(leave_end, pm100) > 0)
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes);
                                        }
                                        else
                                        {
                                            tmpLate = Math.Ceiling(checkin.Subtract(pm100).TotalMinutes);
                                        }
                                    }
                                    else
                                    {
                                        tmpLate = Math.Ceiling(pm1200.Subtract(am831).TotalMinutes) + Math.Ceiling(checkin.Subtract(pm100).TotalMinutes);
                                    }
                                }
                                #endregion
                                #region -60<x<0
                                else if (x <= 0 && x >= -60)
                                {
                                    if (s_ld2.Count != 0)//有請假
                                    { }
                                    else
                                    {
                                        tmpLate = Math.Ceiling(pm1200.Subtract(am831).TotalMinutes);
                                    }
                                }
                                #endregion
                            }
                            if (tmpLate > 0) { count++; }
                            late += tmpLate < 0 ? 0 : tmpLate;
                        }
                        #endregion
                    }
                }
                absenteeism = absenteeism < 0 ? 0 : absenteeism;

                #region 顯示當月就職 & 離職
                //顯示當月就職 & 離職
                string InaugurationDate = "", leavedDT = "";
                string inDate = Convert.ToDateTime(dr["OnboardDT"].ToString()).ToString("yyyy-M-d"); //就職日
                string y = (inDate.Split('-'))[0];
                string m = (inDate.Split('-'))[1];
                if (y == year_dl.SelectedValue && m == month_dl.SelectedValue)
                //{ InaugurationDate = inDate; }
                { InaugurationDate = Convert.ToDateTime(inDate).ToString("M/d"); }

                if (!String.IsNullOrEmpty(dr["ResignationDT"].ToString()))
                {
                    string leavedDate = Convert.ToDateTime(dr["ResignationDT"].ToString()).ToString("yyyy-M-d"); //離職日
                    y = (leavedDate.Split('-'))[0];
                    m = (leavedDate.Split('-'))[1];
                    if (y == year_dl.SelectedValue && m == month_dl.SelectedValue)
                    //{ leavedDT = leavedDate; }
                    { leavedDT = Convert.ToDateTime(leavedDate).ToString("M/d"); }
                }
                #endregion

                #region 顯示資料(reportCheckData)
                //rcd的子資料
                var s_rcd = (from ii in rcd
                             where ii.workerNum == dr["EmpNo"].ToString()
                             select ii).ToList();

                if (s_rcd.Count == 0)
                {
                    //表示rcd尚未有該員工資料
                    rcd.Add(new reportCheckData
                    {
                        workerNum = dr["EmpNo"].ToString(),
                        EnFName = dr["EnFName"].ToString(),
                        ChiName = dr["ChiName"].ToString(),
                        late = late,
                        absenteeism = absenteeism,
                        InaugurationDate = InaugurationDate,
                        leavedDT = leavedDT,
                        countLate=count
                    });
                }
                else
                {
                    //表示rcd已有該員工資料，則直接存入即可
                    foreach (var ii in s_rcd)
                    {
                        ii.late = late; ii.absenteeism = absenteeism;
                        ii.InaugurationDate = InaugurationDate;
                        ii.leavedDT = leavedDT;
                        ii.countLate = count;
                    }
                }
                #endregion

                #endregion
            }
        }

        public string para = ""; //串連get字串
        void showCheckData(ref List<reportCheckData> rcd)
        {
            para = "&y=" + year_dl.SelectedValue + "&m=" + month_dl.SelectedValue;
            checkinout_grid.DataSource = rcd;
            checkinout_grid.DataBind();

            if (checkinout_grid.Rows.Count >= 1)
            {
                excel_btn.Visible = true;
                if (uc_all_employee.getValue() == "***")
                { btn_settleUp.Visible = false; }
               
                int y = int.Parse(year_dl.SelectedValue) - 1911;
                title_lab.Text = y.ToString() + "年" + month_dl.SelectedValue + "月考勤統計";
            }
            else { excel_btn.Visible = false; }
        }

        public string para2 = ""; //串連get字串
        void showParttimeData()
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;

            //工讀生資料
            string sql;  //= " select a.WorkerNum, a.EnFName, a.ChiName"
            //+ " from au_computerform a"
            //+ " join au_userinfo on(au_userinfo.WorkerNum=a.WorkerNum)"
            //+ " left join hr_specialperson h on(au_userinfo.WorkerNum=h.workerNum)"
            //+ " WHERE a.InaugurationDate<='" + time + "-31' AND (au_userinfo.leavedDT >= '" + time + "-1' OR au_userinfo.leavedDT<=>NULL) AND (h.type = 3 AND h.isDel ='X' )"
            //+ " ORDER BY WorkerNum ASC";

            DataTable dt = GlobalAnnounce.dataSearching.search_Parttime(time);

            List<parttimeData> parttimeData = new List<parttimeData>();
            parttimeData = (from DataRow dr in dt.Rows
                            select new parttimeData
                            {
                                workerNum = dr["EmpNo"].ToString(),
                                ChiName = dr["ChiName"].ToString(),
                            }).ToList();

            //工讀生打卡紀錄
            List<checkData> parttimeCheckData = new List<checkData>();
            sql = "select c.*, a.EnFName, a.ChiName "
                + "from hr_per_employee a inner join hr_checkinout c on (a.EmpNo=c.WorkerNum "
                + "AND year(c.CheckTime)='" + year_dl.SelectedValue + "' AND month(c.CheckTime)='" + month_dl.SelectedValue + "' )";
            //sql += " join au_userinfo on(au_userinfo.WorkerNum=a.EmpNo) ";
            sql += " left join hr_specialperson h on(c.WorkerNum=h.workerNum) ";
            sql += " WHERE (c.isDel='X' OR c.isDel<=>NULL) AND  ( a.OnboardDT<='" + time + "-31' AND (a.ResignationDT >= '" + time + "-1' OR a.ResignationDT<=>NULL) AND ( (h.type = 3 AND h.isDel ='X')  ) ) ";
            sql += " ORDER BY WorkerNum, CheckTime ASC";

            dt = GlobalAnnounce.db.GetDataTable(sql);
            parttimeCheckData = HR_class.getCheckDataList(dt); //取得打卡資料

            foreach (var item in parttimeData)
            {
                var ii = (from i in parttimeCheckData
                          where i.WorkerNum == item.workerNum
                          select i).ToList();

                int h = 0, m = 0, totalH = 0, totalM = 0;
                foreach (var t in ii)
                {
                    int tmpH = 0, tmpM = 0;
                    if (String.IsNullOrEmpty(t.CheckIn) || String.IsNullOrEmpty(t.CheckOut)) { continue; }

                    DateTime checkin = Convert.ToDateTime(t.Date + " " + t.CheckIn);
                    DateTime checkout = Convert.ToDateTime(t.Date + " " + t.CheckOut);
                    DateTime am830 = Convert.ToDateTime(t.Date + " " + HR_class.CheckIn);
                    DateTime pm1200 = Convert.ToDateTime(t.Date + " " + HR_class.MidOut);
                    DateTime pm100 = Convert.ToDateTime(t.Date + " " + HR_class.MidIn);
                    DateTime pm530 = Convert.ToDateTime(t.Date + " " + HR_class.CheckOut);

                    if (DateTime.Compare(checkin, am830) < 0)
                    { checkin = am830; }
                    if (DateTime.Compare(checkout, pm530) > 0)
                    { checkout = pm530; }

                    TimeSpan ts;

                    if (DateTime.Compare(checkout, pm1200) >= 0 && DateTime.Compare(checkout, pm100) <= 0) //checkout 在12點~13點
                    { ts = pm1200.Subtract(checkin); tmpH = ts.Hours; tmpM = ts.Minutes; }
                    else if (DateTime.Compare(checkout, pm100) > 0) //checkout 在13點後
                    {
                        if (DateTime.Compare(checkin, pm1200) < 0) //checkin 在12點前
                        { ts = pm1200.Subtract(checkin) + checkout.Subtract(pm100); tmpH = ts.Hours; tmpM = ts.Minutes; }
                        else if (DateTime.Compare(checkin, pm1200) >= 0 && DateTime.Compare(checkin, pm100) <= 0) //checkin 在12點~13點
                        { ts = checkout.Subtract(pm100); tmpH = ts.Hours; tmpM = ts.Minutes; }
                        else if (DateTime.Compare(checkin, pm100) > 0) //checkin 在13點後
                        { ts = checkout.Subtract(checkin); tmpH = ts.Hours; tmpM = ts.Minutes; }
                    }
                    else if (DateTime.Compare(checkout, pm1200) < 0) //checkout 在12點前
                    { ts = checkout.Subtract(checkin); tmpH = ts.Hours; tmpM = ts.Minutes; }
                    //h += HR_class.getHours(Convert.ToDateTime(t.CheckIn), Convert.ToDateTime(t.CheckOut)
                    h += tmpH; m += tmpM;
                }
                totalM = (m % 60); totalH = h + (m / 60);
                item.time = totalH + "小時" + totalM + "分";
                item.day = ii.Count;
            }

            para2 = "&y=" + year_dl.SelectedValue + "&m=" + month_dl.SelectedValue;

            parttime_grid.DataSource = parttimeData;
            parttime_grid.DataBind();

            if (parttimeData.Count >= 1)
            {
                download_btn.Visible = true;
                int y = int.Parse(year_dl.SelectedValue) - 1911;
                parttime_lab.Text = y.ToString() + "年" + month_dl.SelectedValue + "月工讀生考勤統計";
            }
            else { download_btn.Visible = false; }
        }

        protected void download_btn_Click(object sender, EventArgs e)
        {
            download_excel(parttime_lab.Text, parttime_grid);
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

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    //偵錯時可以將打卡資料顯示在下方，以便核對
        //    test.DataSource = (List<checkData>)Session["cd"];
        //    test.DataBind();
        //}

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    //偵錯時可以將請假資料顯示在下方，以便核對
        //    test.DataSource = (List<leaveData>)Session["ld"];
        //    test.DataBind();
        //}

        protected void excel_btn_Click(object sender, EventArgs e)
        {
            download_excel(title_lab.Text, checkinout_grid);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //下載excel需override此function

            /*Tell the compiler that the control is rendered
             * explicitly by overriding the VerifyRenderingInServerForm event.*/
        }

        protected void checkinout_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 2; i < 18; i++)
                {
                    if (Convert.ToDouble(e.Row.Cells[i].Text) > 0)
                    {
                        e.Row.Cells[i].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[i].Text + "</b></span>";
                    }
                }
                //if (Convert.ToInt32(e.Row.Cells[2].Text) > 0)
                //{
                //    e.Row.Cells[2].Text = "<span style=\"color: #ff0033\"><b>" + e.Row.Cells[2].Text + "</b></span>";
                //}
            }
        }

        protected void year_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            uc_all_employee.changeDropdownlist(2, time);//day:1, month:2, personal & memo:3
        }

        protected void month_dl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = year_dl.SelectedValue + "-" + month_dl.SelectedValue;
            uc_all_employee.changeDropdownlist(2, time);//day:1, month:2, personal & memo:3
        }

        protected void btn_settleUp_Click(object sender, EventArgs e)
        {
            settleUp((List < reportCheckData >)Session["rcd"]);
            Session.Remove("rcd");
            btn_settleUp.Visible = false;
        }


        private void settleUp(List<reportCheckData> rcd)
        {
            string sql = string.Empty;

            foreach (var ii in rcd)
            {
                sql += "INSERT INTO hr_att_monthlysettleup(workerNum, ChiName, month, xLeave, perleave, sickleave, workout, compensatory, late, countLate, absenteeism, bereavement, publicleave, xSickleave, marriageleave, maternity, paternity, nopayleave, businesstrip, Seekingleave, OnBoardDate, ResignationDT) VALUES ";
                sql += "("
                    +  ii.workerNum + ","
                    + "'" +ii.ChiName + "',"
                    + month_dl.SelectedValue + ","
                    + "'" + ii.xLeave + "',"
                    + "'" + ii.perleave + "',"
                    + "'" + ii.sickleave + "',"
                    + "'" + ii.workout + "',"
                    + "'" + ii.compensatory + "',"
                    + "'" + ii.late + "',"
                    + "'" + ii.countLate + "',"
                    + "'" + ii.absenteeism + "',"
                    + "'" + ii.bereavement + "',"
                    + "'" + ii.publicleave + "',"
                    + "'" + ii.xSickleave + "',"
                    + "'" + ii.marriageleave + "',"
                    + "'" + ii.maternity + "',"
                   + "'" + ii.paternity + "',"
                   + "'" + ii.nopayleave + "',"
                   + "'" + ii.businesstrip + "',"
                   + "'" + ii.Seekingleave + "',";
                if (!string.IsNullOrEmpty(ii.InaugurationDate))
                { sql += "'" + year_dl.SelectedValue + "/" + ii.InaugurationDate + "',"; }
                else { sql += "NULL,"; }
                if (!string.IsNullOrEmpty(ii.leavedDT))
                { sql += "'" + year_dl.SelectedValue + "/" + ii.leavedDT + "'"; }
                else { sql += "NULL"; }

                sql += ");";
            }
            GlobalAnnounce.db.GetDataTable(sql);
        }
    }

    class leaveData
    {
        public string WorkerNum { get; set; }
        DateTime _Start, _End;
        public DateTime Start
        {
            get { return _Start; }
            set { _Start = Convert.ToDateTime(value); }
        }
        public DateTime End
        {
            get { return _End; }
            set { _End = Convert.ToDateTime(value); }
        }
        public string memo { get; set; }
        public double time { get; set; }
    }

    class reportCheckData
    {
        public string encode_WorkerNum
        {
            get { return HR_class.encodeBase64(workerNum); }
        }
        public string workerNum { get; set; }
        public string EnFName { get; set; }
        public string ChiName { get; set; }
        public double late { get; set; }//遲到(分)
        public double absenteeism { get; set; }//曠職(天)

        public double xLeave { get; set; }//特休(天)
        public double perleave { get; set; }//事假(小時)
        public double sickleave { get; set; }//病假(小時)    
        public double compensatory { get; set; }//補休(小時)
        public double bereavement { get; set; }//喪假(天)
        public double marriageleave { get; set; }//婚假(天)
        public double maternity { get; set; }//產假(天)
        public double publicleave { get; set; }//公假(小時)
        public double xSickleave { get; set; }//公傷病假(小時)
        public double Seekingleave { get; set; }//謀職假(小時)

        public double workout { get; set; }//外出(小時)
        public double paternity { get; set; }//陪產假(天)
        public double nopayleave { get; set; }//留職停薪(天)
        public double businesstrip { get; set; }//出差(天)
        public string InaugurationDate { get; set; }//就職日
        public string leavedDT { get; set; }//離職日
        public int countLate { get; set; }//遲到(次)
    }
    class holidayData
    {
        public string date { get; set; }
    }
    class workdayData
    {
        public string date { get; set; }
    }
    class parttimeData
    {
        public string encode_WorkerNum
        {
            get { return HR_class.encodeBase64(workerNum); }
        }
        public string workerNum { get; set; }
        public string ChiName { get; set; }
        public string time { get; set; }
        public int day { get; set; }
    }
}