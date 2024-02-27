using DatingAppProCardoAI.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace DatingAppProCardoAI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Image> Image { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<MatchProfile> MatchProfile { get; set; }

        public DbSet<Friendships> Friendships { get; set; }
        public DbSet<UserFriendship> UserFriendship { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

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

            modelBuilder.Entity<UserFriendship>()
                .HasKey(uf => new { uf.UserId, uf.FriendshipsId });

            modelBuilder.Entity<UserFriendship>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFriendships)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFriendship>()
                .HasOne(uf => uf.Friendships)
                .WithMany(f => f.UserFriendships)
                .HasForeignKey(uf => uf.FriendshipsId);


        }


    }
}
                