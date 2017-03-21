<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="QC_set_sampling.aspx.cs" Inherits="misSystem.QC.QC_set_sampling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sampling</title>
    <style>
    #out_div {
        position:absolute;
        top:170px;
        width:900px;
        left:300px;
        font-size: 20px;
    }
     .label-1{
        color:#f00;
        display:inline-block;    
    }
    label[name=error] {
        margin-left:2em;
        font-size:12pt;
        color:red;
    }
</style>
<script src="jquery-2.1.4.min.js"></script>
<script>
    function checkRation() {
        return confirm('Sampling Ration is ' + $('#<%=sampling_text.ClientID%>').val() + '%.');
    }
    $(document).ready(function () {
        $('#<%=sampling_text.ClientID%>').on('focusout', function () {
            var regex = /^[0-9]+(\.[0-9]{1,2})?$/;
            if (parseFloat($('#<%=sampling_text.ClientID%>').val()) <= 0 || parseFloat($('#<%=sampling_text.ClientID%>').val()) > 100 || $('#<%=sampling_text.ClientID%>').val() == '' || isNaN($('#<%=sampling_text.ClientID%>').val())) {
                $('#sampling_error_lab').text('The sampling ratio is 0~100%. It can\'t be uut of range or stay empty');
                $('#<%=check_btn.ClientID%>').prop('disabled', true);
                $('#<%=sampling_text.ClientID%>').val('');
                return;
            }
            else if(!regex.test($('#<%=sampling_text.ClientID%>').val())){
                var r = parseFloat($('#<%=sampling_text.ClientID%>').val()).toFixed(2);
                $('#<%=sampling_text.ClientID%>').val(r);
            }
            $('#ratio_lab').text(''); $('#<%=check_btn.ClientID%>').attr('disabled', false);
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="out_div">
        <asp:Panel ID="detail_pan" runat="server" style="margin-top:1.2em;" Visible="False" >
            SN: <asp:Label ID="sn_hide_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
            <asp:Panel ID="old_sampling_pan" runat="server" Visible="False">Sampling: <asp:Label ID="sampling_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br /></asp:Panel>
            Part No: <asp:Label ID="part_no_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            Process Type: <asp:Label ID="process_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            Catalogue: <asp:Label ID="catalogue_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            Item Name: <asp:Label ID="item_name_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            Vender: <asp:Label ID="vender_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            Manufacture By: <asp:Label ID="manuBy_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            Qty: <asp:Label ID="qty_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
            <hr />
            <asp:Panel ID="check_pan" runat="server" Visible="False">
                <asp:Label ID="Label2" runat="server" Text="Sampling Ratio: " CssClass="label-2"></asp:Label>
                <asp:TextBox ID="sampling_text" runat="server" Width="3em" style="text-align:center;" ></asp:TextBox>%
                <asp:Button ID="check_btn" runat="server" Text="Check" style="margin-left:1em;" disabled="true" OnClientClick="checkRation();" OnClick="check_btn_Click" />
                <label id="sampling_error_lab" name="error"></label>
            </asp:Panel>            
        </asp:Panel>
        <p style="text-align:center;"><asp:Label ID="result_lab" runat="server" style="font-size:30pt;" Text=""></asp:Label></p>
    </div>
</asp:Content>
