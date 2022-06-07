using System;

namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public User Sender { get; set; }


        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; }
        public User Receiver { get; set; }

        public string content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;
    }
}