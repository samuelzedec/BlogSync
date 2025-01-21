using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mapping;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_Post_Id");
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

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
            .Property(x => x.Slug)
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();
        builder
            .HasIndex(x => x.Slug)
            .IsUnique()
            .HasDatabaseName("IX_Post_Slug");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder
            .Property(x => x.ModifiedAt)
            .HasColumnName("ModifiedAt")
            .HasColumnType("DATETIME");

        builder
            .HasMany(x => x.Comments)
            .WithOne(y => y.Post)
            .HasForeignKey("PostId")
            .HasConstraintName("FK_Post_PostId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Tags)
            .WithMany(y => y.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "PostTag",
                tag => tag
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .HasConstraintName("FK_PostTag_TagId")
                    .OnDelete(DeleteBehavior.Cascade),
                post => post
                    .HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("PostId")
                    .HasConstraintName("FK_PostTag_PostId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}