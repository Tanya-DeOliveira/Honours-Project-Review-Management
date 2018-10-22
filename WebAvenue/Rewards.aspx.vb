Public Class Rewards
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'need to get and claim rewards
        Dim userID As String = Session("UserId")
        Dim claimed As String = "No"

        'display rewards
        Dim RewardList As ArrayList = New ArrayList()

        RewardList = db.getUserRewards(userID)

        'to reset the lable 
        lblRewards.Text = ""

        If RewardList.Count > 0 Then
            For Each Reward In RewardList
                If Reward.Claimed = "N" Then
                    claimed = "No"
                ElseIf Reward.Claimed = "Y" Then
                    claimed = "Yes"
                End If
                lblRewards.Text &= "<tr>
                                        <td>" & Reward.Name & "</td>
                                        <td>" & Reward.DateUploaded & "</td>
                                        <td>" & Reward.TimeUploaded & "</td>
                                        <td>" & claimed & "</td>
                                        <td><a href='ClaimReward.aspx?RewardID=" & Reward.RewardID & "'>Click Here to Redeam</a></td>
                                    </tr>"
            Next Reward
        Else
            lblRewards.Text = "<tr>
                                        <td>No Rewards To Claim</td>
                               <tr/>"
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("ViewProfile.aspx")
    End Sub
End Class