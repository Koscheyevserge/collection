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

namespace Api.Controllers.Query
{
    [Produces("application/json")]
    [Route("api/query/clients")]
    public class ClientsController : Controller
    {
        private ULFContext _context;
        private IMapper _mapper;

        public ClientsController(ULFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ClientQM Get(int id)
        {
            return _mapper.Map<ClientQM>(_context.Clients.FirstOrDefault(c => c.Id == id));
        }
    }
}