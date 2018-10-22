Public Class GiveReward
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserID As String = Request.QueryString("UserID")
        If (UserID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        lblUserName.Text = db.getPatientName(UserID)
    End Sub

    Protected Sub btnAddReward_Click(sender As Object, e As EventArgs) Handles btnAddReward.Click
        Dim UserID As String = Request.QueryString("UserID")
        If (UserID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If
        'admin will give a reward to a user

        'getting reward name from user
        Dim RewardName As String = "Unknown"
        RewardName = txtRewardName.Text

        'getting date the reward was given
        Dim regDate As Date = Date.Now()
        Dim TodaysDate As String = regDate.ToString("dd\/MM\/yyyy")
        Dim TodaysTime As String = regDate.ToString("HH:mm:ss")

        'insert reward into databse
        db.addReward(UserID, RewardName, TodaysDate, TodaysTime)

        lblMessage.Text = "Reward issued to user"
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Admin.aspx")
    End Sub
End Class