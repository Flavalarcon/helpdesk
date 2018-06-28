using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using HelpDesk.cs;
using System.Text;
using System.Web.UI;

/// <summary>
/// Descripción breve de Consulta
/// </summary>
public class ConfigSMTP
{
    private string smtp { get; set; }
    private int host { get; set; }
    private string id { get; set; }
    private string password { get; set; }
    private Boolean SslTls { get; set; }
    

    private Conexion cn;

    public ConfigSMTP()
    {
        this.cn = new Conexion();
    }

    //CORREO DE REGISTRO DE USUARIOS
    public void enviar(string correo, string usuario, string clave)
    {
        try
        {

       
        MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add(correo);        
        email.IsBodyHtml = true;
        email.Subject = "Clave - Sistema de Atención de Tickets";
        email.Body = "Usted a sido registrado exitosamente en el sistema de Atención de tickets de Informática Delta, desde ahora podra registrar sus incidencias en el siguiente enlace " +
            "http://192.168.100.98:81/login.aspx y en el menor tiempo posible soporte técnico atenderá su ticket. <br/> " +
            "Su usuario es " + usuario + " " + "y su clave para su ingreso es" +" " + clave + "" +
            "<br/><br/><br/> Atentamente <br/><b>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";       
        
        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());
        
        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);
            cn.Cerrar();

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            smt.Credentials = new System.Net.NetworkCredential(id, password);
            
            smt.EnableSsl = SslTls;
            smt.Send(email);
            }
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error inesperado. El correo no se envió.";
            enviarError(x.Message.ToString(), "envioCorreo", usuario, "envio correo");
            return;
        }

    }
    //NOTIFICACION AL ADMIN DE QUE SE A REGISTRADO UN NUEVO REQUERIMIENTO
    public void emailADM(string usuario, string req)
    {
        try
        {
            cn.Conectar();
            SqlCommand sc1 = new SqlCommand("select responsable from areas where idArea = 1", cn.getConexion());
            SqlDataReader dr1 = sc1.ExecuteReader();
            dr1.Read();
            string responsables = "";
            responsables = dr1["responsable"].ToString();
            cn.Cerrar();
            MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add(responsables); //ezuniga@deltasac.com.pe                
        email.IsBodyHtml = true;
        email.Subject = "Nuevo Requerimiento Registrado - Sistema de Atención de Tickets";
        email.Body = "<b>Se ha registrado un nuevo requerimiento.</b><br/> El siguiente usuario " + usuario + 
            " ha registrado un nuevo requerimiento: " + req + "<br/><br/><br/><b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";
        
        
        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);
            
            

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            
            smt.Credentials = new System.Net.NetworkCredential(id, password);
            smt.EnableSsl = SslTls;
            
            smt.Send(email);
        }
        cn.Cerrar();
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Contacte a Soporte Técnico. " + x.Message.ToString();
            Console.WriteLine( x.Message.ToString());
            
            enviarError(x.Message.ToString(), "envioCorreo", usuario, "emailADM");
            return;
        }
    }

    public void emailTec(string usuario, string req, string correo)
    {

        try
        {

        
        MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add(correo);
        email.IsBodyHtml = true;
        email.Subject = "Requerimiento asignado - Sistema de Atención de Tickets";
        email.Body = "Usted a sido seleccionado para atender el siguiente requerimiento: " + "<b>" + req + "</b>" +
            " del usuario " + "<b>" + usuario + "</b><br/><br/><br/><b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";

        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);

            cn.Cerrar();

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            smt.Credentials = new System.Net.NetworkCredential(id, password);
            
            smt.EnableSsl = SslTls;
            smt.Send(email);
        }
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Intentelo más tarde.";
            enviarError(x.Message.ToString(), "envioCorreo", usuario, "emailTec");
            return;
        }
    }

    public void emailTecADM(string usuario, string req)
    {
        try
        {

        
        MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add("ezuniga@deltasac.com.pe"); //ezuniga@deltasac.com.pe
        email.IsBodyHtml = true;
        email.Subject = "Requerimiento Terminado - Sistema de Atención de Tickets";
        email.Body = "El siguiente técnico " + "<b>" + usuario + "</b> ha concluido el siguiente requerimiento: " + req + ""
         + "<br/><br/><br/><b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";

        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);
            
            cn.Cerrar();

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            smt.Credentials = new System.Net.NetworkCredential(id, password);
            
            smt.EnableSsl = SslTls;
            smt.Send(email);
        }
        }
        catch (Exception x)
        {
            enviarError(x.Message.ToString(), "envioCorreo", usuario, "emailTecADM");
            return;
        }
    }
    public void enviarClave(string correo, string usuario)
    {
        try
        {

        
        string id = "";
        cn.Conectar();
        SqlCommand sc1 = new SqlCommand("select id from usuario where username = @user", cn.getConexion());
        sc1.Parameters.AddWithValue("@user", usuario);
        SqlDataReader dr1 = sc1.ExecuteReader();
        dr1.Read();
         id = dr1["id"].ToString();
        cn.Cerrar();

        string user = Encryptdata(id);

        MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add(correo);
        email.IsBodyHtml = true;
        email.Subject = "Solicitud de Clave Nueva - Sistema de Atención de Tickets";
        email.Body = "Usted a realizado una solicitud de cambio de clave. Haga clic " +
            " <a href='192.168.100.98:81/NuevaClave.aspx?id=" + user + "'>AQUI</a> para realizar el cambio de clave </b><br/>En caso no haber solicitado un cambio de clave, comuniquese con <b>Soporte Técnico</b>." +
            " <br/><br/><br/><b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src= 'https://imageshack.com/a/img922/8869/hFXiyq.png' width='176' height='90' border='0'>";

        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);
            cn.Cerrar();

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            smt.UseDefaultCredentials = false;
            smt.Credentials = new System.Net.NetworkCredential(id, password);

            smt.EnableSsl = SslTls;
            smt.Send(email);
        }
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Intentelo más tarde.";
            enviarError(x.Message.ToString(), "envioCorreo", usuario, "enviarClave");
            return;
            
        }

    }
    public void enviarClaveNueva(string correo, string clave)
    {
        try
        {
        
        MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add(correo);
        email.IsBodyHtml = true;
        email.Subject = "Clave Nueva - Sistema de Atención de Tickets";
        email.Body = "Usted a cambiado exitosamente su clave. Su nueva clave es: " + clave + "" +
            "<br/><br/><br/> <b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";


        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);
            cn.Cerrar();

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            smt.Credentials = new System.Net.NetworkCredential(id, password);

            smt.EnableSsl = SslTls;
            smt.Send(email);
        }
        }
        catch (Exception x)
        {
            enviarError(x.Message.ToString(), "envioCorreo", "clavenueva", "enviaClavenueva");
            return;
        }

    }

    public void enviarTecUsu(string correo, string requerimiento)
    {
        try
        {

        
        MailMessage email = new MailMessage();
        email.From = new MailAddress("ticket.atencion1@gmail.com");
        email.To.Add(correo);
        email.IsBodyHtml = true;
        email.Subject = "Requerimiento finalizado - Sistema de Atención de Tickets";
        email.Body = "Se ha concluido satisfactoriamente el requerimiento que solicitó: " + "<b>'" + requerimiento + "'</b>. <br/><b> Atentamente <br/> Soporte Técnico.</b>" +
            "<br/><br/><img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";


        cn.Conectar();
        SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

        SqlDataReader dr = sc.ExecuteReader();
        dr.Read();
        if (dr.HasRows)
        {
            smtp = dr.GetValue(0).ToString();
            host = (int)dr.GetValue(1);
            id = dr.GetValue(2).ToString();
            password = dr.GetValue(3).ToString();
            SslTls = (bool)dr.GetValue(4);
            cn.Cerrar();

            SmtpClient smt = new SmtpClient();
            smt.Host = smtp;
            smt.Port = host;
            smt.Credentials = new System.Net.NetworkCredential(id, password);

            smt.EnableSsl = SslTls;
            smt.Send(email);
        }
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Intentelo más tarde.";
            enviarError(x.Message.ToString(), "envioCorreo", "tecnico", "enviarTecUsu");
            return;
        }

    }

    private string Encryptdata(string id)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[id.Length];
        encode = Encoding.UTF8.GetBytes(id);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }

    public void enviarError(string error, string pagina, string usuario, string accion)
    {
        try
        {
            //cn.Conectar();
            //SqlCommand sc1 = new SqlCommand("select responsable from areas where descArea = 'Soporte Tecnico'", cn.getConexion());
            //SqlDataReader dr1 = sc1.ExecuteReader();
            //dr1.Read();
            //string responsables = "";
            //responsables = dr1["responsable"].ToString();
            //cn.Cerrar();

            MailMessage email = new MailMessage();
            email.From = new MailAddress("ticket.atencion1@gmail.com");
            email.To.Add("msalcedo@deltasac.com");
            //email.CC.Add(responsables);
            email.IsBodyHtml = true;
            email.Subject = "Error detectado - Sistema de Atención de Tickets";
            email.Body = "Se ha detectado un error en la página:  " + pagina + " por el usuario: " + usuario + ". <br/>El error es el siguiente: " + error + ". Fue detectado al ejecutar: " + accion + " <br/><b> Atentamente <br/> Soporte Técnico.</b>" +
                "<br/><br/><img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";


            cn.Conectar();
            SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

            SqlDataReader dr = sc.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                smtp = dr.GetValue(0).ToString();
                host = (int)dr.GetValue(1);
                id = dr.GetValue(2).ToString();
                password = dr.GetValue(3).ToString();
                SslTls = (bool)dr.GetValue(4);
                cn.Cerrar();

                SmtpClient smt = new SmtpClient();
                smt.Host = smtp;
                smt.Port = host;
                smt.Credentials = new System.Net.NetworkCredential(id, password);

                smt.EnableSsl = SslTls;
                smt.Send(email);
            }
        }
        catch (Exception)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Intentelo más tarde.";
            return;
        }

    }

    public void emailAreaRRHH(string usuario, string req)
    {
        try
        {
            cn.Conectar();
            SqlCommand sc1 = new SqlCommand("select responsable from areas where idArea = 2", cn.getConexion());
            SqlDataReader dr1 = sc1.ExecuteReader();
            dr1.Read();
            string responsables = "";
            responsables = dr1["responsable"].ToString();
            cn.Cerrar();
            MailMessage email = new MailMessage();
            email.From = new MailAddress("ticket.atencion1@gmail.com");
            email.To.Add(responsables); //ezuniga@deltasac.com.pe                
            email.IsBodyHtml = true;
            email.Subject = "Nuevo Requerimiento Registrado - Sistema de Atención de Tickets";
            email.Body = "<b>Se ha registrado un nuevo requerimiento.</b><br/> El siguiente usuario " + usuario +
                " ha registrado un nuevo requerimiento: " + req + "<br/><br/><br/><b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";


            cn.Conectar();
            SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

            SqlDataReader dr = sc.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                smtp = dr.GetValue(0).ToString();
                host = (int)dr.GetValue(1);
                id = dr.GetValue(2).ToString();
                password = dr.GetValue(3).ToString();
                SslTls = (bool)dr.GetValue(4);



                SmtpClient smt = new SmtpClient();
                smt.Host = smtp;
                smt.Port = host;

                smt.Credentials = new System.Net.NetworkCredential(id, password);
                smt.EnableSsl = SslTls;

                smt.Send(email);
            }
            cn.Cerrar();
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Contacte a Soporte Técnico. " + x.Message.ToString();
            Console.WriteLine(x.Message.ToString());

            enviarError(x.Message.ToString(), "envioCorreo", usuario, "emailADM");
            return;
        }
    }

    public void emailAreaAdm(string usuario, string req)
    {
        try
        {
            cn.Conectar();
            SqlCommand sc1 = new SqlCommand("select responsable from areas where idArea = 3", cn.getConexion());
            SqlDataReader dr1 = sc1.ExecuteReader();
            dr1.Read();
            string responsables = "";
            responsables = dr1["responsable"].ToString();
            cn.Cerrar();
            MailMessage email = new MailMessage();
            email.From = new MailAddress("ticket.atencion1@gmail.com");
            email.To.Add(responsables); //ezuniga@deltasac.com.pe                
            email.IsBodyHtml = true;
            email.Subject = "Nuevo Requerimiento Registrado - Sistema de Atención de Tickets";
            email.Body = "<b>Se ha registrado un nuevo requerimiento.</b><br/> El siguiente usuario " + usuario +
                " ha registrado un nuevo requerimiento: " + req + "<br/><br/><br/><b>Atentamente<br/>Sistema de Atención de Tickets</b><br/><br/> <img src = 'https://imageshack.com/a/img922/8869/hFXiyq.png' width = '176' height = '90' border = '0' >";


            cn.Conectar();
            SqlCommand sc = new SqlCommand("select smtp, host, id, pwd, SslTls from configSMTP", cn.getConexion());

            SqlDataReader dr = sc.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                smtp = dr.GetValue(0).ToString();
                host = (int)dr.GetValue(1);
                id = dr.GetValue(2).ToString();
                password = dr.GetValue(3).ToString();
                SslTls = (bool)dr.GetValue(4);



                SmtpClient smt = new SmtpClient();
                smt.Host = smtp;
                smt.Port = host;

                smt.Credentials = new System.Net.NetworkCredential(id, password);
                smt.EnableSsl = SslTls;

                smt.Send(email);
            }
            cn.Cerrar();
        }
        catch (Exception x)
        {
            HttpContext.Current.Session["error"] = "Ha ocurrido un error al enviar el correo. Contacte a Soporte Técnico. " + x.Message.ToString();
            Console.WriteLine(x.Message.ToString());

            enviarError(x.Message.ToString(), "envioCorreo", usuario, "emailADM");
            return;
        }
    }
}