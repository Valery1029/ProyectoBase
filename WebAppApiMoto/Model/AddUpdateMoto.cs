namespace WebAppApiMoto.Model
{
    public class AddUpdateMoto
    {
        public required string Codigo { get; set; }
        public required string Marca { get; set; }
        public required string Descripcion { get; set; }
        public double Modelo { get; set; }
        public double Precio { get; set; }
        public bool Disponible { get; set; }
    }
}
