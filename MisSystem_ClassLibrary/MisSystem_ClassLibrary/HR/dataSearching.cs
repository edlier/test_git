using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary
{
    public class dataSearching
    {
        public DataTable search_leaveReasonList()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += "SELECT * FROM hr_leave_reason";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public void insert_leaveData(string workerNum, string leaveReasonID, string start_time, string end_time,string leaveReason)
        {

            //INSERT INTO hr_application_leave

            //(workerNum,leaveReason,start_time,end_time,status)

            //VALUES(

            string aa;
            aa = " INSERT INTO hr_application_leave "
                + "(workerNum,leave_reason,start_time,end_time,status,content)"
                + " values ("
                + publicNewClass.mydb.qo(workerNum) + ","
                + publicNewClass.mydb.qo(leaveReasonID) + ","
                + publicNewClass.mydb.qo(start_time) + ","
                + publicNewClass.mydb.qo(end_time) + ","
                + publicNewClass.mydb.qo("0")+","
                + publicNewClass.mydb.qo(leaveReason) + ")";


            publicNewClass.mydb.InsertDataTable(aa);
        }

        public DataTable search_user(string id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select c.EnFName from hr_per_employee c where c.EmpPID=" + id;

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_worker(string id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select c.ChiName,c.EnLName, c.DeptID from hr_per_employee c where c.EmpNo=" + id + ";";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_check(string id, string date)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select c.Id, a.EmpNo, c.CheckTime, c.CheckType, c.status from hr_per_employee a left join hr_checkinout c "
            + "on (a.EmpNo=c.WorkerNum and date(c.CheckTime)='" + date + "')"
            + " where a.EmpNo=" + id + " AND c.isDel='X'" + " ORDER BY CheckTime ASC";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public void insert_checkinout(int style, string WorkerNum, string CheckTime, string CheckType)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "INSERT INTO hr_checkinout(WorkerNum, CheckTime, CheckType, isDel, status) VALUES ("
                    + WorkerNum + ",'" + CheckTime + "','" + CheckType + "','X',";
            if (style == 1) //update_btn
            {
                sqlstr +="1)";
            }
            else if (style == 2) //transfer_btn
            {
                sqlstr += "2)";
            }
            else if (style == 3) //彈性補回_btn
            {
                sqlstr += "3)";
            }
            
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public void update_checkinout(string id, string updateT, string WorkerNum, string CheckTime, string CheckType)
        { //personal 彈性補登用
            DataTable dt;
            string sqlstr = "";
            sqlstr = "Update hr_checkinout SET isDel='O', updateID='" + id + "', updateTime='" + updateT + "' WHERE WorkerNum=" + WorkerNum + " and CheckTime='" + CheckTime + "' and CheckType='" + CheckType + "'";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public void update_checkinout(string updateId, string updateT, string id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "Update hr_checkinout SET isDel='O', updateID='" + updateId + "', updateTime='" + updateT + "' WHERE Id=" + id;

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable search_holidays(int y,int m)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select h.Date from hr_holidays h where year(h.Date)='" + y + "' and month(h.Date)="+publicNewClass.mydb.qo(m.ToString())
                +" Group by h.Date";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public int count_checkinout(string WorkerNum, string date)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT count(CheckTime) FROM hr_checkinout WHERE date(CheckTime)='" + date + "' and WorkerNum=" + WorkerNum;

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            int count = int.Parse(dt.Rows[0]["count(CheckTime)"].ToString());
            return count;
        }

        public DataTable select_allEmployee(string time)
        {
            DataTable dt;
            string sqlstr = "";

            sqlstr = "select c.EmpNo, c.EnFName, c.ChiName, CONCAT(c.EmpNo,',',c.ChiName,', ',c.EnFName) as Name, c.OnboardDT, c.ResignationDT from hr_per_employee c  "
                //+ " join au_userinfo on(au_userinfo.WorkerNum=c.EmpNo) "
                + " left join hr_specialperson h on(c.EmpNo=h.workerNum)"
                + " WHERE "
                + "  ( c.OnboardDT<=" + publicNewClass.mydb.qo(time + "-31") + " AND (c.ResignationDT >=" + publicNewClass.mydb.qo(time + "-1") + " OR c.ResignationDT<=>NULL) AND ((h.type = 2) OR (h.type = 1 AND h.isDel ='O') OR (h.type = 3 AND h.isDel ='O')   OR h.type<=>NULL)) "
                + " ORDER BY EmpNo ASC;";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable select_allEmployee(string time,string WorkerNum)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select c.EmpNo, c.EnFName, c.ChiName, CONCAT(c.EmpNo,',',c.ChiName,', ',c.EnFName) as Name, c.OnboardDT, c.ResignationDT from hr_per_employee c "
                //+ " join au_userinfo on(au_userinfo.WorkerNum=c.EmpNo) "
                + " WHERE c.EmpNo=" + WorkerNum;
            //+ " AND au_userinfo.leaved=0 "
            //+" ORDER BY WorkerNum ASC;";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_spcialPersonType()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr = "SELECT * FROM hr_specialpersontype";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_sysTime(int i,string time) //1:全部 2:查詢checkin/checkout
        {
            DataTable dt = new DataTable();
            string sqlstr = "SELECT * FROM hr_sys_time";
            
            if (i == 1) { sqlstr += " ORDER BY time ASC";}
            else if (i == 2) { sqlstr += " WHERE id=" + time; }

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public void insert_specialperson(string workerNum, string checkin, string checkout, string type)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "INSERT INTO hr_specialperson(workerNum, checkin, checkout, type)  VALUES (";
            if (checkin == "***") { checkin = "NULL"; }
            if (checkout == "***") { checkout = "NULL"; }
            sqlstr += workerNum + "," + checkin + "," + checkout + "," + type + ")";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        //public void update_userinfo(string workerNum, string date)
        //{
        //    DataTable dt;
        //    string sqlstr = "";
        //    sqlstr = "UPDATE au_userinfo SET leaved=1, leavedDT=" + publicNewClass.mydb.qo(date) + " WHERE workerNum=" + workerNum;

        //    dt = publicNewClass.mydb.GetDataTable(sqlstr);
        //}

        public void update_empResignation(string workerNum, string date, string userID)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "UPDATE hr_per_employee SET ResignationDT=" + publicNewClass.mydb.qo(date) + ", UpdUserID=" + userID + ", UpdDT=NOW()" + " WHERE EmpNo=" + workerNum;

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable search_specialperson(int i,string data)//i=1->data=workerNum, i=2->data=id
        {
            DataTable dt = new DataTable();
            string sqlstr = "SELECT * FROM hr_specialperson WHERE";
            if (i == 1) { sqlstr += " workerNum=" + data+" AND isDel='X'"; }
            else if (i == 2) { sqlstr += " id=" + data+" AND isDel='X'"; }
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_specialT()
        {
            DataTable dt = new DataTable();
            string sqlstr = "SELECT h.*,s.time as 'inT',sy.time as 'outT' FROM hr_specialperson h LEFT JOIN hr_sys_time s on (h.checkin=s.id) LEFT JOIN hr_sys_time sy on (h.checkout=sy.id) WHERE type=2 AND isDel='X'";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        } 

        public DataTable search_spePersonTable()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr = "SELECT s.*,spt.Descript, st.time as 'inT', st2.time as 'outT', c.ChiName FROM hr_specialperson s";
            sqlstr += " LEFT JOIN hr_sys_time st on(s.checkin=st.id)";
            sqlstr += " LEFT JOIN hr_sys_time st2 on(s.checkout=st2.id)";
            sqlstr += " LEFT JOIN hr_per_employee c on(s.workerNum=c.EmpNo)";
            sqlstr += " LEFT JOIN hr_specialpersontype spt on(s.type=spt.id)";
            sqlstr += " WHERE isDel='X' ORDER BY s.workerNum";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public void update_specialperson(int num, string id, string delT, string checkin, string checkout, string type)//1:del 2:update
        {
            DataTable dt;
            string sqlstr = "";
            if (num == 1)
            {
                sqlstr = "Update hr_specialperson SET isDel='O', Deltime=" + publicNewClass.mydb.qo(delT) + "WHERE id=" + id;
            }
            else if (num == 2)
            {
                if (checkin == "***") { checkin = "NULL"; }
                if (checkout == "***") { checkout = "NULL"; }
                sqlstr = "Update hr_specialperson SET checkin="+checkin + ", checkout=" + checkout + ", type=" + type + " WHERE id=" + id;
            }

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        //type -->  1=hr_application_leave, 2=hr_application_workingout
        public void update_leave(string workerNum, string startT, string endT, string status, string type, string delT, string sn) 
        {
            DataTable dt;
            string sqlstr = "";
            if (type == "1")
            {
                sqlstr = "Update hr_application_leave SET isDelete=1, DeleteT=" + publicNewClass.mydb.qo(delT)
                    + " WHERE SN=" + sn;
                    //+ " WHERE workerNum=" + workerNum + " AND start_time=" + publicNewClass.mydb.qo(startT) + " AND end_time="
                    //+ publicNewClass.mydb.qo(endT) + " AND status=" + status;
            }
            else if (type == "2")
            {
                sqlstr = "Update hr_application_workingout SET isDelete=1, DeleteT=" + publicNewClass.mydb.qo(delT)
                    + " WHERE SN=" + sn;
                    //+" WHERE workerNum=" + workerNum + " AND start_time=" + publicNewClass.mydb.qo(startT) + " AND end_time="
                    //+ publicNewClass.mydb.qo(endT) + " AND status=" + status;
            }
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable search_DayCheckinout(string time, string workerNum)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select a.EmpNo, a.EnFName, a.ChiName, MIN(c.CheckTime) as checkin, MIN(c2.CheckTime) as checkout, c.status from hr_per_employee a"
                + " left join hr_checkinout c on (a.EmpNo=c.WorkerNum and date(c.CheckTime)='" + time + "' and c.CheckType='I' AND (c.isDel='X' OR c.isDel<=>NULL))"
                + " left join hr_checkinout c2 on (a.EmpNo=c2.WorkerNum and date(c2.CheckTime)='" + time + "' and c2.CheckType='O' AND (c2.isDel='X' OR c2.isDel<=>NULL))"
                //+ " join au_userinfo on(au_userinfo.WorkerNum=a.EmpNo)"
                + " left join hr_specialperson h on(a.EmpNo=h.workerNum)"
                + " WHERE ( (a.OnboardDT<='" + time + "' AND (a.ResignationDT >= '" + time + "' OR a.ResignationDT<=>NULL))  AND ((h.type = 2) OR (h.type = 1 AND h.isDel ='O') OR (h.type = 3)   OR h.type<=>NULL) )";
            if (workerNum != "***") { sqlstr += " and a.EmpNo=" + workerNum; }
            sqlstr += " GROUP BY EmpNo ORDER BY EmpNo ASC";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_DayLeave(string time)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select l.workerNum, r.Descript, l.start_time as start, l.end_time as end "
                + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
                + "where ('" + time + "' BETWEEN date(l.start_time) and date(l.end_time)) and l.isDelete=0 "
                + "UNION "
                + "select w.workerNum, '外出' as Descripit, w.start_time as start, w.end_time as end "
                + "from hr_application_workingout w "
                + "where ('" + time + "' BETWEEN date(w.start_time) and date(w.end_time)) and w.isDelete=0"
                + " ORDER BY start, workerNum ASC;";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_PersonalCheckinout(string year, string month, string workerNum)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select a.EmpNo, a.EnFName, a.ChiName, c.CheckTime as checkin, c2.CheckTime as checkout, date(c.CheckTime) as Date, c.status from hr_per_employee a"
                + " left join hr_checkinout c on ( a.EmpNo=c.WorkerNum and year(c.CheckTime)=" + publicNewClass.mydb.qo(year) + " and month(c.CheckTime)=" + publicNewClass.mydb.qo(month) + " and c.CheckType='I' and (c.isDel='X' OR c.isDel<=>NULL))"
                + " left join hr_checkinout c2 on ( a.EmpNo=c2.WorkerNum and date(c2.CheckTime) = date(c.CheckTime) and c2.CheckType='O' and (c2.isDel='X' OR c2.isDel<=>NULL))"
                + " WHERE a.EmpNo=" + workerNum
                + " GROUP BY Date"
                + " UNION"
                + " select a.EmpNo, a.EnFName, a.ChiName, c2.CheckTime as checkin, c.CheckTime as checkout, date(c.CheckTime) as Date, c2.status from hr_per_employee a"
                + " left join hr_checkinout c on ( a.EmpNo=c.WorkerNum and year(c.CheckTime)=" + publicNewClass.mydb.qo(year) + " and month(c.CheckTime)=" + publicNewClass.mydb.qo(month) + " and c.CheckType='O' and (c.isDel='X' OR c.isDel<=>NULL))"
                + " left join hr_checkinout c2 on ( a.EmpNo=c2.WorkerNum and date(c2.CheckTime) = date(c.CheckTime) and c2.CheckType='I' and (c2.isDel='X' OR c2.isDel<=>NULL))"
                + " WHERE a.EmpNo=" + workerNum
                + " GROUP BY Date ORDER BY Date ASC";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_PersonalLeave(string year, string month, string workerNum)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select l.workerNum, r.Descript, l.start_time as start, l.end_time as end, date(l.start_time) as s_date, date(l.end_time) as e_date "
                + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
                + "where year(l.start_time)=" + publicNewClass.mydb.qo(year) + " and "
                + "month(l.start_time)=" + publicNewClass.mydb.qo(month) + " and "
                + "l.workerNum=" + workerNum + " and l.isDelete=0 "
                + "UNION "
                + "select w.workerNum, '外出' as Descripit, w.start_time as start, w.end_time as end, date(w.start_time) as s_date, date(w.end_time) as e_date "
                + "from hr_application_workingout w "
                + "where year(w.start_time)=" + publicNewClass.mydb.qo(year) + " and "
                + "month(w.start_time)=" + publicNewClass.mydb.qo(month) + " and "
                + "w.workerNum=" + workerNum + " and w.isDelete=0 "
                + " ORDER BY start ASC;";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_MonthLeave(string year, string month)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select l.workerNum, l.start_time, l.end_time, r.Descript as memo, l.status as s "
                + "from hr_application_leave l inner join hr_leave_reason r on l.leave_reason = r.ID "
                + "where year(l.start_time)='" + year + "' AND month(l.start_time)='" + month + "'  AND l.isDelete=0 "
                + "UNION "
                + "select w.workerNum, w.start_time, w.end_time, '外出', w.status "
                + "from hr_application_workingout w "
                + "where year(w.start_time)='" + year + "' AND month(w.start_time)='" + month + "'  AND w.isDelete=0 "
                +"order by start_time ASC;";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_Parttime(string time)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select a.EmpNo, a.EnFName, a.ChiName"
            + " from hr_per_employee a"
            //+ " join au_userinfo on(au_userinfo.WorkerNum=a.EmpNo)"
            + " left join hr_specialperson h on(a.EmpNo=h.workerNum)"
            + " WHERE a.OnboardDT<='" + time + "-31' AND (a.ResignationDT >= '" + time + "-1' OR a.ResignationDT<=>NULL) AND (h.type = 3 AND h.isDel ='X' )"
            + " ORDER BY EmpNo ASC";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
    }
}
