using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace misSystem.Service.Service
{
    public partial class test : System.Web.UI.Page
    {
        DataTable servicelogList = new DataTable();
        DataTable servicelogList2 = new DataTable();
        DataTable servicelogList3 = new DataTable();
        int qc = 0;
        int shipping = 0;
        int igrs = 0;
        int igrpe = 0;
        int pe = 0;


        
        int servicereport  =0;
        int checksr    =0;
        int sff  =0;
        int dp   =0;
        int checksup  =0;
        int check   =0;
        int nocapa = 0;


        int capa  =0;
        int manager=0;
        int redress=0;
        int fmea=0;
        int validation=0;
        int qcmanager=0;
        int srccapa=0;
        int srccr = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                servicelogList = GlobalAnnounce.serviceDB.search_all1();
                servicelogList2 = GlobalAnnounce.serviceDB.search_all2();
                servicelogList3 = GlobalAnnounce.serviceDB.search_all3();
                servicelogList.Columns.Add("status");

                servicelogList.Columns.Add("shipping");
                servicelogList.Columns.Add("qc");
                servicelogList.Columns.Add("igrs");
                servicelogList.Columns.Add("igrpe");
                servicelogList.Columns.Add("pe");


                servicelogList2.Columns.Add("servicereport");
                servicelogList2.Columns.Add("checksr");
                servicelogList2.Columns.Add("sff");
                servicelogList2.Columns.Add("dp");
                servicelogList2.Columns.Add("checksup");
                servicelogList2.Columns.Add("check");
                servicelogList2.Columns.Add("nocapa");

                servicelogList3.Columns.Add("capa");
                servicelogList3.Columns.Add("manager");
                servicelogList3.Columns.Add("redress");
                servicelogList3.Columns.Add("fmea");
                servicelogList3.Columns.Add("validation");
                servicelogList3.Columns.Add("qcmanager");
                servicelogList3.Columns.Add("srccapa");
                servicelogList3.Columns.Add("srccr");


                for (int i = 0; i < servicelogList.Rows.Count; i++)
                {
                    if (servicelogList.Rows[i]["flow"].ToString() == "2")
                    {
                        servicelogList.Rows[shipping]["shipping"] = servicelogList.Rows[i]["ID"].ToString();
                        shipping += 1;
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "3")
                    {

                        servicelogList.Rows[qc]["qc"] = servicelogList.Rows[i]["ID"].ToString();
                        qc += 1;
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "4")
                    {

                        servicelogList.Rows[igrs]["igrs"] = servicelogList.Rows[i]["ID"].ToString();
                        igrs += 1;
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "5")
                    {

                        servicelogList.Rows[igrpe]["igrpe"] = servicelogList.Rows[i]["ID"].ToString();
                        igrpe += 1;
                    }
                    else if (servicelogList.Rows[i]["flow"].ToString() == "6")
                    {

                        servicelogList.Rows[pe]["pe"] = servicelogList.Rows[i]["ID"].ToString();
                        pe += 1;
                    }


                }







                for (int i = 0; i < servicelogList2.Rows.Count; i++)
                {

                if (servicelogList2.Rows[i]["flow"].ToString() == "7")
                    {

                        servicelogList2.Rows[servicereport]["servicereport"] = servicelogList2.Rows[i]["ID"].ToString();
                        servicereport += 1;
                    }
                    else if (servicelogList2.Rows[i]["flow"].ToString() == "8")
                    {

                        servicelogList2.Rows[checksr]["checksr"] = servicelogList2.Rows[i]["ID"].ToString();
                        checksr += 1;
                    }
                    else if (servicelogList2.Rows[i]["flow"].ToString() == "9")
                    {

                        servicelogList2.Rows[sff]["sff"] = servicelogList2.Rows[i]["ID"].ToString();
                        sff += 1;
                    }
                    else if (servicelogList2.Rows[i]["flow"].ToString() == "10")
                    {

                        servicelogList2.Rows[dp]["dp"] = servicelogList2.Rows[i]["ID"].ToString();
                        dp += 1;
                    }
                    else if (servicelogList2.Rows[i]["flow"].ToString() == "11")
                    {

                        servicelogList2.Rows[checksup]["checksup"] = servicelogList2.Rows[i]["ID"].ToString();
                        checksup += 1;
                    }
                    else if (servicelogList2.Rows[i]["flow"].ToString() == "12")
                    {

                        servicelogList2.Rows[check]["check"] = servicelogList2.Rows[i]["ID"].ToString();
                        check += 1;
                    }
                    else if (servicelogList2.Rows[i]["flow"].ToString() == "132")
                    {

                        servicelogList2.Rows[nocapa]["nocapa"] = servicelogList2.Rows[i]["ID"].ToString();
                        nocapa += 1;
                    }
                }






                for(int i = 0; i < servicelogList3.Rows.Count; i++)
                {


                  if (servicelogList3.Rows[i]["flow"].ToString() == "13")
                  {
                      servicelogList3.Rows[capa]["capa"] = servicelogList3.Rows[i]["ID"].ToString();
                      capa++;
                  }
                  else if(servicelogList3.Rows[i]["flow"].ToString() == "14")
                  {
                      servicelogList3.Rows[manager]["manager"] = servicelogList3.Rows[i]["ID"].ToString();
                      manager++;
                  }
                  else if (servicelogList3.Rows[i]["flow"].ToString() == "15")
                  {
                      servicelogList3.Rows[redress]["redress"] = servicelogList3.Rows[i]["ID"].ToString();
                      redress++;
                  }
                  else if (servicelogList3.Rows[i]["flow"].ToString() == "16")
                  {
                      servicelogList3.Rows[fmea]["fmea"] = servicelogList3.Rows[i]["ID"].ToString();
                      fmea++;
                  }
                  else if (servicelogList3.Rows[i]["flow"].ToString() == "17")
                  {
                      servicelogList3.Rows[validation]["validation"] = servicelogList3.Rows[i]["ID"].ToString();
                      validation++;
                  }
                  else if (servicelogList3.Rows[i]["flow"].ToString() == "18")
                  {
                      servicelogList3.Rows[qcmanager]["qcmanager"] = servicelogList3.Rows[i]["ID"].ToString();
                      qcmanager++;
                  }
                  else if (servicelogList3.Rows[i]["flow"].ToString() == "19")
                  {
                      servicelogList3.Rows[srccapa]["srccapa"] = servicelogList3.Rows[i]["ID"].ToString();
                      srccapa++;
                  }
                  else if (servicelogList3.Rows[i]["flow"].ToString() == "20")
                  {
                      servicelogList3.Rows[srccr]["srccr"] = servicelogList3.Rows[i]["ID"].ToString();
                      srccr++;
                  }


                }




                grid_UserList.DataSource = servicelogList;
                grid_UserList.DataBind();

                GridView1.DataSource = servicelogList2;
                GridView1.DataBind();

                GridView2.DataSource = servicelogList3;
                GridView2.DataBind();

               
                
            }
        }
    }
}