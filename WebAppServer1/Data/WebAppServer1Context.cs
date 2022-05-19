using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1.Data
{
    public class WebAppServer1Context
    {
        public readonly List<UserF> UserF;
        public WebAppServer1Context ()
        {
            DateTime dt = DateTime.Now;
            UserF = new List<UserF>();
            List<Contact> contacts = new List<Contact>();
            List<Message> messages = new List<Message>();
            List<Chat> chats = new List<Chat>();


            Contact Sagiv = new Contact("Sagiv", "sag", "Hi", "s1", dt);
            contacts.Add(Sagiv);
            Message message1 = new Message(1, "Hello", "text", dt, true);
            messages.Add (message1);
            message1 = new Message(1, "Hello", "text", dt.AddSeconds(5), false);
            messages.Add(message1);
            Chat SagivBen = new Chat(1, "Sagiv", messages);
            chats.Add(SagivBen);   
            UserF Ben = new UserF ("BenG", "1234", "Ben", "A", "s1", chats, contacts);
        }

        public List<UserF> ToListAsync()
        {
            return this.UserF;
        }
        public void Add(UserF user)
        {
            UserF.Add(user);
        }
        public bool SaveChangesAsync()
        {
            return false;
        }
        public void Update(UserF user)
        {
            foreach (UserF u in this.UserF)
            {
                if(u.Username == user.Username)
                {
                    if(user.Server != null)
                        u.Server = user.Server;
                    if(user.NickName != null)
                        u.NickName = user.NickName;
                }
            }
        }
    }
}
