<%@ Page Title="Verify Your Email" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VerifyCode.aspx.cs" Inherits="Account_VerifyCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-card">
        <h1>Check your email</h1>
        <p class="auth-subtitle">Enter the 6-digit code we just sent to your email address.</p>

        <asp:Panel ID="pnlError" runat="server" CssClass="error-message" Visible="false">
            <asp:Literal ID="litError" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlNotice" runat="server" CssClass="success-message" Visible="false">
            <asp:Literal ID="litNotice" runat="server" />
        </asp:Panel>

        <div class="form-group">
            <label for="<%= txtCode.ClientID %>">Verification code</label>
            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control otp-input" MaxLength="6" />
        </div>

        <asp:Button ID="btnVerify" runat="server" Text="Verify and sign in" CssClass="btn-primary" OnClick="btnVerify_Click" />

        <div class="form-footer">
            Didn't get a code?
            <asp:LinkButton ID="lnkResend" runat="server" CssClass="btn-link" OnClick="lnkResend_Click">Resend code</asp:LinkButton>
        </div>
    </div>
</asp:Content>
