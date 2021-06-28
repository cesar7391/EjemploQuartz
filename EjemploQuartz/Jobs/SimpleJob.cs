using EjemploQuartz.Models;
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
    public class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //Parámetros
            //JobDataMap dataMap = context.JobDetail.JobDataMap;
            JobDataMap dataMap2 = context.MergedJobDataMap;
            //UserModel user = (UserModel)dataMap2.Get("user");
            ProductoModel pro_model = (ProductoModel)dataMap2.Get("Producto");

            //string triggerparam = dataMap2.GetString("triggerparam");

            InsertarDatosJ(pro_model.Nombre, pro_model.Descripcion, pro_model.Precio);
            //var mensaje = $"Ejecución simple, USERNAME: {user.Username}, PASSWORD: {user.Password}, TRIGGERPARAM: {triggerparam} el día {DateTime.Now.ToString()}";
            var mensaje = $"Ejecución simple, Insertando datos [{pro_model.Nombre},{pro_model.Descripcion},{pro_model.Precio}] el día {DateTime.Now.ToString()}";
            Debug.WriteLine(mensaje);
        }

        public async void InsertarDatosJ(string nombre, string descripcion, double precio)
        {
            try
            {
                string cs = "Data Source=.;Initial Catalog=QuartzTest;Integrated Security=True";

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
