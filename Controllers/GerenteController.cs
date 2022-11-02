
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
    public class GerenteControler : ControllerBase
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public GerenteControler(AppDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);

            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarGerentePorId), new {Id = gerente.Id}, gerente); //status da requisição e o local onde foi criado o recurso "https://localhost:5000/Filmes/1"
        }
       
        [HttpGet]
        public IEnumerable<Gerente> RecuperaGerente()
        {   
            return _context.Gerentes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return Ok(gerenteDto);
            }
            else
            {
                return NotFound();
            }
        }
       

        [HttpDelete("{id}")]
        public IActionResult DeletaGeremte(int id){
            
            
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);

            if (gerente == null)
            {
                return NotFound();
            }
            _context.Remove(gerente);
            _context.SaveChanges();

            return NoContent();

        }


    }
}