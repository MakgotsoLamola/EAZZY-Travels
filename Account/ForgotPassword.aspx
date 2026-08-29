<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Account_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-card">
        <h1>Forgot password</h1>
        <p class="auth-subtitle">Enter your account email and we'll send you a link to reset your password.</p>

        <asp:Panel ID="pnlError" runat="server" CssClass="error-message" Visible="false">
            <asp:Literal ID="litError" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlSuccess" runat="server" CssClass="success-message" Visible="false">
            <asp:Literal ID="litSuccess" runat="server" />
        </asp:Panel>

        <div class="form-group">
            <label for="<%= txtEmail.ClientID %>">Email address</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
        </div>

        <asp:Button ID="btnSendCode" runat="server" Text="Send reset link" CssClass="btn-primary" OnClick="btnSendCode_Click" />

        <div class="form-footer">
            <asp:HyperLink ID="lnkBackToLogin" runat="server" NavigateUrl="~/Account/Login.aspx">Back to sign in</asp:HyperLink>
        </div>
    </div>
</asp:Content>
