using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class AuthOptions
    {
        public const string ISSUER = "Collection";
        public const string AUDIENCE = "Collection";
        private const string KEY = "Ckbirjvlkbyysqljkubqctrhtnysqrk.xghbdtnvbh";
        public const int LIFETIME = 1440;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
