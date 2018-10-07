﻿Imports System.Data
Imports System.Data.SqlClient
Public Class PractitionerProfile
    Inherits System.Web.UI.Page

    'user doesnt use the same rating more than 3 times
    Public consistency As Boolean = False
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

        'load practitioners info onto the page
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Practitioner] ON [User].UserID = [Practitioner].PractitionerID) WHERE [Practitioner].PractitionerID='" & ProfileID & "';"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        reader = db.getPracProfileDetails(ProfileID)

        'displays practitioners info on the page
        If reader.HasRows Then
            reader.Read()
            lblName.Text = "Dr." & reader("Name") & " " & reader("Surname")
            lblBio.Text = reader("Bio")
            lblSpecialization.Text = reader("Specialization")
            lblTelephone.Text = reader("Telephone")
            lblLocation.Text = reader("Address")
            lblExperiance.Text = reader("YearsOfExperiance")
            lblNameSecond.Text = "Dr." & reader("Name") & " " & reader("Surname")
        End If

        reader.Close()
        'connection.Close()
        'command.Dispose()
        'connection.Dispose()
        db.closeReader()
        db.closeDB()

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
        Dim reader As SqlDataReader

        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        reader = db.getReviews(ProfileID)

        'to reset the lable 
        lblReviews.Text = ""
        If reader.HasRows Then
            While reader.Read()
                lblReviews.Text &= "<li Class='list-group-item'>
                                        <div Class='card-body'>
                                            <h5 Class='card-title'><a href='PatientProfile.aspx?Profile=" & reader("PatientID") & "'>" & db.getUsersName(reader("PatientID")) & "</a><span class='badge badge-dark'>" & reader("PracRating") & " star</span></h5>
                                                <p Class='card-text'>" & reader("Review") & "</p>
                                                    <div Class='container'><div Class='row'>
                                                        <div Class='col col-lg-12'>
                                                            Overall Score: " & reader("OverallScore") & "<br/>
                                                         <a href='About.aspx?Review=" & reader("ReviewID") & "'>Like/Dislike Review</a>"
                'lblReviews.Text &= "<div Class='col col-lg-2'>"
                'lblReviews.Text &= "<a href = '#' Class='btn btn-outline-primary'>Like</a>"
                'lblReviews.Text &= "</div><div Class='col col-lg-8'></div>"
                'lblReviews.Text &= "<div Class='col col-lg-2'>"
                'lblReviews.Text &= "<a href = '#' Class='btn btn-outline-primary'>Dislike</a>"
                lblReviews.Text &= "</div></div></div></div></li>"
            End While
        Else
            lblReviews.Text &= "<li Class='list-group-item'>
                                    <div Class='card-body'>
                                        <p Class='card-text'>No Reviews made for this Practitioner</p>
                                    </div>
                                </li>"
        End If

        reader.Close()
        'connection.Close()
        'command.Dispose()
        'connection.Dispose()
        db.closeReader()
        db.closeDB()
    End Sub

    'Private Function getUsersName(ByVal UserID As Integer)
    '    Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    '    Dim commandString As String = "SELECT [User].Name, [User].Surname FROM [User] WHERE UserID=" & UserID & ";"
    '    Dim connection As SqlConnection = New SqlConnection(connectionString)
    '    Dim command As SqlCommand = New SqlCommand()
    '    Dim reader As SqlDataReader

    '    Dim name As String = ""
    '    command.Connection = connection
    '    command.CommandType = CommandType.Text
    '    command.CommandText = commandString

    '    connection.Open()
    '    reader = command.ExecuteReader()

    '    If (reader.HasRows) Then
    '        reader.Read()
    '        name = reader("Name") & " " & reader("Surname")
    '    End If

    '    reader.Close()
    '    connection.Close()
    '    command.Dispose()
    '    connection.Dispose()

    '    Return name
    'End Function

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
                ''review is not consistant - 1 point
                ''reviews word count > 15 - 2 points
                ''review is made in a good time frame - 1 point
                ''likes are > dislikes - 1 point - get it after posting - adressed in about page

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
                If goodTimeFrame(userID, TodaysDate, TodaysTime) = True Then
                    reviewOriginalScore = reviewOriginalScore + 1
                End If

                'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

                'Dim commandString As String = "INSERT INTO [Review] (PractitionerID,PatientID,Review,PracRating,Date,Time,NumLikes,NumDislikes,OverallScore,OriginalPoint,ExtraPoint) 
                '                               VALUES ('" & ProfileID & "','" & userID & "','" & review & "','" & rating & "','" & TodaysDate & "','" & TodaysTime & "','0','0','" & reviewOriginalScore & "','" & reviewOriginalScore & "','0') SELECT SCOPE_IDENTITY() AS id;"
                'Dim connection As SqlConnection = New SqlConnection(connectionString)
                'Dim command As SqlCommand = New SqlCommand()
                Dim reader As SqlDataReader

                'command.Connection = connection
                'command.CommandType = CommandType.Text
                'command.CommandText = commandString

                'connection.Open()
                reader = db.addReview(ProfileID, userID, review, rating, TodaysDate, TodaysTime, reviewOriginalScore)
                'get ID for Review
                If (reader.HasRows()) Then
                    reader.Read()
                    reviewID = reader("id")
                End If
                reader.Close()

                'connection.Dispose()
                'command.Dispose()
                'connection.Close()
                db.closeReader()
                db.closeDB()

                'puting practitioners rating in rating table
                db.addRating(userID, ProfileID, reviewID, rating, TodaysDate, TodaysTime)

                'might need messages to say it went through

                'for the page to reload
                Me.Page_Load(sender, e)
            End If
        End If
    End Sub

    'Private Sub addRating(ByVal patientID As Integer, ByVal PractitionerID As Integer, ByVal reviewID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
    '    Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    '    Dim commandString As String = "INSERT INTO [Rating] (PractitionerID,PatientID,ReviewID,Rating,Date,Time) VALUES ('" & PractitionerID & "','" & patientID & "','" & reviewID & "','" & rating & "','" & TodaysDate & "','" & TodaysTime & "');"
    '    Dim connection As SqlConnection = New SqlConnection(connectionString)
    '    Dim command As SqlCommand = New SqlCommand()

    '    command.Connection = connection
    '    command.CommandType = CommandType.Text
    '    command.CommandText = commandString

    '    connection.Open()
    '    command.ExecuteNonQuery()

    '    'need to calculate Pracs new rating in a new sub!!

    '    connection.Dispose()
    '    command.Dispose()
    '    connection.Close()
    'End Sub

    Private Sub updateRatingConsistancy(ByVal userID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
        'need to check if user is in the database
        Dim previousRating As Integer
        Dim ratingNum As Integer = 1 'starts at 1 and ends at 3
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM [RatingConsistency] WHERE PatientID ='" & userID & "';"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        reader = db.getRatingConsistencys(userID)

        If (reader.HasRows()) Then
            reader.Read()
            'user is in database so need to get previous rating
            previousRating = reader("Rating")
            ratingNum = reader("RateNum")
            reader.Close()
            'might not have this here
            db.closeReader()
            db.closeDB()
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
            reader.Close()
            'add user to table
            db.insertNewUserToRatingConsistency(userID, rating, TodaysDate, TodaysTime)

            'commandString = "INSERT INTO [RatingConsistency] (PatientID,RateNum,Rating,Date,Time) VALUES ('" & userID & "','1','" & rating & "','" & TodaysDate & "','" & TodaysTime & "');"

            'command.Connection = connection
            'command.CommandType = CommandType.Text
            'command.CommandText = commandString

            'command.ExecuteNonQuery()
        End If
        db.closeDB()
        'connection.Dispose()
        'command.Dispose()
        'connection.Close()
    End Sub

    Private Function goodTimeFrame(ByVal userID As Integer, ByVal incommingDate As String, ByVal incommingTime As String) As Boolean
        Dim goodTime As Boolean = True
        Dim regDate As Date = Date.Now()
        Dim previousDate As String = regDate.ToString("dd\/MM\/yyyy")
        Dim previousTime As String = regDate.ToString("HH:mm:ss")
        Dim counter As Integer = 1
        Dim previousTimeArray() As String
        Dim nextTimeArray() As String
        Dim previousHour As String
        Dim previousMinute As String
        Dim nextHour As String
        Dim nextMinute As String
        Dim TimeDifference As Integer

        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "SELECT Date,Time FROM [Review] WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While (reader.Read())
                previousDate = reader("Date")
                previousTime = reader("Time")
                If previousDate = incommingDate Then
                    'can break up time into a string
                    previousTimeArray = previousTime.Split(":")
                    previousHour = previousTimeArray(0)
                    previousMinute = previousTimeArray(1)
                    nextTimeArray = incommingTime.Split(":")
                    nextHour = nextTimeArray(0)
                    nextMinute = nextTimeArray(1)
                    If previousHour = nextHour Then
                        TimeDifference = CInt(nextMinute) - CInt(previousMinute)
                        'if next review was written 5 minutes later
                        If TimeDifference < 5 Then
                            goodTime = False
                        End If
                    End If
                End If
            End While
        End If
        reader.Close()
        connection.Close()
        command.Dispose()
        connection.Dispose()

        Return goodTime
    End Function

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