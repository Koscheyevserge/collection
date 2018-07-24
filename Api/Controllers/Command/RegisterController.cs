using Api.Interfaces;
using Api.Models.Command;
using AutoMapper;
using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Command
{
    [Produces("application/json")]
    [Route("api/command/register")]
    public class RegisterController : Controller, ICommand<RegisterCM>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly CollectionContext _context;

        public RegisterController(IMapper mapper, UserManager<User> userManager, CollectionContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Do([FromBody]RegisterCM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User { Email = model.Email, UserName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var key = Guid.NewGuid();
            return Ok(key);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Undo(Guid key)
        {
            throw new NotImplementedException();
        }
    }
}
