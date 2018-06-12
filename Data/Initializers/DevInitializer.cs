using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Initializers
{
    public class DevInitializer : IDbInitializer
    {
        private DevContext _context;

        public DevInitializer(DevContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();
        }
    }
}
