using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary.HR
{
    public class SysSetting
    {
        //取得設定檔
        public DataTable search_SysSettingCode(string moduleCode,string code)
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_sys_setting"
                + " WHERE ModuleCode=" + publicNewClass.mydb.qo(moduleCode) + " AND Code=" + publicNewClass.mydb.qo(code)
                + " ORDER BY ID";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //取得設定檔
        public DataTable search_SysSetting(string moduleCode)
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_sys_setting WHERE ModuleCode=" + publicNewClass.mydb.qo(moduleCode)
                +" ORDER BY ID";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }


    }
}
