<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ticketFin.aspx.cs" Inherits="HelpDesk.ticketFin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <script type="text/javascript"> 
       window.opener.location.href = window.opener.location.href; 
</script>
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
    <title>Ticket Finalizado</title>
     
</head>
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
       </script>
        <script>
            function checkSpecialKeys(e) {
                if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                    return false;
                else
                    return true;
            }

   </script>
        <script type="text/javascript" language="javascript">
            function contadorTexto(campo, cuentaCampos, limiteMaximo) {
                if (campo.value.length > limiteMaximo) //Si muy largo, cortar.
                    campo.value = campo.value.substring(0, limiteMaximo);
                else
                    cuentaCampos.value = (limiteMaximo - campo.value.length);
            }
     </script>
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
            <asp:Label ID="ticket" runat="server" ForeColor="#4682B4"></asp:Label><br /><br />
    
                <asp:Label ID="lblObs" runat="server" Text="Observaciones: " ForeColor="#4682B4"></asp:Label>
                <asp:TextBox ID="txtobs" runat="server" TextMode="MultiLine" MaxLength="200" class="form-control btn-round" Width="300px" Height="100px" onkeyDown=" contadorTexto(this.form.txtobs, this.form.txtContador, 200);" onkeyup="contadorTexto(this.form.txtobs, this.form.txtContador, 200);"></asp:TextBox><br />
                 <div style="margin-left:220px;">
                <asp:TextBox ID="txtContador" runat="server" ReadOnly="true" Width="40px"></asp:TextBox><asp:Label ID="lblContador" runat="server" Text="/200" ForeColor="#4682B4" Font-Size="Medium"></asp:Label>
                     </div>
                <br /><br />

                <asp:Button ID="btngrabar" runat="server" CssClass="btn btn-info" OnClick="btngrabar_Click" Text="Grabar" BackColor="white" ForeColor="#4682B4"/>
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btn btn-info" OnClick="btnCerrar_Click" BackColor="white" ForeColor="#4682B4" style="margin-left:165px"/>
    
        </div>
    </form>
</body>
</html>
