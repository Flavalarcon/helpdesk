
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ticket.aspx.cs" Inherits="HelpDesk.ticket" %>

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
    <script type="text/javascript">

        function Confirmacion() {

            var seleccion = confirm("¿Desea confirmar la accion CIERRE?");

            if (seleccion)
                alert("Se realizo el cierre");
            else
                alert("No se realizo el cierre");
            return seleccion;

        }

    </script>
    <title>Atencion de Tickets</title>
</head>
<body>
    <form id="form1" runat="server" class="form-inline">
   
   <nav class="navbar" style="background-color:#4682B4 !important; color: white !important; display:flex; align-items:center; justify-content: center;">
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
    <div class="collapse navbar-collapse" id="myNavbar">
      <ul class="nav navbar-nav">
        <li class="active"><asp:HyperLink ID="listaTicket" runat="server" NavigateUrl="~/ticket.aspx" style="color: white !important;">Tickets</asp:HyperLink></li>
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
        <a class="dropdown-toggle" data-toggle="dropdown" href="#" style="color: white !important;"><span class="glyphicon glyphicon-user" aria-hidden="true" ></span>
        <span class="caret"></span></a>
        <ul class="dropdown-menu">
          <li><asp:HyperLink ID="cambioClave" runat="server" NavigateUrl="~/cambiarClave.aspx" style="color: #4682B4 !important;">Cambio de clave</asp:HyperLink></li>
          <li><asp:LinkButton ID="cerrarsesion" runat="server" OnClick="cerrarsesion_Click" ForeColor="#4682B4">Cerrar sesion</asp:LinkButton></li>
          
        </ul>
      </li>
      </ul>
    </div>
  </div>
</nav>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script src="../js/bootstrap.min.js"></script>

<script>
   
function checkTextAreaMaxLength(textBox, e, length) {

var mLen = textBox["MaxLength"];
if (null == mLen)
   mLen = length;

var maxLength = parseInt(mLen);
 if (!checkSpecialKeys(e)) {
if (textBox.value.length > maxLength - 1) {
   if (window.event)//IE
   {
     e.returnValue = false;
     return false;
   }
    else//Firefox
   e.preventDefault();
   }
  }
}
       
function checkSpecialKeys(e) {
if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
 return false;
else
 return true;
}

function contadorTexto(campo, cuentaCampos, limiteMaximo) {
   if (campo.value.length > limiteMaximo) //Si muy largo, cortar.
      campo.value = campo.value.substring(0, limiteMaximo);
   else
      cuentaCampos.value = (limiteMaximo - campo.value.length);
    }

</script>

<div  class="form-group" style="margin-left: 20px;" >
    <h2><asp:Label id="Titulo" runat="server" style="color: #4682B4;"></asp:Label></h2><br />            
</div>
    
     
    
        
    <%--<asp:Button ID="btnHistorial" runat="server" Text="Seleccionar" CssClass="btn btn-round" OnClick="btnHistorial_Click" BackColor="White" ForeColor="Black"/>--%>

    
  <div class="form-group">
    <asp:Label ID="lblFec" runat="server" Text="Fecha Inicio" style="color: #4682B4; margin-left: 20px;"></asp:Label> &nbsp;
 <asp:TextBox ID="txtFec" runat="server" TextMode="Date" CssClass="form-control" Width="150px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
 <asp:Label ID="lblfec2" runat="server" Text="Fecha Fin" ForeColor="#4682B4"></asp:Label> &nbsp;
 <asp:TextBox ID="TxtFec2" runat="server" TextMode="Date" CssClass="form-control" Width="150px"></asp:TextBox>&nbsp&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp&nbsp;

<asp:Label ID="lblEstado" runat="server" Text="Estados" style="color: #4682B4;"></asp:Label> &nbsp;
<asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control dropdown-toggle" >
    <asp:ListItem Value="0" >Todos</asp:ListItem>
    <asp:ListItem Value="1">Pendiente</asp:ListItem>
    <asp:ListItem Value="2">En proceso</asp:ListItem>
    <asp:ListItem Value="3">En ejecución</asp:ListItem>
    <asp:ListItem Value="4">Aprobado</asp:ListItem>
</asp:DropDownList>
&nbsp;&nbsp;
<asp:Button ID="btnFiltro" runat="server" Text="Filtrar" OnClick="btnFiltro_Click" CssClass="btn btn-info" BackColor="White" ForeColor="#4682B4"> </asp:Button>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
<asp:Button ID="btnCierre" runat="server" Text="Cierre" OnClick="btnCierre_Click" OnClientClick="return Confirmacion();"  CssClass="btn btn-info" BackColor="White" ForeColor="#4682B4" style="text-align:right; margin-left:140px !important;"/>
</div>

<br /><br />
 
<div class="col-md-12" style="margin-left:0.7%">
<asp:Button ID="btnAsignar" Text="Asignar" runat="server" OnClick="btnAsignar_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>
    <asp:Button ID="btnEjecucion" Text="En ejecucion" runat="server" OnClick="btnEjecucion_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>&nbsp;&nbsp;
    <asp:Button ID="btnRealizado" Text="Finalizado" runat="server" OnClick="btnRealizado_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>

    
<asp:Label class="col-md-offset-2" ID="lblHistorial" runat="server" Text="Historial" ForeColor="#4682B4" ></asp:Label>&nbsp;
    <asp:DropDownList class="col-md-offset-2" ID="cboHistorial" runat="server"  CssClass="form-control" Width="150px" OnSelectedIndexChanged="cboHistorial_SelectedIndexChanged" AutoPostBack="true" >    
    </asp:DropDownList>
        <br /><br /><br />
    </div>
    

        <div style="margin-left: 20px;">
<asp:GridView ID="gvTicket" runat="server" CssClass="table table-striped table-bordered table-list table-hover"
RowStyle-BackColor="White" ShowHeaderWhenEmpty="true" Width="100%" AutoGenerateColumns="False" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#4682B4"
RowStyle-ForeColor="Black" DataKeyNames="idTicket" EditRowStyle-Wrap="False" HeaderStyle-BorderColor="#1592C8"
SortedDescendingHeaderStyle-Wrap="True" RowStyle-Width="200px" SelectedRowStyle-Width="200px" 
EditRowStyle-Width="100px" AllowPaging="True" PagerStyle-HorizontalAlign="Center" BorderColor="Black"
OnPageIndexChanging="gvTicket_PageIndexChanging" PagerStyle-BackColor="#4682B4" PagerStyle-ForeColor="white" PagerStyle-Font-Size="18px"
OnSelectedIndexChanged="gvTicket_SelectedIndexChanged" OnRowDataBound="gvTicket_RowDataBound">
<Columns>

<asp:BoundField DataField="idTicket" HeaderText="N°" DataFormatString="{0:D6}" HtmlEncode="False" >  
<ControlStyle Width="40px"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="fecRegistro" HeaderText="Fecha de registro" ControlStyle-CssClass="hidden-xs"  >  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="username" HeaderText="Usuario" ControlStyle-CssClass="hidden-xs" >  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="requerimiento" HeaderText="Requerimiento" ControlStyle-CssClass="hidden-xs"  ItemStyle-Width="300px">  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="desEstado" HeaderText="Estado" ControlStyle-CssClass="hidden-xs" >  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="usertec" HeaderText="Tecnico" ControlStyle-CssClass="hidden-xs" ReadOnly="true">  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="fecAtendido" HeaderText="Fecha de atencion" ControlStyle-CssClass="hidden-xs" ReadOnly="true">  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>    
<asp:BoundField DataField="tiempo" HeaderText="Tiempo" ControlStyle-CssClass="hidden-xs" ReadOnly="true">  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:BoundField DataField="observaciones" HeaderText="Observaciones" ControlStyle-CssClass="hidden-xs">  
<ControlStyle CssClass="hidden-xs"></ControlStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Imagen" ControlStyle-CssClass="hidden-xs">
<ItemTemplate>
<asp:HyperLink ID="hlDescarga" runat="server" Text='<%# Eval("nombreImg") %>' ForeColor="Black" NavigateUrl='<%# Eval("idTicket", "descarga.aspx?idTicket={0}" ) %>' Target="_blank" ></asp:HyperLink>
</ItemTemplate>        
</asp:TemplateField>
    <asp:TemplateField HeaderText="Acción" ControlStyle-CssClass="btn btn-info">
<ItemTemplate>
<asp:Button ID="btnEditar" Text="Editar" runat="server" OnClick="btnEditar_Click" class="btn btn-info" BackColor="White" ForeColor="#4682B4"/>
</ItemTemplate>        
</asp:TemplateField>
    
</Columns>
</asp:GridView><br />

    
    
    </div>
   
                   
    </form>
</body>
</html>
