using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos;
using Core.Interfaces;

namespace EventManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepo _ticketRepo;
        private IMapper _mapper;

        public TicketController(ITicketRepo ticketRepo, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }
        [HttpGet]

        public async Task<ActionResult> GetTickets()
        {
            List<Ticket> ticketsList = (List<Ticket>)await _ticketRepo.GetAllAsync(e=>e.Event,e=>e.Purchase);
            List<TicketDTO> ticketDTOs = (List<TicketDTO>)_mapper.Map<IReadOnlyList<Ticket>, IReadOnlyList<TicketDTO>>(ticketsList);
            if (ticketsList.Count == 0) { return NotFound(); }
            return Ok(ticketDTOs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDTO>> GetTicketByID(int id)
        {
            if (await _ticketRepo.GetByIdAsync(id) == null) return NotFound();
            Ticket ticket = await _ticketRepo.GetByIdAsync(id, e => e.Event, e => e.Purchase);
            return Ok(_mapper.Map<Ticket, TicketDTO>(ticket));

        }
        [HttpPost]
        public async Task<ActionResult<TicketDTO>> AddTicket(TicketPostDTO ticket)
        {
            if (ticket == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
         
            Ticket ticketObj = _mapper.Map<TicketPostDTO, Ticket>(ticket);
            Ticket PostedTicket = await _ticketRepo.AddAsync(ticketObj);
            return Ok(_mapper.Map<Ticket, TicketPostDTO>(PostedTicket));

        }
        [HttpDelete]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            Ticket ticket = await _ticketRepo.GetByIdAsync(id);
            if (ticket == null) return NotFound();
             await _ticketRepo.DeleteAsync(id);

            return Ok(_mapper.Map<Ticket, TicketDTO>(ticket));
        }
        [HttpPut ("{id}")]
        public async Task<ActionResult<TicketDTO>>UpdateTicket( int id ,Ticket ticket )
        {

            if (ticket == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Ticket ticketFound = await _ticketRepo.GetByIdAsync(id);
            if (ticketFound == null) return NotFound();
            // ticketFound=_mapper.Map<TicketPostDTO,Ticket>(ticket);
            ticketFound.TicketQuentity = ticket.TicketQuentity;
            await _ticketRepo.UpdateAsync(id, ticketFound);
            return Ok(_mapper.Map<Ticket,TicketDTO>(ticketFound));

        }
        
        [HttpGet("/api/eventtickets/{eventId}")]
        public async Task<ActionResult<TicketDTO>> GetAllTicketsByEventId(int eventId)
        {
            List<Ticket> ticketsList= (List<Ticket>)await _ticketRepo.GetListByEventIDAsync(eventId);
            List<TicketDTO> ticketDTOs = (List<TicketDTO>)_mapper.Map<IReadOnlyList<Ticket>, IReadOnlyList<TicketDTO>>(ticketsList);
            if (ticketsList.Count == 0) { return NotFound(); }
            return Ok(ticketDTOs);
        }

       
    }
}
