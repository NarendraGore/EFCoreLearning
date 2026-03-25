using System.Runtime.CompilerServices;
using EFCoreLearning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLearning.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //sync
        //[HttpGet("")]
        //public IActionResult GetAllLanguage() {
        //    var result = _appDbContext.Languages.ToList();
        //    return Ok(result);
        //}

        //[HttpGet("{id}")]

        //public IActionResult GetLanguageById([FromRoute] int id) {
        //    //var result = (from Languages in _appDbContext.Languages select Languages).Find(id);
        //    var result = _appDbContext.Languages.Find(id);

        //    return Ok(result);

        //}

        //Async
        [HttpGet("")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var result = await _appDbContext.Languages.ToListAsync();
            return Ok(result);

        }
        //[HttpGet("{id:int}")]

        //public async Task<IActionResult> GetLanguageByIdAsync([FromRoute] int id)
        //{

        //    var result = await _appDbContext.Languages.FindAsync(id);
        //    return Ok(result);

        //}

        //[HttpGet("{name}")]

        //public async Task<IActionResult> GetLanguageByNameAsync([FromRoute] string name)
        //{
        //Using FirstAsync()  shows an error

        //    var result = await _appDbContext.Languages.Where(x => x.Title == name).FirstAsync();
        //    return Ok(result);
        //}

        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetLanguageByNameAsync([FromRoute] string name) {
        //    //Using FirstOrDefaultAsync() not shows an error
        //    var result  = await _appDbContext.Languages.Where(x=>x.Title==name).FirstOrDefaultAsync();
        //    return Ok(result);
        //}

        //By Route
        [HttpGet("{name}/{description}")]
        public async Task<IActionResult> GetLanguageByNameOrDescription([FromRoute] string name, [FromRoute] string description) {

            var result = await _appDbContext.Languages.FirstAsync(x => x.Title == name && x.Description == description);
            return Ok(result);
        }
        //By Description or Query
        [HttpGet("{name}")]
        public async Task<IActionResult> GetLanguageByNameOrDescriptionFromQuery([FromRoute] string name, [FromQuery] string? description)
        {

            var result = await _appDbContext.Languages.FirstAsync(x => x.Title == name && (string.IsNullOrEmpty(description) || x.Description == description));
            return Ok(result);
        }
        //Access the multiple data from multiple id

        [HttpPost("all")]

        public async Task<IActionResult> GetLanguageByMultipleId([FromBody] List<int> ids) {
            //var ids = new List<int> { 1, 2, 4 };

            var result = await _appDbContext.currencies.Where(x => ids.Contains(x.Id)).ToListAsync();
            
            return Ok(result);

        }

    }
}
