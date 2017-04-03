using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAvailability.Entities;

namespace VehicleAvailability.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly SoapDatabaseContext _context;

        public VehiclesController(SoapDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public IEnumerable<VehicleTbl> GetVehicleTbl()
        {
            return _context.VehicleTbl;
        }

        // GET: api/Vehicles/5
        [HttpGet]
        [Route("GetAvailableVehicles")]
        public IActionResult GetAvailableVehicles(string city)
        {
            var vehicleList = _context.VehicleTbl.Where(vehicle => vehicle.VehicleCity == city && vehicle.VehicleStatus == "Ready");

            if (vehicleList == null)
            {
                return NotFound();
            }

            return Ok(vehicleList);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleTbl([FromRoute] string id, [FromBody] VehicleTbl vehicleTbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleTbl.VehicleVin)
            {
                return BadRequest();
            }

            _context.Entry(vehicleTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleTblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<IActionResult> PostVehicleTbl([FromBody] VehicleTbl vehicleTbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VehicleTbl.Add(vehicleTbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleTbl", new { id = vehicleTbl.VehicleVin }, vehicleTbl);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleTbl([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleTbl = await _context.VehicleTbl.SingleOrDefaultAsync(m => m.VehicleVin == id);
            if (vehicleTbl == null)
            {
                return NotFound();
            }

            _context.VehicleTbl.Remove(vehicleTbl);
            await _context.SaveChangesAsync();

            return Ok(vehicleTbl);
        }

        private bool VehicleTblExists(string id)
        {
            return _context.VehicleTbl.Any(e => e.VehicleVin == id);
        }
    }
}