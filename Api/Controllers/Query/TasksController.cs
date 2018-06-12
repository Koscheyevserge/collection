using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Query;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/query/tasks")]
    public class TasksController : Controller
    {
        private DbContext _context;

        public TasksController(DbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public IEnumerable<TaskQM> GetAll()
        {
            return new TaskQM[] { 
                new ClientTaskQM { Id = 1, Description = "Позвонить клиенту",
                    Title = "Позвонить клиенту", Date = new DateTime(2018, 6, 8) },
                new ClientTaskQM { Id = 2, Description = "Позвонить клиенту",
                    Title = "Позвонить клиенту", Date = new DateTime(2018, 6, 8) },
            };
        }
        
        [HttpGet("{id}")]
        public TaskQM Get(int id)
        {
            return new ClientTaskQM
            {
                Id = id,
                Description = "Позвонить клиенту",
                Title = "Позвонить клиенту",
                Date = new DateTime(2018, 6, 8)
            };
        }        
    }
}
