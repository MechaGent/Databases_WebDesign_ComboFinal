Public Class Homepage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Server.Transfer("LoginPage.aspx")
    End Sub

    Protected Sub CreateNewUserButton_Click(sender As Object, e As EventArgs) Handles CreateNewUserButton.Click
        Server.Transfer("CreateNewCustomer.aspx")
    End Sub
End Class