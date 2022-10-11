namespace Happilly.Domain.Entities
{
    public class Medicine : IEntity
    {
        public Guid Id { get; set; }
        //Why not annotate using EF? [Required] --> Configuration over Convention (Database choice has nothing to do with your domain!).
        public string Name { get; set; }
        public string Description { get; set; }
        public int Group { get; set; }
        public int Stock { get; set; }
        public virtual List<Reminder> Reminders { get; set; }
    }
}