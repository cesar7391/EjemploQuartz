using EjemploQuartz.Models;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploQuartz.Jobs
{
    public class SimpleJob : RegistroEvento, IJob
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
                //var mensaje = $"Ejecución simple, Insertando datos [{pro_model.Nombre},{pro_model.Descripcion},{pro_model.Precio}] el día {DateTime.Now.ToString()}";
                //System.Diagnostics.Debug.WriteLine(mensaje);
                //Console.WriteLine(mensaje);
                //RegistrarEvento("SimpleJob","Tarea ejecutada correctamente","EXITO");

        }

        public void InsertarDatosJ(string nombre, string descripcion, double precio)
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

                    SqlDataReader rdr = cmd.ExecuteReader();                    
                }

                RegistrarEvento(this.GetType().Name, "Tarea ejecutada correctamente", "EXITO");
                var mensaje = $"Insertando datos [{nombre},{descripcion},{precio}], el día {DateTime.Now.ToString()}";
                System.Diagnostics.Debug.WriteLine(mensaje);
                Console.WriteLine(mensaje);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                RegistrarEvento(this.GetType().Name, "Hubo un error: " + ex, "ERROR");
                var mensaje = $"Hubo en ERROR en la ejecución";
                System.Diagnostics.Debug.WriteLine(mensaje);
                Console.WriteLine(mensaje);
            }
        }        
    }
}
