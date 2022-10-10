namespace Happilly_backend.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Medicine()
        {

        }

        public Medicine(Medicine medicine)
        {
            Id = medicine.Id;
            Name = medicine.Name;
            Description = medicine.Description;
        }
    }
}
