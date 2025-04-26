using DatabaseContext.DBHelper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatabaseContext.DBHelper.Methods
{
    public class DbHelper : DbContext
    {
        public DbSet<CatEntity> Cats { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<CatTag> CatTags { get; set; }

        public DbHelper(DbContextOptions<DbHelper> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatTag>()
                .HasKey(ct => new { ct.CatId, ct.TagId });

            modelBuilder.Entity<CatTag>()
                .HasOne(ct => ct.Cat)
                .WithMany(c => c.CatTags)
                .HasForeignKey(ct => ct.CatId);

            modelBuilder.Entity<CatTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.CatTags)
                .HasForeignKey(ct => ct.TagId);
        }
    }
}
