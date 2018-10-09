Public Class Admin
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        getFlaggedUsers()
        getNumQuestions()
        getNumReviews()
        getNumSuggestions()
        getNumPatients()
        getNumPractitioners()
    End Sub

    Private Sub getFlaggedUsers()
        Dim flaggedReviewersList As ArrayList = New ArrayList()
        'getting all the reviewers with a overall Score < 30%
        flaggedReviewersList = db.getAllFlaggedReviwUsers()

        'reset lable
        lblFlagUsers.Text = ""
        If flaggedReviewersList.Count > 0 Then
            For Each flaggedReviewer In flaggedReviewersList
                lblFlagUsers.Text &= "<tr> 
                                        <td>" & db.getPatientName(flaggedReviewer) & "</td>
                                        <td><a href='#'>Remove User</a></td>
                                      </tr>"
            Next flaggedReviewer
        Else
            lblFlagUsers.Text = "No Reviewers have their Overall Score for Reviews below 30%"
        End If

        Dim flaggedSuggestersList As ArrayList = New ArrayList()
        'getting all the reviewers with a overall Score < 30%
        flaggedSuggestersList = db.getAllFlaggedSuggestionUsers()

        Dim userType As String = "Unknown"

        'reset lable
        lblFlaggedSuggesters.Text = ""
        If flaggedReviewersList.Count > 0 Then
            For Each flaggedSuggester In flaggedSuggestersList
                If flaggedSuggester.TypeOfUser = "Pat" Then
                    userType = "Patient"
                ElseIf flaggedSuggester.TypeOfUser = "Prac" Then
                    userType = "Practitioner"
                End If

                lblFlaggedSuggesters.Text &= "<tr> 
                                        <td>" & db.getUsersName(flaggedSuggester.UserID) & "</td>
                                        <td>" & userType & "</td>
                                        <td><a href='#'>Remove User</a></td>
                                      </tr>"
            Next flaggedSuggester
        Else
            lblFlaggedSuggesters.Text = "No Suggesters have their Overall Score for Suggestions below 30%"
        End If
    End Sub

    Private Sub getNumQuestions()
        Dim Questions As ArrayList = New ArrayList
        Dim numQuestions As Integer = 0
        Questions = db.getQuestions()

        lblNumQuestions.Text = "<b>" & Questions.Count.ToString & "</b>"
    End Sub

    Private Sub getNumReviews()
        Dim numReviews As Integer = 0
        numReviews = db.getNumReviewsMade()

        lblNumReviews.Text = "<b>" & numReviews & "</b>"
    End Sub

    Private Sub getNumSuggestions()
        Dim numSuggestions As Integer = 0
        numSuggestions = db.getNumSuggestionsMade()

        lblNumSuggestions.Text = "<b>" & numSuggestions & "</b>"
    End Sub

    Private Sub getNumPatients()
        Dim numPatients As Integer = 0
        numPatients = db.getNumPatients("Pat")

        lblNumPatients.Text = "<b>" & numPatients & "</b>"
    End Sub

    Private Sub getNumPractitioners()
        Dim numPractitioners As Integer = 0
        numPractitioners = db.getNumPatients("Prac")

        lblNumPractitioners.Text = "<b>" & numPractitioners & "</b>"
    End Sub
End Class