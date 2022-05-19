using System.ComponentModel.DataAnnotations;
using WebAppServer1.Models;

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

        public ICollection<Contact>? Contacts { get; set; }
        public UserF(string username, string password, string nickName, string image, string server, ICollection<Chat> chats, ICollection<Contact> contacts)
        {
            Username = username;
            Password = password;
            NickName = nickName;
            Image = image;
            Server = server;
            Chats = chats;
            Contacts = contacts;
        }
    }
    
}
