<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaUsuario.aspx.cs" Inherits="HelpDesk.ListaUsuario" %>

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
    <title>Usuarios</title>
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
          <li><asp:LinkButton ID="cerrarsesion" runat="server" OnClick="cerrarsesion_Click" ForeColor="#4682B4" Font-Size="Medium">Cerrar Sesion</asp:LinkButton></li>
          
        </ul>
      </li>
      </ul>
    </div>
  </div>
</nav>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script src="../js/bootstrap.min.js"></script>

        <div style="margin-left: 60px;">
            
            <h2><asp:Label ID="lbltl" runat="server" Text="Usuarios" ForeColor="#4682B4"></asp:Label></h2>
                <br /><br />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorVS" Text="" ForeColor="red" runat="server" Visible="false"></asp:Label>
            
            <asp:GridView ID="gvUsuario" runat="server" CssClass="table table-striped table-bordered table-list"
ShowHeaderWhenEmpty="true" Width="1100px" AutoGenerateColumns="False" DataKeyNames="id" HeaderStyle-BackColor="#4682B4" HeaderStyle-ForeColor="White"
OnPageIndexChanging="gvUsuario_PageIndexChanging" OnRowCancelingEdit="gvUsuario_RowCancelingEdit" 
OnRowEditing="gvUsuario_RowEditing"  OnRowUpdating="gvUsuario_RowUpdating" 
EditRowStyle-Wrap="False" SortedDescendingHeaderStyle-Wrap="True" RowStyle-Width="100px" OnRowDataBound="gvUsuario_RowDataBound"
SelectedRowStyle-Width="100px" EditRowStyle-Width="100px" AllowPaging="True" RowStyle-BackColor="White"
                PagerStyle-BackColor="#4682B4" PagerStyle-ForeColor="white" PagerStyle-Font-Size="18px" PagerStyle-HorizontalAlign="Center"
    >
<Columns>

<asp:BoundField DataField="username" HeaderText="Usuario" ControlStyle-CssClass="hidden-xs">  
<ControlStyle CssClass="form-control" Width="150px"></ControlStyle>
</asp:BoundField>

<asp:BoundField DataField="nombre" HeaderText="Nombre" ControlStyle-CssClass="hidden-xs">  
<ControlStyle CssClass="form-control" Width="150px"></ControlStyle>
</asp:BoundField>

<asp:BoundField DataField="email" HeaderText="Email" ControlStyle-CssClass="hidden-xs">  
<ControlStyle CssClass="form-control" Width="150px"></ControlStyle>
</asp:BoundField>
<asp:TemplateField ControlStyle-CssClass="form-control dropdown-toggle" HeaderText="Perfil">
    <EditItemTemplate>
        <asp:DropDownList ID="cboPerfil" runat="server" CssClass="form-control dropdown-toggle"></asp:DropDownList>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:Label ID="lblPerfil" runat="server" Text='<%# Bind("descPerfil") %>' ></asp:Label>
    </ItemTemplate>
    <ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:TemplateField>
<asp:BoundField DataField="fecRegistro" HeaderText="Fecha de registro" ControlStyle-CssClass="hidden-xs" ReadOnly="true">  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
    
<asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-primary" HeaderText="Acción">  
<ControlStyle CssClass="btn btn-primary" ForeColor="#4682B4" ></ControlStyle>
</asp:CommandField>

</Columns>
   
</asp:GridView>
         
        </div>
    </form>
</body>
</html>
