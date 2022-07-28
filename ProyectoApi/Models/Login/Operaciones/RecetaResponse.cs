using ProyectoApi.Datos;
using System.Collections.Generic;

namespace ProyectoApi.Models.Login.Operaciones
{
    public class RecetaResponse
    {
        public int IdReceta { get; set; }
        public string Title { get; set; }
        public string CookingTime { get; set; }
        public string Method { get; set; }
        public List<RecetaDetalle> Detalles { get; set; }

        public RecetaResponse(int IdReceta, string Title, string CookingTime, string Method, List<RecetaDetalle> Detalles)
        {
            this.IdReceta = IdReceta;
            this.Title = Title;
            this.CookingTime = CookingTime;
            this.Method = Method;
            this.Detalles = Detalles;
        }

        public class RecetaBuilder : IBuilder<RecetaResponse>
        {
            private int IdReceta;
            private string Title;
            private string CookingTime;
            private string Method;
            private List<RecetaDetalle> Detalles;

            public RecetaBuilder setIdReceta(int IdReceta) { 
                this.IdReceta= IdReceta;
                return this;
            }

            public RecetaBuilder setTitle(string Title)
            {
                this.Title = Title;
                return this;
            }

            public RecetaBuilder setCookingTime(string CookingTime)
            {
                this.CookingTime = CookingTime;
                return this;
            }

            public RecetaBuilder setMethod(string Method)
            {
                this.Method = Method;
                return this;
            }

            public RecetaBuilder setDetalles(List<RecetaDetalle> Detalles)
            {
                this.Detalles = Detalles;
                return this;
            }

            public RecetaResponse build()
            {
                return new RecetaResponse(IdReceta, Title, CookingTime, Method, Detalles);
            }
        }

    }

    public class RecetaDetalle
    {
        public int IdDetalle { get; set; }
        public int IdReceta { get; set; }
        public string Ingrediente { get; set; }
    }
}
