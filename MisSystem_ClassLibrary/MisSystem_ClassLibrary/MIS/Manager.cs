using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{
    public class Manager
    {
        public void insertau_managerlist(string userID, string depID, string level)
        {
            string aa = "";
            aa =
                SQLString.PString.insert_into +
                SQLString.PString.au_managerlist +
                SQLString.PString.Lparentheses +
                SQLString.PString.userID +
                SQLString.PString.comma +
                SQLString.au_userinfo.level +
                SQLString.PString.comma +
                SQLString.PString.depID +
                SQLString.PString.Rparentheses +
                SQLString.PString.VALUES +
                SQLString.PString.Lparentheses +
                userID +
                SQLString.PString.comma +
                level +
                SQLString.PString.comma +
                depID +
                SQLString.PString.Rparentheses;
            publicNewClass.mydb.InsertDataTable(aa);
        }
        public void deleteManager(string id)
        {
            string sqlstr = "";
            sqlstr =
                SQLString.PString.deleteFrom +
                SQLString.PString.au_managerlist +
                SQLString.PString.where +
                SQLString.PString.id +
                SQLString.PString.equal +
                publicNewClass.mydb.qo(id);

            publicNewClass.mydb.InsertDataTable(sqlstr);
        }
        public DataTable search_ManagerList()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr =
                SQLString.PString.select +
                SQLString.PString.au_managerlist +
                SQLString.PString.dot +
                SQLString.PString.id +
                SQLString.PString.ASS + publicNewClass.mydb.qo("ID") +

                SQLString.PString.comma +
                SQLString.PString.au_managerlist +
                SQLString.PString.dot +
                SQLString.PString.userID +
                SQLString.PString.ASS + publicNewClass.mydb.qo("userID") +

                SQLString.PString.comma +
                SQLString.au_userinfo.dt_au_userinfo +
                SQLString.PString.dot +
                SQLString.au_userinfo.missysID +
                SQLString.PString.ASS + publicNewClass.mydb.qo("missysID") +
                SQLString.PString.comma +

                SQLString.PString.au_managerlist +
                SQLString.PString.dot +
                SQLString.au_userinfo.level +
                SQLString.PString.ASS +publicNewClass.mydb.qo("level")+
                SQLString.PString.comma +

                SQLString.PString.au_managerlist +
                SQLString.PString.dot +
                SQLString.PString.depID +
                SQLString.PString.ASS +publicNewClass.mydb.qo("depID")+

                SQLString.PString.from +
                SQLString.PString.au_managerlist +

                SQLString.PString.join +
                SQLString.au_userinfo.dt_au_userinfo +

                SQLString.PString.on +
                SQLString.PString.Lparentheses +
                SQLString.PString.au_managerlist +
                SQLString.PString.dot +
                SQLString.PString.userID +
                SQLString.PString.equal +
                SQLString.au_userinfo.dt_au_userinfo +
                SQLString.PString.dot +
                SQLString.PString.id+
                SQLString.PString.Rparentheses;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }


    }
}
