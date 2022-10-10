using System.ComponentModel.DataAnnotations;

namespace Happilly_backend.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public DateTime Registered { get; set; }
        
        // Foreign Key
        public int UserId { get; set; }
        // Navigation propterty
        public Medicine Medicine { get; set; }
        
    }
}
