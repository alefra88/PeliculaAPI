using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaManana.DAL.Models;

namespace PruebaManana.DAL.Data;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Peliculas> Peliculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Peliculas>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK__Pelicula__60537FD06C5C0A87");

            entity.Property(e => e.FechaEstreno).HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);




    public async Task<List<Peliculas>> ConsultarPeliculas()
    {
        return await Peliculas
            .FromSqlRaw("EXEC sp_ConsultarPelicula")
            .ToListAsync();
       
    }

    public async Task<Peliculas?> ConsultarPeliculaId(int idPelicula)
        {
        var result = await Peliculas
        .FromSqlRaw("EXEC sp_ConsultarPeliculaId @IdPelicula", new SqlParameter("@IdPelicula", idPelicula))
        .ToListAsync();
        return result.FirstOrDefault();

        }
    public async Task AgregarPelicula(CreatePeliculaDTO pelicula)
    {
        var tituloParam = new SqlParameter("@Titulo", pelicula.Titulo);
        var imagenParam = new SqlParameter("@Imagen", pelicula.Imagen ?? (object)DBNull.Value);  
        var descripcionParam = new SqlParameter("@Descripcion", pelicula.Descripcion);
        var fechaEstrenoParam = new SqlParameter("@FechaEstreno", Convert.ToDateTime(pelicula.FechaEstreno));
        var estrellasParam = new SqlParameter("@Estrellas", pelicula.Estrellas);

        await Database.ExecuteSqlRawAsync(
            "EXEC sp_InsertarPelicula @Titulo, @Imagen, @Descripcion, @FechaEstreno,@Estrellas",
            tituloParam,
            imagenParam,
            descripcionParam,
            fechaEstrenoParam,
            estrellasParam
            );
    }

    public async Task EditarPelicula(Peliculas pelicula)
    {
        var idPeliculaParam = new SqlParameter("@IdPelicula", pelicula.IdPelicula);
        var tituloParam = new SqlParameter("@Titulo", pelicula.Titulo);
        var imagenParam = new SqlParameter("@Imagen", pelicula.Imagen ?? (object)DBNull.Value);
        var descripcionParam = new SqlParameter("@Descripcion", pelicula.Descripcion);
        var fechaEstrenoParam = new SqlParameter("@FechaEstreno", pelicula.FechaEstreno);
        var estrellasParam = new SqlParameter("@Estrellas", pelicula.Estrellas);

         await Database.ExecuteSqlRawAsync("EXEC sp_ActualizarPelicula @IdPelicula,@Titulo,@Imagen,@Descripcion,@FechaEstreno,@Estrellas",
            idPeliculaParam,
            tituloParam,
            imagenParam,
            descripcionParam,
            fechaEstrenoParam,
            estrellasParam);
    }

    public async Task EliminarPelicula(int idPelicula)
    {
        await Database.ExecuteSqlRawAsync("EXEC sp_EliminarPelicula @IdPelicula", new SqlParameter("@IdPelicula", idPelicula));
    }

}
