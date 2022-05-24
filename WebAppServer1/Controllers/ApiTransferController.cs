using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1.Controllers
{
    public class InvitationReq
    {
        public string mto { get; set; }
        public string mfrom { get; set; }
        public string server { get; set; }
        public InvitationReq(string mto, string mfrom, string server)
        {
            this.mto = mto;
            this.mfrom = mfrom;
            this.server = server;
        }   
    }
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
                return Created("api/transfer/"+mto, messageSend);

            } else
            {
                List<Message> newChatMessages = new List<Message>();
                newChatMessages.Add(new Message(1, mcontent, "text", DateTime.Now.ToShortTimeString(), true));
                Chat newChat = new Chat(from.Chats.Count, to.Username, newChatMessages);
                from.Chats.Add(newChat);
                return Ok();
            }
            
        }
        [Route("api/invitations")]
        [HttpPost]
        public IActionResult Invitation([FromBody] InvitationReq i)
        {
            UserF to = HardContext.Users.Find(x => x.Username == i.mto);
            if (to == null)
                return NotFound();
            if(to.Contacts.Exists(x => x.Id == i.mfrom))
            {
                return NoContent();
            } else
            {
                Contact newContact = new Contact(i.mfrom, i.mfrom, "", i.server, DateTime.Now);
                to.Contacts.Add(newContact);
                Chat newChat = new Chat(to.Chats.Count + 1, i.mfrom, new List<Message>());
                to.Chats.Add(newChat);
                return Created("api/invitation/"+i.mfrom, newContact);
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
