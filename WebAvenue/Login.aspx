<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="WebAvenue.Login"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
	body {
		 background-image:url('Media/walkway.jpg');
		 /*height: 100%;*/ 
		background-position: center;
		background-repeat: no-repeat;
		background-size: 200%;
	}
</style>
    <br/>
	<br/>
	<br/>

 <div class="container">
  <div class="row">
    <div class="col-sm">
      
    </div>
    <div class="col-sm">
      <div class="card">
		<div class="card-body">
			<asp:Label foreColor="#343a40" Visible="true" ID="Label1" runat="server" Text="Username"></asp:Label>
			<asp:TextBox class="form-control" ID="txtUsername" runat="server"></asp:TextBox>
			<asp:Label  foreColor="#343a40" Visible="true" ID="Label2" runat="server" Text="Password"></asp:Label>
			<asp:TextBox TextMode="Password" Text="Password" class="form-control" ID="txtPassword" runat="server"></asp:TextBox>
            <asp:Button ID="btnEnter" CssClass="btn btn-outline-dark btn-block" runat="server" Text="Login" OnClick=" btnEnter_Click" />
			Need an account? Register <a href="Default.aspx">here</a><br/>
			<b><asp:Label CssClass="alert-danger" style="width:400px;" Visible="false" ID="lblErr" runat="server" Text="Username/Password is incorrect. Change things up and try submitting again."></asp:Label></b>
		</div>
	</div>
    </div>
    <div class="col-sm">
      
    </div>
  </div>
</div>
    <br/>
	<br/>
	<br/>
    <br/>
	<br/>
	<br/>
    <br/>
	<br/>
	<br/>
</asp:Content>
