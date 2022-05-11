using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class MessageChat
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public bool Sent { get; set; }

        [Required]
        public Contact ClientSent { get; set; }

    }
}
