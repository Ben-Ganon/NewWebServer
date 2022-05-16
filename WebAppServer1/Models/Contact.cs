using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class Contact
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string NickName { get; set; }

        public string Server { get; set; }

        public DateTime DateTime { get; set; }

        public ChatBox[] Chats { get; set; }
    }
}
