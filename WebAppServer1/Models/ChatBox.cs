using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class ChatBox
    {
        [Required]
        public int Id { get; set; }

        public MessageChat[] Messages { get; set; }

    }
}
