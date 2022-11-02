
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Data;
using System;
using System.Collections.Generic;
using FilmesAPI.Data.Dtos;
using AutoMapper;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public CinemaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarCinemaPorId), new { Id = cinema.Id }, cinema); //status da requisição e o local onde foi criado o recurso "https://localhost:5000/Filmes/1"
        }

        [HttpGet]

        public IActionResult showCinemas()
        {
            return Ok(_context.Cinemas.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarCinemaPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(filme => filme.Id == id);

            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("{id}")]
        public IActionResult AtualizCinema(int id, [FromBody] UpdateDiretorDto filmeNovoDiretorDto)
        {


            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeNovoDiretorDto, filme);

            _context.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCampo(int id, [FromBody] UpdateCinemaDto cinemaNovoDto)
        {


            Cinema cinema = _context.Cinemas.FirstOrDefault(filme => filme.Id == id);


            if (cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaNovoDto, cinema);

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {


            Cinema cinema = _context.Cinemas.FirstOrDefault(filme => filme.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();

            return NoContent();

        }


    }
}