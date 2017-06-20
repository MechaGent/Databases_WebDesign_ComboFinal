<%@ page language="vb" autoeventwireup="false" codebehind="test_addOrder.aspx.vb" inherits="WebApplication1.test_addOrder" enablesessionstate="True" %>

<!DOCTYPE html>

<header>
    <h1>Checkout</h1>
</header>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checkout</title>
    <link rel="stylesheet" type="text/css" href="test_addOrder.css" />
</head>
    
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <h1>Choose a Product:</h1>
            Shape:
            <asp:DropDownList CssClass="DropDownListClass" ID="ShapeDropDownList" runat="server" AutoPostBack="True" AppendDataBoundItems="True" ToolTip="The shape of the widget you desire to purchase">
            </asp:DropDownList>
            <br />
            Color:
            <asp:DropDownList CssClass="DropDownListClass" ID="ColorDropDownList" runat="server" AutoPostBack="True" ToolTip="The color of the widget you wish to purchase">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Image ID="WidgetDisplay_Cube" runat="server" ImageUrl="~/Widget_Cube2.bmp" BackColor="Transparent" AlternateText="The Square One" />
            <asp:Image ID="WidgetDisplay_Sphere" runat="server" ImageUrl="~/Widget_Sphere.bmp" Height="518px" Width="733px" BackColor="Transparent" ForeColor="Transparent" AlternateText="The Spherical One" />
            <br />
            <br />
            Quantity:
            <asp:TextBox CssClass="TextBoxClass" ID="QuantBox" runat="server" AutoPostBack="True" ToolTip="The quantity of the item you wish to purchase"></asp:TextBox>
            <asp:Label ID="BadQuantityFlag" runat="server" Text="Invalid Input!"></asp:Label>
            <br />
            <br />
            Price per widget:
            <asp:TextBox CssClass="TextBoxClass" ID="UnitPriceBox" runat="server" ReadOnly="True" ToolTip="The price per unit of the widget you wish to purchase"></asp:TextBox>
            <br />
            Price Subtotal:
            <asp:TextBox CssClass="TextBoxClass" ID="PriceSubtotal" runat="server" ReadOnly="True" ToolTip="The cost of purchasing your chosen widget in the quantity selected"></asp:TextBox>
            <br />
            <br />
            <asp:Button CssClass="TextBoxClass" ID="AddToCart" runat="server" Text="Add To Cart" ToolTip="Add your selected item, in your selected quantity, to the cart"/>
            <br />
            <br />
            <asp:Button CssClass="TextBoxClass" ID="SubmitOrder" runat="server" Text="Submit Order" ToolTip="Purchase everything in the shopping cart"/>
            <asp:Label ID="BadSubmitOrderFlag" runat="server"></asp:Label>
            <br />
            <br />
            <table class="CartTableClass">
                <caption>Current Shopping Cart</caption>
                <tr>
                    <th>Shapes</th>
                    <th>Colors</th>
                    <th>Quantities</th>
                    <th>Unit Prices</th>
                    <th>Subtotals</th>
                    <th>Current Total</th>
                </tr>
                <tr>
                    <td><asp:ListBox CssClass="DropDownListClass" ID="ShapesListBox" runat="server"></asp:ListBox></td>
                    <td><asp:ListBox CssClass="DropDownListClass" ID="ColorsListBox" runat="server"></asp:ListBox></td>
                    <td><asp:ListBox CssClass="DropDownListClass" ID="QuantityListBox" runat="server"></asp:ListBox></td>
                    <td><asp:ListBox CssClass="DropDownListClass" ID="UnitPriceListBox" runat="server"></asp:ListBox></td>
                    <td><asp:ListBox CssClass="DropDownListClass" ID="Subtotals" runat="server"></asp:ListBox></td>
                    <td><asp:TextBox CssClass="TextBoxClass" ID="CurrentTotal" runat="server" AutoPostBack="True" ToolTip="The current total of the cost of the items you wish to purchase" ReadOnly="True"></asp:TextBox></td>
                </tr>
            </table>
            &nbsp;&nbsp;&nbsp;
            <asp:ListBox ID="ComboKeyStorage" runat="server" Visible="False"></asp:ListBox>
        </div>
    </form>
</body>
</html>
