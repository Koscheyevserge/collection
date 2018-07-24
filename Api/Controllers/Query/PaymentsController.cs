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
    [Route("api/query/payments")]
    public class PaymentsController : Controller
    {
        private CollectionContext _context;
        private IMapper _mapper;

        public PaymentsController(CollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public PaymentQM Get(int id)
        {
            return _mapper.Map<PaymentQM>(_context.Payments.FirstOrDefault(c => c.Id == id));
        }

        [HttpGet("byagreement/{agreementId}")]
        public IEnumerable<PaymentQM> GetByAgreement(int agreementId)
        {
            return _mapper.Map<IEnumerable<PaymentQM>>(_context.Payments.Where(a => a.AgreementId == agreementId));
        }

        [HttpGet("byclient/{clientId}")]
        public IEnumerable<PaymentQM> GetByClient(int clientId)
        {
            return _mapper.Map<IEnumerable<PaymentQM>>(_context.Payments.Where(a => a.Agreement.ClientId == clientId));
        }
    }
}
