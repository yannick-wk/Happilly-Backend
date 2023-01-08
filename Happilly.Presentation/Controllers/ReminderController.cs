using Happilly.Application.Dtos;
using Happilly.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Happilly.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IService<ReminderDto> _reminderService;

        public ReminderController(IService<ReminderDto> reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet, Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ReminderDto>> GetAllAsync()
        {
            return await _reminderService.GetAllAsync();
        }

        [HttpGet, Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ReminderDto> GetByIdAsync(Guid id)
        {
            return await _reminderService.FindSingleOrDefaultAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionReturnObject> CreateReminderAsync(ReminderDto reminder)
        {
            bool success = await _reminderService.CreateAsync(reminder);
            return new ActionReturnObject(success);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReminderAsync(Guid id)
        {
            bool success = await _reminderService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateReminderAsync(ReminderDto reminder)
        {
            bool success = await _reminderService.UpdateAsync(reminder);
            return success ? Ok() : NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReminderMedicineAsync(Guid id)
        {
            bool success = await _reminderService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }

    }
}
