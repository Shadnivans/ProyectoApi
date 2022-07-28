using System.Collections.Generic;

namespace ProyectoApi.Datos
{
    public interface IBuilder<T>
    {
        public T build();
    }
}
