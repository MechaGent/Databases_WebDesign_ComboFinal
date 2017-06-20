<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Homepage.aspx.vb" Inherits="WebApplication1.Homepage" enablesessionstate="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homepage</title>
    <link rel="stylesheet" type="text/css" href="Homepage.css" />
</head>
<body>
    <h1>Homepage</h1>
    <form id="form1" runat="server">
        <asp:Button CssClass="TextBoxClass" ID="LoginButton" runat="server" Text="Login?" ToolTip="Click This If you've Got An Account"/>
        <div>
            <asp:Button CssClass="TextBoxClass" ID="CreateNewUserButton" runat="server" Text="Create New User?" ToolTip="Click This If You Don't Have An Account" />
        </div>
    </form>
</body>
</html>
