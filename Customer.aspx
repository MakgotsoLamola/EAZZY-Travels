<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Eazzy_Travelss.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 80px;
        }
        .auto-style2 {
            height: 80px;
            width: 87px;
        }
        .auto-style4 {
            width: 87px;
            height: 29px;
        }
        .auto-style5 {
            height: 29px;
        }
        .auto-style6 {
            height: 80px;
            width: 132px;
        }
        .auto-style7 {
            height: 29px;
            width: 132px;
        }
        .auto-style9 {
            height: 30px;
            width: 87px;
        }
        .auto-style10 {
            height: 30px;
            width: 132px;
        }
        .auto-style11 {
            height: 30px;
        }
        .auto-style12 {
            width: 87px;
            height: 31px;
        }
        .auto-style13 {
            height: 31px;
        }
        .auto-style14 {
            height: 31px;
        }
        .auto-style15 {
            width: 87px;
            height: 34px;
        }
        .auto-style16 {
            width: 132px;
            height: 34px;
        }
        .auto-style17 {
            height: 34px;
        }
        .auto-style18 {
            width: 87px;
            height: 38px;
        }
        .auto-style19 {
            height: 38px;
        }
        .auto-style20 {
            height: 38px;
        }
        .auto-style24 {
            width: 87px;
            height: 36px;
        }
        .auto-style25 {
            height: 36px;
        }
        .auto-style26 {
            height: 36px;
        }
        .auto-style27 {
            width: 87px;
            height: 53px;
        }
        .auto-style28 {
            height: 53px;
        }
        .auto-style29 {
            height: 53px;
        }
        .auto-style30 {
            width: 87px;
            height: 35px;
        }
        .auto-style31 {
            height: 35px;
        }
        .auto-style32 {
            height: 35px;
        }
        .auto-style33 {
            width: 87px;
            height: 32px;
        }
        .auto-style34 {
            width: 132px;
            height: 32px;
        }
        .auto-style35 {
            height: 32px;
        }
        .auto-style36 {
            height: 80px;
            width: 306px;
        }
        .auto-style37 {
            height: 29px;
            width: 306px;
        }
        .auto-style38 {
            height: 30px;
            width: 306px;
        }
        .auto-style40 {
            height: 34px;
            width: 306px;
        }
        .auto-style41 {
            height: 38px;
            width: 306px;
        }
        .auto-style43 {
            height: 32px;
            width: 306px;
        }
        .auto-style45 {
            height: 53px;
            width: 306px;
        }
        .auto-style46 {
            width: 132px;
            height: 38px;
        }
        .auto-style47 {
            width: 132px;
            height: 53px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #CC00FF">
        <div>
            <table style="width: 100%; height: 491px; background-color: #FFFFFF;">
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style6">&nbsp;</td>
                    <td class="auto-style36">
                        <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="Eazzy-Travels" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style37">
                        <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Create Your Account"></asp:Label>
                        ..</td>
                    <td class="auto-style5">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="#CC0000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9"></td>
                    <td class="auto-style10">
                        <asp:Label ID="lblName" runat="server" Text="Full Name:" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="auto-style38"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style12"></td>
                    <td class="auto-style13" colspan="2">
                        <asp:TextBox ID="txtName" runat="server" Width="222px" Height="21px"></asp:TextBox>
                    </td>
                    <td class="auto-style14"></td>
                </tr>
                <tr>
                    <td class="auto-style15"></td>
                    <td class="auto-style16">
                        <asp:Label ID="Label4" runat="server" Text="Age:" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="auto-style40"></td>
                    <td class="auto-style17"></td>
                </tr>
                <tr>
                    <td class="auto-style18"></td>
                    <td class="auto-style19" colspan="2">
                        <asp:TextBox ID="txtAge" runat="server" Width="218px" Height="21px"></asp:TextBox>
                    </td>
                    <td class="auto-style20"></td>
                </tr>
                <tr>
                    <td class="auto-style18"></td>
                    <td class="auto-style46">
                        <asp:Label ID="Label5" runat="server" Text="Contact Info:" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="auto-style41"></td>
                    <td class="auto-style20"></td>
                </tr>
                <tr>
                    <td class="auto-style24"></td>
                    <td class="auto-style25" colspan="2">
                        <asp:TextBox ID="txtContact" runat="server" Width="216px" Height="21px"></asp:TextBox>
                    </td>
                    <td class="auto-style26"></td>
                </tr>
                <tr>
                    <td class="auto-style33"></td>
                    <td class="auto-style34">
                        <asp:Label ID="Label6" runat="server" Text="UserName/Email:" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="auto-style43"></td>
                    <td class="auto-style35"></td>
                </tr>
                <tr>
                    <td class="auto-style30"></td>
                    <td class="auto-style31" colspan="2">
                        <asp:TextBox ID="txtEmail" runat="server" Width="214px" Height="24px"></asp:TextBox>
                    </td>
                    <td class="auto-style32"></td>
                </tr>
                <tr>
                    <td class="auto-style18"></td>
                    <td class="auto-style46">
                        <asp:Label ID="Label7" runat="server" Text="Password:" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="auto-style41"></td>
                    <td class="auto-style20"></td>
                </tr>
                <tr>
                    <td class="auto-style27"></td>
                    <td class="auto-style28" colspan="2">
                        <asp:TextBox ID="txtPassword" runat="server" Width="217px" Height="21px"></asp:TextBox>
                    </td>
                    <td class="auto-style29"></td>
                </tr>
                <tr>
                    <td class="auto-style27">&nbsp;</td>
                    <td class="auto-style47">&nbsp;</td>
                    <td class="auto-style45">
                        <asp:Button ID="Button1" runat="server" BackColor="#CC99FF" Height="39px" Text="Create Account" Width="271px" OnClick="Button1_Click" />
                    </td>
                    <td class="auto-style29">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
