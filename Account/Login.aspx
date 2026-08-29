<%@ Page Title="Sign In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-card">
        <h1>Sign in</h1>
        <p class="auth-subtitle">Enter your account details to continue.</p>

        <asp:Panel ID="pnlError" runat="server" CssClass="error-message" Visible="false">
            <asp:Literal ID="litError" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlNotice" runat="server" CssClass="success-message" Visible="false">
            <asp:Literal ID="litNotice" runat="server" />
        </asp:Panel>

        <div class="form-group">
            <label for="<%= txtLogin.ClientID %>">Username or email</label>
            <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="<%= txtPassword.ClientID %>">Password</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
        </div>

        <asp:Button ID="btnLogin" runat="server" Text="Sign in" CssClass="btn-primary" OnClick="btnLogin_Click" />

        <div class="link-row">
            <asp:HyperLink ID="lnkForgotPassword" runat="server" NavigateUrl="~/Account/ForgotPassword.aspx">Forgot password?</asp:HyperLink>
        </div>

        <div class="form-footer">
            Don't have an account? <asp:HyperLink ID="lnkCreateAccount" runat="server" NavigateUrl="~/Account/CreateAccount.aspx">Create one</asp:HyperLink>
        </div>
    </div>
</asp:Content>
