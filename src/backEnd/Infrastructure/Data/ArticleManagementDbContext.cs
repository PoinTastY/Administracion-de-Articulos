using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ArticleManagementDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public Microsoft.EntityFrameworkCore.DbSet<ExtendRequest> ExtendRequests { get; set; } = null!;
    public Microsoft.EntityFrameworkCore.DbSet<Student> Students { get; set; } = null!;

    public ArticleManagementDbContext(DbContextOptions<ArticleManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExtendRequest>()
            .HasOne(er => er.Student)
            .WithMany(s => s.ExtendRequests)
            .HasForeignKey(er => er.StudentCode)
            .HasPrincipalKey(s => s.StudentCode); // Because StudentCode is unique, not PK
    }

}
