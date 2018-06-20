using Api.Models.Query;
using AutoMapper;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
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
        private ULFContext _context;
        private IMapper _mapper;

        public AgreementsController(ULFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public AgreementQM Get(int id)
        {
            return _mapper.Map<AgreementQM>(_context.Agreements.FirstOrDefault(c => c.Id == id));
        }

        [HttpGet("byclient/{clientId}")]
        public IEnumerable<AgreementQM> GetByClient(int clientId)
        {
            return _mapper.Map<IEnumerable<AgreementQM>>(_context.Agreements.Where(c => c.ClientId == clientId));
        }
    }
}
