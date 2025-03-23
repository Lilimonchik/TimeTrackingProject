using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Domain.Services.Activities;
using TimeTrackingApp.Dto;
using FluentValidation;

namespace TimeTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IValidator<ActivityDto> _activityValidator;

        public ActivityController(IActivityService activityService, IValidator<ActivityDto> activityValidator)
        {
            _activityService = activityService;
            _activityValidator = activityValidator;
        }

        [HttpPost("TrackActivity")]
        public async Task<IActionResult> TrackActivity([FromBody] ActivityDto activityDto, CancellationToken cancellationToken)
        {
            if (activityDto == null)
            {
                return BadRequest("Activity data is required.");
            }

            await _activityService.TrackActivityAsync(activityDto, cancellationToken);
            return Ok("Activity tracked successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities(CancellationToken cancellationToken)
        {
            var activities = await _activityService.GetAllActivitiesAsync(cancellationToken);
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id, CancellationToken cancellationToken)
        {
            var activity = await _activityService.GetActivityByIdAsync(id, cancellationToken);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity([FromBody] ActivityDto activity, CancellationToken cancellationToken)
        {
            if (activity == null)
            {
                return BadRequest("Activity cannot be null.");
            }

            var validationResult = await _activityValidator.ValidateAsync(activity, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _activityService.AddActivityAsync(activity, cancellationToken);
            return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] ActivityDto activity, CancellationToken cancellationToken)
        {
            if (activity == null)
            {
                return BadRequest("Activity cannot be null.");
            }

            var existingActivity = await _activityService.GetActivityByIdAsync(id, cancellationToken);
            if (existingActivity == null)
            {
                return NotFound();
            }

            var validationResult = await _activityValidator.ValidateAsync(activity, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _activityService.UpdateActivityAsync(activity, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken)
        {
            var existingActivity = await _activityService.GetActivityByIdAsync(id, cancellationToken);
            if (existingActivity == null)
            {
                return NotFound();
            }

            await _activityService.DeleteActivityAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetActivitiesByName(Guid employeeId, int week, CancellationToken cancellationToken)
        {
            var activities = await _activityService.GetActivitiesByEmployeeAndWeekAsync(employeeId, week, cancellationToken);
            return Ok(activities);
        }
    }
}
