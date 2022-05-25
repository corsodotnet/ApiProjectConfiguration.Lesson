using ApiProject.Lesson.Models;
using ApiProject.Lesson.Persistence.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProject.Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;
        private DatabaseCxt _context;
        private IOptions<AppSettings> _setting;
        public UniversityController(ILogger<UniversityController> logger,DatabaseCxt ctx, IOptions<AppSettings> setting)
        {
            _logger = logger;
            _context = ctx;
            _setting = setting;
        }
        // GET: api/<UniversityController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UniversityController>/5
        [HttpGet("GetByCorso")]
        public IActionResult GetByCorso(string CorsoTitle)
        {
            Corso c;
            using (_context)
            {
                try
                {
                    c = _context.Corso.Where(c => c.Name == CorsoTitle).First();
                    var data = _context.Corso
                   .Include(s => s.Students)
                   .First(c => c.Id == c.Id);

                  // throw new Exception("Errore:  corso di laurea non trovato"); 
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                } 
            }
            return Ok(c);
        }
        [HttpGet("GetByStudente")]
        public IActionResult GetByStudente(string Studente)
        {
            Studente s;
            using (_context)
            {
                try
                {
                    s = _context.Studente.Where(c => c.Name == Studente).First();
                   // var data = _context.Corso
                   //.Include(s => s.Students)
                  // .First(c => c.Id == c.Id);

                    //throw new Exception("Errore:  corso di laurea non trovato");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok(s);
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
