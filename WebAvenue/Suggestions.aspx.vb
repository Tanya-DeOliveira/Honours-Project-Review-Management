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


        'Dim commandString As String = "SELECT * FROM [Question] WHERE QuestionID='" & questionID & "';"

        ''displays question on the page
        'If reader.HasRows Then
        '    reader.Read()
        'End If
        Dim Question As MediAvenueDatabase.Question = New MediAvenueDatabase.Question

        'load question onto the page
        Question = db.getQuestion(questionID)

        lblQuestionTopic.Text = Question.Question
        lblPatientName.Text = db.getUsersName(Question.PatientID)
        lblDescription.Text = Question.Description
        lblDateTime.Text = Question.DateUploaded & " @ " & Question.TimeUploaded

        'Load Suggestions for the question
        loadSuggestions(questionID)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub loadSuggestions(ByVal questionID As Integer)
        'Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Suggestion] ON [User].UserID = [Suggestion].UserID) WHERE QuestionID=" & questionID & ";"

        'loads suggestions of question onto page
        Dim SuggestionsList As ArrayList = New ArrayList()

        SuggestionsList = db.getSuggestions(questionID)
        'to reset the lable 
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
                    lblSuggestions.Text &= "<div Class='col col-lg-12'> Overall Score: " & Suggestion.OverallScore & "<br/>"
                    lblSuggestions.Text &= "<a href ='LikeDislikeSuggestion.aspx?Suggestion=" & Suggestion.SuggestionID & "'>Like/Dislike Review</a></div>"
                    'lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    ''need to make this a dynamic button
                    'lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
                    'lblSuggestions.Text &= "</div>"
                    'lblSuggestions.Text &= "<div Class='col col-lg-8'>"
                    'lblSuggestions.Text &= "</div>"
                    'lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    ''need to make this a dynamic button
                    'lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a>"
                    lblSuggestions.Text &= "</div></div></div></li>"
                ElseIf Suggestion.TypeOfUser = "Prac" Then
                    lblSuggestions.Text &= "<li Class='list-group-item'>"
                    lblSuggestions.Text &= "<div Class='card-body'>"
                    'edit this line to show the star rating for this question
                    lblSuggestions.Text &= "<h5 Class='card-title'><a href='PractitionerProfile.aspx?Profile=" & Suggestion.UserID & "'>" & db.getUsersName(Suggestion.UserID) & "</a> <span class='badge badge-dark'>5 star</span></h5>"
                    lblSuggestions.Text &= "<p Class='card-text'>" & Suggestion.Suggestion & "</p>"
                    lblSuggestions.Text &= "<div Class='container'>"
                    lblSuggestions.Text &= "<div Class='row'>"
                    lblSuggestions.Text &= "<div Class='col col-lg-12'> Overall Score: " & Suggestion.OverallScore & "<br/>"
                    lblSuggestions.Text &= "<a href ='LikeDislikeSuggestion.aspx?Suggestion=" & Suggestion.SuggestionID & "'>Like/Dislike Review</a></div>"
                    'lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    ''need to make this a dynamic button
                    'lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
                    'lblSuggestions.Text &= "</div>"
                    'lblSuggestions.Text &= "<div Class='col col-lg-8'>"
                    'lblSuggestions.Text &= "</div>"
                    'lblSuggestions.Text &= "<div Class='col col-lg-2'>"
                    ''need to make this a dynamic button
                    'lblSuggestions.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a></div>"
                    lblSuggestions.Text &= "</div></div></div></li>"
                End If
            Next Suggestion
        Else
            lblSuggestions.Text &= "<li Class='list-group-item'>"
            lblSuggestions.Text &= "<div Class='card-body'>"
            lblSuggestions.Text &= "<p Class='card-text'>No Suggestions made for this Question</p>"
            lblSuggestions.Text &= "</div></li>"
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'adds a suggestion to question
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim SuggestionOriginalScore As Integer = 0
        Dim suggestionArray() As String

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

            'need to calculate suggestion score
            ''reviews word count > 200 - 3 points
            ''likes are > dislikes - 1 point - get it after posting - adressed in another page
            ''doctor has > 10 years of experiance - 1 point/ suggester has a master suggester badge - 1 point

            'getting and calculating word count
            suggestionArray = suggestion.Split(" ")
            If suggestionArray.Length >= 200 Then
                SuggestionOriginalScore = SuggestionOriginalScore + 3
            End If
            'getting doctors experiance
            If userType = "Prac" Then
                'doctor has > 10 years of experiance - 1 point
                Dim yearsOfExperiance As Integer = 0
                Dim Practitioner As MediAvenueDatabase.Practitioner = New MediAvenueDatabase.Practitioner
                'getting yearsOfExperiance
                Practitioner = db.getPracProfileDetails(userID)

                yearsOfExperiance = Practitioner.YearsOfExperiance

                If yearsOfExperiance >= 10 Then
                    SuggestionOriginalScore = SuggestionOriginalScore + 1
                End If
            ElseIf userType = "Pat" Then
                'suggester has a master suggester badge - 1 point
                Dim Patient As MediAvenueDatabase.Patient = New MediAvenueDatabase.Patient
                'getting yearsOfExperiance
                Patient = db.getBadges(userID)

                If Patient.MasterSuggester = "Y" Then
                    'have the suggester badge
                    SuggestionOriginalScore = SuggestionOriginalScore + 1
                End If
            End If

            'Dim commandString As String = "INSERT INTO [Suggestion] (UserID,QuestionID,Suggestion,Date,Time,NumLikes,NumDislikes,Remove,OverallScore,OriginalPoint,ExtraPoint) VALUES ('" & userID & "','" & questionID & "','" & suggestion & "','" & TodaysDate & "','" & TodaysTime & "','0','0','N','0','0','0');"

            'add suggest
            db.addSuggestion(userID, questionID, suggestion, TodaysDate, TodaysTime, SuggestionOriginalScore)

            'update Question table to specify there is suggestions
            db.updateNumSuggestions(questionID)

            'updating the num suggestions a user has made
            'got to get the num reviews 
            Dim numSuggestions As Integer = 0
            numSuggestions = db.getNumSuggestionsUserMade(userID)

            'updating the numReviews for the user 
            db.updateNumSuggestionsUserMade(userID, numSuggestions)

            'for the page to reload
            Me.Page_Load(sender, e)
        End If
    End Sub
End Class