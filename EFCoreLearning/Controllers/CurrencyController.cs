using EFCoreLearning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace EFCoreLearning.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //sync methods
        //[HttpGet("")]
        //public IActionResult GetAllCurrencies()
        //{
        //    //var result  = _appDbContext.currencies.ToList();

        //    var result = (from currencies in _appDbContext. currencies select currencies).ToList();
        //    return Ok(result);
        //}
        //[HttpGet("{id}")]
        //public IActionResult GetCurrenciesById([FromRoute] int id) {

        //    var result = _appDbContext.currencies.Find(id);
        //    return Ok(result);

        //}

        //async Methods

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrenciesAsync()
        {
            var result = await _appDbContext.currencies.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetCurrenciesByIdAsync([FromRoute] int id)
        {
            var result = await _appDbContext.currencies.FindAsync(id);

            return Ok(result);
        }

        //[HttpGet("{name}")]

        //public async Task<IActionResult> GetCUrrenciesByNameAsync([FromRoute] string name) {
        //    Using SingleAsync()  shows an error
        //    var result = await _appDbContext.currencies.Where(x => x.Title == name).SingleAsync();
        //    return Ok(result);
        //}

        [HttpGet("{name}")]

        public async Task<IActionResult> GetCurrenciesByNameAsync([FromRoute] string name) {

            var result = await _appDbContext.currencies.SingleOrDefaultAsync(x=>x.Title ==name);
            return Ok(result);

        }
    }
}
