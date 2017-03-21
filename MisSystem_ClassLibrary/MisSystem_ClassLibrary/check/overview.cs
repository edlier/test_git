using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisSystem_ClassLibrary
{
    public class overview
    {
        public DataTable get_dcr()
        {
            string sqlstr ="";
            DataTable tb = new DataTable();

            //sqlstr = "select documentchangerequest.id AS 'id',au_computerform.EnFName AS 'changeby',priority.description AS 'priority', RnDprojectName,DCRassignedNo,changePerposeDate";
            //sqlstr += " from documentchangerequest";
            //sqlstr += " join au_computerform on (documentchangerequest.changePerposedBy = au_computerform.ID)";
            //sqlstr += " join priority on (priority.code = documentchangerequest.priority)";

            sqlstr = "select id,DocumentChangeRequestID,EngineeringChangeRequestID,ReceiveAndApprovalRecordID,status";
            sqlstr += " from documentchange";
            sqlstr += "";

            tb = publicNewClass.mydb.GetDataTable(sqlstr);
            //tb = dateform(tb, "changePerposeDate");

            return tb;
        }
        public DataTable dateform(DataTable dt, string datecol)
        {
            //新增date2 Collin↑↓來Format 日期資料
            DataColumn column;
            column = new DataColumn();
            string newcol = datecol + "2";
            column.ColumnName = newcol;
            dt.Columns.Add(column);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (String.Format("{0:yyyy-MM-dd}", dt.Rows[i][datecol]) == "0001-01-01" || String.Format("{0:yyyy-MM-dd}", dt.Rows[i][datecol]) == null)
                    {
                        dt.Rows[i][newcol] = "";
                    }
                    else
                    {
                        dt.Rows[i][newcol] = String.Format("{0:yyyy-MM-dd}", dt.Rows[i][datecol]);
                    }
                }
            }
            return dt;
        }

        public DataTable getDCRinit(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select concat(au_computerform.EnFName,' ',au_computerform.EnLName) as changePerposedBy,changePerposeDate,product.description as productModel,priority.description as priority,RnDprojectName,DCRassignedNo,DepManagerID,if(DepManagerCheckDate='0000-00-00',null,DepManagerCheckDate) as DepManagerCheckDate";
            sqlstr += " from documentchangerequest";
            sqlstr += " join priority on (documentchangerequest.priority = priority.code)";
            sqlstr += " join product on (documentchangerequest.productModel = product.id)";
            sqlstr += " join au_computerform on (documentchangerequest.changePerposedBy = au_computerform.ID)";
            sqlstr += " where documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getDCRdocNo(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select DCRDocNum1,DCRDocNum2,DCRDocNum3,DCRDocNum4,DCRDocNum5 from dcrdocnum";
            sqlstr += " join documentchangerequest on (documentchangerequest.DCRDocNumID = dcrdocnum.id)";
            sqlstr += " where documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getDCRdocName(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select DocName1,DocName2,DocName3,DocName4,DocName5 from dcrdocname";
            sqlstr += " join documentchangerequest on (documentchangerequest.DCRDocNameID = dcrdocname.id)";
            sqlstr += " where documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getDCRversion(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select newVersion1,newVersion2,newVersion3,newVersion4,newVersion5,oldVersion1,oldVersion2,oldVersion3,oldVersion4,oldVersion5 from dcrversion";
            sqlstr += " join documentchangerequest on (documentchangerequest.DCRversionID = dcrversion.id)";
            sqlstr += " where documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getDCRchangereason(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select ChangeReason1,ChangeReason2,ChangeReason3,ChangeReason4,ChangeReason5,remark from dcrdocchangereason";
            sqlstr += " join documentchangerequest on (documentchangerequest.changeReasonID = dcrdocchangereason.id)";
            sqlstr += " where documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getaffectdoc(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select dcraffectdoc.* from dcraffectdoc,documentchangerequest where documentchangerequest.AffectedDocID = dcraffectdoc.id and documentchangerequest.id='" + dcrid + "'";
            
            return publicNewClass.mydb.GetDataTable(sqlstr);
        }
        public string getaffectdes(int affectid)
        {
            string sqlstr = "";
            sqlstr = "select description from dcraffecteddocsdes where id='" + affectid + "'";

            return publicNewClass.mydb.getSingleData(0,sqlstr);
        }

        public DataTable getraabasic(string raaid)
        {
            string sqlstr = "";
            sqlstr = "select raarNo, fillBy,au_audep.Description as resDept, managerID";
            sqlstr += " from reviewandapprovalrecord";
            sqlstr += " join au_audep on (reviewandapprovalrecord.resDept = au_audep.id)";
            //sqlstr += " join au_computerform on (reviewandapprovalrecord.fillBy = au_computerform.ID)";
            //sqlstr += " join au_computerform on (reviewandapprovalrecord.managerID = au_computerform.ID)";
            sqlstr += " where reviewandapprovalrecord.id='" + raaid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getpdne(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select dcr_pdne.* from dcr_pdne ,documentchangerequest where PDnEID=dcr_pdne.id and documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getpcmc(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select QuantityPartOnHand,QuantityPartOnOrder,QuantitySubComponentOnHand,QuantitySubComponentOnOrder,QuantityFinishedProductOnHand,QuantityFinishedProductOnOrder,";
            sqlstr += "StockDealReviseStocks,if(StockDealReviseStocksDate='0000-00-00',null,StockDealReviseStocksDate) as StockDealReviseStocksDate,";
            sqlstr += "StockDealUsedStocks,if(StockDealUsedStocksDate='0000-00-00',null,StockDealUsedStocksDate) as StockDealUsedStocksDate,";
            sqlstr += "StockDealDepleteOfRP,if(StockDealDepleteOfRPDate='0000-00-00',null,StockDealDepleteOfRPDate) as StockDealDepleteOfRPDate,";
            sqlstr += "PCMCmanagerID,PCMCmanagerCheck,PCMCmanagerCheckDate ";
            sqlstr += "from dcr_pcmc,documentchangerequest where PCMCID=dcr_pcmc.id and documentchangerequest.id='" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }
}
