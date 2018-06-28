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
    public partial class ticket : System.Web.UI.Page
    {
        private Conexion cn;
        private int id;
        private int idP;
        



        protected void Page_Load(object sender, EventArgs e)
        {                  

            try
            {
                TimeoutException to = new TimeoutException();
                
                this.cn = new Conexion();
               
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero deberá iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                else if (Session["error"] != null)
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
                        }
                        else
                        {
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

                btnEjecucion.Visible = false;
                btnRealizado.Visible = false;

                if(Session["mensaje"] != null)
                {
                    lblCorrecto.Text = Session["mensaje"].ToString();
                    lblCorrecto.Visible = true;
                }

                //if (!IsPostBack)
                //{
                //    EditaRequerimiento.id_ticket = "";
                //    EditaRequerimiento.requerimiento = "";
                //    asignarTec.id_ticket2 = "";
                //    ticket.idCheck = "";
                //    ticketFin.id_ticket2 = "";
                //}
                
                //MUESTRA CONTROLES POR TIPO DE USUARIO
                //PERFIL ADMIN
                if (idP == 1)
                {
                    if (!IsPostBack)
                    {


                        Titulo.Visible = false;


                        cboHistorial_Data();
                        gvTicket.Visible = true;
                        ListarTicket();
                    }
                    btnAsignar.Visible = true;
                    btnEjecucion.Visible = true;
                    btnRealizado.Visible = true;
                }

                //PERFIL TECNICO
                if (idP == 2)
                {
                    if (!IsPostBack)
                    {
                        ListarTicket();
                        Titulo.Visible = false;

                    }
                    btnEjecucion.Visible = true;
                    btnRealizado.Visible = true;
                    btnAsignar.Visible = false;

                    regRequerimiento.Visible = true;

                    lblEstado.Visible = false;
                    txtFec.Visible = false;
                    cboEstado.Visible = false;
                    btnFiltro.Visible = false;
                    lblFec.Visible = false;
                    //hpregistroUsuarios.Visible = false;
                    
                    
                    TxtFec2.Visible = false;
                    lblfec2.Visible = false;

                    cboHistorial.Visible = false;
                    //btnHistorial.Visible = false;
                    btnCierre.Visible = false;
                    
                    lblHistorial.Visible = false;
                }

                //PERFIL USUARIO
                if (idP == 3)
                {
                    if (!IsPostBack)
                    {
                        ListarTicket();
                        cboHistorial.SelectedIndex = 1;
                    }
                    regUsuarios.Visible = false;
                    ListaUsuarios.Visible = false;
                    Titulo.Text = "Mis Requerimientos";
                    Titulo.Visible = true;
                    DropDownListTitulo.Visible = false;
                    lblEstado.Visible = false;
                    txtFec.Visible = false;
                    cboEstado.Visible = false;
                    btnFiltro.Visible = false;
                    lblFec.Visible = false;
                    //hpregistroUsuarios.Visible = false;                
                    TxtFec2.Visible = false;
                    lblfec2.Visible = false;
                    btnAsignar.Visible = false;
                    cboHistorial.Visible = false;
                    lblHistorial.Visible = false;
                    btnCierre.Visible = false;
           
                }

                if (idP == 4)
                {
                    if (!IsPostBack)
                    {
                        ListarTicket();
                    }
                    regUsuarios.Visible = false;
                    ListaUsuarios.Visible = false;
                    Titulo.Visible = false;
                    btnRealizado.Visible = true;
                    lblEstado.Visible = true;
                    txtFec.Visible = true;
                    cboEstado.Visible = true;
                    btnFiltro.Visible = true;
                    lblFec.Visible = true;
                    //hpregistroUsuarios.Visible = false;                
                    TxtFec2.Visible = true;
                    lblfec2.Visible = true;
                    btnAsignar.Visible = false;
                    cboHistorial.Visible = false;
                    lblHistorial.Visible = false;
                    btnCierre.Visible = false;
                }

                if (idP == 5)
                {
                    if (!IsPostBack)
                    {
                        ListarTicket();
                    }
                    regUsuarios.Visible = false;
                    ListaUsuarios.Visible = false;
                    Titulo.Visible = false;
                    btnRealizado.Visible = true;
                    lblEstado.Visible = true;
                    txtFec.Visible = false;
                    cboEstado.Visible = true;
                    btnFiltro.Visible = false;
                    lblFec.Visible = false;
                    //hpregistroUsuarios.Visible = false;                
                    TxtFec2.Visible = false;
                    lblfec2.Visible = false;
                    btnAsignar.Visible = false;
                    cboHistorial.Visible = false;
                    lblHistorial.Visible = false;
                    btnCierre.Visible = false;
                }
            }
            catch(Exception x)
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

        

        //LISTAR TICKET POR AÑOS PASADOS
        public void ListarTicketCbo()
        {
            try { 
            cn.Conectar();
            string query1 = "";
            if (int.Parse(cboHistorial.SelectedValue.ToString()) == 1)
            {
                query1 = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido, t.tiempo, t.observaciones, t.nombreImg" +
                   " from ticket t inner join estado e on t.idEstado=e.idEstado" +
                   " inner join usuario u on t.id=u.id left outer join usuario u2 on t.idTecnico=u2.id order by t.fecRegistro desc";
            }
            else
            {
                query1 = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido,t.tiempo, t.observaciones, t.nombreImg" +
                    " from ticket" + cboHistorial.SelectedValue.ToString() + " t inner join estado e on t.idEstado=e.idEstado" +
                    " inner join usuario u on t.id=u.id left outer join usuario u2 on t.idTecnico=u2.id order by t.fecRegistro desc";
            }
            SqlCommand sc = new SqlCommand(query1, cn.getConexion());

            SqlDataAdapter sa = new SqlDataAdapter(sc);
            DataTable ds = new DataTable();

            sa.Fill(ds);

            gvTicket.DataSource = ds;

            gvTicket.DataBind();
            cn.Cerrar();
            if (0 < gvTicket.Rows.Count)
            {
                if (idP == 3)
                {
                    gvTicket.Rows[0].Cells[5].Visible = false;
                    gvTicket.Rows[0].Cells[6].Visible = false;
                    gvTicket.Rows[0].Cells[7].Visible = false;
                    gvTicket.Rows[0].Cells[8].Visible = false;
                }
                gvTicket.Visible = true;
            }
            else
            {
                ds.Rows.Add(ds.NewRow());
                gvTicket.DataSource = ds;
                gvTicket.DataBind();

                int totalColumns = gvTicket.Rows[0].Cells.Count;
                gvTicket.Rows[0].Cells.Clear();
                gvTicket.Rows[0].Cells.Add(new TableCell());
                gvTicket.Rows[0].Cells[0].ColumnSpan = totalColumns;
                gvTicket.Rows[0].Cells[0].Text = "No tiene tickets registrados";
                gvTicket.Rows[0].Cells[0].Width = 1150;
                gvTicket.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
        smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "LISTARTICKETCBO");
                }
                return;
            }
        }

        //LISTADO DE TICKET DEL USUARIO
        public void ListarTicket()
        {
            try
            {

                string login = (string)(Session["username"]);
                cn.Conectar();
                string query = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, " +
                    "u2.username as usertec, t.fecAtendido, t.tiempo, " +
                    "t.observaciones, t.nombreImg, t.area" +
                    " from ticket t inner join estado e on t.idEstado=e.idEstado" +
                    " inner join usuario u on t.id=u.id left outer join usuario u2 on t.idTecnico=u2.id ";

                if (idP == 3)
                {
                    query = query + "where t.idEstado IN (1, 2, 3, 4) and u.id = " + id + " order by t.fecRegistro desc";
                }
                else if (idP == 2)
                {
                    query = query + "where t.area = 'Soporte Tecnico' and t.idEstado in (2, 3) and u2.id = " + id + " order by t.fecRegistro desc, t.idEstado desc";
                }
                else if (idP == 1)
                {
                    query = query + "where t.area = 'Soporte Tecnico' order by t.fecRegistro desc, t.idEstado desc";
                }
                else if (idP == 4)
                {
                    query = query + "where t.area = 'Recursos Humanos' order by t.fecRegistro desc, t.idEstado desc";
                }
                else if (idP == 5)
                {
                    query = query + "where t.area = 'Administrativa' order by t.fecRegistro desc, t.idEstado desc";
                }
                SqlCommand sc = new SqlCommand(query, cn.getConexion());
                SqlDataAdapter sa = new SqlDataAdapter(sc);
                DataTable ds = new DataTable();

                sa.Fill(ds);

                gvTicket.DataSource = ds;

                gvTicket.DataBind();
                cn.Cerrar();
                if (0 < gvTicket.Rows.Count)
                {
                    if (idP == 3)
                    {
                        gvTicket.Columns[2].Visible = false;
                        gvTicket.Columns[6].Visible = false;
                        gvTicket.Columns[7].Visible = false;
                        gvTicket.Columns[8].Visible = false;

                    }
                    else if (idP == 1 || idP == 2)
                    {
                        gvTicket.Columns[10].Visible = false;
                    }
                    else if (idP == 4 || idP == 5)
                    {
                        gvTicket.Columns[6].Visible = false;
                        gvTicket.Columns[8].Visible = false;


                    }
                    gvTicket.Visible = true;

                }
                else
                {
                    if (idP == 3)
                    {

                        gvTicket.Columns[2].Visible = false;
                        gvTicket.Columns[6].Visible = false;
                        gvTicket.Columns[7].Visible = false;
                        gvTicket.Columns[8].Visible = false;

                    }
                    else if (idP == 1 || idP == 2)
                    {
                        gvTicket.Columns[10].Visible = false;
                    }
                    else if (idP == 4 || idP == 5)
                    {
                        gvTicket.Columns[6].Visible = false;
                        gvTicket.Columns[8].Visible = false;
                    }
                    ds.Rows.Add(ds.NewRow());
                    gvTicket.DataSource = ds;
                    gvTicket.DataBind();

                    int totalColumns = gvTicket.Rows[0].Cells.Count;
                    gvTicket.Rows[0].Cells.Clear();
                    gvTicket.Rows[0].Cells.Add(new TableCell());
                    gvTicket.Rows[0].Cells[0].ColumnSpan = totalColumns;
                    gvTicket.Rows[0].Cells[0].Text = "No tiene tickets registrados";
                    gvTicket.Rows[0].Cells[0].Width = 1150;
                    gvTicket.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "LISTATICKET");
                }
                return;
            }
        }
               

        //BOTON REGISTRO REQUERIMIENTO
        

        private void filtrar_datos() {


            try
            {
            
            string query = "";         
    
            string estado = "";

            if (cboEstado.SelectedValue.ToString().Equals("0"))
            {
                estado = "null";
            }
            else
            {
                estado = cboEstado.SelectedValue.ToString();
            }

            String f1 = "";
            String f2 = "";

            if (string.IsNullOrWhiteSpace(txtFec.Text))
            {
                f1 = "null";
            }
            else
            {
                f1 = "'" + txtFec.Text + " 00:00:00.000'";
            }

            if (string.IsNullOrWhiteSpace(TxtFec2.Text))
            {
                f2 = "null";
            }
            else
            {
                f2 = "'" + TxtFec2.Text + " 23:59:59.999'";
            }
                try
                {

                    if (int.Parse(cboHistorial.SelectedValue.ToString()) == 1)
                    {

                        query = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido, Convert(Time(0),t.tiempo,0) as tiempo, t.observaciones, t.area, t.nombreImg " +
                        "from ticket t inner join estado e on t.idEstado = e.idEstado" +
                            " inner join usuario u on t.id = u.id left outer join usuario u2 on t.idTecnico=u2.id WHERE t.fecRegistro BETWEEN COALESCE(convert(datetime, " + f1 + ", 121), t.fecRegistro) AND COALESCE(convert(datetime, " + f2 + ", 121), t.fecRegistro) AND t.idEstado = COALESCE(" + estado + ", t.idEstado) order by t.fecRegistro desc, t.idEstado desc";
                    }
                    else if (int.Parse(cboHistorial.SelectedValue.ToString()) < 2018)
                    {
                        query = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido, Convert(Time(0),t.tiempo,0) as tiempo, t.observaciones, t.nombreImg " +
                     "from ticket" + cboHistorial.SelectedValue + " t inner join estado e on t.idEstado = e.idEstado" +
                         " inner join usuario u on t.id = u.id left outer join usuario u2 on t.idTecnico=u2.id WHERE t.fecRegistro BETWEEN COALESCE(convert(datetime, " + f1 + ", 121), t.fecRegistro) AND COALESCE(convert(datetime, " + f2 + ", 121), t.fecRegistro) AND t.idEstado = COALESCE(" + estado + ", t.idEstado) order by t.fecRegistro desc, t.idEstado desc";
                    }
                    else if (int.Parse(cboHistorial.SelectedValue.ToString()) >= 2018)
                    {
                        query = "select t.idTicket, t.fecRegistro, t.area, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido, Convert(Time(0),t.tiempo,0) as tiempo, t.observaciones, t.nombreImg " +
                 "from ticket" + cboHistorial.SelectedValue + " t inner join estado e on t.idEstado = e.idEstado" +
                     " inner join usuario u on t.id = u.id left outer join usuario u2 on t.idTecnico=u2.id WHERE t.fecRegistro BETWEEN COALESCE(convert(datetime, " + f1 + ", 121), t.fecRegistro) AND COALESCE(convert(datetime, " + f2 + ", 121), t.fecRegistro) AND t.idEstado = COALESCE(" + estado + ", t.idEstado) order by t.fecRegistro desc, t.idEstado desc";
                    }
                    if (query != null)
                    {
                        cn.Conectar();

                        SqlCommand sc = new SqlCommand(query, cn.getConexion());

                        SqlDataAdapter sa = new SqlDataAdapter(sc);
                        DataTable ds = new DataTable();

                        sa.Fill(ds);

                        gvTicket.DataSource = ds;

                        gvTicket.DataBind();
                        cn.Cerrar();

                        if (0 < gvTicket.Rows.Count)
                        {

                        }
                        else
                        {
                            ds.Rows.Add(ds.NewRow());
                            gvTicket.DataSource = ds;
                            gvTicket.DataBind();

                            int totalColumns = gvTicket.Rows[0].Cells.Count;
                            gvTicket.Rows[0].Cells.Clear();
                            gvTicket.Rows[0].Cells.Add(new TableCell());
                            gvTicket.Rows[0].Cells[0].ColumnSpan = totalColumns;
                            gvTicket.Rows[0].Cells[0].Text = "No tiene tickets registrados";
                            gvTicket.Rows[0].Cells[0].Width = 1150;
                            gvTicket.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        }
                    } }
                catch (Exception x)
                {
                    if (idP == 1)
                    {
                        query = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido, Convert(Time(0),t.tiempo,0) as tiempo, t.observaciones, t.area, t.nombreImg " +
                            "from ticket t inner join estado e on t.idEstado = e.idEstado" +
                                " inner join usuario u on t.id = u.id left outer join usuario u2 on t.idTecnico=u2.id WHERE t.fecRegistro BETWEEN COALESCE(convert(datetime, " + f1 + ", 121), t.fecRegistro) AND COALESCE(convert(datetime, " + f2 + ", 121), t.fecRegistro) AND t.idEstado = COALESCE(" + estado + ", t.idEstado) order by t.fecRegistro desc, t.idEstado desc";
                    }
                    else if (idP == 4 || idP == 5)
                    {
                        query = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, u2.username as usertec, t.fecAtendido, Convert(Time(0),t.tiempo,0) as tiempo, t.observaciones, t.area, t.nombreImg " +
                            "from ticket t inner join estado e on t.idEstado = e.idEstado" +
                                " inner join usuario u on t.id = u.id left outer join usuario u2 on t.idTecnico=u2.id WHERE t.fecRegistro BETWEEN COALESCE(convert(datetime, " + f1 + ", 121), t.fecRegistro) AND COALESCE(convert(datetime, " + f2 + ", 121), t.fecRegistro) AND t.idEstado = COALESCE(" + estado + ", t.idEstado) and t.id = " + id + " order by t.fecRegistro desc, t.idEstado desc";
                    }

                    cn.Conectar();

                    SqlCommand sc = new SqlCommand(query, cn.getConexion());

                    SqlDataAdapter sa = new SqlDataAdapter(sc);
                    DataTable ds = new DataTable();

                    sa.Fill(ds);

                    gvTicket.DataSource = ds;

                    gvTicket.DataBind();
                    cn.Cerrar();

                    if (0 < gvTicket.Rows.Count)
                    {

                    }
                    else
                    {
                        ds.Rows.Add(ds.NewRow());
                        gvTicket.DataSource = ds;
                        gvTicket.DataBind();

                        int totalColumns = gvTicket.Rows[0].Cells.Count;
                        gvTicket.Rows[0].Cells.Clear();
                        gvTicket.Rows[0].Cells.Add(new TableCell());
                        gvTicket.Rows[0].Cells[0].ColumnSpan = totalColumns;
                        gvTicket.Rows[0].Cells[0].Text = "No tiene tickets registrados";
                        gvTicket.Rows[0].Cells[0].Width = 1150;
                        gvTicket.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "FILTRARDATOS");
                }
                return;
            }
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
             if (Session["username"] == null)
            {
                Session["error"] = "Primero debe iniciar sesión";
                Response.Redirect("login.aspx");
                return;

            }
            filtrar_datos();
            
            }
       
        
        //BOTON DE EN EJECUCION DEL TECNICO
        protected void btnEjecucion_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }

                if (Session["idTicket"] == null)
            {
                lblError.Text = "Debe seleccionar un requerimiento de la tabla";
                lblError.Visible = true;
                return;
            }
            else
            {
                SqlCommand sc = new SqlCommand("select idEstado from ticket where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = @tck", cn.getConexion());
                sc.Parameters.AddWithValue("@tck", Session["idTicket"].ToString());
                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();
                int estado;
                estado = Convert.ToInt32(dr["idEstado"].ToString());                
                cn.Cerrar();

                    if (estado.Equals(1))
                    {
                        lblError.Text = "El ticket seleccionado de ser asignado a un técnico";
                        lblError.Visible = true;
                        return;
                    }

                else if (estado.Equals(3))
                {
                    lblError.Text = "El ticket seleccionado ya se encuentra en Ejecución.";
                    lblError.Visible = true;
                    return;
                }
                else
                {
                    cn.Conectar();
                    SqlCommand cmd = new SqlCommand("update ticket set idEstado=3, fecAtendido=getdate() where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = @tck", cn.getConexion());
                    cmd.Parameters.AddWithValue("@tck", Session["idTicket"].ToString());
                    cmd.ExecuteNonQuery();
                    cn.Cerrar();
                    Session.Remove("idTicket");
                    ListarTicket();
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
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNEJECUCION");
                }
                return;
            }
        }

        //BOTON EDITAR DEL USUARIO
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                 if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                if (Session["idTicket"] == null)
            {
                    lblError.Text = "Debe seleccionar un requerimiento de la tabla";
                    lblError.Visible = true;
                    //ListarTicket();
                    //return;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#myModal2').modal();", true);
                    upModal2.Update();
                    return;
                }
                txtreq.Text = Session["requerimiento"].ToString();
                txtObs.Text = Session["observaciones"].ToString();
                lblticket.Text = "Editar Ticket N° - " + Session["idTicket"].ToString();
                int contador1 =  Session["requerimiento"].ToString().Length;
                string contadors1 = contador1.ToString();
                txtContador.Text = contadors1;
                int contador2 =  Session["observaciones"].ToString().Length;
                string contadors2 = contador2.ToString();
                txtContador2.Text = contadors2;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                upModal.Update();
            }
            catch (Exception)
            {
                lblError.Text = "Ha sucedido un error inesperado.";
                lblError.Visible = true;
                return;
            }

        }
        
        //BOTON DE REALIZADO DEL TECNICO
        protected void btnRealizado_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                if (Session["idTicket"] == null) {
                lblError.Text = "Debe seleccionar un requerimiento de la tabla";
                lblError.Visible = true;
                ListarTicket();
                return;
                }
                else
                {
                    string fecAtendido = "";
                    string idEstado = "";

                    SqlCommand sc = new SqlCommand("select fecAtendido, idEstado from ticket where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = @tck", cn.getConexion());
                    sc.Parameters.AddWithValue("@tck", Session["idTicket"].ToString());
                    cn.Conectar();
                    SqlDataReader dr = sc.ExecuteReader();

                    dr.Read();
                    
                    fecAtendido = dr["fecAtendido"].ToString();
                    idEstado = dr["idEstado"].ToString();
                    cn.Cerrar();

                    if (string.IsNullOrEmpty(fecAtendido))
                    {
                        lblError.Text = "El estado del ticket seleccionado debe estar 'En Ejecucion' para poder finalizar el ticket";
                        lblError.Visible = true;
                        return;
                    }
                    else if (idEstado.Equals("4"))
                    {
                        lblError.Text = "El ticket seleccionado ya a sido finalizado anteriormente";
                        lblError.Visible = true;
                        return;
                    }

                    string popup = "window.open('ticketFin.aspx','ticket','titlebar=yes,location=yes,status=no,menubar=yes,scrollbars=yes,resizable=yes, width=600,Height=450,left=400,top=250')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", popup, true);

                }            
            }
            catch (Exception)
            {
                lblError.Text = "Ha sucedido un error inesperado.";
                lblError.Visible = true;
                return;
            }
        }

        //BOTON DE ASIGNAR UN TECNICO 
        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            { if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                if (Session["idTicket"] == null)
                {
                lblError.Text = "Debe seleccionar un requerimiento de la tabla";
                lblError.Visible = true;                
                ListarTicket();
                return;
                }
                else
                {
                    
                    string idTecnico = "";

                    SqlCommand sc = new SqlCommand("select idTecnico from ticket where RIGHT('000000' + cast(idTicket AS varchar(6)), 6) = @tck", cn.getConexion());
                    sc.Parameters.AddWithValue("@tck", Session["idTicket"].ToString());
                    cn.Conectar();
                    SqlDataReader dr = sc.ExecuteReader();

                    dr.Read();
                    
                    idTecnico = dr["idTecnico"].ToString();
                    cn.Cerrar();

                   if (!string.IsNullOrEmpty(idTecnico))
                    {
                        lblError.Text = "El ticket seleccionado ya a sido asiganado a un técnico anteriormente";
                        lblError.Visible = true;
                        return;
                    }
                }
            string popup = "window.open('AsignarTecnico.aspx','ticket','titlebar=yes,location=yes,status=no,menubar=yes,scrollbars=yes,resizable=yes,width=400,Height=450,left=400,top=250')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", popup, true);
            }
            catch (Exception)
            {
                lblError.Text = "Ha sucedido un error inesperado.";
                lblError.Visible = true;
                return;
            }
        }

        
        protected void gvTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTicket.PageIndex = e.NewPageIndex;
            if (cboHistorial.SelectedValue.ToString() == "ticket")
            {
                if (string.IsNullOrEmpty(txtFec.Text) && string.IsNullOrEmpty(TxtFec2.Text))
                {                    
                    ListarTicket();
                }
                else
                {
                    filtrar_datos();
                }
            }
            else
            {
                ListarTicketCbo();
            }
        }

        
        

        protected void btnCierre_Click(object sender, EventArgs e)
        {
            try
            {
               
               if (Session["username"] == null)
                {
                    Session["error"] = "Primero debe iniciar sesión";
                    Response.Redirect("login.aspx");
                    return;

                }
                DateTime fecha = DateTime.Now;
                int año = fecha.Year;
                string query1 = "EXEC sp_rename @objname = 'ticket', @newname = 'ticket" + año + "'";

                string query2 = "SELECT * Into ticket From ticket" + año + " where 0=1";

                string query3 = "insert into ListaTicket values (" + año + ", 'Ticket " + año + "', GETDATE()";

                cn.Conectar();
                SqlCommand q1 = new SqlCommand(query1, cn.getConexion());
                q1.ExecuteNonQuery();
                cn.Cerrar();

                cn.Conectar();
                SqlCommand q2 = new SqlCommand(query2, cn.getConexion());
                q2.ExecuteNonQuery();
                cn.Cerrar();

                cn.Conectar();
                SqlCommand q3 = new SqlCommand(query3, cn.getConexion());
                q3.ExecuteNonQuery();
                cn.Cerrar();

                Response.Redirect("ticket.aspx");

                lblCorrecto.Text = "El cierre se realizó correctamente.";
                lblCorrecto.Visible = true;
                
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNCIERRE");
                }
                return;
            }

        }

        protected void gvTicket_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Session["error"] = "Primero debe iniciar sesión";
                Response.Redirect("login.aspx");
                return;

            }
            foreach (GridViewRow row in gvTicket.Rows)
            {
                if (row.RowIndex == gvTicket.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#2a73b2");
                    row.ForeColor = ColorTranslator.FromHtml("#ffffff");
                    row.ToolTip = string.Empty;
                    GridViewRow row2 = (GridViewRow)gvTicket.SelectedRow;
                    //Response.Write("<script>alert('" + row2.Cells[3].Text + "');</script>");


                    Session["requerimiento"] = row2.Cells[4].Text;
                    Session["observaciones"] = row2.Cells[9].Text;
                    Session["idTicket"] = row2.Cells[0].Text;
                    
                    
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ForeColor = ColorTranslator.FromHtml("#818181");           
                    row.ToolTip = "Seleccionar para editar.";

                }
            }
        }

    

        protected void gvTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTicket, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click para seleccionar este ticket";
            }
        }
              
        //BOTON FINALIZADO DE LOS TICKETS POR EL ADMIN
        

        protected void cboHistorial_SelectedIndexChanged(object sender, EventArgs e){
            if (int.Parse(cboHistorial.SelectedValue) == 1)
            {
                ListarTicket();
            }
            else
            {
                ListarTicketCbo();
            }
        }

       

        protected void cboHistorial_Data()
        {
            try
            {

            
            cn.Conectar();
            SqlCommand sc = new SqlCommand("select idListaTicket, descLarga from ListaTicket " +
                " order by idListaTicket asc", cn.getConexion());

            SqlDataAdapter sa = new SqlDataAdapter(sc);
            DataSet ds = new DataSet();
            sa.Fill(ds);
            
            cboHistorial.DataSource = ds;
            
            cboHistorial.DataTextField = "descLarga";
            cboHistorial.DataValueField = "idListaTicket";
            cboHistorial.DataBind();

            cn.Cerrar();
            }
            catch (Exception x)
            {
                lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblError.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "CBOHISTORIAL_DATA");
                }
                return;
            }

        }

        protected void cerrarsesion_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }



        /*Boton guarda la edicion del ticket*/
        protected void btnEditarTicket_Click(object sender, EventArgs e)
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
                    if (idP == 1)
                    {
                        if (fileImg.HasFile)
                        {
                            query = "update ticket set requerimiento = '" + txtreq.Text + "', observaciones = '" + txtObs.Text + "', imagen = @img, nombreImg = @nomImg where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                            cn.Conectar();
                            SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                            cmd.Parameters.AddWithValue("@img", fileImg.FileBytes);
                            cmd.Parameters.AddWithValue("@nomImg", fileImg.FileName);

                            cmd.ExecuteNonQuery();
                            cn.Cerrar();

                            lblCorrectoEditar.Text = "Se ha modificado correctamente.";
                            lblCorrectoEditar.Visible = true;

                        }
                        else
                        {
                            query = "update ticket set requerimiento = '" + txtreq.Text + "', observaciones = '" + txtObs.Text + "'  where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                            cn.Conectar();
                            SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                            cmd.ExecuteNonQuery();
                            cn.Cerrar();

                            lblCorrectoEditar.Text = "Se ha modificado correctamente.";
                            lblCorrectoEditar.Visible = true;
                        }
                        ListarTicket();

                    }
                    else if (idP == 2)
                    {
                        query = "update ticket set observaciones = '" + txtObs.Text + "' where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                        cn.Conectar();
                        SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                        cmd.ExecuteNonQuery();
                        cn.Cerrar();

                        lblCorrectoEditar.Text = "Se ha modificado correctamente.";
                        lblCorrectoEditar.Visible = true;

                    }
                    else if (idP == 3)
                    {
                        query = "update ticket set requerimiento = '" + txtreq.Text + "' where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                        cn.Conectar();
                        SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                        cmd.ExecuteNonQuery();
                        cn.Cerrar();

                        lblCorrectoEditar.Text = "Se ha modificado correctamente.";
                        lblCorrectoEditar.Visible = true;
                    }
                    else if (idP == 4 || idP == 5)
                    {
                        query = "update ticket set observaciones = '" + txtObs.Text + "' where RIGHT('000000'+ cast(idTicket AS varchar(6)), 6) = '" + Session["idTicket"].ToString() + "'";
                        cn.Conectar();
                        SqlCommand cmd = new SqlCommand(query, cn.getConexion());

                        cmd.ExecuteNonQuery();
                        cn.Cerrar();
                        lblCorrectoEditar.Text = "Se ha modificado correctamente.";
                        lblCorrectoEditar.Visible = true;
                    }
                }
            }
            catch (Exception x)
            {
                lblErrorEditar.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                lblErrorEditar.Visible = true;
                if (Session["username"] != null)
                {
                    ConfigSMTP smtp = new ConfigSMTP();
                    smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "BTNGRABAR");
                }
                return;
            }
        }

        protected void DropDownListTitulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(DropDownListTitulo.SelectedValue) == 0)
            {
                Titulo.Text = "Tickets Pendientes";
                Session.Remove("error");
                ListarTicket();

            }
            else
            {
                try
                {

                    Titulo.Text = "Mis Tickets";
                    string login = (string)(Session["username"]);
                    cn.Conectar();
                    string query = "select t.idTicket, t.fecRegistro, u.username, t.requerimiento, e.desEstado, " +
                        "u2.username as usertec, t.fecAtendido, t.tiempo, " +
                        "t.observaciones, t.nombreImg, t.area" +
                        " from ticket t inner join estado e on t.idEstado=e.idEstado" +
                        " inner join usuario u on t.id=u.id left outer join usuario u2 on t.idTecnico=u2.id ";


                    query = query + "where t.idEstado IN (1, 2, 3, 4) and u.id = " + id + " order by t.fecRegistro desc";

                    SqlCommand sc = new SqlCommand(query, cn.getConexion());
                    SqlDataAdapter sa = new SqlDataAdapter(sc);
                    DataTable ds = new DataTable();

                    sa.Fill(ds);

                    gvTicket.DataSource = ds;

                    gvTicket.DataBind();
                    cn.Cerrar();
                    Session.Remove("error");
                    if (0 < gvTicket.Rows.Count)
                    {

                        if (idP == 4 || idP == 5)
                        {
                            gvTicket.Columns[6].Visible = false;
                            gvTicket.Columns[8].Visible = false;


                        }
                        gvTicket.Visible = true;

                    }
                    else
                    {
                    
                    if (idP == 4 || idP == 5)
                    {
                        gvTicket.Columns[6].Visible = false;
                        gvTicket.Columns[8].Visible = false;
                    }
                    ds.Rows.Add(ds.NewRow());
                    gvTicket.DataSource = ds;
                    gvTicket.DataBind();

                    int totalColumns = gvTicket.Rows[0].Cells.Count;
                    gvTicket.Rows[0].Cells.Clear();
                    gvTicket.Rows[0].Cells.Add(new TableCell());
                    gvTicket.Rows[0].Cells[0].ColumnSpan = totalColumns;
                    gvTicket.Rows[0].Cells[0].Text = "No tiene tickets registrados";
                    gvTicket.Rows[0].Cells[0].Width = 1150;
                    gvTicket.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
                } 
                catch (Exception x)
                {
                    lblError.Text = "Ha ocurrido un error inesperado. Comuniquese con Soporte Técnico.";
                    lblError.Visible = true;
                    if (Session["username"] != null)
                    {
                        ConfigSMTP smtp = new ConfigSMTP();
                        smtp.enviarError(x.Message.ToString(), Request.Url.ToString(), Session["username"].ToString(), "LISTATICKET");
                    }
                    return;
                }
            }
        }
        //COSAS DE RENATO
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            string cerrar = "myModal.hide();";
            Session.Remove("idTicket");
            Session.Remove("requerimiento");
            Session.Remove("observaciones");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", cerrar, true);
        }
    }
}