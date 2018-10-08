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
            Dim CategoryTypesList As ArrayList = New ArrayList()
            'Dim CategoryType As CategoryType = New CategoryType
            'Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

            'Dim commandString As String = "SELECT * FROM [CategoryType];"
            'Dim connection As SqlConnection = New SqlConnection(connectionString)
            'Dim command As SqlCommand = New SqlCommand()
            'Dim reader As SqlDataReader
            'command.Connection = connection
            'command.CommandType = CommandType.Text
            'command.CommandText = commandString

            'connection.Open()
            CategoryTypesList = db.getCategoryTypes()

            'Dim CategoryType As String = ""
            'filling out items into dropdown list for user to select category for question and also for user to filter questions
            For Each CategoryType In CategoryTypesList
                'CategoryType = reader("CategoryType")
                ddCategoryForQuestion.Items.Add(New ListItem(CategoryType.CategoryType, CategoryType.CategoryTypeID))
                Category.Items.Add(New ListItem(CategoryType.CategoryType, CategoryType.CategoryTypeID))
            Next CategoryType

            'reader.Close()
            'db.closeReader()
            'db.closeDB()

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

                'Dim commandString As String = "INSERT INTO [Question] (PatientID,Question,Description,Date,Time,Popularity,Remove) VALUES ('" & userID & "','" & Question & "','" & Description & "','" & TodaysDate & "','" & TodaysTime & "','0','N') SELECT SCOPE_IDENTITY() AS id;"

                Dim QuestionID As Integer

                'add question and get ID of Question
                QuestionID = db.addQuestion(userID, Question, Description, TodaysDate, TodaysTime)

                'If (reader.HasRows()) Then
                '    reader.Read()

                'End If

                AddCategorysToDB(QuestionID, ddCategoryForQuestion.SelectedItem.Value)

                lblMessage.Visible = True
                lblMessage.Text = "Question has been posted"

                'reload the page after poasting a question
                Me.Page_Load(sender, e)
            End If
        End If
    End Sub

    Private Sub AddCategorysToDB(ByVal questionID As Integer, ByVal CategoryTypeID As Integer)
        db.addQuestionsCategory(questionID, CategoryTypeID)
        'Dim commandString As String = "INSERT INTO [MedicalCategory] (QuestionID,CategoryTypeID) VALUES ('" & questionID & "','" & CategoryTypeID & "');"
    End Sub

    Private Sub loadQuestions()
        Dim QuestionsList As ArrayList = New ArrayList()
        'Dim commandString As String = "SELECT * FROM [Question];"

        QuestionsList = db.getQuestions
        'displays all the questions on the page
        'reset lable
        lblDisplayQuestions.Text = ""
        If QuestionsList.Count > 0 Then
            For Each Question In QuestionsList
                lblDisplayQuestions.Text &= "<div Class='card border-secondary mb-3'>"
                lblDisplayQuestions.Text &= "<div Class='card-header'><b>" & Question.Question & "</b></div>"
                lblDisplayQuestions.Text &= "<div Class='card-body text-secondary'>"
                lblDisplayQuestions.Text &= "<h5 Class='card-title'>" & db.getPatientName(Question.PatientID) & "</h5>"
                lblDisplayQuestions.Text &= "<p Class='card-text'>" & ShortenDescription(Question.Description) & "<br/>"
                'use popularity as the number of suggestions
                lblDisplayQuestions.Text &= "<a href = 'Suggestions.aspx?Question=" & Question.QuestionID & "' Class='lk badge badge-dark'>View Suggestions <span Class='badge badge-dark'>" & Question.Popularity & " Suggestions</span></a></p>"
                lblDisplayQuestions.Text &= "<p Class='card-text'><small class='text-muted'>" & Question.DateUploaded & " @ " & Question.TimeUploaded & "</small></p></div></div><br/>"
            Next Question
        Else
            'no questions to display
            lblDisplayQuestions.Text &= "<div Class='card border-secondary mb-3'>"
            lblDisplayQuestions.Text &= "<div Class='card-body text-secondary'>"
            lblDisplayQuestions.Text &= "<p Class='card-text'>No Questions to Display<br/>"
            lblDisplayQuestions.Text &= "</div></div><br/>"
        End If

        'While reader.Read

        'End While
    End Sub

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