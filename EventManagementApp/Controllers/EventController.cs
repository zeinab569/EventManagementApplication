using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.EventDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepo _eventRepo;
        IMapper _mapper;
        public EventController(IEventRepo eventRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var eventsList = (List<Event>)await _eventRepo.GetAllAsync();
            var eventDTOs = _mapper.Map<List<EventDTO>>(eventsList);

            if (eventDTOs.Count == 0) return NotFound();
            return Ok(eventDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var Event = await _eventRepo.GetByIdAsync(id,
                s => s.Sponsors, sp => sp.Speakers, g => g.Gallaries);

            if (Event == null) return NotFound();
            var eventDTOs = _mapper.Map<EventByIdDTO>(Event);
            return Ok(eventDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventDTO eventDTOs)
        {
            if (eventDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            await _eventRepo.AddAsync(_mapper.Map<Event>(eventDTOs));
            //return CreatedAtAction("GetEventById", new { id = eventDTOs.id }, eventDTOs);
            return Created("Add Successfully", eventDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataEvent(int id, AddEventDTO eventDTOs)
        {
            if (eventDTOs == null) return BadRequest();
            await _eventRepo.UpdateAsync(id, _mapper.Map<Event>(eventDTOs));
            return Ok(eventDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var Event = await _eventRepo.GetByIdAsync(id);
            if (Event == null) return NotFound();
            try
            {
                await _eventRepo.DeleteAsync(id);
                return Ok(Event);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
