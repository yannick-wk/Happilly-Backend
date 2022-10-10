using Happilly.Application.Dtos;
using Happilly.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Happilly.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IService<MedicineDto> _medicineService;

        public MedicineController(IService<MedicineDto> medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet, Route("all")]
        public async Task<IEnumerable<MedicineDto>> GetAllAsync()
        {
            return await _medicineService.GetAllAsync();
        }

        [HttpGet, Route("{id:Guid}")]
        public async Task<MedicineDto> GetByIdAsync(Guid id)
        {
            return await _medicineService.FindSingleOrDefaultAsync(id);
        }
    }
}
