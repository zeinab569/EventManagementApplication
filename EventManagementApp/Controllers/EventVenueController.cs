using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    public class EventVenueController : BaseApiController
    {
        private readonly IEventVenueRepo _eventvenueRepo;
        IMapper _mapper;
       
        public EventVenueController(IEventVenueRepo eventRepo, IMapper mapper)
        {
            _eventvenueRepo = eventRepo;
            _mapper = mapper;
        }

        //get
        [HttpGet]
        public async Task<IActionResult> GetAllEventVenues()
        {
            var eventvenueList = (List<EventVenue>)await _eventvenueRepo.GetAllAsync(
                 s => s.Events
                );
            var eventvenueDTOs = _mapper.Map<List<EventVenueDto>>(eventvenueList);

            if (eventvenueDTOs.Count == 0) return NotFound();
            return Ok(eventvenueDTOs);
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventVenueById(int id)
        {
            var Event = await _eventvenueRepo.GetByIdAsync(id,
                s => s.Events);

            if (Event == null) return NotFound();
            var eventvenueDTOs = _mapper.Map<EventVenueDto>(Event);
            return Ok(eventvenueDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddEventVenue(EventVenueDto eventvenueDTOs)
        {
            if (eventvenueDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            await _eventvenueRepo.AddAsync(_mapper.Map<EventVenueDto, EventVenue>(eventvenueDTOs));
            return Created("Add Successfully", eventvenueDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataEventVenue(int id, EventVenueDto eventvenueDTOs)
        {
            if (eventvenueDTOs == null) return BadRequest();
            await _eventvenueRepo.UpdateAsync(id, _mapper.Map<EventVenueDto, EventVenue>(eventvenueDTOs));
            return Ok(eventvenueDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventVenue(int id)
        {
            var eventVenue = await _eventvenueRepo.GetByIdAsync(id);
            if (eventVenue == null) return NotFound();
            try
            {
                await _eventvenueRepo.DeleteAsync(id);
                return Ok(eventVenue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
