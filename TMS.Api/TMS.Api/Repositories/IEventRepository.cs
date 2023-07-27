﻿using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event> GetById(int id);

        Task<Event> Add(Event @event);

        void Update(Event @event);

        void Delete(Event @event);
       
    }
}