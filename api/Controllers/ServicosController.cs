
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize]
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
        [Authorize(Policy = "VendedorPolicy")]
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
        [Authorize(Policy = "VendedorPolicy")]
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
        [Authorize(Policy = "VendedorPolicy")]
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

        [HttpGet("fornecedor/{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<ServicoGetDTO>>> GetServicosPorFornecedor (int id)
        {
            var servico = await _uow.ServicoRepository.GetServicosByFornecedorId(id);

            if (servico is null)
                return NotFound();

            var servicoDTO = _mapper.Map<List<ServicoGetDTO>>(servico);

            return Ok(servicoDTO);
            
        }

        [HttpGet("pagination")]
        [Authorize]
        public ActionResult<List<ServicoGetDTO>> Get([FromQuery] PaginationParameters paginationParameters)
        {
            var servicos = _uow.ServicoRepository.GetServicosPaginados(paginationParameters);

            var servicosDTO = _mapper.Map<List<ServicoGetDTO>>(servicos);
            return Ok(servicosDTO);
        }

        [HttpGet("filter/nome/pagination")]
        [Authorize]
        public ActionResult<IEnumerable<ServicoGetDTO>> GetServicosFilterNome([FromQuery] NomeFilter nomeFilter)
        {
            var servicos = _uow.ServicoRepository.GetServicosFiltroNome(nomeFilter);
            return ObterServicos(servicos);
        }

        private ActionResult<IEnumerable<ServicoGetDTO>> ObterServicos(PagedList<Servico> servicos)
        {
            var metadata = new
        {
            servicos.TotalCount,
            servicos.PageSize,
            servicos.CurrentPage,
            servicos.TotalPages,
            servicos.HasNext,
            servicos.HasPrevious
        };
        
            var servicoDTO = _mapper.Map<IEnumerable<ServicoGetDTO>>(servicos);
            return Ok(servicoDTO);

        }








     }
     
}