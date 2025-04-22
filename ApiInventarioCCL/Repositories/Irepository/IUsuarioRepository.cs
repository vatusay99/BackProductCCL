using ApiInventarioCCL.Models;
using ApiInventarioCCL.Models.Dtos;

namespace ApiInventarioCCL.Repositories.Irepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuarioById(int id);
        bool IsUniqueUser(string user);
        
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);


    }
}
