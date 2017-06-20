<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateNewCustomer.aspx.vb" Inherits="WebApplication1.WebForm2" enablesessionstate="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create New Customer</title>
    <link rel="stylesheet" type="text/css" href="CreateNewCustomer.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name:
            <asp:TextBox ID="NameBox" runat="server" AutoPostBack="True"></asp:TextBox>
            <asp:Label ID="RequiresNameLabel" runat="server" Text="Name required!"></asp:Label>
            <br />
            Date of Birth:
            <asp:TextBox ID="DobBox" runat="server" TextMode="Date" AutoPostBack="True"></asp:TextBox>
            <asp:Label ID="DobRequired" runat="server" Text="Date of birth required!"></asp:Label>
            <br />
            Email: <asp:TextBox ID="EmailBox" runat="server" TextMode="Email" AutoPostBack="True"></asp:TextBox>
            <br />
            <br />
            <br />
            <div>
                <h1>Credit Card Info</h1>
            Address:
            <asp:TextBox ID="AddressBox" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="BadAddressLabel" runat="server" Text="Address Required!"></asp:Label>
                <br />
                FrontNumber:
                <asp:TextBox ID="CreditCardFrontBox" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="BadFrontNumberLabel" runat="server" Text="Front Number Required!"></asp:Label>
                <br />
                Expiration Date: <asp:TextBox ID="ExpirationDateBox" runat="server" TextMode="Date" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="BadExpirDateLabel" runat="server" Text="Expiration Date Required!"></asp:Label>
                <br />
                BackNumber:
                <asp:TextBox ID="CreditCardBackBox" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="BadBackNumLabel" runat="server" Text="Back Number Required!"></asp:Label>
            </div>
            <br />
            Username desired:
            <asp:TextBox ID="UsernameBox" runat="server" AutoPostBack="True"></asp:TextBox>
            <asp:Label ID="BadUsernameLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="PasswordsErrorLabel" runat="server"></asp:Label>
            <br />
            Password desired:&nbsp; 
            <asp:TextBox ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            Confirm password desired:&nbsp;
            <asp:TextBox ID="ConfirmPasswordBox" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="CreateButton" runat="server" Text="Create New Customer" />
            <br />
        </div>
    </form>
</body>
</html>
