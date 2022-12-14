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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<MedicineDto>> GetAllAsync()
        {
            return await _medicineService.GetAllAsync();
        }

        [HttpGet, Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MedicineDto> GetByIdAsync(Guid id)
        {
            return await _medicineService.FindSingleOrDefaultAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionReturnObject> CreateMedicineAsync(MedicineDto medicine)
        {
            bool success = await _medicineService.CreateAsync(medicine);
            return new ActionReturnObject(success);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedicineAsync(Guid id)
        {
            bool success = await _medicineService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMedicineAsync(MedicineDto medicine)
        {
            bool success = await _medicineService.UpdateAsync(medicine);
            return success ? Ok() : NotFound();
        }
    }

    public class ActionReturnObject
    {
        public bool Success { get; }

        public ActionReturnObject(bool success)
        {
            Success = success;
        }

        public override bool Equals(object? obj)
        {
            return obj is ActionReturnObject other &&
                   Success == other.Success;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Success);
        }
    }
}
