using Microsoft.AspNetCore.Mvc;
using ServiceSoap.Dto;
using ServiceSoap.Interface;
using ServiceSoap.Models;
using System.Net.Mime;

namespace ServiceSoap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioService;

        public UsuarioController(IUsuario usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtém todos os usuários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _usuarioService.ReadAll();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtém um usuário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            try
            {
                var usuario = await _usuarioService.Read(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Usuário com ID {id} não encontrado");
            }
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario usuario)
        {
            try
            {
                var createdUsuario = await _usuarioService.Create(usuario);
                return CreatedAtAction(nameof(Get), new { id = createdUsuario.Id }, createdUsuario);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage }));
            }
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioDto>> Update(int id, [FromBody] UsuarioDto usuario)
        {
            if (id != usuario.Id)
                return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição");

            try
            {
                var updatedUsuario = await _usuarioService.Update(usuario);
                return Ok(updatedUsuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Usuário com ID {id} não encontrado");
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage }));
            }
        }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioService.Delete(id);
            if (!result)
                return NotFound($"Usuário com ID {id} não encontrado");

            return NoContent();
        }
    }
}
