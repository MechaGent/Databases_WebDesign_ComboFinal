Public Class LoginPage
    Inherits System.Web.UI.Page

    Shared ErrorMsg_NoUsername As String = "Username required!"
    Shared ErrorMsg_NoPassword As String = "Password required!"
    Shared ErrorMsg_IncorrectUserOrPassword As String = "Incorrect Username and/or password!"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        If (String.IsNullOrEmpty(Me.UsernameBox.Text)) Then
            Me.ErrorsLabel.Text = ErrorMsg_NoUsername
            Me.ErrorsLabel.Visible = True
        ElseIf (String.IsNullOrEmpty(Me.PasswordBox.Text)) Then
            Me.ErrorsLabel.Text = ErrorMsg_NoPassword
            Me.ErrorsLabel.Visible = True
        Else
            Globals.DbConnection.Open()
            Dim sqlCustomerIdCheck As String = "SELECT CustomerId FROM Usernames WHERE Username='" & Me.UsernameBox.Text & "' AND Password='" & Me.PasswordBox.Text & "'"
            Dim checkCustomerIdCommand As New OleDb.OleDbCommand(sqlCustomerIdCheck)
            checkCustomerIdCommand.Connection = Globals.DbConnection
            Dim tempCustomerId As Object = checkCustomerIdCommand.ExecuteScalar

            If (tempCustomerId Is Nothing) Then
                Me.ErrorsLabel.Text = ErrorMsg_IncorrectUserOrPassword
                Me.ErrorsLabel.Visible = True
            Else
                Dim CustomerId As Integer = CType(tempCustomerId, Integer)
                Session.Item("CustomerId") = CustomerId
                Globals.DbConnection.Close()
                Server.Transfer("test_addOrder.aspx")
            End If

            Globals.DbConnection.Close()
        End If

    End Sub

    Protected Sub ErrorsLabel_Init(sender As Object, e As EventArgs) Handles ErrorsLabel.Init
        Me.ErrorsLabel.Visible = False
    End Sub
End Class