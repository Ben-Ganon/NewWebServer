using Microsoft.AspNetCore.Mvc;
using ServerFreak.Models;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppServer1.Controllers
{
    public class Mess
    {
        public string content { get; set; }
    }
    [Route("api/contacts/{contact}/messages")]
    [ApiController]
    public class ApiMessageController : ControllerBase
    {
        // GET: api/messages/{contact}/messages
        [HttpGet]
        public IEnumerable<Message> Get(string username, string contact)
        {
            var user = HardContext.Get(username);
            var retCont = user.Chats.Find(x => x.ContactId == contact);
            if (retCont == null || retCont.Messages == null) {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            base.Response.StatusCode = (int)HttpStatusCode.OK;
            return retCont.Messages;
        }

        // GET api/messages/{contact}/messages/181
        [HttpGet("{id}")]
        public IActionResult Get([FromQuery]string username, string contact, int id)
        {
            var user = HardContext.Get(username);
            if (user == null)
                return NotFound();
            var chat = user.Chats.Find(x => x.ContactId == contact);
            if (chat == null)
                return NotFound();
            var message = chat.Messages.ElementAt(id);
            if (message == null)
                return NotFound();
            return Ok(message);
        }

        // POST api/messages/{contact}/messages/
        [HttpPost]
        public void Post(string contact, [FromQuery] string username, [FromBody]Mess m)
        {
            var user = HardContext.Get(username);
            var chat = user.Chats.Find(x => x.ContactId == contact);
            if (chat == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            Message message = new Message(chat.Messages.Count(), m.content, "text", DateTime.Now.ToShortTimeString(), true);
            chat.Messages.Add(message);

            var userReceive = HardContext.Get(contact);
            if (userReceive == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            chat = userReceive.Chats.Find(x => x.ContactId == username);
            if (chat == null)
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            Message m2 = new Message(message);
            m2.Sent = false;
            chat.Messages.Add(m2);  

            base.Response.StatusCode = (int)HttpStatusCode.Created;

        }

        // PUT api/messages/{contact}/messages/181
        [HttpPut("{id}")]
        public void Put([FromQuery] string username, string contact, int id, [FromBody] string value)
        {
            if(!HardContext.MessageExists(username, contact, id))
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

        }

        // DELETE api/messages/{contact}/messages
        [HttpDelete("{id}")]
        public void Delete([FromQuery] string username, string contact, int id)
        {
            if(!HardContext.MessageExists(username, contact, id))
            {
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            HardContext.Delete(username, contact, id);
        }
    }
}
