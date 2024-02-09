using DatingAppProCardoAI.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;


namespace DatingAppProCardoAI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Image> Image { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<MatchProfile> MatchProfile { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchProfile>()
               .HasOne(m => m.currentprofile)
               .WithMany()
               .HasForeignKey(m => m.CurrentProfileId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MatchProfile>()
                .HasOne(m => m.matchprofile)
                .WithMany()
                .HasForeignKey(m => m.MatchProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
                