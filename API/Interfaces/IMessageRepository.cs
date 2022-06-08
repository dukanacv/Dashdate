using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<IEnumerable<MessageDTO>> GetUserMessages();
        Task<IEnumerable<MessageDTO>> GetMessageThread(int currUserId, int receiverId);
        Task<bool> SaveAllAsync();
    }
}