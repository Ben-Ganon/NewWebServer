using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1.Controllers
{
    public static class HardContext
    {
        public static  List<UserF> Users;
        public static string CurrentUser;
        public static  void newDB()
        {
            DateTime dt = DateTime.Now;
            Users = new List<UserF>();
            List<Contact> contacts = new List<Contact>();
            List<Message> messages = new List<Message>();
            List<Chat> chats = new List<Chat>();


            for (int i = 0; i < 5; i++)
            {
                messages.Add(new Message(i, "Hello"+i,"text", dt.AddSeconds(i), true));
            }
            
            Contact Sagiv = new Contact("SagivA", "sag", "Hi", "s1", dt);
            Contact Omri = new Contact("Omri", "om", "Hi", "s1", dt);
            contacts.Add(Sagiv);
            contacts.Add(Omri);
          
            Chat SagivBen = new Chat(1, "SagivA", messages);
            chats.Add(SagivBen);
            UserF Ben = new UserF("BenG", "1234", "Ben", "A", "s1", chats, contacts);
            Users.Add(Ben);


            List<Contact> contacts2 = new List<Contact>();
            List<Message> messages2 = new List<Message>();
            foreach (Message m in messages)
            {
                messages2.Add(new Message(m));
                m.Sent = false;
            }
            Contact BenC = new Contact("BenG", "bb", "Hi", "s1", dt);
            contacts.Add(BenC);
            var chats2 = new List<Chat>();
            Chat BenSag = new Chat(1, "BenG", messages2);
            chats2.Add(BenSag);
            UserF SagivU = new UserF("SagivA", "1111", "Sag", "a", "s1", chats2, contacts2);
            Users.Add(SagivU);
            
        }
        
        public static UserF Get(string username)
        {
            UserF u = Users.First(x => x.Username == username);
            return u;
        }
        public static List<UserF> ToListAsync()
        {
            return Users;
        }
        public static List<UserF> ToList()
        {
            return Users;
        }
        public static void Add(UserF user)
        {
            Users.Add(user);
        }
        public static void AddContact(string username, Contact c)
        {
            if(Users.Exists(x => x.Username == username))
                Users.Find(x => x.Username == username).Contacts.Add(c);
        }
        public static Contact PutContact(string username, string id, string server, string nickname)
        {
            int index = Users.Find(x => x.Username == username).Contacts.FindIndex(x => x.Id == id);
            Contact c = Users.Find(x => x.Username == username).Contacts[index];
            Users.Find(x => x.Username == username).Contacts[index] = new Contact(c.Id, nickname, c.Last, server, c.LastDate);
            return c;
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
        public static void Delete(UserF user)
        {
            Users.Remove(user);
        }
        public static int DeleteContact(string username, string id)
        {
            int r = Users.Find(x => x.Username == username).Contacts.RemoveAll(x => x.Id == id);
            return r;
        }
        public static void SaveChanges()
        {

        }


    }
}
