using BibliotecaMVP.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVP.Services
{
    public class LoginService(DataContext context)
    {
        private readonly DataContext _context = context;

        public bool IsAuthenticated { get; private set; } = false;

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == email);

            if (user == null)
            {
                IsAuthenticated = false;
                return IsAuthenticated;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user!.Password))
            {
                IsAuthenticated = true;
            }
            else
            {
                IsAuthenticated = false;
            }
            return IsAuthenticated;
        }
    }
}