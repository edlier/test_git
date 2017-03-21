using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace misSystem.HR
{
    public class HR_class
    {

        //把檔案從232 複製到路徑底下
        public static void changeAccessFileDirToMIS()
        {
            //string strFrom = HttpContext.Current.Server.MapPath("../QC/");

            string strFrom = "\\\\192.168.168.232\\Att_DB\\";
            string strTo = HttpContext.Current.Server.MapPath("copyFile") + @"\";

            System.IO.File.Copy(strFrom + "att2000.mdb", strTo + "att2000.mdb", true);
        }
        public static string Administration = "5"; //行政部門對應db值
        //審核的狀態，與DB hr_status 對應
        public enum STATUS { Department_Manager = 1, HR = 2, Administration_Employee = 3, Administration_Manager = 4, Vice_General_Manager = 5, General_Manager = 6, Proxy = 7, Pass = 0, Fail = 99 };
        //上下班及其它使用的時間
        public static string CheckIn = "8:31:00", CheckOut = "17:30:00", MidOut = "12:00:00", MidIn = "13:00:00", Late = "8:31:00", Absen_time = "9:00:00", spe_in = "9:00:00", spe_out = "18:00:00", flexibleT = "";

        public static void setSysTime()
        {
            //get checktime from db
            string str = "select * from hr_sys_time;";
            DataTable dt = GlobalAnnounce.db.GetDataTable(str);
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr["Descript"].ToString())
                {
                    case "CheckIn":
                        CheckIn = dr["Time"].ToString();
                        break;
                    case "CheckOut":
                        CheckOut = dr["Time"].ToString();
                        break;
                    case "MidOut":
                        MidOut = dr["Time"].ToString();
                        break;
                    case "MidIn":
                        MidIn = dr["Time"].ToString();
                        break;
                    case "Late":
                        Late = dr["Time"].ToString();
                        break;
                    case "Absen_time":
                        Absen_time = dr["Time"].ToString();
                        break;
                    case "Spe_in":
                        spe_in = dr["Time"].ToString();
                        break;
                    case "Spe_out":
                        spe_out = dr["Time"].ToString();
                        break;
                    case "flexibleT":
                        flexibleT = dr["minutes"].ToString();
                        break;
                }
            }
        }

        public static bool check_login(Page page)
        {
            //確認登入
            if (page.Session[SessionString.userID] == null)
            {
                page.Response.Write("<script>alert('請先登入！');");
                page.Response.Write("window.location.href='../Default.aspx';</script>");
                return false;
            }
            return true;
        }

        public static void prevent_cache()
        {
            //防止送出表單後，按上一頁出現舊資訊而重覆新增
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        public static void setDropdownlist(string str, string text, string value, object ob)
        {
            //設定下拉選單
            //str為sql指令
            //ob 的可為dropdownlist or radiobuttonlist
            switch (ob.GetType().Name)
            {
                case "DropDownList":
                    try
                    {
                        DataTable dt = GlobalAnnounce.db.GetDataTable(str);
                    }
                    catch (Exception ee) { Debug.WriteLine(ee.Message); }

                    ((DropDownList)ob).DataSource = GlobalAnnounce.db.GetDataTable(str);
                    ((DropDownList)ob).DataTextField = text;
                    ((DropDownList)ob).DataValueField = value;
                    ((DropDownList)ob).DataBind();
                    break;
                case "RadioButtonList":
                    ((RadioButtonList)ob).DataSource = GlobalAnnounce.db.GetDataTable(str);
                    ((RadioButtonList)ob).DataTextField = text;
                    ((RadioButtonList)ob).DataValueField = value;
                    ((RadioButtonList)ob).DataBind();
                    break;
            }
        }

        public static bool checkDate(TextBox startDate, TextBox startTime, TextBox endDate, TextBox endTime, Label hour, Label error)
        {
            //檢查時間防呆
            //Date格式 yyyy-MM-dd，Time格式 hh-mm-ss，不足需補0
            //傳入hour & error時使用的label，不使用時可直接傳入 new label()

            DateTime start, end;
            hour.Text = "";
            if (startDate.Text != "" || startTime.Text != "" || endDate.Text != "" || endTime.Text != "")
            {
                try
                {
                    start = Convert.ToDateTime(startDate.Text + " " + startTime.Text + ":00");
                    end = Convert.ToDateTime(endDate.Text + " " + endTime.Text + ":00");
                    if (DateTime.Compare(start, end) < 0)
                    {
                        //TimeSpan ts = end.Subtract(start);
                        //double h = 0.0; // 總時數
                        //h = ts.Days * 24 + ts.Hours + (ts.Minutes == 30 ? 0.5 : 0);

                        //DateTime s_start = Convert.ToDateTime(startDate);
                        //DateTime s_end = Convert.ToDateTime(endDate);
                        //int s_day = s_end.Subtract(s_start).Days;

                        //h -= s_day * 15 + ts.Days;

                        //if (startTime.Text == "12:00" || endTime.Text == "13:00")
                        //{ h--; } 

                        double h = getHours(startDate.Text, startTime.Text, endDate.Text, endTime.Text);
                        hour.Text = h.ToString() + " hour(s)";

                        return true;
                    }
                    else { error.Text = "EndTime can't be less than(equal to) StartTime"; }
                }
                catch (Exception ee) { error.Text = "Datetime format is illegal."; }
            }
            else { error.Text = "DateTime fields can't be empty."; }
            return false;
        }

        public static double getHours(DateTime start, DateTime end)
        {
            //算時數
            string startDate, startTime, endDate, endTime;
            startDate = start.ToString("yyyy-MM-dd");
            startTime = start.ToString("HH:mm:ss");
            endDate = end.ToString("yyyy-MM-dd");
            endTime = end.ToString("HH:mm:ss");

            double h = getHours(startDate, startTime, endDate, endTime);
            return h;
        }

        public static double getHours(string startDate, string startTime, string endDate, string endTime)
        {
            //算時數
            //Date格式 yyyy-MM-dd，Time格式 hh-mm-ss，不足需補0

            //請假紀錄時間
            DateTime start = Convert.ToDateTime(startDate + " " + startTime);
            DateTime end = Convert.ToDateTime(endDate + " " + endTime);

            TimeSpan ts = end.Subtract(start);
            double h = 0.0; // 總時數
            h = ts.Days * 24 + ts.Hours + (ts.Minutes == 30 ? 0.5 : 0); //算出這段時間總時數

            DateTime s_start = Convert.ToDateTime(startDate);
            DateTime s_end = Convert.ToDateTime(endDate);
            int s_day = s_end.Subtract(s_start).Days; //不考慮時間時，start & end間隔幾天

            h -= s_day * 15 + ts.Days; //減掉非上班時間及1個小時的午休

            //if (ts.Days == 0)
            //{
            //    if (int.Parse(start.ToString("HH")) < 12 & int.Parse(end.ToString("HH")) > 13)
            //    { h--; }
            //}
            if (int.Parse(start.ToString("HH")) <= 12 & int.Parse(end.ToString("HH")) >= 13)
            { h--; } //跨中午時要減一個小時
            //if (start.ToString("HH") == "12" || end.ToString("HH") == "13")
            //{ h--; }

            string y = (startDate.Split('-'))[0];
            string m = (startDate.Split('-'))[1];
            string sql = "select Date from hr_holidays h "
                + "where year(h.Date)='" + y + "' and month(h.Date)='" + m + "';";
            string[] holidays; //從db讀取已設定的假日
            System.Data.DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            holidays = dt.Rows.OfType<DataRow>().Select(k => Convert.ToDateTime(k[0].ToString()).ToString("yyyy-MM-dd HH:mm:ss")).ToArray();

            DateTime dd = Convert.ToDateTime(startDate);
            int count = 0;
            while (true) //判斷有時間段有幾個假日，並減去假日時間
            {
                if (holidays.Contains(dd.ToString("yyyy-MM-dd HH:mm:ss")))
                { count++; }
                if (DateTime.Compare(dd, Convert.ToDateTime(endDate)) >= 0) { break; }
                dd = dd.AddDays(1);
            }
            h -= count * 8;

            return h;
        }

        public static bool isBetween(DateTime a, DateTime b, DateTime c)
        {
            //回傳是否 a 在 b~c 的區間
            return (DateTime.Compare(a, b) >= 0 && DateTime.Compare(a, c) <= 0);
        }

        public static string getCheckFromAccess(string start, string end)
        {
            //從打卡機的access抓取從 start~end 的打卡記錄，回傳json格式
            string dataSource = HttpContext.Current.Server.MapPath("copyFile") + @"\";
            string constr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataSource + "att2000.mdb";

            //string constr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\192.168.168.232\\Att_DB\att2000.mdb";

            OleDbConnection myCon = new OleDbConnection(constr);

            string str = @"SELECT USERINFO.Badgenumber, CHECKINOUT.CHECKTIME, CHECKINOUT.CHECKTYPE
            FROM USERINFO INNER JOIN CHECKINOUT ON USERINFO.USERID = CHECKINOUT.USERID
            WHERE CHECKINOUT.CHECKTIME between #" + start + @"# and #" + end + @"#
            ORDER BY CHECKINOUT.CHECKTIME;";

            OleDbCommand myCom = new OleDbCommand(str, myCon);
            OleDbDataAdapter myAdp = new OleDbDataAdapter(myCom);

            myCon.Open();
            DataTable dt = new DataTable();
            myAdp.Fill(dt);
            myCon.Close();
            //DataTable dt = new DataTable();
            string json = JsonConvert.SerializeObject(dt);
            Debug.Write(("{\"CheckInOut\":" + json + "}"));
            return "{\"CheckInOut\":" + json + "}";
        }

        public static string getAllUserFromAccess()
        {
            //從打卡機的access抓出所有已登錄的員工資料，回傳json
            //OleDbConnection myCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\192.168.168.232\\Att\att2000.mdb");            
            OleDbConnection myCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\192.168.168.232\\Att\att2000.mdb");

            string str = @"SELECT USERINFO.Badgenumber, USERINFO.Name FROM USERINFO;";

            OleDbCommand myCom = new OleDbCommand(str, myCon);
            OleDbDataAdapter myAdp = new OleDbDataAdapter(myCom);

            myCon.Open();
            DataTable dt = new DataTable();
            myAdp.Fill(dt);
            myCon.Close();

            string json = JsonConvert.SerializeObject(dt);
            Debug.Write(("{\"User\":" + json + "}"));
            return "{\"User\":" + json + "}";
        }

        public static string encodeBase64(string input)
        {
            //使用get傳值時，先以base64編碼
            string encodeBytes = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));
            encodeBytes = HttpUtility.UrlEncode(encodeBytes);
            return encodeBytes;
        }

        public static string decodeBase64(string input)
        {
            //base64解碼            
            //Debug.WriteLine("before url de:" + input);
            // string temp = HttpUtility.UrlDecode(input);
            Debug.WriteLine("after url de:" + input);
            var decodeBytes = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(decodeBytes);
        }

        public static int getStatus(string dep)
        {
            //若使用者是行政人員，回傳狀態:行政主管，反之回傳狀態:部門主管
            return dep == "1" ? (int)STATUS.Administration_Manager : (int)STATUS.Department_Manager;
            // return dep == HR_class.Administration ? (int)STATUS.Administration_Manager : (int)STATUS.Department_Manager;
        }

        public static string getWorkerNum(object wk, string id)
        {
            //傳入workerNumb的session，若為空，則依id查詢回傳workerNum
            string str = "";
            if (wk != null)
            { return wk.ToString(); }
            else
            {
                str = "select workerNum from au_userinfo where id = " + id + ";";
                return (GlobalAnnounce.db.GetDataTable(str)).Rows[0]["workerNum"].ToString();
            }
        }

        public static List<checkData> getCheckDataList(DataTable dt)
        {
            //將打卡記錄格式整理為 checkData 的格式並回傳
            //dt 需要 WorkerNum, EnFName, ChiName, CheckTime, CheckType

            //整理出所有員工
            List<checkData> checkData = new List<checkData>();
            checkData = (from DataRow dr in dt.Rows
                         //where dr["CheckType"].ToString() == "I" || dr["CheckType"].ToString() == ""
                         select new checkData
                         {
                             WorkerNum = dr["EmpNo"].ToString(),
                             EnFName = dr["EnFName"].ToString(),
                             ChiName = dr["ChiName"].ToString(),
                             Date = dr["CheckTime"].ToString()
                             //CheckIn = dr["CheckTime"].ToString(),
                             //CheckType = dr["CheckType"].ToString()
                         }).ToList();

            checkData = (from item in checkData
                         group item by new
                         {
                             item.WorkerNum,
                             item.EnFName,
                             item.ChiName,
                             item.Date
                         } into tt
                         select new checkData
                         {
                             WorkerNum = tt.Key.WorkerNum,
                             EnFName = tt.Key.EnFName,
                             ChiName = tt.Key.ChiName,
                             Date = tt.Key.Date
                         }).ToList();

            //查出上班及下班打卡
            var checkIn = (from DataRow dr in dt.Rows
                           where dr["CheckType"].ToString() == "I"
                           select dr).ToList();

            var checkOut = (from DataRow dr in dt.Rows
                            where dr["CheckType"].ToString() == "O"
                            select dr).ToList();

            //將上班打卡匯至checkData
            foreach (var dr in checkIn)
            {
                var i = (from item in checkData
                         where item.WorkerNum == dr["EmpNo"].ToString() && item.Date == Convert.ToDateTime(dr["CheckTime"].ToString()).ToString("yyyy-MM-dd")
                         select item).ToList();
                foreach (var item in i) { item.CheckIn = dr["CheckTime"].ToString(); item.status = dr["status"].ToString(); }
            }

            //將下班打卡匯至checkData
            foreach (var dr in checkOut)
            {
                var i = (from item in checkData
                         where item.WorkerNum == dr["EmpNo"].ToString() && item.Date == Convert.ToDateTime(dr["CheckTime"].ToString()).ToString("yyyy-MM-dd")
                         select item).ToList();
                foreach (var item in i) { item.CheckOut = dr["CheckTime"].ToString(); }
            }
            //將 I/O 處理為一個record



            ////依時間判斷 遲到、上班未打卡、下班未打卡、未打卡、早退 等狀態並記錄在memo
            //foreach (var i in checkData)
            //{
            //    //特殊打卡時間的人
            //    string s = "SELECT h.*,s.time as 'inT',sy.time as 'outT' FROM hr_specialperson h LEFT JOIN hr_sys_time s on (h.checkin=s.id) LEFT JOIN hr_sys_time sy on (h.checkout=sy.id) WHERE workerNum=" + i.WorkerNum + " AND type=2 AND isDel='X'";
            //    DataTable d = GlobalAnnounce.db.GetDataTable(s);

            //    string str = "";
            //    if (d.Rows.Count > 0)
            //    {
            //        if (i.CheckIn != "")
            //        {
            //            DateTime ct = Convert.ToDateTime(i.CheckIn);
            //            DateTime tt = Convert.ToDateTime(ct.ToString("yyyy/MM/dd") + " " + d.Rows[0]["inT"].ToString());
            //            if (DateTime.Compare(ct, tt) > 0) { str += "遲到,"; }
            //        }
            //        else { str += "上班未打卡,"; }

            //        if (i.CheckOut != "")
            //        {
            //            DateTime ct = Convert.ToDateTime(i.CheckOut);
            //            DateTime tt = Convert.ToDateTime(ct.ToString("yyyy/MM/dd") + " " + d.Rows[0]["outT"].ToString());
            //            if (DateTime.Compare(ct, tt) < 0) { str += "早退,"; }
            //        }
            //        else { str += "下班未打卡,"; }
            //    }
            //    else
            //    {
            //        if (i.CheckIn != "")
            //        {
            //            DateTime ct = Convert.ToDateTime(i.CheckIn);
            //            DateTime tt = Convert.ToDateTime(ct.ToString("yyyy/MM/dd") + " " + Late);//CheckIn -> Late
            //            if (DateTime.Compare(ct, tt) > 0) { str += "遲到,"; }
            //        }
            //        else { str += "上班未打卡,"; }

            //        if (i.CheckOut != "")
            //        {
            //            DateTime ct = Convert.ToDateTime(i.CheckOut);
            //            DateTime tt = Convert.ToDateTime(ct.ToString("yyyy/MM/dd") + " " + CheckOut);
            //            if (DateTime.Compare(ct, tt) < 0) { str += "早退,"; }
            //        }
            //        else { str += "下班未打卡,"; }
            //    }

            //    if (str.Length > 1) { str = str.Remove(str.Length - 1); }
            //    if (i.CheckIn == "" && i.CheckOut == "") { i.memo = "整天未打卡"; }
            //    else { i.memo = str; }

            //    if (!string.IsNullOrEmpty(i.memo))
            //    {
            //        i.log = "修正";
            //    }

            //}

            return checkData;
        }

        //p  ----> 1=day, 2=personal
        public static List<checkData> getCheckData(int p, DataTable dt_checkinout, DataTable dt_leave)
        {
            List<checkData> checkData = new List<checkData>();
            checkData = (from DataRow dr in dt_checkinout.Rows
                         select new checkData
                         {
                             WorkerNum = dr["EmpNo"].ToString(),
                             EnFName = dr["EnFName"].ToString(),
                             ChiName = dr["ChiName"].ToString(),
                             Date = (dr["checkin"].ToString() == "" ? dr["checkout"].ToString() : dr["checkin"].ToString()),
                             CheckIn = dr["checkin"].ToString(),
                             CheckOut = dr["checkout"].ToString(),
                             status = dr["status"].ToString()
                         }).ToList();

            DataTable dt_spe = GlobalAnnounce.dataSearching.search_specialT();

            //將打卡紀錄  先判定是否請假，再計算遲到 並記錄到memo
            foreach (var d in checkData)
            {
                //系統時間
                DateTime am830 = Convert.ToDateTime(d.Date + " " + CheckIn);
                DateTime am831 = Convert.ToDateTime(d.Date + " " + Late);
                DateTime am900 = Convert.ToDateTime(d.Date + " " + Absen_time);
                DateTime pm1200 = Convert.ToDateTime(d.Date + " " + MidOut);
                DateTime pm100 = Convert.ToDateTime(d.Date + " " + MidIn);
                DateTime pm530 = Convert.ToDateTime(d.Date + " " + CheckOut);
                //計算遲到的請假時間
                DateTime leave_start = am830, leave_end = pm530;
                //打卡時間
                DateTime checkin = am830, checkout = pm530;
                //請假時間 
                d.LeaveHour = 0;

                if (d.CheckIn != "" || d.CheckOut != "")
                {
                    try
                    {
                        checkin = Convert.ToDateTime(d.Date + " " + d.CheckIn);
                        checkout = Convert.ToDateTime(d.Date + " " + d.CheckOut);

                        //DateTime flexT = Convert.ToDateTime(Late).AddMinutes(double.Parse(flexibleT));

                        //if (Convert.ToDateTime(d.CheckIn) <= flexT && Convert.ToDateTime(d.CheckIn) > Convert.ToDateTime(Late) && d.status != "3")
                        //{
                        //TimeSpan ts = checkout.Subtract(checkin);
                        //if (checkin > pm100)
                        //{
                        //    TimeSpan ts = checkout.Subtract(checkin);
                        //    int h = ts.Hours;  
                        //    d.T = h;
                        //}
                        //else if (checkin > pm1200)
                        //{
                        //    TimeSpan ts = checkout.Subtract(pm100);
                        //    int h = ts.Hours;  
                        //    d.T = h;
                        //}
                        //else
                        //{
                        //    TimeSpan ts = checkout.Subtract(checkin);
                        //    int h = ts.Hours - 1;  //減中午1小時
                        //    d.T = h;
                        //}
                        //}                       
                    }
                    catch { }
                }

                //特殊打卡時間人員
                DateTime SPE_IN = am830, SPE_OUT = pm530;
                Boolean isSPE = false;
                DataRow[] foundRows_spe;
                foundRows_spe = dt_spe.Select("workerNum =" + d.WorkerNum);

                if (foundRows_spe.Length > 0)
                {
                    spe_in = foundRows_spe[0][7].ToString();
                    spe_out = foundRows_spe[0][8].ToString();

                    SPE_IN = Convert.ToDateTime(d.Date + " " + spe_in);
                    SPE_OUT = Convert.ToDateTime(d.Date + " " + spe_out);

                    //打卡時間
                    if (d.CheckIn != "" || d.CheckOut != "")
                    {
                        try
                        {
                            //DateTime flexT = Convert.ToDateTime(spe_in).AddMinutes(double.Parse(flexibleT));
                            //d.T = 0;
                            //if (Convert.ToDateTime(d.CheckIn) <= flexT && Convert.ToDateTime(d.CheckIn) > Convert.ToDateTime(spe_in) && d.status != "3")
                            //{
                            //    TimeSpan ts = checkout.Subtract(checkin);
                            //    int h = ts.Hours - 1;  //減中午1小時
                            //    d.T = h;
                            //}

                            //if (checkin > pm100)
                            //{
                            //    TimeSpan ts = checkout.Subtract(checkin);
                            //    int h = ts.Hours;
                            //    d.T = h;
                            //}
                            //else if (checkin > pm1200)
                            //{
                            //    TimeSpan ts = checkout.Subtract(pm100);
                            //    int h = ts.Hours;
                            //    d.T = h;
                            //}
                            //else
                            //{
                            //    TimeSpan ts = checkout.Subtract(checkin);
                            //    int h = ts.Hours - 1;  //減中午1小時
                            //    d.T = h;
                            //}
                        }
                        catch { }
                    }
                    else if (d.CheckIn == "" || d.CheckOut == "")
                    {
                        checkin = SPE_IN;
                        checkout = SPE_OUT;
                    }

                    isSPE = true;
                }

                //是否有請假紀錄
                //DataRow[] foundRows;
                var l = new List<DataRow>();
                if (p == 1)
                {
                    //foundRows = dt_leave.Select("workerNum =" + d.WorkerNum); 
                    l = (from DataRow dr in dt_leave.Rows
                         where dr["workerNum"].ToString() == d.WorkerNum
                         select dr).ToList();
                }
                else //if (p == 2)
                {
                    if (d.Date != "")
                    {
                        //foundRows = dt_leave.Select("(s_date = '" + d.Date + "') AND (e_date = '" + d.Date + "')");
                        l = (from DataRow dr in dt_leave.Rows
                             where HR_class.isBetween(Convert.ToDateTime(d.Date), Convert.ToDateTime(dr["s_date"].ToString()), Convert.ToDateTime(dr["e_date"].ToString()))
                             select dr).ToList();
                    }
                    else { continue; }
                }

                //memo str
                string str = "";
                #region 有請假
                //if (foundRows.Length > 0) //有請假紀錄
                if (l.Count > 0) //有請假紀錄
                {
                    foreach (var a in l)
                    {
                        DateTime start = Convert.ToDateTime(a["start"]);
                        DateTime end = Convert.ToDateTime(a["end"]);

                        if (a == l[0])
                        {
                            str += a["Descript"].ToString();
                            d.LeaveT = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
                            leave_start = start; leave_end = end;
                        }
                        else
                        {
                            str += "," + a["Descript"].ToString();
                            d.LeaveT2 = start.ToString("HH:mm") + "~" + end.ToString("HH:mm");
                            leave_end = end;
                        }
                    }

                    double late = 0.0;
                    d.LeaveHour = getHours(leave_start, leave_end);
                    #region 特殊打卡時間人員
                    //特殊打卡時間人員                    
                    if (isSPE == true)
                    {
                        if (leave_start.Date != am830.Date)
                        { leave_start = SPE_IN; }
                        if (leave_end.Date != pm530.Date)
                        { leave_end = SPE_OUT; }

                        if (leave_start == SPE_IN)
                        {
                            if (leave_end == SPE_OUT) { } //請整天假
                            else if (leave_end != SPE_OUT)
                            {
                                if (d.CheckIn != "" || d.CheckOut != "")
                                {
                                    try
                                    {
                                        DateTime flexT;
                                        if (leave_end == pm1200)
                                        {
                                            flexT = pm100.AddMinutes(double.Parse(flexibleT));
                                        }
                                        else
                                        {
                                            flexT = leave_end.AddMinutes(double.Parse(flexibleT));
                                        }


                                        d.T = 0.0;
                                        if (checkin <= flexT && checkin > Convert.ToDateTime(leave_end) && d.status != "3")
                                        {
                                            if (checkin > pm100)
                                            {
                                                TimeSpan ts = checkout.Subtract(checkin);
                                                double h = ts.Hours + (ts.Minutes / 60.0);
                                                d.T = h;
                                            }
                                            else if (checkin > pm1200)
                                            {
                                                TimeSpan ts = checkout.Subtract(pm100);
                                                double h = ts.Hours + (ts.Minutes / 60.0);
                                                d.T = h;
                                            }
                                            else
                                            {
                                                TimeSpan ts = checkout.Subtract(checkin);
                                                double h = ts.Hours - 1 + (ts.Minutes / 60.0);  //減中午1小時
                                                d.T = h;
                                            }
                                        }
                                    }
                                    catch { }
                                }


                                if (DateTime.Compare(leave_end, pm1200) < 0) //請假結束時間在12:00前
                                { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }
                                else if (leave_end >= pm1200 && leave_end <= pm100) //請假結束時間在12:00~13:00
                                { late = Math.Ceiling(checkin.Subtract(pm100).TotalMinutes); }
                                else if (leave_end > pm100) //請假結束時間在13:00後
                                { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }

                                if (checkout < SPE_OUT && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                        }
                        else if (leave_start > SPE_IN)
                        {
                            if (d.CheckIn != "" || d.CheckOut != "")
                            {
                                try
                                {
                                    DateTime flexT = SPE_IN.AddMinutes(double.Parse(flexibleT));
                                    d.T = 0.0;
                                    if (checkin <= flexT && checkin > SPE_IN && d.status != "3")
                                    {
                                        if (checkout > pm100)
                                        {
                                            TimeSpan ts = checkout.Subtract(checkin);
                                            double h = ts.Hours - 1 + (ts.Minutes / 60.0);   //減中午1小時
                                            d.T = h;
                                        }
                                        else if (checkout > pm1200)
                                        {
                                            TimeSpan ts = checkout.Subtract(checkin);
                                            double h = ts.Hours + (ts.Minutes / 60.0);
                                            d.T = h;
                                        }
                                        else
                                        {
                                            TimeSpan ts = checkout.Subtract(checkin);
                                            double h = ts.Hours + (ts.Minutes / 60.0);
                                            d.T = h;
                                        }
                                    }

                                }
                                catch { }
                            }

                            late = Math.Ceiling(checkin.Subtract(SPE_IN).TotalMinutes);

                            if (leave_start == pm100)
                            {
                                if (checkout < pm1200 && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                            else
                            {
                                if (checkout < leave_start && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }

                            if (leave_end >= SPE_OUT) { }
                            else if (leave_end == pm100)
                            {
                                if (checkout < pm1200 && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                            else
                            {
                                if (checkout < leave_end && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                                else if (leave_end < SPE_OUT && checkout < SPE_OUT && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                        }
                    }
                    #endregion
                    #region 一般人員
                    else
                    {
                        if (leave_start.Date != am830.Date)
                        { leave_start = am830; }
                        if (leave_end.Date != pm530.Date)
                        { leave_end = pm530; }

                        if (DateTime.Compare(leave_start, am830) == 0) //請假開始時間等於8:30
                        {
                            if (DateTime.Compare(leave_end, pm530) == 0) { } //請整天假
                            else if (leave_end != pm530)
                            {
                                if (d.CheckIn != "" || d.CheckOut != "")
                                {
                                    try
                                    {
                                        DateTime flexT;
                                        d.T = 0.0;
                                        if (leave_end == pm1200)
                                        {
                                            flexT = pm100.AddMinutes(double.Parse(flexibleT));
                                        }
                                        else
                                        {
                                            flexT = leave_end.AddMinutes(double.Parse(flexibleT));
                                        }

                                        DateTime leaveEnd_tmp = leave_end == pm1200 ? pm100 : leave_end;
                                        if (checkin <= flexT && checkin > leaveEnd_tmp && d.status != "3")
                                        {
                                            //TimeSpan ts = checkout.Subtract(checkin);
                                            if (checkin > pm100)
                                            {
                                                TimeSpan ts = checkout.Subtract(checkin);
                                                double h = ts.Hours + (ts.Minutes / 60.0);
                                                d.T = h;
                                            }
                                            else if (checkin > pm1200)
                                            {
                                                TimeSpan ts = checkout.Subtract(pm100);
                                                double h = ts.Hours + (ts.Minutes / 60.0);
                                                d.T = h;
                                            }
                                            else
                                            {
                                                TimeSpan ts = checkout.Subtract(checkin);
                                                double h = ts.Hours - 1 + (ts.Minutes / 60.0);  //減中午1小時
                                                d.T = h;
                                            }
                                        }

                                    }
                                    catch { }
                                }
                                if (DateTime.Compare(leave_end, pm1200) < 0) //請假結束時間在12:00前
                                { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }
                                else if (DateTime.Compare(leave_end, pm1200) >= 0 && DateTime.Compare(leave_end, pm100) <= 0) //請假結束時間在12:00~13:00
                                { late = Math.Ceiling(checkin.Subtract(pm100).TotalMinutes); }
                                else if (DateTime.Compare(leave_end, pm100) > 0) //請假結束時間在13:00後
                                { late = Math.Ceiling(checkin.Subtract(leave_end).TotalMinutes); }

                                if (checkout < pm530 && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                        }
                        else if (DateTime.Compare(leave_start, am830) > 0)
                        {
                            if (d.CheckIn != "" || d.CheckOut != "")
                            {
                                try
                                {
                                    DateTime flexT = am831.AddMinutes(double.Parse(flexibleT));
                                    d.T = 0.0;
                                    if (checkin <= flexT && checkin > am831 && d.status != "3")
                                    {
                                        if (checkout > pm100)
                                        {
                                            TimeSpan ts = checkout.Subtract(checkin);
                                            double h = ts.Hours - 1 + (ts.Minutes / 60.0);   //減中午1小時
                                            d.T = h;
                                        }
                                        else if (checkout > pm1200)
                                        {
                                            TimeSpan ts = checkout.Subtract(checkin);
                                            double h = ts.Hours + (ts.Minutes / 60.0);
                                            d.T = h;
                                        }
                                        else
                                        {
                                            TimeSpan ts = checkout.Subtract(checkin);
                                            double h = ts.Hours + (ts.Minutes / 60.0);
                                            d.T = h;
                                        }
                                    }

                                }
                                catch { }
                            }
                            late = Math.Ceiling(checkin.Subtract(am831).TotalMinutes);

                            if (leave_start == pm100)
                            {
                                if (checkout < pm1200 && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                            else
                            {
                                if (checkout < leave_start && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }

                            if (leave_end >= pm530) { }
                            else if (leave_end == pm100)
                            {
                                if (checkout < pm1200 && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                            else
                            {
                                if (checkout < leave_end && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                                else if (leave_end < pm530 && checkout < pm530 && d.CheckOut != "")
                                {
                                    str = strIsNullOrEmpty(str, "早退");
                                }
                            }
                        }
                    }
                    #endregion

                    if (late > 0)
                    {
                        str += ",遲到";
                    }
                }
                #endregion
                #region 沒請假
                else
                {
                    //特殊打卡時間人員                   
                    if (isSPE == true)
                    {
                        if (d.CheckIn != "" || d.CheckOut != "")
                        {
                            try
                            {
                                DateTime flexT = SPE_IN.AddMinutes(double.Parse(flexibleT));
                                d.T = 0.0;
                                if (checkin <= flexT && checkin > SPE_IN && d.status != "3")
                                {
                                    if (checkout > pm100)
                                    {
                                        TimeSpan ts = checkout.Subtract(checkin);
                                        double h = ts.Hours - 1 + (ts.Minutes / 60.0);   //減中午1小時
                                        d.T = h;
                                    }
                                    else if (checkout > pm1200)
                                    {
                                        TimeSpan ts = checkout.Subtract(checkin);
                                        double h = ts.Hours + (ts.Minutes / 60.0);
                                        d.T = h;
                                    }
                                    else
                                    {
                                        TimeSpan ts = checkout.Subtract(checkin);
                                        double h = ts.Hours + (ts.Minutes / 60.0);
                                        d.T = h;
                                    }
                                }

                            }
                            catch { }
                        }


                        if (checkin > SPE_IN)
                        {
                            str = strIsNullOrEmpty(str, "遲到");
                        }

                        if (checkout < SPE_OUT && d.CheckOut != "")
                        {
                            str = strIsNullOrEmpty(str, "早退");
                        }
                    }
                    else
                    {
                        if (d.CheckIn != "" || d.CheckOut != "")
                        {
                            try
                            {
                                DateTime flexT = am831.AddMinutes(double.Parse(flexibleT));
                                d.T = 0.0;
                                if (checkin <= flexT && checkin > am831 && d.status != "3")
                                {
                                    if (checkout > pm100)
                                    {
                                        TimeSpan ts = checkout.Subtract(checkin);
                                        double h = ts.Hours - 1 + (ts.Minutes / 60.0);   //減中午1小時
                                        d.T = h;
                                    }
                                    else if (checkout > pm1200)
                                    {
                                        TimeSpan ts = checkout.Subtract(checkin);
                                        double h = ts.Hours + (ts.Minutes / 60.0);
                                        d.T = h;
                                    }
                                    else
                                    {
                                        TimeSpan ts = checkout.Subtract(checkin);
                                        double h = ts.Hours + (ts.Minutes / 60.0);
                                        d.T = h;
                                    }
                                }

                            }
                            catch { }
                        }


                        if (checkin > am831)
                        {
                            str = strIsNullOrEmpty(str, "遲到");
                        }

                        if (checkout < pm530 && d.CheckOut != "")
                        {
                            str = strIsNullOrEmpty(str, "早退");
                        }
                    }
                }
                #endregion

                if (d.CheckIn == "" && d.CheckOut == "")
                {
                    if (string.IsNullOrEmpty(str)) { str += "整天未打卡"; }
                    else { }
                }
                else if (d.CheckIn == "")
                {
                    str = strIsNullOrEmpty(str, "上班未打卡");
                }
                else if (d.CheckOut == "")
                {
                    str = strIsNullOrEmpty(str, "下班未打卡");
                }

                if (d.status == "3") { str = str.Replace("遲到", "已彈性補回"); }

                d.memo = str;
                if (!string.IsNullOrEmpty(d.memo)) { d.log = "修正"; }
            }

            return checkData;
        }

        private static string strIsNullOrEmpty(string str, string memo)
        {
            if (string.IsNullOrEmpty(str)) { str += memo; }
            else { str += "," + memo; }

            return str;
        }
    }


    public class checkData //checkData 資料模型
    {
        public string WorkerNum { get; set; }
        public string encode_WorkerNum
        {
            get { return HR_class.encodeBase64(WorkerNum); }
        }
        public string EnFName { get; set; }
        public string ChiName { get; set; }
        private string _Date = "";
        private DateTime? _CheckIn = null, _CheckOut = null;
        public string Date
        {
            get { return _Date; }
            set { _Date = value != "" ? Convert.ToDateTime(value).ToString("yyyy-MM-dd") : ""; }
        }
        public string CheckIn
        {
            get { return _CheckIn != null ? ((DateTime)_CheckIn).ToString("HH:mm:ss") : ""; }
            set { _CheckIn = value == "" ? (DateTime?)null : Convert.ToDateTime(value.ToString()); }
        }
        public string CheckOut
        {
            get { return _CheckOut != null ? ((DateTime)_CheckOut).ToString("HH:mm:ss") : ""; }
            set { _CheckOut = value == "" ? (DateTime?)null : Convert.ToDateTime(value.ToString()); }
        }
        public string CheckType { get; set; }
        public string memo { get; set; }
        public string log { get; set; }
        public string LeaveT { get; set; }
        public string LeaveT2 { get; set; }
        public double T { get; set; }
        public double LeaveHour { get; set; }
        public string status { get; set; }
    }
}