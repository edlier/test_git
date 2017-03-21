<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="cnnSAP_List_inProcess.aspx.cs" Inherits="misSystem.QC.cnnSAP_List_inProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IQC - Validate Raw</title>
    <script src="../scripts/jquery-1.11.2.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <%--<script src="../scripts/prototype-1.7.3.js"></script>--%>
    <%--<script src="../scripts/jquery-1.6.4.min.js"></script>--%>
    <style>
        .V2W50_Georgia18px {
            font-size: 18px;
            font-family: Georgia;
            width: 50px;
        }
    </style>
    <script>
        $(document).ready(function () {
            <%--$('#<%=tb_TotalQty.ClientID%>').on('focusout', function () {
                var regex = /^[0-9]+$/;
                if (!regex.test($('#<%=tb_TotalQty.ClientID%>').val()))
                {
                    $('#tb_totalError').text(" Total  count cannot be String!");
                    $("#<%=tb_TotalQty.ClientID%>").val('');
                }

                else {
                    $('#tb_totalError').text("");
                }
            });--%>

            $('#<%=tb_TotalQty.ClientID%>').on('focusout', function () {
                if ($('#<%=tb_TotalQty.ClientID%>').val() < 0) { $("#<%=tb_TotalQty.ClientID%>").val('0'); }
            });

            $('#<%=tb_ProMin.ClientID%>').on('focusout', function () {
                if ($('#<%=tb_ProMin.ClientID%>').val() < 0) { $("#<%=tb_ProMin.ClientID%>").val('0'); }
                else if ($('#<%=tb_ProMin.ClientID%>').val() > 60) { $("#<%=tb_ProMin.ClientID%>").val('59'); }
            });
            $('#<%=tb_ProHr.ClientID%>').on('focusout', function () {
                if ($('#<%=tb_ProHr.ClientID%>').val() < 0) { $("#<%=tb_ProHr.ClientID%>").val('0'); }
            });
        });
        $(function () {
            $('#<%=btn_submit.ClientID%>').click(function () {

                var sum = 0;
                //iterate through each textboxes and add the values
                $(".V2W50_Georgia18px").each(function () {

                    //add only if the value is number
                    if (!isNaN(this.value) && this.value.length != 0) {
                        sum += parseFloat(this.value);
                    }
                });

                if (sum != $('#<%=tb_FailedQty.ClientID%>').val()) {
                    alert(' Total Failed Reason Count isnt match with Choosed !');
                    return false;
                }
                else {
                    var $arr = $.map($(".Georgia18pxV2"), function ($ele) {
                        return $($ele).val();
                    }),
                        $unique = $.unique($arr.slice(0));

                    if ($unique.length != $arr.length) {
                        alert('Duplicate Faild Reason !');
                        return false;
                    } else {
                        //alert('送出');
                    }
                }
            });
        });
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Lp550_Tm5_gradient40px">IQC - Validate Raw Material</div>
    
    <div class="Lp550-Tm325-georgia18px">
        
        <asp:Label ID="Label2" runat="server" Text="Doc Num:"></asp:Label>
        <asp:Label ID="lb_docnum" runat="server"></asp:Label>
        <br /><br />
        <asp:Label ID="Label1" runat="server" Text="Item Code:"></asp:Label>
        <asp:Label ID="lb_ItemCode" runat="server"></asp:Label>
        <br /><br />
        <asp:Label ID="Label3" runat="server" Text="Item Description:"></asp:Label>
        <asp:Label ID="lbl_ltemdsrp" runat="server"></asp:Label>
        <br /><br />
        <asp:Label ID="Label4" runat="server" Text="Supplier Code:"></asp:Label>
        <asp:Label ID="lbl_supplierCode" runat="server"></asp:Label>
        <br /><br />
        <asp:Label ID="Label5" runat="server" Text="Supplier Name:"></asp:Label>
        <asp:Label ID="lbl_supplierName" runat="server"></asp:Label>
        <br /><br />
        <asp:Label ID="Label6" runat="server" Text="Doc Date:"></asp:Label>
        <asp:Label ID="lbl_DocDate" runat="server" DataFormatString="{0:yyyy-MM-dd}"></asp:Label>
        <br /><br />
        <asp:Label ID="Label7" runat="server" Text=" Quantity:"></asp:Label>
        <asp:Label ID="lbl_Qty" runat="server" DataFormatString="{0:0.##}"></asp:Label>
        <br /><br />
        <asp:Button ID="btn_StartValidation" runat="server" Text="Start Validation" 
            OnClick="btn_StartValidation_Click" Visible="false" CssClass="W180H30-georgia20px"
            OnClientClick="if (!confirm('Do you want to Start Validation?')) return false;"/>
        


        <%--↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓--%>
        <%--↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓--%>
        <%--↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓--%>



        <asp:Panel ID="Panl_FilVaInfo" runat="server" Visible="false">
            <br /><br />
           <hr style="border:0; height:3px; background-color:#b5ca60"/> 
            <br /><br />
            <asp:Label ID="Label8" runat="server" Text="Total Qty : "></asp:Label>
            <asp:TextBox ID="tb_TotalQty" runat="server" CssClass="W50_Georgia18px" TextMode="Number"></asp:TextBox>
            <%--<label id="tb_totalError" class="RedBold_Georgia18px"></label>--%>
            <br /><br />

            <%-----------------------------------------------------------------%>
            <%-------------------UPDATE PANEL 1---------------------------%>
            <%-----------------------------------------------------------------%>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style = "display:none"><asp:Label ID="test_lbl" runat="server" Text="0"></asp:Label></div>

                    <asp:Label ID="Label9" runat="server" Text="Failed Qty : "></asp:Label>
                    <asp:TextBox ID="tb_FailedQty" runat="server" CssClass="W50_Georgia18px" OnTextChanged="tb_FailedQty_TextChanged" AutoPostBack="true" TextMode="Number"></asp:TextBox>
                    <br /><br />

                    <asp:Label ID="lbl_failedReason" runat="server" Text="Failed Reason :"></asp:Label>
                    <asp:Button ID="btn_AddFailedItem" runat="server" Text="ADD" CssClass="W140H28-georgia18px" OnClick="btn_AddFailedItem_Click" Enabled="false" />
                    <asp:Button ID="btn_RemoveFItem" runat="server" Text="Remove" CssClass="W140H28-georgia18px" OnClick="btn_RemoveFItem_Click" Enabled="false" />
                    <br /><br />
                    <%---------------------------------------------------%>
                    <%-------------------DROP   1--------------------%>
                    <%---------------------------------------------------%>
                    <asp:DropDownList ID="drop_FailedType1" runat="server" DataValueField="id" DataTextField="idDes" CssClass="Georgia18px" OnTextChanged="Drop_FailedType_TextChanged" AutoPostBack="true" Visible="false" AppendDataBoundItems="True" >
                        <asp:ListItem Text='<%$ AppSettings:emptyItem %>' Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drop_FailedR1" runat="server" DataValueField="id" DataTextField="idDes" CssClass="Georgia18pxV2" Visible="false"   name="sid" >
                    </asp:DropDownList>
                    <asp:Label ID="lbl_FQty1" runat="server" Text=" QTY:" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_FailedQty1" runat="server" Visible="false"  CssClass="V2W50_Georgia18px"></asp:TextBox>
                    <br /><br />

                    <%---------------------------------------------------%>
                    <%-------------------DROP   2--------------------%>
                    <%---------------------------------------------------%>
                    <asp:DropDownList ID="drop_FailedType2" runat="server" DataValueField="id" DataTextField="idDes" CssClass="Georgia18px" OnTextChanged="Drop_FailedType_TextChanged" AutoPostBack="true" Visible="false" AppendDataBoundItems="True" >
                            <asp:ListItem Text='<%$ AppSettings:emptyItem %>' Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drop_FailedR2" runat="server" DataValueField="id" DataTextField="idDes" Visible="false" CssClass="Georgia18pxV2"  name="sid"></asp:DropDownList>
                    <asp:Label ID="lbl_FQty2" runat="server" Text=" QTY:" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_FailedQty2"  Visible="false" runat="server" CssClass="V2W50_Georgia18px"></asp:TextBox>
                    <br /><br />

                    <%---------------------------------------------------%>
                    <%-------------------DROP   3--------------------%>
                    <%---------------------------------------------------%>
                    <asp:DropDownList ID="drop_FailedType3" runat="server" DataValueField="id" DataTextField="idDes" CssClass="Georgia18px" OnTextChanged="Drop_FailedType_TextChanged" AutoPostBack="true" Visible="false" AppendDataBoundItems="True" >
                            <asp:ListItem Text='<%$ AppSettings:emptyItem %>' Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drop_FailedR3" runat="server" DataValueField="id" DataTextField="idDes" Visible="false" CssClass="Georgia18pxV2"  name="sid"></asp:DropDownList>
                    <asp:Label ID="lbl_FQty3" runat="server" Text=" QTY:" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_FailedQty3"  Visible="false" runat="server" CssClass="V2W50_Georgia18px"></asp:TextBox>
                    <br /><br />

                    <%---------------------------------------------------%>
                    <%-------------------DROP   4--------------------%>
                    <%---------------------------------------------------%>
                    <asp:DropDownList ID="drop_FailedType4" runat="server" DataValueField="id" DataTextField="idDes" CssClass="Georgia18px" OnTextChanged="Drop_FailedType_TextChanged" AutoPostBack="true" Visible="false" AppendDataBoundItems="True" >
                            <asp:ListItem Text='<%$ AppSettings:emptyItem %>' Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drop_FailedR4" runat="server" DataValueField="id" DataTextField="idDes" Visible="false" CssClass="Georgia18pxV2" name="sid"></asp:DropDownList>
                    <asp:Label ID="lbl_FQty4" runat="server" Text=" QTY:" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_FailedQty4"  Visible="false" runat="server" CssClass="V2W50_Georgia18px"></asp:TextBox>
                    <br /><br />

                    <%---------------------------------------------------%>
                    <%-------------------DROP   5--------------------%>
                    <%---------------------------------------------------%>
                    <asp:DropDownList ID="drop_FailedType5" runat="server" DataValueField="id" DataTextField="idDes" CssClass="Georgia18px" OnTextChanged="Drop_FailedType_TextChanged" AutoPostBack="true" Visible="false" AppendDataBoundItems="True" >
                            <asp:ListItem Text='<%$ AppSettings:emptyItem %>' Value="0"></asp:ListItem>
                    </asp:DropDownList>                    
                    <asp:DropDownList ID="drop_FailedR5" runat="server" DataValueField="id" DataTextField="idDes" Visible="false" CssClass="Georgia18pxV2" name="sid"></asp:DropDownList>
                    <asp:Label ID="lbl_FQty5" runat="server" Text=" QTY:" Visible="false"></asp:Label>
                    <asp:TextBox ID="tb_FailedQty5"  Visible="false" runat="server" CssClass="V2W50_Georgia18px"></asp:TextBox>
                    <br /><br />
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Label ID="Label10" runat="server" Text="Process Time : "></asp:Label>
            <asp:TextBox ID="tb_ProHr" runat="server" CssClass="W50_Georgia18px" TextMode="Number"></asp:TextBox>
            <asp:Label ID="Label11" runat="server" Text="HR "></asp:Label>
            <asp:TextBox ID="tb_ProMin" runat="server" CssClass="W50_Georgia18px" TextMode="Number"></asp:TextBox>
            <asp:Label ID="label" runat="server" Text="MIN "></asp:Label>
            <br /><br /><br />
            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" CssClass="W100H28-georgia18px" />
            <div style="margin-bottom:50px"></div>
    </asp:Panel>
    </div>

</asp:Content>
