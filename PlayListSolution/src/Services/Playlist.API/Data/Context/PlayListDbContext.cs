using Microsoft.EntityFrameworkCore;
using Playlist.API.Data.Mapping;
using Playlist.API.Domain.Models;
using System.Linq;

namespace Playlist.API.Data.Context
{
    public class PlayListDbContext : DbContext
    {
        private static bool PrimeiraExecucao = true;

        public PlayListDbContext(DbContextOptions<PlayListDbContext> options) 
            : base(options)
        {
            if (PrimeiraExecucao)
            {
                Seed();
                PrimeiraExecucao = false;
            }                
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * Realizando a configuração dessa forma, não é necessário informar todos as 
             * class de Mapping que for configurada para a aplicação, como por exemplo: modelBuilder.ApplyConfiguration(new ClienteMap());
             * 
             * Utilizando o modelBuilder.ApplyConfigurationsFromAssembly e informando uma classe
             * que está contida dentro do assembly das classes de configuração, o Entity Framework Core
             * realizará um verificação no warm-up da aplicação e aplicará a configuração para todas as 
             * classes que implementarem a interface IEntityTypeConfiguration
             */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoMapping).Assembly);

            // Chamada para o método de configuração default
            DefautMappingConfiguration(modelBuilder);            
        }

        private void DefautMappingConfiguration(ModelBuilder modelBuilder)
        {
            // Iterando a lista de Entidades da Aplicação
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Obter todas as propriedades (IEnumerable) da Entidade corrente do tipo string
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                // Iterando cada uma das propriedade para validar se a mesma foi confugurada ou se devemos adicionar o valor Default
                foreach (var propertyValue in properties)
                {
                    //Verifica se a propriedade possui um valor já configurado e se não tem nenhum tamanho definido                     
                    if (string.IsNullOrEmpty(propertyValue.GetColumnType()) && !propertyValue.GetMaxLength().HasValue)
                    {
                        // Definindo o tamanho default para os campos do tipo string
                        propertyValue.SetColumnType("varchar(100)");
                    }
                }
            }
        }

        private async void Seed()
        {
            var categorias = new Categoria[] { 
                new Categoria
                {
                    Id = new System.Guid(),
                    Nome = "Técnicos - Linguagens de Programação"
                },
                new Categoria
                {
                    Id = new System.Guid(),
                    Nome = "Técnicos - Banco de Dados"
                },
                new Categoria
                {
                    Id = new System.Guid(),
                    Nome = "Entreterimento"
                },
                new Categoria
                {
                    Id = new System.Guid(),
                    Nome = "Diversos"
                },
            };

            Categorias.AddRange(categorias);
            await base.SaveChangesAsync();

           //await Videos.Add(new Video
           // {
           //     Id = new System.Guid(),
           //     Nome = "Why you think you're right even if you're wrong",
           //     NomeCanal = "TED",
           //     DataCadastro = System.DateTime.Now,
           //     LinkVideo = "https://www.youtube.com/embed/w4RLfVxTGH4",
           //     Visualizado = false,
           //     Categoria = null
           //})
           //.Context.SaveChangesAsync();
        }
    }
}