using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Api.Models;
using TMS.Api.Models.Dto;
using TMS.Api.Repositories;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventController> _logger;
        public EventController(IEventRepository eventRepository, IMapper mapper, ILogger<EventController> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            if (events == null)
            {
                return NotFound();
            }
            /* var eventDto = events.Select(e => new EventDto()
             {
                 EventId = e.EventId,
                 EventDescription = e.EventDescription,
                 EventName = e.EventName,
                 EventType = e.EventType?.EventTypeName ?? string.Empty,
                 Location = e.Location?.LocationName ?? string.Empty
             });*/
            var eventDto = _mapper.Map<IEnumerable<Event>, List<EventDto>>(events);
            return Ok(eventDto);
        }

        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {



            var @event = await _eventRepository.GetById(id);


            /*var eventDto = new EventDto()
            {
                EventId = @event.EventId,
                EventDescription = @event.EventDescription,
                EventName = @event.EventName,
                EventType = @event.EventType?.EventTypeName ?? string.Empty,
                Location = @event.Location?.LocationName ?? string.Empty
            };*/

            var eventDto = _mapper.Map<EventDto>(@event);

            return Ok(eventDto);


        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {

            if (eventPatch == null) throw new ArgumentNullException(nameof(eventPatch));
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
            if (eventEntity == null)
            {
                return NotFound();
            }

            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.EventDescription = eventPatch.EventDescription;
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }

        [HttpPost]

        public async Task<ActionResult<EventAddDto>> Add(EventAddDto eventAdd)
        {
            var eventToSave = _mapper.Map<Event>(eventAdd);
            await _eventRepository.Add(eventToSave);
            return CreatedAtAction(nameof(GetById), new { id = eventToSave.EventId }, eventToSave);
        }



    }
}


 

