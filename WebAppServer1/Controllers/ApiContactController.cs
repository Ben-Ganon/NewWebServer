using Microsoft.AspNetCore.Mvc;
using ServerFreak.Models;
using WebAppServer1.Data;
using System.Net;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppServer1.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ApiContactController : ControllerBase
    {
        private readonly WebAppServer1Context _context;

        public ApiContactController(WebAppServer1Context context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _context.Contact.ToList();
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var cont = _context.Contact.Find(id);

            if (cont != null)
            {
                return Ok(cont);
            }
            return NotFound();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Stream a)
        {
            Contact b = (Contact)JsonSerializer.Deserialize(a, default);
            var ans = _context.Contact.Add(b);
            var ans2 = _context.SaveChanges();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
