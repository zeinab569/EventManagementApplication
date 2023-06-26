using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using EventManagementApp.Dtos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketPurcheaseController : ControllerBase
    {

        private readonly ITicketPurchasesRepo _ticketPurchasesRepo;
        private IMapper _mapper;
        public TicketPurcheaseController(ITicketPurchasesRepo ticketPurchasesRepo,IMapper mapper)
        {
            _ticketPurchasesRepo = ticketPurchasesRepo;
            _mapper = mapper;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetTicketPurchases()
        {
            List< TicketPurchase> ticketPurchaseList= (List<TicketPurchase>)await _ticketPurchasesRepo.GetAllAsync(e=>e.Ticket);
           List<TicketPurchasesDTO>ticketPurchasesDTOs= (List<TicketPurchasesDTO>)_mapper.Map<IReadOnlyList<TicketPurchase>, IReadOnlyList<TicketPurchasesDTO>>(ticketPurchaseList);
            if (ticketPurchaseList.Count <= 0) return NotFound();
            return Ok(ticketPurchasesDTOs);
             
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPurchasesById(int id)
        {
            TicketPurchase ticketPurchase = await _ticketPurchasesRepo.GetByIdAsync(id,e=>e.Ticket);
           if (ticketPurchase==null)return NotFound();
           return Ok(_mapper.Map<TicketPurchase,TicketPurchasesDTO>(ticketPurchase));

        }
        [HttpPost]
        public async Task<ActionResult<TicketPurchasePostDTO>> PostTicketPurchases(TicketPurchasePostDTO ticketPurchasesDTO)
        {
            if (ticketPurchasesDTO == null) return BadRequest();
            TicketPurchase ticketPurchase= await _ticketPurchasesRepo.AddAsync(_mapper.Map<TicketPurchase>(ticketPurchasesDTO));
            TicketPurchasePostDTO postedTicketPurchasesDTO = _mapper.Map<TicketPurchase, TicketPurchasePostDTO>(ticketPurchase);
            return Ok(postedTicketPurchasesDTO);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketPurchasesDTO>> DeleteTicketPurchease(int id )
        {
            TicketPurchase ticketPurchase = await _ticketPurchasesRepo.GetByIdAsync(id);
            if (ticketPurchase == null) return NotFound();
            await _ticketPurchasesRepo.DeleteAsync(id);
            return Ok(_mapper.Map<TicketPurchase,TicketPurchasesDTO>(ticketPurchase));

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TicketPurchasePostDTO>> UpdateTicketPurchease(int id ,TicketPurchasePostDTO ticketPurchasesdto)
        {
            if(ticketPurchasesdto == null) return BadRequest();
            if(!ModelState.IsValid)return BadRequest();
            TicketPurchase ticketPurchase = await _ticketPurchasesRepo.GetByIdAsync(id);
            if (ticketPurchase == null) return NotFound();
            
             await _ticketPurchasesRepo.UpdateAsync(id, _mapper.Map<TicketPurchase>(ticketPurchasesdto));
            return Ok(ticketPurchasesdto);
        }
    }
}
