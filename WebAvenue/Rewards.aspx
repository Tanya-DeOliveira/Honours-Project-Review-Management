<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Rewards.aspx.vb" Inherits="WebAvenue.Rewards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="row">
         <div class="col col-lg-1">
         </div>
        <div class="col col-lg-10">
            <br/>
            <h1 class="display-4">Your Rewards</h1>
    <table class="table table-hover">
    <thead>
      <tr>
        <th>Reward Name</th>
        <th>Date Received</th>
        <th>Time Received</th>
        <th>Claimed</th>
      </tr>
    </thead>
    <tbody>
        <asp:Label ID="lblRewards" runat="server" Text=""></asp:Label>
    </tbody>
  </table>
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-dark" Text="Back" />
        </div>
        <div class="col col-lg-1">
        </div>
    </div>
</div>
</asp:Content>
