Imports System.Data
Imports System.Data.SqlClient

Public Class ViewProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        btnSave.Visible = False
        lblSuccess.Visible = False

        If (Not Page.IsPostBack) Then
            If (userType = "Prac") Then
                lblSpecialization.Visible = True
                lblTelephone.Visible = True
                lblAddress.Visible = True
                lblExperiance.Visible = True

                txtSpecialization.Visible = True
                txtTelephone.Visible = True
                txtAddress.Visible = True
                txtExperience.Visible = True
            ElseIf (userType = "Pat") Then
                CheckBadges(userID)
            End If

            Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

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
                txtCellphone.Text = reader("ContactNum")
                txtEmail.Text = reader("Email")
                txtName.Text = reader("Name")
                txtSurname.Text = reader("Surname")
            End If

            reader.Close()
            connection.Dispose()
            command.Dispose()
            connection.Close()
        End If
    End Sub

    Private Sub CheckBadges(ByVal userID As Integer)
        Dim masterReviewer As String = "N"
        Dim masterSuggester As String = "N"

        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString

        Dim commandString As String = "SELECT * FROM [Patient] WHERE PatientID=" & userID & ";"
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
            masterReviewer = reader("MasterReviewer")
            masterSuggester = reader("MasterSuggester")
        End If
        reader.Close()
        connection.Dispose()
        command.Dispose()
        connection.Close()

        If masterReviewer = "N" Then
            'dont have the badge
            lblMasterReviewer.Visible = False
        Else
            'have the badge
            lblMasterReviewer.Visible = True
            lblMasterReviewer.Text = " <span class='b badge badge-secondary'>Master Reviewer</span>"
        End If

        If masterSuggester = "N" Then
            'dont have the badge
            lblMasterSuggester.Visible = False
        Else
            'have the badge
            lblMasterSuggester.Visible = True
            lblMasterSuggester.Text = "<span Class='b badge badge-dark'>Master Suggester</span><br/>"
        End If

        If masterReviewer = "N" And masterSuggester = "N" Then
            lblNoBadge.Visible = True
        End If
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        txtCellphone.Enabled = True
        txtEmail.Enabled = True
        txtName.Enabled = True
        txtSurname.Enabled = True
        txtSpecialization.Enabled = True
        txtTelephone.Enabled = True
        txtAddress.Enabled = True
        txtExperience.Enabled = True
        btnSave.Visible = True
        btnModify.Visible = False
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim cellphone As String = txtCellphone.Text
        Dim email As String = txtEmail.Text
        Dim name As String = txtName.Text
        Dim surname As String = txtSurname.Text
        Dim specilaization As String = txtSpecialization.Text
        Dim telephone As String = txtTelephone.Text
        Dim address As String = txtAddress.Text
        Dim experiance As String = txtExperience.Text

        Dim connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("MediAvenueConnectionString").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim commandString As String

        connection.Open()

        If userType = "Prac" Then

            commandString = "UPDATE [Practitioner] SET Specialization ='" & specilaization.ToString & "', Address='" & address.ToString & "', YearsOfExperiance='" & experiance.ToString & "', Telephone='" & telephone.ToString & "' WHERE [Practitoner].PractitionerID= '" & userID & "';"

            command.Connection = connection
            command.CommandType = CommandType.Text
            command.CommandText = commandString

            command.ExecuteNonQuery()

        End If

        commandString = "UPDATE [User] SET Name='" & name.ToString & "', Surname='" & surname.ToString & "', ContactNum='" & cellphone.ToString & "', Email='" & email.ToString & "' WHERE [User].UserID= '" & userID & "';"
        command.Connection = connection
        command.CommandType = CommandType.Text
        command.CommandText = commandString

        command.ExecuteNonQuery()


        connection.Close()
        command.Dispose()
        connection.Dispose()

        lblSuccess.Visible = True
        lblSuccess.Text = "Changes Are Saved"

        txtCellphone.Enabled = False
        txtEmail.Enabled = False
        txtName.Enabled = False
        txtSurname.Enabled = False
        txtSpecialization.Enabled = False
        txtTelephone.Enabled = False
        txtAddress.Enabled = False
        txtExperience.Enabled = False
        btnSave.Visible = False
        btnModify.Visible = True
    End Sub

    Protected Sub btnRewards_Click(sender As Object, e As EventArgs) Handles btnRewards.Click
        Response.Redirect("Rewards.aspx")
    End Sub
End Class