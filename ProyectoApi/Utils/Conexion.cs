using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoApi.Utils
{
    public class Conexion
    {


        public List<string> Get()
        {
            List<Estudiantes> ListaEstudiante = new List<Estudiantes>();
            using (SqlConnection Connection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    SqlCommand Cmd = this.ConnectionCommand(Connection, this.GetEstudiantes);
                    Connection.Open();
                    SqlDataReader DataReader = Cmd.ExecuteReader();
                    DataTable DtEstudiante = new DataTable();
                    DtEstudiante.Load(DataReader);
                    ListaEstudiante = this.ConvertToList<Estudiantes>(DtEstudiante);
                    Connection.Close();
                }
                catch
                {
                    Connection.Close();
                }
            }
            return ListaEstudiante;
        }









        #region Metodos Auxiliares

        private SqlCommand ConnectionCommand(SqlConnection Connection, string Sintaxis, bool IsProcedure = false)
        {
            CommandType commadType = (IsProcedure == true ? CommandType.StoredProcedure : CommandType.Text);
            return new SqlCommand
            {
                CommandType = commadType,
                CommandText = Sintaxis,
                Connection = Connection
            };
        }

        #endregion

    }
}
