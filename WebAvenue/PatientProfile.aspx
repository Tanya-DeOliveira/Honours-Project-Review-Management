<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PatientProfile.aspx.vb" Inherits="WebAvenue.PatientProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .jumbotron {
        border: 1px solid #9d478e !important; 
        color:#9d478e;
        background-color: #deb0d7 !important;
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
    .b {
       border: 1px solid whitesmoke !important; 
        background-color:whitesmoke !important; 
        color:darkgoldenrod;
        box-shadow:0px 2px 5px #808080;
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
</style>

<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="display-4">Mareli Brooks</h1><span class="b badge badge-dark">Master Suggester</span><span class="b badge badge-secondary">Master Reviewer</span>
  </div>
</div>

<br/>
<div class="card border-secondary mb-3">
  <div class="card-header">
    <b>Reviews Mareli Brooks Has Made</b>
  </div>
    <ul class="list-group list-group-flush">
    <li class="list-group-item">
<div class="card-body">
    <h5 class="card-title"><a href="#">Dr. Houseman</a> <span class="badge badge-dark">5 star</span></h5>
   <p class="card-text">Dr. Houseman is very careful when it comes to performing plastic surgery on his patients and is there for you throughout the recovery process.</p>
  </div>
    </li>
  </ul>
</div>
    <asp:Button ID="btnBack" class="btn btn-dark" runat="server" Text="Back" />
</asp:Content>
