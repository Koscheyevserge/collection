using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Initializers
{
    public interface IDbInitializer
    {
        void Initialize();
    }
}
