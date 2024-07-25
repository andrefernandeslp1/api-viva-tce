using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UsuariosController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()
        {
            var usuarios = await _uow.UsuarioRepository.GetAllAsync();
            if (usuarios is null)
            {
                return NotFound();
            }
            var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuario = await _uow.UsuarioRepository.GetAsync(user => user.Id == id);
            if (usuario is null)
            {
                return NotFound();
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Post(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO is null){
                return BadRequest();
            }

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var novoUsuario = await _uow.UsuarioRepository.CreateAsync(usuario);
            _uow.Commit();

            var novoUsuarioDTO = _mapper.Map<UsuarioDTO>(novoUsuario);
            return Ok(novoUsuarioDTO);

        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> Put (int id, UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                return BadRequest("O corpo da requisição não pode ser nulo.");
        
            if (id != usuarioDTO.Id) 
                return BadRequest("Os ids fornecidos não são compatíveis");

            var existeUsuário = await _uow.UsuarioRepository.GetAsync(p => p.Id == id);
            
            if (existeUsuário == null)
                return NotFound("Usuário não encontrado.");

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioAtualizado = await _uow.UsuarioRepository.UpdateAsync(usuario);
            _uow.Commit();

            var usuarioAtualizadoDTO = _mapper.Map<UsuarioDTO>(usuarioAtualizado);

            return Ok(usuarioAtualizadoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> Delete(int id)
        {
            var usuario = await _uow.UsuarioRepository.GetAsync(user => user.Id == id);
            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }
            var usuarioDeletado = await _uow.UsuarioRepository.DeleteAsync(usuario);
            _uow.Commit();

            var usuarioDeletadoDto = _mapper.Map<UsuarioDTO>(usuarioDeletado);

            return Ok(usuarioDeletadoDto);
        }

        [HttpGet("pagination")]
        public ActionResult<List<UsuarioDTO>> Get([FromQuery] PaginationParameters paginationParameters)
        {
            var usuarios = _uow.UsuarioRepository.GetUsuariosPaginados(paginationParameters);

            var usuarioDTOs = _mapper.Map<List<UsuarioDTO>>(usuarios);
            return Ok(usuarioDTOs);
        }

        [HttpGet("filter/nome/pagination")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUsuariosFilterNome([FromQuery] NomeFilter nomeFilter)
        {
            var usuarios = _uow.UsuarioRepository.GetUsuariosFiltroNome(nomeFilter);
            return ObterServicos(usuarios);
        }

        private ActionResult<IEnumerable<UsuarioDTO>> ObterServicos(PagedList<Usuario> usuarios)
        {
            var metadata = new
        {
            usuarios.TotalCount,
            usuarios.PageSize,
            usuarios.CurrentPage,
            usuarios.TotalPages,
            usuarios.HasNext,
            usuarios.HasPrevious
        };
        

            var usuarioDTOs = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuarioDTOs);

        }



    }

}