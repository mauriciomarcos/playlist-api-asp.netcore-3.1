using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Playlist.API.Domain.Models;

namespace Playlist.API.Data.Mapping
{
    public class VideoMapping : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Video");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            builder.Property(e => e.NomeCanal)
                .HasColumnType("nvarchar(250)");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property(e => e.DataViualizacao);

            builder.Property(e => e.LinkVideo)
                .HasMaxLength(1000);

        }
    }
}