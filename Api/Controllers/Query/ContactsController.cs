using Api.Models.Query;
using AutoMapper;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Query
{
    [Produces("application/json")]
    [Route("api/query/contacts")]
    public class ContactsController : Controller
    {
        private CollectionContext _context;
        private IMapper _mapper;

        public ContactsController(CollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public CollectionQM<ContactQM> GetAll([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery]string filter = "")
        {
            var contacts = _context.Contacts.Include(c => c.Communications).OrderBy(c => c.Surname).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var strings = filter.Trim().ToLowerInvariant().Split(' ');
                contacts = contacts.Where(c => strings.Any(s => c.Name.ToLowerInvariant().Contains(s)
                || c.Surname.ToLowerInvariant().Contains(s)
                || c.Patronym.ToLowerInvariant().Contains(s)));
            }
            var count = contacts.Count();
            if (pageNumber >= 0 && pageSize > 0)
                contacts = contacts.Skip(pageSize * pageNumber).Take(pageSize);
            return new CollectionQM<ContactQM> { Values = _mapper.Map<IEnumerable<ContactQM>>(contacts), Count = count };
        }

        [HttpGet("{id}")]
        public ContactQM Get(int id)
        {
            return _mapper.Map<ContactQM>(_context.Contacts.Include(c => c.Communications).FirstOrDefault(c => c.Id == id));
        }

        [HttpGet("byclient/{clientId}")]
        public IEnumerable<ContactQM> GetByClient(int clientId)
        {
            return _mapper.Map<IEnumerable<ContactQM>>(_context.Contacts.Include(c => c.Communications).Where(a => a.ClientId == clientId));
        }
    }
}
