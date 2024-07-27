using CleanArchitecture.Infrastructure.FileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.FileManager.Contexts;

public class FileManagerDbContext(DbContextOptions<FileManagerDbContext> options) : DbContext(options)
{
    public DbSet<FileEntity> Files { get; set; }
}
