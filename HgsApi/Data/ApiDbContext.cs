using Microsoft.EntityFrameworkCore;
using HgsApi.Models;

namespace HgsApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<HgsInfo> HgsInfos { get; set; }
    }
}
