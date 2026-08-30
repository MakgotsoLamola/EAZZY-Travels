<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransportProvider.aspx.cs" Inherits="TransportProvider" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>EaZZy-Travels | Maintain Transport Provider Records</title>
    <style>
        body { font-family: Segoe UI, Arial, sans-serif; background:#f5f5f7; margin:40px; }
        h2 { color:#1b4a6a; }
        .form-box { background:#fff; padding:20px; border-radius:8px; box-shadow:0 1px 4px rgba(0,0,0,0.15); max-width:650px; }
        .form-row { margin-bottom:12px; }
        label { display:inline-block; width:130px; font-weight:600; }
        input[type=text] { width:300px; padding:6px; border:1px solid #ccc; border-radius:4px; }
        .btn { padding:8px 16px; margin-right:8px; border:none; border-radius:4px; color:#fff; cursor:pointer; }
        .btn-save { background:#1b4a6a; }
        .btn-update { background:#2b7a78; }
        .btn-delete { background:#c0392b; }
        .btn-clear { background:#888; }
        .msg { margin-top:10px; font-weight:600; }
        table { border-collapse:collapse; width:100%; margin-top:25px; background:#fff; }
        th, td { border:1px solid #ddd; padding:8px; font-size:14px; }
        th { background:#1b4a6a; color:#fff; }
        tr:hover { background:#f1f1f1; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Maintain Transport Provider Records</h2>

        <div class="form-box">
            <asp:HiddenField ID="hfTransportID" runat="server" />

            <div class="form-row">
                <label for="txtLocation">Location:</label>
                <asp:TextBox ID="txtLocation" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtLocation" runat="server"
                    ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="txtTransportType">Transport Type:</label>
                <asp:TextBox ID="txtTransportType" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtTransportType" runat="server"
                    ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="txtInsurance">Insurance:</label>
                <asp:TextBox ID="txtInsurance" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtInsurance" runat="server"
                    ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="txtService">Service:</label>
                <asp:TextBox ID="txtService" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtService" runat="server"
                    ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="ddlEmployee">Employee:</label>
                <asp:DropDownList ID="ddlEmployee" runat="server" DataTextField="FirstName" DataValueField="EmployeeID" />
            </div>

            <div class="form-row">
                <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="btn btn-save" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-delete" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-clear" OnClick="btnClear_Click" CausesValidation="false" />
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="msg" />
        </div>

        <asp:GridView ID="gvTransportProviders" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="gvTransportProviders_SelectedIndexChanged" DataKeyNames="TransportID">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                <asp:BoundField DataField="TransportID" HeaderText="Transport ID" ReadOnly="True" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
                <asp:BoundField DataField="TransportType" HeaderText="Transport Type" />
                <asp:BoundField DataField="Insurance" HeaderText="Insurance" />
                <asp:BoundField DataField="Service" HeaderText="Service" />
                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
