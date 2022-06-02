using ChoPap.Api.Data;
using ChoPap.Api.Features.StockInfo;
using ChoPap.Features.GetStockInfo;
using ChoPap.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static ChoPap.Model.StockModel;

namespace ChoPap.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApiContext _context;

        public StocksController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listaDiscussion = await _context.BoughtStocks.ToListAsync();
            //var converted = JsonConvert.SerializeObject(listaDiscussion);

            return Ok(listaDiscussion);
        }

        //GET: api/Stocks/5
        [HttpGet("{name}")]
        public async Task<ActionResult<rootobject>> GetStocks(string? name)
        {
            ApiHelper.InitializeClient();
            List<rootobject> stocks = ToFromJson.ImportJson();
            return await GetStockInfo.SelectSpecifiedStockAsync(stocks, name);
        }

        //// PUT: api/Stocks/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStocks(int? id, Stocks stocks)
        //{
        //    if (id != stocks.StockID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(stocks).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StocksExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Stocks
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Stocks>> PostStocks(Stocks stocks)
        //{
        //    _context.Stocks.Add(stocks);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStocks", new { id = stocks.StockID }, stocks);
        //}

        //// DELETE: api/Stocks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStocks(int? id)
        //{
        //    var stocks = await _context.Stocks.FindAsync(id);
        //    if (stocks == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Stocks.Remove(stocks);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool StocksExists(int? id)
        {
            return _context.Stocks.Any(e => e.StockID == id);
        }
    }
}
