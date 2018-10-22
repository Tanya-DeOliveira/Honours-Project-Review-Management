Imports System.Data.SqlClient
Public Class MediAvenueDatabase
    'variables
    Private connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    'Private commandString As String 'SQL statment
    'Private connection As SqlConnection
    'Private command As SqlCommand = New SqlCommand()
    'Private reader As SqlDataReader

    Private Structure CategoryType
        Public CategoryTypeID As Integer
        Public CategoryType As String
    End Structure
    Private Structure MedicalCategory
        Public QuestionID As Integer
        Public CategoryTypeID As Integer
    End Structure
    Public Structure Patient
        Public PatientID As Integer
        Public OverallReviewScore As Double
        Public MasterReviewer As String
        Public MasterSuggester As String
        Public NumReviewsMade As Integer
    End Structure
    Public Structure Practitioner
        Public PractitionerID As Integer
        Public Name As String
        Public Surname As String
        Public Specialization As String
        Public Telephone As String
        Public Address As String
        Public YearsOfExperiance As Integer
        Public Bio As String
        Public NumRatingHave As Integer
        Public AveRating As Double
        Public CV As String
        Public Active As String
    End Structure
    Public Structure Question
        Public QuestionID As Integer
        Public PatientID As Integer
        Public Question As String
        Public Description As String
        Public DateUploaded As String
        Public TimeUploaded As String
        Public Popularity As Integer
        Public Remove As String
    End Structure
    Private Structure Rating
        Public RatingID As Integer
        Public PractitionerID As Integer
        Public PatientID As Integer
        Public ReviewID As Integer
        Public Rating As Integer
        Public DateUploaded As String
        Public TimeUploaded As String
    End Structure
    Public Structure RatingConsistency
        Public RatingConsistencyID As Integer
        Public PatientID As Integer
        Public RateNum As Integer
        Public Rating As Integer
        Public DateUploaded As String
        Public TimeUploaded As String
    End Structure
    Public Structure Review
        Public ReviewID As Integer
        Public PractitionerID As Integer
        Public PatientID As Integer
        Public Review As String
        Public PracRating As Integer
        Public DateUploaded As String
        Public TimeUploaded As String
        Public NumLikes As Integer
        Public NumDislikes As Integer
        Public OverallScore As Double
        Public OriginalPoint As Integer
        Public ExtraPoint As Integer
    End Structure
    Private Structure Reward
        Public RewardID As Integer
        Public PatientID As Integer
        Public Name As String
        Public DateUploaded As String
        Public TimeUploaded As String
        Public Claimed As String
    End Structure
    Public Structure Suggestion
        Public SuggestionID As Integer
        Public UserID As Integer
        Public QuestionID As Integer
        Public TypeOfUser As String
        Public Suggestion As String
        Public DateUploaded As String
        Public TimeUploaded As String
        Public NumLikes As Integer
        Public NumDislikes As Integer
        Public Remove As String
        Public OverallScore As Double
        Public OriginalPoint As Integer
        Public ExtraPoint As Integer
    End Structure
    Public Structure User
        Public UserID As Integer
        Public Name As String
        Public Surname As String
        Public Username As String
        Public ContactNum As String
        Public Email As String
        Public Password As String
        Public TypeOfUser As String
        Public ProfilePicLocation As String
        Public LastLoggedIn As String
        Public OverallSuggestionScore As Double
        Public NumSuggestionsMade As Integer
    End Structure


    Private consistency As Boolean = False
    'constructor
    Public Sub New()

    End Sub

    'methods
    'Public Sub closeDB()
    '    'method permanently close and removes connection object from memory and the resource no longer exists for any further 
    '    command.Connection.Close()
    '    command.Dispose()
    '    connection.Dispose()
    'End Sub

    'Public Sub closeReader()
    '    reader.Close()
    'End Sub

    'Public Sub initaliseConnection()
    '    connection = New SqlConnection(connectionString)
    'End Sub

    'select Statments
    'get everything from category table
    Public Function getCategoryTypes() As ArrayList
        Dim CategoryTypesList As ArrayList = New ArrayList()
        Dim CategoryType As CategoryType = New CategoryType

        Dim commandString As String = "SELECT * FROM [CategoryType];"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        'filling out items into dropdown list for user to select category for question and also for user to filter questions
        While reader.Read
            CategoryType.CategoryType = reader("CategoryType")
            CategoryType.CategoryTypeID = reader("CategoryTypeID")
            CategoryTypesList.Add(CategoryType)
        End While

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return CategoryTypesList
    End Function
    'get all the questions in questions table
    Public Function getQuestions() As ArrayList
        Dim AllQuestionsList As ArrayList = New ArrayList()
        Dim Question As Question = New Question

        Dim commandString As String = "SELECT * FROM [Question];"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        While reader.Read
            Question.Question = reader("Question")
            Question.PatientID = reader("PatientID")
            Question.Description = reader("Description")
            Question.QuestionID = reader("QuestionID")
            Question.Popularity = reader("Popularity")
            Question.DateUploaded = reader("Date")
            Question.TimeUploaded = reader("Time")
            AllQuestionsList.Add(Question)
        End While

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return AllQuestionsList
    End Function

    Public Function getQuestion(ByVal QuestionID As Integer) As Question
        Dim Question As Question = New Question

        Dim commandString As String = "SELECT * FROM [Question] WHERE QuestionID='" & QuestionID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        'displays question on the page
        If reader.HasRows Then
            reader.Read()
            Question.Question = reader("Question")
            Question.PatientID = reader("PatientID")
            Question.Description = reader("Description")
            Question.DateUploaded = reader("Date")
            Question.TimeUploaded = reader("Time")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return Question
    End Function
    'get the patients name
    Public Function getPatientName(ByVal patientID As Integer) As String
        Dim commandString As String = "SELECT [User].Name, [User].Surname FROM [Patient] INNER JOIN [User] ON Patient.PatientID = [User].UserID WHERE [Patient].PatientID=" & patientID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        Dim name As String = ""
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If (reader.HasRows) Then
            reader.Read()
            name = reader("Name") & " " & reader("Surname")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
        Return name
    End Function

    'get practioners details for profile
    Public Function getPracProfileDetails(ByVal ProfileID As Integer) As Practitioner
        Dim PracDetails As Practitioner = New Practitioner

        Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Practitioner] ON [User].UserID = [Practitioner].PractitionerID) WHERE [Practitioner].PractitionerID='" & ProfileID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            PracDetails.Name = reader("Name")
            PracDetails.AveRating = reader("AveRating")
            PracDetails.Surname = reader("Surname")
            PracDetails.Bio = reader("Bio")
            PracDetails.Specialization = reader("Specialization")
            PracDetails.Telephone = reader("Telephone")
            PracDetails.Address = reader("Address")
            PracDetails.YearsOfExperiance = reader("YearsOfExperiance")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return PracDetails
    End Function
    'get all the reviews for a practitiner
    Public Function getReviews(ByVal ProfileID As Integer) As ArrayList
        Dim AllReviewsList As ArrayList = New ArrayList()
        Dim Review As Review = New Review

        Dim commandString As String = "SELECT * FROM [Review] WHERE PractitionerID=" & ProfileID & " ORDER BY OverallScore DESC;"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        While reader.Read
            Review.PatientID = reader("PatientID")
            Review.PracRating = reader("PracRating")
            Review.Review = reader("Review")
            Review.OverallScore = reader("OverallScore")
            Review.ReviewID = reader("ReviewID")
            AllReviewsList.Add(Review)
        End While

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return AllReviewsList
    End Function

    'get a users name
    Public Function getUsersName(ByVal UserID As Integer) As String
        Dim commandString As String = "SELECT [User].Name, [User].Surname FROM [User] WHERE UserID=" & UserID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        Dim name As String = ""
        Command.Connection = connection
        Command.CommandType = CommandType.Text
        Command.CommandText = commandString

        connection.Open()
        reader = Command.ExecuteReader()

        If (reader.HasRows) Then
            reader.Read()
            name = reader("Name") & " " & reader("Surname")
        End If

        reader.Close()
        Command.Connection.Close()
        Command.Dispose()
        connection.Dispose()

        Return name
    End Function
    'get everything from rating consistancy table
    Public Function getRatingConsistencys(ByVal userID As Integer) As RatingConsistency
        Dim UsersRatingConsistency As RatingConsistency = New RatingConsistency

        Dim commandString As String = "SELECT * FROM [RatingConsistency] WHERE PatientID ='" & userID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            UsersRatingConsistency.Rating = reader("Rating")
            UsersRatingConsistency.RateNum = reader("RateNum")
        Else
            'no user in database
            UsersRatingConsistency.Rating = -1
            UsersRatingConsistency.RateNum = -1
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return UsersRatingConsistency
    End Function

    Public Function getSuggestions(ByVal questionID As Integer) As ArrayList
        Dim AllSuggestionsList As ArrayList = New ArrayList()
        Dim Suggestion As Suggestion = New Suggestion

        Dim commandString As String = "SELECT * FROM ([User] INNER JOIN [Suggestion] ON [User].UserID = [Suggestion].UserID) WHERE QuestionID=" & questionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                Suggestion.TypeOfUser = reader("TypeOfUser")
                Suggestion.UserID = reader("UserID")
                Suggestion.Suggestion = reader("Suggestion")
                Suggestion.SuggestionID = reader("SuggestionID")
                AllSuggestionsList.Add(Suggestion)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return AllSuggestionsList
    End Function

    Public Function getPopularity(ByVal questionID As Integer) As Integer
        Dim numPopularity As Integer = 0

        Dim commandString As String = "SELECT Popularity FROM [Question];"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            numPopularity = reader("Popularity")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numPopularity
    End Function

    Public Function getUserData(ByVal userID As Integer) As User
        Dim User As User = New User

        Dim commandString As String = "SELECT * FROM [User] WHERE UserID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If (reader.HasRows()) Then
            reader.Read()
            User.ContactNum = reader("ContactNum")
            User.Email = reader("Email")
            User.Name = reader("Name")
            User.Surname = reader("Surname")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return User
    End Function

    Public Function getBadges(ByVal userID As Integer) As Patient
        Dim Patient As Patient = New Patient

        Dim commandString As String = "Select * FROM [Patient] WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If (reader.HasRows()) Then
            reader.Read()
            Patient.MasterReviewer = reader("MasterReviewer")
            Patient.MasterSuggester = reader("MasterSuggester")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return Patient
    End Function

    Public Function getPracReview(ByVal ReviewID As Integer) As Review
        Dim PracReview As Review = New Review

        Dim commandString As String = "SELECT * FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            PracReview.PractitionerID = reader("PractitionerID")
            PracReview.PatientID = reader("PatientID")
            PracReview.Review = reader("Review")
            PracReview.PracRating = reader("PracRating")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return PracReview
    End Function

    Public Function getNumReviewDislikes(ByVal ReviewID As Integer) As Integer
        Dim commandString As String = "Select NumDislikes FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim numDislikes As Integer = 0
        If reader.HasRows Then
            reader.Read()
            numDislikes = CInt(reader("NumDislikes"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numDislikes
    End Function

    Public Function getNumReviewLikes(ByVal ReviewID As Integer) As Integer
        Dim commandString As String = "Select NumLikes FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim numLikes As Integer = 0
        If reader.HasRows Then
            reader.Read()
            numLikes = CInt(reader("NumLikes"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numLikes
    End Function

    Public Function getOverallReviewScore(ByVal ReviewID As Integer) As Integer
        Dim commandString As String = "Select OverallScore FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim OverallScore As Integer = 0
        If reader.HasRows Then
            reader.Read()
            OverallScore = CInt(reader("OverallScore"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return OverallScore
    End Function

    Public Function getExtraReviewPoint(ByVal ReviewID As Integer) As Integer
        Dim commandString As String = "Select ExtraPoint FROM [Review] WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim extraPoint As Integer = 0
        If reader.HasRows Then
            reader.Read()
            extraPoint = CInt(reader("ExtraPoint"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return extraPoint
    End Function

    Public Function getSuggestion(ByVal suggestionID As Integer) As Suggestion
        Dim Suggestion As Suggestion = New Suggestion

        Dim commandString As String = "SELECT * FROM [Suggestion] WHERE SuggestionID=" & suggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            Suggestion.UserID = reader("UserID")
            Suggestion.Suggestion = reader("Suggestion")
            Suggestion.QuestionID = reader("QuestionID")
            'might need to get num likes and dislikes
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return Suggestion
    End Function

    Public Function getNumSuggestionLikes(ByVal SuggestionID As Integer) As Integer
        Dim commandString As String = "Select NumLikes FROM [Suggestion] WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim numLikes As Integer = 0
        If reader.HasRows Then
            reader.Read()
            numLikes = CInt(reader("NumLikes"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numLikes
    End Function

    Public Function getNumSuggestionDislikes(ByVal SuggestionID As Integer) As Integer
        Dim commandString As String = "Select NumDislikes FROM [Suggestion] WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim numDislikes As Integer = 0
        If reader.HasRows Then
            reader.Read()
            numDislikes = CInt(reader("NumDislikes"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numDislikes
    End Function

    Public Function getOverallSuggestionScore(ByVal SuggestionID As Integer) As Integer
        Dim commandString As String = "Select OverallScore FROM [Suggestion] WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim OverallScore As Integer = 0
        If reader.HasRows Then
            reader.Read()
            OverallScore = CInt(reader("OverallScore"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return OverallScore
    End Function

    Public Function getExtraSuggestionPoint(ByVal SuggestionID As Integer) As Integer
        Dim commandString As String = "Select ExtraPoint FROM [Suggestion] WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Dim extraPoint As Integer = 0
        If reader.HasRows Then
            reader.Read()
            extraPoint = CInt(reader("ExtraPoint"))
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return extraPoint
    End Function

    Public Function getOverallReviewScoreDetails(ByVal PatientID As Integer) As ArrayList
        Dim OverallScoreDetails As Review = New Review
        Dim UsersOverallScoresList As ArrayList = New ArrayList()

        Dim commandString As String = "SELECT * FROM [Review] WHERE PatientID=" & PatientID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                OverallScoreDetails.OverallScore = reader("OverallScore")
                UsersOverallScoresList.Add(OverallScoreDetails)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return UsersOverallScoresList
    End Function

    Public Function getOverallSuggestionScoreDetails(ByVal UserID As Integer) As ArrayList
        Dim OverallScoreDetails As Review = New Review
        Dim UsersOverallScoresList As ArrayList = New ArrayList()

        Dim commandString As String = "SELECT * FROM [Suggestion] WHERE UserID=" & UserID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                OverallScoreDetails.OverallScore = reader("OverallScore")
                UsersOverallScoresList.Add(OverallScoreDetails)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return UsersOverallScoresList
    End Function

    Public Function getPracAveRatingScores(ByVal PractitionerID As Integer) As ArrayList
        Dim PracDetails As Review = New Review
        Dim PracAveRatingList As ArrayList = New ArrayList()

        Dim commandString As String = "SELECT * FROM [Rating] WHERE PractitionerID=" & PractitionerID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                PracDetails.OverallScore = reader("Rating")
                PracAveRatingList.Add(PracDetails)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return PracAveRatingList
    End Function

    Public Function getAllFlaggedReviwUsers() As ArrayList
        'this will get all the review users with their Review/Suggestion Score < 30%
        Dim flaggedUsers As ArrayList = New ArrayList()
        Dim ReviewUser As Patient = New Patient

        Dim commandString As String = "SELECT * FROM [Patient] WHERE OverallReviewerScore < 30;"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                ReviewUser.PatientID = reader("PatientID")
                ReviewUser.OverallReviewScore = reader("OverallReviewerScore")
                ReviewUser.NumReviewsMade = reader("NumReviewsMade")
                flaggedUsers.Add(ReviewUser)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return flaggedUsers
    End Function

    Public Function getAllFlaggedSuggestionUsers() As ArrayList
        'this will get all the review users with their Review/Suggestion Score < 30%
        Dim flaggedUsers As ArrayList = New ArrayList()
        Dim suggestionUser As User = New User

        Dim commandString As String = "SELECT UserID, TypeOfUser, OverallSuggestionScore, NumSuggestionsMade FROM [User] WHERE OverallSuggestionScore < 30;"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                suggestionUser.UserID = reader("UserID")
                suggestionUser.TypeOfUser = reader("TypeOfUser")
                suggestionUser.OverallSuggestionScore = reader("OverallSuggestionScore")
                suggestionUser.NumSuggestionsMade = reader("NumSuggestionsMade")
                flaggedUsers.Add(suggestionUser)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return flaggedUsers
    End Function

    Public Function getNumReviewsMade() As Integer
        Dim numReviews As Integer = 0

        Dim commandString As String = "SELECT COUNT(Review) AS numReviews FROM [Review];"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            numReviews = reader("numReviews")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numReviews
    End Function

    Public Function getNumSuggestionsMade() As Integer
        Dim numSuggestions As Integer = 0

        Dim commandString As String = "SELECT COUNT(Suggestion) AS numSuggestions FROM [Suggestion];"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            numSuggestions = reader("numSuggestions")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numSuggestions
    End Function

    Public Function getNumPatients(ByVal type As String) As Integer
        Dim numUsers As Integer = 0

        Dim commandString As String = "SELECT COUNT(UserID) AS numUsers FROM [User] WHERE TypeOfUser = '" & type & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            numUsers = reader("numUsers")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return numUsers
    End Function

    'gets the reviewersID in order to calculate the reviewers Overall Reviewer Score for a badge
    Public Function getReviewerID(ByVal reviewID As Integer) As Integer
        Dim reviewerID As Integer = 1

        Dim commandString As String = "SELECT PatientID FROM [Review] WHERE ReviewID = '" & reviewID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            reviewerID = reader("PatientID")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return reviewerID
    End Function

    'gets the suggestersID in order to calculate the suggesters Overall Reviewer Score for a badge
    Public Function getSuggesterID(ByVal suggestionID As Integer) As Integer
        Dim suggesterID As Integer = 1

        Dim commandString As String = "SELECT UserID FROM [Suggestion] WHERE SuggestionID = " & suggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            reader.Read()
            suggesterID = reader("UserID")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return suggesterID
    End Function

    'these are to show on admin page to see who has a badge in order to get a reward for reviews
    Public Function getAllTopReviwUsers() As ArrayList
        'this will get all the review users with badges for reviews based on their Review Score > 90%
        Dim TopUsers As ArrayList = New ArrayList()
        Dim ReviewUser As Patient = New Patient

        Dim commandString As String = "SELECT * FROM [Patient] WHERE MasterReviewer = 'Y';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                ReviewUser.PatientID = reader("PatientID")
                ReviewUser.OverallReviewScore = reader("OverallReviewerScore")
                ReviewUser.NumReviewsMade = reader("NumReviewsMade")
                TopUsers.Add(ReviewUser)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return TopUsers
    End Function

    'these are to show on admin page to see who has a badge in order to get a reward for suggestions
    Public Function getAllTopSuggestionUsers() As ArrayList
        'this will get all the suggestion users with badges for suggestions based on their suggestion Score > 90%
        Dim TopUsers As ArrayList = New ArrayList()
        Dim suggestionUser As User = New User

        Dim commandString As String = "SELECT UserID, TypeOfUser, OverallSuggestionScore, NumSuggestionsMade FROM [User] INNER JOIN [Patient] ON [User].UserID = [Patient].PatientID WHERE MasterSuggester = 'Y';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                suggestionUser.UserID = reader("UserID")
                suggestionUser.TypeOfUser = reader("TypeOfUser")
                suggestionUser.OverallSuggestionScore = reader("OverallSuggestionScore")
                suggestionUser.NumSuggestionsMade = reader("NumSuggestionsMade")
                TopUsers.Add(suggestionUser)
            End While
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return TopUsers
    End Function

    'insert statments 
    'add question to the Question table
    Public Function addQuestion(ByVal userID As Integer, ByVal Question As String, ByVal Description As String, ByVal TodaysDate As String, ByVal TodaysTime As String) As Integer
        Dim QuestionID As Integer

        Dim commandString As String = "INSERT INTO [Question] (PatientID,Question,Description,Date,Time,Popularity,Remove) 
                         VALUES ('" & userID & "', @Question, @Description,'" & TodaysDate & "','" & TodaysTime & "','0','N') SELECT SCOPE_IDENTITY() AS id;"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@Question", Question)
        command.Parameters.AddWithValue("@Description", Description)

        connection.Open()
        reader = command.ExecuteReader()

        If (reader.HasRows()) Then
            reader.Read()
            QuestionID = reader("id")
        End If

        reader.Close()
        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return QuestionID
    End Function

    'add category of question to database
    Public Sub addQuestionsCategory(ByVal questionID As Integer, ByVal CategoryTypeID As Integer)

        Dim commandString As String = "INSERT INTO [MedicalCategory] (QuestionID,CategoryTypeID) 
                         VALUES ('" & questionID & "','" & CategoryTypeID & "');"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    'add a review for a practioner 
    Public Function addReview(ByVal ProfileID As Integer, ByVal userID As Integer, ByVal review As String, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String, ByVal reviewOriginalScore As Integer) As Integer
        Dim commandString As String = "INSERT INTO [Review] (PractitionerID,PatientID,Review,PracRating,Date,Time,NumLikes,NumDislikes,OverallScore,OriginalPoint,ExtraPoint) 
                                       VALUES ('" & ProfileID & "','" & userID & "',@review ,@rating ,'" & TodaysDate & "','" & TodaysTime & "','0','0','" & reviewOriginalScore & "','" & reviewOriginalScore & "','0') SELECT SCOPE_IDENTITY() AS id;"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        Dim reviewID As Integer

        command.Connection = connection
        Command.CommandType = CommandType.Text
        Command.CommandText = commandString

        Command.Parameters.AddWithValue("@review", review)
        Command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        reader = Command.ExecuteReader()

        If (reader.HasRows()) Then
            reader.Read()
            reviewID = reader("id")
        End If

        reader.Close()
        Command.Connection.Close()
        Command.Dispose()
        connection.Dispose()

        Return reviewID
    End Function

    'add a rating for a practioner
    Public Sub addRating(ByVal patientID As Integer, ByVal PractitionerID As Integer, ByVal reviewID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
        Dim commandString As String = "INSERT INTO [Rating] (PractitionerID,PatientID,ReviewID,Rating,Date,Time) 
                         VALUES ('" & PractitionerID & "','" & patientID & "','" & reviewID & "', @rating,'" & TodaysDate & "','" & TodaysTime & "');"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        Command.CommandType = CommandType.Text
        Command.CommandText = commandString

        Command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        Command.ExecuteNonQuery()

        'need to calculate Pracs new rating in a new sub!!

        Command.Connection.Close()
        Command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub addSuggestion(ByVal userID As Integer, ByVal questionID As Integer, ByVal Suggestion As String, ByVal TodaysDate As String, ByVal TodaysTime As String, ByVal SuggestionOriginalScore As String)
        Dim commandString As String = "INSERT INTO [Suggestion] (UserID,QuestionID,Suggestion,Date,Time,NumLikes,NumDislikes,Remove,OverallScore,OriginalPoint,ExtraPoint) 
                                       VALUES ('" & userID & "','" & questionID & "', @Suggestion,'" & TodaysDate & "','" & TodaysTime & "','0','0','N','" & SuggestionOriginalScore & "','" & SuggestionOriginalScore & "','0');"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@Suggestion", Suggestion)

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub addReviewLike(ByVal ReviewID As Integer, ByVal numLikes As Integer)
        Dim commandString As String = "UPDATE [Review] SET NumLikes = '" & numLikes & "' WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub addReviewDislike(ByVal ReviewID As Integer, ByVal numDislikes As Integer)
        Dim commandString As String = "UPDATE [Review] SET NumDislikes = '" & numDislikes & "' WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub addSuggestionLike(ByVal SuggestionID As Integer, ByVal numLikes As Integer)
        Dim commandString As String = "UPDATE [Suggestion] SET NumLikes = '" & numLikes & "' WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub addSuggestionDislike(ByVal SuggestionID As Integer, ByVal numDislikes As Integer)
        Dim commandString As String = "UPDATE [Suggestion] SET NumDislikes = '" & numDislikes & "' WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    'update

    'update Rating Consistency table when user uses the same rating consistency
    'updating database to reset the count because they have reached 3
    Public Sub updateRatingConsistency(ByVal userID As Integer, ByVal ratingNum As Integer)
        Dim commandString As String = "UPDATE [RatingConsistency] SET RateNum = '" & ratingNum & "' WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        Command.CommandType = CommandType.Text
        Command.CommandText = commandString

        connection.Open()
        Command.ExecuteNonQuery()

        Command.Connection.Close()
        Command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateRatingConsistencyWithRating(ByVal userID As Integer, ByVal ratingNum As Integer, ByVal rating As Integer)
        Dim commandString As String = "UPDATE [RatingConsistency] SET RateNum='1', Rating= @rating WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        Command.CommandType = CommandType.Text
        Command.CommandText = commandString

        Command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        Command.ExecuteNonQuery()

        Command.Connection.Close()
        Command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateNumSuggestions(ByVal questionID As Integer)
        'need to get the number of popularity first
        Dim numPopularity As Integer = 0
        numPopularity = getPopularity(questionID)
        'add one to it as another suggestion was added to a question
        numPopularity = numPopularity + 1

        Dim commandString As String = "UPDATE [Question] SET Popularity='" & numPopularity & "' WHERE QuestionID='" & questionID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub insertNewUserToRatingConsistency(ByVal userID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
        Dim commandString As String = "INSERT INTO [RatingConsistency] (PatientID,RateNum,Rating,Date,Time) 
                         VALUES ('" & userID & "','1', @rating,'" & TodaysDate & "','" & TodaysTime & "');"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        Command.CommandType = CommandType.Text
        Command.CommandText = commandString

        Command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        Command.ExecuteNonQuery()

        Command.Connection.Close()
        Command.Dispose()
        connection.Dispose()
    End Sub
    'Public Sub updateRatingConsistancy(ByVal userID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
    '    'need to check if user is in the database
    '    Dim previousRating As Integer
    '    Dim ratingNum As Integer = 1 'starts at 1 and ends at 3
    '    Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    '    commandString = "SELECT * FROM [RatingConsistency] WHERE PatientID ='" & userID & "';"

    '    command.Connection = connection
    '    command.CommandType = CommandType.Text
    '    command.CommandText = commandString

    '    connection.Open()
    '    reader = command.ExecuteReader()
    '    If (reader.HasRows()) Then
    '        reader.Read()
    '        'user is in database so need to get previous rating
    '        previousRating = reader("Rating")
    '        ratingNum = reader("RateNum")
    '        reader.Close()
    '        If ratingNum = 3 Then
    '            If previousRating = rating Then
    '                ratingNum = 1
    '                'updating database to reset the count
    '                commandString = "UPDATE [RatingConsistency] SET RateNum = '" & ratingNum & "' WHERE PatientID=" & userID & ";"

    '                command.Connection = connection
    '                command.CommandType = CommandType.Text
    '                command.CommandText = commandString

    '                command.ExecuteNonQuery()

    '                consistency = True
    '                'need to put review at the bottom!!
    '            Else
    '                'person didnt make the rating the same for the 4th time
    '                commandString = "UPDATE [RatingConsistency] SET RateNum='1', Rating='" & rating & "' WHERE PatientID=" & userID & ";"

    '                command.Connection = connection
    '                command.CommandType = CommandType.Text
    '                command.CommandText = commandString

    '                command.ExecuteNonQuery()
    '                'dont put review at buttom
    '            End If
    '        Else
    '            If previousRating = rating Then
    '                'user hasnt reached its limit but reviewed the person the same as the last time
    '                ratingNum = ratingNum + 1
    '                'update database to give another change
    '                commandString = "UPDATE [RatingConsistency] SET RateNum = '" & ratingNum & "' WHERE PatientID=" & userID & ";"

    '                command.Connection = connection
    '                command.CommandType = CommandType.Text
    '                command.CommandText = commandString

    '                command.ExecuteNonQuery()
    '            End If
    '        End If
    '    Else
    '        reader.Close()
    '        'add user to table
    '        commandString = "INSERT INTO [RatingConsistency] (PatientID,RateNum,Rating,Date,Time) VALUES ('" & userID & "','1','" & rating & "','" & TodaysDate & "','" & TodaysTime & "');"

    '        command.Connection = connection
    '        command.CommandType = CommandType.Text
    '        command.CommandText = commandString

    '        command.ExecuteNonQuery()
    '    End If

    '    connection.Dispose()
    '    command.Dispose()
    '    connection.Close()
    'End Sub

    Public Sub updatePractionerProfile(ByVal specilaization As String, ByVal Address As String, ByVal experiance As Integer, ByVal Telephone As String, ByVal userID As Integer)
        Dim commandString As String = "UPDATE [Practitioner] 
                                       SET Specialization = @specilaization, Address= @Address, YearsOfExperiance= @experiance, Telephone= @Telephone WHERE [Practitioner].PractitionerID = '" & userID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@specilaization", specilaization.ToString)
        command.Parameters.AddWithValue("@Address", Address.ToString)
        command.Parameters.AddWithValue("@experiance", experiance.ToString)
        command.Parameters.AddWithValue("@Telephone", Telephone.ToString)

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
        End Sub

    Public Sub updateUserProfile(ByVal name As String, ByVal surname As String, ByVal cellphone As Integer, ByVal email As String, ByVal userID As Integer)
        Dim commandString As String = "UPDATE [User] 
                                       SET Name= @name, Surname= @surname, ContactNum= @cellphone, Email= @email WHERE [User].UserID= '" & userID & "';"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@name", name.ToString)
        command.Parameters.AddWithValue("@surname", surname.ToString)
        command.Parameters.AddWithValue("@cellphone", cellphone.ToString)
        command.Parameters.AddWithValue("@email", email.ToString)

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateExtraReviewPoint(ByVal ReviewID As Integer, ByVal extraPoint As Integer)
        Dim commandString As String = "UPDATE [Review] SET ExtraPoint = '" & extraPoint & "' WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateOverallReviewScore(ByVal ReviewID As Integer, ByVal OverallScore As Integer)
        Dim commandString As String = "UPDATE [Review] SET OverallScore = '" & OverallScore & "' WHERE ReviewID=" & ReviewID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateExtraSuggestionPoint(ByVal SuggestionID As Integer, ByVal extraPoint As Integer)
        Dim commandString As String = "UPDATE [Suggestion] SET ExtraPoint = '" & extraPoint & "' WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateOverallSuggestionScore(ByVal SuggestionID As Integer, ByVal OverallScore As Integer)
        Dim commandString As String = "UPDATE [Suggestion] SET OverallScore = '" & OverallScore & "' WHERE SuggestionID=" & SuggestionID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateOverallReviewScoreForReviewer(ByVal userID As Integer, ByVal OverallReviewerScore As Double)
        'OverallReviewerScore is coming in with a comma and not a full stop so need to add the full stop
        Dim OverallReviewerScoreSplit As String() = OverallReviewerScore.ToString.Split(",")
        Dim wholeNumber As String = OverallReviewerScoreSplit(0)
        Dim decimalNumber As String = 0
        'array already has 1 elemnt
        If OverallReviewerScoreSplit.Count > 1 Then
            decimalNumber = OverallReviewerScoreSplit(1)
        End If
        Dim OverallReviewerScoreForDatabase As String = wholeNumber & "." & decimalNumber

        Dim commandString As String = "UPDATE [Patient] SET OverallReviewerScore = '" & OverallReviewerScoreForDatabase & "' WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateOverallSuggestionScoreForUser(ByVal userID As Integer, ByVal OverallSugesstionScore As Double)
        'OverallSugesstionScore is coming in with a comma and not a full stop so need to add the full stop
        Dim OverallSugesstionScoreForDatabaseSplit As String() = OverallSugesstionScore.ToString.Split(",")
        Dim wholeNumber As String = OverallSugesstionScoreForDatabaseSplit(0)
        Dim decimalNumber As String = 0
        'array already has 1 elemnt
        If OverallSugesstionScoreForDatabaseSplit.Count > 1 Then
            decimalNumber = OverallSugesstionScoreForDatabaseSplit(1)
        End If

        Dim OverallSugesstionScoreForDatabase As String = wholeNumber & "." & decimalNumber

        Dim commandString As String = "UPDATE [User] SET OverallSuggestionScore = '" & OverallSugesstionScoreForDatabase & "' WHERE UserID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updatePracAveRating(ByVal PractitionerID As Integer, ByVal AveRating As Double)
        'Ave Rating is coming in with a comma and not a full stop so need to add the full stop
        Dim aveSplit As String() = AveRating.ToString.Split(",")
        Dim wholeNumber As String = aveSplit(0)
        Dim decimalNumber As String = 0
        'array already has 1 elemnt
        If aveSplit.Count > 1 Then
            decimalNumber = aveSplit(1)
        End If
        Dim AveNumForDatabase As String = wholeNumber & "." & decimalNumber

        Dim commandString As String = "UPDATE [Practitioner] SET AveRating = " & AveNumForDatabase & " WHERE PractitionerID=" & PractitionerID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateReviewBadgeStatus(ByVal userID As Integer, ByVal qualifier As String)
        Dim commandString As String = "UPDATE [Patient] SET MasterReviewer = '" & qualifier & "' WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Sub updateSuggesterBadgeStatus(ByVal userID As Integer, ByVal qualifier As String)
        Dim commandString As String = "UPDATE [Patient] SET MasterSuggester = '" & qualifier & "' WHERE PatientID=" & userID & ";"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Function goodTimeFrame(ByVal userID As Integer, ByVal incommingDate As String, ByVal incommingTime As String) As Boolean
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

        command.Connection.Close()
        command.Dispose()
        connection.Dispose()

        Return goodTime
    End Function
    'delete

End Class
