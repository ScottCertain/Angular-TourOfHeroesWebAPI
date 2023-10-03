using Microsoft.EntityFrameworkCore;
using TourOfHeroesWebApi.Models;

namespace TourOfHeroesWebApi.Data
{
    public class HeroesContext : DbContext
    {
        public HeroesContext(DbContextOptions<HeroesContext> options) : base(options)
        {
        }

        public DbSet<Hero> Heroes { get; set; }
    }
}
