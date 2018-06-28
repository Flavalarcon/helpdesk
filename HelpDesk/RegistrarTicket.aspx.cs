using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using HelpDesk.cs;
using System.IO;

namespace HelpDesk
{
    public partial class RegistrarTicket : System.Web.UI.Page
    {
        private Conexion cn;
        private int id;
        private int idP;
        public static string mensaje = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            txtReq.Focus();

            //OBTIENE EL USUARIO DE QUIEN ESTA EN LA SESION

            try
            {
                this.cn = new Conexion();
               if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");


                }
                else
                {
                    if (!IsPostBack)
                    {
                        if (Session["username"] != null)
                        {
                            lblUsuario.Text = "Bienvenido" + " " + Session["username"].ToString();
                            lblCorrecto.Visible = false;
                            lblError.Visible = false;
                        }
                        else
                        {
                            mensaje = "Primero debe iniciar sesión";
                            Response.Redirect("login.aspx");
                        }
                    }
                }

                //OBTENER EL ID DEL USUARIO Y SU ID PERFIL PARA VALIDACION
                string login = (string)(Session["username"]);
                SqlCommand sc = new SqlCommand("select id, idPerfil from usuario where username=@login", cn.getConexion());
                sc.Parameters.AddWithValue("@login", login);
                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();
                id = Convert.ToInt32(dr["id"].ToString());
                idP = Convert.ToInt32(dr["idPerfil"].ToString());
                cn.Cerrar();




                //MUESTRA CONTROLES POR TIPO DE USUARIO
                //PERFIL usuario
                if (idP == 3)
                {
                    if (!IsPostBack)
                    {
                        regUsuarios.Visible = false;
                        ListaUsuarios.Visible = false;
                    }

                }

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

        //BOTON REGISTRO REQUERIMIENTO
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string area = "";
                 if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                }

                if (string.IsNullOrWhiteSpace(txtReq.Text))
                {
                    lblError.Text = "El requerimiento debe contener alguna palabra.";
                    lblError.Visible = true;
                    return;
                }
                else
                {
                   
                    if (fileImg.HasFile)
                    {
                        int contador = fileImg.FileName.Length;
                        int extension = contador - 4;
                        string file = fileImg.FileName.Substring(extension, 4);
                        if (file != ".jpg" && file != ".png")
                        {
                            lblError.Text = "Solo permite imágenes (Formato .jpg .png)";
                            lblError.Visible = true;
                            return;
                        }
                    }
                    if(!chkAdm.Checked && !chkRRHH.Checked && !chkSoporte.Checked)
                    {
                        lblError.Text = "Para registrar su ticket debe seleccionar el aréa";
                        lblError.Visible = true;
                        return;
                    }
                    if (chkSoporte.Checked)
                    {
                       area = "Soporte Tecnico";
                    }
                    else if (chkRRHH.Checked)
                    {
                        area = "Recursos Humanos";
                    }
                    else if (chkAdm.Checked)
                    {
                        area = "Administrativa";
                    }
                    int estado = 1;
                    string login = (string)(Session["username"]);

                    SqlCommand cmd = new SqlCommand("insert into ticket (fecRegistro, id, requerimiento, idEstado, imagen, nombreImg, area) values (@fec, @usu, @req, @estado, @img, @nomImg, @area)", cn.getConexion());
                    cmd.Parameters.AddWithValue("@fec", DateTime.Now);
                    cmd.Parameters.AddWithValue("@usu", id);
                    cmd.Parameters.AddWithValue("@req", txtReq.Text);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@img", fileImg.FileBytes);
                    cmd.Parameters.AddWithValue("@nomImg", fileImg.FileName);
                    cmd.Parameters.AddWithValue("@area", area);
                    cn.Cerrar();
                    cn.Conectar();
                    cmd.ExecuteNonQuery();
                    
                    cn.Cerrar();

                    if (chkSoporte.Checked)
                    {
                        ConfigSMTP smtp = new ConfigSMTP();
                        smtp.emailADM(login, txtReq.Text);
                    }
                    else if (chkRRHH.Checked)
                    {
                        ConfigSMTP smtp = new ConfigSMTP();
                        smtp.emailAreaRRHH(login, txtReq.Text);
                    }
                    else if (chkAdm.Checked)
                    {
                        ConfigSMTP smtp = new ConfigSMTP();
                        smtp.emailAreaAdm(login, txtReq.Text);
                    }
                   
                    if (Session["error"] != null)
                    {
                        lblError.Text = mensaje;
                        lblError.Visible = true;
                        return;
                    }
                    cn.Cerrar();

                    txtReq.Text = "";
                    //ClientScript.RegisterStartupScript(GetType(), "sw", "alertaCustom", true);

                    lblCorrecto.Text = "Ticket registrado exitosamente. <br/>En un momento Soporte Técnico atenderá su ticket.";
                    lblCorrecto.Visible = true;
                    //string popup = "alertaCustom()";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", popup, true);                    

                }
                
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNREGISTRAR");
                }
                return;
            }
        }                               

        protected void cerrarsesion_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }

        
    }
}