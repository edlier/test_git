<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_application_hire.aspx.cs" Inherits="misSystem.HR.HR_application_hire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Hire application</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>
    <link href="myCss/HR_css.css" rel="stylesheet"/>
    <script src="myJs/HR_js.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="out_panel" CssClass="out_panel" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:Panel ID="form_panel" runat="server">

        <h3>人員撥補申請單</h3>
        申請人: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
        <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label><br />
        <asp:Label ID="dep_no_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label><br />
        <span style="font-size:10pt; color:red;"> 以下為人員撥補訊息</span>
        <hr />        
        部門: <asp:DropDownList ID="dep_dl" runat="server" Font-Size="14pt"></asp:DropDownList><br />
        撥補職位: <asp:DropDownList ID="position_dl" runat="server" Font-Size="14pt"></asp:DropDownList><br />
        希望報到日期: <asp:TextBox ID="date_text" runat="server" CssClass="datepicker" TextMode="DateTime" AutoPostBack="True" OnTextChanged="date_text_TextChanged"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="date_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        編制人數: <asp:TextBox ID="prepare_no_text" runat="server" TextMode="Number" Width="5em" style="text-align:center;" AutoPostBack="True" OnTextChanged="prepare_no_text_TextChanged" Enabled="False">0</asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="prepare_no_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        現有人數: <asp:TextBox ID="now_no_text" runat="server" TextMode="Number" Width="5em" style="text-align:center;" ReadOnly="true" Enabled="False"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="now_no_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        申請撥補人數: <asp:TextBox ID="apply_no_text" runat="server" TextMode="Number" Width="5em" style="text-align:center;" AutoPostBack="True" OnTextChanged="apply_no_text_TextChanged">0</asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="apply_no_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        申請撥補原因:<br /> <asp:RadioButtonList ID="hire_reason_rl" runat="server" RepeatLayout="Flow" RepeatColumns="5" style="display:inline; margin-left:2em;" RepeatDirection="Horizontal"></asp:RadioButtonList>
        <%--<asp:TextBox ID="hire_other_text" runat="server" style="display:inline; width:7em;" MaxLength="10" Visible="False"></asp:TextBox>--%><br />
        <p style="padding:0; margin:0;"><asp:Label ID="hire_reason_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        所需條件<br />
        性別: <asp:RadioButtonList ID="sex_dl" runat="server" RepeatLayout="Flow" RepeatColumns="3" RepeatDirection="Horizontal" >
                <asp:ListItem Text="男" Value="0"></asp:ListItem>
                <asp:ListItem Text="女" Value="1"></asp:ListItem>
                <asp:ListItem Text="均可" Value="2" Selected="True"></asp:ListItem>
              </asp:RadioButtonList><br />
        <p style="padding:0; margin:0;"><asp:Label ID="sex_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        年齡: <asp:RadioButtonList ID="age_dl" runat="server" RepeatLayout="Flow" RepeatColumns="3" RepeatDirection="Horizontal" >
                <asp:ListItem Text="18~25" Value="18~25" Selected="True"></asp:ListItem>
                <asp:ListItem Text="26~35" Value="26~35"></asp:ListItem>
                <asp:ListItem Text="36~50" Value="36~50"></asp:ListItem>
              </asp:RadioButtonList><br />
        <p style="padding:0; margin:0;"><asp:Label ID="age_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        教育程度:<br /> <asp:RadioButtonList ID="edu_dl" runat="server" RepeatLayout="Flow" RepeatColumns="5" style="display:inline; margin-left:2em;" RepeatDirection="Horizontal" >
                    <asp:ListItem Text="國中" Value="國中" ></asp:ListItem>
                    <asp:ListItem Text="高中職" Value="高中職" ></asp:ListItem>
                    <asp:ListItem Text="專科" Value="專科"></asp:ListItem>
                    <asp:ListItem Text="大學" Value="大學" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="其它" Value="0" ></asp:ListItem>
                  </asp:RadioButtonList><asp:TextBox ID="edu_other_text" runat="server" style="display:inline; width:7em;" MaxLength="10" OnTextChanged="edu_other_text_TextChanged"></asp:TextBox> 以上<br />
        <p style="padding:0; margin:0;"><asp:Label ID="edu_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        主修科系: <asp:TextBox ID="major_text" runat="server" Width="10em" MaxLength="30" AutoPostBack="True" OnTextChanged="major_text_TextChanged"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="major_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        專長需求: <asp:TextBox ID="skill_text" runat="server" Width="10em" MaxLength="30" AutoPostBack="True" OnTextChanged="skill_text_TextChanged"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="skill_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        其它: <asp:TextBox ID="other_text" runat="server" Width="10em" MaxLength="30" AutoPostBack="True"></asp:TextBox><br />
        <p style="padding:0; margin:0;"><asp:Label ID="other_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        <table style="margin:0; padding:0; height:auto;"><tr>
            <td style="vertical-align:top;">工作內容:</td>
            <td style="vertical-align:top; padding:0;"><asp:TextBox ID="content_text" runat="server" MaxLength="100" TextMode="MultiLine" style="width:15em; height:100%;" AutoPostBack="True" OnTextChanged="content_text_TextChanged"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2"><asp:Label ID="content_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></td>
        </tr></table>       
             
        </asp:Panel>

        <asp:Panel ID="hr_panel" runat="server" style="border-top:1pt black solid;" Visible="false">
            <table style="width:400px;">
                <tr>
                    <td style="vertical-align:text-top;">
                        缺額現況:
                        <asp:RadioButtonList ID="now_situration_rl" runat="server" OnSelectedIndexChanged="now_situration_rl_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="核對無誤" Value="correct" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="有誤" Value="error"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        甄選方法:
                        <asp:RadioButtonList ID="way_rl" runat="server">
                            <asp:ListItem Text="員工推介"></asp:ListItem>
                            <asp:ListItem Text="人力仲介" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="其它"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="hr_panel2" runat="server" Visible="false" style="color:red;">
            甄選方法: <asp:Label ID="way_lab" runat="server" Text="---"></asp:Label>
        </asp:Panel>

        <asp:Button ID="send_btn" runat="server" Text="Send" Font-Size="16pt" style="margin-left:10em" OnClick="send_btn_Click" />        
        <asp:Button ID="pass_btn" runat="server" Text="Pass" Font-Size="16pt" style="margin-left:10em" OnClick="pass_btn_Click" Visible="False" />
        <asp:Button ID="fail_btn" runat="server" Text="Fail" Font-Size="16pt" style="margin-left:1em" OnClick="fail_btn_Click" Visible="False" />
        <asp:Button ID="edit_btn" runat="server" Text="Edit" Font-Size="16pt" style="margin-left:10em" OnClick="edit_btn_Click" Visible="False" />
        <asp:Button ID="delete_btn" runat="server" Text="Delete" Font-Size="16pt" style="margin-left:1em" OnClick="delete_btn_Click" Visible="False" />
        <br /><br /><br />
                
            </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Panel> 
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", -5);
            $('.datepicker').datepicker("option", "maxDate", null);

            //配合update panel不會重新執行ready事件
            //為update panel註冊事件，在update完成後執行initi_up();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_pageLoaded(initi_up);
            function initi_up() {
                date_initi();
                $('.datepicker').datepicker("option", "minDate", -5);
                $('.datepicker').datepicker("option", "maxDate", null);
            }
        });
    </script>
</asp:Content>
