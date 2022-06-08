using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]//we need user to be logged in, in order to like smth
    [ServiceFilter(typeof(LogUserActivity))]
    public class MessagesController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessageDto)
        {
            var username = User.GetUsername();

            if (username == createMessageDto.ReceiverUsername.ToLower())
            {
                return BadRequest("Can not send messages to yourself!");
            }

            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var receiver = await _userRepository.GetUserByUsernameAsync(createMessageDto.ReceiverUsername);

            if (receiver == null)
            {
                return NotFound("Couldnt find user");
            }

            var message = new Message
            {
                Sender = sender,
                Receiver = receiver,
                SenderUsername = sender.Username,
                ReceiverUsername = receiver.Username,
                content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<MessageDTO>(message));
            }

            return BadRequest("Sending Failed.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetUserMessages([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();
            var messages = await _messageRepository.GetUserMessages(messageParams);

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageThread(string username/* other user name*/)
        {
            var currentUsername = User.GetUsername();

            return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        }
    }
}