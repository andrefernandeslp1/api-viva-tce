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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FornecedoresController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FornecedorDTO>>> Get()
        {
            var fornecedor = await _uow.FornecedorRepository.GetAllAsync();
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
            var fornecedor = await _uow.FornecedorRepository.GetAsync(p => p.Id == id);
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
            var novoFornecedor = await _uow.FornecedorRepository.CreateAsync(fornecedor);
            _uow.Commit();

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

            var existeFornecedor = await _uow.FornecedorRepository.GetAsync(p => p.Id == id);
            
            if (existeFornecedor == null)
                return NotFound("Fornecedor não encontrado.");

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
            var fornecedorAtualizado = await _uow.FornecedorRepository.UpdateAsync(fornecedor);
            _uow.Commit();

            var fornecedorAtualizadoDTO = _mapper.Map<FornecedorDTO>(fornecedorAtualizado);

            return Ok(fornecedorAtualizadoDTO);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FornecedorDTO>> Delete(int id)
        {
            var fornecedor = await _uow.FornecedorRepository.GetAsync(p => p.Id == id);
            if (fornecedor is null)
                return NotFound("Usuário não encontrado");
                
            var fornecedorDeletado = await _uow.FornecedorRepository.DeleteAsync(fornecedor);
            _uow.Commit();

            var fornecedorDeletadoDTO = _mapper.Map<FornecedorDTO>(fornecedorDeletado);

            return Ok(fornecedorDeletadoDTO);
        }



     }
}