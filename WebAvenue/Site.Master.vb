Public Class SiteMaster
    Inherits MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim userName As String = Session("Username")
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        If userName IsNot Nothing Then
            If (userType = "Pat") Then
                Dashboardlink.Visible = True
                viewProfilelink.Visible = True
                QuestionsAsked.Visible = True
                loginlink.Visible = False
                logoutlink.Visible = True
                txtSearch.Visible = True
                btnSearch.Visible = True
            ElseIf (userType = "Prac") Then
                Dashboardlink.Visible = True
                viewProfilelink.Visible = True
                loginlink.Visible = False
                logoutlink.Visible = True
                'txtSearch.Visible = True
                'btnSearch.Visible = True
            ElseIf (userType = "A") Then
                Dashboardlink.Visible = True
                logoutlink.Visible = True
            End If
        Else
            logoutlink.Visible = False
        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Response.Redirect("Search.aspx")
    End Sub
End Class