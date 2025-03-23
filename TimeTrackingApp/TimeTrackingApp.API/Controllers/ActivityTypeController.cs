using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Domain.Services.ActivityTypes;
using TimeTrackingApp.Dto;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace TimeTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityTypeController : ControllerBase
    {
        private readonly IActivityTypeService _activityTypeService;
        private readonly IValidator<ActivityTypeDto> _activityTypeValidator;

        public ActivityTypeController(IActivityTypeService activityTypeService, IValidator<ActivityTypeDto> activityTypeValidator)
        {
            _activityTypeService = activityTypeService;
            _activityTypeValidator = activityTypeValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivityTypes(CancellationToken cancellationToken)
        {
            var activityTypes = await _activityTypeService.GetAllActivityTypesAsync(cancellationToken);
            return Ok(activityTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityTypeById(Guid id, CancellationToken cancellationToken)
        {
            var activityType = await _activityTypeService.GetActivityTypeByIdAsync(id, cancellationToken);
            if (activityType == null)
            {
                return NotFound();
            }
            return Ok(activityType);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivityType([FromBody] ActivityTypeDto activityType, CancellationToken cancellationToken)
        {
            if (activityType == null)
            {
                return BadRequest("Activity type cannot be null.");
            }

            var validationResult = await _activityTypeValidator.ValidateAsync(activityType, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _activityTypeService.AddActivityTypeAsync(activityType, cancellationToken);
            return CreatedAtAction(nameof(GetActivityTypeById), new { id = activityType.Id }, activityType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivityType(Guid id, [FromBody] ActivityTypeDto activityType, CancellationToken cancellationToken)
        {
            if (activityType == null)
            {
                return BadRequest("Activity type cannot be null.");
            }

            var existingActivityType = await _activityTypeService.GetActivityTypeByIdAsync(id, cancellationToken);
            if (existingActivityType == null)
            {
                return NotFound();
            }

            var validationResult = await _activityTypeValidator.ValidateAsync(activityType, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _activityTypeService.UpdateActivityTypeAsync(activityType, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityType(Guid id, CancellationToken cancellationToken)
        {
            var existingActivityType = await _activityTypeService.GetActivityTypeByIdAsync(id, cancellationToken);
            if (existingActivityType == null)
            {
                return NotFound();
            }

            await _activityTypeService.DeleteActivityTypeAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetActivityTypesByName(string name, CancellationToken cancellationToken)
        {
            var activityTypes = await _activityTypeService.GetActivitiesTypeByNameAsync(name, cancellationToken);
            return Ok(activityTypes);
        }
    }
}
