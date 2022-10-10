namespace Happilly.Domain.Entities
{
    public class Reminder : IEntity
    {
        public Guid Id { get; set; }
        // [Required]
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public DateTime Registered { get; set; }
        // Foreign Key
        public int UserId { get; set; }
        // Navigation property
        public virtual List<Medicine> Medicines { get; set; }
    }
}
