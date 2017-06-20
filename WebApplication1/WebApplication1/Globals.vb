Public Class Globals

    Public Shared ConnectionString_DataSource As String = "Data Source='H:\Users\Thrawnboo\Documents\School Stuff\ESC\Spring 2017\Database Design\EscDatabasesFinalProject.accdb'"
    Public Shared ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;" & ConnectionString_DataSource
    Public Shared DbConnection As New OleDb.OleDbConnection(ConnectionString)
    Public Shared ErrorMsg_CustomerIdRequired As String = "Customer ID required!"
    Public Shared BasePath = "C:\Users\MechaGent\Documents\School Stuff\2017 - DatabaseDesign\Website12"
End Class
