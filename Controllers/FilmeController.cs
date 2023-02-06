using AutoMapper;
using FilmeApi.Data;
using FilmeApi.Data.DTOS;
using FilmeApi.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmeApi.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {

            try
            {
                Filme filme = _mapper.Map<Filme>(filmeDto);
                _context.Filmes.Add(filme);
                _context.SaveChanges();
                return CreatedAtAction(nameof(FindById), new { id = filme.Id }, filme);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id) {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound("Id Não localizado");
            }
                return Ok(filme);   
        }


        [HttpGet]
        public IEnumerable<Filme> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 30)
        {
            return _context.Filmes.Skip(skip).Take(take);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto filmedto)
        {
            var filmeold = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filmeold == null) return NotFound("Nenhum id foi encontrado para o update");
            _mapper.Map(filmedto, filmeold);
            _context.SaveChanges();
            return NoContent();
             
                
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateFilmePatch(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filmeold = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filmeold == null) return NotFound("Nenhum id foi encontrado para o update");

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filmeold);

            patch.ApplyTo(filmeParaAtualizar, ModelState);
            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(filmeParaAtualizar, filmeold);
            _context.SaveChanges();
            return NoContent();


        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Nenhum id foi encontrado para deletar");

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();

    }
}
