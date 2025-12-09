using EventManagementAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("participants")]
    public class ParticipantController : ControllerBase
    {
        private readonly DataContext _context;

        public ParticipantController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Participant>> GetAll()
        {
            return _context.Participants;
        }

        [HttpGet("{id}")]
        public ActionResult<Participant> GetById(int id)
        {
            var p = _context.Participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public ActionResult<Participant> Create(Participant newParticipant)
        {
            newParticipant.Id = _context.Participants.Count > 0
                ? _context.Participants.Max(x => x.Id) + 1
                : 1;

            _context.Participants.Add(newParticipant);
            return CreatedAtAction(nameof(GetById), new { id = newParticipant.Id }, newParticipant);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Participant updatedParticipant)
        {
            var p = _context.Participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            p.Name = updatedParticipant.Name;
            p.Email = updatedParticipant.Email;
            p.Phone = updatedParticipant.Phone;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var p = _context.Participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            _context.Participants.Remove(p);

            // מחיקת כל ההרשמות הקשורות למשתתף זה לשמירה על עקביות הנתונים
            _context.Registrations.RemoveAll(r => r.ParticipantId == id);

            return NoContent();
        }

        [HttpGet("{id}/registrations")]
        public ActionResult<List<Registration>> GetRegistrationsForParticipant(int id)
        {
            var p = _context.Participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            var regs = _context.Registrations.Where(r => r.ParticipantId == id).ToList();
            return regs;
        }
    }
}