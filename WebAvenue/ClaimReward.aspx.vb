Public Class ClaimReward
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userID As String = Session("UserId")

        Dim RewardID As String = Request.QueryString("RewardID")

        If (RewardID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        'need to get reward details
        Dim rewardDetails As MediAvenueDatabase.Reward = New MediAvenueDatabase.Reward

        rewardDetails = db.getRewardDetail(RewardID)

        lblTo.Text = db.getPatientName(rewardDetails.PatientID)
        lblRewardName.Text = rewardDetails.Name
        lblDate.Text = rewardDetails.DateUploaded
        lblTime.Text = rewardDetails.TimeUploaded

        If rewardDetails.Claimed = "Y" Then
            btnClaim.Enabled = False
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Rewards.aspx")
    End Sub

    Protected Sub btnClaim_Click(sender As Object, e As EventArgs) Handles btnClaim.Click
        Dim userID As String = Session("UserId")
        Dim RewardID As String = Request.QueryString("RewardID")

        If (RewardID Is Nothing) Then
            Response.Redirect("Logout.aspx")
        End If

        'update table to say user has claimed reward
        db.updateRewardDetail(RewardID)

        lblMessage.Text = "You have claimed the reward"
        btnClaim.Enabled = False
    End Sub
End Class