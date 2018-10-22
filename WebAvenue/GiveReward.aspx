<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GiveReward.aspx.vb" Inherits="WebAvenue.GiveReward" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <div class="row">
        <h1 class="display-4">Reward User</h1>
        <div class="col col-lg-12">
            <br/>
            <div class="card">
                <div class="card-header">Reward Details</div>
                <div class="card-body">
                    <div class="container">
                    <div class="row">
                        <div class="col col-lg-3">
                            <asp:Label ID="lblUserName"  runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col col-lg-9">      
                            <asp:TextBox ID="txtRewardName" runat="server" Text="Type Name of Reward Here" class="form-control" style="width: 100% !important;"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnAddReward" class="btn btn-dark" runat="server" Text="Submit Reward to User" />
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                   </div>
                </div>
             </div> 
           </div>
        </div> 
        <br/>
        <asp:Button ID="btnBack" class="btn btn-dark" runat="server" Text="Back" />
        </div>
</div>
</asp:Content>
