<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ViewProfile.aspx.vb" Inherits="WebAvenue.ViewProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .jumbotron {
        border: 1px solid #9d478e !important; 
        color:#9d478e;
        background-color: #deb0d7 !important;
     }
    .badge {
        background-color:#3B475E;
    }
    .b {
       border: 1px solid whitesmoke !important; 
        background-color:whitesmoke !important; 
        color:darkgoldenrod;
        box-shadow:0px 2px 5px #808080;
    }
    .card {
        border: 1px solid #9d478e !important; 
    }
    .card-title {
        color:#5C4978;
    }
</style>

<div class="container">
    <div class="row">
         <div class="col col-lg-4">
         </div>
        <div class="col col-lg-4">
            <div class="jumbotron jumbotron-fluid">
                <div class="container">
                    <h1 class="display-4">Your Profile</h1>
                        <p class="lead">
                            <%-- might need to use AutoPostBack="True" to modify profile --%>
                            <small class="text-muted">Details not visiable to others:</small><br/>
                            <asp:Label ID="Label1" runat="server" Text="Cellphone Number:"></asp:Label><br/>
                            <asp:TextBox ID="txtCellphone" Text="082 234 5678" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="txt form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Label ID="Label2" runat="server" Text="Email:"></asp:Label><br/>
                            <asp:TextBox ID="txtEmail" Text="bwhite@gmail.com" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass=" txt form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <%-- for practitioner --%>
                            <small class="text-muted">Details visiable to others:</small><br/>
                            <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label><br/>
                            <asp:TextBox ID="txtName" Text="Betty" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Label ID="Label4" runat="server" Text="Surname:"></asp:Label><br/>
                            <asp:TextBox ID="txtSurname" Text="White" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Label ID="lblSpecialization" runat="server" Visible="false" Text="Specialization in Medical Field:"></asp:Label><br/>
                            <asp:TextBox ID="txtSpecialization" Visible="false" Text="General Practitioner" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Label ID="lblTelephone" runat="server" Visible="false" Text="Telephone Number:"></asp:Label><br/>
                            <asp:TextBox ID="txtTelephone" Visible="false" Text="011 945 6734" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Label ID="lblAddress" Visible="false" runat="server" Text="Adress of Place of Practice:"></asp:Label><br/>
                            <asp:TextBox ID="txtAddress" Visible="false" Text="305 Calico Drive, Johannesburg, 1792" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Label ID="lblExperiance" Visible="false" runat="server" Text="Years of Experience:"></asp:Label><br/>
                            <asp:TextBox ID="txtExperience" Visible="false" text="15" Style="border: 1px solid #B463A6 !important; background-color:#edd4e9;" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox><br/>
                            <asp:Button ID="btnChangePassword" CssClass="btn btn-dark" runat="server" Text="Change Password" /><br/>
                            <asp:Button ID="btnModify" CssClass="btn btn-dark" runat="server" Text="Modify Profile" /><asp:Button ID="btnSave" Visible="false" CssClass="btn btn-dark" runat="server" Text="Save" /><br/>
                            <asp:Label ID="lblSuccess" CssClass="alert-info" runat="server" Text=""></asp:Label>
                        </p>
                </div>
            </div>
        </div>
        <div class="col col-lg-4">
            <br/>
            <div class="card" style="width: 18rem;">
                <div class="card-body">
                    <h5 class="card-title">Badges Earned</h5>
                    <p class="card-text">
                        <asp:Label ID="lblNoBadge" Visible="false" runat="server" Text="No Badges"></asp:Label>
                        <asp:Label ID="lblMasterSuggester" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblMasterReviewer" runat="server" Text="Label"></asp:Label>
                    </p>
                    <asp:Button ID="btnRewards" runat="server" CssClass="btn btn-dark" Text="View Rewards" />
                </div>
            </div>
        </div>
    </div>
</div>
    
</asp:Content>
