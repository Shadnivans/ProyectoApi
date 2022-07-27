using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProyectoApi.Utils
{
    internal class StringHelpers
    {
        public static T ConvertTo<T>(DataTable Dt)
        {
            string JsonString = JsonConvert.SerializeObject(Dt);
            List<T> Model = JsonConvert.DeserializeObject<List<T>>(JsonString);
            return Model.FirstOrDefault();
        }

        public static List<T> ConvertToList<T>(DataTable Dt)
        {
            string JsonString = JsonConvert.SerializeObject(Dt);
            return JsonConvert.DeserializeObject<List<T>>(JsonString);
        }

        public static SqlCommand ConnectionCommand(SqlConnection Connection, string Sintaxis, bool IsProcedure = false, int TimeOut = 30)
        {
            CommandType commadType = (IsProcedure == true ? CommandType.StoredProcedure : CommandType.Text);
            return new SqlCommand
            {
                CommandType = commadType,
                CommandText = Sintaxis,
                Connection = Connection,
                CommandTimeout = TimeOut
            };
        }
    }
}
