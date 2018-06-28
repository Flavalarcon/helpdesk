using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using HelpDesk.cs;

namespace HelpDesk
{
    
    public partial class AsignarTecnico : System.Web.UI.Page
    {
        private Conexion cn;
        private int id;
        private int idP;
        private string email;
        private string usu;
        private string req;
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            
            this.cn = new Conexion();

                //if (!IsPostBack)
                //{
                //   ListarTicketADM();
                //}
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }

                string login = (string)(Session["username"]);
            SqlCommand sc = new SqlCommand("select id, idPerfil from usuario where username=@login", cn.getConexion());
            sc.Parameters.AddWithValue("@login", login);
            cn.Conectar();
            SqlDataReader dr = sc.ExecuteReader();

            dr.Read();
            id = Convert.ToInt32(dr["id"].ToString());
            idP = Convert.ToInt32(dr["idPerfil"].ToString());
            cn.Cerrar();

     
            if (!this.IsPostBack)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT id, username FROM usuario where idPerfil in (1,2)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn.getConexion();
                    cn.Conectar();
                    cboTecnico.DataSource = cmd.ExecuteReader();
                    cboTecnico.DataTextField = "username";
                    cboTecnico.DataValueField = "id";
                    cboTecnico.DataBind();

                    cn.Cerrar();
                }
                lblticket.Text = Session["idTicket"].ToString();
            }
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado.";
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
            try
            {
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;
                }
                cn.Conectar();
                SqlCommand sc = new SqlCommand("update ticket set idTecnico= '" + cboTecnico.SelectedValue.ToString() + "', idEstado=2 where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'", cn.getConexion());
                sc.ExecuteNonQuery();
                cn.Cerrar();
                cn.Conectar();
                SqlCommand q = new SqlCommand("select u.username, u2.email, t.requerimiento from ticket t inner join usuario u on t.id=u.id" +
                    " inner join usuario u2 on t.idTecnico = u2.id where RIGHT('000000' + cast(t.idTicket AS varchar(6)), 6)=@tck", cn.getConexion());
                q.Parameters.AddWithValue("@tck", Session["idTicket"].ToString());
                SqlDataReader dr = q.ExecuteReader();
                dr.Read();
                email = dr["email"].ToString();
                usu = dr["username"].ToString();
                req = dr["requerimiento"].ToString();

                //ConfigSMTP smtp = new ConfigSMTP();
                //smtp.emailTec(usu, req, email);
                //cn.Cerrar();

                //if (!string.IsNullOrEmpty(Session["error"].ToString()))
                //{
                //    lblError.Text = Session["error"].ToString();
                //    lblError.Visible = true;
                //    return;
                //}
                Session["mensaje"] = "Se asignó correctamente el técnico.";
                string cerrar = "window.close();";
                Session.Remove("idTicket");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
            }catch(Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNGRABAR");
                }
                return;
            }
        }
    }
}