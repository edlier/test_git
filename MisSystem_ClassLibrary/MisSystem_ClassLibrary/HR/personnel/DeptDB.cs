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

    public class DeptDB
    {

        public DataTable search_Dept()
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT a.* " +
                     " FROM  `hr_per_dept` AS a " +
                     " WHERE  ( `VersionEndDate` IS NULL OR DATE(`VersionEndDate`) >= DATE(NOW()) ) " +
                     " ORDER BY a.CompanyOrder ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }

        public DataTable search_DeptByComapnyID(string pKey)
        {
            if (string.IsNullOrEmpty(pKey)) { pKey = "0"; }
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT a.DeptPID, a.Description " +
                     " FROM  `hr_per_dept` AS a WHERE `CompanyID` = " + pKey + " AND `ActiveYN`='Y'";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }

        public DataTable search_DDLDeptOTPermission(string companyID)
        {

            DataTable dt = new DataTable();

            string sqlstr = string.Empty;

            sqlstr  = " SELECT a.DeptPID, a.Description " +
                      " FROM  `hr_per_dept` AS a ";
            sqlstr += " WHERE CompanyID = '" + companyID + "' AND OT_Permission = '1' ";

            dt = publicNewClass.mydb.GetDataTable( sqlstr );

            return dt;
        }

        public bool UpdateRecords_Dept(DeptCls slCls, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                string query = " UPDATE sys_Company SET " +
                    //"   CompanyCode = @CompanyCode " +
                    "   CompanyName = @CompanyName " +
                    " , CompanyFullName = @CompanyFullName " +
                    " , Principal = @Principal " +
                    " , CompanyTel = @CompanyTel " +
                    " , CompanyFax = @CompanyFax " +
                    " , CompanyEmail = @CompanyEmail " +
                    " , CompanyWebSite = @CompanyWebSite " +
                    " , CompanyAddress = @CompanyAddress " +
                    " , CompanyLocation = @CompanyLocation " +
                    " , BeneficiaryName = @BeneficiaryName " +
                    " , BeneficiaryBank = @BeneficiaryBank " +
                    " , AccountNO = @AccountNO " +
                    " , SwiftCode = @SwiftCode " +
                    " , CountryCode = @CountryCode " +
                    " , CompanyOrder = @CompanyOrder " +
                    " , ActiveYN = @ActiveYN " +
                    " , CompanyAddress = @CompanyAddress " +
                    " , UpdUserID = @UpdUserID " +
                    " , UpdDT = NOW() " +
                    " WHERE CompanyPID = @CompanyPID ";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CompanyPID", slCls.CompanyPID);
                    //cmd.Parameters.AddWithValue("@CompanyCode", slCls.CompanyCode);
                    cmd.Parameters.AddWithValue("@CompanyName", slCls.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyFullName", slCls.CompanyFullName);
                    cmd.Parameters.AddWithValue("@Principal", slCls.Principal);
                    cmd.Parameters.AddWithValue("@CompanyTel", slCls.CompanyTel);
                    cmd.Parameters.AddWithValue("@CompanyFax", slCls.CompanyFax);
                    cmd.Parameters.AddWithValue("@CompanyEmail", slCls.CompanyEmail);
                    cmd.Parameters.AddWithValue("@CompanyWebSite", slCls.CompanyWebSite);
                    cmd.Parameters.AddWithValue("@CompanyAddress", slCls.CompanyAddress);
                    cmd.Parameters.AddWithValue("@CompanyLocation", slCls.CompanyLocation);
                    cmd.Parameters.AddWithValue("@BeneficiaryName", slCls.BeneficiaryName);
                    cmd.Parameters.AddWithValue("@BeneficiaryBank", slCls.BeneficiaryBank);
                    cmd.Parameters.AddWithValue("@AccountNO", slCls.AccountNO);
                    cmd.Parameters.AddWithValue("@SwiftCode", slCls.SwiftCode);
                    cmd.Parameters.AddWithValue("@CountryCode", slCls.CountryCode);
                    cmd.Parameters.AddWithValue("@CompanyOrder", slCls.CompanyOrder);
                    cmd.Parameters.AddWithValue("@ActiveYN", slCls.ActiveYN);
                    cmd.Parameters.AddWithValue("@UpdUserID", slCls.UpdUserID);

                    liExecute = publicNewClass.mydb.ExecuteCmd(query, 300, cmd.Parameters, out returnMessage);
                    b = (liExecute == -1) ? false : true;
                }
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                liExecute = -1;
            }

            b = liExecute == -1 ? false : true;
            return b;
        }

        public bool InsertRecords_Dept(DeptCls slCls, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                string query = " INSERT INTO sys_Company (CompanyCode, CompanyName " +
                               " , CompanyFullName, Principal, CompanyTel, CompanyFax " +
                               " , CompanyEmail, CompanyWebSite, CompanyAddress, CompanyLocation " +
                               " , BeneficiaryName, BeneficiaryBank, AccountNO, SwiftCode " +
                               " , CountryCode, CompanyOrder, ActiveYN, UpdUserID, UpdDT) VALUES " +
                               " ( @CompanyCode, @CompanyName " +
                               " , @CompanyFullName, @Principal, @CompanyTel, @CompanyFax " +
                               " , @CompanyEmail, @CompanyWebSite, @CompanyAddress, @CompanyLocation " +
                               " , @BeneficiaryName, @BeneficiaryBank, @AccountNO, @SwiftCode " +
                               " , @CountryCode, @CompanyOrder, @ActiveYN, @UpdUserID " +
                               " , NOW() ) ";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@CompanyPID", slCls.CompanyPID);
                    cmd.Parameters.AddWithValue("@CompanyCode", slCls.CompanyCode);
                    cmd.Parameters.AddWithValue("@CompanyName", slCls.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyFullName", slCls.CompanyFullName);
                    cmd.Parameters.AddWithValue("@Principal", slCls.Principal);
                    cmd.Parameters.AddWithValue("@CompanyTel", slCls.CompanyTel);
                    cmd.Parameters.AddWithValue("@CompanyFax", slCls.CompanyFax);
                    cmd.Parameters.AddWithValue("@CompanyEmail", slCls.CompanyEmail);
                    cmd.Parameters.AddWithValue("@CompanyWebSite", slCls.CompanyWebSite);
                    cmd.Parameters.AddWithValue("@CompanyAddress", slCls.CompanyAddress);
                    cmd.Parameters.AddWithValue("@CompanyLocation", slCls.CompanyLocation);
                    cmd.Parameters.AddWithValue("@BeneficiaryName", slCls.BeneficiaryName);
                    cmd.Parameters.AddWithValue("@BeneficiaryBank", slCls.BeneficiaryBank);
                    cmd.Parameters.AddWithValue("@AccountNO", slCls.AccountNO);
                    cmd.Parameters.AddWithValue("@SwiftCode", slCls.SwiftCode);
                    cmd.Parameters.AddWithValue("@CountryCode", slCls.CountryCode);
                    cmd.Parameters.AddWithValue("@CompanyOrder", slCls.CompanyOrder);
                    cmd.Parameters.AddWithValue("@ActiveYN", slCls.ActiveYN);
                    cmd.Parameters.AddWithValue("@UpdUserID", slCls.UpdUserID);

                    liExecute = publicNewClass.mydb.ExecuteCmd(query, 300, cmd.Parameters, out returnMessage);
                    b = (liExecute == -1) ? false : true;
                }
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                liExecute = -1;
            }

            b = liExecute == -1 ? false : true;
            return b;
        }

        //取得是否允許彈性補回
        public DataTable search_DeptFlextimeYN(int deptID)
        {
            string sqlstr = "";

            sqlstr = "SELECT * FROM hr_per_dept"
                + " WHERE DeptPID=" + deptID;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

    }

    public class DeptCls
    {
        public string CompanyPID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyFullName { get; set; }
        public string Principal { get; set; }
        public string CompanyTel { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebSite { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyLocation { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryBank { get; set; }
        public string AccountNO { get; set; }
        public string SwiftCode { get; set; }
        public string CountryCode { get; set; }
        public string CompanyOrder { get; set; }
        public string CompanyCustPID { get; set; }
        public string CompanyVersion { get; set; }
        public string VersionEndDate { get; set; }
        public string PrevVersion { get; set; }
        public string UpdUserID { get; set; }
        public string ActiveYN { get; set; }
    }

}
