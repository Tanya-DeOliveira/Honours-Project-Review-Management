<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="WebAvenue._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
<style>
    .navbar {
      margin-bottom: 0;
      background-color: white !important;
      border: 0;
      opacity: 0.9 !important;
      }
    .nav-link {
      color:black !important;
    }
    .btn-outline-light {
      background-color: white;
      color: black;
      border: 2px solid #555555;
    }
  .btn-outline-light:hover {
    background-color: #555555;
    color: white;
    border-color:#555555;
    }
	body, html {
    height: 100%;
    margin: 0;
    }
    body {
    /* The image used */
    background-image: url("Media/flower.jpg"); 
    /* Center and scale the image nicely */
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    }
    .jumbotron {
      background-color: white !important;
      border: 0;
      opacity: 0.9 !important;
      padding-bottom: 0;
     padding-top: 2em;
    }
    .navbar-brand {
      color:#c97cbc !important;
    }
    .display-4 {
        font-size: 40px !important;
    }
</style>
 <br/>
 <div class="container">
  <div class="row">
    <div class="col-sm-4">
        <div class="jumbotron">
            <h2 class="display-4">Sign Up Here</h2>
            <p class="lead">
                Become apart of the communicty and join as a patient or practitioner.</p>
                <hr class="my-4">
            <p>
            <asp:TextBox ID="txtName" Text="Name" class="form-control" runat="server"></asp:TextBox>  
            <asp:TextBox ID="txtSurname" Text="Surname" class="form-control" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtUsername" Text="Username" class="form-control" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtCellphone" Text="Contact Number" class="form-control"  runat="server"></asp:TextBox> <%--for practitioner, do a pop up if they havnt filled in the extra infor of tellphone number and years experiance etc--%>
            <asp:TextBox ID="txtEmail" Text="Email" class="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Confrim Password"></asp:Label>
            <asp:TextBox ID="txtConfPass" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
            <br/>
            <asp:CheckBox ID="isPractitioner" Text=" Sign Up as Practitioner" class="form-control" Style="width:280px;"  runat="server" />
            <asp:Button ID="btnRegister" CssClass="btn btn-outline-dark btn-block" runat="server" Text="Sign Up" />
            <br/>
            <asp:Label ID="lblErr" Visible="false" CssClass="alert-danger" runat="server" Text=""></asp:Label>
            </p>
        </div>
    </div>
    <div class="col-8">
        <div class="container">
  <div class="row">
        <div class="col-2">
        </div>
        <div class="col-8 text-center">
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
        Looking to get accurate Medical Advice?<br/>
        If that is you then you have come to the right place.
        MediAvenue has been designed specially to let both Practitoners and Patients communicate
        with each other. You can fish for the best Docotor for the problem you are experiencing all by asking others for advice 
        or simply looking at a docotrs profile.
        </div>
        <div class="col-2">
        </div>
      </div>
  </div>
    </div>
  </div>
</div>
</asp:Content>
