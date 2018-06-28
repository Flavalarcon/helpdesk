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
    public partial class ListaUsuario : System.Web.UI.Page
    {
        private Conexion cn;

        private int idP;
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
            if (!IsPostBack)
            {
                ListarUsuario();
            }
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
                if (idP == 2)
                {
                    regRequerimiento.Visible = false;
                }


            }
           
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

        private void ListarUsuario()
        {
            try
            {
                cn.Conectar();


                SqlDataAdapter sa = new SqlDataAdapter("select u.id, u.username, u.nombre, u.email, p.descPerfil, u.fecRegistro from usuario u" +
                    " inner join perfil p on u.idPerfil=p.idPerfil order by u.fecRegistro desc", cn.getConexion());
                DataTable ds = new DataTable();

                sa.Fill(ds);

                gvUsuario.DataSource = ds;

                gvUsuario.DataBind();
                cn.Cerrar();
            }catch(Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "LISTARUSUARIO");
                }
                return;
            }
        }

       

        protected void gvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsuario.EditIndex = e.NewEditIndex;
            ListarUsuario();
        }

        protected void gvUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //clave=autoClave(10);
            try
            {
                 if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }

                string usu = gvUsuario.DataKeys[e.RowIndex].Values["id"].ToString();

            GridViewRow row1 = (GridViewRow)gvUsuario.Rows[e.RowIndex];


            TextBox user = (TextBox)row1.Cells[0].Controls[0];
            TextBox nombre = (TextBox)row1.Cells[1].Controls[0];
            TextBox email = (TextBox)row1.Cells[2].Controls[0];
            string perfil = (gvUsuario.Rows[e.RowIndex].FindControl("cboPerfil") as DropDownList).SelectedItem.Value;
            gvUsuario.EditIndex = -1;

            cn.Conectar();
            SqlCommand cmd = new SqlCommand("update usuario set username = '" + user.Text + "', nombre='" + nombre.Text + "', email='" + email.Text + "'" +
                ", idPerfil='" + perfil + "' where id = '" + usu + "'", cn.getConexion());

            cmd.ExecuteNonQuery();
           
            cn.Cerrar();
            
            ListarUsuario();
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "GVUSUARIO_ROWUPDATING");
                }
                return;
            }
        }

        protected void gvUsuario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsuario.EditIndex = -1;
            ListarUsuario();
        }

        protected void gvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuario.PageIndex = e.NewPageIndex;
            ListarUsuario();
        }

        protected void gvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            
            if (e.Row.RowType == DataControlRowType.DataRow && gvUsuario.EditIndex == e.Row.RowIndex)
            {

                DropDownList ddlTec = (DropDownList)e.Row.FindControl("cboPerfil");
                cn.Cerrar();
                cn.Conectar();
                SqlCommand cmd = new SqlCommand("select idPerfil, descPerfil from perfil", cn.getConexion());
                                
                ddlTec.DataSource = cmd.ExecuteReader();
                ddlTec.DataTextField = "descPerfil";
                ddlTec.DataValueField = "idPerfil";
                ddlTec.DataBind();

                cn.Cerrar();

            }
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "GVUSUARIO_ROWDATABOUND");
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