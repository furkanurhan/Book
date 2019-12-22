using Entities.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.BookDbContext
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() { }
        public BookDbContext(DbContextOptions options)
        : base(options) { }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("Book");
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.Title).HasColumnName("Title");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.PublishedDate).HasColumnName("PublishedDate");
                entity.Property(e => e.AuthorID).HasColumnName("AuthorID");
            });
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("Author");
                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("Name");
            });
        }
    }
}
