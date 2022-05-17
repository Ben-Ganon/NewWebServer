using ServerFreak.Models;
using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class UserF
    {
        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string NickName { get; set; }

        public string Image { get; set; }

        public string Server { get; set; } 
        public ICollection<Chat>? Chats { get; set; }
    }
}
