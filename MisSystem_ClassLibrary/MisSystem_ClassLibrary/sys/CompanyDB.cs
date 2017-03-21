using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;


namespace MisSystem_ClassLibrary.sys
{
    public class CompanyDB
    {
        public DataTable search_Company()
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT a.* " +
                     " FROM  `sys_company` AS a " +
                     " WHERE  ( `VersionEndDate` IS NULL OR DATE(`VersionEndDate`) >= DATE(NOW()) ) " +
                     " ORDER BY a.CompanyOrder ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }

        public DataTable search_CompanyByPID(string pKey)
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT a.* " +
                     " FROM  `sys_company` AS a WHERE `CompanyPID` = " + pKey;

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }

        public DataTable search_DDLCompany()
        {

            DataTable dt = new DataTable();

            dt = publicNewClass.mydb.GetDataTable(" SELECT * FROM sys_company ORDER BY CompanyOrder ");

            return dt;
        }

        public DataTable search_ServiceComplaintType(string pLogId)
        {
            DataTable dt;
            string sqlstr = string.Empty;
            sqlstr = " select a.*, b.* FROM Sys_Code as a " +
                       " Left outer join ServiceLogDtl as b " +
                       " on a.Code = b.LogDtlType and b.ServceLogID = '" + pLogId + "' " +
                       " WHERE a.SysCodeType = 'COMPLAINTTYPE' " +
                       " ORDER BY a.CodeOrder ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;

        }

        public DataTable search_ServiceLogByLogID(string pLogId)
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT a.*, b.AccountID AS UserAccount, b.Name AS UserName " +
                     " FROM  `ServiceLog` AS a " +
                     " Left outer join sys_user as b ON a.LogUserID = b.ID " +
                     " WHERE a.ServiceLogID = '" + pLogId + "' ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;

        }

        public DataTable search_ServiceLogByLogNo(string pLogNo)
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT a.*, b.AccountID AS UserAccount, b.Name AS UserName " +
                     " FROM  `ServiceLog` AS a " +
                     " Left outer join sys_user as b ON a.LogUserID = b.ID " +
                     " WHERE a.LogNo = '" + pLogNo + "' ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;

        }
        
        public bool Update_LogCancelByLogNo(string pLogNo, string pUserID, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            try
            {
                string sqlstr = "";
                string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                sqlstr = string.Format(" UPDATE `ServiceLog` SET `CancelYN`= 'Y', CancelDate = NOW(), CancelUser = '{1}' WHERE `LogNo` = '{0}' ", pLogNo, pUserID);
                publicNewClass.mydb.InsertDataTable(sqlstr);

                b = true;
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                b = false;
            }

            return b;
        }

        public DataTable search_ServiceLogFiles(string pLogId)
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT ServiceLogID, FileId, FileName, ContentType, Content " +
                     "   , UploadUserID , ProcessID, UploadDT " +
                     "   , '' AS ProcessName , b.Name AS UploadUserName " +
                     " FROM  `ServiceLogFiles` AS a " +
                     " LEFT JOIN  `sys_user` AS b ON a.UploadUserID = b.ID " +
                     " WHERE a.ServiceLogID = '" + pLogId + "' ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;

        }

        public DataTable search_ServiceLogFile(string pFileId)
        {
            DataTable dt;
            string sqlstr = string.Empty;

            sqlstr = " SELECT ServiceLogID, FileId, FileName, ContentType, Content " +
                     " FROM  `ServiceLogFiles` AS a " +
                     " WHERE a.FileId = '" + pFileId + "' ";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;

        }

        public bool InsertRecords_ServiceLogFile(string logID, string fileName, string contentType, byte[] bytes, string userPID, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                string query = "INSERT INTO servicelogfiles(ServiceLogID, FileName, ContentType, Content, UploadUserID, UploadDT) VALUES (@ServiceLogID, @FileName, @ContentType, @Content, @UploadUserID, NOW() )";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ServiceLogID", logID);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@ContentType", contentType);
                    cmd.Parameters.AddWithValue("@Content", bytes);
                    cmd.Parameters.AddWithValue("@UploadUserID", userPID);

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

        public bool DeleteRecords_ServiceLogFile(string fileID, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                string query = " DELETE FROM servicelogfiles WHERE FileID=@FileID ";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FileID", fileID);
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

        public bool UpdateRecords_Company(CompanyCls slCls, out string returnMessage)
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

        public bool InsertRecords_Company(CompanyCls slCls, out string returnMessage)
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

    }

    public class CompanyCls
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
