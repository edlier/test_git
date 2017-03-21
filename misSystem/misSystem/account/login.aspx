<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="misSystem.account.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link href="../CssFile/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btn_enter">

        <div id="centerlizeRecord">
        <div id="login_tittle">Login</div>
        <br />
        
                <section id="loginForm">
                    <div class="form-horizontal">
                        <hr />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                        <br />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lab1" CssClass="georgia20px">ID</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="UserName" CssClass="georgia20px" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                        CssClass="text-danger" ErrorMessage="You must enter account!" /></td>
                            </tr>
                            <tr>
                                <td style="height: 50px; width: 130px ;">
                                    <asp:Label runat="server" ID="lab2" CssClass="georgia20px">password</asp:Label></td>
                                <td>
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="georgia20px" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="You must enter password!" /></td>
                            </tr>
                        </table>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" OnClick="LogIn" Text="Login" CssClass="btn" ID="btn_enter"/>
                            </div>
                        </div>
                    </div>
                </section>
        </div>
        
    </form>
</body>
</html>
