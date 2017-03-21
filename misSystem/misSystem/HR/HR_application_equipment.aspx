<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_application_equipment.aspx.cs" Inherits="misSystem.HR.HR_equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Equipment application</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>  
    <link href="myCss/HR_css.css" rel="stylesheet" />  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="out_panel" CssClass="out_panel" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:Panel ID="form_panel" runat="server">
        <h3>設備申請單</h3>
        申請人: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
        <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label><br />
        申請: <asp:TextBox ID="equipment_text" runat="server" MaxLength="50" Width="16em" AutoPostBack="True" OnTextChanged="equipment_text_TextChanged"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="equipment_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        事由: <asp:TextBox ID="content_text" runat="server" MaxLength="20" Width="16em" AutoPostBack="True" OnTextChanged="content_text_TextChanged"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="content_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        
        <asp:Label ID="isFirstUpload_lab" runat="server" Text="true" Visible="False"></asp:Label>
        <table style="margin:0; padding:0; height:auto;"><tr>
            <td style="vertical-align:top;">報價:</td>
            <td style="vertical-align:top; padding:0;"><asp:TextBox ID="price_text" runat="server" MaxLength="100" TextMode="MultiLine" style="width:15em; height:100%;" AutoPostBack="True" OnTextChanged="price_text_TextChanged"></asp:TextBox></td>
            </tr> 
            <tr><td colspan="2"><asp:Label ID="price_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></td>
        </tr></table>            
        </asp:Panel>
        
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Panel ID="file_panel" runat="server" Visible="true">
            上傳(PDF 檔案不得超過 5MB): <asp:FileUpload ID="upload_fl" style="color:red;" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pdf_panel" runat="server" Visible="false"><a href="PDF_equipment/<%= ViewState["check_sn"] %>.pdf" target="_blank">檢視PDF</a></asp:Panel>        
        <p style="padding:0; margin:0;"><asp:Label ID="upload_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>


        <asp:Button ID="send_btn" runat="server" Text="Send" Font-Size="16pt" style="margin-left:10em" OnClick="send_btn_Click" />
        <asp:Button ID="pass_btn" runat="server" Text="Pass" Font-Size="16pt" style="margin-left:10em" OnClick="pass_btn_Click" Visible="False" />
        <asp:Button ID="fail_btn" runat="server" Text="Fail" Font-Size="16pt" style="margin-left:1em" OnClick="fail_btn_Click" Visible="False" />
        <asp:Button ID="edit_btn" runat="server" Text="Edit" Font-Size="16pt" style="margin-left:10em" OnClick="edit_btn_Click" Visible="False" />
        <asp:Button ID="delete_btn" runat="server" Text="Delete" Font-Size="16pt" style="margin-left:1em" OnClick="delete_btn_Click" Visible="False" />
            
    </asp:Panel> 
</asp:Content>
