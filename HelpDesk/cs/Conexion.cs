using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace HelpDesk.cs
{
    public class Conexion
    {
        private SqlConnection conexion;

        public Conexion()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["helpdeskEntities"].ConnectionString;
            this.conexion = new SqlConnection(s);
        }
        public void Conectar()
        {
            this.conexion.Open();
        }

        public void Cerrar()
        {
            this.conexion.Close();

        }

        public SqlConnection getConexion()
        {
            return this.conexion;
        }
    }
}