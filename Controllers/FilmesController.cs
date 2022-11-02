
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
    public class FilmesController : ControllerBase
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public FilmesController(AppDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarFilmePorId), new {Id = filme.Id}, filme); //status da requisição e o local onde foi criado o recurso "https://localhost:5000/Filmes/1"
        }
       
        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody]UpdateDiretorDto filmeNovoDiretorDto){
            
            
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
        public IActionResult AtualizaCampo(int id, [FromBody] UpdateFilmeDto filmeNovoDto){
            
            
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);


            if (filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeNovoDto, filme);

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id){
            
            
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();

        }


    }
}


/*
{
    "Titulo": "Senhor Dos Aneis",
    "Diretor": "Peter Jackson",
    "Genero": "Aventura",
    "Duracao": "180"
}
*/