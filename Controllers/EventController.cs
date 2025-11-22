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
        private static List<Event> events = new List<Event>();

        [HttpGet]
        public ActionResult<List<Event>> GetAll() => events;

        [HttpGet("{id}")]
        public ActionResult<Event> GetById(int id)
        {
            var ev = events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();
            return ev;
        }

        [HttpPost]
        public ActionResult<Event> Create(Event newEvent)
        {
            newEvent.Id = events.Count > 0 ? events.Max(e => e.Id) + 1 : 1;
            events.Add(newEvent);
            return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, newEvent);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Event updatedEvent)
        {
            var ev = events.FirstOrDefault(e => e.Id == id);
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
            var ev = events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            events.Remove(ev);
            return NoContent();
        }
        [HttpGet("{id}/availability")]
        public ActionResult<int> GetAvailability(int id)
        {
            var ev = events.FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            int registeredCount = RegistrationController.registrations.Count(r => r.EventId == id && r.Status == "Registered");
            int availableSeats = ev.Capacity - registeredCount;
            return availableSeats;
        }

    }
}
