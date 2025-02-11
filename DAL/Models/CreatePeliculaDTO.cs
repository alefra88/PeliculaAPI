namespace PruebaManana.DAL.Models
{
    public class CreatePeliculaDTO
    {
        public string Titulo { get; set; } = null!;

        public string? Imagen { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaEstreno { get; set; }

        public int? Estrellas { get; set; }
    }
}
