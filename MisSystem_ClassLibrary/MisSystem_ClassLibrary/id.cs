using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary
{
    public class id
    {
        public string getName(int userID)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select EnFName,EnLName from au_computerform ";
            sqlstr += " where ID=" + publicNewClass.mydb.qo(userID.ToString());
            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt.Rows[0]["EnFName"].ToString() + dt.Rows[0]["EnLName"].ToString();
        }

    }
}
