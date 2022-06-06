using Microsoft.AspNetCore.Mvc;
using ServerFreak.Models;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppServer1.Controllers
{


    public class PutUser
    {
        public string Nickname { get; set; }
        public string Server { get; set; }
    }
    [Route("api/users")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        // GET: api/users
        [HttpGet]
        public IEnumerable<UserF> Get()
        {
            return HardContext.ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            UserF seekU = HardContext.Get(id);
            if (seekU == null)
                return NotFound();
            return Ok(seekU);
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] UserF newUser)
        {
            HardContext.Users.Add(newUser);
            base.Response.StatusCode = (int)HttpStatusCode.Created;

        }
        
        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] PutUser u)
        {
            HardContext.Put(id, u);
            base.Response.StatusCode = (int)HttpStatusCode.NoContent;

        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            UserF seek = HardContext.Users.Find(x => x.Username == id);
            if(seek == null)
                base.Response.StatusCode = (int)HttpStatusCode.NotFound;
            HardContext.Users.Remove(seek);
            base.Response.StatusCode = (int)HttpStatusCode.NoContent;

        }

    }
}
