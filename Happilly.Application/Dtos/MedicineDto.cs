namespace Happilly.Application.Dtos
{
    public class MedicineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Group { get; set; }
        public int Stock { get; set; }
    }
}