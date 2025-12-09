using EventManagementAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventController : ControllerBase
    {
        private readonly DataContext _context;

        public EventController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Event>> GetAll()
        {
            return _context.Events;
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetById(int id)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();
            return ev;
        }

        [HttpPost]
        public ActionResult<Event> Create(Event newEvent)
        {
            newEvent.Id = _context.Events.Count > 0 ? _context.Events.Max(e => e.Id) + 1 : 1;
            _context.Events.Add(newEvent);
            return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, newEvent);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Event updatedEvent)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            ev.Title = updatedEvent.Title;
            ev.DateTime = updatedEvent.DateTime;
            ev.Location = updatedEvent.Location;
            ev.Capacity = updatedEvent.Capacity;
            ev.Description = updatedEvent.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            _context.Events.Remove(ev);
            _context.Registrations.RemoveAll(r => r.EventId == id);

            return NoContent();
        }

        [HttpGet("{id}/availability")]
        public ActionResult<int> GetAvailability(int id)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            int registeredCount = _context.Registrations
                                        .Count(r => r.EventId == id && r.Status == "מאושר");

            int availableSeats = ev.Capacity - registeredCount;
            return availableSeats;
        }
    }
}