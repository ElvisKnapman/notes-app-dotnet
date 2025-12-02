using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Persistence.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired()
            .ValueGeneratedNever();
            //.ValueGeneratedOnAdd()
            //.HasDefaultValueSql("NEWID()");

        builder.Property(u => u.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Username)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(72)
            .IsUnicode(false) // Helps EF infer VARCHAR rather than NVARCHAR
            .IsRequired();

    }
}
