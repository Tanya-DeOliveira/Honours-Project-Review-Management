Public Class LikeDislikeSuggestion
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim SuggestionID As String = Request.QueryString("Suggestion")

        If (SuggestionID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        'load Question and suggestion on page

        'Dim commandString As String = "SELECT * FROM [Suggestion] WHERE SuggestionID=" & SuggestionID & ";"

        Dim Suggestion As MediAvenueDatabase.Suggestion = New MediAvenueDatabase.Suggestion

        'get suggestion of question
        Suggestion = db.getSuggestion(SuggestionID)

        lblUserName.Text = db.getUsersName(Suggestion.UserID)
        lblSuggestion.Text = Suggestion.Suggestion
        Dim QuestionID As Integer = Suggestion.QuestionID
        'If reader.HasRows Then
        'End If

        'get Question
        Dim Question As MediAvenueDatabase.Question = New MediAvenueDatabase.Question

        Question = db.getQuestion(QuestionID)

        lblPatientName.Text = db.getUsersName(Question.PatientID)
        lblQuestionTopic.Text = Question.Question
        lblDescription.Text = Question.Description
        lblDateTime.Text = Question.DateUploaded & " @ " & Question.TimeUploaded

    End Sub

    Private Sub updateOverallScore(ByVal SuggestionID As Integer)
        Dim numLikes As Integer = db.getNumSuggestionLikes(SuggestionID)
        Dim numDislikes As Integer = db.getNumSuggestionDislikes(SuggestionID)
        Dim OverallScore As Integer = db.getOverallSuggestionScore(SuggestionID)
        Dim extraPoint As Integer = db.getExtraSuggestionPoint(SuggestionID)

        If numLikes > numDislikes Then
            db.updateExtraSuggestionPoint(SuggestionID, 1)

            If extraPoint = 0 Then
                OverallScore = OverallScore + 1
            End If

            'Dim commandString As String = "UPDATE [Suggestion] SET OverallScore = '" & OverallScore & "' WHERE SuggestionID=" & SuggestionID & ";"

            db.updateOverallSuggestionScore(SuggestionID, OverallScore)
        Else
            'num likes < num dislikes
            db.updateExtraSuggestionPoint(SuggestionID, 0)
            'they loose the point
            If extraPoint = 1 Then
                OverallScore = OverallScore - 1
            End If

            'Dim commandString As String = "UPDATE [Suggestion] SET OverallScore = '" & OverallScore & "' WHERE SuggestionID=" & SuggestionID & ";"

            db.updateOverallSuggestionScore(SuggestionID, OverallScore)
        End If
    End Sub

    Protected Sub btnLikeSugg_Click(sender As Object, e As EventArgs) Handles btnLikeSugg.Click
        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim SuggestionID As String = Request.QueryString("Suggestion")

            If (SuggestionID Is Nothing) Then
                Response.Redirect("Logout.aspx")
            End If

            Dim numLikes As Integer = db.getNumSuggestionLikes(SuggestionID)
            numLikes = numLikes + 1

            'Dim commandString As String = "UPDATE [Suggestion] SET NumLikes = '" & numLikes & "' WHERE SuggestionID=" & SuggestionID & ";"

            db.addSuggestionLike(SuggestionID, numLikes)
            lblMessage.Text = "You Have Liked This Suggestion"

            updateOverallScore(SuggestionID)
        End If
    End Sub

    Protected Sub btnDislikeSugg_Click(sender As Object, e As EventArgs) Handles btnDislikeSugg.Click
        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim SuggestionID As String = Request.QueryString("Suggestion")

            If (SuggestionID Is Nothing) Then
                Response.Redirect("Logout.aspx")
            End If

            Dim numDislikes As Integer = db.getNumSuggestionDislikes(SuggestionID)
            numDislikes = numDislikes + 1

            'Dim commandString As String = "UPDATE [Suggestion] SET NumDislikes = '" & numDislikes & "' WHERE SuggestionID=" & SuggestionID & ";"
            db.addSuggestionDislike(SuggestionID, numDislikes)

            lblMessage.Text = "You Have Disliked This Suggestion"

            updateOverallScore(SuggestionID)
        End If
    End Sub
End Class