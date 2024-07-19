using API.Context;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> Get()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> Get(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == id);

        return Ok(usuario);
    }

    [HttpPost]
    public ActionResult Post(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return Ok("Usuário criado com sucesso");

    }

    }

}