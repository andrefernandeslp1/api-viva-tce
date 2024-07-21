using API.Context;
using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
     public class ServicosController : ControllerBase 
     {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ServicosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ServicoGetDTO>>> Get()
        {
            var servicos = await _uof.ServicoRepository.GetAllWithFornecedoresAsync();
            if (servicos is null)
            {
                return NotFound();
            }
            var servicosDTO = _mapper.Map<List<ServicoGetDTO>>(servicos);
            return Ok(servicosDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicoGetDTO>> Get(int id)
        {
            var servico = await _uof.ServicoRepository.GetbyIdWithFornecedoresAsync(id);
            if (servico is null)
            {
                return NotFound();
            }
            var servicoDTO = _mapper.Map<ServicoGetDTO>(servico);
            return Ok(servicoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ServicoPostDTO>> Post(ServicoPostDTO servicoDTO)
        {
            if (servicoDTO is null)
            {
            return BadRequest();
            }

            var servico = _mapper.Map<Servico>(servicoDTO);
            var novoServico = await _uof.ServicoRepository.CreateAsync(servico);
            _uof.Commit();

            var novoServicoDTO = _mapper.Map<ServicoPostDTO>(novoServico);
            return Ok(novoServicoDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServicoPostDTO>> Put (int id, ServicoPostDTO servicoPostDTO)
        {
            if (id != servicoPostDTO.Id)
            return BadRequest();

            var servico = _mapper.Map<Servico>(servicoPostDTO);
            var servicoAtualizado = await _uof.ServicoRepository.UpdateAsync(servico);
            _uof.Commit();

            var servicoAtualizadoDTO = _mapper.Map<ServicoPostDTO>(servicoAtualizado);
            return Ok(servicoAtualizadoDTO);
        }

        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServicoGetDTO>> Delete(int id)
        {
            var servico = await _uof.ServicoRepository.GetbyIdWithFornecedoresAsync(id);
            if (servico is null)
            {
                return NotFound("Serviço não encontrado");
            }
            var servicoDeletado = await _uof.ServicoRepository.DeleteAsync(servico);
            _uof.Commit();

            var servicoDeletadoDTO = _mapper.Map<ServicoGetDTO>(servicoDeletado);

            return Ok(servicoDeletadoDTO);
        }






     }
     
}