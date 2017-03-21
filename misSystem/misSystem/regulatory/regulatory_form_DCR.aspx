<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="regulatory_form_DCR.aspx.cs" Inherits="misSystem.regulatory.regulatory_form_DCR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>REDGULATORY-DCR FROM</title>
    <link href="../../CssFile/btn.css" rel="stylesheet" />
    <link href="../../CssFile/position.css" rel="stylesheet" />
    <link href="../../CssFile/tittle.css" rel="stylesheet" />
    <link href="../../CssFile/tb.css" rel="stylesheet" />

    <style>
        #dcr_middle {
            position: absolute;
            top: 170px;
            left: 300px;
        }

        #dcrFormTittle {
            font-weight: bold;
            font-size: 28px;
            color: blue;
        }

        #dcrFlowleft {
            position: absolute;
            margin-left: 50%;
            margin-top: 80px;
            left: -400px;
        }

        #dcrFlowRight {
            position: absolute;
            margin-left: 50%;
            margin-top: 80px;
            left: -80px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dcr_middle">
        <div id="dcrFormTittle">文件更改申請單 (DCR Form) </div>
        <br />
        <br />
        <div id="fillTextID">
            Change Proposed by (申請人)：<asp:Label ID="lbl_proposedPerson" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            Change Proposed Date (申請日期)：<asp:Label ID="lbl_proposedDate" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            Production Product (產品)： Model (型號)&nbsp;<asp:DropDownList ID="drop_product" runat="server" DataTextField="description" DataValueField="id"></asp:DropDownList>
            <br />
            <br />
            DCR Assigned No (申請單編號)：<asp:TextBox ID="tb_assignedNo" required runat="server"></asp:TextBox><%--<span id="span_err1" style="color:red" runat="server"><strong> Cannot be empty!</strong></span>--%>
            <br />
            <br />
            R&D Project (專案項目)：Name (名稱)&nbsp;<asp:TextBox ID="tb_projectName" required runat="server"></asp:TextBox><%--<span id="span_err2" style="color:red" runat="server"><strong> Cannot be empty!</strong></span>--%>
            <br />
            <br />
            Priority (優先)：&nbsp;<asp:DropDownList ID="drop_priority" runat="server" DataTextField="description" DataValueField="code"></asp:DropDownList>
            <br />
            <br />

            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="upp" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_err" runat="server" ForeColor="Red" Visible="false">請填寫完整</asp:Label>
                    <asp:Table ID="tbl_doc" GridLines="Both" BorderWidth="2" BorderColor="Black" runat="server">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell>零件/文件編號</asp:TableHeaderCell>
                            <asp:TableHeaderCell>新版次</asp:TableHeaderCell>
                            <asp:TableHeaderCell>舊版次</asp:TableHeaderCell>
                            <asp:TableHeaderCell>文件名稱</asp:TableHeaderCell>
                            <asp:TableHeaderCell>修改原因</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docNum_1" runat="server" required></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_newVersion_1" runat="server" required onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_oldVersion_1" runat="server" required onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docName_1" runat="server" required></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="drop_changeReason_1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_changeReason_1_SelectedIndexChanged" DataValueField="No" DataTextField="NoDescription" OnInit="drop_changeReason_1_Init" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="reason_err" runat="server" Text="error" Visible="false"></asp:Label>
                                <asp:TextBox ID="tb_other_1" runat="server" required Visible="false"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docNum_2" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_newVersion_2" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_oldVersion_2" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docName_2" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="drop_changeReason_2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_changeReason_2_SelectedIndexChanged" DataValueField="No" DataTextField="NoDescription" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="tb_other_2" runat="server" required Visible="false"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docNum_3" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_newVersion_3" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_oldVersion_3" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docName_3" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="drop_changeReason_3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_changeReason_3_SelectedIndexChanged" DataValueField="No" DataTextField="NoDescription" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="tb_other_3" runat="server" required Visible="false"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docNum_4" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_newVersion_4" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_oldVersion_4" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docName_4" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="drop_changeReason_4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_changeReason_4_SelectedIndexChanged" DataValueField="No" DataTextField="NoDescription" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="tb_other_4" runat="server" required Visible="false"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docNum_5" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_newVersion_5" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_oldVersion_5" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" Width="50px"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tb_docName_5" runat="server"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="drop_changeReason_5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_changeReason_5_SelectedIndexChanged" DataValueField="No" DataTextField="NoDescription" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="tb_other_5" runat="server" required Visible="false"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <br />
                    <br />
                    更改涉及文件：
                    <asp:Label ID="lbl_err01" runat="server" ForeColor="Red" Visible="false">請至少選擇一項</asp:Label><br />
                    <asp:CheckBoxList ID="cblist" AutoPostBack="true" OnSelectedIndexChanged="cblist_SelectedIndexChanged" runat="server">
                    </asp:CheckBoxList>
                    <asp:TextBox ID="tb_other_affectDoc_1" Visible="false" runat="server"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:Button ID="btn_DCR_submint" runat="server" Text="送出" OnClick="btn_DCR_submint_Click" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
