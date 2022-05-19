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
            lastdate = c.LastDate;
        }
        public string id { get; set; }
        public string name { get; set; }
        
        public string server { get; set; }

        public string last { get; set; }

        public DateTime lastdate { get; set; }
    }
    [Route("api/contacts")]
    [ApiController]
    public class ApiContactsController : ControllerBase
    {
        
        private readonly string nameOfUser;

        public ApiContactsController()
        {
            
            //string nameOfUser = HttpContext.Session.GetString("username");
            nameOfUser = "BenG";

        }
        // GET: api/contacts
        [HttpGet]
        public IEnumerable<ReturnCont> Get()
        {

            
            UserF u = HardContext.Get(nameOfUser);
            if(u == null)
                return null;
            var c = u.Contacts.Select(x => new ReturnCont(x));
            return c;
        }

        // GET api/contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var c = HardContext.Get(nameOfUser).Contacts.Find(x => x.Id == id);
            if(c == null)
                return NotFound();
            ReturnCont d = new ReturnCont(c);
            return Ok(d);
        }

        // POST api/contacts
        [HttpPost]
        public void Post([FromBody] Contact c)
        {
            c.LastDate = DateTime.Now;
            
            HardContext.AddContact(nameOfUser, c);
            HardContext.SaveChanges();
            base.Response.StatusCode = (int)HttpStatusCode.Created;
        }

        // PUT api/contacts/5
        [HttpPut("{id}")]
        public void Put([Bind("Title, Body")] string id, string newName, string newServer)
        {
            var c = HardContext.PutContact(nameOfUser, id, newServer, newName);
            if (c == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            base.Response.StatusCode = (int)HttpStatusCode.NoContent;

        }

        // DELETE api/<ApiContactsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var r = HardContext.DeleteContact(nameOfUser, id);


        }
    }
}
