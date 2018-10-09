<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Admin.aspx.vb" Inherits="WebAvenue.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
    <div class="row">
         <div class="col col-lg-2">
         </div>
        <div class="col col-lg-8">
            <br/>
            <h1 class="display-4">Mangement of MediAvenue</h1>

            <div class="card">
                <div class="card-header">Flagged Users For Reviews</div>
                <div class="card-body">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Reviewer(Patient) Name</th>
                                <th>Remove</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Label ID="lblFlagUsers" runat="server" Text=""></asp:Label>
                        </tbody>
                    </table>
                </div> 
                </div>
            <div class="card">
                <div class="card-header">Flagged Users For Reviews</div>
                <div class="card-body">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Suggester Name</th>
                                <th>Type of User</th>
                                <th>Remove</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Label ID="lblFlaggedSuggesters" runat="server" Text=""></asp:Label>
                        </tbody>
                    </table>
                </div> 
                </div>
            <br/>
            </div>    
        <div class="col col-lg-2">
        </div>
        <div class="col col-lg-12">
        <div class="card-deck">
                <div class="card bg-light">
                    <div class="card-header">Questions Asked On System</div>
                        <div class="card-body text-center">
                            <asp:Label ID="lblNumQuestions" runat="server" Text=""></asp:Label>
                        </div>
                </div>
                <div class="card bg-light">
                    <div class="card-header">Reviews Made On System</div>
                        <div class="card-body text-center">
                            <asp:Label ID="lblNumReviews" runat="server" Text=""></asp:Label>
                        </div>
                </div>
                <div class="card bg-light">
                    <div class="card-header">Suggestions Made On System</div>
                        <div class="card-body text-center">
                            <asp:Label ID="lblNumSuggestions" runat="server" Text=""></asp:Label>
                        </div>
                </div>
                <div class="card bg-light">
                    <div class="card-header">Patients On System</div>
                        <div class="card-body text-center">
                            <asp:Label ID="lblNumPatients" runat="server" Text=""></asp:Label>
                        </div>
                </div>
                <div class="card bg-light">
                    <div class="card-header">Practitioners On System</div>
                        <div class="card-body text-center">
                            <asp:Label ID="lblNumPractitioners" runat="server" Text=""></asp:Label>
                        </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
