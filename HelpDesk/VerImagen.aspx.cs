using HelpDesk.cs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelpDesk
{
    public partial class VerImagen : System.Web.UI.Page
    {
        private Conexion cn;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                this.cn = new Conexion();

                int id = Convert.ToInt32(Request.QueryString["idTicket"]);
                byte[] img;
                string nombre = "";

                SqlCommand sc = new SqlCommand("select imagen, nombreImg from ticket where idTicket= " + id + "", cn.getConexion());

                cn.Conectar();
                SqlDataReader dr = sc.ExecuteReader();

                dr.Read();

                img = (Byte[])dr["imagen"];
                //img = Encoding.ASCII.GetBytes(dr["imagen"].);
                nombre = dr["nombreImg"].ToString();
                cn.Cerrar();
                string strBase64 = Convert.ToBase64String(img);

                Image1.ImageUrl = "data:Image/png;base64," + strBase64;



                //File.WriteAllBytes("D:\\Mirko\\" + nombre, img);
                //Response.Redirect("ticket.aspx");
                //FileStream file = new FileStream("D:\\Mirko\\" + nombre, FileMode.Create);
                //file.Write(img, 0, img.Length);
                //file.Close();

                //Response.Clear();
                //Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", nombre));
                //Response.Clear();
                //// Con esto le decimos al browser que la salida sera descargable
                //Response.ContentType = "application/octet-stream";
                //// esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                //Response.AddHeader("Content-Disposition", "attachment; filename='"+nombre+"'");
                //// Escribimos el fichero a enviar 
                //Response.WriteFile("img");
                //// volcamos el stream 
                //Response.Flush();
                //// Enviamos todo el encabezado ahora
                //Response.End();
                //switch (Path.GetExtension(nombre).ToLower())
                //{
                //    case ".jpg":
                //        Response.ContentType = "image/jpg";
                //        break;
                //    case ".gif":
                //        Response.ContentType = "image/gif";
                //        break;
                //    case ".png":
                //        Response.ContentType = "image/png";
                //        break;
                //}

                //Response.BinaryWrite(img);
                //Response.End();



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
    }
}
