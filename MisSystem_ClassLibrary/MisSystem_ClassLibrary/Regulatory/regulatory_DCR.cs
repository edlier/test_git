using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Windows.Forms;

namespace MisSystem_ClassLibrary
{
    public class regulatory_DCR
    {
        public DataTable priorityOption()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select code,description from priority ";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable reasonForChangeNumber()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select No ,CONCAT_WS('  ,  ',No,Description) as NoDescription from dcrReasonForChange ";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public int[] insert_DCR_form_changeDocDetail(string[] docNum, int[] newVersion, int[] oldVersion, string[] docName, int[] changeReason,string[] changeReasonText)
        {
            string sqlstr = "";
            int[] form_changeDocDetail = new int[4];

            //insert into dcrdocnum
            sqlstr = string.Format("insert into dcrdocnum(DCRDocNum1,DCRDocNum2,DCRDocNum3,DCRDocNum4,DCRDocNum5) values('{0}','{1}','{2}','{3}','{4}');", docNum[0], docNum[1], docNum[2], docNum[3], docNum[4]);
            sqlstr += "select LAST_INSERT_ID() from dcrdocnum";
            form_changeDocDetail[0] = int.Parse(publicNewClass.mydb.GetDataTable(sqlstr).Rows[0][0].ToString());
            //publicNewClass.mydb.getID("dcrdocnum");

            //insert into dcrdocname
            sqlstr = string.Format("insert into dcrdocname(DocName1,DocName2,DocName3,DocName4,DocName5) values('{0}','{1}','{2}','{3}','{4}');", docName[0], docName[1], docName[2], docName[3], docName[4]);
            sqlstr += "select LAST_INSERT_ID() from dcrdocname";
            //publicNewClass.mydb.InsertDataTable(sqlstr);
            form_changeDocDetail[1] = int.Parse(publicNewClass.mydb.GetDataTable(sqlstr).Rows[0][0].ToString());

            //insert into dcrversion
            sqlstr = string.Format("insert into dcrversion(newVersion1,newVersion2,newVersion3,newVersion4,newVersion5,oldVersion1,oldVersion2,oldVersion3,oldVersion4,oldVersion5) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');", newVersion[0], newVersion[1], newVersion[2], newVersion[3], newVersion[4], oldVersion[0], oldVersion[1], oldVersion[2], oldVersion[3], oldVersion[4]);
            sqlstr += "select LAST_INSERT_ID() from dcrversion";
            //publicNewClass.mydb.InsertDataTable(sqlstr);
            form_changeDocDetail[2] = int.Parse(publicNewClass.mydb.GetDataTable(sqlstr).Rows[0][0].ToString());

            //insert into dcrdocchangereason
            sqlstr = string.Format("insert into dcrdocchangereason(ChangeReason1,ChangeReason2,ChangeReason3,ChangeReason4,ChangeReason5) values('{0}','{1}','{2}','{3}','{4}');", changeReason[0], changeReason[1], changeReason[2], changeReason[3], changeReason[4]);
            sqlstr += "select LAST_INSERT_ID() from dcrdocchangereason";
            //publicNewClass.mydb.InsertDataTable(sqlstr);
            int changeReasonID;
            form_changeDocDetail[3] = changeReasonID = int.Parse(publicNewClass.mydb.GetDataTable(sqlstr).Rows[0][0].ToString());
            //update changeReasonText in ChangeReason8-10
            for (int i = 0; i < 5;i++ )
            {
                if (changeReason[i] == 8)
                {
                    sqlstr = string.Format("update dcrdocchangereason set ChangeReason8No{0} = '{1}' where id = '{2}'", i + 1, changeReasonText[i], changeReasonID);
                    publicNewClass.mydb.InsertDataTable(sqlstr);
                }
                if (changeReason[i] == 9)
                {
                    sqlstr = string.Format("update dcrdocchangereason set ChangeReason9No{0} = '{1}' where id = '{2}'", i + 1, changeReasonText[i], changeReasonID);
                    publicNewClass.mydb.InsertDataTable(sqlstr);
                }
                if (changeReason[i] == 10)
                {
                    sqlstr = string.Format("update dcrdocchangereason set ChangeReason10No{0} = '{1}' where id = '{2}'", i + 1, changeReasonText[i], changeReasonID);
                    publicNewClass.mydb.InsertDataTable(sqlstr);
                }
            }
            return form_changeDocDetail;
        }
        public int insert_DCR_affectedDoc(int[] affectedDoc,string other) 
        {
            string sqlstr = "";

            sqlstr = string.Format("insert into dcraffectdoc(AffectDoc01,AffectDoc02,AffectDoc03,AffectDoc04,AffectDoc05,AffectDoc06,AffectDoc07,AffectDoc08,AffectDoc09,AffectDoc10,AffectDoc11,AffectDoc12,AffectDoc13,AffectDoc14) ");
            sqlstr += string.Format("values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", affectedDoc[1], affectedDoc[2], affectedDoc[3], affectedDoc[4], affectedDoc[5], affectedDoc[6], affectedDoc[7], affectedDoc[8], affectedDoc[9], affectedDoc[10], affectedDoc[11], affectedDoc[12], affectedDoc[13], affectedDoc[14]);
            publicNewClass.mydb.InsertDataTable(sqlstr);
            int affectedDocID = publicNewClass.mydb.getID("dcraffectdoc");
            if (affectedDoc[14] == 1)
            {
                sqlstr = string.Format("update dcraffectdoc set AffectDoc14O='{0}' where id='{1}'", other, affectedDocID);
                publicNewClass.mydb.InsertDataTable(sqlstr);
            }
            return affectedDocID;
        }

        public int insert_DCR(int changePerposedBy, string changePerposeDate, int productModel, string RnDProjectName, string priority, int DCRDocNumID, int DCRDocNameID, int changeReasonID, int AffectedDocID, int DCRversionID, string DCRassignedNo)
        {
            string sqlstr = "";
            sqlstr = string.Format("insert into documentchangerequest(changePerposedBy,changePerposeDate,productModel,RnDProjectName,priority,DCRDocNumID,DCRDocNameID,changeReasonID,AffectedDocID,DCRversionID,DCRassignedNo ) ");
            sqlstr += string.Format("values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", changePerposedBy, changePerposeDate, productModel, RnDProjectName, priority, DCRDocNumID, DCRDocNameID, changeReasonID, AffectedDocID, DCRversionID, DCRassignedNo);
            publicNewClass.mydb.InsertDataTable(sqlstr);
            int dcr_id = publicNewClass.mydb.getID("documentchangerequest");
            return dcr_id;
        }
        public void insert_DocumentChange_DCR_ID(int dcr_id)
        {
            string sqlstr = "";
            sqlstr = string.Format("insert into documentchange(DocumentChangeRequestID,status) values('{0}','{1}')", dcr_id,1);
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }
        public int returnWaitingApprovalRecordCount(int accountID)
        {
            int x=0;

            return x;
        }

        public DataTable getaffectdes()
        {
            string sqlstr = "";
            sqlstr = string.Format("select * from dcraffecteddocsdes order by id");
            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public bool DCRNoIsExist(string dcrno)
        {
            string sqlstr = "";
            sqlstr = string.Format("select id from documentchangerequest where DCRassignedNo ='{0}'",dcrno);
            DataTable result = publicNewClass.mydb.GetDataTable(sqlstr);
            if (result.Rows.Count != 0)
                return true;
            else
                return false; 
        }
    }
}
