
using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public AuthenticationController(TokenService tokenService, IUnitOfWork uow, IMapper mapper)
        {
            _tokenService = tokenService;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _uow.UsuarioRepository.GetAsync(u => u.Email == request.Email && u.Senha == request.Senha);

        if (usuario == null)
            return BadRequest("E-mail ou senha inválidos.");

        var token = _tokenService.GenerateToken(usuario.Email, usuario.Role, usuario.Id, usuario.Nome);

        return Ok(new { Token = token });


        }

        [HttpPost, Route("cadastrar")]
        public async Task<ActionResult<UsuarioPostDTO>> Cadastrar(UsuarioPostDTO usuarioDTO)
        {

            if (usuarioDTO is null){
                return BadRequest();
            }

            if(!usuarioDTO.Role.Equals("cliente"))
            {
                return Unauthorized();
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
      
         
    }
}