using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MSSQL_CON
{
    public class connect_MSSQL
    {
        //String strCon = "Data Source=MY-PC;Initial Catalog=test;Integrated Security=SSPI;";
        //Lightmed_TEST
        //Lightmed_20151023

        public DataTable GetDataTable(string sqlstr)
        {
            SqlConnection conn = new SqlConnection("server=sap-server; database=Lightmed_20151023;uid=sa;pwd=dwins123!@#");
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet dss = new DataSet();
            dss.Clear();
            sda.Fill(dss);
            DataTable ddt = dss.Tables[0];
            cmd.Cancel();
            conn.Close();
            conn.Dispose();
            return (ddt);
        }
    }
}
