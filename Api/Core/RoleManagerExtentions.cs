using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core
{
    public static class RoleManagerExtentions
    {
        public static void Create(this RoleManager<UserRole> manager, UserRole role)
        {
            var task = System.Threading.Tasks.Task.Run(async () => { await manager.CreateAsync(role); });
            task.Wait();
            if (task.Exception != null)
                throw task.Exception;
        }
    }
}