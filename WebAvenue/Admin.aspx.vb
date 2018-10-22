Public Class Admin
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        getFlaggedUsers()
        getTopUsers()
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
                                        <td>" & db.getPatientName(flaggedReviewer.PatientID) & "</td>
                                        <td>" & flaggedReviewer.OverallReviewScore & "%</td>
                                        <td>" & flaggedReviewer.NumReviewsMade & "</td>
                                        <td><a href='#'>Remove User</a></td>
                                      </tr>"
            Next flaggedReviewer
        Else
            lblFlagUsers.Text = "<tr> 
                                        <td>No Reviewers have their Overall Score for Reviews below 30%</td>
                                      </tr>"
        End If

        Dim flaggedSuggestersList As ArrayList = New ArrayList()
        'getting all the reviewers with a overall Score < 30%
        flaggedSuggestersList = db.getAllFlaggedSuggestionUsers()

        Dim userType As String = "Unknown"

        'reset lable
        lblFlaggedSuggesters.Text = ""
        If flaggedSuggestersList.Count > 0 Then
            For Each flaggedSuggester In flaggedSuggestersList
                If flaggedSuggester.TypeOfUser = "Pat" Then
                    userType = "Patient"
                ElseIf flaggedSuggester.TypeOfUser = "Prac" Then
                    userType = "Practitioner"
                End If

                lblFlaggedSuggesters.Text &= "<tr> 
                                        <td>" & db.getUsersName(flaggedSuggester.UserID) & "</td>
                                        <td>" & userType & "</td>
                                        <td>" & flaggedSuggester.OverallSuggestionScore & "%</td>
                                        <td>" & flaggedSuggester.NumSuggestionsMade & "</td>
                                        <td><a href='#'>Remove User</a></td>
                                      </tr>"
            Next flaggedSuggester
        Else
            lblFlaggedSuggesters.Text = "<tr> 
                                        <td>No Suggesters have their Overall Score for Suggestions below 30%</td>
                                      </tr>"
        End If
    End Sub

    Private Sub getTopUsers()
        Dim TopReviewersList As ArrayList = New ArrayList()
        'getting all the reviewers with a review badge
        TopReviewersList = db.getAllTopReviwUsers()

        'need to get to see if the user already has rewards
        Dim RewardList As ArrayList = New ArrayList()
        Dim lastUserReward As MediAvenueDatabase.Reward = New MediAvenueDatabase.Reward

        'reset lable
        lblTopReviewers.Text = ""
        If TopReviewersList.Count > 0 Then
            For Each TopReviewer In TopReviewersList
                'reteieving to see if user has rewards
                RewardList = db.getUserRewards(TopReviewer.PatientID)

                If RewardList.Count > 0 Then
                    'user has rewards
                    'getting last iteam user has got
                    lastUserReward = RewardList.Item(RewardList.Count - 1)

                    lblTopReviewers.Text &= "<tr> 
                                        <td>" & db.getPatientName(TopReviewer.PatientID) & "</td>
                                        <td>" & TopReviewer.OverallReviewScore & "%</td>
                                        <td>" & TopReviewer.NumReviewsMade & "</td>
                                        <td> <a href='GiveReward.aspx?UserID=" & TopReviewer.PatientID & "'>Reward User</a></td>
                                        <td> " & lastUserReward.DateUploaded & "</td>
                                        </tr>"
                Else
                    'user doesnt have rewards
                    lblTopReviewers.Text &= "<tr> 
                                        <td>" & db.getPatientName(TopReviewer.PatientID) & "</td>
                                        <td>" & TopReviewer.OverallReviewScore & "%</td>
                                        <td>" & TopReviewer.NumReviewsMade & "</td>
                                        <td> <a href='GiveReward.aspx?UserID=" & TopReviewer.PatientID & "'>Reward User</a></td>
                                        <td> No Rewards Recieved </td>
                                        </tr>"
                End If
            Next TopReviewer
        Else
            'in order to qulaify for a badeg, user must get a overall score of 90% for reviews
            lblTopReviewers.Text = "<tr> 
                                        <td>No Reviewers have their Overall Score for Reviews above 90%</td>
                                      </tr>"
        End If

        Dim TopSuggestersList As ArrayList = New ArrayList()
        'getting all the reviewers with a overall Score < 30%
        TopSuggestersList = db.getAllTopSuggestionUsers()

        Dim userType As String = "Unknown"

        'reset lable
        lblTopSuggesters.Text = ""
        If TopSuggestersList.Count > 0 Then
            For Each TopSuggester In TopSuggestersList
                If TopSuggester.TypeOfUser = "Pat" Then
                    userType = "Patient"
                End If

                'reteieving to see if user has rewards
                RewardList = db.getUserRewards(TopSuggester.UserID)

                If RewardList.Count > 0 Then
                    'user has rewards
                    'getting last iteam user has got
                    lastUserReward = RewardList.Item(RewardList.Count - 1)

                    lblTopSuggesters.Text &= "<tr> 
                                        <td>" & db.getUsersName(TopSuggester.UserID) & "</td>
                                        <td>" & userType & "</td>
                                        <td>" & TopSuggester.OverallSuggestionScore & "%</td>
                                        <td>" & TopSuggester.NumSuggestionsMade & "</td>
                                        <td><a href='GiveReward.aspx?UserID=" & TopSuggester.UserID & "'>Reward User</a></td>
                                        <td> " & lastUserReward.DateUploaded & "</td>
                                      </tr>"
                Else
                    'user doesnt have rewards
                    lblTopSuggesters.Text &= "<tr> 
                                        <td>" & db.getUsersName(TopSuggester.UserID) & "</td>
                                        <td>" & userType & "</td>
                                        <td>" & TopSuggester.OverallSuggestionScore & "%</td>
                                        <td>" & TopSuggester.NumSuggestionsMade & "</td>
                                        <td><a href='GiveReward.aspx?UserID=" & TopSuggester.UserID & "'>Reward User</a></td>
                                        <td> No Rewards Recieved</td>
                                      </tr>"
                End If
            Next TopSuggester
        Else
            'in order to qulaify for a badeg, user must get a overall score of 90% for suggestions
            lblTopSuggesters.Text = "<tr> 
                                        <td>No Users have their Overall Score for Suggestions above 90%</td>
                                     </tr>"
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