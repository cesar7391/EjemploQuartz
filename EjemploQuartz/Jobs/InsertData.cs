using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploQuartz.Jobs
{
    public class InsertData: IJob
    {
        private readonly IConfiguration configuration;

        public InsertData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {                
                //string cs = configuration.GetConnectionString("DefaultConnection");
                string cs = "Data Source=.;Initial Catalog=QuartzTest;Integrated Security=True";
                //var lista = new List<ResponsePromociones>();

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Guardar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pro_nombre", "ProductoSch"));
                    cmd.Parameters.Add(new SqlParameter("@pro_descripcion", "DescripciónProductoSch"));
                    cmd.Parameters.Add(new SqlParameter("@pro_precio", 1600));

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        var mensaje = $"Ejecución simple, el día {DateTime.Now.ToString()}";
                        Debug.WriteLine(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void InsertarDatosJ()
        {
            try
            {
                //string cs = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                string cs = configuration.GetConnectionString("DefaultConnection");
                //var lista = new List<ResponsePromociones>();

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Guardar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pro_nombre", "ProductoSch"));
                    cmd.Parameters.Add(new SqlParameter("@pro_descripcion", "DescripciónProductoSch"));
                    cmd.Parameters.Add(new SqlParameter("@pro_precio", 1600));

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarDatos(string nombre, string descripcion, double precio)
        {
            try
            {
                //string cs = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                string cs = configuration.GetConnectionString("DefaultConnection");
                //var lista = new List<ResponsePromociones>();

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Guardar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pro_nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@pro_descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@pro_precio", precio));

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
