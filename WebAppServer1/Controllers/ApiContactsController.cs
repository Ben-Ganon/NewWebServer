using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
using System.Net;
using WebAppServer1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppServer1.Controllers
{

    public class ReturnCont
    {
        public ReturnCont(Contact c)
        {
            id = c.Id;
            name = c.Name;
            server = c.server;
            last = c.Last;
            lastdate = c.LastDate.ToShortTimeString();
        }

        public string id { get; set; }
        public string name { get; set; }
        
        public string server { get; set; }

        public string last { get; set; }

        public string lastdate { get; set; }
    }
    public class Cont
    {
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }
    }
    public class PutCont
    {
        public string name { get; set; }
        public string server { get; set; }
    }

    public class AndoridCont
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string server { get; set; }

        public string Last { get; set; }

        public string LastDate { get; set; }

        public string userId { get; set; }

        public AndoridCont(Contact c)
        {
            id = 0;
            Name = c.Name;
            server = c.server;
            Last = c.Last;
            LastDate = c.LastDate.ToShortTimeString();
            userId = "";

        }

    }
    [Route("api/contacts")]
    [ApiController]
    public class ApiContactsController : ControllerBase
    {
        

        public ApiContactsController()
        {
            
            //string nameOfUser = HttpContext.Session.GetString("user");

        }
        // GET: api/contacts
        [HttpGet]
        public IEnumerable<ReturnCont> Get([FromQuery] string username)
        {

            List<Contact> conts = HardContext.ContactList(username);
            if(conts == null)
                return null;
            var c = conts.Select(x => new ReturnCont(x));
            return c;
        }

        //// GET: api/contacts/android
        //[Route("api/contacts/android")]
        //public IEnumerable<AndoridCont> Get([FromQuery] string username)
        //{
        //    List<Contact> conts = HardContext.ContactList(username);
        //    if (conts == null)
        //        return null;
        //    var c = conts.Select(x => new AndoridCont(x));
        //    return c;
        //}

        // GET api/contacts/5
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery] UserPayload user, string id)
        {
            var c = HardContext.Get(user.username).Contacts.Find(x => x.Id == id);
            if(c == null)
                return NotFound();
            ReturnCont d = new ReturnCont(c);
            return Ok(d);
        }

        // POST api/contacts
        [HttpPost]
        public void Post([FromQuery] string username, [FromBody] Cont c)
        {
            var user = HardContext.Get(username);
            if(user == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            Contact checkExist = user.Contacts.Find(x => x.Id == c.id);
            if(checkExist != null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.Created;
                return;
            }
            var contacts = user.Contacts.ToList();
            Contact newContact = new Contact(c.id, c.name, "Start a New Conversation", c.server, DateTime.Now);
            //user.Contacts.Add(newContact);
            //Chat chat = new Chat(user.Chats.Count()+1,c.id, new List<Message>());
            //user.Chats.Add(chat);
            HardContext.Add(user.Username, newContact);
            
            if (!HardContext.UserExists(c.id))
            {
                base.Response.StatusCode = (int)HttpStatusCode.Created;
                return;
            }
            var contUser = HardContext.Get(c.id);
            checkExist = contUser.Contacts.Find(x => x.Id == username);
            if (checkExist != null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.Created;
                return;
            }
            Contact newCont = new Contact(username, username, "Start New Conversation", "s1", DateTime.Now);
            //contUser.Contacts.Add(newCont);
            //Chat chat2 = new Chat(contUser.Chats.Count() + 1, username, new List<Message>());
            //contUser.Chats.Add(chat2);
            HardContext.Add(contUser.Username, newCont);

            base.Response.StatusCode = (int)HttpStatusCode.Created;


        }

        // PUT api/contacts/5
        [HttpPut("{id}")]
        public void Put([FromQuery] UserPayload user,string id, [FromBody] PutCont d)
        {
            
            var c = HardContext.PutContact(user.username, id, d.server, d.name);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            base.Response.StatusCode = (int)HttpStatusCode.NoContent;

        }

        // DELETE api/<ApiContactsController>/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery] UserPayload user, string id)
        {
            var r = HardContext.DeleteContact(user.username, id);


        }
    }
}
