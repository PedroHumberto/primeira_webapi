
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt){

        }
        protected override void OnModelCreating(ModelBuilder builder){
            
            // metodo 1 pra 1
            builder.Entity<Endereco>()
            .HasOne(endereco => endereco.Cinema)
            .WithOne(cinema => cinema.Endereco)
            .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);

            //metodo de 1 -> para varios
            builder.Entity<Cinema>()
            .HasOne(cinema => cinema.Gerente)
            .WithMany(gerente => gerente.Cinemas)
            .HasForeignKey(cinema => cinema.GerenteId);

        }


        public DbSet<Filme> Filmes {get; set;}
        public DbSet<Cinema> Cinemas {get; set;}
        public DbSet<Endereco> Enderecos {get; set;}
        public DbSet<Gerente> Gerentes {get; set;}


    }

}
