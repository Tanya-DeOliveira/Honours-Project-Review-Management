<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Rewards.aspx.vb" Inherits="WebAvenue.Rewards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="row">
         <div class="col col-lg-2">
         </div>
        <div class="col col-lg-8">
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
      <tr>
        <td>Reward08</td>
        <td>2018/02/13</td>
        <td>12:34</td>
        <td>Yes</td>
      </tr>
      <tr>
        <td>Reward025</td>
        <td>2018/03/23</td>
        <td>14:04</td>
        <td>No</td>
      </tr>
    </tbody>
  </table>
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-dark" Text="Back" />
        </div>
        <div class="col col-lg-2">
        </div>
    </div>
</div>
</asp:Content>
