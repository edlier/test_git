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
    public class EmpDB
    {

        public DataTable search_DDLEmpOTPermission(string deptID)
        {

            DataTable dt = new DataTable();

            string sqlstr = string.Empty;

            sqlstr =  " SELECT a.EmpPID, CONCAT(LPAD(a.EmpNo, 4, 0),'-', a.ChiName) AS EmpName " +
                      " FROM  `hr_per_employee` AS a ";
            sqlstr += " WHERE DeptID = '" + deptID + "' ";

            dt = publicNewClass.mydb.GetDataTable( sqlstr );

            return dt;
        }

        public DataTable search_DeptEmpNoByEmpPID(string pEmpPID)
        {

            DataTable dt = new DataTable();

            string sqlstr = string.Empty;

            sqlstr =  " SELECT a.EmpPID, a.EmpNo, a.ChiName " +
                      "      , CONCAT(LPAD(a.EmpNo, 5, 0),'-', a.ChiName) AS EmpName " +
                      "      , b.DeptPID, b.Description " +
                      "      , CONCAT(LPAD(b.DeptPID, 4,0),'-', b.Description) AS DeptName " +
                      " FROM  `hr_per_employee` AS a " +
                      " LEFT OUTER JOIN  `hr_per_dept` AS b ON b.DeptPID = a.DeptID "; 
            sqlstr += " WHERE a.EmpPID = '" + pEmpPID + "' ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }

        public DataTable search_DeptEmpNoByUserPID(string pUserPID)
        {

            DataTable dt = new DataTable();

            string sqlstr = string.Empty;

            sqlstr = " SELECT a.EmpPID, a.EmpNo, a.ChiName " +
                      "      , CONCAT(LPAD(a.EmpNo, 5, 0),'-', a.ChiName) AS EmpName " +
                      "      , b.DeptPID, b.Description " +
                      "      , CONCAT(LPAD(b.DeptPID, 4,0),'-', b.Description) AS DeptName " +
                      " FROM  `hr_per_employee` AS a " +
                      " LEFT OUTER JOIN  `hr_per_dept` AS b ON b.DeptPID = a.DeptID ";
            sqlstr += " WHERE a.UserID = '" + pUserPID + "' ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        } 

    }
 

}
