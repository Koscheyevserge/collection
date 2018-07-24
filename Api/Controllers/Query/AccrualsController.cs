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
    [Route("api/query/accruals")]
    public class AccrualsController : Controller
    {
        private CollectionContext _context;
        private IMapper _mapper;

        public AccrualsController(CollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public AccrualQM Get(int id)
        {
            return _mapper.Map<AccrualQM>(_context.Accruals.FirstOrDefault(c => c.Id == id));
        }

        [HttpGet("byagreement/{agreementId}")]
        public IEnumerable<AccrualQM> GetByAgreement(int agreementId)
        {
            return _mapper.Map<IEnumerable<AccrualQM>>(_context.Accruals.Where(a => a.AgreementId == agreementId));
        }

        [HttpGet("byclient/{clientId}")]
        public IEnumerable<AccrualQM> GetByClient(int clientId)
        {
            return _mapper.Map<IEnumerable<AccrualQM>>(_context.Accruals.Where(a => a.Agreement.ClientId == clientId));
        }
    }
}
