﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HelpDesk.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="/CSS/bootstrap-theme.css" rel="stylesheet" />
    <link rel="stylesheet" href="../CSS/login2.css"  />
    <link rel="stylesheet" href="/CSS/botones.css"  />
    <title>Atencion de Tickets</title>

    <script type="text/javascript" >
    
</script>
</head>
<body>
    <div class="container">
            <div class="row">
        <div class="col-md-12">
            <div class="wrap">
                <nav class="navbar" style="background-color:#4682B4 !important; color: white !important; ">
  <div class="container-fluid" style="width:100%;">
    <div class="navbar-header" style="background-color:#4682B4 !important; color: white !important;">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span> 
      </button>
      <%--<asp:Image ID="imgLogo" runat="server" ImageUrl="~/img/delta2.png" Width="100px" />--%>
        <a class="navbar-brand" href="#" style="color: white !important;  margin-left: 30px;">Informática Delta </a>
    </div>
   
  </div>
</nav>
                
    <form id="form1" runat="server" class="login">
        <div style="margin-top:100px;">
        <center>
            <h2><asp:Label ID="titulo" class="form-title" runat="server" ForeColor="#4682B4" Text="Atencion de tickets" Width="260px" style="margin-top:50px;"></asp:Label></h2>
            <br /><br />
            <asp:Label ID="lblusu" Text="Usuario" runat="server" ForeColor="#4682B4"></asp:Label>&nbsp; &nbsp;
            <asp:TextBox ID="txtUsuario" runat="server" placeHolder="Ingrese su usuario" MaxLength="10" CssClass="form-control" Width="200px" foreColor="Black" BorderColor="#4682B4" ></asp:TextBox><br /><br />
            <asp:Label ID="lblClave" Text="Clave" runat="server" ForeColor="#4682B4"></asp:Label>&nbsp; &nbsp;
            <asp:TextBox ID="txtClave" runat="server" placeHolder="Ingrese su clave" TextMode="Password" MaxLength="10" CssClass="form-control" Width="200px" foreColor="Black" BorderColor="#4682B4"></asp:TextBox>
            <br />
            <asp:Label ID="lblErrorVS" Text="" ForeColor="red" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblError" Text="" runat="server" ForeColor="Red" Visible="false"></asp:Label><br />
            <asp:HyperLink ID="hlOlvClave" runat="server" Text="¿Olvido su clave? Solicite una nueva." CssClass="remember-forgot" NavigateUrl="NuevaClave.aspx" ForeColor="#4682B4"></asp:HyperLink>
            
            <br /><br /><br />
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" BackColor="#4682B4"/>
        </center>
        </div>
    </form>
            </div></div></div></div>
</body>
</html>
