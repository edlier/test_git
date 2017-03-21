using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.HR.personnel
{
    public partial class SchoolList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM hr_per_schoollist";
            DataTable dt = GlobalAnnounce.db.GetDataTable(sql);
            Session["dt"] = dt;
            
            BindData(dt, grid_SchoolList);
        }

        protected void btn_Sorting_Click(object sender, EventArgs e)
        {
            string name = string.Empty, sort = string.Empty;
            switch (drop_sort.SelectedIndex)
            {
                case 0:
                    name = "ID";
                    break;
                case 1:
                    name = "SchoolName";
                    break;
                case 2:
                    name = "City";
                    break;
            }

            if (btn_Sorting.Text == "ASC")
            {
                sort = "ASC";
                btn_Sorting.Text = "DESC";
            }
            else if (btn_Sorting.Text == "DESC")
            {
                sort = "DESC";
                btn_Sorting.Text = "ASC";
            }

            DataTable dt = (DataTable)Session["dt"];
            dt.DefaultView.Sort = name + " " + sort;
            BindData(dt, grid_SchoolList);
            
        }

        private void BindData(DataTable dt, GridView gv)
        {
            if (dt != null && dt.Rows.Count > 0)
            { GlobalAnnounce.OtherFuction.BindDataToGrid(dt, gv); }
            else
            { EmptyBindGrid(dt, gv); }
        }

        private void EmptyBindGrid(DataTable dt, GridView gv)
        {
            dt.Constraints.Clear();
            foreach (DataColumn dc in dt.Columns)
            { dc.AllowDBNull = true; }

            // Add a blank row to the dataset
            dt.Columns[0].AllowDBNull = true;
            dt.Rows.Add(dt.NewRow());

            // Bind the DataSet to the GridView
            gv.DataSource = dt;
            gv.DataBind();
            //GlobalAnnounce.OtherFuction.BindDataToGrid(dt, gv);

            // Get the number of columns to know what the Column Span should be
            int columnCount = gv.Rows[0].Cells.Count;

            // Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv.Rows[0].Cells[0].Text = "No Data!";
            gv.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

        }

        private DataTable SearchData(DataRow[] dr, DataTable myDataTable)
        {
            //建立新的DataTable
            DataTable dt = new DataTable();
            //建立新的DataTable Columns
            foreach (DataColumn dc in myDataTable.Columns)
            {
                dt.Columns.Add(dc.ToString());
                dt.Columns[dc.ToString()].DataType = dc.DataType;
            }

            int i = 0;
            //讀取過濾的資料
            foreach (DataRow item in dr)
            {
                DataRow row = dt.NewRow();
                i = 0;
                foreach (DataColumn dc in myDataTable.Columns)
                {
                    //建立DataRow的資料
                    row[dc.ToString()] = item.ItemArray[i];
                    i++;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

    }
}