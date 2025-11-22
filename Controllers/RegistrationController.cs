using EventManagementAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("registrations")]
    public class RegistrationController : ControllerBase
    {
        public static List<Registration> registrations = new List<Registration>();

        [HttpGet]
        public ActionResult<List<Registration>> GetAll() => registrations;

        [HttpGet("{id}")]
        public ActionResult<Registration> GetById(int id)
        {
            var r = registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();
            return r;
        }

        [HttpPost]
        public ActionResult<Registration> Create(Registration newReg)
        {
            newReg.Id = registrations.Count > 0 ? registrations.Max(x => x.Id) + 1 : 1;
            registrations.Add(newReg);
            return CreatedAtAction(nameof(GetById), new { id = newReg.Id }, newReg);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Registration updatedReg)
        {
            var r = registrations.FirstOrDefault(x => x.Id == id);
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
            var r = registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            registrations.Remove(r);
            return NoContent();
        }
        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(int id, [FromBody] string newStatus)
        {
            var r = registrations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            r.Status = newStatus;
            return NoContent();
        }

    }
}
