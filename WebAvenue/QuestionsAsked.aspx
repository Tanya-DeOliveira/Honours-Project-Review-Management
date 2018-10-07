<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QuestionsAsked.aspx.vb" Inherits="WebAvenue.QuestionsAsked" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .card {
       border: 1px solid #B463A6 !important;
    }
    .card-header {
        border: 1px solid #B463A6 !important; 
        background-color:#B463A6 !important; 
        color:white;
    }
   .card-title {
        color:#5C4978;
    }
    .card-text {
        color:#343a40;
    }
    .badge {
        background-color:#3B475E;
    }
    .lk:hover, .lk:active {
        background-color: #B463A6 !important;
    }
     .btn-outline-primary {
        border: 1px solid #B463A6 !important; 
        color:#5C4978;
        background-color:#B463A6 !important; 
     }
     .jumbotron {
         border: 1px solid #B463A6 !important; 
        color:#B463A6;
        background-color: #5C4978 !important;
     }
</style>
    <br/>
    <div class="container">
        <h1 class="display-4">Questions You Have Asked</h1>
  <div class="row">
    <div class="col col-lg-2">
        </div>
        <div class="col col-lg-8">
<br/>
<div class="card border-secondary mb-3">
  <div class="card-header"><b>Back pain for a couple of months</b></div>
  <div class="card-body text-secondary">
    <p class="card-text">Gristle feeling along lumber spine. Saw chiro recently….he thinks disc bulge? I get restless legs…tingly feeling in back thigh to feet at times…walking feels heavy. It can feel like my back goes into spasm….I have a sitting job. Shooting and tingly pain in arms at times too. Your thoughts? Been for XRays but shows nothing serious<br/>
       <a href="#" class="lk badge badge-dark">View Suggestions <span class="badge badge-dark">0 Suggestions</span></a> 
    </p>
    <p class="card-text"><small class="text-muted">Asked 2 days ago</small></p>
  </div>
</div>
<br/>
<div class="card border-secondary mb-3">
  <div class="card-header"><b>Constant headache for a few days</b></div>
  <div class="card-body text-secondary">
    <p class="card-text">Any Recommendations are welcome.<br/>
       <a href="#" class="lk badge badge-dark">View Suggestions <span class="badge badge-dark">1 Suggestions</span></a>
    </p>
    <p class="card-text"><small class="text-muted">Asked 6 Months ago</small></p>
  </div>
</div>
<br/>
            </div>
    <div class="col col-lg-2">
        </div>
      </div>
        </div>
</asp:Content>
