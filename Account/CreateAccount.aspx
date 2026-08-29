<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="Account_CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-card">
        <h1>Create account</h1>
        <p class="auth-subtitle">Fill in your details to get started.</p>

        <asp:Panel ID="pnlError" runat="server" CssClass="error-message" Visible="false">
            <asp:Literal ID="litError" runat="server" />
        </asp:Panel>

        <div class="form-group">
            <label for="<%= txtFullName.ClientID %>">Full name</label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="<%= txtEmail.ClientID %>">Email address</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
        </div>

        <div class="form-group">
            <label for="<%= txtUsername.ClientID %>">Username</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="<%= txtPassword.ClientID %>">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
        </div>

        <div class="form-group">
            <label for="<%= txtConfirmPassword.ClientID %>">Confirm password</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
        </div>

        <asp:Button ID="btnCreateAccount" runat="server" Text="Create account" CssClass="btn-primary" OnClick="btnCreateAccount_Click" />

        <div class="form-footer">
            Already have an account? <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Account/Login.aspx">Sign in</asp:HyperLink>
        </div>
    </div>
</asp:Content>
