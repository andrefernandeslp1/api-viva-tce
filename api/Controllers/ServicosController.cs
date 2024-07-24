using API.Context;
using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
     public class ServicosController : ControllerBase 
     {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ServicosController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ServicoGetDTO>>> Get()
        {
            var servicos = await _uow.ServicoRepository.GetAllWithFornecedoresAsync();
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
            var servico = await _uow.ServicoRepository.GetbyIdWithFornecedoresAsync(id);
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
            var novoServico = await _uow.ServicoRepository.CreateAsync(servico);
            _uow.Commit();

            var novoServicoDTO = _mapper.Map<ServicoPostDTO>(novoServico);
            return Ok(novoServicoDTO);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServicoPostDTO>> Put (int id, ServicoPostDTO servicoPostDTO)
        {
            if (servicoPostDTO == null)
                return BadRequest("O corpo da requisição não pode ser nulo.");
        
            if (id != servicoPostDTO.Id) 
                return BadRequest("Os ids fornecidos não são compatíveis");

            var existeServico = await _uow.ServicoRepository.GetAsync(p => p.Id == id);
            
            if (existeServico == null)
                return NotFound("Serviço não encontrado.");

            var servico = _mapper.Map<Servico>(servicoPostDTO);
            var servicoAtualizado = await _uow.ServicoRepository.UpdateAsync(servico);
            _uow.Commit();

            var servicoAtualizadoDTO = _mapper.Map<ServicoPostDTO>(servicoAtualizado);
            return Ok(servicoAtualizadoDTO);
        }

        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServicoGetDTO>> Delete(int id)
        {
            var servico = await _uow.ServicoRepository.GetbyIdWithFornecedoresAsync(id);
            if (servico is null)
            {
                return NotFound("Serviço não encontrado");
            }
            var servicoDeletado = await _uow.ServicoRepository.DeleteAsync(servico);
            _uow.Commit();

            var servicoDeletadoDTO = _mapper.Map<ServicoGetDTO>(servicoDeletado);

            return Ok(servicoDeletadoDTO);
        }

        [HttpGet("compras/{id:int}")]
        public async Task<ActionResult<List<ServicoGetDTO>>> GetServicosPorFornecedor (int id)
        {
            var servico = await _uow.ServicoRepository.GetServicosByFornecedorId(id);

            if (servico is null)
                return NotFound();

            var servicoDTO = _mapper.Map<List<ServicoGetDTO>>(servico);

            return Ok(servicoDTO);
            
        }






     }
     
}