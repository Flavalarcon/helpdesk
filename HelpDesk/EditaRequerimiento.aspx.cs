using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HelpDesk.cs;

namespace HelpDesk
{
    public partial class EditaRequerimiento : System.Web.UI.Page
    {

        private Conexion cn;
        
        private int idP;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                string login = Session["username"].ToString();
                SqlCommand sc = new SqlCommand("select idPerfil from usuario where username=@login", cn.getConexion());
                sc.Parameters.AddWithValue("@login", login);
                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();

                idP = Convert.ToInt32(dr["idPerfil"].ToString());
                cn.Cerrar();
            }
            
            
            if (!IsPostBack)
            {
                    if (Session["requerimiento"] != null)
                    {
                        txtreq.Text = Session["requerimiento"].ToString();
                    }
                    if (Session["observaciones"] != null)
                    {
                        txtObs.Text = Session["observaciones"].ToString();
                    }
                    if (Session["idTicket"] != null)
                    {
                        lblticket.Text = Session["idTicket"].ToString();
                    }
                    req.Visible = false;
                txtreq.Visible = false;
                lblImg.Visible = false;
                fileImg.Visible = false;
                txtObs.Visible = false;
                lblContador2.Visible = false;
                txtContador2.Visible = false;
                Obs.Visible = false;
                btnGrabar.Visible = false;
                lblCorrecto.Visible = false;
                lblContador.Visible = false;
                txtContador.Visible = false;

            }

                
            if (idP.Equals(3))
            {
                req.Visible = true;
                txtContador.Visible = true;
                lblContador.Visible = true;
                txtreq.Visible = true;
                lblImg.Visible = true;
                fileImg.Visible = true;
                btnGrabar.Visible = true;
                btnEditObs.Visible = false;
                btnEditRq.Visible = false;
                btnFinalizar.Visible = false;
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



       
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            string cerrar = "window.close();";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            string query = "";
            try
            {
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                if (Session["idTicket"] != null)
                {
                    if (txtreq.Visible.Equals(true))
                    {

                        if (fileImg.HasFile)
                        {
                            query = "update ticket set requerimiento = '" + txtreq.Text + "', imagen = @img, nombreImg = @nomImg where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                            cn.Conectar();
                            SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                            cmd.Parameters.AddWithValue("@img", fileImg.FileBytes);
                            cmd.Parameters.AddWithValue("@nomImg", fileImg.FileName);

                            cmd.ExecuteNonQuery();
                            cn.Cerrar();
                            if (idP.Equals(3))
                            {
                                string cerrar = "window.close();";
                                Session.Remove("idTicket");
                                Session.Remove("requerimiento");
                                Session.Remove("observaciones");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
                                Session["mensaje"] = "Se ha modificado correctamente.";
                                return;
                            }

                            lblCorrecto.Text = "Se ha modificado correctamente.";
                            lblCorrecto.Visible = true;

                        }
                        else
                        {
                            query = "update ticket set requerimiento = '" + txtreq.Text + "' where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                            cn.Conectar();
                            SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                            cmd.ExecuteNonQuery();
                            cn.Cerrar();
                            if (idP.Equals(3))
                            {
                                string cerrar = "window.close();";
                                Session.Remove("idTicket");
                                Session.Remove("requerimiento");
                                Session.Remove("observaciones");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
                                Session["mensaje"] = "Se ha modificado correctamente.";
                                return;
                            }
                            lblCorrecto.Text = "Se ha modificado correctamente.";
                            lblCorrecto.Visible = true;
                        }
                    }
                    else
                    {
                        query = "update ticket set observaciones = '" + txtObs.Text + "' where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                        cn.Conectar();
                        SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                        cmd.ExecuteNonQuery();
                        cn.Cerrar();
                        lblCorrecto.Text = "Se ha modificado correctamente.";
                        lblCorrecto.Visible = true;

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
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNGRABAR");
                }
                return;
            }


        }

        protected void btnEditRq_Click(object sender, EventArgs e)
        {
            txtObs.Visible = false;
            lblContador2.Visible = false;
            txtContador2.Visible = false;
            Obs.Visible = false;
            req.Visible = true;
            txtreq.Visible = true;
            lblImg.Visible = true;
            fileImg.Visible = true;
            btnGrabar.Visible = true;
        }

        protected void btnEditObs_Click(object sender, EventArgs e)
        {
            req.Visible = false;
            txtreq.Visible = false;
            lblImg.Visible = false;
            fileImg.Visible = false;
            lblContador.Visible = false;
            txtContador.Visible = false;
            txtObs.Visible = true;
            lblContador2.Visible = true;
            txtContador2.Visible = true;
            Obs.Visible = true;
            btnGrabar.Visible = true;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Session.Remove("idTicket");
            string cerrar = "window.close();";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
            
        }
    }
}