using Microsoft.EntityFrameworkCore;

namespace Cine.Repository;

public class CineDbContext(DbContextOptions<CineDbContext> options) : AppDbContext(options)
{
}
