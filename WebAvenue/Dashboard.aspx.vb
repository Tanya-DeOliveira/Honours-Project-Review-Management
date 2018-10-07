Imports System.Data
Imports System.Data.SqlClient
Public Class Dashboard
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'display all the questions
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        If (userType = "Prac") Then
            btnAdd.Visible = False
            ddCategoryForQuestion.Visible = False
            txtAddQuestionTopic.Visible = False
            txtDescription.Visible = False
            lblCategoryLable.Text = "Practitioners cannot post Questions"
        ElseIf (userType = "Pat") Then

        End If

        'this is to load items into the dropdown lists on the page
        If (Not Page.IsPostBack) Then
            'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            'Dim commandString As String = "SELECT * FROM [CategoryType];"
            'Dim connection As SqlConnection = New SqlConnection(connectionString)
            'Dim command As SqlCommand = New SqlCommand()
            Dim reader As SqlDataReader
            'command.Connection = connection
            'command.CommandType = CommandType.Text
            'command.CommandText = commandString

            'connection.Open()
            reader = db.getCategoryTypes()

            Dim CategoryType As String = ""
            'filling out items into dropdown list for user to select category for question and also for user to filter questions
            While reader.Read
                CategoryType = reader("CategoryType")
                ddCategoryForQuestion.Items.Add(New ListItem(CategoryType, reader("CategoryTypeID")))
                Category.Items.Add(New ListItem(CategoryType, reader("CategoryTypeID")))
            End While

            reader.Close()
            db.closeReader()
            db.closeDB()

            'reader.Close()
            'connection.Close()
            'command.Dispose()
            'connection.Dispose()
        End If

        'load questions onto the page
        loadQuestions()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'will add a question to the database
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        'if session varriable is nothing, checking if user is logged in
        If Session("UserId") Is Nothing Then
            Response.Redirect("Login.aspx")
        Else
            Dim Question As String = txtAddQuestionTopic.Text
            Dim Description As String = txtDescription.Text
            Dim category As String = ddCategoryForQuestion.SelectedItem.Text
            'SQL database stores dates 
            'DATE - format YYYY-MM-DD
            'DateTime -Format(): YYYY-MM - DD HH: MI : SS
            'but my DB stores it as a string

            'question and categeory must be selected
            If category = "Select Category" Then
                lblMessage.Visible = True
                lblMessage.Text = "Please Select A Category your Question"
            ElseIf Question = "Type Question Here" Then
                lblMessage.Visible = True
                lblMessage.Text = "Please enter your Question in the textbox"
            ElseIf Description = "Type Description Here" Then
                lblMessage.Visible = True
                lblMessage.Text = "Please enter a Description for your Question in the textbox"
            Else
                Dim regDate As Date = Date.Now()
                Dim TodaysDate As String = regDate.ToString("dd\/MM\/yyyy")
                Dim TodaysTime As String = regDate.ToString("HH:mm:ss")

                'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

                'Dim commandString As String = "INSERT INTO [Question] (PatientID,Question,Description,Date,Time,Popularity,Remove) VALUES ('" & userID & "','" & Question & "','" & Description & "','" & TodaysDate & "','" & TodaysTime & "','0','N') SELECT SCOPE_IDENTITY() AS id;"
                'Dim connection As SqlConnection = New SqlConnection(connectionString)
                'Dim command As SqlCommand = New SqlCommand()
                Dim reader As SqlDataReader

                Dim QuestionID As Integer

                'command.Connection = connection
                'command.CommandType = CommandType.Text
                'command.CommandText = commandString

                'connection.Open()
                reader = db.addQuestion(userID, Question, Description, TodaysDate, TodaysTime)
                'get ID of Question
                If (reader.HasRows()) Then
                    reader.Read()
                    QuestionID = reader("id")
                End If
                reader.Close()
                db.closeReader()

                db.closeDB()
                'connection.Dispose()
                'command.Dispose()
                'connection.Close()

                AddCategorysToDB(QuestionID, ddCategoryForQuestion.SelectedItem.Value)

                lblMessage.Visible = True
                lblMessage.Text = "Question has been posted"
            End If
        End If
        'reload the page after poasting a question
        'Me.Page_Load(sender, e)
    End Sub

    Private Sub AddCategorysToDB(ByVal questionID As Integer, ByVal CategoryTypeID As Integer)
        db.addQuestionsCategory(questionID, CategoryTypeID)

        db.closeDB()
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "INSERT INTO [MedicalCategory] (QuestionID,CategoryTypeID) VALUES ('" & questionID & "','" & CategoryTypeID & "');"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()

        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        'command.ExecuteNonQuery()

        'connection.Dispose()
        'command.Dispose()
        'connection.Close()
    End Sub

    Private Sub loadQuestions()
        'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        'Dim commandString As String = "SELECT * FROM [Question];"
        'Dim connection As SqlConnection = New SqlConnection(connectionString)
        'Dim command As SqlCommand = New SqlCommand()
        Dim reader As SqlDataReader
        'command.Connection = connection
        'command.CommandType = CommandType.Text
        'command.CommandText = commandString

        'connection.Open()
        reader = db.getQuestions

        'displays all the questions on the page
        While reader.Read
            lblDisplayQuestions.Text &= "<div Class='card border-secondary mb-3'>"
            lblDisplayQuestions.Text &= "<div Class='card-header'><b>" & reader("Question") & "</b></div>"
            lblDisplayQuestions.Text &= "<div Class='card-body text-secondary'>"
            lblDisplayQuestions.Text &= "<h5 Class='card-title'>" & db.getPatientName(reader("PatientID")) & "</h5>"
            lblDisplayQuestions.Text &= "<p Class='card-text'>" & ShortenDescription(reader("Description")) & "<br/>"
            'use popularity as the number of suggestions
            lblDisplayQuestions.Text &= "<a href = 'Suggestions.aspx?Question=" & reader("QuestionID") & "' Class='lk badge badge-dark'>View Suggestions <span Class='badge badge-dark'>" & reader("Popularity") & " Suggestions</span></a></p>"
            lblDisplayQuestions.Text &= "<p Class='card-text'><small class='text-muted'>" & reader("Date") & " @ " & reader("Time") & "</small></p></div></div><br/>"
        End While

        reader.Close()

        db.closeReader()
        db.closeDB()
        'connection.Close()
        'command.Dispose()
        'connection.Dispose()
    End Sub

    'Private Function getPatientName(ByVal patientID As Integer) As String
    '    Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

    '    Dim commandString As String = "SELECT [User].Name, [User].Surname FROM [Patient] INNER JOIN [User] ON Patient.PatientID = [User].UserID WHERE [Patient].PatientID=" & patientID & ";"
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

    Public Function ShortenDescription(ByVal description As String)
        'will only display 17 words 
        Dim descriptionArray() As String = description.Split(" ")
        Dim shortedDescription As String = ""

        If descriptionArray.Length > 16 Then
            For i As Integer = 0 To 16
                shortedDescription &= descriptionArray(i) & " "
            Next
            shortedDescription &= "..."
        Else
            'if it is already short then you cant shorten it
            shortedDescription = description
        End If

        Return shortedDescription
    End Function
End Class