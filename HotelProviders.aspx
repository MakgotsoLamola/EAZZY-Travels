<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelProviders.aspx.cs" Inherits="EazzyTravels.HotelProviders" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>EaZZy-Travels — Hotel Providers</title>
    <link rel="stylesheet" type="text/css" href="Styles/EazzyTheme.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="eazzy-wrap">

            <div class="eazzy-header">
                <div>
                    <div class="eazzy-eyebrow">EaZZy-Travels &middot; Internal Records System</div>
                    <h1 class="eazzy-title hotel">Hotel Providers</h1>
                </div>
                <div class="eazzy-session">
                    <label>Staff ID</label>
                    <asp:TextBox ID="txtStaffId" runat="server" Width="90px"></asp:TextBox>
                    <label>Role</label>
                    <asp:DropDownList ID="ddlRole" runat="server">
                        <asp:ListItem Text="Employee" Value="Employee" />
                        <asp:ListItem Text="Administrator" Value="Administrator" />
                    </asp:DropDownList>
                    <asp:Button ID="btnSetSession" runat="server" Text="Set" CssClass="eazzy-btn ghost" OnClick="btnSetSession_Click" />
                </div>
            </div>

            <asp:Panel ID="pnlAlert" runat="server" CssClass="eazzy-alert" Visible="false">
                <asp:Label ID="lblAlert" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlSuccess" runat="server" CssClass="eazzy-msg-ok" Visible="false">
                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            </asp:Panel>

            <div class="eazzy-controls">
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search by ID, type, location..."></asp:TextBox>
                <asp:DropDownList ID="ddlTypeFilter" runat="server">
                    <asp:ListItem Text="All types" Value="" />
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="eazzy-btn ghost" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="eazzy-btn ghost" OnClick="btnClear_Click" />
                <span style="flex:1"></span>
                <asp:Button ID="btnAdd" runat="server" Text="+ Add Record" CssClass="eazzy-btn hotel" OnClick="btnAdd_Click" />
            </div>

            <asp:GridView ID="gvHotels" runat="server" CssClass="eazzy-grid" AutoGenerateColumns="false"
                DataKeyNames="HotelID" OnRowCommand="gvHotels_RowCommand" OnRowDataBound="gvHotels_RowDataBound"
                GridLines="None" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate><span class="eazzy-id-tag"><%# Eval("HotelID") %></span></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="HotelType" HeaderText="Type" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:BoundField DataField="Rating" HeaderText="Rating" />
                    <asp:BoundField DataField="EmployeeID" HeaderText="Added by" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord"
                                CommandArgument='<%# Eval("HotelID") %>' CssClass="eazzy-btn ghost">Edit</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord"
                                CommandArgument='<%# Eval("HotelID") %>' CssClass="eazzy-btn danger"
                                OnClientClick="return confirm('Delete this hotel provider record? This cannot be undone.');">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="eazzy-empty">No hotel providers on file yet. Add the first record.</div>
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:Panel ID="pnlForm" runat="server" CssClass="eazzy-panel" Visible="false">
                <h3><asp:Literal ID="litFormTitle" runat="server">New Hotel Provider</asp:Literal></h3>

                <asp:HiddenField ID="hfHotelId" runat="server" />

                <div class="eazzy-field">
                    <label>Hotel ID</label>
                    <asp:TextBox ID="txtHotelIdDisplay" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="eazzy-field">
                    <label>Type of hotel</label>
                    <asp:DropDownList ID="ddlHotelType" runat="server">
                        <asp:ListItem Text="Select..." Value="" />
                        <asp:ListItem Text="Boutique" Value="Boutique" />
                        <asp:ListItem Text="Resort" Value="Resort" />
                        <asp:ListItem Text="Lodge" Value="Lodge" />
                        <asp:ListItem Text="Guesthouse" Value="Guesthouse" />
                        <asp:ListItem Text="Business" Value="Business" />
                        <asp:ListItem Text="Backpackers" Value="Backpackers" />
                    </asp:DropDownList>
                </div>
                <div class="eazzy-field">
                    <label>Location</label>
                    <asp:TextBox ID="txtLocation" runat="server" placeholder="e.g. Cape Town, Western Cape"></asp:TextBox>
                </div>
                <div class="eazzy-field">
                    <label>Rating</label>
                    <asp:DropDownList ID="ddlRating" runat="server">
                        <asp:ListItem Text="Select..." Value="" />
                        <asp:ListItem Text="1 star" Value="1" />
                        <asp:ListItem Text="2 stars" Value="2" />
                        <asp:ListItem Text="3 stars" Value="3" />
                        <asp:ListItem Text="4 stars" Value="4" />
                        <asp:ListItem Text="5 stars" Value="5" />
                    </asp:DropDownList>
                </div>
                <div class="eazzy-field">
                    <label>Employee ID (added by)</label>
                    <asp:TextBox ID="txtEmployeeIdDisplay" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Save record" CssClass="eazzy-btn hotel" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancelForm" runat="server" Text="Cancel" CssClass="eazzy-btn ghost" CausesValidation="false" OnClick="btnCancelForm_Click" />
            </asp:Panel>

        </div>
    </form>
</body>
</html>
