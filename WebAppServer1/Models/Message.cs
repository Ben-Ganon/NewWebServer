using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string Type { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public bool Sent { get; set; }

        

    }
}
