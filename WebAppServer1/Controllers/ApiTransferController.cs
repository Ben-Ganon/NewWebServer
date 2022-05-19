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
        public IActionResult Transfer([FromBody] RecMessage m)
        {
            string from = m.from;
            string to = m.to;
            UserF fromU = HardContext.Get(from);
            if(fromU == null)
                return NotFound();
            UserF toU = HardContext.Get(to);
            if (toU == null)
                return NotFound();

            if(HardContext.Users.Exists(x => x.Username == username)){
                UserF user = HardContext.Users.Find(x => x.Username == username);
                Contact from = 
                Chat userChat = user.Chats.Find(x => x.)

            }
            
            Message newM = new Message();
        }
    }
    public class RecMessage
    {
        public string from;
        public string to;
        public string content;
    }
}
