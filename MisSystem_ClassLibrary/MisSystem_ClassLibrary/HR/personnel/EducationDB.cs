using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary.HR.personnel
{
    public class EducationDB
    {

        //取得單一員工學歷資料
        public DataTable search_EmpEduDetail(string pid)
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_per_education WHERE EmpPID=" + pid;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //新增學歷資料
        public void insert_EduData(string pid,string schoolID,string majorID,string degreeID,string startD,string endD,string status,string comments,string userID,string company)
        {
            string sqlstr = "";

            sqlstr = "INSERT INTO hr_per_education(EmpPID, School, Major, DegreeID, StartDT, EndDT, StatusID, Comments, UpdUserID, UpdDT, CompanyID)";
            sqlstr += " VALUES";
            sqlstr += "(";
            sqlstr += pid + ","
                + publicNewClass.mydb.qo(schoolID) + ","
                + publicNewClass.mydb.qo(majorID) + ","
                + publicNewClass.mydb.qo(degreeID) + ",";

            if (startD == string.Empty)
            { sqlstr += "NULL,"; }
            else
            { sqlstr += publicNewClass.mydb.qo(startD) + ","; }

            if (endD == string.Empty)
            { sqlstr += "NULL,"; }
            else
            { sqlstr += publicNewClass.mydb.qo(endD) + ","; }

            sqlstr += publicNewClass.mydb.qo(status) + ","
                + publicNewClass.mydb.qo(comments) + ","
                + userID + ","
                + "NOW(),"
                + publicNewClass.mydb.qo(company);
            sqlstr += ")";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        //修改學歷資料
        public void update_EduData(string id, string schoolID, string majorID, string degreeID, string startD, string endD, string status, string comments, string userID, string company)
        {
            string sqlstr = "";

            sqlstr = "UPDATE hr_per_education";
            sqlstr += " SET";
            sqlstr += " School=" + publicNewClass.mydb.qo(schoolID) + ","
                + " Major=" + publicNewClass.mydb.qo(majorID) + ","
                + " DegreeID=" + publicNewClass.mydb.qo(degreeID) + ",";

            if (startD == string.Empty) { }
            else { sqlstr += " StartDT=" + publicNewClass.mydb.qo(startD) + ","; }
            if (endD == string.Empty) { }
            else { sqlstr += " EndDT=" + publicNewClass.mydb.qo(endD) + ","; }

            sqlstr += " StatusID=" + publicNewClass.mydb.qo(status) + ","
                + " Comments=" + publicNewClass.mydb.qo(comments) + ","
                + " UpdUserID=" + userID + ","
                + " UpdDT=" + "NOW(),"
                + " CompanyID=" + company;
            sqlstr += " WHERE ID=" + id;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }
}
