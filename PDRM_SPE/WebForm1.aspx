<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="PDRM_SPE.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table td {
            border: 1px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div>
            <asp:Repeater runat="server" ID="rptTable" OnItemCommand="rptTable_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped table-hover table-bordered managedTable" id="table1">
                        <thead>
                            <tr>
                                <th style="width: 10% !important">Imei
                                </th>
                                <th style="width: 10% !important">SimNo
                                </th>
                                <th style="width: 10% !important"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="text-align: left">
                            <%#Eval("fldImei")%>
                        </td>
                        <td style="text-align: left">
                            <%#Eval("fldSImNo")%>
                        </td>
                        <td style="text-align: center">
                            <asp:Button runat="server" ID="Button4" Text="Alarm" CommandName="Alarm" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                            <asp:Button runat="server" ID="Button1" Text="AlarmOff" CommandName="AlarmOff" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                            <asp:Button runat="server" ID="Button2" Text="Lock" CommandName="Lock" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                            <asp:Button runat="server" ID="Button3" Text="Unlock" CommandName="Unlock" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                            <asp:Button runat="server" ID="Button5" Text="Vibrate" CommandName="Vibrate" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                            <asp:Button runat="server" ID="btnEdit" Text="LowBattery" CommandName="LowBattery" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                                        </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
