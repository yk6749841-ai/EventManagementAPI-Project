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
        private static List<Participant> participants = new List<Participant>();

        [HttpGet]
        public ActionResult<List<Participant>> GetAll() => participants;

        [HttpGet("{id}")]
        public ActionResult<Participant> GetById(int id)
        {
            var p = participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public ActionResult<Participant> Create(Participant newParticipant)
        {
            newParticipant.Id = participants.Count > 0 ? participants.Max(x => x.Id) + 1 : 1;
            participants.Add(newParticipant);
            return CreatedAtAction(nameof(GetById), new { id = newParticipant.Id }, newParticipant);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Participant updatedParticipant)
        {
            var p = participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            p.Name = updatedParticipant.Name;
            p.Email = updatedParticipant.Email;
            p.Phone = updatedParticipant.Phone;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var p = participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            participants.Remove(p);
            return NoContent();
        }
        [HttpGet("{id}/registrations")]
        public ActionResult<List<Registration>> GetRegistrationsForParticipant(int id)
        {
            var p = participants.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();

            var regs = RegistrationController.registrations.Where(r => r.ParticipantId == id).ToList();
            return regs;
        }

    }
}
