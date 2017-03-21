using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary.HR.personnel
{
    public class ExperienceDB
    {
        //取得單一員工經歷資料
        public DataTable search_EmpExpDetail(string pid)
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_per_experience WHERE EmpPID=" + pid;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //新增經歷資料
        public void insert_ExpData(string pid, string companyN, string department, string position, string startD, string endD, string seniority, string leavedRsnID, string leavedOtherRsn, string leavedNote, string Achievement, string userID, string company)
        {
            string sqlstr = "";

            sqlstr = "INSERT INTO hr_per_experience(EmpPID, CompanyName, Department, Position, StartDT, EndDT, Seniority, LeavedRsnID, LeavedOtherRsn, LeavedNote, Achievement, UpdUserID, UpdDT, CompanyID)";
            sqlstr += " VALUES";
            sqlstr += "(";
            sqlstr += pid + ","
                + publicNewClass.mydb.qo(companyN) + ","
                + publicNewClass.mydb.qo(department) + ","
                + publicNewClass.mydb.qo(position) + ",";
            
            if (startD == string.Empty)
            { sqlstr += "NULL,"; }
            else
            { sqlstr += publicNewClass.mydb.qo(startD) + ","; }

            if (endD == string.Empty)
            { sqlstr += "NULL,"; }
            else
            { sqlstr += publicNewClass.mydb.qo(endD) + ","; }

            sqlstr += publicNewClass.mydb.qo(seniority) + ","
                + publicNewClass.mydb.qo(leavedRsnID) + ","
                + publicNewClass.mydb.qo(leavedOtherRsn) + ","
                + publicNewClass.mydb.qo(leavedNote) + ","
                + publicNewClass.mydb.qo(Achievement) + ","
                + userID + ","
                + "NOW(),"
                + publicNewClass.mydb.qo(company);
            sqlstr += ")";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }


        public void update_ExpData(string expID, string companyN, string department, string position, string startD, string endD, string seniority, string leavedRsnID, string leavedOtherRsn, string leavedNote, string Achievement, string userID, string company)
        {
            string sqlstr = "";

            sqlstr = "UPDATE hr_per_experience";
            sqlstr += " SET";
            sqlstr += " CompanyName=" + publicNewClass.mydb.qo(companyN) + ","
                + " Department=" + publicNewClass.mydb.qo(department) + ","
                + " Position=" + publicNewClass.mydb.qo(position) + ",";

            if (startD == string.Empty) { }
            else { sqlstr += " StartDT=" + publicNewClass.mydb.qo(startD) + ","; }
            if (endD == string.Empty) { }
            else { sqlstr += " EndDT=" + publicNewClass.mydb.qo(endD) + ","; }

            sqlstr += " Seniority=" + publicNewClass.mydb.qo(seniority) + ","
                + " LeavedRsnID=" + publicNewClass.mydb.qo(leavedRsnID) + ","
                + " LeavedOtherRsn=" + publicNewClass.mydb.qo(leavedOtherRsn) + ","
                + " LeavedNote=" + publicNewClass.mydb.qo(leavedNote) + ","
                + " Achievement=" + publicNewClass.mydb.qo(Achievement) + ","
                + " UpdUserID=" + userID + ","
                + " UpdDT=" + "NOW()" + ","
                + " CompanyID=" + publicNewClass.mydb.qo(company);
            sqlstr += " WHERE ID=" + expID;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }
}
