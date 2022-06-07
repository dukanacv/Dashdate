using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface ILikeRepository
    {
        Task<Like> GetUserLike(int likingUserId, int likedUserId);
        Task<User> GetUserWithLikes(int UserId);
        Task<IEnumerable<LikeDTO>> GetUserLikes(string whatWeNeed, int UserId);
    }
}