<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Send.aspx.vb" Inherits="PDRM_SPE.Send" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>TCP Sender</title>
</head>
<body>
    <form id="form1" runat="server">
<asp:TextBox ID="txtContent" runat="server" Rows="5"  ></asp:TextBox>

        <asp:Button ID="btnSendMessage" runat="server" Text="Send TCP Message" OnClick="btnSendMessage_Click" />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>