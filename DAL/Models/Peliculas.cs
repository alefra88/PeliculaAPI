using System;
using System.Collections.Generic;

namespace PruebaManana.DAL.Models;

public partial class Peliculas
{
    public int IdPelicula { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Imagen { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaEstreno { get; set; }

    public int? Estrellas { get; set; }
}
