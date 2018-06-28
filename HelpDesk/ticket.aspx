
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
   <%-- <script src="https://use.fontawesome.com/45e03a14ce.js"></script>--%>
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

function contadorTexto(campo, cuentaCampos, limiteMaximo, limiteMinimo) {
   if (campo.value.length > limiteMaximo) //Si muy largo, cortar.
      campo.value = campo.value.substring(0, limiteMaximo);
   else
      cuentaCampos.value = (limiteMinimo + campo.value.length);
    }

</script>

<div style="margin-left:25px">
<div  class="form-group" style="margin-left: 20px;" >
    <h2><asp:Label id="Titulo" runat="server" style="color: #4682B4;"></asp:Label></h2>   
     <asp:DropDownList  ID="DropDownListTitulo" runat="server"  class="form-control" Width="320px" OnSelectedIndexChanged="DropDownListTitulo_SelectedIndexChanged" Height="50px" style="border:none; font-size: 30px; color: #4682B4; font-weight:bold; box-shadow: 0px 0px 0px 0px" AutoPostBack="true">  
         <asp:ListItem Value="0" >Tickets Pendientes</asp:ListItem>
         <asp:ListItem Value="1" >Mis Tickets</asp:ListItem>
            </asp:DropDownList>        
      
</div>
            
   <div class="col-md-12" style="margin-top: 20px">
     <span class ="col-md-9">
        <asp:Button ID="btnAsignar" Text="Asignar" runat="server" OnClick="btnAsignar_Click" class="btn btn-primary" ForeColor="#4682B4"/>
        <asp:Button ID="btnEjecucion" Text="En ejecucion" runat="server" OnClick="btnEjecucion_Click" class="btn btn-primary" ForeColor="#4682B4"/>
        <asp:Button ID="btnRealizado" Text="Finalizado" runat="server" OnClick="btnRealizado_Click" class="btn btn-primary" ForeColor="#4682B4"/>   
    </span>
         <br /><br /><br />
    </div>
       
 <div class="col-md-12">
     <span class ="col-md-5">
         <span style="margin-left:10px">
             <asp:Label ID="lblFec" runat="server" Text="Fecha Inicio" style="color: #4682B4;"></asp:Label>
            <asp:TextBox ID="txtFec" runat="server" TextMode="Date" class="form-control" Width="130px" style="font-size:13px"  ></asp:TextBox>
         </span>
         <span style="margin-left:20px">
            <asp:Label ID="lblfec2" runat="server" Text="Fecha Fin" ForeColor="#4682B4"></asp:Label> 
            <asp:TextBox ID="TxtFec2" runat="server" TextMode="Date" class="form-control" Width="130px" style="font-size:13px" ></asp:TextBox>
        </span>
     <br /><br />
     </span>

     <span class ="col-md-7">
         <span>
          <asp:Label ID="lblHistorial" runat="server" Text="Historial" ForeColor="#4682B4" ></asp:Label>&nbsp;
         <asp:DropDownList  ID="cboHistorial" runat="server"  class="form-control" Width="150px" OnSelectedIndexChanged="cboHistorial_SelectedIndexChanged" AutoPostBack="true" >    
            </asp:DropDownList>
           
            <asp:Label ID ="lblCorrecto" runat="server" Visible="false" ForeColor="#00cc00"></asp:Label>
        </span>

        <span style="margin-left: 15px">
            <asp:Label ID="lblEstado" runat="server" Text="Estados" style="color: #4682B4;"></asp:Label>
            <asp:DropDownList ID="cboEstado" runat="server" class="form-control dropdown-toggle">
                <asp:ListItem Value="0" >Todos</asp:ListItem>
                <asp:ListItem Value="1">Pendiente</asp:ListItem>
                <asp:ListItem Value="2">En proceso</asp:ListItem>
                <asp:ListItem Value="3">En ejecución</asp:ListItem>
                <asp:ListItem Value="4">Aprobado</asp:ListItem>
            </asp:DropDownList>
        </span>

        <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" OnClick="btnFiltro_Click" class="btn btn-primary" ForeColor="#4682B4" style="margin-left:15px; margin-bottom:10px"> </asp:Button>
     </span>
    </div>
   

      <div style="margin-left: 35px;">
          <asp:GridView ID="gvTicket" runat="server" class="table table-striped table-bordered table-list table-hover"
                RowStyle-BackColor="White" ShowHeaderWhenEmpty="true" Width="100%" AutoGenerateColumns="False" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#4682B4"
                RowStyle-ForeColor="Black" DataKeyNames="idTicket" EditRowStyle-Wrap="False" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None"
                SortedDescendingHeaderStyle-Wrap="True" RowStyle-Width="200px" SelectedRowStyle-Width="200px" 
                EditRowStyle-Width="100px" AllowPaging="True" PagerStyle-HorizontalAlign="Center" BorderColor="Black"
                OnPageIndexChanging="gvTicket_PageIndexChanging" PagerStyle-BackColor="#4682B4" PagerStyle-ForeColor="white" PagerStyle-Font-Size="18px"
                OnSelectedIndexChanged="gvTicket_SelectedIndexChanged" OnRowDataBound="gvTicket_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="idTicket" HeaderText="N°" DataFormatString="{0:D6}" HtmlEncode="False">
                    <FooterStyle HorizontalAlign="Center" Width="10px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="10px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="10px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fecRegistro" HeaderText="Fecha de registro">  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />

                    </asp:BoundField>
                    <asp:BoundField DataField="username" HeaderText="Usuario" >  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="area" HeaderText="Área" >  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="requerimiento" HeaderText="Requerimiento">  
                    <FooterStyle Width="150px" Wrap="true" />
                    <HeaderStyle  Width="150px" Wrap="true" />
                    <ItemStyle  Width="150px" Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="desEstado" HeaderText="Estado">  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="usertec" HeaderText="Tecnico"  ReadOnly="true">  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fecAtendido" HeaderText="Fecha de atencion" ReadOnly="true">  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    </asp:BoundField>    
                    <asp:BoundField DataField="tiempo" HeaderText="Tiempo" ReadOnly="true">  
                    <FooterStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Width="20px" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones">  <%--9--%>
                    <FooterStyle  Width="150px" Wrap="true" />
                    <HeaderStyle  Width="150px" Wrap="true" />
                    <ItemStyle Width="150px" Wrap="true" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Imagen">
                    <ItemTemplate>
                    <asp:HyperLink ID="hlDescarga" runat="server" Text='<%# Eval("nombreImg") %>' ForeColor="Black" NavigateUrl='<%# Eval("idTicket", "VerImagen.aspx?idTicket={0}" ) %>' Target="_blank" ></asp:HyperLink>
                    </ItemTemplate>
                    <FooterStyle  Width="100px" Wrap="true" />
                    <HeaderStyle HorizontalAlign="Center"  Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                    <asp:Button ID="btnEditar" Text="Editar" runat="server" OnClick="btnEditar_Click" class="btn btn-primary" ForeColor="#4682B4"/>
                    </ItemTemplate>
                    <FooterStyle  Width="100px" Wrap="true" />
                    <HeaderStyle  Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    </asp:TemplateField>
    
                    </Columns>
                    </asp:GridView><br />

  </div>  
    
    </div>
    <div  style="margin:auto;width:30px">
         <asp:Button ID="btnCierre" runat="server" Text="Cierre" OnClick="btnCierre_Click" OnClientClick="return Confirmacion();" BackColor="Red" class="btn btn-primary col-md-offset-11" ForeColor="#4682B4" style="margin-bottom:40px"/>  
      </div>  


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        


  



<!-- modal editar-->
<div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"> <asp:Label ID="lblticket" runat="server" ForeColor="#4682B4"></asp:Label></h4>
                    </div>
                 
                    <div class="modal-body">
                        <div class='row'>
                        <div class="col-md-12" style="margin-top: 30px">
                            <asp:Label class="col-md-3" ID="req" runat="server" Text="Requerimiento: " ForeColor="#4682B4"></asp:Label>
                            <asp:TextBox ID="txtreq" runat="server" TextMode="MultiLine" class="form-control col-md-6" Width="50%" Height="150px" MaxLength="500" onkeyDown=" contadorTexto(this.form.txtreq, this.form.txtContador, 500, 0);" onkeyup="contadorTexto(this.form.txtreq, this.form.txtContador, 500, 0);"></asp:TextBox>
                            <span class="col-md-3">
                                <asp:TextBox ID="txtContador" runat="server" ReadOnly="true" Width="40px"></asp:TextBox>
                                <asp:Label ID="lblContador" runat="server" Text="/500" ForeColor="#4682B4" Font-Size="Medium"></asp:Label>
                            </span>
                        </div>


                        <div class="col-md-12" style="margin-top: 40px">
                            <asp:Label class="col-md-3" ID="Obs" runat="server" Text="Observaciones: " ForeColor="#4682B4"></asp:Label>
                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" class="form-control col-md-6" Width="50%" Height="150px" MaxLength="500" onkeyDown=" contadorTexto(this.form.txtreq, this.form.txtContador, 500, 0);" onkeyup="contadorTexto(this.form.txtObs, this.form.txtContador2, 500, 0);"></asp:TextBox>
                            <span class="col-md-3">
                                <asp:TextBox ID="txtContador2" runat="server" ReadOnly="true" Width="40px"></asp:TextBox>
                                <asp:Label ID="lblContador2" runat="server" Text="/500" ForeColor="#4682B4" Font-Size="Medium"></asp:Label>
                            </span>
                        </div>


                        <div class="col-md-12" style="margin-top: 40px; margin-left: 15px">
                            <asp:Label ID="lblImg" runat="server" Text="Seleccione para subir una imagen: " ForeColor="#4682B4"></asp:Label>
                            <asp:FileUpload ID="fileImg" runat="server" ToolTip="¿Desea subir una imagen?" CssClass="form-control" style="border:none; box-shadow:0px 0px 0px 0px;" ForeColor="#4682B4" Width="350px" /><br />
                            <br />
                            <asp:Label ID="lblCorrectoEditar" runat="server" Text="" ForeColor="#33cc33"></asp:Label>
                            <asp:Label ID="lblErrorEditar" runat="server" Text="" ForeColor="Red"></asp:Label>
                            
                        </div>
                    </div>
                    </div>
                
                 <div class="modal-footer">
                      <asp:Button ID="btnEditarTicket" runat="server" OnClick="btnEditarTicket_Click" Text="Grabar" class="btn btn-primary" ForeColor="#4682B4"/>
                     <asp:Button ID="btnCerrar" runat="server" OnClick="btnCerrar_Click" Text="Cerrar" class="btn btn-primary" ForeColor="#4682B4"/>
                      <%--<button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Cerrar</button>--%>
                 </div>
              </div>
           </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

        <!--modal mensaje -->
        <div class="modal fade" id="myModal2" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModal2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"> Mensaje de Error</h4>
                    </div>
                 
                    <div class="modal-body">
                        
                            <asp:Label ID="lblError" runat="server" ForeColor="#4682B4"/>
                           
                 <div class="modal-footer">
                      <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Cerrar</button>

                 </div>
              </div>
           </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            </div>

    </form>
</body>
</html>