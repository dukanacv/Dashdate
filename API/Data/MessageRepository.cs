using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(MyDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string receiverUsername)
        {
            var messages = await _context.Messages//getting message conversations
                .Include(u => u.Sender).ThenInclude(p => p.Photos)//access sender photos
                .Include(u => u.Receiver).ThenInclude(p => p.Photos)//access receiver photos
                .Where(m => m.Receiver.Username == currentUsername
                    && m.Sender.Username == receiverUsername
                    || m.Receiver.Username == receiverUsername
                    && m.Sender.Username == currentUsername//SVE SA SVIM MORA DA SE POKLAPA
                ).OrderBy(m => m.DateSent)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null
                && m.Receiver.Username == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)//LOOP over unread and read them
                {
                    message.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }

        public async Task<PagedList<MessageDTO>> GetUserMessages(MessageParams messageParams)
        {
            var query = _context.Messages.OrderByDescending(m => m.DateSent).AsQueryable();
            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Receiver.Username == messageParams.Username),//if we are receiving the message
                "Outbox" => query.Where(u => u.Sender.Username == messageParams.Username),//if we sending
                _ => query.Where(u => u.Receiver.Username == messageParams.Username && u.DateRead == null)//default
            };

            var messages = query.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDTO>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}