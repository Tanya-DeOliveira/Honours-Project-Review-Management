<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PractitionerProfile.aspx.vb" Inherits="WebAvenue.PractitionerProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .jumbotron {
        border: 1px solid #9d478e !important; 
        color:#9d478e;
        background-color: #deb0d7 !important;
     }
    .badge-secondary {
        border: 1px solid #B463A6 !important; 
        background-color:#B463A6 !important; 
    }
    .badge {
        background-color:#3B475E;
    }
    .card {
        border: 1px solid #9d478e !important; 
    }
    .btn-outline-primary {
        border: 1px solid #B463A6 !important; 
        color:#5C4978;
     }
    a.btn:hover{
     text-decoration: underline;
     color: #3B475E;
     background-color:transparent;
    }
    .lk:hover, .lk:active {
        background-color: #B463A6 !important;
    }
    .card-footer {
        border: 1px solid #B463A6 !important; 
    }
    .card-header {
        border: 1px solid #B463A6 !important; 
        color:#B463A6;
    }
    a:link {
        color:#5C4978;
    }
    a:hover,.a:active {
    text-decoration: underline;
    color:#5C4978;
    }
    a:visited {
    color: #5C4978;
    }
</style>
<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="display-4"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></h1><span class="badge badge-dark">5 star</span><span class="badge badge-secondary">Nurse</span>
    <p class="lead"><asp:Label ID="lblBio" runat="server" Text=""></asp:Label></p>
  </div>
</div>
<div class="card">
  <div class="card-body">
    <h4 class="card-title">About</h4>
    <p class="card-text"> 
        <b>Specialization in Medical Field:</b> <asp:Label ID="lblSpecialization" runat="server" Text=""></asp:Label><br/>
        <b>Telephone Number:</b> <asp:Label ID="lblTelephone" runat="server" Text=""></asp:Label><br/>
        <b>Location:</b> <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label><br/>
        <b>Years of Experience:</b> <asp:Label ID="lblExperiance" runat="server" Text=""></asp:Label><br/>
    </p>
  </div>
</div>
<br/>
<div class="card border-secondary mb-3">
  <div class="card-header">
    <b>Rate And Review <asp:Label ID="lblNameSecond" runat="server" Text=""></asp:Label></b>
  </div>
    <ul class="list-group list-group-flush">
        <asp:Label ID="lblReviews" runat="server" Text=""></asp:Label>
  </ul>
 
  <div class="card-footer text-muted">
    <div class="container">
        <div class="row">
            <div class="col col-lg-6">
                <asp:TextBox ID="txtAddReview" Style="max-width:100%; border: 1px solid #B463A6 !important;" class="form-control" runat="server"></asp:TextBox> 
            </div>
            <div class="col col-lg-4">
                <asp:Label ID="lblRating" runat="server" Text="Choose a Rating: "></asp:Label>
                <asp:DropDownList ID="ddRating" CssClass="btn-outline-primary dropdown-item" runat="server">
                    <asp:ListItem>Select Rating</asp:ListItem>
                </asp:DropDownList>
                
                <asp:Button ID="btn1" Visible="false" class="btn btn-secondary" runat="server" Text="1" />
                <asp:Button ID="btn2" Visible=" false" class="btn btn-secondary" runat="server" Text="2" />
                <asp:Button ID="btn3" Visible="false" class="btn btn-secondary" runat="server" Text="3" />
                <asp:Button ID="btn4" Visible="false" class="btn btn-secondary" runat="server" Text="4" />
                <asp:Button ID="btn5" Visible="false" class="btn btn-secondary" runat="server" Text="5" />
            </div>
            <div class="col col-lg-2">
                <asp:Button ID="btnAddReview" class="lk btn btn-outline-primary" runat="server" Text="Add Review" /><br/>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</div>
</div>
    <asp:Button ID="btnBack" class="btn btn-dark" runat="server" Text="Back" />
</asp:Content>
