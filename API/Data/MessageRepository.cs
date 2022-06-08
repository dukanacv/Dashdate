using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MyDbContext _context;
        public MessageRepository(MyDbContext context)
        {
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

        public Task<IEnumerable<MessageDTO>> GetMessageThread(int currUserId, int receiverId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MessageDTO>> GetUserMessages()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}