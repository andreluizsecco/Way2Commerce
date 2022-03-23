using Way2Commerce.Application.DTOs.Request;
using Way2Commerce.Application.DTOs.Response;

namespace Way2Commerce.Application.Interfaces.Services;

public interface IIdentityService
{
    Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
    Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
}