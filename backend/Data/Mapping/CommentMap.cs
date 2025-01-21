using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mapping;

public class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasKey(x => x.Id)
            .HasName("PK_Comment_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Content)
            .HasColumnName("Content")
            .HasColumnType("NVARCHAR(MAX)")
            .IsRequired();

        builder
            .Property(x => x.AuthorName)
            .HasColumnName("AuthorName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();
        builder
            .HasIndex(x => x.AuthorName)
            .HasDatabaseName("IX_Comment_AuthorName");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder
            .Property(x => x.ModifiedAt)
            .HasColumnName("ModifiedAt")
            .HasColumnType("DATETIME");
    }
}