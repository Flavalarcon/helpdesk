using HelpDesk.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Data;

namespace HelpDesk
{
    public partial class SolicitudClave : System.Web.UI.Page
    {
        
        Conexion cn;
        private string clave;
        private string claveN;
        private string email;
        private string usu;
        private string idUsuario;
        public static string mensaje = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            cn = new Conexion();
            clave = autoClave(10);
            claveN = sha2(clave);
            lblAviso.Visible = false;
            lblError.Visible = false;
            lblErrorVS.Visible = false;
            

        }
        
        protected void btnNuevaClave_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtUsu.Text))
                {
                    SqlCommand sc1 = new SqlCommand("select username from usuario where username=@usu", cn.getConexion());
                    sc1.Parameters.AddWithValue("@usu", txtUsu.Text);
                    cn.Conectar();
                    SqlDataAdapter sa1 = new SqlDataAdapter(sc1);
                    DataTable dt = new DataTable();
                    sa1.Fill(dt);

                    cn.Cerrar();

                    if (dt.Rows.Count > 0)
                    {
                        SqlCommand sc = new SqlCommand("select email, username, id from usuario where username=@usu", cn.getConexion());
                        sc.Parameters.AddWithValue("@usu", txtUsu.Text);
                        cn.Conectar();
                        SqlDataReader sa = sc.ExecuteReader();
                        sa.Read();
                        email = sa["email"].ToString();
                        usu = sa["username"].ToString();
                        idUsuario = sa["id"].ToString();
                        cn.Cerrar();
                        ConfigSMTP smtp = new ConfigSMTP();
                        smtp.enviarClave(email, usu);
                        if (!string.IsNullOrEmpty(mensaje))
                        {
                            lblError.Text = mensaje;
                            lblError.Visible = true;
                            return;
                        }

                        SqlCommand sc2 = new SqlCommand("insert into solicitudClave values (@usuario, @fecha, 0)", cn.getConexion());
                        sc2.Parameters.AddWithValue("@usuario", idUsuario.ToString());
                        sc2.Parameters.AddWithValue("@fecha", DateTime.Today);
                        cn.Conectar();

                        sc2.ExecuteNonQuery();
                        cn.Cerrar();

                        lblAviso.Text = "Su solicitud fue registrada. Verifique su correo.";
                        lblAviso.Visible = true;

                    }
                    else
                    {
                        lblError.Text = "El usuario es erróneo o no está registrado";
                        lblError.Visible = true;
                        return;
                    }
                }
                else
                {
                    lblError.Text = "Debe ingresar un usuario.";
                    lblError.Visible = true;
                    return;
                }
            }
            catch(Exception x)
            {

                lblError.Text = "Ha ocurrido un error inesperado.";
                lblError.Visible = true;
                ConfigSMTP smtp = new ConfigSMTP();
                smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNNUEVACLAVE");

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}