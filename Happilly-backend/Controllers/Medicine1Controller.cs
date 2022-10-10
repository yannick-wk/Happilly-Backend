using Microsoft.AspNetCore.Mvc;
using Happilly_backend.Models;

namespace Happilly_backend.Controllers
{
    [Route("api/medicine")]
    [ApiController]
    public class Medicine1Controller : Controller
    {
        public Medicine medicine = new();
        
        [HttpGet]
        [Route("all-medicine")]
        public JsonResult GetMedicine()
        {
            List<Medicine> medicines = new();
            Medicine medicine1 = new();
            medicine1.Id = 1;
            medicine1.Name = "Paracetamol";
            medicine1.Description = "Paracetamol, also known as acetaminophen, is a medication used to treat fever and mild to moderate pain.";
            medicines.Add(medicine1);
            Medicine medicine2 = new();
            medicine2.Id = 2;
            medicine2.Name = "Adderall";
            medicine2.Description = "Adderall and Mydayis are trade names for a combination drug called mixed amphetamine salts containing four salts of amphetamine.";
            medicines.Add(medicine2);
            Medicine medicine3 = new();
            medicine3.Id = 3;
            medicine3.Name = "Ibuprofen";
            medicine3.Description = "Ibuprofen is a nonsteroidal anti-inflammatory drug that is used for treating pain, fever, and inflammation. This includes painful menstrual periods, migraines, and rheumatoid arthritis.";
            medicines.Add(medicine3);
            return Json(medicines);
        }
    }
}
