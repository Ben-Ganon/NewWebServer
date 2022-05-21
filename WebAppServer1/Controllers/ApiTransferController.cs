using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1.Controllers
{

    [ApiController]
    public class ApiTransferController : ControllerBase
    {
        [Route("api/transfer")]
        [HttpPost]
        public IActionResult Transfer([Bind("Title, Body")] string from, string to, string content)
        {
           
            UserF from = HardContext.Get(m.from);
            if(from == null)
                return NotFound();
            UserF to = HardContext.Get(m.to);
            if (to == null)
                return NotFound();

            if(from.Contacts.Exists(x => x.Id == to.Username))
            {
                Chat chatToUpdate = from.Chats.Find(x => x.ContactId == to.Username);
                Message messageSend = new Message(chatToUpdate.Messages.Count + 1, m.content, "text", DateTime.Now, true);
                chatToUpdate.Messages.Add(messageSend);
                return Ok();

            } else
            {
                List<Message> newChatMessages = new List<Message>();
                newChatMessages.Add(new Message(1, m.content, "text", DateTime.Now, true));
                Chat newChat = new Chat(from.Chats.Count + 1, to.Username, newChatMessages);
                from.Chats.Add(newChat);
                return Ok();
            }
            
        }
    }
    public class RecMessage
    {
        public string from;
        public string to;
        public string content;
    }
}
