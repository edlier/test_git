using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MisSystem_ClassLibrary;
using MSSQL_CON;
namespace misSystem
{
    public class GlobalAnnounce
    {
        //資料庫連線 
        //public static dbLibrary.db mydb = new dbLibrary.db();

        public static loginDB logindb = new loginDB();
        
        public static machine machine = new machine();
        public static id id = new id();
        public static User user = new User();
        public static Manager manager = new Manager();
        public static db db = new db();
        public static QCList QCList = new QCList(); 
        //Regulatory

        //regulatory
        public static regulatory_DCR regulatory_DCR = new regulatory_DCR();
        public static regulatory_Raar regulatory_raar = new regulatory_Raar();
        //check
        public static unconfirmed unconfirmed = new unconfirmed();
        public static overview overview = new overview();
        public static check_raa check_raa = new check_raa();
        public static check_dcr check_dcr = new check_dcr();

        public static serviceDB serviceDB = new serviceDB();
        public static customerDetailDB customerDetail = new customerDetailDB();

        public static validateSession validateSession = new validateSession();
        public static OtherFuction OtherFuction = new OtherFuction();
        public static SQL_String SQL_String = new SQL_String();

        public static dataSearching dataSearching = new dataSearching();

        
    }
}