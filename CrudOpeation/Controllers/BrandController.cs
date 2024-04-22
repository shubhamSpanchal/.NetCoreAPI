using CrudOpeation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOpeation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _brandContext;
        public BrandController(BrandContext brandContext)
        {
            _brandContext = brandContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>>GetBrandList()
        {
            if(_brandContext.Brands == null)
            {
                return NotFound();
            }
            else
            {
                return await _brandContext.Brands.ToListAsync();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Brands>> GetBrandById(int id)
        {
            if (_brandContext.Brands == null)
            {
                return NotFound();
            }
            else
            {
                var brands = await _brandContext.Brands.FindAsync(id);
                if (brands == null)
                {
                    return NotFound();
                }
                else
                {
                    return brands;
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<Brands>>PostBrand(Brands brands)
        {
            _brandContext.Brands.Add(brands);
            await _brandContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrandList),new { id = brands.id}, brands);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateBrand(Brands brand)
        {
            if(brand.id == 0)
            {
                return BadRequest();
            }
            _brandContext.Entry(brand).State = EntityState.Modified;
            try
            {
                await _brandContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!BrandAvailbale(brand.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteBrand(int id)
        {
            if(_brandContext.Brands == null)
            {
                return NotFound();
            }
            var brand= await _brandContext.Brands.FindAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
            _brandContext.Brands.Remove(brand);
            await _brandContext.SaveChangesAsync();
            return Ok();
        }
        private bool BrandAvailbale(int id)
        {
            return(_brandContext.Brands?.Any(b => b.id == id)).GetValueOrDefault();
        }
    }
}
