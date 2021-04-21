using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playlist.API.Domain.Models;

namespace Playlist.API.Data.Mapping
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            builder.HasMany(e => e.Videos)
                .WithOne(video => video.Categoria)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();
        }
    }
}