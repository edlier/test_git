using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary
{
    public class check_dcr
    {
        public void updatestatus(string dcrid, string status)
        {
            string sqlstr = "";
            sqlstr = "update documentchange set status ='" + status + "' where DocumentChangeRequestID ='" + dcrid + "'";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public DataTable insertpdne(int ce,int foz,int sf,int st,string date,string usrid)
        {
            string sqlstr = "";
            sqlstr = "insert into dcr_pdne(CEmake,510k,Safetyrisk,Standards,manager,date)";
            sqlstr += string.Format(" values('{0}','{1}','{2}','{3}','{4}','{5}');",ce,foz,sf,st,usrid,date);
            sqlstr += "select LAST_INSERT_ID() from dcr_pdne";
            DataTable id = publicNewClass.mydb.GetDataTable(sqlstr);

            return id;
        }

        public void update_pdneid(string pdneid,string dcrid)
        {
            string sqlstr = "";
            sqlstr = "update documentchangerequest set PDnEID = '" + pdneid + "' where id='" + dcrid + "'";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public DataTable insertpcmc(int qpHand, int qpOrder, int qsHand, int qsOrder, int qfHand, int qfOrder, int srStock, string srStockdate, int suStock, string suStockdate, int sdrp, string sdrpdate,int pcmcMid,int pcmcMcheck,string pcmcMcheckdate)
        {
            string sqlstr = "";
            sqlstr = "insert into dcr_pcmc(QuantityPartOnHand,QuantityPartOnOrder,QuantitySubComponentOnHand,QuantitySubComponentOnOrder,QuantityFinishedProductOnHand,QuantityFinishedProductOnOrder,StockDealReviseStocks,StockDealReviseStocksDate,StockDealUsedStocks,StockDealUsedStocksDate,StockDealDepleteOfRP,	StockDealDepleteOfRPDate,PCMCmanagerID,PCMCmanagerCheck,PCMCmanagerCheckDate)";
            sqlstr += string.Format(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}');",
                                qpHand, qpOrder, qsHand, qsOrder, qfHand, qfOrder, srStock, srStockdate, suStock, suStockdate, sdrp, sdrpdate, pcmcMid, pcmcMcheck, pcmcMcheckdate);
            sqlstr += " select LAST_INSERT_ID() from dcr_pcmc";
            DataTable id = publicNewClass.mydb.GetDataTable(sqlstr);

            return id;
        }

        public void update_pcmcid(string pcmcid, string dcrid)
        {
            string sqlstr = "";
            sqlstr = "update documentchangerequest set PCMCID = '" + pcmcid + "' where id='" + dcrid + "'";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public DataTable getDCR_basic(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select changePerposedBy,changePerposeDate,product.description as productModel,priority.description as priority,RnDprojectName,DCRassignedNo";
            sqlstr += " from documentchangerequest";
            sqlstr += " join product on (documentchangerequest.productModel = product.id)";
            sqlstr += " join priority on (documentchangerequest.priority = priority.code)";
            sqlstr += " where documentchangerequest.id = '" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getDCR_doc(int dcrid)
        {
            string sqlstr = "";
            sqlstr = "select ";
            sqlstr += " from documentchangerequest where id = '" + dcrid + "'";

            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public void updateMC(string dcrid,string date,int usrid)
        {
            string sqlstr = "";
            sqlstr = "update documentchangerequest set managerCheck= '1',managerID='" + usrid + "',managerCheckDate='" + date + "' where id='" + dcrid + "'";

            publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }
}
