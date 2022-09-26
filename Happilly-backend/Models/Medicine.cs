namespace Happilly_backend.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineDescription { get; set; }

        public Medicine()
        {

        }

        public Medicine(Medicine medicine)
        {
            MedicineId = medicine.MedicineId;
            MedicineName = medicine.MedicineName;
            MedicineDescription = medicine.MedicineDescription;
        }
    }
}
