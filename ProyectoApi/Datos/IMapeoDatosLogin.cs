using ProyectoApi.Models.Login.Operaciones;
using System.Threading.Tasks;

namespace ProyectoApi.Datos
{
    public interface IMapeoDatosLogin
    {
        Task<AccessResponse> Access(AccessRequest Request);
    }
}
