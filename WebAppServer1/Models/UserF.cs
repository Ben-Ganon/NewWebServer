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
        public List<Chat>? Chats { get; set; }

        public List<Contact>? Contacts { get; set; }
        public UserF()
        {
            Chats = new List<Chat>();
            Contacts = new List<Contact>(); 
        }
        
        public UserF(string username, string password, string nickName, string image, string server, List<Chat> chats, List<Contact> contacts)
        {
            Username = username;
            Password = password;
            NickName = nickName;
            Image = image;
            Server = server;
            Chats = chats;
            Contacts = contacts;
        }
        public Contact Contact(string Id)
        {
            Contact c = this.Contacts.First(x => x.Id == Id);
            return c;
        }
    }
    
}
