using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos;
using EventManagementApp.Dtos.EventDTOs;
using EventManagementApp.Dtos.SponsorDTO;
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
        private IMapper _mapper;

        public SponsorController(ISponsorRepo sponsorRepo, IMapper mapper)
        {
            _sponsorRepo = sponsorRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sponsor>))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetAllSponsor()
        {
            var sponsors = _mapper.Map<List<SponsorDTO>>(await _sponsorRepo.GetListAsync());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(sponsors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Sponsor))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetSponsorById(int id)
        {
            if (!_sponsorRepo.IsSponsorExist(id))
                return NotFound();

            var sponsor =await _sponsorRepo.GetByIdAsync(id,
                s => s.Events);
            var sponsorDTO = _mapper.Map<SponsorDTO>(sponsor);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sponsorDTO);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SponsorDTO>> AddSponsor(AddSponsorDTO sponsorToCreate)
        {

         
            if (sponsorToCreate == null)
                return BadRequest(ModelState);

            var sponsor = (await _sponsorRepo.GetListAsync())
                .Where(c => c.SponsorName.Trim().ToUpper() == sponsorToCreate.SponsorName.Trim().ToUpper())
                .FirstOrDefault();

            if (sponsor != null)
            {
                ModelState.AddModelError("", $"Sponsor {sponsorToCreate.SponsorName} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            Sponsor sponsorObj = _mapper.Map<AddSponsorDTO, Sponsor>(sponsorToCreate);
            Sponsor PostedSponsor = await _sponsorRepo.AddAsync(sponsorObj);
            if(PostedSponsor == null)
            {
                ModelState.AddModelError("", $"Something went wrong saving the Sponsor " +
                                             $"{sponsorObj.SponsorName}");
                return StatusCode(500, ModelState);
            }
            return Ok($"{sponsorObj.SponsorName} added successfully");

        }


        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AddSponsorDTO>> UpdateSponsor(int id,
        AddSponsorDTO sponsorToUpdate)
        {
            if (sponsorToUpdate == null)
                return BadRequest(ModelState);

            if (!_sponsorRepo.IsSponsorExist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sponsorObj = _mapper.Map<AddSponsorDTO, Sponsor>(sponsorToUpdate);
            if ( _sponsorRepo.UpdateAsync(id, sponsorObj) == null)
            {
                ModelState.AddModelError("", $"Something went wrong updating the Sponsor " +
                                                    $"{sponsorObj.SponsorName}");
                return StatusCode(500, ModelState);
            }

            return Ok(sponsorToUpdate);
        }

        //delete 
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteSponsor(int id)
        {
            if (!_sponsorRepo.IsSponsorExist(id))
            {
                return NotFound();
            }

            Sponsor sponsorToDelete = await _sponsorRepo.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_sponsorRepo.DeleteAsync(id) == null)
            {
                ModelState.AddModelError("", $"Something went wrong deleting the sponsor " +
                                      $"{sponsorToDelete.SponsorName}");
            }
            return Ok($"{sponsorToDelete.SponsorName} deleted successfully");

        }
    }


}

