using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.EventDTOs;
using EventManagementApp.Dtos.SpeakerDTOs;
using EventManagementApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepo _speakerRepo;
        private readonly UploadImage _uploadImage;
        IMapper _mapper;

        public SpeakerController(ISpeakerRepo speakerRepo, IMapper mapper, UploadImage uploadImage)
        {
            _speakerRepo = speakerRepo;
            _mapper = mapper;
            _uploadImage = uploadImage;
        }

        //get
        [HttpGet]
        public async Task<IActionResult> GetAllSpeakrs()
        {
            var speakersList = (List<Speaker>)await _speakerRepo.GetAllAsync(s => s.Events);
            var speakerDTOs = _mapper.Map<List<SpeakerDTO>>(speakersList);

            if (speakerDTOs.Count == 0) return NotFound();
            return Ok(speakerDTOs);
            #region old code
            /*
            var sponsors = _mapper.Map<List<SpeakerDTO>>(await _speakerRepo.GetAllAsync(s => s.Events));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sponsors);
            */
            #endregion
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpeakerById(int id)
        {
            var speaker = await _speakerRepo.GetByIdAsync(id, s => s.Events);

            if (speaker == null) return NotFound();
            var speakerDTOs = _mapper.Map<SpeakerDTO>(speaker);
            return Ok(speakerDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpeaker([FromForm] AddSpeakerDTO speakerDTOs)
        {
            if (speakerDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var speakerEntity = _mapper.Map<Speaker>(speakerDTOs);
            //--------------to uplaod img on asure -------
            if (speakerDTOs.SpeakerImage != null)
                speakerEntity.SpeakerImage = await _uploadImage.UploadToCloud(speakerDTOs.SpeakerImage);

            await _speakerRepo.AddAsync(speakerEntity);
            return Created("Add Successfully", speakerDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataSpeaker(int id, [FromForm] AddSpeakerDTO speakerDTOs)
        {
            if (speakerDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var eSpeaker = _mapper.Map<Speaker>(speakerDTOs);

            if (speakerDTOs.SpeakerImage != null)
                eSpeaker.SpeakerImage = await _uploadImage.UploadToCloud(speakerDTOs.SpeakerImage);

            await _speakerRepo.UpdateAsync(id, eSpeaker);
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
