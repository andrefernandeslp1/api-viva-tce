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
     public class ServicosUsuariosController : ControllerBase
     {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ServicosUsuariosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServicoUsuarioGetDTO>>> Get()
        {
            var servicosUsuarios = await _uof.ServicoUsuarioRepository.GetAllWithDataAsync();
            if (servicosUsuarios is null)
            {
                return NotFound();
            }
            var servicoUsuariosDTO = _mapper.Map<List<ServicoUsuarioGetDTO>>(servicosUsuarios);
            return Ok(servicoUsuariosDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicoUsuarioGetDTO>> Get(int id)
        {
            var servicoUsuario = await _uof.ServicoUsuarioRepository.GetByIdWithDataAsync(id);
            if (servicoUsuario is null)
            {
                return NotFound();
            }
            var servicoUsuarioDTO = _mapper.Map<ServicoUsuarioGetDTO>(servicoUsuario);
            return Ok(servicoUsuarioDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ServicoUsuarioPostDTO>> Post(ServicoUsuarioPostDTO servicoUsuarioDTO)
        {
            if (servicoUsuarioDTO is null)
            {
            return BadRequest();
            }

            var servicoUsuario = _mapper.Map<ServicoUsuario>(servicoUsuarioDTO);
            var novoServicoUsuario = await _uof.ServicoUsuarioRepository.CreateAsync(servicoUsuario);
            _uof.Commit();

            var novoServicoDTO = _mapper.Map<ServicoUsuarioPostDTO>(novoServicoUsuario);
            return Ok(novoServicoDTO);

        }

        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ServicoUsuarioPostDTO>> Put (int id, ServicoUsuarioPostDTO servicoUsuarioPostDTO)
        {
            if (id != servicoUsuarioPostDTO.Id)
            return BadRequest();

            var servicoUsuario = _mapper.Map<ServicoUsuario>(servicoUsuarioPostDTO);
            var servicoUsuarioAtualizado = await _uof.ServicoUsuarioRepository.UpdateAsync(servicoUsuario);
            _uof.Commit();

            var servicoUsuarioAtualizadoDTO = _mapper.Map<ServicoUsuarioPostDTO>(servicoUsuarioAtualizado);
            return Ok(servicoUsuarioAtualizadoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServicoUsuarioGetDTO>> Delete(int id)
        {
            var servicoUsuario = await _uof.ServicoUsuarioRepository.GetByIdWithDataAsync(id);
            if (servicoUsuario is null)
            {
                return NotFound("Serviço-Usuário não encontrado");
            }
            var servicoUsuarioDeletado = await _uof.ServicoUsuarioRepository.DeleteAsync(servicoUsuario);
            _uof.Commit();

            var servicoUsuarioDeletadoDTO = _mapper.Map<ServicoUsuarioGetDTO>(servicoUsuarioDeletado);

            return Ok(servicoUsuarioDeletadoDTO);
        }

    
        
     }
}