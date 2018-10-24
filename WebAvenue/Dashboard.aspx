<%@ Page Title="Dashboard" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Dashboard.aspx.vb" Inherits="WebAvenue.Dashboard" %>
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
  <div class="row">
    <div class="col col-lg-1">
    </div>
        <div class="col col-lg-8">
<%--<div class="jumbotron jumbotron-fluid">--%>
  <div class="container" style="border: 1px solid #B463A6 !important; color:#B463A6; background-color: #5C4978 !important;">
    <h1 class="display-4">Ask A Question</h1>
<div class="container">
  <div class="row">
    <div class="col col-lg-10">
        <div style="float:left;">
            <asp:Label ID="lblCategoryLable" runat="server" Text="Choose a Category for your Question: "></asp:Label>
        </div>
        <div style="float:left;">
            <asp:DropDownList ID="ddCategoryForQuestion" Style="max-width:100%;color:white;background-color: #776b8a !important; border: 1px solid #B463A6 !important;" CssClass="form-control" runat="server" AutoPostBack="True">
                <asp:ListItem>Select Category</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:TextBox ID="txtAddQuestionTopic" Text="Type Subject of Question Here" Style="max-width:100%;color:white;background-color: #776b8a !important; border: 1px solid #B463A6 !important;" class="form-control" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtDescription" Text="Type Description Here" Style="max-width:100%;color:white;background-color: #776b8a !important; border: 1px solid #B463A6 !important;" class="form-control" runat="server"></asp:TextBox><br/>
        <asp:Label ID="lblMessage" runat="server" CssClass="alert-info" Visible="false" Text=""></asp:Label>
    </div>
    <div class="col col-lg-2">
        <br/>
        <br/>
        <asp:Button ID="btnAdd" class="lk btn btn-outline-primary" runat="server" Text="Post" />
    </div>
   </div>
</div>
    
  </div>
<%--</div>--%>

<asp:Label ID="lblDisplayQuestions" runat="server" Text=""></asp:Label>
<%--<div class="card border-secondary mb-3">
  <div class="card-header"><b>How can you treat a swollen frenulum?</b></div>
  <div class="card-body text-secondary">
    <h5 class="card-title">Charlie Burns</h5>
    <p class="card-text">Is there natural remedies that work?<br/>
       <a href="Suggestions.aspx" class="lk badge badge-dark">View Suggestions <span class="badge badge-dark">2 Suggestions</span></a>
    </p>
    <p class="card-text"><small class="text-muted">Asked 8 hours ago</small></p>
  </div>
</div>
<br/>--%>
  </div>  
    <div class="col col-lg-3">
        <br/>
        <asp:DropDownList ID="Category" CssClass="btn-outline-primary dropdown-item" runat="server">
        <asp:ListItem Text="Select Category"></asp:ListItem>
    </asp:DropDownList>    
        </div>
      </div>
        </div>
</asp:Content>
