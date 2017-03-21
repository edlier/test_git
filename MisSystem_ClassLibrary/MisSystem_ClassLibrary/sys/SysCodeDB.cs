using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MisSystem_ClassLibrary.sys
{
    public class SysCodeDB
    {
        public static CommonUtils PrgUtil = new CommonUtils();

        public DataTable search_OrderStatusCode()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM Sys_Code WHERE SysCodeType = 'ORDERSTATUS' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_RBLsyscode()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM Sys_Code WHERE SysCodeType = 'SERVICELOGCATALOG' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_MARKETAREA()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM Sys_Code WHERE SysCodeType = 'MARKETAREA' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_CBLsyscode()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("SELECT * FROM Sys_Code WHERE SysCodeType = 'SERVICELOGCOMPLAINT' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_DDLCodeType()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("SELECT SysCodeType AS Code, CodeTypeName AS CodeName FROM sys_CodeType ORDER BY CodeTypeOrder ");
            return dt;
        }

        public DataTable search_DDLCode(string pCodeType)
        {           

            DataTable dt = new DataTable();

            dt = publicNewClass.mydb.GetDataTable("SELECT * FROM Sys_Code WHERE SysCodeType = '" + pCodeType + "' ORDER BY CodeOrder ");

            return dt;
        }

        public DataTable search_DDLCodeTypeGroup()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("SELECT * FROM Sys_Code WHERE SysCodeType = 'SYSCODETYPEGROUP' ");
            return dt;
        }

        public DataTable search_DDLCountry()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM Sys_Code WHERE SysCodeType = 'COUNTRY' ORDER BY CodeOrder");
            return dt;
        }
        
        public DataTable search_syscode(string pCodeType, out string returnMessage)
        {
            returnMessage = string.Empty;
            DataTable dt = new DataTable();
            string sqlstr;
            //string sqlorder = " ORDER BY c.CodeTypeOrder, a.SysCodeType, a.CodeOrder ";
            //sqlstr = " SELECT * FROM sysCode ";

            sqlstr = " SELECT a.*, b.Name AS UpdUserName, c.CodeTypeName, d.CompanyCode " +
                     " FROM Sys_Code a " + 
                     " LEFT OUTER JOIN  `sys_user` AS b ON b.ID = a.UpdUserID " +
                     " LEFT OUTER JOIN  `sys_sysCodeType` AS c ON c.SysCodeType = a.SysCodeType " +
                     " LEFT OUTER JOIN  `sys_company` AS d ON d.CompanyPID = a.CompanyID ";

            try
            {
                if (!(pCodeType.Equals("")))
                {
                    sqlstr += " WHERE IFNULL(a.SysCodeType,'') = '" + pCodeType + "' ";
                }

                sqlstr += " ORDER BY c.CodeTypeOrder, a.SysCodeType, a.CodeOrder "; 

                dt = publicNewClass.mydb.GetDataTable(sqlstr);

                returnMessage = string.Empty;
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
            }
            return dt;
        }

        public DataTable search_syscode(string pCompany, string pCodeType, out string returnMessage)
        {
            returnMessage = string.Empty;
            DataTable dt = new DataTable();
            string sqlstr;
            //string sqlorder = " ORDER BY c.CodeTypeOrder, a.SysCodeType, a.CodeOrder ";
            //sqlstr = " SELECT * FROM sysCode ";

            sqlstr = " SELECT a.*, b.Name AS UpdUserName, c.CodeTypeName, d.CompanyCode " +
                     " FROM Sys_Code a " +
                     " LEFT OUTER JOIN  `sys_user` AS b ON b.ID = a.UpdUserID " +
                     " LEFT OUTER JOIN  `sys_CodeType` AS c ON c.SysCodeType = a.SysCodeType " +
                     " LEFT OUTER JOIN  `sys_company` AS d ON d.CompanyPID = a.CompanyID ";
            sqlstr += " WHERE 1 = 1 ";

            try
            {
                if (!(pCompany.Equals("")))
                {
                    sqlstr += " AND IFNULL(a.CompanyID,'') = '" + pCompany + "' ";
                }

                if (!(pCodeType.Equals("")))
                {
                    sqlstr += " AND IFNULL(a.SysCodeType,'') = '" + pCodeType + "' ";
                }

                sqlstr += " ORDER BY c.CodeTypeOrder, a.SysCodeType, a.CodeOrder ";

                dt = publicNewClass.mydb.GetDataTable(sqlstr);

                returnMessage = string.Empty;
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
            }
            return dt;
        }

        public DataTable search_syscodetype()
        {
            string sqlstr;
            sqlstr = " SELECT * FROM sys_CodeType ";
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_syscodetypegroup()
        {
            string sqlstr;
            sqlstr = " SELECT * FROM sys_CodeType ";
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_syscodetype(string pCodeGroup, out string returnMessage)
        {
            returnMessage = string.Empty;
            DataTable dt = new DataTable();
            string sqlstr;
            string sqlorder = " ORDER BY CodeTypeOrder ";
            //sqlstr = " SELECT * FROM sysCodeType ";

            sqlstr = " SELECT a.*, b.Name AS UpdUserName FROM sys_CodeType a " +
                     " LEFT JOIN  `sys_user` AS b ON b.ID = a.UpdUserID ";

            try
            {
                if (!(pCodeGroup.Equals("")))
                {
                    sqlstr += " WHERE IFNULL(TypeGroup,'') = '" + pCodeGroup + "' ";
                }

                sqlstr += sqlorder;

                dt = publicNewClass.mydb.GetDataTable(sqlstr);

                returnMessage = "Ok.";
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;                
            }
            return dt;
        }

        public void insert_syscodetype(string SysCodeType, string CodeTypeName,
            string CodeTypeDescr, string CodeTypeLevel, string CodeTypeOrder, string Att1, string Att2, string userID, string ActiveYN)
        {
            string sqlstr = "insert into sys_CodeType(SysCodeType,CodeTypeName,"
               + "CodeTypeDescr,CodeTypeLevel,CodeTypeOrder,Att1,Att2,UpdUserID,UpdDT,ActiveYN)"
                  + "values("
                  + publicNewClass.mydb.qo(SysCodeType) + ","

                  + publicNewClass.mydb.qo(CodeTypeName) + ","
                   + publicNewClass.mydb.qo(CodeTypeDescr) + ","
                   + publicNewClass.mydb.qo(CodeTypeLevel) + ","
                  + publicNewClass.mydb.qo(CodeTypeOrder) + ","
                  + publicNewClass.mydb.qo(Att1) + ","
                + publicNewClass.mydb.qo(Att2) + ","

                + publicNewClass.mydb.qo(userID) + ","
                 + "NOW(),"
                   + publicNewClass.mydb.qo(ActiveYN) + ")";

            publicNewClass.mydb.InsertDataTable(sqlstr);

        }
        
        public void insert_syscode(string SysCodeType,string Code, string CodeName,
           string CodeDescr, string CodeMemo, string CodeOrder, string Att1, string Att2, string userID, string ActiveYN)
        {
            string sqlstr = "insert into sys_code(SysCodeType,Code,CodeName,"
               + "CodeDescr,CodeMemo,CodeOrder,Att1,Att2,UpdUserID,UpdDT,ActiveYN)"
                  + "values("
                  + publicNewClass.mydb.qo(SysCodeType) + ","
                     + publicNewClass.mydb.qo(Code) + ","
                  + publicNewClass.mydb.qo(CodeName) + ","
                   + publicNewClass.mydb.qo(CodeDescr) + ","
                   + publicNewClass.mydb.qo(CodeMemo) + ","
                  + publicNewClass.mydb.qo(CodeOrder) + ","
                  + publicNewClass.mydb.qo(Att1) + ","
                + publicNewClass.mydb.qo(Att2) + ","

                + publicNewClass.mydb.qo(userID) + ","
                 + "NOW(),"
                   + publicNewClass.mydb.qo(ActiveYN) + ")";

            publicNewClass.mydb.InsertDataTable(sqlstr);

        }
        
        public void replace_syscodetype(string ID,string SysCodeType, string CodeTypeName,
           string CodeTypeDescr, string CodeTypeLevel, string CodeTypeOrder, string Att1, string Att2, string userID, string ActiveYN)
        {
            string sqlstr = "replace into sys_CodeType(ID,SysCodeType,CodeTypeName,"
               + "CodeTypeDescr,CodeTypeLevel,CodeTypeOrder,Att1,Att2,UpdUserID,UpdDT,ActiveYN)"
                  + "values("
                   + publicNewClass.mydb.qo(ID) + ","
                  + publicNewClass.mydb.qo(SysCodeType) + ","

                  + publicNewClass.mydb.qo(CodeTypeName) + ","
                   + publicNewClass.mydb.qo(CodeTypeDescr) + ","
                   + publicNewClass.mydb.qo(CodeTypeLevel) + ","
                  + publicNewClass.mydb.qo(CodeTypeOrder) + ","
                  + publicNewClass.mydb.qo(Att1) + ","
                + publicNewClass.mydb.qo(Att2) + ","

                + publicNewClass.mydb.qo(userID) + ","
                 + "NOW(),"
                   + publicNewClass.mydb.qo(ActiveYN) + ")";

            publicNewClass.mydb.InsertDataTable(sqlstr);

        }

        public void replace_syscode(string ID, string SysCodeType, string Code, string CodeName,
           string CodeDescr, string CodeMemo, string CodeOrder, string Att1, string Att2, string userID, string ActiveYN)
        {
            string sqlstr = "replace into sys_code(ID,SysCodeType,Code,CodeName,"
                + "CodeDescr,CodeMemo,CodeOrder,Att1,Att2,UpdUserID,UpdDT,ActiveYN)"
                  + "values("
                   + publicNewClass.mydb.qo(ID) + ","

                  + publicNewClass.mydb.qo(SysCodeType) + ","
                     + publicNewClass.mydb.qo(Code) + ","
                  + publicNewClass.mydb.qo(CodeName) + ","
                   + publicNewClass.mydb.qo(CodeDescr) + ","
                   + publicNewClass.mydb.qo(CodeMemo) + ","
                  + publicNewClass.mydb.qo(CodeOrder) + ","
                  + publicNewClass.mydb.qo(Att1) + ","
                + publicNewClass.mydb.qo(Att2) + ","

                + publicNewClass.mydb.qo(userID) + ","
                 + "NOW(),"
                   + publicNewClass.mydb.qo(ActiveYN) + ")";

            publicNewClass.mydb.InsertDataTable(sqlstr);

        }

        public bool validateInsert(string pCodeType, string pCode, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;

            try
            {
                DataTable dt;
                string sqlstr = "";
                sqlstr = " SELECT * FROM Sys_Code WHERE SysCodeType = '" + pCodeType + "' AND Code = '" + pCode + "' ";
                dt = publicNewClass.mydb.GetDataTable(sqlstr);

                //如果搜尋到的是空值
                if (dt.Rows.Count != 0)
                {
                    returnMessage = "There already have a same code in database.";
                    b = false;
                }
                else
                {
                    returnMessage = string.Empty;
                    b = true;
                }

            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                b = false;
            }

            return b;

        }

        public bool goInsert(string pID, string pCode, string pTypeName, string pActive, string pTypeOrder, string pTypeDescr, string pCodeType, string pAtt1, string pAtt2, string pUserID, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;

            try
            {
                string sqlstr = "";
                string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                sqlstr = string.Format("INSERT INTO sysCode (Code, CodeName, ActiveYN, CodeOrder, CodeDescr, CodeType, Att1, Att2, UpdUserID, UpdDT) " +
                           " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}', now())"
                             , pCode, pTypeName, pActive, pTypeOrder, pTypeDescr, pCodeType, pAtt1, pAtt2, pUserID);
                publicNewClass.mydb.InsertDataTable(sqlstr);
                b = true;
                returnMessage = "Insert is Ok.";
            }
            catch (Exception ex)
            {
                b = false;
                returnMessage = ex.Message;                
            }

            return b;
        }

        public bool validateEdit(string pCodeType, string pCode, out string returnMessage)
        {
            bool b = false;

            returnMessage = string.Empty;

            try
            {
                DataTable dt;
                string sqlstr = "";
                sqlstr = " SELECT * FROM Sys_Code WHERE SysCodeType = '" + pCodeType + "' AND Code = '" + pCode + "' ";
                dt = publicNewClass.mydb.GetDataTable(sqlstr);

                if (dt.Rows.Count == 0)
                {
                    returnMessage = "Can not find data in database.";
                    b = false;
                }
                else
                {
                    returnMessage = string.Empty;
                    b = true;
                }
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                b = false;
            }

            return b;
        }

        public bool goEdit(string pID, string pCodeType, string pCode, string pCodeName, string pActive, string pCodeOrder, string pCodeDescr, string pAtt1, string pAtt2, string pUserID, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;

            try
            {
                string sqlstr = "";
                string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                sqlstr = string.Format(" UPDATE Sys_Code SET CodeName = '{2}', ActiveYN = '{3}', CodeOrder = '{4}' " +
                                       " , CodeDescr = '{5}' " +
                                       " , Att1 = '{6}', Att2 = '{7}' " +
                                       " , UpdUserID ='{8}', UpdDT = NOW() " +
                                       " WHERE SysCodeType = '{0}' AND Code = '{1}' "
                                         , pCodeType, pCode, pCodeName, pActive, pCodeOrder, pCodeDescr, pAtt1, pAtt2, pUserID);
                publicNewClass.mydb.InsertDataTable(sqlstr);
                returnMessage = "Edit is Ok.";
                b = true;
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                b = false;
            }

            return b;
        }

        public bool validateDelete(string pID, out string returnMessage)
        {
            bool b = false;

            returnMessage = string.Empty;

            try
            {
                DataTable dt;
                string sqlstr = "";
                sqlstr = " SELECT * FROM Sys_Code WHERE ID = '" + pID + "'";
                dt = publicNewClass.mydb.GetDataTable(sqlstr);

                if (dt.Rows.Count == 0)
                {
                    returnMessage = "Can not find data in database.";
                    b = false;
                }
                else
                {
                    returnMessage = string.Empty;
                    b = true;
                }
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                b = false;
            }

            return b;
        }

        public void goDelete(string pID, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;

            string sqlstr = "";
            string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            sqlstr = string.Format(" DELETE FROM Sys_Code WHERE Id = '{0}'", pID);
            publicNewClass.mydb.InsertDataTable(sqlstr);

        }

        public bool UpdateRecords_SysCode(CodeCls slCls, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                string query = " UPDATE Sys_Code SET " +
                    "   CodeName = @CodeName " +
                    " , CodeDescr = @CodeDescr " +
                    " , ActiveYN = @ActiveYN " +
                    " , CodeOrder = @CodeOrder " +
                    " , SysCodeType = @SysCodeType " +
                    " , Att1 = @Att1 " +
                    " , Att2 = @Att2 " +
                    " , UpdUserID = @UpdUserID " +
                    " , UpdDT = NOW() " +
                    " WHERE ID = @ID ";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", slCls.ID);
                    cmd.Parameters.AddWithValue("@CodeName", slCls.CodeName);
                    cmd.Parameters.AddWithValue("@CodeDescr", slCls.CodeDescr);
                    cmd.Parameters.AddWithValue("@ActiveYN", slCls.ActiveYN);
                    cmd.Parameters.AddWithValue("@CodeOrder", slCls.CodeOrder);
                    cmd.Parameters.AddWithValue("@SysCodeType", slCls.SysCodeType);
                    cmd.Parameters.AddWithValue("@Att1", slCls.Att1);
                    cmd.Parameters.AddWithValue("@Att2", slCls.Att2);
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

        public bool InsertRecords_SysCode(CodeCls slCls, out string returnMessage)
        {
            bool b = false;
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                string query = " INSERT INTO Sys_Code (CompanyID, Code, CodeName " +
                               " , ActiveYN, CodeOrder, CodeDescr, SysCodeType " +
                               " , Att1, Att2, UpdUserID, UpdDT) VALUES " +
                               " ( @CompanyID, @Code, @CodeName " +
                               " , @ActiveYN, @CodeOrder, @CodeDescr, @SysCodeType " +
                               " , @Att1, @Att2, @UpdUserID, NOW()) ";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CompanyID", slCls.CompanyID);
                    cmd.Parameters.AddWithValue("@Code", slCls.Code);
                    cmd.Parameters.AddWithValue("@CodeName", slCls.CodeName);
                    cmd.Parameters.AddWithValue("@CodeDescr", slCls.CodeDescr);
                    cmd.Parameters.AddWithValue("@ActiveYN", slCls.ActiveYN);
                    cmd.Parameters.AddWithValue("@CodeOrder", slCls.CodeOrder);
                    cmd.Parameters.AddWithValue("@SysCodeType", slCls.SysCodeType);
                    cmd.Parameters.AddWithValue("@Att1", slCls.Att1);
                    cmd.Parameters.AddWithValue("@Att2", slCls.Att2);
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

        public string search_OTAPPLYSTATUS_Created()
        {
            string rtnCode = string.Empty;
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable(" SELECT * FROM sys_code WHERE SysCodeType = 'OTAPPLYSTATUS' ORDER BY CodeOrder LIMIT 1 ");
            if (dt != null && dt.Rows.Count > 0)
            {
                rtnCode = PrgUtil.getStr(dt.Rows[0]["Code"]);
            }
            return rtnCode;
        }

        public string search_OTAPPLYSTATUS_Discard()
        {
            string rtnCode = "OTD";
            return rtnCode;
        }

        public string search_OTAPPLYSTATUS_Send()
        {
            string rtnCode = "OTS";
            return rtnCode;
        }

        public string search_OTAPPLYSTATUS_Approve()
        {
            string rtnCode = "OTAA";
            return rtnCode;
        }

        public string search_OTAPPLYSTATUS_Confirm()
        {
            string rtnCode = "OTAC";
            return rtnCode;
        }

        public string search_OTAPPLYSTATUS_Reject()
        {
            string rtnCode = "OTR";
            return rtnCode;
        }


        public DataTable search_EDUSCHOOL(string code)
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'EDUSCHOOL' AND Code='" + code + "' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_EDUMAJORSUBJECT(string code)
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'EDUMAJORSUBJECT' AND Code='" + code + "' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_EDUDEGREE(string code)
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'EDUDEGREE' AND Code='" + code + "' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_EDUSTATUS(string code)
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'EDUSTATUS' AND Code='" + code + "' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_RESIGNREASON(string code)
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'RESIGNREASON' AND Code='" + code + "' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_PERCHANGETYPE(string code)
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'PERCHANGETYPE' AND Code='" + code + "' ORDER BY CodeOrder ");
            return dt;
        }

        public DataTable search_NEWRECRUIT()
        {
            DataTable dt = new DataTable();
            dt = publicNewClass.mydb.GetDataTable("select * FROM sys_code WHERE SysCodeType = 'NEWRECRUIT' AND ActiveYN='Y' ORDER BY CodeOrder ");
            return dt;
        }

    }

    public class CodeCls
    {
        public string ID { get; set; }
        public string SysCodeType { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeDescr { get; set; }
        public string CodeMemo { get; set; }
        public string CodeOrder { get; set; }
        public string Att1 { get; set; }
        public string Att2 { get; set; }
        public string UpdUserID { get; set; }
        public string ActiveYN { get; set; }
        public string CompanyID { get; set; }
    }



}
