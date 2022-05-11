using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1,5)]
        public int Stars { get; set; }

        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}
