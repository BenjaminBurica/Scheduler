using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.UseCases;
using Entities;

namespace SchedulerWeb.Controllers
{
    [Authorize(Roles = "Admin,ServiceProvider")]
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleApiController : ControllerBase
    {
        private readonly ISaveSchedule saveSchedule;
        private readonly IGetSchedule getSchedule; //Make IGetSchedule function

        public ScheduleApiController(
            ISaveSchedule saveSchedule,
            IGetSchedule getSchedule
        )
        {
            this.saveSchedule = saveSchedule;
            this.getSchedule = getSchedule;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<StoreSchedule>>> Get(int id)
        {
            var schedule = await getSchedule.GetAsync(id);
            return Ok(schedule);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Save(int id, [FromBody] List<StoreSchedule> schedule)
        {
            await saveSchedule.SaveScheduleAsync(id, schedule);
            return Ok();
        }

    }

}