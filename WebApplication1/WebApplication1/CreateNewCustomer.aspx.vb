Public Class WebForm2
    Inherits System.Web.UI.Page

    Shared ErrorMsg_NeedsUsername As String = "Username required!"
    Shared ErrorMsg_NonUniqueUsername As String = "Username already in use!"
    Shared ErrorMsg_NonMatchingPasswords As String = "Passwords do not match!"
    Shared ErrorMsg_NeedsPassword As String = "Password required!"

    Protected Sub BadAddressLabel_Init(sender As Object, e As EventArgs) Handles BadAddressLabel.Init

    End Sub

    Protected Sub BadFrontNumberLabel_Init(sender As Object, e As EventArgs) Handles BadFrontNumberLabel.Init

    End Sub

    Protected Sub BadExpirDateLabel_Init(sender As Object, e As EventArgs) Handles BadExpirDateLabel.Init

    End Sub

    Protected Sub BadBackNumLabel_Init(sender As Object, e As EventArgs) Handles BadBackNumLabel.Init

    End Sub

    Protected Sub CreateButton_Init(sender As Object, e As EventArgs) Handles CreateButton.Init
        Me.CreateButton.Enabled = False
    End Sub

    Private Sub checkForCreationValidity()
        If (Me.BadUsernameLabel.Visible Or Me.DobRequired.Visible Or Me.BadAddressLabel.Visible Or Me.BadFrontNumberLabel.Visible Or Me.BadExpirDateLabel.Visible Or Me.BadBackNumLabel.Visible Or Me.BadUsernameLabel.Visible) Then
            Me.CreateButton.Enabled = False
        Else
            Me.CreateButton.Enabled = True
        End If
    End Sub

    Protected Sub BadUsernameLabel_Init(sender As Object, e As EventArgs) Handles BadUsernameLabel.Init
        Me.BadUsernameLabel.Text = ErrorMsg_NeedsUsername
    End Sub

    Protected Sub PasswordsErrorLabel_Init(sender As Object, e As EventArgs) Handles PasswordsErrorLabel.Init
        Me.PasswordsErrorLabel.Text = ErrorMsg_NeedsPassword
    End Sub

    Protected Sub NameBox_TextChanged(sender As Object, e As EventArgs) Handles NameBox.TextChanged
        If (String.IsNullOrEmpty(Me.NameBox.Text)) Then
            Me.RequiresNameLabel.Visible = True
        Else
            Me.RequiresNameLabel.Visible = False
        End If

        checkForCreationValidity()
    End Sub

    Protected Sub DobBox_TextChanged(sender As Object, e As EventArgs) Handles DobBox.TextChanged
        If (String.IsNullOrEmpty(Me.DobBox.Text)) Then
            Me.DobRequired.Visible = True
        Else
            Me.DobRequired.Visible = False
        End If

        checkForCreationValidity()
    End Sub

    Protected Sub AddressBox_TextChanged(sender As Object, e As EventArgs) Handles AddressBox.TextChanged
        Me.BadAddressLabel.Visible = String.IsNullOrEmpty(Me.AddressBox.Text)
    End Sub

    Protected Sub CreditCardFrontBox_TextChanged(sender As Object, e As EventArgs) Handles CreditCardFrontBox.TextChanged
        Me.BadFrontNumberLabel.Visible = String.IsNullOrEmpty(Me.CreditCardFrontBox.Text)
    End Sub

    Protected Sub ExpirationDateBox_TextChanged(sender As Object, e As EventArgs) Handles ExpirationDateBox.TextChanged
        Me.BadExpirDateLabel.Visible = String.IsNullOrEmpty(Me.ExpirationDateBox.Text)
    End Sub

    Protected Sub CreditCardBackBox_TextChanged(sender As Object, e As EventArgs) Handles CreditCardBackBox.TextChanged
        Me.BadBackNumLabel.Visible = String.IsNullOrEmpty(Me.CreditCardBackBox.Text)
    End Sub

    Protected Sub UsernameBox_TextChanged(sender As Object, e As EventArgs) Handles UsernameBox.TextChanged
        If (String.IsNullOrEmpty(Me.UsernameBox.Text)) Then
            Me.BadUsernameLabel.Visible = True
            Me.BadUsernameLabel.Text = ErrorMsg_NeedsUsername
        Else
            Globals.DbConnection.Open()
            Dim sqlCustomerIdCheck As String = "SELECT 1 FROM Usernames WHERE Username='" & Me.UsernameBox.Text & "'"
            Dim checkCustomerIdCommand As New OleDb.OleDbCommand(sqlCustomerIdCheck)
            checkCustomerIdCommand.Connection = Globals.DbConnection

            If (checkCustomerIdCommand.ExecuteScalar Is Nothing) Then
                Me.BadUsernameLabel.Visible = False
                Me.BadUsernameLabel.Text = ""
            Else
                Me.BadUsernameLabel.Visible = True
                Me.BadUsernameLabel.Text = ErrorMsg_NonUniqueUsername
            End If

            Globals.DbConnection.Close()
        End If

        checkForCreationValidity()
    End Sub

    Protected Sub ConfirmPasswordBox_TextChanged(sender As Object, e As EventArgs) Handles ConfirmPasswordBox.TextChanged
        If (String.IsNullOrEmpty(Me.PasswordBox.Text)) Then
            Me.PasswordsErrorLabel.Visible = True
            Me.PasswordsErrorLabel.Text = ErrorMsg_NeedsPassword
        ElseIf (String.IsNullOrEmpty(Me.ConfirmPasswordBox.Text)) Then
            Me.PasswordsErrorLabel.Visible = True
            Me.PasswordsErrorLabel.Text = ErrorMsg_NonMatchingPasswords
        ElseIf (Not Me.PasswordBox.Text.Equals(Me.ConfirmPasswordBox.Text)) Then
            Me.PasswordsErrorLabel.Visible = True
            Me.PasswordsErrorLabel.Text = ErrorMsg_NonMatchingPasswords
        Else
            Me.PasswordsErrorLabel.Visible = False
            Me.PasswordsErrorLabel.Text = ""
            checkForCreationValidity()
            Exit Sub
        End If

        Me.CreateButton.Enabled = False
    End Sub

    Protected Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        If (String.IsNullOrEmpty(Me.PasswordBox.Text)) Then
            Me.PasswordsErrorLabel.Visible = True
            Me.PasswordsErrorLabel.Text = ErrorMsg_NeedsPassword
        ElseIf (String.IsNullOrEmpty(Me.ConfirmPasswordBox.Text)) Then
            Me.PasswordsErrorLabel.Visible = True
            Me.PasswordsErrorLabel.Text = ErrorMsg_NonMatchingPasswords
        ElseIf (Not Me.PasswordBox.Text.Equals(Me.ConfirmPasswordBox.Text)) Then
            Me.PasswordsErrorLabel.Visible = True
            Me.PasswordsErrorLabel.Text = ErrorMsg_NonMatchingPasswords
        Else
            Me.PasswordsErrorLabel.Visible = False
            Me.PasswordsErrorLabel.Text = ""
            checkForCreationValidity()

            If (Not Me.CreateButton.Enabled) Then
                Exit Sub
            End If

            Globals.DbConnection.Open()

            Dim CreditCardInfoSqlString As String = "INSERT INTO [Credit Cards] (NameOnCard, Address, ExpirationDate, CreditCardFrontNumber, BackNumber) VALUES('" &
                Me.NameBox.Text & "', '" &
                Me.AddressBox.Text & "', '" &
                Me.ExpirationDateBox.Text & "', '" &
                Me.CreditCardFrontBox.Text & "', " &
                Me.CreditCardBackBox.Text & ")"

            Dim addCreditCardInfoCommand As New OleDb.OleDbCommand(CreditCardInfoSqlString)
            addCreditCardInfoCommand.Connection = Globals.DbConnection
            addCreditCardInfoCommand.ExecuteNonQuery()

            Dim getCreditCardIdSqlString As String = "SELECT MAX(CreditCardIdNum) FROM [Credit Cards]"
            Dim getCreditCardIdCommand As New OleDb.OleDbCommand(getCreditCardIdSqlString)
            getCreditCardIdCommand.Connection = Globals.DbConnection
            Dim tempCreditCardId = getCreditCardIdCommand.ExecuteScalar()

            If (tempCreditCardId Is Nothing) Then
                Throw New System.Exception("could not find creditCardId")
            End If

            Dim creditCardId As Integer = CType(tempCreditCardId, Integer)

            Dim customerSqlString As String = "INSERT INTO Customers (CustomerName, DoB, Email, PreferredCreditCardId) Values('" &
                Me.NameBox.Text & "', '" &
                Me.DobBox.Text & "', '" &
                Me.EmailBox.Text & "', " &
                creditCardId &
                ")"

            Dim command As New OleDb.OleDbCommand(customerSqlString)
            command.Connection = Globals.DbConnection
            command.ExecuteNonQuery()

            Dim getCustomerIdSqlString As String = "SELECT MAX(CustomerId) FROM Customers"
            Dim checkOrderIdCommand As New OleDb.OleDbCommand(getCustomerIdSqlString)
            checkOrderIdCommand.Connection = Globals.DbConnection
            Dim tempCustomerId = checkOrderIdCommand.ExecuteScalar()

            'checking if tempOrderId returned a number
            If (tempCustomerId Is Nothing) Then
                Throw New System.Exception("could not find customerId")
            End If

            Dim customerId As Integer = CType(tempCustomerId, Integer)

            Dim userSqlString As String = $"INSERT INTO Usernames Values ('{Me.UsernameBox.Text}', '{Me.PasswordBox.Text}', {Convert.ToString(customerId)})"
            Dim UsernameCommand As New OleDb.OleDbCommand(userSqlString)
            UsernameCommand.Connection = Globals.DbConnection
            UsernameCommand.ExecuteNonQuery()
            Session.Add("CustomerId", customerId)
            Globals.DbConnection.Close()
            Server.Transfer("test_addOrder.aspx")

        End If
    End Sub

End Class