using ApiInventarioCCL.Data;
using ApiInventarioCCL.Models;
using ApiInventarioCCL.Models.Dtos;
using ApiInventarioCCL.Repositories.Irepository;
using XSystem.Security.Cryptography;

namespace ApiInventarioCCL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

       public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Usuario GetUsuarioById(int id)
        {
            return _db.Usuarios.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuarios.OrderBy(c => c.Id).ToList();
        }

        public bool IsUniqueUser(string user)
        {
            var usuarioDb = _db.Usuarios.FirstOrDefault(u => u.Email == user);
            if (usuarioDb == null) 
            {
                return true;
            }

            return false;
        }

        public Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptado = obtenerMD5(usuarioRegistroDto.Password);
            Usuario usuario = new Usuario()
            {
                Email = usuarioRegistroDto.Email,
                Password = usuarioRegistroDto.Password,
                Role = usuarioRegistroDto.Role,

            };

            _db.Add(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = passwordEncriptado;
            return usuario;
        }

        public static string obtenerMD5(string password)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            string resp="";
            for (int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }

            return resp;
        }
    }
}
