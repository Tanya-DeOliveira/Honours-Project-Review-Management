<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="WebAvenue.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %>.</h2>
    <p>Your app description page.</p>
    <p>Use this area to provide additional information.</p>--%>
    <style>
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
    <br/>
<div class="card border-secondary mb-3">
  <div class="card-header">
    <b>Like/Dislike Rate And Review <asp:Label ID="lblPracName" runat="server" Text=""></asp:Label></b>
  </div>
  <ul class="list-group list-group-flush">
     <li Class="list-group-item">
          <div Class="card-body">
              <h5 Class="card-title"><asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label> <span class="badge badge-dark"><asp:Label ID="lblPracRating" runat="server" Text=""></asp:Label>star</span></h5>
                  <p Class="card-text"><asp:Label ID="lblReview" runat="server" Text=""></asp:Label></p>
                      <div Class="container"><div Class="row">
                          <div Class="col col-lg-2">
                              <asp:Button ID="btnLikeSec" Class="btn btn-outline-primary" runat="server" Text="Like" />
                          </div>
                          <div Class="col col-lg-8" style="left: 0px; top: 0px">

                          </div>
                           <div Class="col col-lg-2">
                               <asp:Button ID="btnDisLike" Class="btn btn-outline-primary" runat="server" Text="Dislike" />
                            </div>
                          <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </div>
                 </div>
              </div>
          </li>
  </ul>
</div>
</asp:Content>
