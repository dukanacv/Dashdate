using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        //dont need photo DbSet bcs it will only add to users collection of photos

        public DbSet<Like> Likes { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)//CONFIGURATION
        {
            base.OnModelCreating(builder);

            builder.Entity<Like>().HasKey(key => new { key.LikingUserId, key.LikedUserId });

            builder.Entity<Like>()
                .HasOne(l => l.LikingUser)
                .WithMany(l => l.WhatILiked)
                .HasForeignKey(l => l.LikingUserId)
                .OnDelete(DeleteBehavior.Cascade);//if delete user delete related entities

            builder.Entity<Like>()
                .HasOne(l => l.LikedUser)
                .WithMany(l => l.WhoLiked)
                .HasForeignKey(l => l.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);





            builder.Entity<Message>()
                .HasOne(u => u.Receiver)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}