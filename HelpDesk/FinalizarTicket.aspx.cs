using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HelpDesk.cs;
using System.Globalization;

namespace HelpDesk
{
    public partial class FinalizarTicket : System.Web.UI.Page
    {
        private Conexion cn;
        
        private string rqc;        
        private string tecnico;
        private string email;
        private string idTicket;
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
            if (Session["idTicket"]== null)
            {
                ticket.Text = Session["idTicket"].ToString();
                idTicket = Session["idTicket"].ToString();
            }
            else
            {
                Session["error"] = "El número de ticket no existe";
                Response.Redirect("ticket.aspx");
            }

                string login = (string)(Session["username"]);
                SqlCommand sc = new SqlCommand("select id, idPerfil from usuario where username=@login", cn.getConexion());
                sc.Parameters.AddWithValue("@login", login);
                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();                
                idP = Convert.ToInt32(dr["idPerfil"].ToString());
                cn.Cerrar();

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
            //if (!IsPostBack)
            //{
            //    ListarTicketTec();
            //}
        }

        //public void ListarTicketTec()
        //{
        //    string login = (string)(Session["username"]);
        //    cn.Conectar();
        //    SqlCommand sc = new SqlCommand("select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usutec, t.fecAtendido, t.tiempo, t.observaciones from ticket t inner join estado e on t.idEstado=e.idEstado" +
        //        " inner join usuario u on t.id=u.id " + "left outer join usuario u2 on t.idTecnico=u2.id"+
        //        " where u2.username= '"+ login + "' and t.idEstado=3 order by t.fecRegistro desc", cn.getConexion());

        //    SqlDataAdapter sa = new SqlDataAdapter(sc);
        //    DataTable ds = new DataTable();

        //    sa.Fill(ds);

        //    gvTecnico.DataSource = ds;

        //    gvTecnico.DataBind();
        //    cn.Cerrar();
        //}

        //protected void gvTecnico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvTecnico.PageIndex = e.NewPageIndex;
        //    ListarTicketTec();
        //}

        //protected void gvTecnico_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    string login = (string)(Session["username"]);
        //    string ticket = gvTecnico.DataKeys[e.RowIndex].Value.ToString();

        //    GridViewRow row = (GridViewRow)gvTecnico.Rows[e.RowIndex];
        //    TextBox obs = (TextBox)row.Cells[8].Controls[0];


        //    gvTecnico.EditIndex = -1;
        //    cn.Conectar();

        //    SqlCommand cmd = new SqlCommand("update ticket set idEstado=4, tiempo=getDate() - fecAtendido, observaciones=@obs where idTicket=@tck ", cn.getConexion());
        //    cmd.Parameters.AddWithValue("@tck", ticket);
        //    cmd.Parameters.AddWithValue("@obs", obs.Text);

        //    cmd.ExecuteNonQuery();
        //    cn.Cerrar();

        //    cn.Conectar();
        //    SqlCommand q = new SqlCommand("select t.requerimiento, u.email from ticket t join usuario u on t.id=u.id where idTicket= '" + ticket + "'", cn.getConexion());
        //    SqlDataReader dr = q.ExecuteReader();
        //    dr.Read();

        //}

        //protected void gvTecnico_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvTecnico.EditIndex = e.NewEditIndex;
        //    ListarTicketTec();
        //}

        //protected void gvTecnico_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvTecnico.EditIndex = -1;
        //    ListarTicketTec();
        //}

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            string cerrar = "window.close();";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
        }

        protected void btngrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;
                }
                if (idP == 1 || idP == 2)
                {

                    DateTime hoy = DateTime.Now;
                    DateTime inicio;

                    cn.Conectar();
                    SqlCommand q2 = new SqlCommand("select t.fecAtendido from ticket t inner join usuario u on t.id=u.id" +
                        " inner join usuario u2 on t.idTecnico = u2.id where RIGHT('000000' + cast(t.idTicket AS varchar(6)), 6)=@tck", cn.getConexion());
                    q2.Parameters.AddWithValue("@tck", idTicket);
                    SqlDataReader dr2 = q2.ExecuteReader();
                    dr2.Read();
                    inicio = DateTime.Parse(dr2["fecAtendido"].ToString());

                    cn.Cerrar();

                    double minutos = (hoy - inicio).TotalMinutes;
                    String minutosF = "";
                    if (minutos > 59)
                    {
                        if (minutos < 120)
                        {
                            double horas = (hoy - inicio).TotalHours;
                            int residuo = Convert.ToInt32(minutos % 60);
                            if (residuo == 1)
                            {
                                minutosF = horas.ToString("##") + " hora " + residuo + " minuto";
                            }
                            else if (residuo < 1)
                            {
                                minutosF = horas.ToString("##") + " hora";
                            }
                            else
                            {
                                minutosF = horas.ToString("##") + " hora " + residuo + " minutos";
                            }
                        }
                        else
                        {
                            double horas = (hoy - inicio).TotalHours;
                            int residuo = Convert.ToInt32(minutos % 60);
                            if (residuo == 1)
                            {
                                minutosF = horas.ToString("##") + " horas " + residuo + " minuto";
                            }
                            else if (residuo < 1)
                            {
                                minutosF = horas.ToString("##") + " horas";
                            }
                            else
                            {
                                minutosF = horas.ToString("##") + " horas " + residuo + " minutos";
                            }
                        }
                    }
                    else
                    {
                        minutosF = minutos.ToString("##") + " minutos";
                    }







                    cn.Conectar();
                    SqlCommand sc = new SqlCommand("update ticket set observaciones ='" + txtobs.Text + "', idEstado=4, tiempo = '" + minutosF +
                        "' where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = '" + idTicket + "'", cn.getConexion());
                    sc.ExecuteNonQuery();
                    cn.Cerrar();
                    cn.Conectar();
                    SqlCommand q = new SqlCommand("select u.username, u.email, u2.username as tecnico, t.requerimiento from ticket t inner join usuario u on t.id=u.id" +
                        " inner join usuario u2 on t.idTecnico = u2.id where RIGHT('000000' + cast(t.idTicket AS varchar(6)), 6)=@tck", cn.getConexion());
                    q.Parameters.AddWithValue("@tck", idTicket);
                    SqlDataReader dr = q.ExecuteReader();
                    dr.Read();
                    rqc = dr["requerimiento"].ToString();
                    email = dr["email"].ToString();
                    tecnico = dr["tecnico"].ToString();

                    //ConfigSMTP smtp = new ConfigSMTP();
                    //smtp.emailTecADM(tecnico, rqc);
                    //smtp.enviarTecUsu(email, rqc);
                    //cn.Cerrar();

                    Session["mensaje"] = "Se finalizó el ticket exitosamente.";
                    string cerrar = "window.close();";
                    Session.Remove("idTicket");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
                }
                else if (idP == 4 || idP == 5)
                {
                    cn.Conectar();

                    SqlCommand sc = new SqlCommand("update ticket set fecAtencion = GETDATE(), observaciones ='" + txtobs.Text + "', idEstado=4, tiempo = case " +
                        "when convert(varchar(8), fecAtendido, 108) <= getdate() " +
                        "then getdate() - convert(varchar(8), fecAtendido, 108)" +
                        " else convert(varchar(8), fecAtendido, 108) - getdate() end " +
                        "where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = '" + idTicket + "'", cn.getConexion());
                    sc.ExecuteNonQuery();
                    cn.Cerrar();
                    cn.Conectar();
                    SqlCommand q = new SqlCommand("select u.username, u.email, u2.username as tecnico, t.requerimiento from ticket t inner join usuario u on t.id=u.id" +
                        " inner join usuario u2 on t.idTecnico = u2.id where RIGHT('000000' + cast(t.idTicket AS varchar(6)), 6)=@tck", cn.getConexion());
                    q.Parameters.AddWithValue("@tck", idTicket);
                    SqlDataReader dr = q.ExecuteReader();
                    dr.Read();
                    rqc = dr["requerimiento"].ToString();
                    email = dr["email"].ToString();
                    tecnico = dr["tecnico"].ToString();

                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.emailTecADM(tecnico, rqc);
                    smtp.enviarTecUsu(email, rqc);
                    cn.Cerrar();

                    Session["mensaje"] = "Se finalizó el ticket exitosamente.";
                    string cerrar = "window.close();";
                    Session.Remove("idTicket");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
                }
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.UserHostAddress.ToString(), Session["username"].ToString(), "BTNGRABAR");
                }
                return;
            }

        }
    }
}