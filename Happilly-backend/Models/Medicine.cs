using System.ComponentModel.DataAnnotations;

namespace Happilly_backend.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Group { get; set; }
        public int Stock { get; set; }

    }
}
