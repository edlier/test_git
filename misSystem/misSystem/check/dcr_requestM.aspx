﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="dcr_requestM.aspx.cs" Inherits="misSystem.check.dcr_requsetM" %>
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
            Dep Manager :<asp:Label ID="lbl_depm" ForeColor="Blue" runat="server"></asp:Label>
            Approval Date :<asp:Label ID="lbl_pdate" ForeColor="Blue" runat="server"></asp:Label><br/><br/>
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
            <asp:ListBox ID="lbox_affect" runat="server"></asp:ListBox>
        </asp:Panel>
        <hr/>
        <asp:Panel ID="pdne" runat="server">
            <h2>Evaluation or Regulatory or safety</h2>
            CE make : <asp:Label ID="lbl_cem" runat="server"></asp:Label><br/>
            510(K) : <asp:Label ID="lbl_510" runat="server"></asp:Label><br/>
            Safety risk : <asp:Label ID="safe" runat="server"></asp:Label><br/>
            Standards : <asp:Label ID="stand" runat="server"></asp:Label>
        </asp:Panel>
        <hr/>
        <asp:Panel ID="pcmc" runat="server">
            <h2>Quantity on Hand and Order</h2>
            Part 零件 : On Hand<asp:Label ID="lbl_ph" runat="server"></asp:Label>  ,On Order<asp:Label ID="lbl_po" runat="server"></asp:Label> <br/>
            Sub Component 半成品 : On Hand<asp:Label ID="lbl_sh" runat="server"></asp:Label>  ,On Order<asp:Label ID="lbl_so" runat="server"></asp:Label> <br/>
            Finished product 成品 : On Hand<asp:Label ID="lbl_fh" runat="server"></asp:Label>  ,On Order<asp:Label ID="lbl_fo" runat="server"></asp:Label> <br/>
            <h2>Stock Deal</h2>
            Revise stocks Date 修改庫存日期<asp:Label runat="server"></asp:Label><br/>
            Used Stocks 庫存品報廢<asp:Label runat="server"></asp:Label><br/>
            Deplete of remaining printout 至庫存用完,自然切換, New Issue Applied Date 預定使用本版日期<asp:Label runat="server"></asp:Label>
        </asp:Panel>
    </div>
</asp:Content>
