Imports System.Data
Imports System.Data.SqlClient
Public Class PractitionerProfile
    Inherits System.Web.UI.Page

    'user doesnt use the same rating more than 3 times
    Private consistency As Boolean = False
    Private db As MediAvenueDatabase = New MediAvenueDatabase()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim ProfileID As String = Request.QueryString("Profile")

        If (ProfileID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        Dim counter As Integer
        lblMessage.Visible = False

        If (userType = "Prac") Then
            btnAddReview.Visible = False
            txtAddReview.Visible = False
            lblRating.Visible = False
            ddRating.Visible = False
        End If

        'so doesnt repeat on page
        If (Not Page.IsPostBack) Then
            For counter = 1 To 5
                ddRating.Items.Add(New ListItem(counter, counter))
            Next counter
        End If

        'update practitoners rating
        calculatePracsAveRating(ProfileID)

        Dim PracDetails As MediAvenueDatabase.Practitioner = New MediAvenueDatabase.Practitioner
        'load practitioners info onto the page
        'Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Practitioner] ON [User].UserID = [Practitioner].PractitionerID) WHERE [Practitioner].PractitionerID='" & ProfileID & "';"

        PracDetails = db.getPracProfileDetails(ProfileID)

        'displays practitioners info on the page
        lblName.Text = "Dr." & PracDetails.Name & " " & PracDetails.Surname
        lblRatingScore.Text = Math.Round(PracDetails.AveRating, 2)
        lblOccupation.Text = PracDetails.Specialization
        lblBio.Text = PracDetails.Bio
        lblSpecialization.Text = PracDetails.Specialization
        lblTelephone.Text = PracDetails.Telephone
        lblLocation.Text = PracDetails.Address
        lblExperiance.Text = PracDetails.YearsOfExperiance
        lblNameSecond.Text = "Dr." & PracDetails.Name & " " & PracDetails.Surname

        'displays all reviews onto page
        loadReviews(ProfileID)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Suggestions.aspx")
    End Sub

    Private Sub loadReviews(ByVal ProfileID As Integer)
        'displaying all the reviews for the practitioner
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM [Review] WHERE PractitionerID=" & ProfileID & " ORDER BY OverallScore DESC;"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        'Dim reader As SqlDataReader
        Dim ReviewsList As ArrayList = New ArrayList()
        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        ReviewsList = db.getReviews(ProfileID)

        'to reset the lable 
        lblReviews.Text = ""

        If ReviewsList.Count > 0 Then
            For Each Review In ReviewsList
                lblReviews.Text &= "<li Class='list-group-item'>
                                        <div Class='card-body'>
                                            <h5 Class='card-title'><a href='PatientProfile.aspx?Profile=" & Review.PatientID & "'>" & db.getUsersName(Review.PatientID) & "</a><span class='badge badge-dark'>" & Review.PracRating & " star</span></h5>
                                                <p Class='card-text'>" & Review.Review & "</p>
                                                    <div Class='container'><div Class='row'>
                                                        <div Class='col col-lg-12'>
                                                            Overall Score: " & Review.OverallScore & "<br/>
                                                         <a href='About.aspx?Review=" & Review.ReviewID & "'>Like/Dislike Review</a>"
                'lblReviews.Text &= "<div Class='col col-lg-2'>"
                'lblReviews.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
                'lblReviews.Text &= "</div><div Class='col col-lg-8'></div>"
                'lblReviews.Text &= "<div Class='col col-lg-2'>"
                'lblReviews.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a>"
                lblReviews.Text &= "</div></div></div></div></li>"
            Next Review
        Else
            lblReviews.Text &= "<li Class='list-group-item'>
                                    <div Class='card-body'>
                                       <p Class='card-text'>No Reviews made for this Practitioner</p>
                                   </div>
                                </li>"
        End If

        'If reader.HasRows Then
        '    While reader.Read()
        '        lblReviews.Text &= "<li Class='list-group-item'>
        '                                <div Class='card-body'>
        '                                    <h5 Class='card-title'><a href='PatientProfile.aspx?Profile=" & reader("PatientID") & "'>" & db.getUsersName(reader("PatientID")) & "</a><span class='badge badge-dark'>" & reader("PracRating") & " star</span></h5>
        '                                        <p Class='card-text'>" & reader("Review") & "</p>
        '                                            <div Class='container'><div Class='row'>
        '                                                <div Class='col col-lg-12'>
        '                                                    Overall Score: " & reader("OverallScore") & "<br/>
        '                                                 <a href='About.aspx?Review=" & reader("ReviewID") & "'>Like/Dislike Review</a>"
        '        'lblReviews.Text &= "<div Class='col col-lg-2'>"
        '        'lblReviews.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
        '        'lblReviews.Text &= "</div><div Class='col col-lg-8'></div>"
        '        'lblReviews.Text &= "<div Class='col col-lg-2'>"
        '        'lblReviews.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a>"
        '        lblReviews.Text &= "</div></div></div></div></li>"
        '    End While
        'Else
        '    lblReviews.Text &= "<li Class='list-group-item'>
        '                            <div Class='card-body'>
        '                                <p Class='card-text'>No Reviews made for this Practitioner</p>
        '                            </div>
        '                        </li>"
        'End If

        'reader.Close()
        'connection.Close()
        'command.Dispose()
        'connection.Dispose()
        ''db.closeReader()
        ''db.closeDB()
    End Sub

    Protected Sub btnAddReview_Click(sender As Object, e As EventArgs) Handles btnAddReview.Click
        Dim userID As String = Session("UserId")
        Dim reviewOriginalScore As Integer = 0
        Dim reviewArray() As String

        Dim rating As Integer
        Dim ratingString As String = "Select Rating"

        Try
            rating = CInt(ddRating.SelectedItem.Value)
            ratingString = ddRating.SelectedItem.Value
        Catch ex As Exception
            'then it is still a string not a number
            lblMessage.Visible = True
            lblMessage.Text = "Please Select A Rating for your Review"
        End Try

        Dim ProfileID As String = Request.QueryString("Profile")
        If (ProfileID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            If ratingString = "Select Rating" Then
                lblMessage.Visible = True
                lblMessage.Text = "Please Select A Rating for your Review"
            Else
                'need to check if user already reviewed
                Dim regDate As Date = Date.Now()
                Dim TodaysDate As String = regDate.ToString("dd\/MM\/yyyy")
                Dim TodaysTime As String = regDate.ToString("HH:mm:ss")

                Dim review As String = txtAddReview.Text
                Dim reviewID As Integer

                'need to calculate reviews score
                'review is not consistant - 1 point
                'reviews word count > 15 - 2 points
                'review is made in a good time frame - 1 point
                'likes are > dislikes - 1 point - get it after posting - adressed in about page

                'storing users rating consistancey
                updateRatingConsistancy(userID, rating, TodaysDate, TodaysTime)
                If consistency = False Then
                    reviewOriginalScore = reviewOriginalScore + 1
                End If
                'getting word count
                reviewArray = review.Split(" ")
                If reviewArray.Length > 15 Then
                    reviewOriginalScore = reviewOriginalScore + 2
                End If
                'getting time frame
                If db.goodTimeFrame(userID, TodaysDate, TodaysTime) = True Then
                    reviewOriginalScore = reviewOriginalScore + 1
                End If

                'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

                'Dim commandString As String = "INSERT INTO [Review] (PractitionerID,PatientID,Review,PracRating,Date,Time,NumLikes,NumDislikes,OverallScore,OriginalPoint,ExtraPoint) 
                '                               VALUES ('" & ProfileID & "','" & userID & "','" & review & "','" & rating & "','" & TodaysDate & "','" & TodaysTime & "','0','0','" & reviewOriginalScore & "','" & reviewOriginalScore & "','0') SELECT SCOPE_IDENTITY() AS id;"
                'Dim connection As SqlConnection = New SqlConnection(connectionString)
                'Dim command As SqlCommand = New SqlCommand()
                'declare reader

                'command.Connection = connection
                'command.CommandType = CommandType.Text
                'command.CommandText = commandString

                'connection.Open()
                reviewID = db.addReview(ProfileID, userID, review, rating, TodaysDate, TodaysTime, reviewOriginalScore)
                ''get ID for Review
                'If (reader.HasRows()) Then
                '    reader.Read()
                '    reviewID = reader("id")
                'End If
                'reader.Close()

                'connection.Dispose()
                'command.Dispose()
                'connection.Close()
                'db.closeReader()
                'db.closeDB()

                'puting practitioners rating in rating table
                db.addRating(userID, ProfileID, reviewID, rating, TodaysDate, TodaysTime)

                'for the page to reload
                Me.Page_Load(sender, e)
            End If
        End If
    End Sub

    Private Sub updateRatingConsistancy(ByVal userID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
        'need to check if user is in the database
        Dim previousRating As Integer
        Dim ratingNum As Integer = 1 'starts at 1 and ends at 3
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM [RatingConsistency] WHERE PatientID ='" & userID & "';"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        'Dim reader As SqlDataReader

        Dim UsersRatingConsistency As MediAvenueDatabase.RatingConsistency = New MediAvenueDatabase.RatingConsistency
        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        UsersRatingConsistency = db.getRatingConsistencys(userID)

        'checkis if not equal
        If UsersRatingConsistency.Rating <> -1 Then
            'user is in database so need to get previous rating
            previousRating = UsersRatingConsistency.Rating
            ratingNum = UsersRatingConsistency.RateNum
            'reader.Close()

            'might not have this here
            ''db.closeReader()
            ''db.closeDB()
            If ratingNum = 3 Then
                If previousRating = rating Then
                    ratingNum = 1
                    'updating database to reset the count
                    db.updateRatingConsistency(userID, ratingNum)

                    'commandString = "UPDATE [RatingConsistency] SET RateNum = '" & ratingNum & "' WHERE PatientID=" & userID & ";"

                    'command.Connection = connection
                    'command.CommandType = CommandType.Text
                    'command.CommandText = commandString

                    'command.ExecuteNonQuery()

                    consistency = True
                    'need to put review at the bottom!!
                Else
                    'person didnt make the rating the same for the 4th time
                    db.updateRatingConsistencyWithRating(userID, ratingNum, rating)

                    'commandString = "UPDATE [RatingConsistency] SET RateNum='1', Rating='" & rating & "' WHERE PatientID=" & userID & ";"

                    'command.Connection = connection
                    'command.CommandType = CommandType.Text
                    'command.CommandText = commandString

                    'command.ExecuteNonQuery()
                    'dont put review at buttom
                End If
            Else
                If previousRating = rating Then
                    'user hasnt reached its limit but reviewed the person the same as the last time
                    ratingNum = ratingNum + 1
                    'update database to give another chance
                    db.updateRatingConsistency(userID, ratingNum)

                    'commandString = "UPDATE [RatingConsistency] SET RateNum = '" & ratingNum & "' WHERE PatientID=" & userID & ";"

                    'command.Connection = connection
                    'command.CommandType = CommandType.Text
                    'command.CommandText = commandString

                    'command.ExecuteNonQuery()
                End If
            End If
        Else
            'add user to table
            db.insertNewUserToRatingConsistency(userID, rating, TodaysDate, TodaysTime)
        End If

        'reader.Close()



        'commandString = "INSERT INTO [RatingConsistency] (PatientID,RateNum,Rating,Date,Time) VALUES ('" & userID & "','1','" & rating & "','" & TodaysDate & "','" & TodaysTime & "');"

        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'command.ExecuteNonQuery()

        ''db.closeDB()
        'connection.Dispose()
        'command.Dispose()
        'connection.Close()
    End Sub

    Public Sub calculatePracsAveRating(ByVal practitionerID As Integer)
        Dim PracAveRatingList As ArrayList = New ArrayList()
        Dim TotalScore As Integer = 0
        Dim AverageRating As Double = 0.0

        'getting all the ratings scores the prac has obtained
        PracAveRatingList = db.getPracAveRatingScores(practitionerID)

        If PracAveRatingList.Count > 0 Then
            For Each ReviewOverallScore In PracAveRatingList
                'totalling up all the scores together
                TotalScore = TotalScore + ReviewOverallScore.OverallScore
            Next ReviewOverallScore
            'getting avearage
            AverageRating = TotalScore / PracAveRatingList.Count

            'store score in prac Tables
            db.updatePracAveRating(practitionerID, AverageRating)
        End If
    End Sub

    Protected Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click

    End Sub

    Protected Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click

    End Sub

    Protected Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click

    End Sub

    Protected Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click

    End Sub

    Protected Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click

    End Sub
End Class