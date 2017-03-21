using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary.HR.personnel
{
    public class ChangeDB
    {
        //取得單一員工異動資料
        public DataTable search_EmpChaDetail(string pid)
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_per_change WHERE EmpPID=" + pid;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //取得員工異動ID
        public DataTable search_ChaID(string pid, string status)
        {
            string sqlstr = "";

            sqlstr = "SELECT ID FROM hr_per_change WHERE EmpPID=" + pid + " AND StatusID=" + publicNewClass.mydb.qo(status);

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //新增異動紀錄
        public void insert_ChaData(string pid,string boardID,string dept,string position, string chaD,string status,string promSeniority,string promAnnL, string sortOrder,string userID,string company)
        {
            string sqlstr = "";

            sqlstr += "INSERT INTO hr_per_change(EmpPID, DeptID, Position, changeDT, StatusID, PromiseSeniority, PromiseAnnLeave, SortOrder, UpdUserID, UpdDT, CompanyID, BoardID)";
            sqlstr += " VALUES";
            sqlstr += "(";
            sqlstr += pid + ","
                + publicNewClass.mydb.qo(dept) + ","
                + publicNewClass.mydb.qo(position) + "," //職稱
                + publicNewClass.mydb.qo(chaD) + ","
                + publicNewClass.mydb.qo(status) + "," //狀態(晉升、轉調、調任、降職、留職停薪) 1:到職
                + publicNewClass.mydb.qo(promSeniority) + "," //約定年資
                + publicNewClass.mydb.qo(promAnnL) + "," //約定特休
                + publicNewClass.mydb.qo(sortOrder) + "," //排序(到職一定為1)
                + userID + ","
                + "NOW(),"
                + company+ ",";
            if (!string.IsNullOrEmpty(boardID)) { sqlstr += publicNewClass.mydb.qo(boardID); }//公告ID
            else { sqlstr += "NULL"; }
            sqlstr += ");";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        //修改異動紀錄
        public void update_ChaData(string chaDT, string userID,string chaID)
        {
            string sqlstr = "";

            sqlstr += " UPDATE hr_per_change"
                    + " SET"
                    + " changeDT=" + publicNewClass.mydb.qo(chaDT) + ","
                    + " UpdUserID=" + userID + ","
                    + " UpdDT=" + "NOW()"
                    + " WHERE ID=" + chaID;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }
}
