using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.SpeakerDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepo _speakerRepo;
        IMapper _mapper;

        public SpeakerController(ISpeakerRepo speakerRepo, IMapper mapper)
        {
            _speakerRepo = speakerRepo;
            _mapper = mapper;
        }

        //get
        [HttpGet]
        public async Task<IActionResult> GetAllSpeakrs()
        {
            List<Speaker> speakerList = (List<Speaker>)await _speakerRepo.GetAllAsync(
                 s => s.Events
                );
            var speakerDTOs = _mapper.Map<List<SpeakerDTO>>(speakerList);

            if (speakerDTOs.Count == 0) return NotFound();
            return Ok(speakerDTOs);
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpeakerById(int id)
        {
            var speaker = await _speakerRepo.GetByIdAsync(id,
                s => s.Events);

            if (speaker == null) return NotFound();
            var speakerDTOs = _mapper.Map<SpeakerDTO>(speaker);
            return Ok(speakerDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpeaker(AddSpeakerDTO seakerDTOs)
        {
            if (seakerDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            await _speakerRepo.AddAsync(_mapper.Map<Speaker>(seakerDTOs));
            return Created("Add Successfully", seakerDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataSpeaker(int id, AddSpeakerDTO speakerDTOs)
        {
            if (speakerDTOs == null) return BadRequest();
            await _speakerRepo.UpdateAsync(id, _mapper.Map<Speaker>(speakerDTOs));
            return Ok(speakerDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeaker(int id)
        {
            var speaker = await _speakerRepo.GetByIdAsync(id);
            if (speaker == null) return NotFound();
            try
            {
                await _speakerRepo.DeleteAsync(id);
                return Ok(speaker);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
