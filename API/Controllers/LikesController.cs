using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]//we need user to be logged in, in order to like smth
    [ServiceFilter(typeof(LogUserActivity))]
    public class LikesController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;
        public LikesController(IUserRepository userRepository, ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var likingUserId = User.GetUserId();

            var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            var likingUser = await _likeRepository.GetUserWithLikes(likingUserId);

            if (likedUser == null)
            {
                return NotFound();
            }

            if (likingUser.Username == username)
            {
                BadRequest("Dont like yourself!:D");
            }

            var userLike = await _likeRepository.GetUserLike(likingUserId, likedUser.Id);//check if you already liked this user
            if (userLike != null)
            {
                return BadRequest("You have liked this user already.");
            }

            userLike = new Like
            {
                LikingUserId = likingUserId,
                LikedUserId = likedUser.Id
            };

            likingUser.WhatILiked.Add(userLike);

            if (await _userRepository.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest("Cannot like user. There was a problem");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDTO>>> GetUserLikes(string whatIsNeeded)
        {
            var users = await _likeRepository.GetUserLikes(whatIsNeeded, User.GetUserId());

            return Ok(users);
        }
    }
}