<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ClaimReward.aspx.vb" Inherits="WebAvenue.ClaimReward" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <div class="row">
         <div class="col col-lg-2">
         </div>
        <div class="col col-lg-8">
            <br/>
            <h1 class="display-4">Redeam Your Reward</h1>
            Is valid at only the place specified
            <div class="jumbotron jumbotron-fluid">
                <div class="container">
                    <h1>Gift Voucher</h1> 
                    To: <asp:Label ID="lblTo" runat="server" Text=""></asp:Label><br/>
                    From: MediAvenue <br/>
                    Reward: <asp:Label ID="lblRewardName" runat="server" Text=""></asp:Label><br/>
                    <br/>
                    Message: Thank you for being a deligant user on our System!<br/>
                    Date Recieved: <asp:Label ID="lblDate" runat="server" Text=""></asp:Label><br/>
                    Time Recieved: <asp:Label ID="lblTime" runat="server" Text=""></asp:Label><br/>
                    <p>T's & C's Apply</p> 
                </div>
            </div>
            <asp:Button ID="btnClaim" runat="server" CssClass="btn btn-dark" Text="Claim Reward" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label> 
            <br/>
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-dark" Text="Back" />
            </div>
        <div class="col col-lg-2">
        </div>
    </div>
</div>
</asp:Content>
