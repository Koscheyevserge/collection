using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    public class DevContext : DbContext
    {
        public DevContext(DbContextOptions<DevContext> options) : base(options)
        {

        }
    }
}