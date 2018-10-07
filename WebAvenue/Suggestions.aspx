<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Suggestions.aspx.vb" Inherits="WebAvenue.Suggestions" %>
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
    .badge-secondary {
        border: 1px solid #B463A6 !important; 
        background-color:#B463A6 !important; 
    }
    a:link {
        color:#5C4978;
    }
    a:hover,.a:active{
    text-decoration: underline;
    color:#5C4978;
    }
    a:visited {
    color: #5C4978;
    }
</style>
     <br/>
    <%-- to display full question --%>
    <div class="card border-secondary mb-3">
        <div class="card-header">
            <b><asp:Label ID="lblQuestionTopic" runat="server" Text=""></asp:Label></b>
        </div>
    <div class="card-body">
        <h5 class="card-title"><asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label></h5>
        <p class="card-text"><asp:Label ID="lblDescription" runat="server" Text=""></asp:Label></p>
        <p Class="card-text"><small class="text-muted"></small><asp:Label ID="lblDateTime" runat="server" Text=""></asp:Label></p>
    </div>

    <ul class="list-group list-group-flush">
        <asp:Label ID="lblSuggestions" runat="server" Text=""></asp:Label>
    </ul>
        <%-- to add a suggestion --%>
  <div class="card-footer text-muted">
    <div class="container">
        <div class="row">
            <div class="col col-lg-10">
                <asp:TextBox ID="txtAddSuggestion" Style="max-width:100%; border: 1px solid #B463A6 !important;" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col col-lg-2">
                <asp:Button ID="btnAdd" class="lk btn btn-outline-primary" runat="server" Text="Add Suggestion" />
            </div>
        </div>
    </div>
</div>
</div>
<asp:Button ID="btnBack" class="btn btn-dark" runat="server" Text="Back" />
</asp:Content>
