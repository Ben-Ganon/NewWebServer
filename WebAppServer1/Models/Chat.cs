using System.ComponentModel.DataAnnotations;
using WebAppServer1.Models;

namespace ServerFreak.Models
{
    public class Chat
    {
        
        public int Id { get; set; }
        public string ContactId { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Chat(int id, string contactId, ICollection<Message> messages)
        {
            Id = id;
            ContactId = contactId;
            Messages = messages;
        }
    }

    
}
