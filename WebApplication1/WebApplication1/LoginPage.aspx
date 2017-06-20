<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginPage.aspx.vb" Inherits="WebApplication1.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <h1>Login Page</h1>
    <link rel="stylesheet" type="text/css" href="LoginPage.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Username:
            <asp:TextBox CssClass="TextBoxClass" ID="UsernameBox" runat="server"></asp:TextBox>
            <br />
            Password:
            <asp:TextBox CssClass="TextBoxClass" ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button CssClass="TextBoxClass" ID="LoginButton" runat="server" Text="Login" ToolTip="Click This To Log In" />
            <asp:Label ID="ErrorsLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
