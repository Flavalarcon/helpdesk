using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using HelpDesk.cs;

namespace HelpDesk
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        private Conexion cn;
        private string pwd1;
        private string pwd2;
        private string clave;
        private string usu;
        private int idP;

        public static string mensaje = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");


                }
                else
            {
                lblUsuario.Text = "Bienvenido " + Session["username"].ToString();
                
            }
            
            this.cn = new Conexion();
            
            if (!this.IsPostBack)
            {
                try { 
                string usuario = (string)(Session["username"]);
                SqlCommand sc = new SqlCommand("select id, idPerfil from usuario where username=@login", cn.getConexion());
                sc.Parameters.AddWithValue("@login", usuario);
                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();
                
                idP = Convert.ToInt32(dr["idPerfil"].ToString());
                cn.Cerrar();
            }catch (Exception x)
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

            if(idP != 1 && idP != 2)
                {
                    Response.Redirect("login.aspx");
                    regUsuarios.Visible = false;
                    ListaUsuarios.Visible = false;
                }
            
            using (SqlCommand cmd = new SqlCommand("SELECT idPerfil, descPerfil FROM perfil"))
                {
                    cn.Cerrar();
                    cn.Conectar();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn.getConexion();
                    
                    cboPerfil.DataSource = cmd.ExecuteReader();
                    cboPerfil.DataTextField = "descPerfil";
                    cboPerfil.DataValueField = "idPerfil";
                    cboPerfil.DataBind();

                    cn.Cerrar();
                }
            }
           pwd1 = autoClave(10);
            pwd2 = sha2(pwd1);
            string login = (string)(Session["username"]);
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
        }

       

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                }
                cn.Conectar();
                SqlCommand q = new SqlCommand("select username from usuario", cn.getConexion());
                SqlDataReader dr = q.ExecuteReader();
                dr.Read();
                usu = dr["username"].ToString();
                cn.Cerrar();

                SqlCommand cmd = new SqlCommand("insert into usuario (fecRegistro, username, clave, nombre, email, idPerfil) values (getDate(), @usu, @clave, @nom, @email, @perfil)", cn.getConexion());
                cmd.Parameters.AddWithValue("@usu", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@clave", pwd2);
                cmd.Parameters.AddWithValue("@nom", txtNom.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@perfil", int.Parse(cboPerfil.SelectedValue.ToString()));

                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    cn.Conectar();

                    cmd.ExecuteNonQuery();
                    ConfigSMTP cs = new ConfigSMTP();
                    cs.enviar(txtEmail.Text, txtUsuario.Text, pwd1);
                    cn.Cerrar();
                    lblError.Visible = false;
                    lblCorrecto.Text = "Usuario registrado exitosamente.";
                    lblCorrecto.Visible = true;
                    txtEmail.Text = "";
                    txtNom.Text = "";
                    txtUsuario.Text = "";

                    
                }
                else
                {
                    lblCorrecto.Visible = false;
                    lblError.Text = "El usuario es requerido.";
                    lblError.Visible = true;
                }
                
            }catch(Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNREGISTRAR");
                }
                return;
            }


            
        }

        public string autoClave(int letra)

        {
            const string chars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ$#!";
            Random rd = new Random();
            return new string(Enumerable.Repeat(chars, letra).Select(s => s[rd.Next(s.Length)]).ToArray());
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