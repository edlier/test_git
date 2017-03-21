using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace misSystem
{
    public class OtherFuction
    {
        #region Bind Data
        public void BindDataToGrid(DataView dv, GridView grid)
        {
            grid.DataSource = dv;
            grid.DataBind();
        }
        public void BindDataToGrid(DataTable dv, GridView grid)
        {
            grid.DataSource = dv;
            grid.DataBind();
        }

        public void BindDataToDrop(DataView dv, DropDownList drop)
        {
            drop.DataSource = dv;
            drop.DataBind();
            drop.Items.Insert(0, "Choose...");
        }
        public void BindDataToDrop(DataTable dv, DropDownList drop)
        {
            drop.DataSource = dv;
            drop.DataBind();
            drop.Items.Insert(0, "Choose...");
        }
        public void BindDataToDrop(DataTable dv, DropDownList drop,string title)
        {
            drop.DataSource = dv;
            drop.DataBind();
            drop.Items.Insert(0, title);
        }

        #endregion

        public void Drop_SetValueAndText(DropDownList drop, string Value, string Text)
        {
            drop.DataValueField = Value;
            drop.DataTextField = Text;
        }

        #region Object Visible & Enabled CLOSE And OPEN
            #region Drop
        public void CloseDrop_VisibelEnabled(DropDownList dr)
        {
            dr.Visible = false;
            dr.Enabled = false;
        }
        public void Close2Drop_VisibelEnabled(DropDownList dr, DropDownList dr2)
        {
            dr.Visible = false;
            dr.Enabled = false;
            dr2.Visible = false;
            dr2.Enabled = false;
        }

        public void OpenDrop_VisibelEnabled(DropDownList dr)
        {
            dr.Visible = true;
            dr.Enabled = true;
        }
        public void Open2Drop_VisibelEnabled(DropDownList dr,DropDownList dr2)
        {
            dr.Visible = true;
            dr.Enabled = true;
            dr2.Visible = true;
            dr2.Enabled = true;
        }
        #endregion
            #region Label
        public void CloseLabel_VisibelEnabled(Label dr)
        {
            dr.Visible = false;
            dr.Enabled = false;
        }
        public void OpenLabel_VisibelEnabled(Label dr)
        {
            dr.Visible = true;
            dr.Enabled = true;
        }
        #endregion
        #endregion

        public void AlertDialog(Page pg, string alertText)
        {
            ScriptManager.RegisterStartupScript(pg, pg.GetType(), "alert", "alert('"+alertText+"');", true);
        }


    }
}