<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recordarpwd.aspx.cs" Inherits="HelpDesk.recordarpwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="https://code.getmdl.io/1.3.0/material.min.js"></script>
    <link rel="stylesheet" href="https://code.getmdl.io/1.3.0/material.indigo-pink.min.css"/>
    
    <link href="/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="/CSS/bootstrap-theme.css" rel="stylesheet" />
    
    <link rel="stylesheet" href="/CSS/txt.css"  />
    
    
    <script src="/js/txt.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/CSS/botones.css"  />
    <link rel="stylesheet" href="/CSS/grillaDinamic.css"  />
    <link href="/CSS/estiloboton.css" rel="stylesheet" />
    <link href="/CSS/bootstrap.css" rel="stylesheet" />
    <script src="/js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="https://use.fontawesome.com/45e03a14ce.js"></script>
    <title>Solicitud de cambio de clave</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar" style="background-color:#4682B4 !important; color: white !important;">
  <div class="container-fluid">
    <div class="navbar-header" style="background-color:#4682B4 !important; color: white !important;">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span> 
      </button>
      <%--<asp:Image ID="imgLogo" runat="server" ImageUrl="~/img/delta2.png" Width="100px" />--%>
        <a class="navbar-brand" href="#" style="color: white !important; margin-left: 30px;">Informática Delta </a>
    </div>
   
  </div>
</nav>
        <div style="margin-top:100px;">
            <center>

                
                <h2><asp:label ID="lblH1" runat="server" ForeColor="#4682B4">Solicitud de cambio de clave</asp:label></h2><br /><br />
            <asp:TextBox ID="txtUsu" runat="server" placeholder="Ingrese su usuario" CssClass="form-control" MaxLength="10" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator id="rqUsu" runat="server"
                ControlToValidate="txtUsu"
                ErrorMessage="Usuario es requerido"
                ForeColor="Red" EnableClientScript="false"></asp:RequiredFieldValidator>
             <br /><br />
            <asp:Label ID="lblAviso" Text="Nota: Se le enviará un correo con la clave nueva." ForeColor="#4682B4" runat="server"></asp:Label>
            <br /><br />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>&nbsp;&nbsp;
            <asp:Button ID="btnNuevaClave" runat="server" Text="Solicitar clave" OnClick="btnNuevaClave_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>
            </center>
        </div>
    </form>
</body>
</html>
