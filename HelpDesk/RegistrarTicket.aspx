
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarTicket.aspx.cs" Inherits="HelpDesk.RegistrarTicket" %>

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
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="/CSS/txt.css"  />
    



<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="/js/txt.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/CSS/botones.css"  />
    <link rel="stylesheet" href="/CSS/grillaDinamic.css"  />
    <link href="/CSS/estiloboton.css" rel="stylesheet" />
    <link href="/CSS/bootstrap.css" rel="stylesheet" />
    <script src="/js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <%--<script src="https://use.fontawesome.com/45e03a14ce.js"></script>--%>
    
    <title>Registrar Tickets</title>
</head>
<body>
    <form id="form1" runat="server" class="form-inline" >
   
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
          <li><asp:HyperLink ID="cambioClave" runat="server" NavigateUrl="~/cambiarClave.aspx" style="color: #4682B4 !important;" Font-Size="Medium">Cambio de clave</asp:HyperLink></li>
          <li><asp:LinkButton ID="cerrarsesion" runat="server" OnClick="cerrarsesion_Click" ForeColor="#4682B4"  Font-Size="Medium">Cerrar Sesion</asp:LinkButton></li>
          
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

//function contadorTexto(campo, cuentaCampos, limiteMaximo) {
//   if (campo.value.length > limiteMaximo) //Si muy largo, cortar.
//      campo.value = campo.value.substring(0, limiteMaximo);
//   else
//      cuentaCampos.value = (limiteMaximo - campo.value.length);
//    }

    function contadorTexto(campo, cuentaCampos, limiteMaximo, limiteMinimo) {
   if (campo.value.length > limiteMaximo) //Si muy largo, cortar.
      campo.value = campo.value.substring(0, limiteMaximo);
   else
      cuentaCampos.value = (limiteMinimo + campo.value.length);
    }

</script>

    <div class="col-md-12">
    <h2><asp:Label id="Titulo" runat="server" style="color: #4682B4; margin-left:45px" Text="Registrar Ticket"></asp:Label></h2>         
  
        
        <div class="btn-group col-md-4 col-md-offset-4" data-toggle="buttons" style="margin-top:40px">            
            <asp:RadioButton ID="chkSoporte" Text="Soporte Tecnico" runat="server" Width="33%" CssClass="btn btn-primary" Font-Size="Large"/>
            <asp:RadioButton ID="chkAdm" runat="server" Text="Administrativa" Width="33%" CssClass="btn btn-primary" Font-Size="Large"/>
            <asp:RadioButton ID="chkRRHH" runat="server" Text="Recursos Humanos" Width="33%" CssClass="btn btn-primary" Font-Size="Large" />         
          </div>

    <div class="col-md-7 col-md-offset-3" style="margin-bottom:20px">  
    <asp:TextBox id="txtReq" style="margin-top:30px" runat="server" placeholder="Ingrese Requerimiento" TextMode="MultiLine" class="form-control" Width="88%" Height="200px" MaxLength="500" onkeyDown=" contadorTexto(this.form.txtReq, this.form.txtContador, 500,0);" onkeyup="contadorTexto(this.form.txtReq, this.form.txtContador, 500,0);"/>
    <span style="position: absolute;   bottom: 0;  right: 0;">
    <asp:TextBox ID="txtContador" runat="server" ReadOnly="true" Width="40px" class="form-control" />
    <asp:Label ID="txtQuinientos" runat="server" Text="/500" ForeColor="#4682B4" Font-Size="Medium"></asp:Label>
     </span>    
        </div>
    </div>
    
     <div style="width:350px; margin:0 auto;">
    <asp:Label ID="lblImg" runat="server" Text="Seleccione para subir una imagen (Formato .jpg .png): " ForeColor="#4682B4" ></asp:Label>
    <asp:FileUpload ID="fileImg" runat="server" ToolTip="¿Desea subir una imagen?" ForeColor="#4682B4" Width="350px"/>
         </div>

   <%-- <asp:RegularExpressionValidator runat="server" ErrorMessage="Solo permite imágenes (Formato .jpg .gif .png)" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.gif|.GIF|.jpg|.JPG|.jpeg|.PNG|.png|.JPEG)$" ControlToValidate="fileImg" Display="Dynamic" ForeColor="Red" ></asp:RegularExpressionValidator>--%>
        <br />

    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>    
    <asp:Label ID="lblCorrecto" runat="server" ForeColor="#00cc00" Visible="false"></asp:Label><br />

    <div style="width:80px; margin:auto;">
    <asp:LinkButton id="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" class="btn btn-primary" ForeColor="#4682B4" style="margin-left:45%" >Registrar</asp:LinkButton><br />
   </div>
        
    </form>

    <script id="alertaCustom" type="text/x-jquery-tmpl">
        
        $("#btnRegistrar").click(function (e) {
            e.preventDefault();
            swal('Ticket registrado exitosamente. En un momento Soporte Técnico atenderá su ticket.');
        });
    </script>
  <%--  function alertaCustom(e) {
            e.preventDefault();
            swal('Hola mundo');
        }--%>
</body>
</html>
