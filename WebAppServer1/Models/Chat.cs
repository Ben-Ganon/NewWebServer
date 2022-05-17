using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class Chat
    {
        
        [Key]
        public string Contact { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
