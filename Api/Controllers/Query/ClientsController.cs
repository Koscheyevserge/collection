using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Query;
using AutoMapper;
using Data.Contexts;
using Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Query
{
    [Produces("application/json")]
    [Route("api/query/clients")]
    public class ClientsController : Controller
    {
        private CollectionContext _context;
        private IMapper _mapper;

        public ClientsController(CollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ClientQM Get(int id)
        {
            return _mapper.Map<ClientQM>(_context.Clients.FirstOrDefault(c => c.Id == id));
        }

        [HttpGet()]
        public CollectionQM<ClientQM> GetAll([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery]string filter = "", [FromQuery]bool withAgreements = false)
        {
            var clients = _context.Clients.AsNoTracking();
            if (withAgreements)
                clients = clients.Include(c => c.Agreements);
            if (!string.IsNullOrWhiteSpace(filter))
                clients = clients.Where(c => c.NameFull.ToLowerInvariant().Contains(filter.Trim().ToLowerInvariant()) || c.Name.ToLowerInvariant().Contains(filter.Trim().ToLowerInvariant()));
            var count = clients.Count();
            if (pageNumber >= 0 && pageSize > 0)
                clients = clients.Skip(pageSize * pageNumber).Take(pageSize);
            return new CollectionQM<ClientQM>{ Values = _mapper.Map<IEnumerable<ClientQM>>(clients), Count = count };
        }
    }
}