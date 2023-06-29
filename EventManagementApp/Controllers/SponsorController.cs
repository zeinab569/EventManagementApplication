using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos;
using EventManagementApp.Dtos.EventDTOs;
using EventManagementApp.Dtos.SpeakerDTOs;
using EventManagementApp.Dtos.SponsorDTO;
using EventManagementApp.Helpers;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorRepo _sponsorRepo;
        private readonly UploadImage _uploadImage;
        private IMapper _mapper;

        public SponsorController(ISponsorRepo sponsorRepo, IMapper mapper, UploadImage uploadImage)
        {
            _sponsorRepo = sponsorRepo;
            _mapper = mapper;
            _uploadImage = uploadImage;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllSponser()
        {
            var sponsersList = (List<Sponsor>)await _sponsorRepo.GetAllAsync(s => s.Events);
            var sponserDTOs = _mapper.Map<List<SponsorDTO>>(sponsersList);

            if (sponserDTOs.Count == 0) return NotFound();
            return Ok(sponserDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSponserById(int id)
        {
            var sponser = await _sponsorRepo.GetByIdAsync(id, s => s.Events);

            if (sponser == null) return NotFound();
            var sponserDTOs = _mapper.Map<SponsorDTO>(sponser);
            return Ok(sponserDTOs);
        }


        [HttpPost]
        public async Task<ActionResult<SponsorDTO>> AddSponser([FromForm] AddSponsorDTO sponserDTOs)
        {

            if (sponserDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var sponserEntity = _mapper.Map<Sponsor>(sponserDTOs);
            //--------------to uplaod img on asure -------
            if (sponserDTOs.SponsorLogo != null)
                sponserEntity.SponsorLogo = await _uploadImage.UploadToCloud(sponserDTOs.SponsorLogo);

            await _sponsorRepo.AddAsync(sponserEntity);
            return Created("Add Successfully", sponserDTOs);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<AddSponsorDTO>> UpdateSponsor(int id, [FromForm] AddSponsorDTO sponserDTOs)
        {
            if (sponserDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var sponserEntity = _mapper.Map<Sponsor>(sponserDTOs);

            if (sponserDTOs.SponsorLogo != null)
                sponserEntity.SponsorLogo = await _uploadImage.UploadToCloud(sponserDTOs.SponsorLogo);

            await _sponsorRepo.UpdateAsync(id, sponserEntity);
            return Ok(sponserDTOs);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSponsor(int id)
        {
            var sponser = await _sponsorRepo.GetByIdAsync(id);
            if (sponser == null) return NotFound();
            try
            {
                await _sponsorRepo.DeleteAsync(id);
                return Ok(sponser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}

