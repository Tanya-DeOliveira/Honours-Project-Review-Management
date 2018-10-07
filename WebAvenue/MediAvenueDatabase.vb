Imports System.Data.SqlClient
Public Class MediAvenueDatabase
    'variables
    Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    Dim commandString As String 'SQL statment
    Dim connection As SqlConnection
    Dim command As SqlCommand = New SqlCommand()
    Dim reader As SqlDataReader

    Public consistency As Boolean = False
    'constructor
    Public Sub New()

    End Sub

    'methods
    Public Sub closeDB()
        'method permanently close and removes connection object from memory and the resource no longer exists for any further 
        connection.Dispose()
        command.Dispose()
        connection.Close()
    End Sub

    Public Sub closeReader()
        reader.Close()
    End Sub

    Public Sub initaliseConnection()
        connection = New SqlConnection(connectionString)
    End Sub

    'select Statments
    'get everything from category table
    Public Function getCategoryTypes() As SqlDataReader
        initaliseConnection()
        commandString = "SELECT * FROM [CategoryType];"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        'reader.Close()
        'closeDB()
        Return reader
    End Function
    'get all the questions in questions table
    Public Function getQuestions() As SqlDataReader
        initaliseConnection()
        commandString = "SELECT * FROM [Question];"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Return reader
    End Function
    'get the patients name
    Public Function getPatientName(ByVal patientID As Integer) As String
        initaliseConnection()
        commandString = "SELECT [User].Name, [User].Surname FROM [Patient] INNER JOIN [User] ON Patient.PatientID = [User].UserID WHERE [Patient].PatientID=" & patientID & ";"

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
        connection.Close()
        command.Dispose()
        connection.Dispose()
        Return name
    End Function

    'get practioners details for profile
    Public Function getPracProfileDetails(ByVal ProfileID As Integer) As SqlDataReader
        initaliseConnection()
        commandString = "SELECT * FROM ([User] INNER JOIN [Practitioner] ON [User].UserID = [Practitioner].PractitionerID) WHERE [Practitioner].PractitionerID='" & ProfileID & "';"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Return reader
    End Function
    'get all the reviews for a practitiner
    Public Function getReviews(ByVal ProfileID As Integer) As SqlDataReader
        initaliseConnection()
        commandString = "SELECT * FROM [Review] WHERE PractitionerID=" & ProfileID & " ORDER BY OverallScore DESC;"
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Return reader
    End Function

    'get a users name
    Public Function getUsersName(ByVal UserID As Integer) As String
        initaliseConnection()
        commandString = "SELECT [User].Name, [User].Surname FROM [User] WHERE UserID=" & UserID & ";"

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
        connection.Close()
        command.Dispose()
        connection.Dispose()

        Return name
    End Function
    'get everything from rating consistancy table
    Public Function getRatingConsistencys(ByVal userID As Integer)
        initaliseConnection()
        commandString = "SELECT * FROM [RatingConsistency] WHERE PatientID ='" & userID & "';"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        reader = command.ExecuteReader()

        Return reader
    End Function
    'get Time difference between reviews user made
    'Public Function goodTimeFrame(ByVal userID As Integer, ByVal incommingDate As String, ByVal incommingTime As String) As Boolean
    '    Dim goodTime As Boolean = True
    '    Dim regDate As Date = Date.Now()
    '    Dim previousDate As String = regDate.ToString("dd\/MM\/yyyy")
    '    Dim previousTime As String = regDate.ToString("HH:mm:ss")
    '    Dim counter As Integer = 1
    '    Dim previousTimeArray() As String
    '    Dim nextTimeArray() As String
    '    Dim previousHour As String
    '    Dim previousMinute As String
    '    Dim nextHour As String
    '    Dim nextMinute As String
    '    Dim TimeDifference As Integer

    '    Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    '    Dim commandString As String = "SELECT Date,Time FROM [Review] WHERE PatientID=" & userID & ";"
    '    Dim connection As SqlConnection = New SqlConnection(connectionString)
    '    Dim command As SqlCommand = New SqlCommand()
    '    Dim reader As SqlDataReader

    '    command.Connection = connection
    '    command.CommandType = CommandType.Text
    '    command.CommandText = commandString

    '    connection.Open()
    '    reader = command.ExecuteReader()

    '    If reader.HasRows Then
    '        While (reader.Read())
    '            previousDate = reader("Date")
    '            previousTime = reader("Time")
    '            If previousDate = incommingDate Then
    '                'can break up time into a string
    '                previousTimeArray = previousTime.Split(":")
    '                previousHour = previousTimeArray(0)
    '                previousMinute = previousTimeArray(1)
    '                nextTimeArray = incommingTime.Split(":")
    '                nextHour = nextTimeArray(0)
    '                nextMinute = nextTimeArray(1)
    '                If previousHour = nextHour Then
    '                    TimeDifference = CInt(nextMinute) - CInt(previousMinute)
    '                    'if next review was written 5 minutes later
    '                    If TimeDifference < 5 Then
    '                        goodTime = False
    '                    End If
    '                End If
    '            End If
    '        End While
    '    End If
    '    reader.Close()
    '    connection.Close()
    '    command.Dispose()
    '    connection.Dispose()

    '    Return goodTime
    'End Function

    'insert statments 
    'add question to the Question table
    Public Function addQuestion(ByVal userID As Integer, ByVal Question As String, ByVal Description As String, ByVal TodaysDate As String, ByVal TodaysTime As String) As SqlDataReader
        initaliseConnection()
        commandString = "INSERT INTO [Question] (PatientID,Question,Description,Date,Time,Popularity,Remove) 
                         VALUES ('" & userID & "', @Question, @Description,'" & TodaysDate & "','" & TodaysTime & "','0','N') SELECT SCOPE_IDENTITY() AS id;"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@Question", Question)
        command.Parameters.AddWithValue("@Description", Description)

        connection.Open()
        reader = command.ExecuteReader()

        Return reader
    End Function

    'add category of question to database
    Public Sub addQuestionsCategory(ByVal questionID As Integer, ByVal CategoryTypeID As Integer)
        initaliseConnection()
        commandString = "INSERT INTO [MedicalCategory] (QuestionID,CategoryTypeID) 
                         VALUES ('" & questionID & "','" & CategoryTypeID & "');"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()
    End Sub

    'add a review for a practioner 
    Public Function addReview(ByVal ProfileID As Integer, ByVal userID As Integer, ByVal review As String, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String, ByVal reviewOriginalScore As Integer) As SqlDataReader
        initaliseConnection()
        commandString = "INSERT INTO [Review] (PractitionerID,PatientID,Review,PracRating,Date,Time,NumLikes,NumDislikes,OverallScore,OriginalPoint,ExtraPoint) 
                                       VALUES ('" & ProfileID & "','" & userID & "',@review ,@ratingReview ,'" & TodaysDate & "','" & TodaysTime & "','0','0','" & reviewOriginalScore & "','" & reviewOriginalScore & "','0') SELECT SCOPE_IDENTITY() AS id;"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@review", review)
        command.Parameters.AddWithValue("@ratingReview", rating)

        connection.Open()
        reader = command.ExecuteReader()

        Return reader
    End Function

    'add a rating for a practioner
    Public Sub addRating(ByVal patientID As Integer, ByVal PractitionerID As Integer, ByVal reviewID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
        initaliseConnection()
        commandString = "INSERT INTO [Rating] (PractitionerID,PatientID,ReviewID,Rating,Date,Time) 
                         VALUES ('" & PractitionerID & "','" & patientID & "','" & reviewID & "', @rating,'" & TodaysDate & "','" & TodaysTime & "');"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        command.ExecuteNonQuery()

        'need to calculate Pracs new rating in a new sub!!

        connection.Dispose()
        command.Dispose()
        connection.Close()
    End Sub

    'update
    'update Rating Consistency table when user uses the same rating consistency
    'updating database to reset the count because they have reached 3
    Public Sub updateRatingConsistency(ByVal userID As Integer, ByVal ratingNum As Integer)
        initaliseConnection()
        commandString = "UPDATE [RatingConsistency] SET RateNum = '" & ratingNum & "' WHERE PatientID=" & userID & ";"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        connection.Open()
        command.ExecuteNonQuery()

        connection.Dispose()
        command.Dispose()
        connection.Close()
    End Sub

    Public Sub updateRatingConsistencyWithRating(ByVal userID As Integer, ByVal ratingNum As Integer, ByVal rating As Integer)
        initaliseConnection()
        commandString = "UPDATE [RatingConsistency] SET RateNum='1', Rating= @rating WHERE PatientID=" & userID & ";"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        command.ExecuteNonQuery()

        connection.Dispose()
        command.Dispose()
        connection.Close()
    End Sub

    Public Sub insertNewUserToRatingConsistency(ByVal userID As Integer, ByVal rating As Integer, ByVal TodaysDate As String, ByVal TodaysTime As String)
        initaliseConnection()
        commandString = "INSERT INTO [RatingConsistency] (PatientID,RateNum,Rating,Date,Time) 
                         VALUES ('" & userID & "','1', @rating,'" & TodaysDate & "','" & TodaysTime & "');"

        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.Parameters.AddWithValue("@rating", rating)

        connection.Open()
        command.ExecuteNonQuery()

        connection.Dispose()
        command.Dispose()
        connection.Close()
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
    'delete

End Class
