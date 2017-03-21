using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MisSystem_ClassLibrary
{
    public class check_raa
    {
        public DataTable getdept(string raarid)
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr = "select dept1,dept2,dept3,dept4,dept5,dept6 from raardept join reviewandapprovalrecord on (raardept.raarid =reviewandapprovalrecord.SubjectID and reviewandapprovalrecord.id='"+raarid+"')";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }
        public string getdeptdes(string dept)
        {
            string des = "";
            string sqlstr = "";
            sqlstr = "select Description from au_audep where id='" + dept + "'";
            des = publicNewClass.mydb.getSingleData(0, sqlstr);

            return des;
        }
        public string getusrdept(string usrid)
        {
            string dep;
            string sqlstr = "";
            sqlstr = "select DepID from au_computerform where ID ='" + usrid + "'";
            dep = publicNewClass.mydb.getSingleData(0, sqlstr);
            
            return dep;
        }
        public DataTable getsubject(string raarid)
        {
            DataTable dt = new DataTable();
            string sqlstr = "";
            sqlstr = "select subject1,subject2,subject3,subject4,subject5 from raarsubject join reviewandapprovalrecord on (raarsubject.raarid =reviewandapprovalrecord.SubjectID and reviewandapprovalrecord.id='"+raarid+"')";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }
        public void updatestatus(string raarid,string status)
        {
            string sqlstr = "";
            sqlstr = "update documentchange set status ='" + status + "' where ReceiveAndApprovalRecordID ='" + raarid + "'";
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public DataTable getcomment(string raarID,string dep)
        {
            string sqlstr = "";
            sqlstr = string.Format("select comment,Manager,checkdate from raar_comment where raarID='{0}' and dep='{1}'", raarID,dep);
            DataTable dt = publicNewClass.mydb.GetDataTable(sqlstr);

            return dt;
        }

        public DataTable insertComment(string raarid,string dep,string comment,string userid,string date)
        {
            string sqlstr = "";
            sqlstr = string.Format("update raar_comment set comment = '{0}',date = '{1}',ManagerCheck = '{2}' where raarID = '{3}' and dep = '{4}';",comment,date,1,raarid,dep);
            sqlstr += "select ManagerCheck from raar_comment where raarID = '" + raarid + "'";
            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public void updateConclusion(string comment,string date,string raarid)
        {
            string sqlstr = "";
            sqlstr = string.Format("update reviewandapprovalrecord set conclusion = '{0}',conclusionCheck = '{1}',conclusionDate = '{2}' where id='{3}'",comment,1,date,raarid);
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }
    }
}
