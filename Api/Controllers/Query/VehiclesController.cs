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
    [Route("api/query/vehicles")]
    public class VehiclesController : Controller
    {
        private CollectionContext _context;
        private IMapper _mapper;

        public VehiclesController(CollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public CollectionQM<VehicleQM> GetAll([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery]string filter = "", [FromQuery]bool withAgreements = false)
        {
            var vehicles = _context.Vehicles.Include(v => v.Model).ThenInclude(m => m.Manufacturer).OrderBy(v => v.Number).AsNoTracking();
            if (withAgreements)
                vehicles = vehicles.Include(v => v.Agreements);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var strings = filter.Trim().ToLowerInvariant().Split(' ');
                vehicles = vehicles.Where(c => strings.Any(s => c.Number.ToLowerInvariant().Contains(s)
                || (c.Model != null && c.Model.Name.ToLowerInvariant().Contains(s))
                || (c.Model != null  && c.Model.Manufacturer != null && c.Model.Manufacturer.Name.ToLowerInvariant().Contains(s))));
            }
            var count = vehicles.Count();
            if (pageNumber >= 0 && pageSize > 0)
                vehicles = vehicles.Skip(pageSize * pageNumber).Take(pageSize);
            return new CollectionQM<VehicleQM> { Values = _mapper.Map<IEnumerable<VehicleQM>>(vehicles), Count = count };
        }

        [HttpGet("{id}")]
        public VehicleQM Get(int id)
        {
            return _mapper.Map<VehicleQM>(_context.Vehicles.Include(v => v.Model).ThenInclude(m => m.Manufacturer).FirstOrDefault(c => c.Id == id));
        }
    }
}
