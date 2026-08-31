<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntertainmentProviders.aspx.cs" Inherits="EazzyTravels.EntertainmentProviders" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>EaZZy-Travels — Entertainment Providers</title>
    <link rel="stylesheet" type="text/css" href="Styles/EazzyTheme.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="eazzy-wrap">

            <div class="eazzy-header">
                <div>
                    <div class="eazzy-eyebrow">EaZZy-Travels &middot; Internal Records System</div>
                    <h1 class="eazzy-title ent">Entertainment Providers</h1>
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
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search by ID, activity, location..."></asp:TextBox>
                <asp:DropDownList ID="ddlActivityFilter" runat="server">
                    <asp:ListItem Text="All activities" Value="" />
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="eazzy-btn ghost" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="eazzy-btn ghost" OnClick="btnClear_Click" />
                <span style="flex:1"></span>
                <asp:Button ID="btnAdd" runat="server" Text="+ Add Record" CssClass="eazzy-btn ent" OnClick="btnAdd_Click" />
            </div>

            <asp:GridView ID="gvEntertainment" runat="server" CssClass="eazzy-grid" AutoGenerateColumns="false"
                DataKeyNames="EntertainmentID" OnRowCommand="gvEntertainment_RowCommand" OnRowDataBound="gvEntertainment_RowDataBound"
                GridLines="None" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate><span class="eazzy-id-tag ent"><%# Eval("EntertainmentID") %></span></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Activity" HeaderText="Activity" />
                    <asp:BoundField DataField="Food" HeaderText="Food" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:BoundField DataField="EmployeeID" HeaderText="Added by" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord"
                                CommandArgument='<%# Eval("EntertainmentID") %>' CssClass="eazzy-btn ghost">Edit</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord"
                                CommandArgument='<%# Eval("EntertainmentID") %>' CssClass="eazzy-btn danger"
                                OnClientClick="return confirm('Delete this entertainment provider record? This cannot be undone.');">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="eazzy-empty">No entertainment providers on file yet. Add the first record.</div>
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:Panel ID="pnlForm" runat="server" CssClass="eazzy-panel" Visible="false">
                <h3><asp:Literal ID="litFormTitle" runat="server">New Entertainment Provider</asp:Literal></h3>

                <asp:HiddenField ID="hfEntertainmentId" runat="server" />

                <div class="eazzy-field">
                    <label>Entertainment ID</label>
                    <asp:TextBox ID="txtEntertainmentIdDisplay" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="eazzy-field">
                    <label>Activity</label>
                    <asp:TextBox ID="txtActivity" runat="server" placeholder="e.g. Safari tour, Wine tasting, Live music"></asp:TextBox>
                </div>
                <div class="eazzy-field">
                    <label>Types of food</label>
                    <asp:TextBox ID="txtFood" runat="server" placeholder="e.g. Braai, Seafood, Cape Malay"></asp:TextBox>
                </div>
                <div class="eazzy-field">
                    <label>Location</label>
                    <asp:TextBox ID="txtLocation" runat="server" placeholder="e.g. Kruger National Park"></asp:TextBox>
                </div>
                <div class="eazzy-field">
                    <label>Employee ID (added by)</label>
                    <asp:TextBox ID="txtEmployeeIdDisplay" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <asp:Button ID="btnSave" runat="server" Text="Save record" CssClass="eazzy-btn ent" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancelForm" runat="server" Text="Cancel" CssClass="eazzy-btn ghost" CausesValidation="false" OnClick="btnCancelForm_Click" />
            </asp:Panel>

        </div>
    </form>
</body>
</html>
