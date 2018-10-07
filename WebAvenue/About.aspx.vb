Imports System.Data
Imports System.Data.SqlClient
Public Class About
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim ReviewID As String = Request.QueryString("Review")

        If (ReviewID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        'load practitioners review on page
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "SELECT * FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            lblPracName.Text = getUsersName(reader("PractitionerID"))
            lblPatientName.Text = getUsersName(reader("PatientID"))
            lblReview.Text = reader("Review")
            lblPracRating.Text = reader("PracRating")
        End If

        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()

        updateOverallScore(ReviewID)
    End Sub

    Private Function getUsersName(ByVal UserID As Integer)
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "SELECT [User].Name, [User].Surname FROM [User] WHERE UserID=" & UserID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        Dim name As String = ""
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If (reader.HasRows) Then
            reader.Read()
            name = reader("Name") & " " & reader("Surname")
        End If

        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()

        Return name
    End Function

    Protected Sub btnDisLike_Click(sender As Object, e As EventArgs) Handles btnDisLike.Click
        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim ReviewID As String = Request.QueryString("Review")

            If (ReviewID Is Nothing) Then
                Response.Redirect("Logout.aspx")
            End If

            Dim numDislikes As Integer = getNumDislikes(ReviewID)
            numDislikes = numDislikes + 1

            Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            Dim commandString As String = "UPDATE [Review] SET NumDislikes = '" & numDislikes & "' WHERE ReviewID=" & ReviewID & ";"
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand()

            command.Connection = connection
            command.CommandType = CommandType.Text
            command.CommandText = commandString

            connection.Open()
            command.ExecuteNonQuery()

            lblMessage.Text = "You Have Disliked This Review"

            connection.Close()
            command.Dispose()
            connection.Dispose()
        End If
    End Sub

    Protected Sub btnLikeSec_Click(sender As Object, e As EventArgs) Handles btnLikeSec.Click
        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim ReviewID As String = Request.QueryString("Review")

            If (ReviewID Is Nothing) Then
                Response.Redirect("Logout.aspx")
            End If

            Dim numLikes As Integer = getLikes(ReviewID)
            numLikes = numLikes + 1

            Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            Dim commandString As String = "UPDATE [Review] SET NumLikes = '" & numLikes & "' WHERE ReviewID=" & ReviewID & ";"
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand()

            command.Connection = connection
            command.CommandType = CommandType.Text
            command.CommandText = commandString

            connection.Open()
            command.ExecuteNonQuery()

            lblMessage.Text = "You Have Liked This Review"

            connection.Close()
            command.Dispose()
            connection.Dispose()
        End If
    End Sub

    Private Function getNumDislikes(ByVal ReviewID As Integer) As Integer
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "Select NumDislikes FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim numDislikes As Integer
        If reader.HasRows Then
            reader.Read()
            numDislikes = CInt(reader("NumDislikes"))
        End If

        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()
        Return numDislikes
    End Function

    Private Function getLikes(ByVal ReviewID As Integer) As Integer
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "Select NumLikes FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim numLikes As Integer
        If reader.HasRows Then
            reader.Read()
            numLikes = CInt(reader("NumLikes"))
        End If

        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()
        Return numLikes
    End Function

    Private Sub updateOverallScore(ByVal ReviewID As Integer)
        Dim numLikes As Integer = getLikes(ReviewID)
        Dim numDislikes As Integer = getNumDislikes(ReviewID)
        Dim OverallScore As Integer = getOverallScore(ReviewID)
        Dim extraPoint As Integer = getExtra(ReviewID)

        If numLikes > numDislikes Then
            updateExtraPoint(ReviewID, 1)

            If extraPoint = 0 Then
                OverallScore = OverallScore + 1
            End If

            Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            Dim commandString As String = "UPDATE [Review] SET OverallScore = '" & OverallScore & "' WHERE ReviewID=" & ReviewID & ";"
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand()

            command.Connection = connection
            command.CommandType = CommandType.Text
            command.CommandText = commandString

            connection.Open()
            command.ExecuteNonQuery()

            connection.Close()
            command.Dispose()
            connection.Dispose()
        Else
            'num likes < num dislikes
            updateExtraPoint(ReviewID, 0)
            'they loose the point
            If extraPoint = 1 Then
                OverallScore = OverallScore - 1
            End If

            Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            Dim commandString As String = "UPDATE [Review] SET OverallScore = '" & OverallScore & "' WHERE ReviewID=" & ReviewID & ";"
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand()

            command.Connection = connection
            command.CommandType = CommandType.Text
            command.CommandText = commandString

            connection.Open()
            command.ExecuteNonQuery()

            connection.Close()
            command.Dispose()
            connection.Dispose()
        End If
    End Sub

    Private Function getOverallScore(ByVal ReviewID As Integer) As Integer
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "Select OverallScore FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim OverallScore As Integer
        If reader.HasRows Then
            reader.Read()
            OverallScore = CInt(reader("OverallScore"))
        End If

        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()
        Return OverallScore
    End Function

    Private Sub updateExtraPoint(ByVal ReviewID As Integer, ByVal extraPoint As Integer)
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "UPDATE [Review] SET ExtraPoint = '" & extraPoint & "' WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Private Function getExtra(ByVal ReviewID As Integer) As Integer
        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "Select ExtraPoint FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim extraPoint As Integer
        If reader.HasRows Then
            reader.Read()
            extraPoint = CInt(reader("ExtraPoint"))
        End If

        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()
        Return extraPoint
    End Function
End Class