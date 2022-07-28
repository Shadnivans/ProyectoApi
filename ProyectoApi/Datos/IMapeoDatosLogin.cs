using ProyectoApi.Models.Login.Operaciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoApi.Datos
{
    public interface IMapeoDatosLogin
    {
        Task<AccessResponse> Access(AccessRequest Request);

        Task<List<RecetaResponse>> Recetas();
    }
}
