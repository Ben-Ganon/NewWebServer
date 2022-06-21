using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1.Controllers
{

    public class UserPayload
    {
        public string username { get; set; }
    }
    public class HardContext
    {
        public static  List<UserF> Users;

        public HardContext() { }
        public  void newDB()
        {
            DateTime dt = DateTime.Now;
            Users = new List<UserF>();
            List<Contact> contacts = new List<Contact>();
            List<Message> messages = new List<Message>();
            List<Chat> chats = new List<Chat>();


            for (int i = 0; i < 5; i++)
            {
                messages.Add(new Message(i, "Hello"+i,"text", dt.ToShortTimeString(), true));
            }
            
            Contact Sagiv = new Contact("SagivA", "sag", "Hi", "s1", dt);
            Contact Omri = new Contact("Omri", "om", "Hi", "s1", dt);
            contacts.Add(Sagiv);
           // contacts.Add(Omri);
          
            Chat SagivBen = new Chat(1, "SagivA", messages);
            chats.Add(SagivBen);
            Chat OmriBen = new Chat(2, "Omri", new List<Message>());
            //chats.Add(OmriBen);
            UserF Ben = new UserF("BenG", "123456Bg", "Ben", "../images/p1.jpg", "s1", chats, contacts);
            Users.Add(Ben);


            List<Contact> contacts2 = new List<Contact>();
            List<Message> messages2 = new List<Message>();
            foreach (Message m in messages)
            {
                messages2.Add(new Message(m));
                m.Sent = false;
            }
            Contact BenC = new Contact("BenG", "bb", "Hi", "s1", dt);
            contacts2.Add(BenC);
            var chats2 = new List<Chat>();
            Chat BenSag = new Chat(1, "BenG", messages2);
            chats2.Add(BenSag);
            Chat OmriSag = new Chat(2, "Omri", new List<Message>());
            chats2.Add(OmriSag);
            Contact Omri3 = new Contact("Omri", "ommri", "gello", "s1", dt);
            contacts2.Add(Omri3);
            UserF SagivU = new UserF("SagivA", "111145Sa", "Sag", "../images/p2.png", "s1", chats2, contacts2);
            Users.Add(SagivU);

            List<Contact> contacts3 = new List<Contact>();
            Contact sagiv2 = new Contact("SagivA", "sagiv", "Hi", "s1", dt);
            contacts3.Add(sagiv2);
            List<Message> messages3 = new List<Message>();
            var chats3 = new List<Chat>();
            Chat OmriSagiv = new Chat(1, "SagivA", new List<Message>());
            chats3.Add(OmriSagiv);
            UserF OmriU = new UserF("Omri", "111145Ob", "omriB", "../images/p3.png", "s1", chats3, contacts3);
            Users.Add(OmriU);

            UserF Uri = new UserF("Uri", "111145Ug", "Uriel", "../images/p3.png", "s1", new List<Chat>(), new List<Contact>());
            Users.Add(Uri);


            UserF Sahar = new UserF("Sahar", "111145Sr", "One Rofe", "../images/p4.jpg", "s1", new List<Chat>(), new List<Contact>());
            Users.Add(Sahar);

        }
        public static bool UserExists(string username)
        {
            using (var db = new WebServerContext())
            {
                if(db.Users.Find(username) != null) return true;
                return false;
            }
        }
        public static bool MessageExists(string username, string contact, int messagId)
        {
            using (var db = new WebServerContext())
            {
                var user = db.Users.Find(username);
                if (user == null) 
                    return false;
                if (user.Contacts.First(x => x.Id == contact) == null)
                    return false;
                if (user.Chats.First(x => x.ContactId == contact) == null)
                    return false;
                if (user.Chats.First(x => x.ContactId == contact).Messages.First(x => x.Id == messagId) == null)
                    return false;
                return true;
            }
        }
        public static List<UserF> Get()
        {
            using (var db = new WebServerContext())
            {
                List<UserF> users = db.Users.Include(x => x.Contacts).Include(x => x.Chats).ThenInclude(x => x.Messages).ToList();
                return users;
            }
        }
        public static UserF Get(string username)
        {
            using (var db = new WebServerContext())
            {
                UserF user = db.Users.Include(x => x.Contacts).Include(x => x.Chats).ThenInclude(x => x.Messages).First(x => x.Username == username);
                return user;
            }
           
        }
        public static List<UserF> ToListAsync()
        {
            using (var db = new WebServerContext())
            {
                List<UserF> users = db.Users.ToList();
                return users;
            }
        }
        public static List<Contact> ContactList(string username)
        {
            using (var db = new WebServerContext())
            {
                UserF user = db.Users.Include(x => x.Contacts).First(x => x.Username == username);
                return user.Contacts;
            }
        }
        public static List<UserF> ToList()
        {
            using (var db = new WebServerContext())
            {
                List<UserF> users = db.Users.ToList();
                return users;
            }
        }
        public static void Put(string username, PutUser user)
        {
            using (var db = new WebServerContext())
            {
                var userSeek = db.Users.Find(username);
                if (userSeek == null)
                    return;
                userSeek.NickName = user.Nickname;
                userSeek.Server = user.Server;
                db.SaveChanges();
            }
        }
        
        public static void Add(UserF user)
        {
            using (var db = new WebServerContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static void Add(string username, Contact c)
        {
            using (var db = new WebServerContext())
            {
                UserF user = db.Users.Include(x => x.Contacts).Include(x => x.Chats).First(x => x.Username == username);
                if (user == null)
                    return;
                //db.Users.Find(username).Contacts.Add(c);
                //db.Users.Find(username).Chats.Add(new Chat(0, c.Id, new List<Message>()));
                user.Contacts.Add(c);
                user.Chats.Add(new Chat(0, c.Id, new List<Message>()));
                db.SaveChanges();
            }
        }
        public static async void Add(string username,string contact,  Message m)
        {
            using (var db = new WebServerContext())
            {
               m.Id = 0;
               db.Users.Include(x => x.Contacts).Include(x => x.Chats).ThenInclude(x => x.Messages).First(x => x.Username == username).Chats.First(x => x.ContactId == contact).Messages.Add(m);
               //user.Chats.First(x => x.ContactId == contact).Messages.Add(m);
               await db.SaveChangesAsync();
            }
        }
        public static Contact PutContact(string username, string id, string server, string nickname)
        {
            using (var db = new WebServerContext())
            {
                var user = db.Users.Find(username);
                var cont = user.Contacts.Find(x => x.Id == id);
                cont.server = server;
                cont.Name = nickname;
                db.SaveChanges();
                return cont;
            }
            //int index = Users.Find(x => x.Username == username).Contacts.FindIndex(x => x.Id == id);
            //Contact c = Users.Find(x => x.Username == username).Contacts[index];
            //Users.Find(x => x.Username == username).Contacts[index] = new Contact(c.Id, nickname, c.Last, server, c.LastDate
        }
        public static bool SaveChangesAsync()
        {
            return false;
        }
        public static void Update(UserF user)
        {
            UserF u = Users.Find(x => x.Username == user.Username);
            if (u != null)
            {
                if (user.Server != null)
                    u.Server = user.Server;
                if (user.NickName != null)
                    u.NickName = user.NickName;
            }
        }
        public static void Delete(string username)
        {
            using (var db = new WebServerContext())
            {
                var User = db.Users.Include(x => x.Contacts).Include(x => x.Chats).First(x => x.Username == username);
                db.Users.Remove(User);
                db.SaveChanges();
            }
        }
        public static void Delete(string username, string contact, int messageId)
        {
            using (var db = new WebServerContext())
            {
                db.Users.Find(username).Chats.First(x => x.ContactId == contact).Messages.RemoveAll(x => x.Id == messageId);
                db.SaveChanges();
            }
        }
        public static int DeleteContact(string username, string id)
        {
            //int r = Users.Find(x => x.Username == username).Contacts.RemoveAll(x => x.Id == id);
            //return r;
            using (var db = new WebServerContext())
            {
                var User = db.Users.Include(x => x.Contacts).Include(x => x.Chats).First(x => x.Username ==  username);
                User.Contacts.RemoveAll(x => x.Id == id);
                User.Chats.RemoveAll(x => x.ContactId == id);
                db.SaveChanges();
                return 1;

            }
        }
        public static void SaveChanges()
        {

        }


    }
}
