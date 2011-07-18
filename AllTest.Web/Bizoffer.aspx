<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bizoffer.aspx.cs" Inherits="AllTest.Web.Bizoffer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gv" runat="server">
        </asp:GridView>
    <br />
    Name:<asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
    SellerId:<asp:TextBox ID="txtSellerId" runat="server"></asp:TextBox><br />
    SellerName:<asp:TextBox ID="txtSellerName" runat="server"></asp:TextBox><br />
    Price:<asp:TextBox ID="txtPrice" runat="server"></asp:TextBox><br />
    Dis:<asp:RadioButton ID="rbTrue" runat="server" GroupName="dis" Checked ="true"/>
    <asp:RadioButton ID="rbfalse" runat="server" GroupName="dis"></asp:RadioButton><br />
    Desc:<asp:TextBox ID="txtDesc" runat="server"></asp:TextBox><br />
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" onclick="btnSubmit_Click"/>
    </div>
    </form>
</body>
</html>
