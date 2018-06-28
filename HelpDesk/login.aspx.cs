using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HelpDesk.cs;

namespace HelpDesk
{
    public partial class login : System.Web.UI.Page
    {
        private Conexion cn;
        private string pwd;
        public static string sesion;
        private string idPerfil;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cn = new Conexion();
            lblError.Visible = false;
            try
            {
            
            //if(RegistrarTicket.mensaje.Length > 0)
            //{
            //    lblError.Text = Session["error"].ToString();
            //    lblError.Visible = true;
            //}
            //else if (Session["error"] != null)
            {
                lblError.Text = Session["error"].ToString();
                lblError.Visible = true;
               
            }
            }
            catch (Exception)
            {

             return;
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    lblError.Text = "Ingrese su usuario.";
                    lblError.Visible = true;
                    return;
                }
                else if (string.IsNullOrEmpty(txtClave.Text))
                {
                    lblError.Text = "Ingrese su clave.";
                    lblError.Visible = true;
                    return;
                }
               // ClientScript.RegisterStartupScript(this.GetType(), "btnBlockPage", "<script>javascript:btnBlockPage();</script>");

                pwd = sha2(txtClave.Text);


                cn.Conectar();
                SqlCommand sc = new SqlCommand("select username, clave, idPerfil from usuario where username=@login and clave=@clave", cn.getConexion());
                sc.Parameters.AddWithValue("@login", txtUsuario.Text);
                sc.Parameters.AddWithValue("@clave", pwd);
                SqlDataAdapter sa = new SqlDataAdapter(sc);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                int i = sc.ExecuteNonQuery();

                if (dt.Rows.Count > 0)
                {
                    //sesion = Session["username"].ToString();
                    Session["username"] = txtUsuario.Text;
                    SqlDataReader dr = sc.ExecuteReader();
                    dr.Read();
                    sesion = dr["username"].ToString();
                    idPerfil = dr["idPerfil"].ToString();
                    cn.Cerrar();
                    if (int.Parse(idPerfil) == 1 || int.Parse(idPerfil) == 2 || int.Parse(idPerfil) == 4 || int.Parse(idPerfil) == 5)
                    {
                        Session.Remove("error");
                        Session.Remove("mensaje");
                        Response.Redirect("ticket.aspx");
                        return;
                    }
                    else
                    {
                        Session.Remove("error");
                        Response.Redirect("RegistrarTicket.aspx");
                        return;
                    }

                }
                else
                {
                    lblError.Text = "Usuario y/o clave son incorrectos.";
                    lblError.Visible = true;
                    return;

                }
                
            } catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico. " + x.Message.ToString();
                lblError.Visible = true;
                //ConfigSMTP smtp = new ConfigSMTP();
                //smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNINGRESAR");

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
    }
}