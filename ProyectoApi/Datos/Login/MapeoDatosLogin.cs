using Newtonsoft.Json;
using ProyectoApi.Models.Login.Operaciones;
using ProyectoApi.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoApi.Datos.Login
{
    public class MapeoDatosLogin : IMapeoDatosLogin
    {
        async Task<AccessResponse> IMapeoDatosLogin.Access(AccessRequest Request)
        {
            try
            {
                DataSet Result = await AccessAsyncLogin(Request);
                if (Result.Tables.Count == 0)
                {
                    throw new Exception("No se encontró la información");
                }
                if (Result.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("No se encontró la información");
                }
                return StringHelpers.ConvertTo<AccessResponse>(Result.Tables[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        async Task<List<RecetaResponse>> IMapeoDatosLogin.Recetas()
        {
            try
            {
                DataSet Result = await RecetasAsyncLogin();
                if (Result.Tables.Count == 0)
                {
                    throw new Exception("No se encontró la información");
                }
                if (Result.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("No se encontró la información");
                }

                var detalles = StringHelpers.ConvertToList<RecetaDetalle>(Result.Tables[1]);
                var cabeceras = this.ListaRecetas(Result.Tables[0],detalles);

                return cabeceras;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Métodos de consulta de la clase



        #endregion

        #region Métodos de operaciones de la clase

        private async Task<DataSet> AccessAsyncLogin(AccessRequest Request)
        {
            DataSet Response = new DataSet();
            using (SqlConnection Connection = new SqlConnection(Environment.GetEnvironmentVariable(StringHelpers.coneccion)))
            {
                try
                {
                    SqlCommand Cmd = StringHelpers.ConnectionCommand(Connection, "sp_get_usuario", true);
                    Cmd.Parameters.Add(new SqlParameter("@i_Usuario", Request.Usuario));
                    Cmd.Parameters.Add(new SqlParameter("@i_Contrasena", Request.Contrasena));
                    await Connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                    adapter.Fill(Response);
                    await Connection.CloseAsync();
                }
                catch
                {
                    await Connection.CloseAsync();
                }
            }
            return Response;
        }

        private async Task<DataSet> RecetasAsyncLogin()
        {
            DataSet Response = new DataSet();
            using (SqlConnection Connection = new SqlConnection(Environment.GetEnvironmentVariable(StringHelpers.coneccion)))
            {
                try
                {
                    SqlCommand Cmd = StringHelpers.ConnectionCommand(Connection, "sp_get_recetas", true);
                    await Connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(Cmd);
                    adapter.Fill(Response);
                    await Connection.CloseAsync();
                }
                catch
                {
                    await Connection.CloseAsync();
                }
            }
            return Response;
        }

        #endregion

        #region Metodos Auxiliares

        private List<RecetaResponse> ListaRecetas(DataTable data, List<RecetaDetalle> detalles)
        {
            var lista = new List<RecetaResponse>();
            var P_Response = new RecetaResponse.RecetaBuilder();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                var a = Convert.ToInt32(data.Rows[i][0]);
                var b = Convert.ToString(data.Rows[i][1]);
                var c = Convert.ToString(data.Rows[i][2]);
                var d = Convert.ToString(data.Rows[i][3]);
                var z = detalles.Where(y => y.IdReceta == a).ToList();

                P_Response.setIdReceta(a);
                P_Response.setCookingTime(b);
                P_Response.setMethod(c);
                P_Response.setTitle(d);
                P_Response.setDetalles(z);

                lista.Add(P_Response.build());
            }

            return lista;
        }

        #endregion
    }
}
