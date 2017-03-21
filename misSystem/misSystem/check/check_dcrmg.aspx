<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_dcrmg.aspx.cs" Inherits="misSystem.check.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/overview.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div class="title">Documant Change Request</div><br/>
        <asp:Panel ID="basic" runat="server">
            Production Product Model :<asp:Label ID="lbl_model" ForeColor="Blue" runat="server"></asp:Label><br/><br/>
            DCR Assigned No :<asp:Label ID="lbl_ano" ForeColor="Blue" runat="server"></asp:Label><br/><br/>
            R&D Project Name :<asp:Label ID="lbl_pjname" ForeColor="Blue" runat="server"></asp:Label><br/><br/>
            Change Proposed by :<asp:Label ID="lbl_by" ForeColor="Blue" runat="server"></asp:Label>
            Application Date :<asp:Label ID="lbl_adate" ForeColor="Blue" runat="server"></asp:Label><br/><br/>
            
            Priority :<asp:Label ID="lbl_priority" ForeColor="Blue" runat="server"></asp:Label>
            <hr />
            <asp:Table ID="tb_doc" GridLines="both" runat="server">
                <asp:TableHeaderRow HorizontalAlign="Center">
                    <asp:TableHeaderCell>Part/File No.</asp:TableHeaderCell>
                    <asp:TableHeaderCell>New Version</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Old Version</asp:TableHeaderCell>
                    <asp:TableHeaderCell>File Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Modify Reason</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <hr/>
            Affectted Document<br/><br/>
            <ul id="affect" runat="server"></ul><br/>
            <asp:Label ID="lbl_depm" ForeColor="Blue" Visible="false" runat="server"></asp:Label>
            <asp:Label ID="lbl_pdate" ForeColor="Blue" Visible="false" runat="server"></asp:Label><br/>

            <asp:Button ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" runat="server"/>
        </asp:Panel>
        
        <asp:Panel ID="pdne" runat="server"><hr/>
            <h2>Evaluation or Regulatory or safety</h2>
            CE make : <asp:Label ID="lbl_cem" runat="server"></asp:Label><br/>
            510(K) : <asp:Label ID="lbl_510" runat="server"></asp:Label><br/>
            Safety risk : <asp:Label ID="safe" runat="server"></asp:Label><br/>
            Standards : <asp:Label ID="stand" runat="server"></asp:Label><br/>
            PD&E Manager : <asp:Label ID="manager" runat="server"></asp:Label><br/>
            Date : <asp:Label ID="date" runat="server"></asp:Label>
        </asp:Panel>
        
        <asp:Panel ID="pcmc" runat="server"><hr/>
            <h2>Quantity on Hand and Order</h2>
            Part 零件 : On Hand : <asp:Label ID="lbl_ph" ForeColor="Blue" runat="server"></asp:Label>  ,On Order : <asp:Label ID="lbl_po" ForeColor="Blue" runat="server"></asp:Label> <br/>
            Sub Component 半成品 : On Hand : <asp:Label ID="lbl_sh" ForeColor="Blue" runat="server"></asp:Label>  ,On Order : <asp:Label ID="lbl_so" ForeColor="Blue" runat="server"></asp:Label> <br/>
            Finished product 成品 : On Hand : <asp:Label ID="lbl_fh" ForeColor="Blue" runat="server"></asp:Label>  ,On Order : <asp:Label ID="lbl_fo" ForeColor="Blue" runat="server"></asp:Label> <br/>
            <h2>Stock Deal</h2>
            <asp:Label ID="lbl_stock" ForeColor="Blue" runat="server"></asp:Label><br/>
            PCMC Manager : <asp:Label ID="pcmcm" ForeColor="Blue" runat="server"></asp:Label><br/>
            Date : <asp:Label ID="pcmcd" ForeColor="Blue" runat="server"></asp:Label>
        </asp:Panel>


    </div>
</asp:Content>
