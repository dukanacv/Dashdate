using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()//making code async for if having many tasks at once
        {
            var users = await _userRepository.GetUsersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return _mapper.Map<MemberDto>(user);
        }

        [HttpPut]
        public async Task<ActionResult<MemberDto>> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.GetUsername();//returns first claim that matches param extension method
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);//insted of manually going through every user parameter

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Update User Failed.");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());//get user

            var result = await _photoService.AddPhotoAsync(file);//get photo

            if (result.Error != null) return BadRequest(result.Error.Message);//check result

            var photo = new Photo//create new photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                photoPublicId = result.PublicId
            };

            if (user.Photos.Count == 0)//check if it is first photo of user
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())//if db doesnt work
            {
                return CreatedAtRoute("GetUser", new { username = user.Username }, _mapper.Map<PhotoDto>(photo));
            }


            return BadRequest("Cannot add photo");
        }

        [HttpPut("main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());//find user by claims

            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);//find photo that matches given id

            var curr = user.Photos.FirstOrDefault(p => p.IsMain);//check if its main one

            if (curr != null)
            {
                curr.IsMain = false;//turn off current main photo
            }

            photo.IsMain = true;//set that one to main

            if (await _userRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Cannot change main photo.");
        }

    }
}