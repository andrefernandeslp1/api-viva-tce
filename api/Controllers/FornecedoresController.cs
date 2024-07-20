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
        public async Task<ActionResult<List<FornecedorDTO>>> Get()
        {
            var fornecedor = _uof.FornecedorRepository.GetAll();
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
            var fornecedor = _uof.FornecedorRepository.Get(user => user.Id == id);
            if (fornecedor is null)
            {
                return NotFound();
            }
            var fornecedorDTO = _mapper.Map<FornecedorDTO>(fornecedor);
            return Ok(fornecedorDTO);
        }

        [HttpPost]
        public ActionResult<FornecedorDTO> Post(FornecedorDTO fornecedorDTO)
        {
            if (fornecedorDTO is null)
            {
            return BadRequest();
            }

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
            var novoFornecedor = _uof.FornecedorRepository.Create(fornecedor);
            _uof.Commit();

            var novoFornecedorDTO = _mapper.Map<FornecedorDTO>(novoFornecedor);
            return Ok(novoFornecedorDTO);

        }
        [HttpPut("{id:int}")]
        public ActionResult<FornecedorDTO> Put (int id, FornecedorDTO fornecedorDTO)
        {
            if (id != fornecedorDTO.Id)
            return BadRequest();//400

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
            var fornecedorAtualizado = _uof.FornecedorRepository.Update(fornecedor);
            _uof.Commit();

            var fornecedorAtualizadoDTO = _mapper.Map<FornecedorDTO>(fornecedorAtualizado);

            return Ok(fornecedorAtualizadoDTO);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<FornecedorDTO> Delete(int id)
        {
            var fornecedor = _uof.FornecedorRepository.Get(user => user.Id == id);
            if (fornecedor is null)
            {
                return NotFound("Usuário não encontrado");
            }
            var fornecedorDeletado = _uof.FornecedorRepository.Delete(fornecedor);
            _uof.Commit();

            var fornecedorDeletadoDTO = _mapper.Map<FornecedorDTO>(fornecedorDeletado);

            return Ok(fornecedorDeletadoDTO);
        }



     }
}