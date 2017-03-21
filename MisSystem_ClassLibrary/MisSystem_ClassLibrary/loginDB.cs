using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{

    public class loginDB
    {
        

        public DataTable validateLogin(string id, string pwd)
        {
            DataTable dtID;
            string sqlstr="";
            //sqlstr = "select id,auDep1,auDep2,auDep3,auDep4,auDep5,auDep6,auDep7,auDep8,auDep9,auDep10,auDep11,level"
            //    + " from AU_userInfo";
            sqlstr = " SELECT EmpID,AccountID,Pwd FROM sys_user";

            sqlstr += " WHERE AccountID='" + id + "' AND Pwd = '" + pwd + "'";

            //呼叫函示做SQL指令
            dtID = publicNewClass.mydb.GetDataTable(sqlstr);

            return dtID;
        }
        //public Boolean validateUserPermission(int id, int permission)
        //{
        //    string sqlstr = "";
        //    sqlstr = "select id,permission from permissionlist ";
        //    sqlstr += "where id='" + id+"'";

        //    //呼叫函示做SQL指令
        //    dtID = publicNewClass.mydb.GetDataTable(sqlstr);

        //    //如果搜尋到的是空值
        //    if (dtID.Rows.Count == 0)
        //    {
        //        return false;
        //    }
        //    //使用者 email 和 password 有對
        //    else
        //    {
        //        return true;
        //    }
        //}

    }

}
