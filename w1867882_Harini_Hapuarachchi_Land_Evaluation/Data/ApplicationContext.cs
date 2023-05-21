using Microsoft.EntityFrameworkCore;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
        { 

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<TeaLand> TeaLands { get; set; }
        public DbSet<CoconutLand> CoconutLands { get; set; }
        public DbSet<RubberLand> RubberLands { get; set; }
    }
}
