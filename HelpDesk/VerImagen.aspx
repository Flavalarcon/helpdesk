<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerImagen.aspx.cs" Inherits="HelpDesk.VerImagen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Image ID="Image1" runat="server"/><br />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorVS" Text="" ForeColor="red" runat="server" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
