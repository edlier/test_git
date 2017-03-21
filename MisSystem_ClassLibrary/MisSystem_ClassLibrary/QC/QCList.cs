using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{
    public class QCList
    {
        public DataTable search_mySQL_QCwaitForValidateList()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            //sqlstr += " Select DocNum,ItemCode,Dscription,Qty,LineNum,status,CardName,CardCode,Operator,DocDate from qc_savesapdata where status=1 OR status=2";
            sqlstr += "Select " +
                            "DocNum," +
                            "ItemCode," +
                            "Dscription," +
                            "Qty," +
                            "LineNum," +
                            "status," +
                            "CardName," +
                            "CardCode," +
                            "au_computerform.ChiName," +
                            "DocDate " +

                            "from qc_savesapdata " +
                            "join au_computerForm on(qc_savesapdata.Operator=au_computerform.id)" +
                            "where status=1 OR status=2";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        //public DataTable search_QCFailedReason()
        //{
        //    DataTable dt = new DataTable();
        //    string sqlstr = "";
        //    sqlstr += " Select id,+ "
        //        + "CONCAT_WS(',',  id , description ) as idDes from qc_failedreason";

        //    dt = publicNewClass.mydb.GetDataTable(sqlstr);
        //    return dt;
        //}

        public DataTable search_QCFailedType()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += " Select id,+ "
                + "CONCAT_WS(',',  id , description ) as idDes from qc_failedType";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_QCFReasonList()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += " Select id, TypeID, "
                + " CONCAT_WS(',',  id , description ) as idDes from qc_failedreason";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_mySQL_QCValidateItem(string DocNum, string ItemCode, string LineNum)
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += " Select id from qc_savesapdata where DocNum=" + publicNewClass.mydb.qo(DocNum) +
                " AND  ItemCode = " + publicNewClass.mydb.qo(ItemCode) +
                " AND  LineNum = " + publicNewClass.mydb.qo(LineNum);

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_mySQL_QCwaitForValidateItem(string DocNum, string ItemCode, string LineNum)
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += " Select * from qc_savesapdata where DocNum=" + publicNewClass.mydb.qo(DocNum) +
                " AND  ItemCode = " + publicNewClass.mydb.qo(ItemCode) +
                " AND  LineNum = " + publicNewClass.mydb.qo(LineNum);

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_Product()
        {
            DataTable dt = new DataTable();
            string sqlstr = "select * from qc_model";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_FQCList()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += "select SN,";
            sqlstr += " SaveTime,";
            sqlstr += " (Case Failed When '1' then 'F' Else 'P' END) as 'Qulity',";
            sqlstr += " qc_failedreason.Description AS 'FReason',";

            sqlstr += " qc_model.Name AS 'Product',";
            sqlstr += " au_computerform.ChiName AS 'UserName'";
            sqlstr +=  " from qc_fqc ";

            sqlstr += " left join qc_failedreason on(qc_fqc.FailedR=qc_failedreason.ID)";
            sqlstr += " join qc_model on(qc_fqc.Product=qc_model.ID)";
            sqlstr += " join au_computerform on(au_computerform.ID=qc_fqc.Operator)";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_IPQCList()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += "select SN,";
            sqlstr += " SaveTime,";
            sqlstr += " (Case Failed When '1' then 'F' Else 'P' END) as 'Qulity',";
            sqlstr += " qc_failedreason.Description AS 'FReason',";

            sqlstr += " qc_model.Name AS 'Product',";
            sqlstr += " au_computerform.ChiName AS 'UserName'";
            sqlstr += " from qc_ipqc ";

            sqlstr += " left join qc_failedreason on(qc_ipqc.FailedR=qc_failedreason.ID)";
            sqlstr += " join qc_model on(qc_ipqc.Product=qc_model.ID)";
            sqlstr += " join au_computerform on(au_computerform.ID=qc_ipqc.Operator)";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_FQC_DuplicateSN(string sn)
        {
            DataTable dt = new DataTable();
            string sqlstr = "select SN from qc_fqc where SN =" + publicNewClass.mydb.qo(sn);
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_IPQC_DuplicateSN(string sn)
        {
            DataTable dt = new DataTable();
            string sqlstr = "select SN from qc_ipqc where SN =" + publicNewClass.mydb.qo(sn);
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_IQCList()
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr += "select ItemCode," +
            "Dscription," +
            "CardCode," +
            "CardName," +
            "LineNum," +
            "Qty," +
            "End_Time," +
            "TQty," +
            "FQty," +
            "((FQty/TQty)*100) AS 'FRate'," +
            "Qc_passed," +
            "Issue_ID" +

            " from qc_savesapdata" +
            " where status=2";

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }



        //  Insert QC data from SAP to MySQL
        #region insertQCd_FSAP_TMy
        public void insertQCd_FSAP_TMy(
            string DocNum,
            string DocEntry,
            string ItemCode,
            string CardCode,
            string CardName,

            string LineNum,
            string Qty,
            string Dscription,
            string DocDate,
            string Operator,

            string Start_Time)
        {
            string aa;
            aa = " Insert into qc_savesapdata(DocNum,DocEntry,ItemCode,CardCode,CardName,LineNum,Qty,Dscription,DocDate,Operator,Start_Time,status,QType)"
                + " values ("
                + publicNewClass.mydb.qo(DocNum) + ","
                + publicNewClass.mydb.qo(DocEntry) + ","
                + publicNewClass.mydb.qo(ItemCode) + ","
                + publicNewClass.mydb.qo(CardCode) + ","
                + publicNewClass.mydb.qo(CardName) + ","

                + publicNewClass.mydb.qo(LineNum) + ","
                + publicNewClass.mydb.qo(Qty) + ","
                + publicNewClass.mydb.qo(Dscription) + ","
                + publicNewClass.mydb.qo(DocDate) + ","
                + publicNewClass.mydb.qo(Operator) + ","

                + publicNewClass.mydb.qo(Start_Time) + ",    '1' ,  '1'   )";
            

            publicNewClass.mydb.InsertDataTable(aa);
        }
        #endregion
        #region insertQCd_FSAP_TMy2
        public DataTable insertQCd_FSAP_TMy2(
            string DocNum,
            string DocEntry,
            string ItemCode,
            string CardCode,
            string CardName,

            string LineNum,
            string Qty,
            string Dscription,
            string DocDate,
            string Operator,

            string Start_Time)
        {
            string aa;
            aa = " Insert into qc_savesapdata(DocNum,DocEntry,ItemCode,CardCode,CardName,LineNum,Qty,Dscription,DocDate,Operator,Start_Time,status,QType)"
                + " values ("
                + publicNewClass.mydb.qo(DocNum) + ","
                + publicNewClass.mydb.qo(DocEntry) + ","
                + publicNewClass.mydb.qo(ItemCode) + ","
                + publicNewClass.mydb.qo(CardCode) + ","
                + publicNewClass.mydb.qo(CardName) + ","

                + publicNewClass.mydb.qo(LineNum) + ","
                + publicNewClass.mydb.qo(Qty) + ","
                + publicNewClass.mydb.qo(Dscription) + ","
                + publicNewClass.mydb.qo(DocDate) + ","
                + publicNewClass.mydb.qo(Operator) + ","

                + publicNewClass.mydb.qo(Start_Time) + ",    '1' ,  '1'   ); "

                + " SELECT LAST_INSERT_ID() AS outID;  ";

            DataTable dt;
            dt = publicNewClass.mydb.GetDataTable(aa);
            return dt;
        }
        #endregion

        public void insertFQC_data(string SN,string productID,string failed,string failedRID, string SaveTime,string Operator)
        {
            string aa;
            aa = " Insert into qc_fqc(SN,Product,Failed,FailedR,SaveTime,Operator)"
                + " values ("
                + publicNewClass.mydb.qo(SN) + ","
                + publicNewClass.mydb.qo(productID) + ","
                + publicNewClass.mydb.qo(failed) + ","
                + publicNewClass.mydb.qo(failedRID) + ","
                + publicNewClass.mydb.qo(SaveTime) + ","
                + publicNewClass.mydb.qo(Operator) + ")";

            publicNewClass.mydb.InsertDataTable(aa);
        }
        public void insertIPQC_data(string SN, string productID, string failed, string failedRID, string SaveTime, string Operator)
        {
            string aa;
            aa = " Insert into qc_ipqc(SN,Product,Failed,FailedR,SaveTime,Operator)"
                + " values ("
                + publicNewClass.mydb.qo(SN) + ","
                + publicNewClass.mydb.qo(productID) + ","
                + publicNewClass.mydb.qo(failed) + ","
                + publicNewClass.mydb.qo(failedRID) + ","
                + publicNewClass.mydb.qo(SaveTime) + ","
                + publicNewClass.mydb.qo(Operator) + ")";

            publicNewClass.mydb.InsertDataTable(aa);
        }


        #region update_ValidatedData
        public void update_ValidatedData(
            string endTime, 
            string FQTY, 
            string TQty, 
            string IssueID, 
            string fqc_passed,
            string status)
        {
            string aa;
            aa = " update qc_savesapdata set"+
                " End_Time =" + publicNewClass.mydb.qo(endTime) +
                " FQty =" + publicNewClass.mydb.qo(FQTY) +
                " TQty =" + publicNewClass.mydb.qo(TQty) +
                " Issue_Detail =" + publicNewClass.mydb.qo(IssueID) +
                " fqc_passed =" + publicNewClass.mydb.qo(fqc_passed) +
                " status =" + publicNewClass.mydb.qo(status);
            
            publicNewClass.mydb.InsertDataTable(aa);
        }
        #endregion
        public void update_saveForFinishedValidated(string TQty, string FQty, string Process_HR, string Process_MIN, string End_Time, string status, string ID)
        {
            string aa;
            aa = " update qc_savesapdata set "
                + " TQty = " + publicNewClass.mydb.qo(TQty) + ","
                + " FQty = " + publicNewClass.mydb.qo(FQty) + ","
                //+ " Issue_ID = " + publicNewClass.mydb.qo(Issue_ID) + ","
                + " ProcessT_HR = " + publicNewClass.mydb.qo(Process_HR) + ","
                + " ProcessT_MIN = " + publicNewClass.mydb.qo(Process_MIN) + ","
                + " End_Time = " + publicNewClass.mydb.qo(End_Time) + ","
                + " status = " + publicNewClass.mydb.qo(status)
                + "  where id=" +publicNewClass.mydb.qo(ID);
            //where
            publicNewClass.mydb.InsertDataTable(aa);
        }
        public void update_FailedReasonNDQty(string itemID, string[,] failedID)
        {
            string aa = "";
            for (int i = 0; i < 5; i++)
            {
                if (failedID[i, 1] != "0" && failedID[i, 1] != null && failedID[i, 1] != "" && failedID[i, 0] != "" && failedID[i, 0] != null)
                {
                    aa += "  Insert into qc_iqcfdetail(ValidatedID,Qty,FailedID) "
                    + " values("
                    + publicNewClass.mydb.qo(itemID) + ","
                    + publicNewClass.mydb.qo(failedID[i, 1]) + ","
                    + publicNewClass.mydb.qo(failedID[i, 0])
                    + ") ;";
                }
            }
            //where
            publicNewClass.mydb.InsertDataTable(aa);
        }
    }
}
