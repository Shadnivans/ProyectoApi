using ProyectoApi.Models.Login.Operaciones;
using ProyectoApi.Utils;
using System;
using System.Data;
using System.Data.SqlClient;
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

        #region Métodos de consulta de la clase



        #endregion

        #region Métodos de operaciones de la clase

        private async Task<DataSet> AccessAsyncLogin(AccessRequest Request)
        {
            DataSet Response = new DataSet();
            using (SqlConnection Connection = new SqlConnection(""))
            {
                try
                {
                    SqlCommand Cmd = StringHelpers.ConnectionCommand(Connection, "", true);
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
    }
}
