using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Windows.Forms;

namespace MisSystem_ClassLibrary
{
    public class regulatory_Raar
    {
        public DataTable resdeptOption()
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "select id,description from au_audep ";
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
            return dt;
        }
        public DataTable getuserdept(int id)
        {
            DataTable deps = new DataTable();
            string sqlstr = "";
            
            //sqlstr = "select description from deps where id in (select deps1 from useraccount where id='" + id + "')";
            //deps[0] = publicNewClass.mydb.getSingleData(0,sqlstr);
            //sqlstr = "select description from deps where id in (select deps2 from useraccount where id='" + id + "')";
            //deps[1] = publicNewClass.mydb.getSingleData(0, sqlstr);

            sqlstr = "select au_audep.Description as des,au_audep.id as id from au_audep join au_computerform on (au_computerform.DepID = au_audep.id) where au_computerform.ID='" + id + "'";
            deps = publicNewClass.mydb.GetDataTable(sqlstr);
            return deps;
        }
        public DataTable getuser_dcrasNo(int userid)
        {
            string sqlstr = "";
            DataTable dcras_No;
            sqlstr = string.Format("select DCRassignedNo from documentchangerequest where changePerposedBy = '{0}' and id in (select DocumentChangeRequestID from documentchange where status = '{1}')", userid, 1);
            dcras_No = publicNewClass.mydb.GetDataTable(sqlstr);
            return dcras_No;
        }
        public int insert_raar_form(int fillby,int SubjectID,int Dept,string fillDate,string resdept,string raarno)
        {
            string sqlstr = "";
            sqlstr = string.Format("insert into reviewandapprovalrecord(fillby,SubjectID,Dept,fillDate,resDept,raarNo) values('{0}','{1}','{2}','{3}','{4}','{5}')", fillby, SubjectID, Dept, fillDate,resdept,raarno);
            publicNewClass.mydb.InsertDataTable(sqlstr);
            int raarid = publicNewClass.mydb.getID("reviewandapprovalrecord");
            return raarid;
        }
        public int insert_raardept(int[] dept) 
        {
            string sqlstr = "";
            sqlstr = string.Format("insert into raardept(dept1,dept2,dept3,dept4,dept5) values('{0}','{1}','{2}','{3}','{4}')", dept[0], dept[1], dept[2], dept[3], dept[4]);
            publicNewClass.mydb.InsertDataTable(sqlstr);
            int deptid = publicNewClass.mydb.getID("raardept");
            return deptid;
        }
        public void insert_raardeptcomment(int dep,int raarid)
        {
            string sqlstr = "";
            sqlstr = string.Format("select userID from au_managerlist where DepID = '{0}'",dep);
            int manager = int.Parse(publicNewClass.mydb.getSingleData(0,sqlstr));
            sqlstr = string.Format("insert into raar_comment(dep,raarID,Manager) value('{0}','{1}','{2}')", dep, raarid, manager);
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }
        public int insert_raarsubject(string[] subject)
        {
            string sqlstr = "";
            sqlstr = string.Format("insert into raarsubject(subject1,subject2,subject3,subject4,subject5) values('{0}','{1}','{2}','{3}','{4}');", subject[0], subject[1], subject[2], subject[3], subject[4]);
            sqlstr += "select LAST_INSERT_ID() from raarsubject";
            int subjectid = int.Parse(publicNewClass.mydb.GetDataTable(sqlstr).Rows[0][0].ToString());

            return subjectid;
        }
        public void update_documentchange(int raarid,string id)
        {
            string sqlstr = "";
            sqlstr = string.Format("update documentchange set ReceiveAndApprovalRecordID='{0}',status='{1}' where id='{2}'",raarid,2,id);
            publicNewClass.mydb.InsertDataTable(sqlstr);
        }

        public DataTable getdeptManager(int dept)
        {
            string sqlstr = "";
            sqlstr = "select userID from au_managerlist where DepID = '" + dept + "'";
            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable getNoneRaarDCRNo()
        {
            string sqlstr = "";
            sqlstr = "select DCRassignedNo as no,documentchange.id as id from documentchangerequest,documentchange where documentchangerequest.id = documentchange.DocumentChangeRequestID and documentchange.status='1'";
            return publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public DataTable DCRNoIsExist(string dcrno)
        {
            string sqlstr = "";
            sqlstr = "select DCRassignedNo from documentchangerequest where DCRassignedNo ='" + dcrno + "'";
            return publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }
}
