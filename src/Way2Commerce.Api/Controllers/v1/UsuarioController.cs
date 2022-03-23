using Microsoft.AspNetCore.Mvc;
using Way2Commerce.Api.Controllers.Shared;
using Way2Commerce.Application.DTOs.Request;
using Way2Commerce.Application.DTOs.Response;
using Way2Commerce.Application.Interfaces.Services;

namespace Way2Commerce.Api.Controllers.v1;

[ApiVersion("1.0")]
public class UsuarioController : ApiControllerBase
{
    private IIdentityService _identityService;

    public UsuarioController(IIdentityService identityService) =>
        _identityService = identityService;

    [HttpPost("cadastro")]
    public async Task<ActionResult<UsuarioCadastroResponse>> Cadastrar(UsuarioCadastroRequest usuarioCadastro)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var resultado = await _identityService.CadastrarUsuario(usuarioCadastro);
        if (resultado.Sucesso)
            return Ok(resultado);
        else if (resultado.Erros.Count > 0)
            return BadRequest(resultado);
        
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioCadastroResponse>> Login(UsuarioLoginRequest usuarioLogin)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var resultado = await _identityService.Login(usuarioLogin);
        if (resultado.Sucesso)
            return Ok(resultado);
        
        return Unauthorized(resultado);
    }
}