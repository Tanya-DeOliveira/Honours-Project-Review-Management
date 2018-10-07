Imports System.Data
Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Protected Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString
        Dim commandString As String = "SELECT * FROM [User] WHERE Username = @username AND Password = @password;"

        Dim connection As New SqlConnection(connectionString)
        Dim command As New SqlCommand

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@username", username)
        command.Parameters.AddWithValue("@password", password)

        connection.Open()
        command.ExecuteNonQuery()

        Dim reader As SqlDataReader
        reader = command.ExecuteReader(CommandBehavior.CloseConnection)
        If reader.HasRows Then
            While reader.Read
                Session("UserId") = reader("UserId").ToString
                Session("Username") = reader("Username").ToString
                Session("UserType") = reader("TypeOfUser").ToString
            End While

            reader.Close()
            connection.Close()
            command.Dispose()
            connection.Dispose()
            'from the ussername on the dahsboard get its type and then show certian things to its type
            'Response.Redirect("DashBoard.aspx?Uname=" + uname)
            If Session("UserType") = "A" Then
                Response.Redirect("Reports.aspx")
            Else
                Response.Redirect("Dashboard.aspx")
            End If
        Else
            lblErr.Visible = True
        End If
    End Sub
End Class