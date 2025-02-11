using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaManana.BLL;
using PruebaManana.DAL.Data;
using PruebaManana.DAL.Models;

namespace PruebaManana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly Servicios _servicios;

        public PeliculasController(DBContext context, Servicios servicios)
        {
            _context = context;
            _servicios = servicios;
        }

        // GET: api/Peliculas
        [HttpGet]
        public async Task<IActionResult> GetPeliculas()
        {
            var peliculas =  await _servicios.ConsultarPeliculas();
            return Ok(peliculas);
        }   

        // GET: api/Peliculas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeliculas(int id)
        {
            var peliculas = await _servicios.ConsultarPeliculaID(id);

            if (peliculas == null)
            {
                return NotFound();
            }

            return Ok(peliculas);
        }

        // PUT: api/Peliculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeliculas(int id,[FromBody] Peliculas pelicula)
        {
            if (id != pelicula.IdPelicula)
            {
                return BadRequest("El ID de la URL no coincide con el del cuerpo de la solicitud.");
            }

            await _servicios.EditarPelicula(pelicula);
            return NoContent();
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task PostPeliculas(CreatePeliculaDTO pelicula)
        {

            await _servicios.AgregarPelicula(pelicula);
        }

        // DELETE: api/Peliculas/5
        [HttpDelete("{id}")]
        public async Task DeletePeliculas(int id)
        {
            await _servicios.EliminarPelicula(id);
        }

        private bool PeliculasExists(int id)
        {
            return _context.Peliculas.Any(e => e.IdPelicula == id);
        }
    }
}
