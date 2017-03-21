<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_application_transfer.aspx.cs" Inherits="misSystem.HR.HR_application_transfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Transfer application</title>
    <script src="myJs/jquery-2.1.4.min.js"></script> 
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>
    <script src="myJs/jquery.timepicker.js"></script>
    <link href="myCss/jquery.timepicker.css" rel="stylesheet" />
    <script src="myJs/HR_js.js"></script>   
    <link href="myCss/HR_css.css" rel="stylesheet"/>
<style>
    #out_tb {
        width:80%;
        margin:auto 0;
        padding:0px;
        white-space:nowrap;
        border: 1pt solid blue;
        vertical-align:central;
    }    
    .out_td {
        width: 50%;
        margin: 3px;
        border: 1pt solid #0a89e5;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <asp:Panel ID="out_panel" CssClass="out_panel" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

        <asp:Panel ID="form_panel" runat="server">

        <h3>請調申請單</h3>
        <table id="out_tb"><tr>
            <td Class="out_td" style="width:35%;">
                申請人: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />
                員工編號: <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />
                到職日: <asp:Label ID="start_work_time_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />
                工作年資: <asp:Label ID="dur_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />
                目前單位: <asp:Label ID="now_dep_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                <asp:Label ID="now_dep_no" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label><br />
                目前服務單位年資: <asp:Label ID="now_dep_dur_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />   
            </td>
            <td  Class="out_td">
                <table style="margin:0; padding:0; height:auto;"><tr>
                    <td style="vertical-align:top;">此公司經歷:</td>
                    <td style="vertical-align:top; padding:0;"><asp:TextBox ID="experience_text" runat="server" MaxLength="100" TextMode="MultiLine" style="width:15em; height:100%;"></asp:TextBox></td>
                    </tr>
                    <tr><td colspan="2"><asp:Label ID="experience_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></td>
                </tr></table>
                請調單位: <asp:DropDownList ID="transfer_dep_dl" runat="server" Font-Size="14pt"></asp:DropDownList><br />
                職缺名稱: <asp:DropDownList ID="position_dl" runat="server" Font-Size="14pt"></asp:DropDownList><br />
                <table style="margin:0; padding:0; height:auto;"><tr>
                    <td style="vertical-align:top;">未來工作內容:</td>
                    <td style="vertical-align:top; padding:0;"><asp:TextBox ID="future_content_text" runat="server" MaxLength="100" TextMode="MultiLine" style="width:15em; height:100%;"></asp:TextBox></td>
                    </tr>
                    <tr><td colspan="2"><asp:Label ID="future_content_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></td>
                </tr></table>
                請調原因: <asp:DropDownList ID="reason_dl" runat="server" Font-Size="14pt"></asp:DropDownList><br />
                
                <asp:Button ID="send_btn" runat="server" Text="Send" Font-Size="16pt" style="margin-left:10em" OnClick="send_btn_Click" />
                </td>
        </tr></table>  

            </asp:Panel>

            <asp:Panel ID="hr_panel" runat="server" style="margin-top:1em;" Visible="false">
                考核期間: <asp:TextBox ID="start_date_text" runat="server" CssClass="datepicker" TextMode="DateTime" AutoPostBack="True" OnTextChanged="start_date_text_TextChanged"></asp:TextBox>
                至<asp:TextBox ID="end_date_text" runat="server" CssClass="datepicker" AutoPostBack="True" OnTextChanged="start_date_text_TextChanged"></asp:TextBox>
                <p style="padding:0; margin:0;"><asp:Label ID="datetime_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
                    
                職務異動起始日: 
                <asp:TextBox ID="go_date_text" runat="server" CssClass="datepicker" TextMode="DateTime" AutoPostBack="True" OnTextChanged="go_date_text_TextChanged"></asp:TextBox>
                <p style="padding:0; margin:0;"><asp:Label ID="go_date_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
            </asp:Panel>
            <asp:Panel ID="hr_panel2" runat="server" Visible="false" style="color:red;">
                考核期間: <asp:Label ID="start_date_lab" runat="server" Text="---"></asp:Label>
                    至 <asp:Label ID="end_date_lab" runat="server" Text="---"></asp:Label><br />
                職務異動起始日: 
                <asp:Label ID="go_date_l" runat="server" Text="---"></asp:Label>
            </asp:Panel>
            <br />

            <asp:Button ID="pass_btn" runat="server" Text="Pass" Font-Size="16pt" style="margin-left:10em" OnClick="pass_btn_Click" Visible="False" />
            <asp:Button ID="fail_btn" runat="server" Text="Fail" Font-Size="16pt" style="margin-left:1em" OnClick="fail_btn_Click" Visible="False" />
            <asp:Button ID="edit_btn" runat="server" Text="Edit" Font-Size="16pt" style="margin-left:10em" OnClick="edit_btn_Click" Visible="False" />
            <asp:Button ID="delete_btn" runat="server" Text="Delete" Font-Size="16pt" style="margin-left:1em" OnClick="delete_btn_Click" Visible="False" />
       
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>   
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", -5);
            $('.datepicker').datepicker("option", "maxDate", +20);

            //配合update panel不會重新執行ready事件
            //為update panel註冊事件，在update完成後執行initi_up();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_pageLoaded(initi_up);
            function initi_up() {
                date_initi();
                $('.datepicker').datepicker("option", "minDate", -5);
                $('.datepicker').datepicker("option", "maxDate", +20);
            }
        });
    </script>
</asp:Content>
