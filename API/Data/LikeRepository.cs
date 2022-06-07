using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly MyDbContext _context;
        public LikeRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Like> GetUserLike(int likingUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(likedUserId, likedUserId);//these 2 params are PK in Likes table
        }

        public async Task<IEnumerable<LikeDTO>> GetUserLikes(string whatIsNeeded, int userId)
        {
            var users = _context.Users.OrderBy(u => u.Username).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (whatIsNeeded == "liked")
            {
                likes = likes.Where(like => like.LikedUserId == userId);
                users = likes.Select(like => like.LikedUser);//get users from likes table
            }

            if (whatIsNeeded == "likedBy")
            {
                likes = likes.Where(like => like.LikingUserId == userId);
                users = likes.Select(like => like.LikingUser);//users that liked currently logged in user
            }

            return await users.Select(user => new LikeDTO
            {
                Id = user.Id,
                Username = user.Username,
                PhotoUrl = user.Photos.FirstOrDefault(photo => photo.IsMain).Url,
                City = user.City
            }).ToListAsync();//can use automapper aswell
        }

        public async Task<User> GetUserWithLikes(int userId)//get list of users that this user has liked
        {
            return await _context.Users
                .Include(u => u.WhoLiked)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}