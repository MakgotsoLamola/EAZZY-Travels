<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Account_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderNav" runat="server">
    <asp:LinkButton ID="lnkLogout" runat="server" CssClass="btn-nav" OnClick="lnkLogout_Click">Sign Out</asp:LinkButton>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="home-content">

        <div class="hero">
            <h1 class="hero-title">Welcome back<span><asp:Literal ID="litUsername" runat="server" /></span></h1>
            <p class="hero-subtitle">
                Here's your EaZZy-Travels dashboard. From here you'll manage your
                subscription and browse everything our providers offer.
            </p>
        </div>

        <h2 class="section-title">Explore your subscription</h2>
        <div class="feature-grid">
            <div class="feature-card">
                <div class="card-bar"></div>
                <h3>Flights</h3>
                <p>Compare plane providers, ticket types, and services across multiple airlines.</p>
            </div>
            <div class="feature-card">
                <div class="card-bar"></div>
                <h3>Transport</h3>
                <p>Book ground transport with insurance options for safe, reliable travel.</p>
            </div>
            <div class="feature-card">
                <div class="card-bar"></div>
                <h3>Hotels</h3>
                <p>Browse hotel providers by rating, location, and the services they offer.</p>
            </div>
            <div class="feature-card">
                <div class="card-bar"></div>
                <h3>Entertainment</h3>
                <p>Find local entertainment and dining options wherever your trip takes you.</p>
            </div>
        </div>

    </div>
</asp:Content>
