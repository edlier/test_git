using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace MisSystem_ClassLibrary.HR.personnel
{
    public class EmployeeDB
    {
        DateTime now = DateTime.Now;
        string key = "4C494748544D4544434F5250524F524154494F4E406C696768746D65642E636F6D";

        //取得當年度離職員工
        public DataTable search_Leaved()
        {
            string sqlstr = "";

            sqlstr = "select EmpPID, EmpNo, EmpName, ResignationDT from hr_per_employee"
                + " WHERE status='RESIGNATION' AND year(ResignationDT)=" + publicNewClass.mydb.qo(now.Year.ToString())
                + " ORDER BY ResignationDT, EmpNo";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //取得季壽星
        public DataTable search_QuarBirth(int quar)
        {
            string sqlstr = "";

            sqlstr = "select EmpPID, EmpName, EmpNo, Birthday from hr_per_employee"
                + " WHERE QUARTER(Birthday)=" + quar + " AND ResignationDT<=>NULL"
                + " ORDER BY Month(Birthday), Day(Birthday)";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //取得所有員工資料
        public DataTable search_EmpList(string dept, string date1,string date2)
        {
            string sqlstr = "";

            sqlstr = "SELECT hr_per_employee.EmpPID, hr_per_employee.EmpName, hr_per_employee.DeptID, hr_per_employee.EmpNo, hr_per_employee.EmpCountry, hr_per_employee.InitSeniority, hr_per_employee.OnboardDT, hr_per_dept.Description as Dept FROM hr_per_employee";
            sqlstr += " JOIN hr_per_dept ON (hr_per_employee.DeptID = hr_per_dept.DeptPID)";
            sqlstr += " WHERE ( (hr_per_employee.Status='RESIGNATION' AND month(hr_per_employee.ResignationDT)=" + publicNewClass.mydb.qo(now.Month.ToString()) + ") OR hr_per_employee.status='ONSERVICE') ";//hr_per_employee.ResignationDT<=>NULL
            if (!string.IsNullOrEmpty(dept))
            {
                sqlstr += " AND( hr_per_employee.DeptID=" + dept + " OR hr_per_dept.ParentDeptID=" + dept+")";
            }

            if (!string.IsNullOrEmpty(date1)&&!string.IsNullOrEmpty(date2)) 
            {
                sqlstr += " AND( hr_per_employee.OnboardDT>=" + publicNewClass.mydb.qo(date1) + " AND hr_per_employee.OnboardDT<=" + publicNewClass.mydb.qo(date2) + ")";
            }
            sqlstr += " ORDER BY hr_per_employee.EmpNo";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        public DataTable search_EmpName(string pid)
        {
            string sqlstr = "";

            sqlstr = "select EmpPID, EmpName, DeptID, EmpNo FROM hr_per_employee WHERE EmpPID=" + pid;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //取得單一員工資料
        public DataTable search_EmpDetail(string pid)
        {
            string sqlstr = "";

            sqlstr = "select EmpPID, EmpName, ChiName, EnFName, EnLName, DeptID, EmpNo, EmpCountry, InitSeniority, OnboardDT, Status, PhoneNum, Sex, Birthday, Telephone, PerID, MarriageYN, ChildrenNum, BloodType, Height, Weight, Address, MailAddress, ImgName, ResignationDT, UpdUserID, UpdDT, CompanyID, ContactPerson, Relationship, ContactNum, ContactAddress, UnpaidLeaveDT  FROM hr_per_employee WHERE EmpPID=" + pid;

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //取得員工pid & EmpNo(將最新一筆資料的[ID & EmpNo] +1)
        public DataTable search_PIDNEmpNo()
        {
            string sqlstr = "";

            sqlstr = "SELECT EmpPID, EmpNo FROM hr_per_employee WHERE EmpNo!=998 ORDER BY EmpNo DESC";
            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);

            return myTable;
        }

        //修改員工資料
        public void update_EmpDetail(string empN, string enFN, string enLN, string dept, string country, string inDate, string status, string phone, string sex, string birthday, string tele, string perID, string marriage, string childN, string bloodType, string height, string weight, string address, string MailAddress, string userID, string imgN, string company, string pid, string ContactPerson, string Relationship, string ContactNum, string ContactAddress, string ResignationDT)
        {
            string sqlstr = "";

            sqlstr = "UPDATE hr_per_employee";
            sqlstr += " SET";
            sqlstr += " EmpName=" + publicNewClass.mydb.qo(empN) + ","
                + " ChiName=" + publicNewClass.mydb.qo(empN) + ","
                + " EnFName=" + publicNewClass.mydb.qo(enFN) + ","
                //+ " EnLName=" + publicNewClass.mydb.qo(enLN) + ","
                + " DeptID=" + publicNewClass.mydb.qo(dept) + ","
                + " EmpCountry=" + publicNewClass.mydb.qo(country) + ","
                
                + " OnboardDT=" + publicNewClass.mydb.qo(inDate) + ","
                + " Status=" + publicNewClass.mydb.qo(status) + ","
                + " PhoneNum=" + publicNewClass.mydb.qo(phone) + "," 
                + " Sex=" + publicNewClass.mydb.qo(sex) + ","
                + " Birthday=" + publicNewClass.mydb.qo(birthday) + ","
                
                + " Telephone=" + publicNewClass.mydb.qo(tele) + "," 
               // + " PerID=" + publicNewClass.mydb.qo(perID) + "," 
                + " MarriageYN=" + publicNewClass.mydb.qo(marriage) + ","
                + " ChildrenNum=" + publicNewClass.mydb.qo(childN) + ","
                + " BloodType=" + publicNewClass.mydb.qo(bloodType) + ","
                
                + " Height=" + publicNewClass.mydb.qo(height) + ","
                + " Weight=" + publicNewClass.mydb.qo(weight) + ","
                + " Address=" + publicNewClass.mydb.qo(address) + "," 
                + " MailAddress=" + publicNewClass.mydb.qo(MailAddress) + "," 
                + " ContactPerson=" + publicNewClass.mydb.qo(ContactPerson) + ","
                
                + " Relationship=" + publicNewClass.mydb.qo(Relationship) + ","
                + " ContactAddress=" + publicNewClass.mydb.qo(ContactAddress) + "," 
                + " ContactNum=" + publicNewClass.mydb.qo(ContactNum) + "," 
                + " UpdUserID=" + userID + ","
                + " UpdDT=" + "NOW(),"
                
                + " CompanyID=" + "1";
            if (!string.IsNullOrEmpty(imgN)) { sqlstr += ", ImgName=" + publicNewClass.mydb.qo(imgN); }
            if (!string.IsNullOrEmpty(ResignationDT)) { sqlstr += ", ResignationDT=" + publicNewClass.mydb.qo(ResignationDT); }
            else { sqlstr += ", ResignationDT=NULL"; }
           
            sqlstr += " WHERE EmpPID=" + pid + ";";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        //新增員工資料
        public void insert_EmpDatail(string empN, string enFN, string enLN, string dept, string empNo, string country, string initSen, string inDate, string status, string phone, string sex, string birthday, string tele, string perID, string marriage, string childN, string bloodType, string height, string weight, string address, string MailAddress, string userID, string imgN, string company, string pid, string ContactPerson, string Relationship, string ContactNum, string ContactAddress, string ResignationDT)
        {
            string sqlstr = "";

            sqlstr = "INSERT INTO hr_per_employee(EmpPID, EmpName, ChiName, EnFName, EnLName, DeptID, EmpNo, EmpCountry, InitSeniority, OnboardDT, Status, PhoneNum, Sex, Birthday, Telephone, PerID, MarriageYN, ChildrenNum, BloodType, Height, Weight, Address, MailAddress, ImgName, UpdUserID, UpdDT, CompanyID, ContactPerson, Relationship, ContactNum, ContactAddress, ResignationDT)";
            sqlstr += " VALUES";
            sqlstr += "(";
            sqlstr += pid + ","
                + publicNewClass.mydb.qo(empN) + ","
                + publicNewClass.mydb.qo(empN) + ","
                + publicNewClass.mydb.qo(enFN) + ","
                + publicNewClass.mydb.qo(enLN) + ","
                + publicNewClass.mydb.qo(dept) + ","
                + publicNewClass.mydb.qo(empNo) + ","
                + publicNewClass.mydb.qo(country) + ","
                + publicNewClass.mydb.qo(initSen) + ","
                + publicNewClass.mydb.qo(inDate) + ","
                + publicNewClass.mydb.qo(status) + ","
                + publicNewClass.mydb.qo(phone) + ","
                + publicNewClass.mydb.qo(sex) + ","
                + publicNewClass.mydb.qo(birthday) + ","
                + publicNewClass.mydb.qo(tele) + ","
                + publicNewClass.mydb.qo(perID) + ","
                + publicNewClass.mydb.qo(marriage) + ","
                + publicNewClass.mydb.qo(childN) + ","
                + publicNewClass.mydb.qo(bloodType) + ","
                + publicNewClass.mydb.qo(height) + ","
                + publicNewClass.mydb.qo(weight) + ","
                + publicNewClass.mydb.qo(address) + ","
                + publicNewClass.mydb.qo(MailAddress) + ","
                + publicNewClass.mydb.qo(imgN) + ","
                + userID + ","
                + "NOW(),"
                + company + ","
                + publicNewClass.mydb.qo(ContactPerson) + ","
                + publicNewClass.mydb.qo(Relationship) + ","
                + publicNewClass.mydb.qo(ContactNum) + ","
                + publicNewClass.mydb.qo(ContactAddress) + ",";
            if (!string.IsNullOrEmpty(ResignationDT)) { sqlstr += publicNewClass.mydb.qo(ResignationDT); }
            else { sqlstr += "NULL"; }
            sqlstr += ");";

            DataTable myTable = publicNewClass.mydb.GetDataTable(sqlstr);
        }

        public void update_empResignation(string workerNum, string date, string userID)
        {
            DataTable dt;
            string sqlstr = "";
            sqlstr = "UPDATE hr_per_employee SET ResignationDT=" + publicNewClass.mydb.qo(date) + ", UpdUserID=" + userID + ", UpdDT=NOW()" + " WHERE EmpNo=" + workerNum;
                
            dt = publicNewClass.mydb.GetDataTable(sqlstr);
        }
    }


    public class EmpolyeeDetail
    {
        public int EmpPID { get; set; }
        public string EmpName { get; set; }
        public string ChiName { get; set; }
        public string EnFName { get; set; }
        public string EnLName { get; set; }
        public int DeptID { get; set; }
        public string Dept { get; set; }
        public int EmpNo { get; set; }
        public string EmpCountryID { get; set; }
        public string EmpCountry { get; set; }
        public string Seniority { get; set; }
        public string OnboardDT { get; set; }
        public string Status { get; set; }
        public string PhoneNum { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Telephone { get; set; }
        public string PerID { get; set; }
        public string MarriageYN { get; set; }
        public int ChildrenNum { get; set; }
        public string BloodType { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Address { get; set; }
        public string MailAddress { get; set; }
        public string ImgName { get; set; }
        public string ResignationDT { get; set; }
        public int CompanyID { get; set; }
        public string Company { get; set; }
        public string ContactPerson { get; set; }
        public string Relationship { get; set; }
        public string ContactNum { get; set; }
        public string ContactAddress { get; set; }
    }


}
