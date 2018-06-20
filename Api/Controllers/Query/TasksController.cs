using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Query;
using AutoMapper;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/query/tasks")]
    public class TasksController : Controller
    {
        private ULFContext _context;
        private readonly IMapper mapper;

        public TasksController(ULFContext context, IMapper iMapper)
        {
            _context = context;
            mapper = iMapper;
        }

        [HttpGet()]
        public IEnumerable<TaskQM> GetAll()
        {
            return mapper.Map<IEnumerable<TaskQM>>(_context.Tasks.Include(t => t.Type).Include(t => t.Result).Include(t => t.Client));
        }
        
        [HttpGet("{id}")]
        public TaskQM Get(int id)
        {
            var task = mapper.Map<TaskQM>(_context.Tasks.Include(t => t.Result).Include(t => t.Type)
                .ThenInclude(t => t.ResultsGroups).FirstOrDefault(t => t.Id == id));
            task.Type.ResultsGroups.ToList().ForEach(rg => rg.Items =
                mapper.Map<IEnumerable<TaskResultsItemQM>>(_context.TaskResultsItems.Where(tri => tri.ResultsGroupId == rg.Id)));
            return task;
        }        
    }
}
