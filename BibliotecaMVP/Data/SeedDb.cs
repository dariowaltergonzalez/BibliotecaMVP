using BibliotecaMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVP.Data
{
    public class SeedDb(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriasAsync();
            await CheckUsersAsync("tecnologers", "tecnologershn@gmail.com", "Tecno.2025", "Administrador");
        }

        private async Task<Usuario> CheckUsersAsync(string nombre, string correo, string password, string perfil)
        {
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuarioExistente != null)
            {
                return usuarioExistente!;
            }

            Usuario usuario = new()
            {
                Correo = correo,
                Nombre = nombre,
                Perfil = perfil,
                Password = password,
            };

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        private async Task CheckCategoriasAsync()
        {
            if (!_context.Categorias.Any())
            {
                _context.Categorias.Add(new Categoria { Nombre = "Ciencia Ficción" });
                _context.Categorias.Add(new Categoria { Nombre = "Novela" });
                _context.Categorias.Add(new Categoria { Nombre = "Drama" });
                await _context.SaveChangesAsync();
            }
        }
    }
}