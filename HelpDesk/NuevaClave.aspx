<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevaClave.aspx.cs" Inherits="HelpDesk.NuevaClave" %>

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

        
        
        
      </ul>
     
    </div>
  </div>
</nav>

        
            <div class="login_side" style="margin-left:20px;">
            <h2><asp:Label ID="lblTitulo" runat="server" Text="Cambio de clave" ForeColor="#4682B4"></asp:Label></h2>
            <br /><br />
            </div>
            <center>
            
            <asp:Label ID="lblclaveN" runat="server" Text="Clave Nueva" ForeColor="#4682B4"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtClaveN" runat="server" placeholder="Max. 10 caracteres" TextMode="Password" class="form-control" Width="200px" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="RqClave2" runat="server"
            ControlToValidate="txtClaveN"
            ErrorMessage="Es requerido" EnableClientScript="false"
            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <br /><br />
            <asp:Label ID="lblclaveN2" runat="server" Text="Repita su nueva clave" ForeColor="#4682B4"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtClaveN2" runat="server" placeholder="Max. 10 caracteres" TextMode="Password" class="form-control" Width="200px" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="RqClave3" runat="server"
            ControlToValidate="txtClaveN2"
            ErrorMessage="Es requerido" EnableClientScript="false"
            ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>                                    
            
            <br />
            <asp:Label ID="lblNota" Text="" Visible="false" ForeColor="#00cc00" runat="server"></asp:Label>
                <br />
            <asp:Label ID="lblError" ForeColor="red" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorVS" Text="" ForeColor="red" runat="server" Visible="false"></asp:Label>
            <br /><br />
            <asp:Button ID="btnCancelar" runat="server" Text="Regresar" OnClick="btnCancelar_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>
            <asp:Button ID="btnCambiar" runat="server" Text="Cambiar clave" OnClick="btnCambiar_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>
            </center>
           
                
    </form>
</body>
</html>
