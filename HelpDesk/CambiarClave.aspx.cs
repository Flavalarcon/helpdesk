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

namespace HelpDesk
{
    public partial class CambiarClave : System.Web.UI.Page
    { private string claveANT;
        private string nombre;
        private string correo;
        private Conexion cn;
        private int idP;
        private string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            
            this.cn = new Conexion();

               if (string.IsNullOrEmpty(Session["username"].ToString()))
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");


                }
                else
            {
                lblUsuario.Text = "Bienvenido " + Session["username"].ToString();
            }

            try
            {
                string usuario = (string)(Session["username"]);
                SqlCommand sc = new SqlCommand("select id, idPerfil from usuario where username=@login", cn.getConexion());
                sc.Parameters.AddWithValue("@login", usuario);
                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();

                idP = Convert.ToInt32(dr["idPerfil"].ToString());
                cn.Cerrar();
            }
            catch (Exception x)
            {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "PAGELOAD");

                }

                if (idP != 1 && idP != 2)
            {
                regUsuarios.Visible = false;
                ListaUsuarios.Visible = false;
            }
            
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                ConfigSMTP smtp = new ConfigSMTP();
                smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "PAGELOAD");
                return;
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["username"].ToString()))
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                if (txtClaveN.Text == txtClaveN2.Text)
                {

                    String Login = Session["username"].ToString();
                    
                    string claveA = sha2(txtClaveA.Text);
                    String claveNueva = sha2(txtClaveN.Text);
                    
                    cn.Conectar();
                    SqlCommand q = new SqlCommand("select email, nombre, clave from usuario where id=@log", cn.getConexion());
                    q.Parameters.AddWithValue("@log", Login);
                    SqlDataReader dr = q.ExecuteReader();
                    dr.Read();
                    claveANT = dr["clave"].ToString();
                    
                    correo = dr["email"].ToString();
                    cn.Cerrar();
                    SqlCommand sc = new SqlCommand("update usuario set clave=@clave where username=@usu and clave=@claveA", cn.getConexion());
                    sc.Parameters.AddWithValue("@usu", Login);
                    sc.Parameters.AddWithValue("@claveA", claveA);
                    sc.Parameters.AddWithValue("@clave", claveNueva);
                    if (claveA == claveANT)
                    {
                        cn.Conectar();
                        sc.ExecuteNonQuery();
                        //ConfigSMTP smtp = new ConfigSMTP();
                        //smtp.enviarClaveNueva(correo, txtClaveN2.Text);
                        cn.Cerrar();
                        lblCorrecto.Text = "Se ha modificado correctamente";
                        lblCorrecto.Visible = true;

                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "La Clave anterior no es correcta";
                        return;
                    }

                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Las contraseñas no coinciden.";
                    return;
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

       

        protected void cerrarsesion_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }

        

        


    }
}