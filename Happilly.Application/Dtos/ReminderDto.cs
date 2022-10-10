namespace Happilly.Application.Dtos
{
    public class ReminderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public DateTime Registered { get; set; }
        public int UserId { get; set; }
        public MedicineDto Medicine { get; set; }
    }
}