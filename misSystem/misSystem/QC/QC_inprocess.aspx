<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="QC_inprocess.aspx.cs" Inherits="misSystem.QC.QC_inprocess" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <title>QC</title>
<style>
    #out_tb {
        width:90%;
        margin:auto;
        padding:0px;
        white-space:nowrap;
        border: 1pt solid blue;
        vertical-align:central;
    }    
    #out_tb tr td {
        width: 50%;
        margin: 3px;
        border: 1pt solid #0a89e5;
        font-size: 20px;        
    }
    label[name=error] {
        margin-left:10em;
        font-size:12pt;
        color:red;
    }
    .label-1{
        color:#f00;
        margin:8px 0;
        display:inline-block;    
    }
    .label-2 {
        margin: 0 5px;
        display:inline-block;
    }
    #out_div {
        position:absolute;
        top:170px;
        width:900px;
        left:300px;
    }
</style>
<script src="jquery-2.1.4.min.js"></script>
<script>
    function iqc_check() {
        var flag = true;
        var str = '';
        if ($('#<%=iqc_hour_dl.ClientID%>').val() == 0 && $('#<%=iqc_min_dl.ClientID%>').val() == 0) { $('#iqc_time_lab').text('Illegal Time'); str += 'Illegal Time.\n'; flag = false; }
        
        var regex;
        regex = /^[0-9]+$/;
        if (!regex.test($('#<%=iqc_FQty_text.ClientID%>').val())) { $('#iqc_FQty_text').text('Illegal Failure Qty.'); str += 'Illegal Qty\n'; $('#<%=iqc_FQty_text.ClientID%>').val(''); flag = false; }        
        else if (parseInt($('#<%=iqc_FQty_text.ClientID%>').val()) > parseInt($('#<%=qty_lab.ClientID%>').text())) { str += 'Illegal Qty\n'; $('#<%=iqc_FQty_text.ClientID%>').val(''); flag = false; }
                
        if (!flag) { alert(str); return flag; }      

        str = '';
        str += 'Process Time is ' + $('#<%=iqc_hour_dl.ClientID%>').val() + ' hour(s) ' + $('#<%=iqc_min_dl.ClientID%>').val() + ' min(s).\n';
        str += 'Failure Qty is ' + $('#<%=iqc_FQty_text.ClientID%>').val() + '\n';
        return confirm(str + 'are you sure to submit？');
    }
    function fqc_check() {
        var flag = true;
        var str = '';
        var regex;
        if ($('#<%=iqc_hour_dl.ClientID%>').val() == 0 && $('#<%=iqc_min_dl.ClientID%>').val() == 0) { $('#iqc_time_lab').text('Illegal Time'); str += 'Illegal Time.\n'; flag = false; }
        if (!flag) { alert(str); return flag; }
        str = '';
        str += 'Process Time is ' + $('#<%=fqc_hour_dl.ClientID%>').val() + ' hour(s) ' + $('#<%=fqc_min_dl.ClientID%>').val() + ' min(s).\n';
        if ($('#<%=fqc_failure_rad.ClientID%>').is(':checked'))
        { str += 'This FQC is Failure.\n'; }
        else { str += 'This FQC is Passed.\n'; }
        return confirm(str + 'are you sure to submit？');
    }
    
    $(document).ready(function () {        
        $('#<%=iqc_FQty_text.ClientID%>').on('focusout', function () {
            var q;
            if ($('#<%=sampling_lab.ClientID%>').text() != "") {
                q = parseInt($('#<%=sampling_qty_lab.ClientID%>').text());
            }
            else { q = parseInt($('#<%=qty_lab.ClientID%>').text()); }

            var regex = /^[0-9]+$/;
            if (!regex.test($('#<%=iqc_FQty_text.ClientID%>').val())) { $('#iqc_fqty_lab').text('Illegal Failure Qty.'); }
            else if (parseInt($('#<%=iqc_FQty_text.ClientID%>').val()) > q) { $('#iqc_fqty_lab').text('Failure Qty can\'t be more than Qty.'); }
            else if (parseInt($('#<%=iqc_Qty_text.ClientID%>').val()) < parseInt($('#<%=iqc_FQty_text.ClientID%>').val())) { $('#iqc_qty_lab').text('Failure Qty can\'t be more than Qty.'); }
            else { $('#iqc_fqty_lab').text(' '); }

            if ($('#<%=iqc_hour_dl.ClientID%>').val() == 0 && $('#<%=iqc_min_dl.ClientID%>').val() == 0) { $('#iqc_time_lab').text('Illegal Time'); }
            else { $('#iqc_time_lab').text(' '); }
        });
        $('#<%=iqc_Qty_text.ClientID%>').on('focusout', function () {
            var regex = /^[0-9]+$/;
            var q;
            if ($('#<%=sampling_lab.ClientID%>').text() != "") {
                q = parseInt($('#<%=sampling_qty_lab.ClientID%>').text());
            }
            else { q = parseInt($('#<%=qty_lab.ClientID%>').text()); }

            if (!regex.test($('#<%=iqc_Qty_text.ClientID%>').val())) { $('#iqc_qty_lab').text('Illegal Qty'); }
            else if (parseInt($('#<%=iqc_Qty_text.ClientID%>').val()) > q) { $('#iqc_qty_lab').text('IQC Qty can\'t be more than Qty.'); }
            else if (parseInt($('#<%=iqc_Qty_text.ClientID%>').val()) < parseInt($('#<%=iqc_FQty_text.ClientID%>').val())) { $('#iqc_qty_lab').text('Failure Qty can\'t be more than Qty.'); }
            else { $('#iqc_qty_lab').text(' '); }

            if ($('#<%=iqc_hour_dl.ClientID%>').val() == 0 && $('#<%=iqc_min_dl.ClientID%>').val() == 0) { $('#iqc_time_lab').text('Illegal Time'); flag = false; }
            else { $('#iqc_time_lab').text(' '); }
        });
    });    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="id_lab" runat="server" Text="" Visible="False"></asp:Label>
    <asp:Label ID="item_lab" runat="server" Visible="False"></asp:Label>
    <div id="out_div">
    <table id="out_tb">
        <tr>
            <td>
                <asp:Panel ID="sampling_pan" runat="server" Visible="False">
                    Sampling: <asp:Label ID="sampling_lab" runat="server" CssClass="label-1" style="text-decoration:underline;" Text="---"></asp:Label><br />
                    Sampling Qty: <asp:Label ID="sampling_qty_lab" runat="server" CssClass="label-1" style="text-decoration:underline;" Text="---"></asp:Label><br />
                </asp:Panel>
                Machine SN: <asp:Label ID="sn_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Part No: <asp:Label ID="part_no_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Process Type: <asp:Label ID="process_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Catalogue: <asp:Label ID="catalogue_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Item Name: <asp:Label ID="item_name_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Vender: <asp:Label ID="vender_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Manufacture By: <asp:Label ID="manuBy_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                Total Qty: <asp:Label ID="qty_lab" runat="server" CssClass="label-1" Text="---"></asp:Label><br />
                <p style="margin:0px; padding:0px; text-align:center;"><asp:Button ID="vali_btn" runat="server" Text="Start Validation" OnClientClick="return confirm('are you going to start to fill form？');" OnClick="vali_btn_Click"/></p>
            </td>
            <td>
                <asp:Panel ID="b_pan" runat="server" Visible="False">                
                    <asp:Label ID="Label1" runat="server" CssClass="label-2" Text="Process time:" ></asp:Label>
                    <%--<asp:TextBox ID="iqc_time_text" runat="server" ></asp:TextBox>--%>
                    <asp:DropDownList ID="iqc_hour_dl" runat="server"></asp:DropDownList> hour 
                    <asp:DropDownList ID="iqc_min_dl" runat="server"></asp:DropDownList> min<br />
                    <label id="iqc_time_lab" name="error"></label><br />
                    <asp:Label ID="Label2" runat="server" CssClass="label-2" Text="Failure Qty:" ></asp:Label>
                    <asp:TextBox ID="iqc_FQty_text" runat="server" ></asp:TextBox><br />
                    <label id="iqc_fqty_lab" name="error"></label><br />
                    <asp:Label ID="Label4" runat="server" CssClass="label-2" Text="Qty:" ></asp:Label>
                    <asp:TextBox ID="iqc_Qty_text" runat="server" ></asp:TextBox><br />
                    <label id="iqc_qty_lab" name="error"></label><br />
                    <asp:Label ID="Label3" runat="server" CssClass="label-2" Text="Reason for Rejection:" ></asp:Label>
                    <asp:DropDownList ID="iqc_reason_dl" runat="server" OnSelectedIndexChanged="iqc_reason_dl_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><br /><br />
                    <asp:Label ID="iqc_iss_lab" runat="server" CssClass="label-2" Text="Issure Detail:" Visible="False" ></asp:Label><br />
                    <asp:TextBox ID="iqc_iss_text" runat="server" Visible="False" MaxLength="100" Width="80%" ></asp:TextBox><br /><br />
                    <p style="margin:0px; padding:0px; text-align:center;"><asp:Button ID="iqc_submit_btn" runat="server" Text="Submit" OnClientClick="return iqc_check();" OnClick="iqc_submit_btn_Click" /></p>
                </asp:Panel>
                <asp:Panel ID="c_pan" runat="server" Visible="False"> 
                    <asp:Label ID="Label5" runat="server" CssClass="label-2" Text="Process time:" ></asp:Label>
                    <%--<asp:TextBox ID="fqc_time_text" runat="server" ></asp:TextBox>--%>
                    <asp:DropDownList ID="fqc_hour_dl" runat="server"></asp:DropDownList> hour 
                    <asp:DropDownList ID="fqc_min_dl" runat="server"></asp:DropDownList> min<br />
                    <asp:label id="fqc_time_lab" runat="server" style="margin-left:10em; font-size:12pt; color:red;"></asp:label><br />
                    <asp:RadioButton ID="fqc_failure_rad" CssClass="label-2" runat="server" Text="Failure" GroupName="fqc_rad" Checked="True" />
                    <asp:RadioButton ID="fqc_pass_rad" CssClass="label-2" runat="server" Text="Passed" GroupName="fqc_rad" />
                    <br /><br />
                    <asp:Label ID="Label7" runat="server" CssClass="label-2" Text="Reason for Rejection:" ></asp:Label>
                    <asp:DropDownList ID="fqc_reason_dl_1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="fqc_reason_dl_1_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="fqc_reason_dl_2" runat="server" AutoPostBack="True"></asp:DropDownList>
                    <br /><br />
                    <asp:Label ID="fqc_iss_lab" runat="server" CssClass="label-2" Text="Issure Detail:" Visible="False" ></asp:Label><br />
                    <asp:TextBox ID="fqc_iss_text" runat="server" Visible="False" MaxLength="100" Width="80%" ></asp:TextBox><br /><br />
                    <p style="margin:0px; padding:0px; text-align:center;"><asp:Button ID="fqc_submit_btn" runat="server" Text="Submit" OnClientClick="return fqc_check();" OnClick="fqc_submit_btn_Click" /></p>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
