<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Account_ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-card">
        <h1>Reset password</h1>
        <p class="auth-subtitle">Choose a new password for your account.</p>

        <asp:Panel ID="pnlError" runat="server" CssClass="error-message" Visible="false">
            <asp:Literal ID="litError" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlForm" runat="server">
            <div class="form-group">
                <label for="<%= txtNewPassword.ClientID %>">New password</label>
                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <div class="form-group">
                <label for="<%= txtConfirmPassword.ClientID %>">Confirm new password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <asp:Button ID="btnReset" runat="server" Text="Reset password" CssClass="btn-primary" OnClick="btnReset_Click" />
        </asp:Panel>

        <div class="form-footer">
            <asp:HyperLink ID="lnkBackToLogin" runat="server" NavigateUrl="~/Account/Login.aspx">Back to sign in</asp:HyperLink>
        </div>
    </div>
</asp:Content>
