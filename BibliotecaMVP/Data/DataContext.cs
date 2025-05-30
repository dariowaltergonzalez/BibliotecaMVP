using BibliotecaMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVP.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}