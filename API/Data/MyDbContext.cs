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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Like>().HasKey(key => new { key.LikingUserId, key.LikedUserId });

            builder.Entity<Like>()
                .HasOne(liking => liking.LikingUser)
                .WithMany(liked => liked.WhatILiked)
                .HasForeignKey(liking => liking.LikingUserId)
                .OnDelete(DeleteBehavior.Cascade);//if delete user delete related entities

            builder.Entity<Like>()
                .HasOne(liking => liking.LikedUser)
                .WithMany(liked => liked.WhoLiked)
                .HasForeignKey(liking => liking.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}