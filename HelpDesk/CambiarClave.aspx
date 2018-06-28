<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="HelpDesk.CambiarClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <title>Clave nueva</title>
</head>
<body>

    <form id="form1" runat="server">
        <nav class="navbar" style="background-color:#4682B4 !important; color: white !important; display:flex; align-items:center; justify-content: center;">
  <div class="container-fluid" style="width:100%;">
    <div class="navbar-header" style="background-color:#4682B4 !important; color: white !important;">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span> 
      </button>
      <%--<asp:Image ID="imgLogo" runat="server" ImageUrl="~/img/delta2.png" Width="100px" />--%>
        <a class="navbar-brand" href="#" style="color: white !important;  margin-left: 20px;">Informática Delta </a>
    </div>
    <div class="collapse navbar-collapse" id="myNavbar">
      <ul class="nav navbar-nav">

        <li class="active"><a href="ticket.aspx" style="color: white !important;">Tickets</a></li>
          <li>
        <asp:HyperLink ID="regRequerimiento" runat="server" NavigateUrl="~/Registrarticket.aspx" style="color: white !important;">Registrar Ticket</asp:HyperLink>            
        </li>
        
          <li><asp:HyperLink ID="ListaUsuarios" runat="server" NavigateUrl="~/ListaUsuario.aspx" style="color: white !important;">Usuarios</asp:HyperLink>
        </li>
          <li><asp:HyperLink ID="regUsuarios" runat="server" NavigateUrl="~/registroUsuario.aspx" style="color: white !important;">Registrar Usuario</asp:HyperLink>
        </li>
        
        
      </ul>
      <ul class="nav navbar-nav navbar-right" style="display:flex; align-items: center;">
        <li><asp:Label ID="lblUsuario" runat="server" ForeColor="White"></asp:Label></li>&nbsp;
        <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#" style="color: white !important;"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>
        <span class="caret"></span></a>
        <ul class="dropdown-menu">
          <li><asp:HyperLink ID="cambioClave" runat="server" NavigateUrl="~/cambiarClave.aspx" style="color: #4682B4 !important;" Font-Size="Medium">Cambio de clave</asp:HyperLink></li>
          <li><asp:LinkButton ID="cerrarsesion" runat="server" OnClick="cerrarsesion_Click" Font-Size="Medium">Cerrar Sesion</asp:LinkButton></li>
          
        </ul>
      </li>
      </ul>
    </div>
  </div>
</nav>

        
            <div class="login_side" style="margin-left:20px;">
            <h2><asp:Label ID="lblTitulo" runat="server" Text="Cambio de clave" ForeColor="#4682B4"></asp:Label></h2>
            <br /><br />
            </div>
            <center>
            <asp:Label ID="lblclaveA" runat="server" Text="Clave actual" ForeColor="#4682B4"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtClaveA" runat="server" placeholder="Ingrese su clave actual. Max. 10" TextMode="Password" class="form-control" Width="250px" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="rqClave1" runat="server"
            ControlToValidate="txtClaveA"
            ErrorMessage="Clave anterior es requerido"
            ForeColor="Red" EnableClientScript="false"></asp:RequiredFieldValidator>
            <br /><br />
            <asp:Label ID="lblclaveN" runat="server" Text="Clave Nueva" ForeColor="#4682B4"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtClaveN" runat="server" placeholder="Ingrese su clave nueva. Max. 10" TextMode="Password" class="form-control" Width="250px" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="RqClave2" runat="server"
            ControlToValidate="txtClaveN"
            ErrorMessage="Clave nueva es requerido" EnableClientScript="false"
            ForeColor="Red"></asp:RequiredFieldValidator>
            <br /><br />
            <asp:Label ID="lblclaveN2" runat="server" Text="Repita su nueva clave" ForeColor="#4682B4"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtClaveN2" runat="server" placeholder="Ingrese su clave nuevamente. Max. 10" TextMode="Password" class="form-control" Width="270px" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="RqClave3" runat="server"
            ControlToValidate="txtClaveN2"
            ErrorMessage="Repetir clave nueva es requerido" EnableClientScript="false"
            ForeColor="Red"></asp:RequiredFieldValidator>
            
            
            
            
            <br />
            
            
            <asp:Label ID="lblError" ForeColor="red" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblCorrecto" Text="" ForeColor="#00cc00" runat="server" Visible="false"></asp:Label>
            <br /><br />
            
            <asp:Button ID="btnCambiar" runat="server" Text="Cambiar clave" OnClick="btnCambiar_Click" class="btn btn-primary" ForeColor="#4682B4"/>
            </center>
           
                
    </form>
</body>
</html>
