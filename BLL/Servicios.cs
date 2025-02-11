using Microsoft.AspNetCore.Mvc;
using PruebaManana.DAL.Data;
using PruebaManana.DAL.Models;

namespace PruebaManana.BLL
{
    public class Servicios
    {
        private readonly DBContext _dbContext;
        public Servicios(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Peliculas>> ConsultarPeliculas()
        {
            return await _dbContext.ConsultarPeliculas();
        }

        public async Task<Peliculas?> ConsultarPeliculaID(int idPelicula)
        {
            return await _dbContext.ConsultarPeliculaId(idPelicula);
        }

        public async Task AgregarPelicula(CreatePeliculaDTO pelicula)
        {
            await _dbContext.AgregarPelicula(pelicula);
        }

        public async Task EditarPelicula(Peliculas pelicula)
        {
            await _dbContext.EditarPelicula(pelicula);
        }

        public async Task EliminarPelicula(int idPelicula)
        {
            await _dbContext.EliminarPelicula(idPelicula);
        }
    }
}
