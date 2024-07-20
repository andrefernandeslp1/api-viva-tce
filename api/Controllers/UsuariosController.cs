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
            var usuarios = _uof.UsuarioRepository.GetAll();
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
            var usuario = _uof.UsuarioRepository.Get(user => user.Id == id);
            if (usuario is null)
            {
                return NotFound();
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDTO);
        }

        [HttpPost]
        public ActionResult<UsuarioDTO> Post(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO is null)
            {
            return BadRequest();
            }

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var novoUsuario = _uof.UsuarioRepository.Create(usuario);
            _uof.Commit();

            var novoUsuarioDTO = _mapper.Map<UsuarioDTO>(novoUsuario);
            return Ok(novoUsuarioDTO);

        }
        
        [HttpPut("{id:int}")]
        public ActionResult<UsuarioDTO> Put (int id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            return BadRequest();//400

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            var usuarioAtualizado = _uof.UsuarioRepository.Update(usuario);
            _uof.Commit();

            var usuarioAtualizadoDTO = _mapper.Map<UsuarioDTO>(usuarioAtualizado);

            return Ok(usuarioAtualizadoDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<UsuarioDTO> Delete(int id)
        {
            var usuario = _uof.UsuarioRepository.Get(user => user.Id == id);
            if (usuario is null)
            {
                return NotFound("Usuário não encontrado");
            }
            var usuarioDeletado = _uof.UsuarioRepository.Delete(usuario);
            _uof.Commit();

            var usuarioDeletadoDto = _mapper.Map<UsuarioDTO>(usuarioDeletado);

            return Ok(usuarioDeletadoDto);
        }



    }

}