using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Windows.Forms;
using System.Web;

namespace MisSystem_ClassLibrary
{
    public class unconfirmed
    {
        public DataTable getvalue_dcr(int status)
        {
            string sqlstr = "";
            DataTable tb = new DataTable();

            sqlstr = "select documentchangerequest.id AS 'id',au_computerform.EnFName AS 'changeby',priority.description AS 'priority', RnDprojectName,DCRassignedNo,changePerposeDate,documentchange.status AS 'status'";
            sqlstr += " from documentchangerequest";
            sqlstr += " join au_computerform on (documentchangerequest.changePerposedBy = au_computerform.ID)";
            sqlstr += " join priority on (priority.code = documentchangerequest.priority)";
            sqlstr += " join documentchange on (documentchange.DocumentChangeRequestID = documentchangerequest.id and documentchange.status='" + status + "')";

            tb = publicNewClass.mydb.GetDataTable(sqlstr);
            tb = dateform(tb, "changePerposeDate");
            
            return tb;
        }

        public DataTable getvalue_raar(int status)
        {
            DataTable tb = new DataTable();
            string sqlstr = "";

            sqlstr = "select reviewandapprovalrecord.id as 'id',raarNo,Dept, au_computerform.EnFName AS 'fillBy', au_audep.Description AS 'resdept', DATE_FORMAT(fillDate, '%Y-%m-%d') as fillDate";
            sqlstr += " from reviewandapprovalrecord";
            sqlstr += " join au_computerform on (reviewandapprovalrecord.fillBy = au_computerform.ID)";
            sqlstr += " join au_audep on (au_audep.id = reviewandapprovalrecord.resDept)";
            sqlstr += " join documentchange on (reviewandapprovalrecord.id = documentchange.ReceiveAndApprovalRecordID and documentchange.status='" + status + "')";

            //sqlstr = "select reviewandapprovalrecord.id,raarNo from reviewandapprovalrecord,documentchange where reviewandapprovalrecord.id=documentchange.ReceiveAndApprovalRecordID and documentchange.status='" + status + "'";
            tb = publicNewClass.mydb.GetDataTable(sqlstr);
            //tb = dateform(tb, "fillDate");

            return tb;
        }

        public bool check_raardept(int deptid,int usrdept)
        {
            string sqlstr = "select dept1,dept2,dept3,dept4,dept5,dept6 from raardept where raarID = '" + deptid + "'";
            DataTable dept = publicNewClass.mydb.GetDataTable(sqlstr);
            for (int i = 0; i < dept.Columns.Count; i++)
            {
                if (usrdept.ToString() == dept.Columns[i].ToString())
                    return true;
            }
            return false;
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
    }
}
