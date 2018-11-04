Public Class QuestionsAsked
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userID As String = Session("UserId")

        Dim QuestionsAsked As ArrayList = New ArrayList()

        QuestionsAsked = db.getQuestionsAsked(userID)
        'displays all the questions the user asked on the page
        'reset lable
        lblDisplayQuestionsAsked.Text = ""
        If QuestionsAsked.Count > 0 Then
            For Each Question In QuestionsAsked
                'these are just the rest of the questions
                lblDisplayQuestionsAsked.Text &= "<div Class='card border-secondary mb-3'>"
                lblDisplayQuestionsAsked.Text &= "<div Class='card-header'><b>" & Question.Question & "</b></div>"
                lblDisplayQuestionsAsked.Text &= "<div Class='card-body text-secondary'>"
                lblDisplayQuestionsAsked.Text &= "<p Class='card-text'>" & Question.Description & "<br/>"
                'use popularity as the number of suggestions
                lblDisplayQuestionsAsked.Text &= "<a href = 'Suggestions.aspx?Question=" & Question.QuestionID & "' Class='lk badge badge-dark'>View Suggestions <span Class='badge badge-dark'>" & Question.Popularity & " Suggestions</span></a></p>"
                lblDisplayQuestionsAsked.Text &= "<p Class='card-text'><small class='text-muted'>" & Question.DateUploaded & " @ " & Question.TimeUploaded & "</small></p></div></div>"
            Next Question
        Else
            'no questions to display
            lblDisplayQuestionsAsked.Text &= "<div Class='card border-secondary mb-3'>"
            lblDisplayQuestionsAsked.Text &= "<div Class='card-body text-secondary'>"
            lblDisplayQuestionsAsked.Text &= "<p Class='card-text'>No Questions to Display<br/>"
            lblDisplayQuestionsAsked.Text &= "</div></div><br/>"
        End If
    End Sub

End Class