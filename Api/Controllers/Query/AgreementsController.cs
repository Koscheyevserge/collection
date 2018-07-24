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
    [Route("api/query/agreements")]
    public class AgreementsController : Controller
    {
        private CollectionContext _context;
        private IMapper _mapper;

        public AgreementsController(CollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public AgreementQM Get(int id)
        {
            return _mapper.Map<AgreementQM>(_context.Agreements.FirstOrDefault(c => c.Id == id));
        }

        [HttpGet()]
        public CollectionQM<AgreementQM> GetAll([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery]string filter = "", [FromQuery]bool withClient = false)
        {
            var agreements = _context.Agreements.AsNoTracking();
            if (withClient)
                agreements = agreements.Include(a => a.Client);
            if (!string.IsNullOrWhiteSpace(filter))
                agreements = agreements.Where(c => c.Code.ToLowerInvariant().Contains(filter.Trim().ToLowerInvariant()));
            var count = agreements.Count();
            if (pageNumber >= 0 && pageSize > 0)
                agreements = agreements.Skip(pageSize * pageNumber).Take(pageSize);
            return new CollectionQM<AgreementQM> { Values = _mapper.Map<IEnumerable<AgreementQM>>(agreements), Count = count };
        }

        [HttpGet("byclient/{clientId}")]
        public IEnumerable<AgreementQM> GetByClient(int clientId)
        {
            return _mapper.Map<IEnumerable<AgreementQM>>(_context.Agreements.Where(c => c.ClientId == clientId));
        }

        [HttpGet("byvehicle/{vehicleId}")]
        public IEnumerable<AgreementQM> GetByVehicle(int vehicleId)
        {
            return _mapper.Map<IEnumerable<AgreementQM>>(_context.Agreements.Where(c => c.VehicleId == vehicleId));
        }
    }
}
