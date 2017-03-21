using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using System.Windows.Forms;
using System.Drawing;

namespace misSystem.regulatory
{
    public partial class regulatory_form_DCR : System.Web.UI.Page
    {
        int userID;
        //DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoneMIS_AU = "../Default.aspx";
            string NoneLogin = "../account/login.aspx";
            //GlobalAnnounce.validateSession.validateMIS_AU(this.Page, NoneMIS_AU, NoneLogin);

            //顯示目前使用者
            userID = Convert.ToInt32(Session[SessionString.userID]);
            string changeProposedBy = GlobalAnnounce.id.getName(userID);
            lbl_proposedPerson.Text = changeProposedBy;

            //顯示目前日期
            DateTime dateNow = DateTime.Now;
            lbl_proposedDate.Text = dateNow.ToString("yyyy/MM/dd");
            if (!IsPostBack) 
            {
                //Priority的選擇項
                drop_priority.DataSource = GlobalAnnounce.regulatory_DCR.priorityOption();
                drop_priority.DataBind();
            
                //Machine的選項
                drop_product.DataSource = GlobalAnnounce.machine.searchMachine();
                drop_product.DataBind();

                //affected select option
                DataTable affectdes = GlobalAnnounce.regulatory_DCR.getaffectdes();
                for(int i=0;i<affectdes.Rows.Count;i++)
                {
                    cblist.Items.Add(new ListItem(affectdes.Rows[i][1].ToString()));
                }
                
            }
            


        }
        protected void drop_changeReason_1_Init(object sender, EventArgs e)
        {
            DataTable reasonForChangerNumber = new DataTable();
            reasonForChangerNumber = GlobalAnnounce.regulatory_DCR.reasonForChangeNumber();

            drop_changeReason_1.DataSource = reasonForChangerNumber;
            drop_changeReason_1.DataBind();
            drop_changeReason_2.DataSource = reasonForChangerNumber;
            drop_changeReason_2.DataBind();
            drop_changeReason_3.DataSource = reasonForChangerNumber;
            drop_changeReason_3.DataBind();
            drop_changeReason_4.DataSource = reasonForChangerNumber;
            drop_changeReason_4.DataBind();
            drop_changeReason_5.DataSource = reasonForChangerNumber;
            drop_changeReason_5.DataBind();
        }

        //submit=============================================================
        protected void btn_DCR_submint_Click(object sender, EventArgs e)
        {
            int count = 0;
            int[] flagNum = new int[5];
            bool error_doc = true;
            int affectselect = 0;

            string[] docNum = new string[5];
            int[] newVersion = new int[5];
            int[] oldVersion = new int[5];
            string[] docName = new string[5];
            int[] changeReason = new int[5];
            string[] changeReasonText = new string[5];
            int[] affectedDoc = new int[15];

            if (tb_docNum_1.Text != "" && tb_newVersion_1.Text != "" && tb_oldVersion_1.Text != "" && tb_docName_1.Text != "" && drop_changeReason_1.SelectedValue != "0")
            {
                //count++; flagNum[0] = 1;
                docNum[0] = tb_docNum_1.Text;
                newVersion[0] = Convert.ToInt32(tb_newVersion_1.Text);
                oldVersion[0] = Convert.ToInt32(tb_oldVersion_1.Text);
                docName[0] = tb_docName_1.Text;
                changeReason[0] = Convert.ToInt32(drop_changeReason_1.SelectedValue);
                changeReasonText[0] = tb_other_1.Text;

                tbl_doc.Rows[1].BorderWidth = 0;
                tbl_doc.Rows[1].BorderColor = Color.Black;
            }
            else
            {
                error_doc = false;
                tbl_doc.Rows[1].BorderWidth = 2;
                tbl_doc.Rows[1].BorderColor = Color.Red;
                //lbl_err.Text = "請至少填寫一個完整項目";
            }

            if (tb_docNum_2.Text != "" || tb_newVersion_2.Text != "" || tb_oldVersion_2.Text != "" || tb_docName_2.Text != "" || drop_changeReason_2.SelectedValue != "0")
            {
                if (tb_docNum_2.Text != "" && tb_newVersion_2.Text != "" && tb_oldVersion_2.Text != "" && tb_docName_2.Text != "" && drop_changeReason_2.SelectedValue != "0")
                {
                    //count++; flagNum[1] = 1;
                    docNum[1] = tb_docNum_2.Text;
                    newVersion[1] = Convert.ToInt32(tb_newVersion_2.Text);
                    oldVersion[1] = Convert.ToInt32(tb_oldVersion_2.Text);
                    docName[1] = tb_docName_2.Text;
                    changeReason[1] = Convert.ToInt32(drop_changeReason_2.SelectedValue);
                    changeReasonText[1] = tb_other_2.Text;

                    tbl_doc.Rows[2].BorderWidth = 0;
                    tbl_doc.Rows[2].BorderColor = Color.Black;
                }
                else
                {
                    error_doc = false;
                    tbl_doc.Rows[2].BorderWidth = 2;
                    tbl_doc.Rows[2].BorderColor = Color.Red;
                }
            }
            
            
            if (tb_docNum_3.Text != "" || tb_newVersion_3.Text != "" || tb_oldVersion_3.Text != "" || tb_docName_3.Text != "" || drop_changeReason_3.SelectedValue != "0")
            {
                if (tb_docNum_3.Text != "" && tb_newVersion_3.Text != "" && tb_oldVersion_3.Text != "" && tb_docName_3.Text != "" && drop_changeReason_3.SelectedValue != "0")
                {
                    //count++; flagNum[2] = 1;
                    docNum[2] = tb_docNum_3.Text;
                    newVersion[2] = Convert.ToInt32(tb_newVersion_3.Text);
                    oldVersion[2] = Convert.ToInt32(tb_oldVersion_3.Text);
                    docName[2] = tb_docName_3.Text;
                    changeReason[2] = Convert.ToInt32(drop_changeReason_3.SelectedValue);
                    changeReasonText[2] = tb_other_3.Text;

                    tbl_doc.Rows[3].BorderWidth = 0;
                    tbl_doc.Rows[3].BorderColor = Color.Black;
                }
                else
                {
                    error_doc = false;
                    tbl_doc.Rows[3].BorderWidth = 2;
                    tbl_doc.Rows[3].BorderColor = Color.Red;
                }
            }
            
            if (tb_docNum_4.Text != "" || tb_newVersion_4.Text != "" || tb_oldVersion_4.Text != "" || tb_docName_4.Text != "" || drop_changeReason_4.SelectedValue != "0")
            {
                if (tb_docNum_4.Text != "" && tb_newVersion_4.Text != "" && tb_oldVersion_4.Text != "" && tb_docName_4.Text != "" && drop_changeReason_4.SelectedValue != "0")
                {
                    //count++; flagNum[3] = 1;
                    docNum[3] = tb_docNum_4.Text;
                    newVersion[3] = Convert.ToInt32(tb_newVersion_4.Text);
                    oldVersion[3] = Convert.ToInt32(tb_oldVersion_4.Text);
                    docName[3] = tb_docName_4.Text;
                    changeReason[3] = Convert.ToInt32(drop_changeReason_4.SelectedValue);
                    changeReasonText[3] = tb_other_4.Text;

                    tbl_doc.Rows[4].BorderWidth = 0;
                    tbl_doc.Rows[4].BorderColor = Color.Black;
                }
                else
                {
                    error_doc = false;
                    tbl_doc.Rows[4].BorderWidth = 2;
                    tbl_doc.Rows[4].BorderColor = Color.Red;
                }
            }

            if (tb_docNum_5.Text != "" || tb_newVersion_5.Text != "" || tb_oldVersion_5.Text != "" || tb_docName_5.Text != "" || drop_changeReason_5.SelectedValue != "0")
            {
                if (tb_docNum_5.Text != "" && tb_newVersion_5.Text != "" && tb_oldVersion_5.Text != "" && tb_docName_5.Text != "" && drop_changeReason_5.SelectedValue != "0")
                {
                    //count++; flagNum[4] = 1;
                    docNum[4] = tb_docNum_5.Text;
                    newVersion[4] = Convert.ToInt32(tb_newVersion_5.Text);
                    oldVersion[4] = Convert.ToInt32(tb_oldVersion_5.Text);
                    docName[4] = tb_docName_5.Text;
                    changeReason[4] = Convert.ToInt32(drop_changeReason_5.SelectedValue);
                    changeReasonText[4] = tb_other_5.Text;

                    tbl_doc.Rows[5].BorderWidth = 0;
                    tbl_doc.Rows[5].BorderColor = Color.Black;
                }
                else
                {
                    error_doc = false;
                    tbl_doc.Rows[5].BorderWidth = 2;
                    tbl_doc.Rows[5].BorderColor = Color.Red;
                }
            }

            for (int i = 0; i < cblist.Items.Count; i++)
            {
                if (cblist.Items[i].Selected)
                {
                    affectselect++;
                    affectedDoc[i + 1] = 1;
                }
            }

            if (error_doc)
                lbl_err.Visible = false;
            else
                lbl_err.Visible = true;

            if (affectselect > 0)
                lbl_err01.Visible = false;
            else
                lbl_err01.Visible = true;


            if (error_doc && affectselect > 0)
            {
                int[] form_changeDocDetail = new int[4];
                int affectedDocID;
                string DCRassignedNo = tb_assignedNo.Text;

                if (!GlobalAnnounce.regulatory_DCR.DCRNoIsExist(DCRassignedNo))
                {
                    form_changeDocDetail = GlobalAnnounce.regulatory_DCR.insert_DCR_form_changeDocDetail(docNum, newVersion, oldVersion, docName, changeReason, changeReasonText);
                    affectedDocID = GlobalAnnounce.regulatory_DCR.insert_DCR_affectedDoc(affectedDoc, tb_other_affectDoc_1.Text);

                    //DCR init
                    int changePerposedBy = Convert.ToInt32(Session["userID"]);
                    string changePerposeDate = lbl_proposedDate.Text;
                    int productModel = int.Parse(drop_product.SelectedValue);
                    string RnDProjectName = tb_projectName.Text;
                    string priority = drop_priority.SelectedValue;
                    int DCRDocNumID = form_changeDocDetail[0];
                    int DCRDocNameID = form_changeDocDetail[1];
                    int DCRversionID = form_changeDocDetail[2];
                    int changeReasonID = form_changeDocDetail[3];
                    int AffectedDocID = affectedDocID;

                    int DCRid = GlobalAnnounce.regulatory_DCR.insert_DCR(changePerposedBy, changePerposeDate, productModel, RnDProjectName, priority, DCRDocNumID, DCRDocNameID, changeReasonID, AffectedDocID, DCRversionID, DCRassignedNo);
                    GlobalAnnounce.regulatory_DCR.insert_DocumentChange_DCR_ID(DCRid);

                    Response.Redirect("regulatory_form_raar.aspx?no=" + DCRassignedNo);
                }
                else
                    Response.Write("<script>alert(\'The Assigned No is Exist\')</script>");
            }
        }

        protected void drop_changeReason_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_changeReason_1.SelectedValue == 10.ToString() || drop_changeReason_1.SelectedValue == 9.ToString() || drop_changeReason_1.SelectedValue == 8.ToString())
            {
                tb_other_1.Visible = true;
            }
            else
            {
                tb_other_1.Visible = false;
            }
        }
        protected void drop_changeReason_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_changeReason_2.SelectedValue == 10.ToString() || drop_changeReason_2.SelectedValue == 9.ToString() || drop_changeReason_2.SelectedValue == 8.ToString())
            {
                tb_other_2.Visible = true;
            }
            else
            {
                tb_other_2.Visible = false;
            }
        }
        protected void drop_changeReason_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_changeReason_3.SelectedValue == 10.ToString() || drop_changeReason_3.SelectedValue == 9.ToString() || drop_changeReason_3.SelectedValue == 8.ToString())
            {
                tb_other_3.Visible = true;
            }
            else
            {
                tb_other_3.Visible = false;
            }
        }
        protected void drop_changeReason_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_changeReason_4.SelectedValue == 10.ToString() || drop_changeReason_4.SelectedValue == 9.ToString() || drop_changeReason_4.SelectedValue == 8.ToString())
            {
                tb_other_4.Visible = true;
            }
            else
            {
                tb_other_4.Visible = false;
            }
        }
        protected void drop_changeReason_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_changeReason_5.SelectedValue == 10.ToString() || drop_changeReason_5.SelectedValue == 9.ToString() || drop_changeReason_5.SelectedValue == 8.ToString())
            {
                tb_other_5.Visible = true;
            }
            else
            {
                tb_other_5.Visible = false;
            }
        }

        protected void cblist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cblist.Items[13].Selected)
                tb_other_affectDoc_1.Visible = true;
            else
                tb_other_affectDoc_1.Visible = false;
        }
    }
}