using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{
    public class machine
    {
        public DataTable searchMachine()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select id,description from product ";
            sqlstr += " ORDER BY id ASC ";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
    }
}
