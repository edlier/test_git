<%@ Page Title="Employee Detail" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="EmployeeDetail.aspx.cs" Inherits="misSystem.HR.personnel.EmployeeDetail" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/UserControl/sys/webUCDDLCompany.ascx" TagPrefix="uc2" TagName="webUCDDLCompany" %>
<%@ Register Src="~/UserControl/sys/webUCDDLSysCode.ascx" TagPrefix="uc3" TagName="webUCDDLSysCode" %>
<%@ Register Src="~/UserControl/hr/webUCDDLDept.ascx" TagPrefix="uc4" TagName="webUCDDLDept" %>
<%@ Register Src="~/UserControl/utils/webUCDate.ascx" TagPrefix="uc2" TagName="webUCDate" %>







<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../myCss/HR_css.css?v1.011.01" rel="stylesheet"/>
    <link href="../../css/calendar.css" rel="stylesheet" />
    <%--<link href="/css/fontstyle.css" rel="stylesheet" />--%>
    <style>
        h3 {
            color: white;
        }
        /*mutiview*/
        .Initial {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../../picture/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White;
                background: url("../../picture/SelectedButton.png") no-repeat right top;
            }

        .Clicked {
            float: left;
            display: block;
            background: url("../../picture/SelectedButton.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: darkred;
        }
        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
        
    </style>
    <script type="text/javascript">
        function suggest(wgt, next) {
            if (wgt.getInputNode().value.length == 4)
                wgt.$f(next).focus();
        }
    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="tmpdiv" style="top: 161px;">
    <asp:scriptmanager id="scriptmanager1" runat="server"></asp:scriptmanager>
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>--%>
       

        
        <asp:UpdatePanel ID="UpdatePanelData" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divEmployeeInfo">
                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: none">
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="員工資料：" Font-Bold="true"></asp:Label></td>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_Msg" runat="server" ForeColor="Red" />                                
                                <asp:ImageButton ID="imgbtnNew" ToolTip="Add(新增)" runat="server" ImageUrl="~/img/images_add03.png" Width="30" Height="30" Visible="false" OnClick="imgbtnNew_Click"/> &nbsp;&nbsp;&nbsp;</td>
                            <td colspan="4">                                
                                <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit(編輯)" runat="server" ImageUrl="~/img/images_edit.png" Width="30" Height="30" Visible="false" OnClick="imgbtnEdit_Click"></asp:ImageButton>
                                <asp:ImageButton ID="imgbtnSave" ToolTip="Save(儲存單筆)" runat="server" ImageUrl="~/img/images_save01.png" Width="30" Height="30" Visible="false" OnClick="imgbtnSave_Click"></asp:ImageButton> 
                                 <asp:ImageButton ID="imgbtnSaveMore" ToolTip="Add More(再次新增)" runat="server" ImageUrl="~/img/images_save02.jpg" Width="30" Height="30" Visible="false" OnClick="imgbtnSave_Click"></asp:ImageButton>
                                <asp:ImageButton ID="imgbtnUpdate" ToolTip="Update(更新)" runat="server" ImageUrl="~/img/images_update02.jpg" Width="30" Height="30" Visible="true" OnClick="imgbtnUpdate_Click"></asp:ImageButton> &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgbtnExit" ToolTip="Cancal(取消)" runat="server" ImageUrl="~/img/images_exit02.jpg" Width="30" Height="30" Visible="true" OnClick="imgbtnExit_Click"></asp:ImageButton> &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_pdf" runat="server" Text="人事資料卡" OnClick="btn_pdf_Click" Visible="false" />
                                <asp:Button ID="btnReturn" runat="server" Text="上一頁" OnClick="btnReturn_Click" />
                                <%--<asp:ImageButton ID="imgbtnReturn" runat="server" ImageUrl="~/img/btn/images_return01.jpg" Width="30" Height="30" Visible="false" OnClick="imgbtnReturn_Click"></asp:ImageButton>--%>
                            </td>                          
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_employeeName" runat="server" Text="中文姓名:"></asp:Label></td> 
                            <td>
                                <asp:TextBox ID="tb_employeeName" runat="server" CssClass="tb_css"></asp:TextBox></td>              
                            <td style="text-align:right">
                                <asp:Label ID="Label6" runat="server" Text="公司別:"></asp:Label></td>
                            <td>
                                <%--<asp:TextBox ID="tb_company" runat="server" CssClass="tb_css"></asp:TextBox>--%>
                                <uc2:webucddlcompany runat="server" ID="ucCompany" />
                            </td> 
                            <td colspan="2" rowspan="6" style="text-align:start;">                                
                                <asp:Image runat="server" ID="img" Width="150px" Height="200px" />
                                <br />
                                <asp:FileUpload runat="server" ID="filMyFile" Width="170px" />
                                <%--<cc1:AsyncFileUpload runat="server" ID="asyncFileUpload" Width="170px" ThrobberID="imageThrobber" OnClientUploadStarted="uploadStarted" ClientUploadError="uploadError"
                                    ClientIDMode="AutoID" PersistFile="true" PersistedStoreType="Session" Visible="false" />--%>
                                <asp:Button Text="上傳" ID="btn_upload" UseSubmitBehavior="false" runat="server" OnClick="btn_upload_Click" />
                                <%--<asp:Button Text="放棄" ID="btn_del" runat="server" OnClick="btn_del_Click" Visible="false"/>--%>

                                <br />
                                <asp:Label ID="lbl_imgMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:HiddenField ID="hf_imgN" runat="server" />
                            </td>                                                     
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <asp:Label ID="Label1" runat="server" Text="英文姓名:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tb_EnFName" runat="server" CssClass="tb_css"></asp:TextBox>                 
                            </td>
                            <td style="text-align:right">
                                <asp:Label ID="Label8" runat="server" Text="國家別:"></asp:Label></td>
                            <td>
                                <uc3:webucddlsyscode runat="server" ID="ucCountry" />
                            </td>                
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_birthday" runat="server" Text="生日:"></asp:Label></td>
                            <td>
                               <%-- <uc1:webUCDate runat="server" ID="webUCDate" />--%>
                                <%--<asp:TextBox ID="tb_birthday" runat="server" CssClass="tb_css" ></asp:TextBox>--%>
                                <uc2:webUCDate ID="ucDate1" runat="server" />
                            </td>
                             <td style="text-align:right">
                                <asp:Label ID="lbl_employeeNo" runat="server" Text="工號:"></asp:Label></td>
                            <td>                                
                                <asp:TextBox ID="tb_employeeNo" runat="server" ReadOnly="true" CssClass="tb_css" ></asp:TextBox>
                                <asp:HiddenField ID="hf_EmployeePID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                                <asp:Label ID="Label11" runat="server" Text="身分證字號:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tb_ID" runat="server" Style=" text-transform:uppercase" onkeyup="this.value=this.value.replace(/[^A-Za-z0-9]/g,'')" MaxLength="10" CssClass="tb_css"></asp:TextBox></td>       
                            <td style="text-align:right">
                                <asp:Label ID="lbl_dept" runat="server" Text="單位:"></asp:Label></td>
                            <td>
                                <%--<asp:TextBox ID="tb_dept" runat="server" CssClass="tb_css"></asp:TextBox>--%>
                                <uc4:webUCDDLDept runat="server" ID="ucDept" />
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_telephone" runat="server" Text="市話號碼:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tb_telephone1" runat="server" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox> - 
                                <asp:TextBox ID="tb_telephone2" runat="server" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox> - 
                                <asp:TextBox ID="tb_telephone3" runat="server" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_phone" runat="server" Text="手機號碼:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tb_phone1" runat="server" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox> - 
                                <asp:TextBox ID="tb_phone2" runat="server" MaxLength="3" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox> - 
                                <asp:TextBox ID="tb_phone3" runat="server" MaxLength="3" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_AddressC" runat="server" Text="通訊地址:"></asp:Label></td>
                            <td colspan="3">
                                <asp:TextBox ID="tb_AddressC" runat="server" CssClass="tb_css" Width="480px"></asp:TextBox>
                            </td>
                           <%-- <td>
                                <asp:Label ID="lbl_position" runat="server" Text="職稱:" Visible="false"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tb_position" runat="server" CssClass="tb_css" Visible="false"></asp:TextBox>
                            </td>     --%>             
                        </tr>
                         <tr>
                            <td style="text-align:right">
                                <asp:Label ID="lbl_Address" runat="server" Text="戶籍地址:"></asp:Label></td>
                            <td colspan="3">
                                <asp:TextBox ID="tb_Address" runat="server" CssClass="tb_css" Width="480px"></asp:TextBox>
                            </td>
                            <td style="text-align:right">
                                <asp:Label ID="Label15" runat="server" Text="兒女人數:"></asp:Label>&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="tb_Children" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox></td>
                        </tr>
                        <tr>
                           <td style="text-align:right">
                                <asp:Label ID="Label2" runat="server" Text="性別:"></asp:Label></td>
                            <td>
                                <asp:RadioButtonList ID="rb_sex" runat="server" RepeatDirection="Horizontal" Width="120px">
                                    <asp:ListItem Value="M">M</asp:ListItem>
                                    <asp:ListItem Value="F">F</asp:ListItem>
                                </asp:RadioButtonList>
                            </td> 
                            <td style="text-align:right">
                                <asp:Label ID="Label9" runat="server" Text="婚姻狀況:"></asp:Label></td>
                            <td>
                                 <asp:RadioButtonList ID="rb_marriage" runat="server" RepeatDirection="Horizontal" Width="151px">
                                    <asp:ListItem Value="N">未婚</asp:ListItem>
                                    <asp:ListItem Value="Y">已婚</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>                           
                            
                            <td style="text-align:right">
                                <asp:Label ID="Label12" runat="server" Text="血型:"></asp:Label>&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="tb_BloodType" runat="server" CssClass="tb_css" Width="40px"></asp:TextBox></td>
                        </tr>                        
                        <tr>
                             <td style="text-align:right">
                                <asp:Label ID="Label3" runat="server" Text="狀態:"></asp:Label></td>
                            <td>
                                <%--<asp:TextBox ID="tb_status" runat="server" CssClass="tb_css"></asp:TextBox>--%>
                                <uc3:webucddlsyscode runat="server" ID="ucStatus" />
                            </td>  
                             <td style="text-align:right">
                                <asp:Label ID="lbl_seniority" runat="server" Text="初始年資:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="tb_seniority" runat="server" ReadOnly="true" CssClass="tb_css"></asp:TextBox></td> 
                            <td style="text-align:right">
                                <asp:Label ID="Label13" runat="server" Text="身高:"></asp:Label>&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="tb_Height" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox> cm</td>
                        </tr>
                        <tr>                            
                           <td style="text-align:right">
                                <asp:Label ID="Label7" runat="server" Text="到職日:"></asp:Label></td>
                           <td>
                                <%--<asp:TextBox ID="tb_indate" runat="server" CssClass="tb_css" ReadOnly="false" OnTextChanged="tb_indate_TextChanged"></asp:TextBox>--%>
                                <uc2:webUCDate ID="ucDate2" runat="server" />
                            </td>
                            <td style="text-align:right">
                                <asp:Label ID="Label10" runat="server" Text="離職日:"></asp:Label></td>
                            <td>
                                <uc2:webUCDate ID="ucDate3" runat="server" /></td>
                            <td style="text-align:right">
                                <asp:Label ID="Label14" runat="server" Text="體重:"></asp:Label>&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="tb_Weight" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" CssClass="tb_css" Width="40px"></asp:TextBox> kg</td>
                        </tr>                       
                        <tr style="border-top-style:solid;border-top-color:black;height:20px" >
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label16" runat="server" Text="連絡人:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_ContactPer" runat="server" CssClass="tb_css"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label17" runat="server" Text="電話號碼:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_ContactPhone1" runat="server" CssClass="tb_css" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="40px"></asp:TextBox>
                                - <asp:TextBox ID="tb_ContactPhone2" runat="server" CssClass="tb_css" MaxLength="3" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="40px"></asp:TextBox>
                                - <asp:TextBox ID="tb_ContactPhone3" runat="server" CssClass="tb_css" MaxLength="3" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="40px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="Label18" runat="server" Text="關係:"></asp:Label>
                                &nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox ID="tb_Relationship" runat="server" CssClass="tb_css" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label19" runat="server" Text="地址:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="tb_contactAddress" runat="server" CssClass="tb_css" Width="480px"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr style="border-top-style:solid;border-top-color:black;height:20px" >
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_upload" />
                <asp:PostBackTrigger ControlID="imgbtnUpdate" />
                <asp:PostBackTrigger ControlID="imgbtnSaveMore" />
                <asp:PostBackTrigger ControlID="imgbtnSave" />
            </Triggers>
        </asp:UpdatePanel>
             
       
       
        <asp:UpdatePanel ID="UpdatePanelTable" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                <table style="width: 90%;">

                    <tr>
                        <td>
                            <asp:Button Text="學歷" BorderStyle="Solid" BorderColor="Tomato" ID="Tab1" CssClass="Initial" runat="server"
                                OnClick="Tab1_Click" Width="60px" />
                            <asp:Button Text="經歷" BorderStyle="Solid" BorderColor="Tomato" ID="Tab2" CssClass="Initial" runat="server"
                                OnClick="Tab2_Click" Width="60px" />
                            <asp:Button Text="新人作業" BorderStyle="None" ID="Tab9" CssClass="Initial" runat="server"
                                OnClick="Tab9_Click"  Width="80px" />
                            <asp:Button Text="獎懲紀錄" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                                OnClick="Tab3_Click" Width="80px" />
                            <asp:Button Text="異動紀錄" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
                                OnClick="Tab4_Click" Width="80px" />
                            <asp:Button Text="教育訓練" BorderStyle="None" ID="Tab5" CssClass="Initial" runat="server"
                                OnClick="Tab5_Click" Width="80px" />
                            <asp:Button Text="考核紀錄" BorderStyle="None" ID="Tab6" CssClass="Initial" runat="server"
                                OnClick="Tab6_Click" Width="80px" />
                            <asp:Button Text="請假紀錄" BorderStyle="None" ID="Tab7" CssClass="Initial" runat="server"
                                OnClick="Tab7_Click" Width="80px" />
                            <asp:Button Text="加班紀錄" BorderStyle="None" ID="Tab8" CssClass="Initial" runat="server"
                                OnClick="Tab8_Click" Width="80px" />
          
                            <%--===================================================================================--%>
                            <asp:MultiView ID="MainView" runat="server">
                                <%--================================ View1 ========================================--%>
                                <asp:View ID="View1" runat="server" >
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>  
           
                                                              
            <asp:UpdatePanel ID="UpdatePanelGridEDU" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="ImgBtn_eduNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" OnClick="ImgBtn_eduNew_Click"/>
                    <asp:Label ID="lbl_eduMsg" runat="server" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>

                                                          
            
             
                    <%--DataKeyNames="ID"
                        GridLines="None" BorderLine="None" 
                        AutoGenerateColumns="False" 
                        AllowPaging="True" 
                        OnPageIndexChanging="gvCodeType_PageIndexChanging"
                        OnPageIndexChanged="gvCodeType_PageIndexChanged"Width="
                        OnRowDataBound="gvCodeType_RowDataBound"
                        OnRowCreated="gvCodeType_RowCreated"
                        OnSelectedIndexChanged="gvCodeType_SelectedIndexChanged" 
                        OnRowCommand ="gvCodeType_RowCommand"
                        ShowFooter="False" 
                        ShowHeaderWhenEmpty="True"--%>
                    <asp:GridView ID="grid_edu" runat="server" Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" OnSorting="grid_edu_Sorting" OnPageIndexChanging="grid_edu_PageIndexChanging" OnRowCreated="grid_edu_RowCreated" ShowHeaderWhenEmpty="True">
                        <Columns>                            
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpPID" HeaderText="EmpPID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="SchoolID" HeaderText="SchoolID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="School" SortExpression="School" HeaderText="School">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="MajorID" HeaderText="MajorID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Major" SortExpression="Major" HeaderText="Major">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DegreeID" HeaderText="DegreeID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Degree" SortExpression="Degree" HeaderText="Degree">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StartDT" SortExpression="StartDT" HeaderText="StartDT" DataFormatString="{0:yyyy/MM}">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndDT" SortExpression="EndDT" HeaderText="EndDT" DataFormatString="{0:yyyy/MM}">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusID" HeaderText="StatusID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Comments" HeaderText="Comments">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdUserID" HeaderText="UpdUserID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdDT" HeaderText="UpdDT(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CompanyID" HeaderText="CompanyID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtn_eduEdit" ImageUrl="~/img/images_edit.png" runat="server"
                                        Width="30" Height="30" OnClick="ImgBtn_eduEdit_Click"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
				        <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
				        <PagerStyle CssClass="gridview" />
				        <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
				        <RowStyle Height="35" />
				        <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
                    </asp:GridView>
 

    <!--
        ModalPopupExtender
    --> 
       <asp:Button ID="btn_eduShowPopup" runat="server" style="display:none" AutoPostBack="false" UseSubmitBehavior="false" />

        <cc1:ModalPopupExtender ID="mpe_edu" runat="server"
            TargetControlID="btn_eduShowPopup"
            PopupControlID="pnlp_edu"            
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="pnlp_edu" runat="server" BackColor="White" Height="400px" Width="500px" style="display:grid">

            <asp:Panel ID="pnldrag" runat="server" style="cursor:pointer;background-color:#000080" align="center">
            <div>
                <h3>學歷</h3> 
            </div>
            </asp:Panel>

        <table style="border:Solid 3px #000080; width:100%; height:100%;" cellpadding="0" cellspacing="0">

        <%--<tr>
        <td style="text-align:right; width:40%">
            公司別:
        </td>
        <td>            
            <asp:TextBox ID="tb_CompanyCode" runat="server" CssClass="tb_css_filled_w2x" ReadOnly="true"/>
            <asp:HiddenField ID="hf_ComapnyID" runat="server" />
        </td>
        </tr>--%>
        <tr>
        <td style="text-align:right; width:180px">
            學校:
        </td>
        <td> 
            <%--<uc3:webucddlsyscode runat="server" ID="ucEduSchool" />--%>
            <asp:TextBox ID="tb_school" runat="server" CssClass="tb_css_filled_w2x"/>
            <asp:HiddenField ID="hf_CompanyID" runat="server" />
            <asp:HiddenField ID="hf_eduID" runat="server" />
        </td>
        </tr>
        <tr>
        <td style="text-align:right; width:180px">
            科系:
        </td>
        <td>
            <%--<uc3:webucddlsyscode runat="server" ID="ucEduMajor" />--%>
            <asp:TextBox ID="tb_major" runat="server" CssClass="tb_css_filled_w3x"/>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
            學位:
        </td>
        <td>
            <uc3:webucddlsyscode runat="server" ID="ucEduDegree" />
            <%--<asp:TextBox ID="tb_degree" runat="server" CssClass="tb_css_7x"/>--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
            起始時間:
        </td>
        <td>
            <uc2:webUCDate ID="ucEduStartDT" runat="server" />
            <%--<asp:TextBox ID="tb_startDT" runat="server" CssClass="tb_css_7x"/>--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
            結束時間:
        </td>
        <td>
            <uc2:webUCDate ID="ucEduEndDT" runat="server" />
            <%--<asp:TextBox ID="tb_endDT" runat="server" CssClass="tb_css_7x"/>--%>
            <%--<uc2:webUCDDLSysMenu ID="ucSysMenu2" runat="server" />--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
            狀態:
        </td>
        <td>
            <uc3:webucddlsyscode runat="server" ID="ucEduStatus" />
            <%--<asp:TextBox ID="tb_eduStatus" runat="server" CssClass="tb_css_1x"/>--%>
            <%--<asp:CheckBox CssClass="txt" ID="chkActiveYN" runat="server" Checked="True" Text="Active" ToolTip="Y/N" />--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
            備註:
        </td>
        <td>
            <asp:TextBox ID="tb_eduComments" runat="server" TextMode="MultiLine" CssClass="tb_css_1x" Height="50px" Width="166px"/>
            <%--<asp:CheckBox CssClass="txt" ID="chkActiveYN" runat="server" Checked="True" Text="Active" ToolTip="Y/N" />--%>
            
        </td>
        </tr>
        <tr style="background-color:white">
            <td style="text-align: right;">
                <asp:Label ID="lbl_eduResult" runat="server" Text="" ForeColor="Red" />
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImgBtn_eduEdit2" runat="server"
                    ImageUrl="~/img/images_edit.jpg"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_eduEdit2_Click"></asp:ImageButton>
                <asp:ImageButton ID="ImgBtn_eduSave" runat="server"
                    ImageUrl="~/img/images_save01.png"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_eduSave_Click"></asp:ImageButton>
                <asp:ImageButton ID="ImgBtn_eduUpdate" runat="server"
                    ImageUrl="~/img/images_update02.jpg"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_eduUpdate_Click"></asp:ImageButton>
                <asp:ImageButton ID="ImgBtn_eduSaveMore" runat="server"
                    ImageUrl="~/img/images_save02.jpg"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_eduSave_Click"></asp:ImageButton>&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ImgBtn_eduCancel" Width="30" Height="30"
                    runat="server" CausesValidation="false"
                    ImageUrl="~/img/images_exit02.jpg"
                    OnClick="ImgBtn_eduCancel_Click"></asp:ImageButton>
            </td>
        </tr>
        
        </table>              
        
        </asp:Panel>

       <!-- End of ModalPopupExtender -->   

                </td>
            </tr>
        </table>
 
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grid_edu" />
                    <asp:AsyncPostBackTrigger ControlID="ImgBtn_eduUpdate" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ImgBtn_eduSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
       
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View2 ========================================--%>
                                <asp:View ID="View2" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>                                                
            <asp:UpdatePanel ID="UpdatePanelGridEXP" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <table>
	        <tr>
		        <td>
			        <asp:ImageButton ID="imgBtn_expNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" OnClick="imgBtn_expNew_Click"/>
			        <asp:Label ID="lbl_expMsg" runat="server" ForeColor="Red" />
		        </td>
	        </tr>
	        <tr>
		        <td>	 
			        <%--DataKeyNames="ID"
				        GridLines="None" BorderLine="None" 
				        AutoGenerateColumns="False" 
				        AllowPaging="True" 
				        OnPageIndexChanging="gvCodeType_PageIndexChanging"
				        OnPageIndexChanged="gvCodeType_PageIndexChanged"Width="
				        OnRowDataBound="gvCodeType_RowDataBound"
				        OnRowCreated="gvCodeType_RowCreated"
				        OnSelectedIndexChanged="gvCodeType_SelectedIndexChanged" 
				        OnRowCommand ="gvCodeType_RowCommand"
				        ShowFooter="False" 
				        ShowHeaderWhenEmpty="True"--%>
			        <asp:GridView ID="grid_exp" runat="server" Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" OnSorting="grid_exp_Sorting" OnPageIndexChanging="grid_exp_PageIndexChanging" OnRowCreated="grid_exp_RowCreated">
				        <Columns>                            
					        <asp:TemplateField HeaderText="No.">
						        <ItemTemplate>
							        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
					        </asp:TemplateField>
					        <asp:BoundField DataField="ID" HeaderText="ID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="EmpPID" HeaderText="EmpPID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderText="CompanyName">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="Department" SortExpression="Department" HeaderText="Department">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="Position" SortExpression="Position" HeaderText="Position">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                            <asp:BoundField DataField="Achievement" HeaderText="Achievement">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>					       
					        <asp:BoundField DataField="StartDT" SortExpression="StartDT" HeaderText="StartDT" DataFormatString="{0:yyyy/MM}">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="EndDT" SortExpression="EndDT" HeaderText="EndDT" DataFormatString="{0:yyyy/MM}">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                             <asp:BoundField DataField="Seniority" SortExpression="Seniority" HeaderText="Seniority">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="LeavedRsnID" HeaderText="LeavedRsnID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                            <asp:BoundField DataField="LeavedRsn" HeaderText="LeavedRsn">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="LeavedOtherRsn" HeaderText="LeavedOtherRsn">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="LeavedNote" HeaderText="LeavedNote">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="UpdUserID" HeaderText="UpdUserID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="UpdDT" HeaderText="UpdDT(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					         <asp:BoundField DataField="CompanyID" HeaderText="CompanyID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:TemplateField HeaderText="">
						        <ItemTemplate>
							        <asp:ImageButton ID="imgBtn_expEdit" ImageUrl="~/img/images_edit.png" runat="server"
								        Width="30" Height="30" OnClick="imgBtn_expEdit_Click"/>
						        </ItemTemplate>
						        <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
					        </asp:TemplateField>
				        </Columns>
				        <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
				        <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
				        <PagerStyle CssClass="gridview" />
				        <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
				        <RowStyle Height="35" />
				        <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
			        </asp:GridView>


        <!--
            ModalPopupExtender
        --> 
        <asp:Button ID="btn_expShowPopup" runat="server" style="display:none" AutoPostBack="false" UseSubmitBehavior="false" />

        <cc1:ModalPopupExtender ID="mpe_exp" runat="server"
	        TargetControlID="btn_expShowPopup"
	        PopupControlID="pnlp_exp"            
	        BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="pnlp_exp" runat="server" BackColor="White" Height="500px" Width="500px" style="display:grid">

	        <asp:Panel ID="Panel1" runat="server" style="cursor:pointer;background-color:#000080" align="center">
	        <div>
		        <h3>經歷</h3> 
	        </div>
	        </asp:Panel>

        <table style="border:Solid 3px #000080; width:100%; height:100%;" cellpadding="0" cellspacing="0">

       <%-- <tr>
        <td style="text-align:right; width:40%">
	        公司別:
        </td>
        <td>            
	        <asp:TextBox ID="tb_expCompany" runat="server" CssClass="tb_css_filled_w2x" ReadOnly="true"/>
        </td>
        </tr>--%>
        <tr>
        <td style="text-align:right; width:180px">
	        公司名稱:
        </td>
        <td>            
	        <asp:TextBox ID="tb_expCompanyN" runat="server" CssClass="tb_css_filled_w2x"/>
	        <asp:HiddenField ID="hf_expID" runat="server" />
	        <asp:HiddenField ID="hf_expCompanyID" runat="server" />
        </td>
        </tr>
        <tr>
        <td style="text-align:right; width:180px">
	        部門:
        </td>
        <td>
	        <asp:TextBox ID="tb_expDept" runat="server" CssClass="tb_css_filled_w3x"/>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
	        職稱:
        </td>
        <td>
	        <asp:TextBox ID="tb_expPosition" runat="server" CssClass="tb_css_7x"/>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
	        特殊事蹟:
        </td>
        <td>
	        <asp:TextBox ID="tb_expAchievement" runat="server" TextMode="MultiLine" CssClass="tb_css_1x" Height="50px" Width="166px"/>
	        <%--<asp:CheckBox CssClass="txt" ID="chkActiveYN" runat="server" Checked="True" Text="Active" ToolTip="Y/N" />--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
	        起始時間:
        </td>
        <td>
            <uc2:webUCDate ID="ucExpStartDT" runat="server" />
	        <%--<asp:TextBox ID="tb_expStartDT" runat="server" CssClass="tb_css_7x"/>--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
	        結束時間:
        </td>
        <td>
            <uc2:webUCDate ID="ucExpEndDT" runat="server" />
	        <%--<asp:TextBox ID="tb_expEndDT" runat="server" CssClass="tb_css_7x"/>--%>
	        <%--<uc2:webUCDDLSysMenu ID="ucSysMenu2" runat="server" />--%>
        </td>
        </tr>
        <tr>
        <td style="text-align:right;">
	        年資:
        </td>
        <td>
	        <asp:TextBox ID="tb_expSeniority" runat="server" CssClass="tb_css_1x"/>
	        <%--<asp:CheckBox CssClass="txt" ID="chkActiveYN" runat="server" Checked="True" Text="Active" ToolTip="Y/N" />--%>
        </td>
        </tr>
            <tr>
        <td style="text-align:right;">
	        離職原因:
        </td>
        <td>
            <uc3:webucddlsyscode runat="server" ID="ucExpLeavedRsn" />
             <asp:TextBox ID="tb_LeavedOtherRsn" runat="server" CssClass="tb_css_1x" Visible="false"/>
	        <%--<asp:TextBox ID="tb_LeavedRsn" runat="server" CssClass="tb_css_1x"/>--%>
	        <%--<asp:HiddenField ID="hf_expLeavedRsnID" runat="server" />--%>
	        <%--<asp:CheckBox CssClass="txt" ID="chkActiveYN" runat="server" Checked="True" Text="Active" ToolTip="Y/N" />--%>
        </td>
        </tr>        
        <tr>
        <td style="text-align:right;">
	        離職備註:
        </td>
        <td>
	        <asp:TextBox ID="tb_LeavedNote" runat="server" TextMode="MultiLine" CssClass="tb_css_1x" Height="50px" Width="166px"/>
	        <%--<asp:CheckBox CssClass="txt" ID="chkActiveYN" runat="server" Checked="True" Text="Active" ToolTip="Y/N" />--%>
        </td>
        </tr>
        <tr style="background-color: white">
            <td style="text-align: right;">
                <asp:Label ID="lbl_expResult" runat="server" Text="" ForeColor="Red" />
            </td>
            <td style="text-align: center">
                <asp:ImageButton ID="ImgBtn_expEdit2" runat="server"
                    ImageUrl="~/img/images_edit.jpg"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_expEdit2_Click"></asp:ImageButton>
                <asp:ImageButton ID="ImgBtn_expSave"
                    runat="server"
                    ImageUrl="~/img/images_save01.png"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_expSave_Click"></asp:ImageButton>
                <asp:ImageButton ID="ImgBtn_expUpdate"
                    runat="server"
                    ImageUrl="~/img/images_update02.jpg"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_expUpdate_Click"></asp:ImageButton>
                <asp:ImageButton ID="ImgBtn_expSaveMore" runat="server"
                    ImageUrl="~/img/images_save02.jpg"
                    Width="30" Height="30" Visible="true"
                    OnClick="ImgBtn_expSave_Click"></asp:ImageButton>&nbsp;&nbsp;&nbsp;		                          
			    <asp:ImageButton ID="ImgBtn_expCancel" Width="30" Height="30"
                    runat="server" CausesValidation="false"
                    ImageUrl="~/img/images_exit02.jpg"
                    OnClick="ImgBtn_expCancel_Click"></asp:ImageButton>
            </td>
        </tr>

        </table>              

        </asp:Panel>

        <!-- End of ModalPopupExtender -->   

		        </td>
	        </tr>
        </table>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grid_exp" />
                    <asp:AsyncPostBackTrigger ControlID="ImgBtn_expUpdate" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ImgBtn_expSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View3 ========================================--%>
                                <asp:View ID="View3" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <br />
                                                <br />
                                                <h1>View 3
                                                </h1>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View4 ========================================--%>
                                <asp:View ID="View4" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
            <asp:UpdatePanel ID="UpdatePanelGridCHA" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <table>
	        <tr>
		        <td>
			        <asp:ImageButton ID="btn_chaNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" Visible="false" />
			        <asp:Label ID="lbl_chaMsg" runat="server" ForeColor="Red" />
		        </td>
	        </tr>
	        <tr>
		        <td>

			        <asp:GridView ID="grid_cha" runat="server" Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="False" OnRowCreated="grid_cha_RowCreated">
				        <Columns>                            
					        <asp:TemplateField HeaderText="No.">
						        <ItemTemplate>
							        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
						        </ItemTemplate>
						        <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
					        </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="EmpPID" HeaderText="EmpPID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                            <asp:BoundField DataField="BoardID" HeaderText="BoardID">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="DeptID" HeaderText="DeptID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                             <asp:BoundField DataField="Dept" HeaderText="Dept">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                            <asp:BoundField DataField="Position" HeaderText="Position">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					        <asp:BoundField DataField="changeDT" HeaderText="changeDT" DataFormatString="{0:yyyy/MM/dd}">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                            <asp:BoundField DataField="StatusID" HeaderText="StatusID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                             <asp:BoundField DataField="Status" HeaderText="Status">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
                            <asp:BoundField DataField="UpdUserID" HeaderText="UpdUserID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdDT" HeaderText="UpdDT(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
					         <asp:BoundField DataField="CompanyID" HeaderText="CompanyID(Hide)">
						        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
					        </asp:BoundField>
						</Columns>
				        <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
				        <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
				        <PagerStyle CssClass="gridview" />
				        <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
				        <RowStyle Height="35" />
				        <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
			        </asp:GridView>
					
				</td>
	        </tr>
        </table>

                </ContentTemplate>
            </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View5 ========================================--%>
                                <asp:View ID="View5" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <br />
                                                <br />
                                                <h1>View 5
                                                </h1>
                                                <br />
                                                <br />

            <asp:UpdatePanel ID="UpdatePanelGridTRN" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <table>
	        <tr>
		        <td>
			        <asp:ImageButton ID="btn_trnNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" Visible="false" />
			        <asp:Label ID="Label4" runat="server" ForeColor="Red" />
		        </td>
	        </tr>
	        <tr>
		        <td>


                    <asp:GridView ID="grid_trn" runat="server" Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" >
	                    <Columns>                            
		                    <asp:TemplateField HeaderText="No.">
			                    <ItemTemplate>
				                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
			                    </ItemTemplate>
			                    <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:TemplateField>
		                    <asp:BoundField DataField="ID" HeaderText="ID(Hide)">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="EmpPID" HeaderText="EmpPID(Hide)">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderText="CompanyName">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="Department" SortExpression="Department" HeaderText="Department">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="Position" SortExpression="Position" HeaderText="Position">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>					       
		                    <asp:BoundField DataField="StartDT" SortExpression="StartDT" HeaderText="StartDT" DataFormatString="{0:yyyy/MM/dd}">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="EndDT" SortExpression="EndDT" HeaderText="EndDT" DataFormatString="{0:yyyy/MM/dd}">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                     <asp:BoundField DataField="Seniority" SortExpression="Seniority" HeaderText="Seniority">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="LeavedRsnID" HeaderText="LeavedRsnID(Hide)">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="LeavedRsn" HeaderText="LeavedRsn">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="LeavedOtherRsn" HeaderText="LeavedOtherRsn">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="LeavedNote" HeaderText="LeavedNote">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="Comments" HeaderText="Comments">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="UpdUserID" HeaderText="UpdUserID(Hide)">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:BoundField DataField="UpdDT" HeaderText="UpdDT(Hide)">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                     <asp:BoundField DataField="CompanyID" HeaderText="CompanyID(Hide)">
			                    <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:BoundField>
		                    <asp:TemplateField HeaderText="">
			                    <ItemTemplate>
				                    <asp:ImageButton ID="imgBtn_trnEdit" ImageUrl="~/img/images_edit.png" runat="server"
					                    Width="30" Height="30" />
			                    </ItemTemplate>
			                    <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
		                    </asp:TemplateField>
	                    </Columns>
	                    <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
	                    <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
	                    <PagerStyle CssClass="gridview" />
	                    <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
	                    <RowStyle Height="35" />
	                    <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
                    </asp:GridView>


                </td>
	        </tr>
        </table>

                </ContentTemplate>
            </asp:UpdatePanel>

                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View6 ========================================--%>
                                <asp:View ID="View6" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <br />
                                                <br />
                                                <h1>View 6
                                                </h1>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View7 ========================================--%>
                                <asp:View ID="View7" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>

                    <asp:ImageButton ID="imgbtn_leaveNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" OnClick="imgbtn_leaveNew_Click" />
                    <%--<asp:Label ID="Label4" runat="server" ForeColor="Red" />--%>
                </td>
            </tr>
            <tr>
                <td>




            <asp:GridView ID="leave_grid" runat="server"  Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True">
                <AlternatingRowStyle BackColor="White" />

                <Columns>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                            <asp:HiddenField ID="id_hf" runat="server" Value='<%# Eval("SN") %>' />
                            <%--   <asp:Label ID="id_lab" runat="server" Text='<%# Eval("id") %>'></asp:Label>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Start">
                        <ItemTemplate>
                            <asp:Label ID="start" runat="server" Text='<%# Eval("start_time", @"{0:yyyy/MM/dd HH:mm}") %>' Visible="false"></asp:Label>
                            <asp:Label ID="start_lab" runat="server" Text='<%# Eval("start_time", @"{0:M/d HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="End">
                        <ItemTemplate>
                            <asp:Label ID="end" runat="server" Text='<%# Eval("end_time", @"{0:yyyy/MM/dd HH:mm}") %>' Visible="false"></asp:Label>
                            <asp:Label ID="end_lab" runat="server" Text='<%# Eval("end_time", @"{0:M/d HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Memo">
                        <ItemTemplate>
                            <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("memo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Hour">
                        <ItemTemplate>
                            <asp:Label ID="hour_lab" runat="server" Text='<%# Eval("time") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="status_lab" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Type" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="type_lab" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="70px" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
                <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
                <PagerStyle CssClass="gridview" />
                <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
                <RowStyle Height="35" />
                <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
            </asp:GridView>

                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <%--================================ View8 ========================================--%>
                                <asp:View ID="View8" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <br />
                                                <br />
                                                <h1>View 8
                                                </h1>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            <%--================================ View9 ========================================--%>
                                <asp:View ID="View9" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                               
                    <asp:ImageButton ID="imgbtn_fileNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" OnClick="imgbtn_fileNew_Click" Visible="false" />
                    <asp:Button ID="btn_fileView" Text="詳細內容" runat="server" Visible="false" OnClick="btn_fileView_Click" />
                    <%--<asp:Label ID="Label4" runat="server" ForeColor="Red" />--%>
                </td>
            </tr>
            <tr>
                <td>

            <asp:GridView ID="file_grid" runat="server"  Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="False" OnRowDataBound="file_grid_RowDataBound">
                <AlternatingRowStyle BackColor="White" />

                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CodeName" HeaderText="項目說明">
                        <ItemStyle HorizontalAlign="Left" BorderStyle="None" />
                    </asp:BoundField>
                     <asp:BoundField DataField="FileYN" HeaderText="" Visible="false">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="checkYN" HeaderText="繳交狀態">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>

                    <%--<asp:BoundField DataField="Notice" HeaderText="通知單">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LeaveProve" HeaderText="離職證明">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="JobProve" HeaderText="工作證明">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Hospital" HeaderText="體檢表">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="perProve" HeaderText="身分證影本">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EduProve" HeaderText="學歷證明">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pic" HeaderText="照片">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Passbook" HeaderText="存摺">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="seal" HeaderText="印章">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="account" HeaderText="資產">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtn_fileEdit" ImageUrl="~/img/images_edit.png" runat="server"
                                Width="30" Height="30" OnClick="imgBtn_fileEdit_Click"/>
                        </ItemTemplate>
                        <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                    </asp:TemplateField>--%>
                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
                <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
                <PagerStyle CssClass="gridview" />
                <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
                <RowStyle Height="35" />
                <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
            </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            <%--==================================================================================--%>
                            </asp:MultiView>
                        </td>
                    </tr>

                </table>
                <asp:Button ID="btn_def" runat="server" Text=""  CssClass="Georgia18px" OnClick="btn_def_Click" />
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        


    </div>
</asp:Content>
