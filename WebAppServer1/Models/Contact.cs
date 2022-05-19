using ServerFreak.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAppServer1.Models
{
    public class Contact
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Last { get; set; }

        public string server { get; set; }

        public DateTime LastDate { get; set; }
        public Contact()
        {

        }
        public Contact(string id, string name, string last, string server, DateTime lastDate)
        {
            Id = id;
            Name = name;
            Last = last;
            this.server = server;
            LastDate = lastDate;
        }
    }
}
