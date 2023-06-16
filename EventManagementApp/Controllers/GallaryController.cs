using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.GallaryDTOs;
using EventManagementApp.Dtos.SpeakerDTOs;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GallaryController : ControllerBase
    {
        private readonly IGallaryRepo _gallaryRepo;
        IMapper _mapper;

        public GallaryController(IGallaryRepo gallaryRepo, IMapper mapper)
        {
            _gallaryRepo = gallaryRepo;
            _mapper = mapper;
        }

        //get
        [HttpGet]
        public async Task<IActionResult> GetAllGallaries()
        {
            var gallaries = _mapper.Map<List<GallaryDTO>>(await _gallaryRepo.GetAllAsync(s => s.Event));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(gallaries);
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGallaryById(int id)
        {
            var gallary = await _gallaryRepo.GetByIdAsync(id,
                s => s.Event);

            if (gallary == null) return NotFound();
            var gallaryDTOs = _mapper.Map<GallaryDTO>(gallary);
            return Ok(gallaryDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Addgallary(AddGallaryDTO gallaryDTOs)
        {
            if (gallaryDTOs == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            Gallary gallaryObj = _mapper.Map<AddGallaryDTO, Gallary>(gallaryDTOs);
            Gallary PostedSponsor = await _gallaryRepo.AddAsync(gallaryObj);
            if (PostedSponsor == null)
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok($" added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataGallary(int id, AddGallaryDTO gallaryDTOs)
        {
            if (gallaryDTOs == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var gallaryObj = _mapper.Map<AddGallaryDTO, Gallary>(gallaryDTOs);
            if (_gallaryRepo.UpdateAsync(id, gallaryObj) == null)
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok(gallaryDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGallary(int id)
        {
            var gallary = await _gallaryRepo.GetByIdAsync(id);
            if (gallary == null) return NotFound();
            try
            {
                await _gallaryRepo.DeleteAsync(id);
                return Ok(gallary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
