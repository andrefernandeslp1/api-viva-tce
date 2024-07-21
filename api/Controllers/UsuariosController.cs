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
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public UsuariosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()
        {
            var usuarios = await _uof.UsuarioRepository.GetAllAsync();
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
            var usuario = await _uof.UsuarioRepository.GetAsync(user => user.Id == id);
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
            var novoUsuario = await _uof.UsuarioRepository.CreateAsync(usuario);
            _uof.Commit();

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

            var existeUsuário = await _uof.UsuarioRepository.GetAsync(p => p.Id == id);
            
            if (existeUsuário == null)
                return NotFound("Usuário não encontrado.");

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioAtualizado = await _uof.UsuarioRepository.UpdateAsync(usuario);
            _uof.Commit();

            var usuarioAtualizadoDTO = _mapper.Map<UsuarioDTO>(usuarioAtualizado);

            return Ok(usuarioAtualizadoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> Delete(int id)
        {
            var usuario = await _uof.UsuarioRepository.GetAsync(user => user.Id == id);
            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }
            var usuarioDeletado = await _uof.UsuarioRepository.DeleteAsync(usuario);
            _uof.Commit();

            var usuarioDeletadoDto = _mapper.Map<UsuarioDTO>(usuarioDeletado);

            return Ok(usuarioDeletadoDto);
        }



    }

}