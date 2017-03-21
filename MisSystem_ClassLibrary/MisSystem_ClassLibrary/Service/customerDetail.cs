using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{

    public class customerDetailDB
    {
        //驗證資料類型
        //validateDataTypeDB validateDataCD = new validateDataTypeDB();
        //table和col的名
        string strTable = "customerdetail";
        string strColCustomerPID = "customerPID";
        string strColCustomerName = "customerName";
        //string strColEmail = "email";
        //string strColAddress = "address";
        //string strColPhone = "phone";
        //string strColContactName = "contactName";


        
        private bool CheckPhone(string inputInt)
        {
            try
            {
                int intNumber = int.Parse(inputInt);
                if ((int)Math.Floor(Math.Log10(intNumber)) + 2 == 10)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public DataTable getCustomerNameAndPidDataTable()
        {
            string sqlstr = "";
            sqlstr = "select " + strColCustomerPID + ", " + strColCustomerName + " from " + strTable + " ";
            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
            return myTable;
        }

        public DataTable getCommentAndPidDataTable()
        {
            string sqlstr = "";
            sqlstr = "SELECT `customerPID`,`customerName`,CONCAT_WS(',',`customerPID`,`customerName`) AS commentstr FROM `customerdetail`";
            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
            return myTable;
        }

    }

}
