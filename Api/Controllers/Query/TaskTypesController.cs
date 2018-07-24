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
    [Route("api/query/tasktypes")]
    public class TasksTypesController : Controller
    {
        private CollectionContext _context;
        private readonly IMapper mapper;

        public TasksTypesController(CollectionContext context, IMapper iMapper)
        {
            _context = context;
            mapper = iMapper;
        }

        [HttpGet()]
        public IEnumerable<TaskTypeQM> GetAll()
        {
            return mapper.Map<IEnumerable<TaskTypeQM>>(_context.TaskTypes.Include(tt => tt.Results));
        }
    }
}
