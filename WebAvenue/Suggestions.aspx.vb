Imports System.Data
Imports System.Data.SqlClient
Public Class Suggestions
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim questionID As String = Request.QueryString("Question")

        If (questionID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        'load question onto the page
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM [Question] WHERE QuestionID='" & questionID & "';"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        'Dim reader As SqlDataReader
        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        'reader = command.ExecuteReader()

        ''displays question on the page
        'If reader.HasRows Then
        '    reader.Read()

        Dim Question As MediAvenueDatabase.Question = New MediAvenueDatabase.Question
        'load practitioners info onto the page
        'Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Practitioner] ON [User].UserID = [Practitioner].PractitionerID) WHERE [Practitioner].PractitionerID='" & ProfileID & "';"

        Question = db.getQuestion(questionID)

        lblQuestionTopic.Text = Question.Question
        lblPatientName.Text = db.getUsersName(Question.PatientID)
        lblDescription.Text = Question.Description
        lblDateTime.Text = Question.DateUploaded & " @ " & Question.TimeUploaded
        'End If

        'reader.Close()
        'connection.Close()
        'command.Dispose()
        'connection.Dispose()

        'Load Suggestions for the question
        loadSuggestions(questionID)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub loadSuggestions(ByVal questionID As Integer)
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Suggestion] ON [User].UserID = [Suggestion].UserID) WHERE QuestionID=" & questionID & ";"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        'Dim reader As SqlDataReader

        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        'reader = command.ExecuteReader()
        Dim SuggestionsList As ArrayList = New ArrayList()

        SuggestionsList = db.getSuggestions(questionID)
        ''to reset the lable 
        lblSuggestions.Text = ""
        If SuggestionsList.Count > 0 Then
            For Each Suggestion In SuggestionsList
                If Suggestion.TypeOfUser = "Pat" Then
                    lblSuggestions.Text &= "<li Class='list-group-item'>"
                    lblSuggestions.Text &= "<div Class='card-body'>"
                    'edit this line to show the star rating for this question
                    lblSuggestions.Text &= "<h5 Class='card-title'><a href='PatientProfile.aspx?Profile=" & Suggestion.UserID & "'>" & db.getUsersName(Suggestion.UserID) & "</a> <span class='badge badge-dark'>5 star</span></h5>"
                    lblSuggestions.Text &= "<p Class='card-text'>" & Suggestion.Suggestion & "</p>"
                    lblSuggestions.Text &= "<div Class='container'>"
                    lblSuggestions.Text &= "<div Class='row'>"
                    lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    'need to make this a dynamic button
                    lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
                    lblSuggestions.Text &= "</div>"
                    lblSuggestions.Text &= "<div Class='col col-lg-8'>"
                    lblSuggestions.Text &= "</div>"
                    lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    'need to make this a dynamic button
                    lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a>"
                    lblSuggestions.Text &= "</div></div></div></div></li>"
                ElseIf Suggestion.TypeOfUser = "Prac" Then
                    lblSuggestions.Text &= "<li Class='list-group-item'>"
                    lblSuggestions.Text &= "<div Class='card-body'>"
                    'edit this line to show the star rating for this question
                    lblSuggestions.Text &= "<h5 Class='card-title'><a href='PractitionerProfile.aspx?Profile=" & Suggestion.UserID & "'>" & db.getUsersName(Suggestion.UserID) & "</a> <span class='badge badge-dark'>5 star</span></h5>"
                    lblSuggestions.Text &= "<p Class='card-text'>" & Suggestion.Suggestion & "</p>"
                    lblSuggestions.Text &= "<div Class='container'>"
                    lblSuggestions.Text &= "<div Class='row'>"
                    lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    'need to make this a dynamic button
                    lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
                    lblSuggestions.Text &= "</div>"
                    lblSuggestions.Text &= "<div Class='col col-lg-8'>"
                    lblSuggestions.Text &= "</div>"
                    lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    'need to make this a dynamic button
                    lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a>"
                    lblSuggestions.Text &= "</div></div></div></div></li>"
                End If
            Next Suggestion
        Else
            lblSuggestions.Text &= "<li Class='list-group-item'>"
            lblSuggestions.Text &= "<div Class='card-body'>"
            lblSuggestions.Text &= "<p Class='card-text'>No Suggestions made for this Question</p>"
            lblSuggestions.Text &= "</div></li>"
        End If

        'reader.Close()
        'connection.Close()
        'command.Dispose()
        'connection.Dispose()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim userID As String = Session("UserId")

        Dim questionID As String = Request.QueryString("Question")
        If (questionID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim regDate As Date = Date.Now()
            Dim TodaysDate As String = regDate.ToString("dd\/MM\/yyyy")
            Dim TodaysTime As String = regDate.ToString("HH:mm:ss")

            Dim suggestion As String = txtAddSuggestion.Text

            'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            'Dim commandString As String = "INSERT INTO [Suggestion] (UserID,QuestionID,Suggestion,Date,Time,NumLikes,NumDislikes,Remove,OverallScore,OriginalPoint,ExtraPoint) VALUES ('" & userID & "','" & questionID & "','" & suggestion & "','" & TodaysDate & "','" & TodaysTime & "','0','0','N','0','0','0');"
            'Dim connection As SqlConnection = New SqlConnection(connectionString)
            'Dim command As SqlCommand = New SqlCommand()

            'command.Connection = connection
            'command.CommandType = CommandType.Text
            'command.CommandText = commandString

            'connection.Open()
            'command.ExecuteNonQuery()

            'connection.Dispose()
            'command.Dispose()
            'connection.Close()

            db.addSuggestion(userID, questionID, suggestion, TodaysDate, TodaysTime)

            'update Question table to specify there is suggestions
            db.updateNumSuggestions(questionID)

            'might need messages to say it went through

            'for the page to reload
            Me.Page_Load(sender, e)
        End If
    End Sub
End Class