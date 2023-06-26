using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos.HotelDTO;
using EventManagementApp.Dtos.SponsorDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepo _hotelRepo;
        private IMapper _mapper;

        public HotelController(IHotelRepo hotelRepo, IMapper mapper)
        {
            _hotelRepo = hotelRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hotel>))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GETAllHotels()
        {
            var hotels = _mapper.Map<List<Hotel>>(await _hotelRepo.GetListAsync());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Hotel))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetHotelById(int id)
        {
            if (!_hotelRepo.IsHotelExist(id))
                return NotFound();

            var hotel = _mapper.Map<HotelDTO>(await _hotelRepo.GetByIdAsync(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(hotel);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<HotelDTO>> AddHotel(AddHotelDTO hotelToCreate)
        {
            if (hotelToCreate == null)
                return BadRequest(ModelState);

            var hotel = (await _hotelRepo.GetListAsync())
                .Where(c => c.HotelName.Trim().ToUpper() == hotelToCreate.HotelName.Trim().ToUpper())
                .FirstOrDefault();

            if (hotel != null)
            {
                ModelState.AddModelError("", $"Hotel {hotelToCreate.HotelName} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Hotel hotelObj = _mapper.Map<AddHotelDTO, Hotel>(hotelToCreate);

            if (_hotelRepo.AddAsync(hotelObj) == null)
            {
                ModelState.AddModelError("", $"Something went wrong saving the Hotel " +
                                                              $"{hotelObj.HotelName}");
                return StatusCode(500, ModelState);
            }
            return Ok($"{hotelObj.HotelName} added successfully");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AddHotelDTO>> UpdateHotel(int id, AddHotelDTO hotelToUpdate)
        {
            if (hotelToUpdate == null)
                return BadRequest(ModelState);

            if (!_hotelRepo.IsHotelExist(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var hotelObj = _mapper.Map<AddHotelDTO, Hotel>(hotelToUpdate);
            if (_hotelRepo.UpdateAsync(id, hotelObj) == null)
            {
                ModelState.AddModelError("", $"Something went wrong updating the Hotel " +
                                                    $"{hotelObj.HotelName}");
                return StatusCode(500, ModelState);
            }
            return Ok($"{hotelObj.HotelName} updated successfully");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            if (!_hotelRepo.IsHotelExist(id))
            {
                return NotFound();
            }

            Hotel hotelToDelete = await _hotelRepo.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_hotelRepo.DeleteAsync(id) == null)
            {
                ModelState.AddModelError("", $"Something went wrong deleting the Hotel " +
                                      $"{hotelToDelete.HotelName}");
            }
            return Ok($"{hotelToDelete.HotelName} deleted successfully");

        }
    }




}
