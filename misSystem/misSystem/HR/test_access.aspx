<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test_access.aspx.cs" Inherits="misSystem.HR.test_access" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>
    <script src="myJs/jquery.timepicker.js"></script>
    <link href="myCss/jquery.timepicker.css" rel="stylesheet" />    
    <link href="myCss/HR_css.css" rel="stylesheet"/>
    <script src="myJs/HR_js.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="上次更新時間:"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="---"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Get New CheckInOut" OnClick="Button1_Click" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" CssClass="datepicker" onkeyup="this.value='';" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", null);
            $('.datepicker').datepicker("option", "maxDate", "+0");
        });
    </script>
</html>
