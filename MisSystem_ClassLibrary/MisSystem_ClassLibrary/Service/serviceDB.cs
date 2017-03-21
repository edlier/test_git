using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace MisSystem_ClassLibrary
{

    public class serviceDB
    {


        //驗證資料類型
        //validateDataTypeDB validateDataSR = new validateDataTypeDB();
        //table的名
        string strTable = "serviceReport";


        public void insertSR(string companyID, string contactname, string email, string productSN, int complaint
            , string creceivedby,string othermsg ,string describec, int cr, int rma,int ir
            , int flowtype, int flow,string user)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_servicelog`(`companyID`, `contactname`, `email`, `productSN`, `complaint`, `creceivedby`, `othermsg` , `describec`, `cr`, `rma`, `IR` ,`flowtype`, `flow`, `fillpeople`) VALUES ('" + companyID + "','" + contactname + "','" + email + "','" + productSN + "'," + complaint + ",'" + creceivedby + "','" + othermsg +"','"+ describec + "'," + cr + "," + rma + "," +ir+ "," + flowtype + "," + flow +",'"+ user +"')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public void update()
        {
            string sqlstr = "";
            sqlstr = "UPDATE sv_servicelog SET sv_servicelog.complaint = (SELECT ID FROM sv_servicelog_typeofc WHERE sv_servicelog_typeofc.servicelogID = 777) WHERE sv_servicelog.complaint = 777 ; UPDATE sv_servicelog_typeofc SET sv_servicelog_typeofc.servicelogID = (SELECT ID FROM sv_servicelog WHERE sv_servicelog_typeofc.ID = sv_servicelog.complaint) ";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        //把list存入string用,分隔
        string transListToString(List<string> listTmp)
        {
            string strTmpT = "";

            if (listTmp.Count != 0)
                strTmpT = listTmp[0];
            for (int intCount = 1; intCount < listTmp.Count; intCount++)
                strTmpT += "," + listTmp[intCount];
            return strTmpT;
        }



        public void inserttoc(int servicelog, int c1, int c2, int c3,int c4, int c5, int c6,string c6msg, int c7, string c7other, int c8, string c8mname, string c8msn)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_servicelog_typeofc`(`servicelogID`, `c1`, `c2`, `c3`, `c4`, `c5`, `c6`, `c6msg`, `c7`, `c7other`, `c8`,`c8mname`,`c8msn`) VALUES (" + servicelog + "," + c1 + "," + c2 + "," + c3 + "," + c4 + "," + c5 + "," + c6 + ",'" + c6msg + "'," + c7 + ",'" + c7other + "'," + c8 + ",'" + c8mname + "','" + c8msn + "')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }



        public void insertshipping(int servicelog, string via, string dp, string torano, string user)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_c_shippingcheck`(`servicelogID`, `viaID`, `designrecipient`, `torano`, `fillpeople`) VALUES (" + servicelog + ",'" + via + "','" + dp + "','" + torano + "','"+ user +"')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public void insertqc(int servicelog, int qc1, int qc2, int qc3, int qc4, int qc5, string qc5accesories, int qc6, int qc7, int qc8, int qc9, int qc10, int qc11, int qc12, string qc12other,string user)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_c_qc`(`servicelogID`, `qc1`, `qc2`, `qc3`, `qc4`, `qc5`, `qc5accesories`, `qc6`, `qc7`, `qc8`,`qc9`, `qc10`,`qc11`, `qc12`, `qc12other`,`fillpeople`) VALUES (" + servicelog + "," + qc1 + "," + qc2 + "," + qc3 + "," + qc4 + "," + qc5 + ",'" + qc5accesories + "'," + qc6 + "," + qc7 + "," + qc8 + "," + qc9 + "," + qc10 + "," + qc11 + "," + qc12 + ",'" + qc12other + "','" + user + "')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public void insertirs(int servicelog, string fmos, string fps, string pe,string user)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_i_investigationreport`(`servicelogID`, `failuremors`, `failurepartsn`, `pe`, `fillpeopleS` ) VALUES (" + servicelog + ",'" + fmos + "','" + fps + "','" + pe + "','" + user +"')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public void insertirpe(int servicelog, string id1, string id2, string id3, string id4, string id5, string id6, string id7, string frcpc , string other, string recommended,string user)
        {
            string sqlstr = "";
            sqlstr = "UPDATE `sv_i_investigationreport` SET `invesdetailed1`='" + id1 + "',`invesdetailed2`='" + id2 + "',`invesdetailed3`='" + id3 + "',`invesdetailed4`='" + id4 + "',`invesdetailed5`='" + id5 + "',`invesdetailed6`='" + id6 + "',`invesdetailed7`='" + id7 + "',`frcpc`='" + frcpc +"',`other`='" + other + "',`recommended`='" + recommended + "',`fillpeoplePE`='" + user + "' WHERE `servicelogID` = " + servicelog;
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }



        public void insertservicereport(int servicelog, string pr, string cf, int fe1, int fe2, int fe3, string fe3msg, int fe4, int fe5, string fe5msg, int fe6, string fe6msg, int oa, string oamsg, int cm, string cmmsg, int acmc, string acmcmsg, int re, string cin1, string cin2, string cin3, string cin4, string cin5, string qty1, string qty2, string qty3, string qty4, string qty5, string lh, string lc, string lt, string th, string tc, string tt, string total, int ws, string fill)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_servicereport`(`servicelogID`, `pr`, `cf`, `fe1`, `fe2`, `fe3`, `fe3msg`, `fe4`, `fe5`, `fe5msg`, `fe6`, `fe6msg`, `oa`, `oamsg`, `cm`, `cmmsg`, `acmc`, `acmcmsg`, `replaced`, `cin1`, `cin2`, `cin3`, `cin4`, `cin5`,`qty1`, `qty2`, `qty3`, `qty4`, `qty5`, `lh`, `lc`, `lt`, `th`, `tc`, `tt`, `totalcost`, `workstatus`, `fillpeople`) VALUES ("+servicelog+",'"+pr+"','"+cf+"',"+fe1+","+fe2+","+fe3+",'"+fe3msg+"',"+fe4+","+fe5+",'"+fe5msg+"',"+fe6+",'"+fe6msg+"',"+oa+",'"+oamsg+"',"+cm+",'"+cmmsg+"',"+acmc+",'"+acmcmsg+"',"+re+",'"+cin1+"','"+cin2+"','"+cin3+"','"+cin4+"','"+cin5+"','"+qty1+"','"+qty2+"','"+qty3+"','"+qty4+"','"+qty5+"','"+lh+"','"+lc+"','"+lt+"','"+th+"','"+tc+"','"+tt+"','"+total+"',"+ws+",'"+fill+"')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public void insertservicefillform(int servicelog, int rrk , int rrq, int acttr , int fr1, int fr2, int fr3,  int fr4, int fr5,  int fr6, string fr6other, int fr7, int fr8, string fr8dcr, int vp, string vpmsg, string fill)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_c_servicefillform`(`servicelogID`, `rrk`, `rrq`, `acttr`, `fr1`, `fr2`, `fr3`, `fr4`, `fr5`, `fr6`, `fr6other`, `fr7`, `fr8`, `fr8dcr`, `vp`, `vpmsg`,`fillpeople`) VALUES (" + servicelog + "," + rrk + "," + rrq + "," + acttr + "," + fr1 + "," + fr2 + "," + fr3 + "," + fr4 + "," + fr5 + "," + fr6 +",'"+fr6other+"'," + fr7 + "," + fr8 + ",'" + fr8dcr + "'," + vp + ",'" + vpmsg +"','"+fill+"')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }



        public void insertdp(int servicelog, string dp, string outdate, int chargeable,string chargeablemsg, string spino, string trackingno,string fill)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_c_disposionprocess`(`servicelogID`, `dp`, `outdate`, `chargeable`,`chargeablemsg`, `spino`, `trackingno`,`fillpeople`) VALUES (" + servicelog + ",'" + dp + "','" + outdate + "'," + chargeable +",'" +chargeablemsg+"','" + spino + "','" + trackingno +"','"+fill+"')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public void insertcapa(int servicelog, string dtc, string describec, string aobrc, string ia1, string ia2, string ia3, string rd1, string rd2, string rd3, string cd1, string cd2, string cd3,string fill)
        {
            string sqlstr = "";
            sqlstr = "INSERT INTO `sv_a_capa`(`servicelogID`, `dtctype`, `dtc`, `aobrc`, `ia1`, `ia2`, `ia3`, `rd1`, `rd2`, `rd3`, `cd1`, `cd2`, `cd3`,`fillpeople`) VALUES (" + servicelog + ",'" + dtc + "','" + describec + "','" + aobrc + "','" + ia1 + "','" + ia2 + "','" + ia3 + "','" + rd1 + "','" + rd2 + "','" + rd3 + "','" + cd1 + "','" + cd2 + "','" + cd3 +"','"+fill+ "')";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public void insertfmea(int servicelog, int fmea, string fmeamsg,string fill)
        {
            string sqlstr = "";
            sqlstr = "UPDATE `sv_a_capa` SET `fmea`=" + fmea + ",`fmeamsg`='" + fmeamsg + "',`fillfix`='" + fill  + "' WHERE `servicelogID` = " + servicelog;
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public void insertvalidation(int servicelog, string validation,string fill)
        {
            string sqlstr = "";
            sqlstr = "UPDATE `sv_a_capa` SET `vd`='" + validation + "',`fillvalidation` = '" + fill + "' WHERE `servicelogID` = " + servicelog;
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public void insertredress(int servicelog, string ia1, string ia2, string ia3, string rd1, string rd2, string rd3, string cd1, string cd2, string cd3)
        {
            string sqlstr = "";
            sqlstr = "UPDATE `sv_a_capa` SET `APredress1`='" + ia1 + "',`APredress2` = '" + ia2 + "',`APredress3` = '" + ia3 + "',`APRD1` = '" + rd1 + "',`APRD2` = '" + rd2 + "',`APRD3` = '" + rd3 + "',`APDD1` = '" + cd1 + "',`APDD2` = '" + cd2 + "',`APDD3` = '" + cd3 + "' WHERE `servicelogID` = " + servicelog;
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public void updateflow(int flow ,int servicelog)
        {
            string sqlstr = "";
            sqlstr = "UPDATE `sv_servicelog` SET `flow`="+flow+" WHERE `ID` = "+servicelog;
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }



        public void updatecapa(int capa, int servicelog)
        {
            string sqlstr = "";
            sqlstr = "UPDATE `sv_servicelog` SET `Needcapa`=" + capa + " WHERE `ID` = " + servicelog;
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }


        public DataTable search_flowtype(int serveceID)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `flowtype`,`IR` FROM `sv_servicelog` WHERE `ID` = "+serveceID;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }



        public DataTable search_servicelogList()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `au_userinfo`.`missysID` ,`sv_servicelog`.`ID`,`sv_servicelog`.`flow` FROM `sv_servicelog` join `au_userinfo` on ( `sv_servicelog`.`fillpeople`=`au_userinfo`.`ID` )";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }



        public DataTable search_all1()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` < 7 ";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_all2()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` > 6 AND `flow` < 13 OR `flow` = 132 ";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_all3()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` > 12 AND `flow` < 21";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_shipping()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 2";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_qc()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 3";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }


        public DataTable search_sr()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 7";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_IRS()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 4";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_IRPE()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 5";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_SFF()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 9";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_DP()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 10";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_CAPA()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 13";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_FIX()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 16";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_Validation()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 17";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }





        /****search_all****/

        public DataTable search_slog(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_servicelog` WHERE `ID` = "+id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_company(string id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `customerName` FROM `customerdetail` WHERE `customerPID` = " + id;
            

            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable search_complaint(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_servicelog_typeofc`  WHERE `servicelogID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_sreport(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_servicereport` WHERE `servicelogID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_investigation(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_i_investigationreport` WHERE `servicelogID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }


        public DataTable search_capa(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_a_capa` WHERE `servicelogID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_sff(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_c_servicefillform` WHERE `servicelogID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_dp(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT * FROM `sv_c_disposionprocess` WHERE `servicelogID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }


        public DataTable search_status(int id)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `Needcapa`,`IR` FROM `sv_servicelog` WHERE `ID` = " + id;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        /***check****/

        public DataTable search_chech_pe()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 6";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_sr()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 8";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_sc()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 11";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_ch()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 12";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_mach()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 14";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_redress()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 15";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_qc()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 18";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_capa()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 19";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_cr()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 20";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }

        public DataTable search_chech_nocapa()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `ID`,`flow` FROM `sv_servicelog` WHERE `flow` = 132";


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }



        /*******/


        public DataTable getpartno()
        {
            string sqlstr = "";
            sqlstr = "SELECT  `partNo` FROM `partno_info` ";
            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
            return myTable;
        }

        public DataTable getpartsn()
        {
            string sqlstr = "";
            sqlstr = "SELECT `SN` FROM `sn_info` ";
            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
            return myTable;
        }



        //***防呆***//

        public DataTable search_flow(int ID)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "SELECT `flow` FROM `sv_servicelog` WHERE `ID` = "+ID;


            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }




    }

}
