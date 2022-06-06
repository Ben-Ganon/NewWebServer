using System.ComponentModel.DataAnnotations;
using WebAppServer1.Models;

namespace ServerFreak.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ContactId { get; set; }

        public List<Message> Messages { get; set; }

        public Chat()
        {
            Messages = new List<Message>();

        }
        public Chat(int id, string contactId, List<Message> messages)
        {
            Id = id;
            ContactId = contactId;
            Messages = messages;
        }
        public Message Message(int Id)
        {
            Message m = this.Messages.Find(x => x.Id == Id);
            return m;
        }
    }

    
}
