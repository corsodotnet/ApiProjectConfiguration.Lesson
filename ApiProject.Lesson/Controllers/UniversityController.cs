using ApiProject.Lesson.Persistence.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProject.Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;
        private DatabaseCxt _context;
        public UniversityController(ILogger<UniversityController> logger,DatabaseCxt ctx)
        {
            _logger = logger;
            _context = ctx;
        }
        // GET: api/<UniversityController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UniversityController>/5
        [HttpGet("{id}")]
        public string Get(string corso)
        {
            Corso c;
            using (_context)
            {
                try
                {
                    c = _context.Corso.Where(c => c.Name == corso).First();
                    var data = _context.Corso
                   .Include(s => s.Students)
                   .First(c => c.Id == c.Id);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return corso;
        }

        // POST api/<UniversityController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UniversityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UniversityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
