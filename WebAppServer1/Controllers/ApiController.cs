using Microsoft.AspNetCore.Mvc;
using WebAppServer1.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppServer1.Controllers
{

    public class Contact
    {
        public string id;
        public string name;
        public string server;
        public string lastMessage;
        public DateTime lastTime;

    }
    [Route("api/contacts")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly WebAppServer1Context _context;

        public ApiController(WebAppServer1Context context)
        {
            _context = context;
        }
        // GET: api/contacts
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            string username = HttpContext.Session.GetString("username");
            List<Contact> CList = new List<Contact>();
            var contacts = _context.User.Find(username).Chats.ToList();
            foreach(var c in contacts)
            {
                Contact d = new Contact();
                d.server = c.
            }
            return ;
        }

        // GET api/contacts/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/contacts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/contacts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/contacts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
