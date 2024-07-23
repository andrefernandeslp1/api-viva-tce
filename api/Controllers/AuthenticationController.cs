
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUnitOfWork _uow;
        public AuthenticationController(TokenService tokenService, IUnitOfWork uow)
        {
            _tokenService = tokenService;
            _uow = uow;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _uow.UsuarioRepository.GetAsync(u => u.Email == request.Email && u.Senha == request.Senha);

        if (usuario == null)
            return Unauthorized();

        var token = _tokenService.GenerateToken(usuario.Email);

        return Ok(new { Token = token });


        }
      
         
    }
}