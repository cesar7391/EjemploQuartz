using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploQuartz.Jobs
{
    public class RegistroEvento
    {
        public void RegistrarEvento(string JobName, string JobDescription, string JobStatus)
        {
            try
            {
                string cs = "Data Source=.;Initial Catalog=QuartzTest;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Registro", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@job_nombre", JobName));
                    cmd.Parameters.Add(new SqlParameter("@job_descripcion", JobDescription));
                    cmd.Parameters.Add(new SqlParameter("@job_status", JobStatus));

                    SqlDataReader rdr = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
