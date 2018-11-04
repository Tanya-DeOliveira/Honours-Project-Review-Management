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
<asp:Label ID="lblDisplayQuestionsAsked" runat="server" Text=""></asp:Label>
            </div>
    <div class="col col-lg-2">
        </div>
      </div>
        </div>
</asp:Content>
