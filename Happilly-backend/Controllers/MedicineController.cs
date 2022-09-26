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
            return Json(medicines);
        }
    }
}
