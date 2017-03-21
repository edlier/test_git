using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{
    public class User
    {
        static string au_dep = "au_audep";

        public DataTable searchdept()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = doSearchForID_descrip("deps", SQLString.PString.deps);
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable auList_Option()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select auDepID,num,description from au_aulist";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_dropUserList()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = 
                SQLString.PString.select +
                SQLString.PString.id + 
                SQLString.PString.comma
                + "CONCAT_WS(','," + SQLString.PString.id + 
                SQLString.PString.comma + SQLString.au_userinfo.missysID + ") as idMISid "+ 
                SQLString.PString.from +
                SQLString.au_userinfo.dt_au_userinfo;
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable searchLevelList()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr=doSearchForID_descrip("levelList", SQLString.PString.au_aulevel);
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_UserList()
        {
            DataTable dt;
            
            string sqlstr = "";

            sqlstr =
                SQLString.PString.select +
                SQLString.PString.id +
                SQLString.PString.comma +
                SQLString.au_userinfo.missysID +
                SQLString.PString.comma;
            sqlstr += doloopfor_adDep();
            sqlstr +=
                SQLString.au_userinfo.workerNum +
                SQLString.PString.comma +
                SQLString.au_userinfo.level +
                SQLString.PString.comma +
                SQLString.au_userinfo.AD +
                SQLString.PString.comma +
                SQLString.au_userinfo.email +
                SQLString.PString.comma +
                SQLString.au_userinfo.skype +
                SQLString.PString.comma +
                SQLString.au_userinfo.misaccount +
                SQLString.PString.comma +
                SQLString.au_userinfo.leaved +
                SQLString.PString.from +
                SQLString.au_userinfo.dt_au_userinfo;

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_DepnDes(int catchID)
        {
            DataTable dt;
            string combine = "depDes";
            string sqlstr = doSearchForID_descrip(combine, au_dep);
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        //搜尋User的密碼
        public Boolean search_UserPwd(string id,string pwd) {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr = "select missysPwd from au_userinfo where id="+publicNewClass.mydb.qo(id);
            sqlstr += " AND missysPwd=" + publicNewClass.mydb.qo(pwd);
            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
                  
        }

        //搜尋剛剛加入進資料的 UserID
        public string search_userIDAdd(string workerNum)
        {
            
            string sqlstr = "select ID from au_userinfo where workerNum=" + publicNewClass.mydb.qo(workerNum.ToString());
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            string userID = dt.Rows[0]["ID"].ToString();
            return userID;
        }




        //和UserList只差在有加入Where和回傳是陣列 SQL語法幾乎一樣
        public string[] searchSingleUserInfoDetail(int id)
        {
            DataTable dt = new DataTable();
            string sqlstr = "";

            sqlstr =
                SQLString.PString.select +
                SQLString.PString.id +
                SQLString.PString.comma +
                SQLString.au_userinfo.missysID +
                SQLString.PString.comma;
            sqlstr += doloopfor_adDep();
            sqlstr +=
                SQLString.au_userinfo.workerNum +
                SQLString.PString.comma +
                SQLString.au_userinfo.level +
                SQLString.PString.comma +


                SQLString.au_userinfo.AD +
                SQLString.PString.comma +
                SQLString.au_userinfo.email +
                SQLString.PString.comma +
                SQLString.au_userinfo.skype +
                SQLString.PString.comma +
                SQLString.au_userinfo.misaccount +
                SQLString.PString.comma +
                SQLString.au_userinfo.leaved +


                SQLString.PString.from +
                SQLString.au_userinfo.dt_au_userinfo +

                SQLString.PString.where +
                SQLString.PString.id +
                SQLString.PString.equal +
                id;
            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            //呼叫函示做SQL指令
            string[] User = new string[20];

            for(int i = 0; i <20; i++)
            {
                User[i] = (dt.Rows[0][i]).ToString();
            }
                //employee[0] = (dt.Rows[0]["name"]).ToString();
                //employee[1] = (dt.Rows[0]["email"]).ToString();
                //employee[2] = (dt.Rows[0]["pwd"]).ToString();
            return User;
        }

        public void insertUserComputerForm(
            string ChiName,
            string EnFName,
            string EnLName,
            string DepID,
            string workerNum,


            string InaugurationDate,
            string ADAccount,
            string ADPwd,
            string Email,
            string Emailpwd,

            string SkypeID,
            string SkypePwd,
            string RegistrationEmail
            
            )
        {
            string sqlstr = "";
            sqlstr = "insert into au_computerform(ChiName,EnFName,EnLName,DepID,WorkerNum,"
            + "InaugurationDate,ADAccount,ADPwd,Email,Emailpwd,"
            + "SkypeID,SkypePwd,RegistrationEmail)"
            + "values("
            + publicNewClass.mydb.qo(ChiName) + ","
            + publicNewClass.mydb.qo(EnFName) + ","
            + publicNewClass.mydb.qo(EnLName) + ","
            + publicNewClass.mydb.qo(DepID) + ","
            + publicNewClass.mydb.qo(workerNum) + ","

            + publicNewClass.mydb.qo(InaugurationDate) + ","
            + publicNewClass.mydb.qo(ADAccount) + ","
            + publicNewClass.mydb.qo(ADPwd) + ","
            + publicNewClass.mydb.qo(Email) + ","
            + publicNewClass.mydb.qo(Emailpwd) + ","

            + publicNewClass.mydb.qo(SkypeID) + ","
            + publicNewClass.mydb.qo(SkypePwd) + ","
            + publicNewClass.mydb.qo(RegistrationEmail) + ")";

            publicNewClass.mydb.InsertDataTable(sqlstr);
        }
        public void insertUserInfo(int[] totalcount,string misID,string misPwd,string level,string workerNum)
        {
            //string audep2 = "auDep";
            string aa = "";
            aa = "insert into au_userinfo (";
            //for (int i = 1; i < 12; i++)
            //{
            //    aa += audep + i.ToString() + ",";
            //}
            aa += doloopfor_adDep();
            aa += "level,missysID,missysPwd,workerNum) VALUES(";
            for (int i = 1; i < 12; i++)
            {
                if (totalcount[i] > 0)
                {
                    aa += "1,";
                }
                else
                {
                    aa += "0,";
                }
            }
            aa += publicNewClass.mydb.qo(level) + ","
                + publicNewClass.mydb.qo(misID) + ","
                + publicNewClass.mydb.qo(misPwd) + ","
                + publicNewClass.mydb.qo(workerNum) + ")";
            publicNewClass.mydb.InsertDataTable(aa);
        }
        public void insertAuDepData(int dep, int[] select, int[] all, string userID)
        {
            string deps = dep.ToString();
            int min = 1;
            int max = 1;
            switch (dep)
            {
                case 3:
                    min = 1;
                    max = 2;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 4:
                    min = 1;
                    max = 3;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 5:
                    min = 1;
                    max = 1;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 6:
                    min = 1;
                    max = 1;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 7:
                    min = 1;
                    max = 1;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 8:
                    min = 1;
                    max = 1;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 9:
                    min = 1;
                    max = 1;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
                case 10:
                    min = 1;
                    max = 1;
                    insertAuDepDataTodata(au_dep + deps, select, all, userID, min, max);
                    break;
            }
        }





        private void insertAuDepDataTodata(string dep, int[] select, int[] all, string userID,int min,int max)
        {
            string aa = "";
            aa = "insert into "+dep+"(UserID";

            //先列出所有其他的欄位
            for (int x1 = min; x1 < max + 1; x1++)
            {
                aa += ",a" + all[x1].ToString();
            }

            aa += ") VALUES(" + publicNewClass.mydb.qo(userID);

            //選向若為已勾選的值為1，否則為0
            for (int x1 = min; x1 < max + 1; x1++)
            {
                if (select[x1] != 0)
                {
                    aa += "," + publicNewClass.mydb.qo("1");
                }
                else
                {
                    aa += "," + publicNewClass.mydb.qo("0");
                }
            }
            aa += ")";
            publicNewClass.mydb.InsertDataTable(aa);
        }
        

        //合併ID_description字串
        private string doSearchForID_descrip(string combineName,string tableName)
        {
            string sqlstr = "";
            sqlstr =
                SQLString.PString.select +
                SQLString.PString.id +
                SQLString.PString.comma +
                "CONCAT_WS(','," +
                SQLString.PString.id +
                SQLString.PString.comma +
                SQLString.PString.Description +
                ") as " + combineName+
                SQLString.PString.from +
                tableName;
            return sqlstr;
        }
        private string doloopfor_adDep()
        {
            string audep = "auDep";
            string aa="";
            for (int i = 1; i < 12; i++)
            {
                aa += audep + i.ToString() + ",";
            }
            return aa;
        }
    }
}
