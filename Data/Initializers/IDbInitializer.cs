using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Initializers
{
    public interface IDbInitializer
    {
        void Initialize();
    }
}
