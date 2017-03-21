using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;

namespace MisSystem_ClassLibrary.HR.personnel
{

    public class NewRecuritDB
    {
        //新增新人作業資料
        public DataTable insert_NewRecurit(string pid,string ItemID,string FileYN,string Comment,string FileName,string InsUserID,string InsDT,string CompanyID)
        {
            string sqlstr = "";

            sqlstr = "INSERT INTO hr_per_newrecruit(EmpPID, ItemID, FileYN, Comment, FileName, InsUserID, InsDT, CompanyID)"
                + " VALUES(" + pid + "," + publicNewClass.mydb.qo(ItemID) + "," + publicNewClass.mydb.qo(FileYN) + "," + publicNewClass.mydb.qo(Comment) + "," + publicNewClass.mydb.qo(FileName) + "," + InsUserID + "," + InsDT + "," + CompanyID + ")";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        public DataTable search_ALLNewRecurit()
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_per_newrecruit"
                + " ORDER BY EmpPID, ItemID ";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        public DataTable search_NewRecurit(string pid)
        {
            string sqlstr = "";

            sqlstr = "SELECT n.*,n.ItemID as Code,s.CodeDescr,s.CodeName FROM hr_per_newrecruit n"
                + " LEFT JOIN sys_code s ON(n.ItemID = s.Code and s.SysCodeType='NEWRECRUIT')"
                + " WHERE n.EmpPID=" + pid
                + " ORDER BY CodeOrder ";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        public DataTable search_isExist(string pid)
        {
            string sqlstr = "";

            sqlstr = "select * FROM hr_per_newrecruit"
                + " WHERE EmpPID=" + pid;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        public DataTable update_NewRecurit(string id, string FileYN, string FileN, string Comment, string UpdUserID, string UpdDT)
        {
            string sqlstr = "";

            sqlstr = "UPDATE hr_per_newrecruit SET FileYN=" + publicNewClass.mydb.qo(FileYN);
            if (!string.IsNullOrEmpty(FileN))
            {
                sqlstr += ", FileName=" + publicNewClass.mydb.qo(FileN);
            }
            sqlstr +=", Comment=" + publicNewClass.mydb.qo(Comment) + ", UpdUserID=" + UpdUserID + ", UpdDT=" + UpdDT
                + " WHERE ID=" + id;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }
    }
}
