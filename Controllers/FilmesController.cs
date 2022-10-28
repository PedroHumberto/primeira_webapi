
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Data;
using System;
using System.Collections.Generic;
using FilmesAPI.Data.Dtos;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {

        private FilmeContext _context;
        public FilmesController(FilmeContext context){
            _context = context;
        }

        [HttpPost]
        public IActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = new Filme{
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Diretor = filmeDto.Diretor,
                Duracao = filmeDto.Duracao  
            };

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
                ReadFilmeDto filmeDto = new ReadFilmeDto{
                    Titulo = filme.Titulo,
                    Genero = filme.Genero,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    HoraConsulta = DateTime.Now
                };
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
            filme.Diretor = filmeNovoDiretorDto.Diretor;

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
            filme.Duracao = filmeNovoDto.Duracao;
            filme.Genero = filmeNovoDto.Genero;
            filme.Titulo = filmeNovoDto.Titulo;
            filme.Diretor = filmeNovoDto.Diretor;
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