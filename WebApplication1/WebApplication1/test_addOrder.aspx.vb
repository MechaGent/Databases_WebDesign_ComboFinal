Public Class test_addOrder
    Inherits System.Web.UI.Page

    'Shared costPerUnit As Decimal
    'Shared quantity As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.CustomerIdField.Text = CType(Session.Item("CustomerId"), String)
    End Sub

    Protected Sub ColorDropDownList_Init(sender As Object, e As EventArgs) Handles ColorDropDownList.Init
        Me.ColorDropDownList.Items.Add("")
        Dim sqlString As String = "SELECT [Color] FROM [ProductColors]"
        Dim command As New OleDb.OleDbCommand(sqlString)
        command.Connection = Globals.DbConnection
        Globals.DbConnection.Open()
        Dim reader As OleDb.OleDbDataReader = command.ExecuteReader()

        While (reader.Read())
            Me.ColorDropDownList.Items.Add(reader.GetValue(0))
        End While

        Globals.DbConnection.Close()
    End Sub

    Protected Sub SubmitOrder_Init(sender As Object, e As EventArgs) Handles SubmitOrder.Init
        Me.SubmitOrder.Enabled = False
    End Sub

    Protected Sub BadSubmitOrderFlag_Init(sender As Object, e As EventArgs) Handles BadSubmitOrderFlag.Init
        Me.BadSubmitOrderFlag.Visible = False
    End Sub

    Protected Sub ShapeDropDownList_Init(sender As Object, e As EventArgs) Handles ShapeDropDownList.Init
        'Me.ShapeDropDownList.ClearSelection()
        Me.ShapeDropDownList.Items.Add("")
        Dim sqlString As String = "SELECT [Shape] FROM [ProductData]"
        Dim command As New OleDb.OleDbCommand(sqlString)
        command.Connection = Globals.DbConnection
        Globals.DbConnection.Open()
        Dim reader As OleDb.OleDbDataReader = command.ExecuteReader()

        While (reader.Read())
            Me.ShapeDropDownList.Items.Add(reader.GetValue(0))
        End While

        Globals.DbConnection.Close()
    End Sub

    Protected Sub AddToCart_Init(sender As Object, e As EventArgs) Handles AddToCart.Init
        Me.AddToCart.Enabled = False
    End Sub

    Protected Sub WidgetDisplay_Cube_Init(sender As Object, e As EventArgs) Handles WidgetDisplay_Cube.Init
        Me.WidgetDisplay_Cube.Visible = False
    End Sub

    Protected Sub WidgetDisplay_Sphere_Init(sender As Object, e As EventArgs) Handles WidgetDisplay_Sphere.Init
        Me.WidgetDisplay_Sphere.Visible = False
    End Sub

    Protected Sub ShapeDropDownList_DataBound(sender As Object, e As EventArgs) Handles ShapeDropDownList.DataBound
        Me.ShapeDropDownList.Items.Insert(0, New ListItem(vbNullString))
        'Me.ShapeDropDownList.SelectedIndex = -1
    End Sub

    Protected Sub ColorDropDownList_DataBound(sender As Object, e As EventArgs) Handles ColorDropDownList.DataBound
        Session.Item("NumColors") = Me.ColorDropDownList.Items.Count
        Me.ColorDropDownList.Items.Insert(0, New ListItem(vbNullString))
    End Sub

    Protected Sub ShapeDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShapeDropDownList.SelectedIndexChanged
        If (Not String.IsNullOrEmpty(Me.ShapeDropDownList.Text)) Then

            If (Me.ShapeDropDownList.Text.Equals("Cube")) Then
                Me.WidgetDisplay_Cube.Visible = True
                Me.WidgetDisplay_Sphere.Visible = False
            ElseIf (Me.ShapeDropDownList.Text.Equals("Sphere")) Then
                Me.WidgetDisplay_Cube.Visible = False
                Me.WidgetDisplay_Sphere.Visible = True
            Else
                Me.WidgetDisplay_Cube.Visible = False
                Me.WidgetDisplay_Sphere.Visible = False
            End If

            Dim sqlString As String = "SELECT [Price] FROM [ProductData] WHERE [Shape]='" + Me.ShapeDropDownList.Text + "'"
            Dim command As New OleDb.OleDbCommand(sqlString)
            command.Connection = Globals.DbConnection
            Globals.DbConnection.Open()
            Dim reader As OleDb.OleDbDataReader = command.ExecuteReader()
            reader.Read()
            Dim costPerUnit As Decimal

            If (Decimal.TryParse(reader.GetDecimal(0), costPerUnit)) Then
                Session.Item("UnitCost_Mag") = costPerUnit
                'Me.UnitCost_Mag.Value = costPerUnit

                Me.UnitPriceBox.Text = FormatCurrency(costPerUnit)

                Dim quantity As Integer

                If (Not Integer.TryParse(Me.QuantBox.Text, quantity)) Then
                    Me.BadQuantityFlag.Visible = True
                    'Me.Quantity_Mag.Value = vbNullString
                    Session.Item("Quantity_Mag") = -1
                    Me.PriceSubtotal.Text = vbNullString
                Else
                    Me.BadQuantityFlag.Visible = False
                    'Me.Quantity_Mag.Value = quantity
                    Session.Item("Quantity_Mag") = quantity
                    Me.PriceSubtotal.Text = FormatCurrency(costPerUnit * quantity)

                    If (Not String.IsNullOrEmpty(Me.ColorDropDownList.Text)) Then
                        Me.AddToCart.Enabled = True
                    End If
                End If
            End If

            Globals.DbConnection.Close()
        Else
            Me.AddToCart.Enabled = False
            Me.UnitPriceBox.Text = vbNullString
        End If

        If (Me.ShapeDropDownList.Items.Count = 3) Then
            Me.ShapeDropDownList.Items.RemoveAt(0)
        End If
    End Sub

    Protected Sub ColorDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ColorDropDownList.SelectedIndexChanged
        If (Me.ColorDropDownList.Items.Count = 8) Then
            Me.ColorDropDownList.Items.RemoveAt(0)
        End If

        If (String.IsNullOrEmpty(Me.ColorDropDownList.Text)) Then
            Me.AddToCart.Enabled = False
        Else
            If (Me.ShapeDropDownList.Text.Equals("Cube")) Then
                Me.WidgetDisplay_Cube.Visible = True
                Me.WidgetDisplay_Sphere.Visible = False
            ElseIf (Me.ShapeDropDownList.Text.Equals("Sphere")) Then
                Me.WidgetDisplay_Cube.Visible = False
                Me.WidgetDisplay_Sphere.Visible = True
            Else
                Me.WidgetDisplay_Cube.Visible = False
                Me.WidgetDisplay_Sphere.Visible = False
                Me.AddToCart.Enabled = False
                Exit Sub
            End If

            If (Not (Session.Item("Quantity_Mag") = -1)) Then
                Me.AddToCart.Enabled = True
            End If
        End If


    End Sub

    Protected Sub QuantBox_TextChanged(sender As Object, e As EventArgs) Handles QuantBox.TextChanged
        Dim quantity As Integer

        If (Not Integer.TryParse(Me.QuantBox.Text, quantity)) Then
            Me.BadQuantityFlag.Visible = True
            'Me.Quantity_Mag.Value = vbNullString
            Session.Item("Quantity_Mag") = -1
            Me.PriceSubtotal.Text = vbNullString
            Me.AddToCart.Enabled = False
        Else
            Me.BadQuantityFlag.Visible = False
            'Me.Quantity_Mag.Value = Convert.ToString(quantity)
            Session.Item("Quantity_Mag") = quantity
            Dim locUnitCost_Mag As Double = Session.Item("UnitCost_Mag")
            If (Not (locUnitCost_Mag = -1)) Then
                Me.PriceSubtotal.Text = FormatCurrency(locUnitCost_Mag * quantity)

                If (Not String.IsNullOrEmpty(Me.ColorDropDownList.Text)) Then
                    Me.AddToCart.Enabled = True
                End If
            End If
        End If
    End Sub

    'remove this
    ' Protected Sub CustomerIdField_TextChanged(sender As Object, e As EventArgs) Handles CustomerIdField.TextChanged
    'If (String.IsNullOrEmpty(Me.CustomerIdField.Text)) Then
    'Me.BadCustomerIdFlag.Visible = False
    'Me.SubmitOrder.Enabled = False
    'Me.BadSubmitOrderFlag.Text = Globals.ErrorMsg_CustomerIdRequired
    'Me.BadSubmitOrderFlag.Visible = True
    'Exit Sub
    'Else
    '       Globals.DbConnection.Open()
    '
    'Dim sqlCustomerIdCheck As String = "SELECT 1 FROM Customers WHERE CustomerId=" & Me.CustomerIdField.Text
    '      Dim checkCustomerIdCommand As New OleDb.OleDbCommand(sqlCustomerIdCheck)
    '     checkCustomerIdCommand.Connection = Globals.DbConnection

    ' If (checkCustomerIdCommand.ExecuteScalar Is Nothing) Then
    'Me.BadCustomerIdFlag.Visible = True
    'Else
    'Me.BadCustomerIdFlag.Visible = False

    'If (Me.BadSubmitOrderFlag.Visible And Me.BadSubmitOrderFlag.Text.Equals(Globals.ErrorMsg_CustomerIdRequired)) Then
    'Me.BadSubmitOrderFlag.Visible = False
    'Me.BadSubmitOrderFlag.Text = ""
    'Me.SubmitOrder.Enabled = True
    'End If
    'End If

    '       Globals.DbConnection.Close()
    'End If
    'End Sub

    Protected Sub AddToCart_Click(sender As Object, e As EventArgs) Handles AddToCart.Click
        Me.AddToCart.Enabled = False
        Me.SubmitOrder.Enabled = True
        Dim comboKey As String = Me.ShapeDropDownList.Text & Me.ColorDropDownList.Text
        Dim searchIndex As Integer = 0
        Dim maxIndex As Integer = Me.ComboKeyStorage.Items.Count - 1

        For Each element As ListItem In Me.ComboKeyStorage.Items
            If (element.Text.Equals(comboKey)) Then
                Dim NewQuant As Integer = Session.Item("Quantity_Mag") + Val(Me.QuantityListBox.Items(searchIndex).Text)
                Dim newItem As String = Convert.ToString(NewQuant)
                Me.QuantityListBox.Items.RemoveAt(searchIndex)
                Me.QuantityListBox.Items.Insert(searchIndex, newItem)
                Dim tempSubtotal As Decimal = Session.Item("UnitCost_Mag") * NewQuant
                Me.Subtotals.Items.RemoveAt(searchIndex)
                Dim newSubtotal As String = FormatCurrency(tempSubtotal)
                Me.Subtotals.Items.Insert(searchIndex, newSubtotal)
                Session.Item("CurrentTotalVar") += tempSubtotal
                Me.CurrentTotal.Text = FormatCurrency(Session.Item("CurrentTotalVar"))
                Me.ShapeDropDownList.Text = vbNullString
                Me.ColorDropDownList.Text = vbNullString
                Me.QuantBox.Text = vbNullString
                Me.PriceSubtotal.Text = vbNullString
                'Me.Quantity_Mag.Value = vbNullString
                Session.Item("Quantity_Mag") = -1
                'Me.UnitCost_Mag.Value = vbNullString
                Session.Item("UnitCost_Mag") = -1
                Exit Sub
            Else
                searchIndex += 1
            End If
        Next

        Me.ComboKeyStorage.Items.Add(comboKey)
        Me.ShapesListBox.Items.Add(Me.ShapeDropDownList.Text)
        Me.ColorsListBox.Items.Add(Me.ColorDropDownList.Text)
        Me.QuantityListBox.Items.Add(Me.QuantBox.Text)
        Me.UnitPriceListBox.Items.Add(Me.UnitPriceBox.Text)
        Me.Subtotals.Items.Add(Me.PriceSubtotal.Text)

        Session.Item("CurrentTotalVar") += Session.Item("UnitCost_Mag") * Session.Item("Quantity_Mag")
        Me.CurrentTotal.Text = FormatCurrency(Session.Item("CurrentTotalVar"))

        Me.ShapeDropDownList.Text = vbNullString
        Me.ShapeDropDownList.Items.Insert(0, "")
        Me.ShapeDropDownList.SelectedIndex = 0
        Me.ColorDropDownList.Text = vbNullString
        Me.ColorDropDownList.Items.Insert(0, "")
        Me.ColorDropDownList.SelectedIndex = 0
        Me.WidgetDisplay_Cube.Visible = False
        Me.WidgetDisplay_Sphere.Visible = False
        Me.QuantBox.Text = vbNullString
        Me.PriceSubtotal.Text = vbNullString
        'Me.Quantity_Mag.Value = vbNullString
        Session.Item("Quantity_Mag") = -1
        'Me.UnitCost_Mag.Value = vbNullString
        Session.Item("UnitCost_Mag") = -1
    End Sub

    Protected Sub SubmitOrder_Click(sender As Object, e As EventArgs) Handles SubmitOrder.Click
        'only valid if ComboKeyStorage.length > 0, OrderId != null, CustomerId.isValid()


        'If (String.IsNullOrEmpty(Me.CustomerIdField.Text)) Then
        'Me.BadSubmitOrderFlag.Text = Globals.ErrorMsg_CustomerIdRequired
        'Me.BadSubmitOrderFlag.Visible = True
        'Exit Sub
        'End If

        If (Me.ComboKeyStorage.Items.Count > 0) Then
            Globals.DbConnection.Open()

            'log overall order
            Dim currentDateAndTime As String = DateAndTime.Today.ToString() '& " " & DateAndTime.Today
            Debug.Print(currentDateAndTime)
            Dim CustomerIdString As String = (CType(Session.Item("CustomerId"), String))
            Dim sqlLogOrderString As String = "INSERT INTO Orders (CustomerId, DateOfPlacement, DateOfShipping) Values(" _
                            & CustomerIdString & ", '" _
                            & currentDateAndTime & "', '" _
                            & currentDateAndTime & "')"
            Dim sqlLogOrderCommand As New OleDb.OleDbCommand(sqlLogOrderString)
            sqlLogOrderCommand.Connection = Globals.DbConnection
            sqlLogOrderCommand.ExecuteNonQuery()

            Dim sqlCheckOrderId As String = "SELECT MAX(OrderId) FROM Orders"

            Dim checkOrderIdCommand As New OleDb.OleDbCommand(sqlCheckOrderId)
            checkOrderIdCommand.Connection = Globals.DbConnection
            Dim tempOrderId = checkOrderIdCommand.ExecuteScalar()

            'checking if tempOrderId returned a number
            If (Not (tempOrderId Is Nothing)) Then
                Dim orderId As Integer = CType(tempOrderId, Integer)

                If (Not (
                    (Me.ShapesListBox.Items.Count = Me.ColorsListBox.Items.Count) AndAlso
                    (Me.ShapesListBox.Items.Count = Me.QuantityListBox.Items.Count) AndAlso
                    (Me.ShapesListBox.Items.Count = Me.UnitPriceListBox.Items.Count)
                    )) Then
                    Debug.Print("mismatched listboxes!")
                Else
                    Dim Shapes As IEnumerator = Me.ShapesListBox.Items.GetEnumerator()
                    Dim Colors As IEnumerator = Me.ColorsListBox.Items.GetEnumerator()
                    Dim Quantities As IEnumerator = Me.QuantityListBox.Items.GetEnumerator()
                    Dim Prices As IEnumerator = Me.UnitPriceListBox.Items.GetEnumerator()

                    'iterate out the column labels
                    'Shapes.MoveNext()
                    'Colors.MoveNext()
                    'Quantities.MoveNext()
                    'Prices.MoveNext()

                    While (Shapes.MoveNext)
                        Colors.MoveNext()
                        Quantities.MoveNext()
                        Prices.MoveNext()

                        '(OrderId, Shape, Color, Quantity, Price)
                        Dim sqlString_partials As String = "INSERT INTO PartialOrders VALUES ('" _
                            & orderId & "', '" _
                            & Shapes.Current.Text & "', '" _
                            & Colors.Current.Text & "', " _
                            & Quantities.Current.Text & ", " _
                            & Convert.ToString(CDec(Prices.Current.Text)) & ")"
                        Dim command As New OleDb.OleDbCommand(sqlString_partials)
                        command.Connection = Globals.DbConnection
                        command.ExecuteNonQuery()
                    End While

                    'clear and reset everything
                    Me.ShapeDropDownList.Text = vbNullString
                    Me.ShapeDropDownList.Items.Insert(0, "")
                    Me.ShapeDropDownList.SelectedIndex = 0
                    Me.ColorDropDownList.Text = vbNullString
                    Me.ColorDropDownList.Items.Insert(0, "")
                    Me.ColorDropDownList.SelectedIndex = 0
                    Me.WidgetDisplay_Cube.Visible = False
                    Me.WidgetDisplay_Sphere.Visible = False
                    Me.QuantBox.Text = vbNullString
                    Me.PriceSubtotal.Text = vbNullString
                    'Me.Quantity_Mag.Value = vbNullString
                    Session.Item("Quantity_Mag") = -1
                    'Me.UnitCost_Mag.Value = vbNullString
                    Session.Item("UnitCost_Mag") = -1
                    Me.UnitPriceBox.Text = vbNullString

                    Session.Item("CurrentTotal") = 0
                    Me.CurrentTotal.Text = FormatCurrency(0)
                    Session.Item("UnitCost_Mag") = 0
                    Session.Item("Quantity_Mag") = 0

                    Me.ShapeDropDownList.SelectedIndex = 0

                    Me.ComboKeyStorage.Items.Clear()
                    Me.ShapesListBox.Items.Clear()
                    Me.ColorsListBox.Items.Clear()
                    Me.QuantityListBox.Items.Clear()
                    Me.UnitPriceListBox.Items.Clear()
                    Me.Subtotals.Items.Clear()

                    'Me.ShapesListBox.Items.Add("SHAPE")
                    'Me.ColorsListBox.Items.Add("COLOR")
                    'Me.QuantityListBox.Items.Add("QUANTITY")
                    'Me.UnitPriceListBox.Items.Add("UNIT COST")

                    Me.SubmitOrder.Enabled = False
                End If
            End If
            Globals.DbConnection.Close()
        End If
    End Sub

    Protected Sub CurrentTotal_Init(sender As Object, e As EventArgs) Handles CurrentTotal.Init
        Session.Item("CurrentTotalVar") = 0
        Me.CurrentTotal.Text = FormatCurrency(0)
    End Sub
End Class