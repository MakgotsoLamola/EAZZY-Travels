<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="Eazzy_Travelss.Subscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 234px;
            height: 39px;
        }
        .auto-style3 {
            width: 60px;
        }
        .auto-style5 {
            width: 60px;
            height: 46px;
        }
        .auto-style7 {
            height: 46px;
        }
        .auto-style8 {
            height: 46px;
            width: 248px;
        }
        .auto-style9 {
            width: 248px;
        }
        .auto-style10 {
        }
        .auto-style11 {
            width: 186px;
            height: 46px;
        }
        .auto-style12 {
            width: 60px;
            height: 39px;
        }
        .auto-style13 {
            width: 186px;
            height: 39px;
        }
        .auto-style14 {
            width: 248px;
            height: 39px;
        }
        .auto-style15 {
            height: 39px;
        }
        .auto-style16 {
            width: 60px;
            height: 33px;
        }
        .auto-style17 {
            width: 186px;
            height: 33px;
        }
        .auto-style18 {
            width: 248px;
            height: 33px;
        }
        .auto-style19 {
            width: 60px;
            height: 40px;
        }
        .auto-style20 {
            height: 40px;
        }
        .auto-style21 {
            width: 60px;
            height: 57px;
        }
        .auto-style22 {
            height: 57px;
        }
        .auto-style23 {
            width: 186px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; height: 409px;">
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style23">&nbsp;</td>
                    <td class="auto-style9">
                        <asp:Label ID="Label1" runat="server" Text="SUBSCRIPTION MANAGEMENT" Font-Bold="True"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style12"></td>
                    <td class="auto-style13">
                        <asp:Label ID="Label2" runat="server" Text="Customer ID:"></asp:Label>
                    </td>
                    <td class="auto-style14"></td>
                    <td class="auto-style2">
                        <asp:Label ID="Label5" runat="server" Text="Start Date:"></asp:Label>
                    </td>
                    <td class="auto-style15"></td>
                </tr>
                <tr>
                    <td class="auto-style21"></td>
                    <td class="auto-style22" colspan="2">
                        <asp:TextBox ID="txtCustomerID" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td rowspan="5">
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16"></td>
                    <td class="auto-style17">
                        <asp:Label ID="Label3" runat="server" Text="Subscription Type:"></asp:Label>
                    </td>
                    <td class="auto-style18"></td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style10" colspan="2">
                        <asp:DropDownList ID="ddlSubscriptionType" runat="server" Height="16px" Width="155px">
                            <asp:ListItem>Basic</asp:ListItem>
                            <asp:ListItem>Premium</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style11">
                        <asp:Label ID="Label4" runat="server" Text="Subscription Length:"></asp:Label>
                    </td>
                    <td class="auto-style8"></td>
                </tr>
                <tr>
                    <td class="auto-style19"></td>
                    <td class="auto-style20" colspan="2">
                        <asp:DropDownList ID="ddlLength" runat="server" Height="21px" Width="152px">
                            <asp:ListItem>3 months</asp:ListItem>
                            <asp:ListItem>6 months</asp:ListItem>
                            <asp:ListItem>12 months</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                    <td class="auto-style8">
                        <asp:Button ID="Button1" runat="server" Text="Create" Width="103px" OnClick="Button1_Click" />
                    </td>
                    <td class="auto-style7">
                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Update" Width="82px" />
                    </td>
                </tr>
                </table>
        </div>
    </form>
    <p>
&nbsp;</p>
</body>
</html>
