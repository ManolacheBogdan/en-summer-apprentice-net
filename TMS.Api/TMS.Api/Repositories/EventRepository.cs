using Microsoft.EntityFrameworkCore;
using TMS.Api.Exceptions;
using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly PracticaContext _dbContext;

        public EventRepository()
        {
            _dbContext = new PracticaContext();
        }

        public async Task<Event> Add(Event @event)
        {
            _dbContext.Add(@event);
            await _dbContext.SaveChangesAsync();
            return @event;
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;

            return events;
        }

        public async Task<Event> GetById(int id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).Include(e => e.EventType).Include(e => e.Location).FirstOrDefaultAsync();

            if (@event == null)
                throw new EntityNotFoundException(id, nameof(Event));

            return @event;
        }


        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

    }
}