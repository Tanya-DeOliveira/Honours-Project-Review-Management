Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'if Session("UserID").ToString isnull then log in else you allowed to post question
        'if session varriable is not nothing
        'If Not IsNothing(Session("UserID").ToString) Then
        If Not Session("UserId") Is Nothing Then
            Session.Remove("UserId")
            Session.Remove("Username")
            Session.Remove("UserType")
            Response.Redirect("Login.aspx")
        End If
    End Sub

End Class