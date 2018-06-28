<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="HelpDesk.RegistroUsuario" %>

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
    <title>Registro de Usuarios</title>
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
        <a class="navbar-brand" href="#" style="color: white !important; margin-left: 30px;">Informática Delta </a>
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
          <li><asp:LinkButton ID="cerrarsesion" runat="server" OnClick="cerrarsesion_Click" ForeColor="#4682B4" Font-Size="Medium">Cerrar sesion</asp:LinkButton></li>
          
        </ul>
      </li>
      </ul>
    </div>
  </div>
</nav>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script src="../js/bootstrap.min.js"></script>

        <div style="margin-left: 60px;">
            
            <h2><asp:Label ID="lbltl" runat="server" Text="Registro de usuario" ForeColor="#4682B4"></asp:Label></h2>
                <br /><br />
            </div>
        <center>
            <asp:Label ID="lblUsu" runat="server" ForeColor="#4682B4">Usuario <sup>(*)</sup></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtUsuario" runat="server" class="form-control"  MaxLength="10" width="200px" placeholder="Max. 10 caracteres"></asp:TextBox>
            
            <br />
            <asp:Label ID="lblNom" runat="server" Text="" ForeColor="#4682B4">Nombre completo</asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtNom" runat="server" class="form-control"  MaxLength="100" width="200px" placeholder="Max. 100 caracteres"></asp:TextBox>
            
                <br />
            <asp:Label ID="lblEmail" runat="server" Text="" ForeColor="#4682B4">Email</asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtEmail" runat="server" class="form-control"  MaxLength="150" width="200px" placeholder="Max. 150 caracteres"></asp:TextBox>
             
            <br />
            <asp:Label ID="lblPerfil" runat="server" Text="" ForeColor="#4682B4">Perfil</asp:Label>&nbsp;&nbsp;
            <asp:DropDownList ID="cboPerfil" runat="server" class="form-control" width="200px"></asp:DropDownList>

            <br />
            <asp:Label ID="lblNota" runat="server" ForeColor="#4682B4"><sup>(*)</sup> : Es Requerido</asp:Label>
            <br />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorVS" Text="" ForeColor="red" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblCorrecto" runat ="server" ForeColor="#00cc00" Visible="false"></asp:Label>
            <br /><br />
            <asp:LinkButton id="btnRegistrar" runat="server" Text="Registrar"  OnClick="btnRegistrar_Click" class="btn btn-primary" ForeColor="#4682B4">Registrar</asp:LinkButton>
            </center>
                
         
        
    </form>
</body>
</html>
