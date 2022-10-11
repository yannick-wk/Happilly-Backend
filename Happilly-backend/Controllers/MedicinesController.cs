using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Happilly_backend.Data;
using Happilly_backend.Models;

namespace Happilly_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : Controller
    {
        private readonly HappillyContext _context;

        public MedicinesController(HappillyContext context)
        {
            _context = context;
        }

        // GET api/Medicines/all-medicine
        [HttpGet, Route("all")]
        public IQueryable<MedicineDto> GetMedicines()
        {
            var medicines = from b in _context.Medicine
                        select new MedicineDto()
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Description = b.Description
                        };

            return medicines;
        }

        // GET api/Books/5
        [ResponseType(typeof(MedicineDetailDto))]
        public async Task<IHttpActionResult> GetMedicine(int id)
        {
            var medicine = await _context.Medicine.Include(b => b.Name).Select(b =>
                new MedicineDetailDto()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    Group = b.Group,
                    Stock = b.Stock
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (medicine == null)
            {
                return (IHttpActionResult)NotFound();
            }

            return (IHttpActionResult)Ok(medicine);
        }

        // PUT: api/Medicines/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
                {
                    return (IActionResult)NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/post/Medicines
        [ResponseType(typeof(MedicineDto))]
        [Route("medicine/{id}/{dto}")]
        public async Task<IHttpActionResult> PostMedicine(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return (IHttpActionResult)BadRequest(ModelState);
            }

            _context.Medicine.Add(medicine);
            await _context.SaveChangesAsync();

            _context.Entry(medicine).Reference(x => x.Name).Load();

            var dto = new MedicineDto()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description
            };

            return (IHttpActionResult)CreatedAtRoute("DefaultApi", new { id = medicine.Id }, dto);
        }

        // DELETE: api/Medicines/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicine.FindAsync(id);
            if (medicine == null)
            {
                return (IActionResult)NotFound();
            }

            _context.Medicine.Remove(medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicine.Any(e => e.Id == id);
        }

        private IActionResult NoContent()
        {
            throw new NotImplementedException();
        }
    }
}
