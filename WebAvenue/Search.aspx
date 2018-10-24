<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Search.aspx.vb" Inherits="WebAvenue.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btn-outline-primary {
        border: 1px solid #B463A6 !important; 
        color:#5C4978;
        background-color:#B463A6 !important; 
     }
     </style>
    <div class="container">
    <div class="row">
         <div class="col col-lg-2">
         </div>
        <div class="col col-lg-8">
            <br/>
            <h1 class="display-4">Your Search Results</h1>
             <div class="col col-lg-4">
            <asp:DropDownList ID="Rating" CssClass="btn-outline-primary dropdown-item" runat="server">
                 <asp:ListItem Text="Select Rating" Value="0"></asp:ListItem>
                 <asp:ListItem Text="1" Value="1"></asp:ListItem>
                 <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
            </asp:DropDownList>
                 </div>
            <div class="col col-lg-4">
                <asp:DropDownList ID="MedicalCategory" CssClass="btn-outline-primary dropdown-item" runat="server">
                   <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                </asp:DropDownList>
                </div>
            <br/>
            <asp:Label ID="lblNoSearch" runat="server" Visible="false" Text="No Search is Provided"></asp:Label>
    <table class="table table-hover">
    <thead>
      <tr>
        <th>Practitioner</th>
        <th>Address</th>
        <th>Years of Experiance</th>
        <th>Specilization</th>
        <th>Rating</th>
      </tr>
    </thead>
    <tbody>
        <asp:Label ID="lblSearchResults" runat="server" Text=""></asp:Label>
    </tbody>
  </table>
        </div>
        <div class="col col-lg-2">
        </div>
    </div>
</div>
</asp:Content>
