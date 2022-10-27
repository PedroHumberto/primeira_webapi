
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {

        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        [HttpPost]
        public IActionResult AddFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmePorId), new {Id = filme.Id}, filme); //status da requisição e o local onde foi criado o recurso "https://localhost:5000/Filmes/1"
        }
        /*  
            O created action está falando qual é o caminho, qual é a ação que criou este recurso, se for olhar parâmetro por parâmetro, 
            nós precisamos passar para ele, qual é a action que precisa executar para recuperar esse recurso. Então recuperamos esse recurso através da função RecuperarFilmePorId,
            quando passa um filme e um Id, está executando uma ação de recuperar o filme por Id,  então nós passamos o nome dessa action que é o name of.
            Nós queremos essa lógica esteja sendo executada para recuperar o nosso location, queremos no segundo parâmetro passar nele o valor na rota, 
            que é o nosso Id.
            Certo, nossa Id está passando um que é igual o ID do filme que acabou de criar, então ´filme.Id´ que atribui anteriormente e por último passa também o valor, 
            o object em si que estamos querendo, que estamos tratando.        
            O recurso que está tratando é esse filme que recebemos que está passando para action, quando estamos criando um filme, 
            no final ele foi criado e pode ser recuperado através dessa action que é recupera filme por Id.
            Com este Id que foi passado e gerado para ele com este recurso. Seguindo todas boas práticas nas operações de criação e leitura, 
            quando cria um recurso, indica qual é a localização que ele foi criado e também retorna qual foi o recurso criado.
            Além de informarmos que o recurso foi criado, é importante informarmos onde podemos localizá-lo
        */
        [HttpGet]
        public IActionResult RecuperarFilmes()
        {
            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {

            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            else
            {
                return NotFound();
            }
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