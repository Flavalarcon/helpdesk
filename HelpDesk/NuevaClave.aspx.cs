using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using HelpDesk.cs;
using System.IO;
using System.Timers;
using System.Threading;

namespace HelpDesk
{
    public partial class NuevaClave : System.Web.UI.Page
    { 
        private string correo;
        private Conexion cn;
        private int idP;
        private string user;
        private string fechaSolicitud;
        private string estadoSolicitud;
        private string idSolicitud;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cn = new Conexion();
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
                {
                    user = Request.QueryString["id"].ToString();

                    cn.Conectar();
                    SqlCommand q = new SqlCommand("select top 1 fechaSolicitud, estadoSolicitud, idSolicitud from solicitudClave where fechaSolicitud >= @fecha and idUsuario = @id order by idSolicitud desc ", cn.getConexion());
                    q.Parameters.AddWithValue("@id", Decryptdata(user));
                    q.Parameters.AddWithValue("@fecha", DateTime.Today);
                    SqlDataReader dr = q.ExecuteReader();
                    dr.Read();

                    fechaSolicitud = dr["fechaSolicitud"].ToString();
                    estadoSolicitud = dr["estadoSolicitud"].ToString();
                    idSolicitud = dr["idSolicitud"].ToString();
                    cn.Cerrar();

                    if (DateTime.Parse(fechaSolicitud) != DateTime.Today)
                    {
                        Session["error"] = "La solicitud de cambio de clave a vencido. Vuevla a solicitar un cambio de clave";
                        Response.Redirect("login.aspx");
                    }
                    else if(estadoSolicitud.Equals(1)){
                        Session["error"] = "Usted ya a realizado su cambio de clave. Vuevla a solicitar un cambio de clave";
                        Response.Redirect("login.aspx");
                    }
                }
                else
                {
                    Session["error"] = "Usted no a solicitado un cambio de clave.";
                    Response.Redirect("login.aspx");
                }

                //if (!string.IsNullOrEmpty(lblNota.Text))
                //{
                //    Thread.Sleep(5000);
                //    Response.Redirect("login.aspx");
                //}
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "PAGELOAD");
                }
                return;
            }
            
            //if (string.IsNullOrEmpty(HelpDesk.login.sesion))
            //{
            //    Response.Redirect("login.aspx");
            //}
            //else
            //{
            //    lblUsuario.Text = "Bienvenido " + Session["username"].ToString();
            //}

            //try
            //{
            //    string usuario = (string)(Session["username"]);
            //    SqlCommand sc = new SqlCommand("select id, idPerfil from usuario where username=@login", cn.getConexion());
            //    sc.Parameters.AddWithValue("@login", usuario);
            //    cn.Conectar();
            //    SqlDataReader dr = sc.ExecuteReader();

            //    dr.Read();

            //    idP = Convert.ToInt32(dr["idPerfil"].ToString());
            //    cn.Cerrar();
            //}
            //catch (Exception x)
            //{
            //    x.Message.ToString();
            //}

            //if (idP != 1 && idP != 2)
            //{
            //    regUsuarios.Visible = false;
            //    ListaUsuarios.Visible = false;
            //}
            //if (idP == 2)
            //{
            //    regRequerimiento.Visible = false;
            //}

        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtClaveN.Text == txtClaveN2.Text)
                {

                    //String Login = Session["username"].ToString();
                    string id = Decryptdata(user);
                    
                    String claveNueva = sha2(txtClaveN.Text);
                    string usuario = "";
                    cn.Conectar();
                    SqlCommand q = new SqlCommand("select username, email, nombre from usuario where id=@log", cn.getConexion());
                    q.Parameters.AddWithValue("@log", id);
                    SqlDataReader dr = q.ExecuteReader();
                    dr.Read();
                    
                    usuario = dr["username"].ToString();
                    correo = dr["email"].ToString();
                    cn.Cerrar();
                    SqlCommand sc = new SqlCommand("update usuario set clave=@clave where username=@usu", cn.getConexion());
                    sc.Parameters.AddWithValue("@usu", usuario);
                    
                    sc.Parameters.AddWithValue("@clave", claveNueva);
                   
                        
                        cn.Conectar();
                        sc.ExecuteNonQuery();
                        cn.Cerrar();

                    SqlCommand sc2 = new SqlCommand("update solicitudClave set estadoSolicitud=1 where idSolicitud=@id", cn.getConexion());
                    sc2.Parameters.AddWithValue("@id", idSolicitud.ToString());
                    cn.Conectar();
                    sc2.ExecuteNonQuery();
                    cn.Cerrar();
                    lblNota.Text = "Usted a realizado su cambio de clave exitosamente.";
                    lblNota.Visible = true;                                                                                                                                           
                    
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Las contraseñas no coinciden.";
                }
                
                }catch(Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNCAMBIAR");
                }
                return;

            }
        }

        public string sha2(string texto)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(texto), 0, Encoding.UTF8.GetByteCount(texto));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

       

        //protected void cerrarsesion_Click(object sender, EventArgs e)
        //{
        //    Session.RemoveAll();
        //    Response.Redirect("login.aspx");
        //}

        

        private string Decryptdata(string encryptid)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptid);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}