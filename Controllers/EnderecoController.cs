
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
    public class EnderecoController : ControllerBase
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public EnderecoController(AppDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new {Id = endereco.Id}, endereco); //status da requisição e o local onde foi criado o recurso "https://localhost:5000/Filmes/1"
        }
       
        [HttpGet]
        public IEnumerable<Endereco> RecuperaEndereco()
        {
            return _context.Enderecos;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecoPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return Ok(enderecoDto);
            }
            else
            {
                return NotFound();
            }
        }
        

        [HttpPut("{id}")]
        public IActionResult AtualizaCampo(int id, [FromBody] UpdateEnderecoDto enderecoNovoDto){
            
            
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);


            if (endereco == null)
            {
                return NotFound();
            }
            _mapper.Map(enderecoNovoDto, endereco);

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id){
            
            
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return NotFound();
            }
            _context.Remove(endereco);
            _context.SaveChanges();

            return NoContent();

        }


    }
}