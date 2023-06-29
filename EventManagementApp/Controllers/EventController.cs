using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.EventDTOs;
using EventManagementApp.Helpers;
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
        private readonly ISpeakerRepo _speakerRepo;
        private readonly ISponsorRepo _sponserRepo;
        private readonly UploadImage _uploadImage;

        IMapper _mapper;
        public EventController(IEventRepo eventRepo, IMapper mapper,
            ISpeakerRepo speakerRepo, ISponsorRepo sponserRepo, UploadImage uploadImage)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
            _speakerRepo = speakerRepo;
            _sponserRepo = sponserRepo;
            _uploadImage = uploadImage;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var eventsList = (List<Event>)await _eventRepo.GetListAsync();
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
        public async Task<IActionResult> AddEvent([FromForm] AddEventDTO eventDTOs)
        {
            if (eventDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var eventEntity = _mapper.Map<Event>(eventDTOs);

            if (eventDTOs.EventImage != null)
                eventEntity.EventImage = await _uploadImage.UploadToCloud(eventDTOs.EventImage);

            #region Speakers on Event

            var speakers = new List<Speaker>();
            foreach (var speakerId in eventDTOs.SpeakersId)
            {
                var speaker = await _speakerRepo.GetByIDAsync(speakerId);
                if (speaker != null)
                    speakers.Add(speaker);
            }
            eventEntity.Speakers = speakers;

            #endregion

            #region Sponser on Event

            var sponsers = new List<Sponsor>();
            foreach (var sponserId in eventDTOs.SponsorsId)
            {
                var sponser = await _sponserRepo.GetByIDAsync(sponserId);
                if (sponser != null)
                    sponsers.Add(sponser);
            }
            eventEntity.Sponsors = sponsers;

            #endregion

            await _eventRepo.AddAsync(eventEntity);
            return Created("Add Successfully", eventDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataEvent(int id, [FromForm] AddEventDTO eventDTOs)
        {
            var existingEvent = await _eventRepo.GetByIdAsync(id);
            if (existingEvent == null) return NotFound();

            if (eventDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            existingEvent = _mapper.Map<Event>(eventDTOs);
            if (eventDTOs.EventImage != null)
                existingEvent.EventImage = await _uploadImage.UploadToCloud(eventDTOs.EventImage);

            #region Speakers on Event

            var speakers = new List<Speaker>();
            foreach (var speakerId in eventDTOs.SpeakersId)
            {
                var speaker = await _speakerRepo.GetByIDAsync(speakerId);
                if (speaker != null)
                    speakers.Add(speaker);
            }
            existingEvent.Speakers.Clear();
            existingEvent.Speakers.AddRange(speakers);

            #endregion

            #region Sponser on Event

            var sponsers = new List<Sponsor>();
            foreach (var sponserId in eventDTOs.SponsorsId)
            {
                var sponser = await _sponserRepo.GetByIDAsync(sponserId);
                if (sponser != null)
                    sponsers.Add(sponser);
            }
            existingEvent.Sponsors.Clear();
            existingEvent.Sponsors.AddRange(sponsers);

            #endregion

            await _eventRepo.UpdateAsync(id, existingEvent);
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
