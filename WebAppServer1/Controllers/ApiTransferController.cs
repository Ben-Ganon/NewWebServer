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
        public IActionResult Transfer([Bind("Title, Body")] string mfrom, string mto, string mcontent)
        {
           
            UserF from = HardContext.Get(mfrom);
            if(from == null)
                return NotFound();
            UserF to = HardContext.Get(mto);
            if (to == null)
                return NotFound();

            if(from.Contacts.Exists(x => x.Id == to.Username))
            {
                Chat chatToUpdate = from.Chats.Find(x => x.ContactId == to.Username);
                Message messageSend = new Message(chatToUpdate.Messages.Count, mcontent, "text", DateTime.Now.ToShortTimeString(), true);
                chatToUpdate.Messages.Add(messageSend);
                return Ok();

            } else
            {
                List<Message> newChatMessages = new List<Message>();
                newChatMessages.Add(new Message(1, mcontent, "text", DateTime.Now.ToShortTimeString(), true));
                Chat newChat = new Chat(from.Chats.Count, to.Username, newChatMessages);
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
