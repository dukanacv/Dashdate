using System;

namespace API.DTOs
{
    public class CreateMessageDTO
    {
        public string ReceiverUsername { get; set; }
        public string Content { get; set; }
    }
}