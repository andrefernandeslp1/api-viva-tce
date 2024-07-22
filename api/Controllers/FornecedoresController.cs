using API.Context;
using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace API.Controllers
{    
    [Route("[controller]")]
    [ApiController]
     public class FornecedoresController: ControllerBase
     {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public FornecedoresController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FornecedorDTO>>> Get()
        {
            var fornecedor = await _uof.FornecedorRepository.GetAllAsync();
            if (fornecedor is null)
            {
                return NotFound();
            }
            var fornecedoresDTO = _mapper.Map<List<FornecedorDTO>>(fornecedor);
            return Ok(fornecedoresDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FornecedorDTO>> Get(int id)
        {
            var fornecedor = await _uof.FornecedorRepository.GetAsync(p => p.Id == id);
            if (fornecedor is null)
            {
                return NotFound();
            }
            var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);
            return Ok(fornecedorDTO);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> Post(FornecedorDTO fornecedorDTO)
        {
            if (fornecedorDTO is null)
            {
            return BadRequest();
            }

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
            var novoFornecedor = await _uof.FornecedorRepository.CreateAsync(fornecedor);
            _uof.Commit();

            var novoFornecedorDTO = _mapper.Map<FornecedorDTO>(novoFornecedor);
            return Ok(novoFornecedorDTO);

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FornecedorDTO>> Put (int id, FornecedorDTO fornecedorDTO)
        {

            if (fornecedorDTO == null)
                return BadRequest("O corpo da requisição não pode ser nulo.");
        
            if (id != fornecedorDTO.Id) 
                return BadRequest("Os ids fornecidos não são compatíveis");

            var existeFornecedor = await _uof.FornecedorRepository.GetAsync(p => p.Id == id);
            
            if (existeFornecedor == null)
                return NotFound("Fornecedor não encontrado.");

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
            var fornecedorAtualizado = await _uof.FornecedorRepository.UpdateAsync(fornecedor);
            _uof.Commit();

            var fornecedorAtualizadoDTO = _mapper.Map<FornecedorDTO>(fornecedorAtualizado);

            return Ok(fornecedorAtualizadoDTO);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FornecedorDTO>> Delete(int id)
        {
            var fornecedor = await _uof.FornecedorRepository.GetAsync(p => p.Id == id);
            if (fornecedor is null)
                return NotFound("Usuário não encontrado");
                
            var fornecedorDeletado = await _uof.FornecedorRepository.DeleteAsync(fornecedor);
            _uof.Commit();

            var fornecedorDeletadoDTO = _mapper.Map<FornecedorDTO>(fornecedorDeletado);

            return Ok(fornecedorDeletadoDTO);
        }



     }
}