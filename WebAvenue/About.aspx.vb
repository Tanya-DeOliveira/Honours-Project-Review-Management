Imports System.Data
Imports System.Data.SqlClient
Public Class About
    Inherits Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim ReviewID As String = Request.QueryString("Review")

        If (ReviewID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        'load practitioners review on page

        'Dim commandString As String = "SELECT * FROM [Review] WHERE ReviewID=" & ReviewID & ";"

        Dim Review As MediAvenueDatabase.Review = New MediAvenueDatabase.Review

        Review = db.getPracReview(ReviewID)

        lblPracName.Text = db.getUsersName(Review.PractitionerID)
        lblPatientName.Text = db.getUsersName(Review.PatientID)
        lblReview.Text = Review.Review
        lblPracRating.Text = Review.PracRating
        'If reader.HasRows Then
        'End If
    End Sub

    Protected Sub btnDisLike_Click(sender As Object, e As EventArgs) Handles btnDisLike.Click
        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim ReviewID As String = Request.QueryString("Review")

            If (ReviewID Is Nothing) Then
                Response.Redirect("Logout.aspx")
            End If

            Dim numDislikes As Integer = db.getNumReviewDislikes(ReviewID)
            numDislikes = numDislikes + 1

            'Dim commandString As String = "UPDATE [Review] SET NumDislikes = '" & numDislikes & "' WHERE ReviewID=" & ReviewID & ";"
            db.addReviewDislike(ReviewID, numDislikes)

            lblMessage.Text = "You Have Disliked This Review"

            updateOverallScore(ReviewID)
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

            Dim numLikes As Integer = db.getNumReviewLikes(ReviewID)
            numLikes = numLikes + 1

            'Dim commandString As String = "UPDATE [Review] SET NumLikes = '" & numLikes & "' WHERE ReviewID=" & ReviewID & ";"

            db.addReviewLike(ReviewID, numLikes)
            lblMessage.Text = "You Have Liked This Review"

            updateOverallScore(ReviewID)
        End If
    End Sub

    'this is to update the overall Score of the review based on the number of likes/dislikes 
    'it gets. So it will add a extra point to review or not
    Private Sub updateOverallScore(ByVal ReviewID As Integer)
        'get the reviewersID via the ReviewID

        Dim numLikes As Integer = db.getNumReviewLikes(ReviewID)
        Dim numDislikes As Integer = db.getNumReviewDislikes(ReviewID)
        Dim OverallScore As Integer = db.getOverallReviewScore(ReviewID)
        Dim extraPoint As Integer = db.getExtraReviewPoint(ReviewID)

        If numLikes > numDislikes Then
            db.updateExtraReviewPoint(ReviewID, 1)

            If extraPoint = 0 Then
                OverallScore = OverallScore + 1
            End If

            'Dim commandString As String = "UPDATE [Review] SET OverallScore = '" & OverallScore & "' WHERE ReviewID=" & ReviewID & ";"

            'updating the total review score
            db.updateOverallReviewScore(ReviewID, OverallScore)
        Else
            'num likes < num dislikes
            db.updateExtraReviewPoint(ReviewID, 0)
            'they loose the point
            If extraPoint = 1 Then
                OverallScore = OverallScore - 1
            End If

            'Dim commandString As String = "UPDATE [Review] SET OverallScore = '" & OverallScore & "' WHERE ReviewID=" & ReviewID & ";"

            'updating the total review score
            db.updateOverallReviewScore(ReviewID, OverallScore)
        End If
        'Calculating the Reviewers overall Score
        'get the reviewersID via the ReviewID
        Dim reviewerID As Integer = db.getReviewerID(ReviewID)

        'need to update users overall score for reviews for badges
        calculateReviewerOverallScore(reviewerID)
    End Sub

    'need to get overall score from every review the reviewr made
    Public Sub calculateReviewerOverallScore(ByVal userID As Integer)
        Dim UsersOverallScoresList As ArrayList = New ArrayList()
        Dim TotalScore As Integer = 0
        Dim OverallReviewScore As Double = 0.0

        'getting all the review scores the reviewer has obtained
        UsersOverallScoresList = db.getOverallReviewScoreDetails(userID)

        If UsersOverallScoresList.Count > 0 Then
            For Each ReviewOverallScore In UsersOverallScoresList
                'totalling up all the scores together
                TotalScore = TotalScore + ReviewOverallScore.OverallScore
            Next ReviewOverallScore
            'getting avearage
            OverallReviewScore = (TotalScore / (UsersOverallScoresList.Count * 5)) * 100

            'store score in Patient Tables
            db.updateOverallReviewScoreForReviewer(userID, OverallReviewScore)

            'need to check if they qualify for a badge or not
            checkIfQualifyForReviewBadge(userID, OverallReviewScore)
        End If
    End Sub

    Public Sub checkIfQualifyForReviewBadge(ByVal userID As Integer, ByVal OverallReviewScore As Double)
        'need to get overallReviewer score
        Dim limitToQualify As Double = 90

        'Overall Score works with percentages
        If OverallReviewScore >= limitToQualify Then
            'qalifies for a badge
            db.updateReviewBadgeStatus(userID, "Y")
        Else
            'doesnt qalifies for a badge
            db.updateReviewBadgeStatus(userID, "N")
        End If
    End Sub
End Class