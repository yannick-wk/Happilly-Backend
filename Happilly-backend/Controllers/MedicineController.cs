using Microsoft.AspNetCore.Mvc;
using Happilly_backend.Models;

namespace Happilly_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : Controller
    {
        public Medicine medicine = new();
        
        [HttpGet]
        public JsonResult GetMedicine()
        {
            List<Medicine> medicines = new();
            Medicine medicine1 = new();
            medicine1.MedicineId = 1;
            medicine1.MedicineName = "Paracetamol";
            medicine1.MedicineDescription = "Paracetamol, also known as acetaminophen, is a medication used to treat fever and mild to moderate pain.";
            medicines.Add(medicine1);
            Medicine medicine2 = new();
            medicine2.MedicineId = 2;
            medicine2.MedicineName = "Adderall";
            medicine2.MedicineDescription = "Adderall and Mydayis are trade names for a combination drug called mixed amphetamine salts containing four salts of amphetamine.";
            medicines.Add(medicine2);
            Medicine medicine3 = new();
            medicine3.MedicineId = 3;
            medicine3.MedicineName = "Ibuprofen";
            medicine3.MedicineDescription = "Ibuprofen is a nonsteroidal anti-inflammatory drug that is used for treating pain, fever, and inflammation. This includes painful menstrual periods, migraines, and rheumatoid arthritis.";
            medicines.Add(medicine3);
            return Json(medicines);
        }
    }
}
