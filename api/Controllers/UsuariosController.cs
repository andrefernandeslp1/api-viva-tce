using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminPolicy")]        
        public async Task<ActionResult<List<UsuarioGetDTO>>> Get()
        {
            var usuarios = await _uow.UsuarioRepository.GetAllWithFornecedoresAsync();
            if (usuarios is null)
            {
                return NotFound();
            }
            var usuariosDTO = _mapper.Map<List<UsuarioGetDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        [HttpGet("{id:int}")]
        [Authorize]    
        public async Task<ActionResult<UsuarioGetDTO>> Get(int id)
        {

            var usuario = await _uow.UsuarioRepository.GetByIdWithFornecedorAsync(id);
            if (usuario is null)
            {
                return NotFound();
            }
            var usuarioDTO = _mapper.Map<UsuarioGetDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<UsuarioPostDTO>> Post(UsuarioPostDTO usuarioDTO)
        {
            if (usuarioDTO is null){
                return BadRequest();
            }

            var existingUser = await _uow.UsuarioRepository.GetAsync(u => u.Email == usuarioDTO.Email);
            if (existingUser != null)
            {
                return Conflict("E-mail já cadastrado.");
            }

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var novoUsuario = await _uow.UsuarioRepository.CreateAsync(usuario);
            _uow.Commit();

            var novoUsuarioDTO = _mapper.Map<UsuarioPostDTO>(novoUsuario);
            return Ok(novoUsuarioDTO);

        }
        
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<UsuarioPostDTO>> Put (int id, UsuarioPostDTO usuarioDTO)
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

            var usuarioAtualizadoDTO = _mapper.Map<UsuarioPostDTO>(usuarioAtualizado);

            return Ok(usuarioAtualizadoDTO);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<UsuarioGetDTO>> Delete(int id)
        {
            var usuario = await _uow.UsuarioRepository.GetAsync(user => user.Id == id);
            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }
            var usuarioDeletado = await _uow.UsuarioRepository.DeleteAsync(usuario);
            _uow.Commit();

            var usuarioDeletadoDto = _mapper.Map<UsuarioGetDTO>(usuarioDeletado);

            return Ok(usuarioDeletadoDto);
        }

        [HttpGet("pagination")]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<List<UsuarioGetDTO>> Get([FromQuery] PaginationParameters paginationParameters)
        {
            var usuarios = _uow.UsuarioRepository.GetUsuariosPaginados(paginationParameters);

            var usuarioDTOs = _mapper.Map<List<UsuarioGetDTO>>(usuarios);
            return Ok(usuarioDTOs);
        }

        [HttpGet("filter/nome/pagination")]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<IEnumerable<UsuarioGetDTO>> GetUsuariosFilterNome([FromQuery] NomeFilter nomeFilter)
        {
            var usuarios = _uow.UsuarioRepository.GetUsuariosFiltroNome(nomeFilter);
            return ObterServicos(usuarios);
        }

        private ActionResult<IEnumerable<UsuarioGetDTO>> ObterServicos(PagedList<Usuario> usuarios)
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
        

            var usuarioDTOs = _mapper.Map<IEnumerable<UsuarioGetDTO>>(usuarios);
            return Ok(usuarioDTOs);

        }



    }

}