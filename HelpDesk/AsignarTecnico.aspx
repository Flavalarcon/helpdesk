<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarTecnico.aspx.cs" Inherits="HelpDesk.AsignarTecnico" %>

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
    
     <script type="text/javascript"> 
       window.opener.location.href = window.opener.location.href; 
</script>
   
    <title>Asignar Tecnico</title>
    </head>
<body>
    <nav class="navbar" style="background-color:#4682B4 !important; color: white !important; ">
  <div class="container-fluid">
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

    <form id="form1" runat="server">
        <div style="margin-left:20px;">
            

            <asp:Label ID="Label1" runat="server" ForeColor="#4682B4" Text="Número de ticket: "></asp:Label>&nbsp;
                <asp:Label ID="lblticket" runat="server" ForeColor="#4682B4"></asp:Label><br /><br />
                
                <asp:Label ID="lblTecnico" runat="server" Text="Técnico: " ForeColor="#4682B4"></asp:Label>&nbsp;
                <asp:DropDownList ID="cboTecnico" runat="server" CssClass="form-control dropdown-toggle" Width="150px">                    
                </asp:DropDownList><br /><br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                <asp:Label ID="lblErrorVS" Text="" ForeColor="red" runat="server" Visible="false"></asp:Label>
                <asp:Button id="btnGrabar" runat="server" Text="Asignar" OnClick="btnGrabar_Click" CssClass="btn btn-info" BackColor="White" ForeColor="#4682B4"/>
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" OnClick="btnCerrar_Click" CssClass="btn btn-info" BackColor="White" ForeColor="#4682B4" style="margin-left:80px"/>
        
                </div>
    </form>
</body>
</html>
