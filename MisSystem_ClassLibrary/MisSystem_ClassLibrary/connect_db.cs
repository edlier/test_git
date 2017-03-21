using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
namespace MisSystem_ClassLibrary
{
    public class db
    {
        //missystem
        //test3
        // 如果有特殊的編碼在database後面請加上;CharSet=編碼, utf8請使用utf8_general_ci
        string cn = "server=localhost;user id=root;Password=root;persist security info=True;database=missystem;CharSet=utf8";
        MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;Password=root;persist security info=True;database=missystem;CharSet=utf8");
        //string cn = "server=192.168.168.184;port=3306;user id=bonnie;Password=Bonnie01612;persist security info=True;database=lightmedbpm;CharSet=utf8";
        //MySqlConnection conn = new MySqlConnection("server=192.168.168.184;port=3306;user id=bonnie;Password=Bonnie01612;persist security info=True;database=lightmedbpm;CharSet=utf8");

        //insert update  delete
        public void InsertDataTable(string sqlstr)
        {

            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand();
            cmd = new MySqlCommand(sqlstr, conn);
            conn.Open();
            da.InsertCommand = cmd;
            da.InsertCommand.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable GetDataTable(string sqlstr)
        {
            
            MySqlDataAdapter da = new MySqlDataAdapter(sqlstr, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return (dt);
        }

        public string qo(string instr)
        {

            return "'" + instr + "'";
        }

        public string getSingleData(int item_no, string sqlstr)
        {
            string temp = "";
            MySqlCommand cmd = new MySqlCommand();
            cmd = new MySqlCommand(sqlstr, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp = reader[item_no].ToString();
            }
            conn.Close();
            return temp;
        }

        public int getID(string table)
        {
            //get the lastest id
            string sqlstr = "";
            sqlstr = string.Format("select LAST_INSERT_ID() from {0}", table);
            int ID = Convert.ToInt32(getSingleData(0, sqlstr));
            return ID;
        }

        public int ExecuteCmd(string sqlstr, int liCmdTimeOut, MySqlParameterCollection parameters, out string returnMessage)
        {
            string lsParsValue = "";
            returnMessage = string.Empty;
            int liExecute = 0;

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand(sqlstr, conn);
                cmd.CommandTimeout = liCmdTimeOut;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                //處理參數 
                if (parameters != null)
                {
                    cmd.Parameters.Clear();
                    lsParsValue = "";
                    foreach (MySqlParameter item in parameters)
                    {
                        cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                        lsParsValue += item.ParameterName.ToString() + "=" + item.Value.ToString();
                    }
                    //-------------------------------- 
                }

                //da.InsertCommand = cmd;
                //da.InsertCommand.ExecuteNonQuery(); 
                liExecute = cmd.ExecuteNonQuery();
                conn.Close();
                returnMessage = "Done.";
                return liExecute;
            }
            catch (Exception ex)
            {
                returnMessage = "執行SQL命令時發生錯誤!" + "錯誤訊息:\n" + ex.Message;
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                liExecute = -1;

            }
            conn.Dispose();
            return liExecute;
        }
    }
}
