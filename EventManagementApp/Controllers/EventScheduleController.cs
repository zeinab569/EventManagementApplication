using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.EventScheduleDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventScheduleController : ControllerBase
    {
        private readonly IEventScheduleRepo _eventScheduleRepo;
        IMapper _mapper;
        public EventScheduleController(IEventScheduleRepo eventScheduleRepo, IMapper mapper)
        {
            _eventScheduleRepo = eventScheduleRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedule()
        {
            var scheduleList = (List<EventSchedule>)await _eventScheduleRepo.GetAllAsync(e => e.Event);
            var scheduleDTOs = _mapper.Map<List<EventScheduleDTO>>(scheduleList);

            if (scheduleDTOs.Count == 0) return NotFound();
            return Ok(scheduleDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var schedule = await _eventScheduleRepo.GetByIdAsync(id, e => e.Event);
            if (schedule == null) return NotFound();
            var scheduleDTOs = _mapper.Map<EventScheduleDTO>(schedule);
            return Ok(scheduleDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchedule(AddScheduleDTO scheduleDTOs)
        {
            if (scheduleDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            await _eventScheduleRepo.AddAsync(_mapper.Map<EventSchedule>(scheduleDTOs));
            return Created("Add Successfully", scheduleDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, AddScheduleDTO scheduleDTOs)
        {
            if (scheduleDTOs == null) return BadRequest();
            await _eventScheduleRepo.UpdateAsync(id, _mapper.Map<EventSchedule>(scheduleDTOs));
            return Ok(scheduleDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _eventScheduleRepo.GetByIdAsync(id);
            if (schedule == null) return NotFound();
            try
            {
                await _eventScheduleRepo.DeleteAsync(id);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
