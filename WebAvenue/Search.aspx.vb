Public Class Search
    Inherits System.Web.UI.Page

    Private db As MediAvenueDatabase = New MediAvenueDatabase()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userID As String = Session("UserId")
        Dim userType As String = Session("UserType")

        Dim searchResult As String = Request.QueryString("Search")

        If (searchResult Is Nothing) Then
            lblNoSearch.Visible = True
        End If

        'populating filters
        Dim OccupationList As ArrayList = New ArrayList()
        OccupationList = db.getAllPracsOccupations()
        Dim count As Integer = 0

        'filling out items into dropdown list for user to select category for searching for a practitioner
        For Each occupation In OccupationList
            count = count + 1
            MedicalCategory.Items.Add(New ListItem(occupation, count))
        Next occupation

        If (userType = "Pat") Then
            'show practitioners in search
            Dim PractitionerList As ArrayList = New ArrayList()
            PractitionerList = db.getAllPractitioners(searchResult)

            lblSearchResults.Text = ""
            If PractitionerList.Count > 0 Then

                For Each practitioner In PractitionerList
                    'display search results
                    lblSearchResults.Text &= "<tr>
                                                <td><a href = 'PractitionerProfile.aspx?Profile=" & practitioner.PractitionerID & "'> Dr. " & db.getUsersName(practitioner.PractitionerID) & "</a> </td>
                                                <td>" & practitioner.Address & " </td>
                                                <td>" & practitioner.YearsOfExperiance & " </td>
                                                <td>" & practitioner.Specialization & " </td>
                                                <td>" & practitioner.AveRating & " </td>
                                              </tr>"
                Next practitioner
            Else
                'no practitioners to display
                lblSearchResults.Text &= "<tr>
                                             <td>No Results to Display</td>
                                         </tr>"
            End If
            'practitioners are not able to do searches
            'ElseIf (userType = "Prac") Then
            '    'show patients in seach

        End If
    End Sub

End Class