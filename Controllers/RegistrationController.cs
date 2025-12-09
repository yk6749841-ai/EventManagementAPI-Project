using EventManagementAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("registrations")]
    public class RegistrationController : ControllerBase
    {
        private readonly DataContext _context;

        public RegistrationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Registration>> GetAll()
        {
            return _context.Registrations;
        }

        [HttpGet("{id}")]
        public ActionResult<Registration> GetById(int id)
        {
            var r = _context.Registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();
            return r;
        }

        [HttpPost]
        public ActionResult<Registration> Create(Registration newReg)
        {
            // בדיקת תקינות: ודא שהאירוע קיים
            var ev = _context.Events.FirstOrDefault(e => e.Id == newReg.EventId);
            if (ev == null) return BadRequest("האירוע המבוקש לא קיים.");

            // בדיקת תקינות: ודא שהמשתתף קיים
            var p = _context.Participants.FirstOrDefault(p => p.Id == newReg.ParticipantId);
            if (p == null) return BadRequest("המשתתף המבוקש לא קיים.");

            newReg.Id = _context.Registrations.Count > 0
                ? _context.Registrations.Max(r => r.Id) + 1
                : 1;

            // הגדרת תאריך הרשמה נוכחי אם לא הוגדר
            if (newReg.RegistrationDate == DateTime.MinValue)
            {
                newReg.RegistrationDate = DateTime.Now;
            }

            _context.Registrations.Add(newReg);

            return CreatedAtAction(nameof(GetById), new { id = newReg.Id }, newReg);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Registration updatedReg)
        {
            var r = _context.Registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            r.EventId = updatedReg.EventId;
            r.ParticipantId = updatedReg.ParticipantId;
            r.Status = updatedReg.Status;
            r.RegistrationDate = updatedReg.RegistrationDate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var r = _context.Registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            _context.Registrations.Remove(r);

            return NoContent();
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(int id, [FromBody] string newStatus)
        {
            var r = _context.Registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            r.Status = newStatus;
            return NoContent();
        }
    }
}