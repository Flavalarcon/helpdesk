using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Configuration;

namespace GuardarImagenBaseDatos
{
    public static class ImagenesDAL
    {

        public static Imagenes GetImagenById(int Id)
        {
            Imagenes img = null;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();

                string query = @"SELECT Id, Nombre, Length, Imagen
                                FROM Imagenes
                                WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    img = new Imagenes(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToString(reader["nombre"]),
                                    Convert.ToInt32(reader["length"]));

                    img.Imagen = (byte[])reader["Imagen"];

                }

            }

            return img;

        }

    }


    public class Imagenes
    {
        public Imagenes(int id, string nombre, int length)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Length = length;
        }
        public int Id { get; set; }
        public int Length { get; set; }
        public string Nombre { get; set; }

        public byte[] Imagen { get; set; }
    }

}
