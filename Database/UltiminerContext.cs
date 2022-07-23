using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database {

    public class UltiminerContext : DbContext {

        public DbSet<User> Users {get; set;}

        public UltiminerContext(DbContextOptions<UltiminerContext> options) : base(options){}
    }
}