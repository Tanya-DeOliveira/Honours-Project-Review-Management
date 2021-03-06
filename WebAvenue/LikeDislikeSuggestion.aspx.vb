﻿Public Class LikeDislikeSuggestion
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

    'this is to update the overall Score of the suggestion based on the number of likes/dislikes 
    'it gets. So it will add a extra point to suggestion or not
    Private Sub updateOverallScore(ByVal SuggestionID As Integer)
        Dim numLikes As Integer = db.getNumSuggestionLikes(SuggestionID)
        Dim numDislikes As Integer = db.getNumSuggestionDislikes(SuggestionID)
        Dim OverallScore As Integer = db.getOverallSuggestionScore(SuggestionID)
        Dim extraPoint As Integer = db.getExtraSuggestionPoint(SuggestionID)

        If numLikes > numDislikes Then
            db.updateExtraSuggestionPoint(SuggestionID, 1)

            If extraPoint = 0 Then
                OverallScore = OverallScore + 1
            ElseIf extraPoint = -1 Then
                'get a points back
                OverallScore = OverallScore + 1
            End If

            'Dim commandString As String = "UPDATE [Suggestion] SET OverallScore = '" & OverallScore & "' WHERE SuggestionID=" & SuggestionID & ";"

            db.updateOverallSuggestionScore(SuggestionID, OverallScore)
        Else
            'num likes < num dislikes

            'they loose the point
            If extraPoint = 1 Then
                db.updateExtraSuggestionPoint(SuggestionID, 0)
                OverallScore = OverallScore - 1
            ElseIf extraPoint = -1 Then
                'do nothing
                OverallScore = OverallScore
            Else
                'to say they already lost a point
                db.updateExtraSuggestionPoint(SuggestionID, -1)
                'or they loose point overall
                If OverallScore = 0 Then
                    'do nothing because they already reached 0 for overall score
                    OverallScore = 0
                Else
                    OverallScore = OverallScore - 1
                End If
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

            'getting suggestersID to update the Overall Suggestion Score
            Dim suggesterID As Integer = db.getSuggesterID(SuggestionID)

            'need to update users overall score for suggestions for badges
            calculateSuggestionerOverallScore(suggesterID)
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

            'getting suggestersID to update the Overall Suggestion Score
            Dim suggesterID As Integer = db.getSuggesterID(SuggestionID)

            'need to update users overall score for suggestions for badges
            calculateSuggestionerOverallScore(suggesterID)
        End If
    End Sub

    'need to get overall score from every suggestion the 'user that made the suggestion' made
    Public Sub calculateSuggestionerOverallScore(ByVal userID As Integer)
        Dim UsersOverallScoresList As ArrayList = New ArrayList()
        Dim TotalScore As Integer = 0
        Dim OverallSuggestionScore As Double = 0.0
        Dim userType As String = Session("UserType")

        'getting all the review scores the patient has obtained
        UsersOverallScoresList = db.getOverallSuggestionScoreDetails(userID)

        If UsersOverallScoresList.Count > 0 Then
            For Each SuggestionOverallScore In UsersOverallScoresList
                'totalling up all the scores together
                TotalScore = TotalScore + SuggestionOverallScore.OverallScore
            Next SuggestionOverallScore
            'getting avearage
            OverallSuggestionScore = (TotalScore / (UsersOverallScoresList.Count * 5)) * 100

            'store score in user Tables
            db.updateOverallSuggestionScoreForUser(userID, OverallSuggestionScore)

            If userType = "Pat" Then
                'need to check if patient qualify for a badge or not
                checkIfQualifyForSuggesterBadge(userID, OverallSuggestionScore)
            End If
        End If
    End Sub

    Public Sub checkIfQualifyForSuggesterBadge(ByVal userID As Integer, ByVal OverallSuggestionScore As Double)
        'need to get overallReviewer score
        Dim limitToQualify As Double = 90

        'Overall Score works with percentages
        If OverallSuggestionScore >= limitToQualify Then
            'qalifies for a badge
            db.updateSuggesterBadgeStatus(userID, "Y")
        Else
            'doesnt qalifies for a badge
            db.updateSuggesterBadgeStatus(userID, "N")
        End If
    End Sub
End Class