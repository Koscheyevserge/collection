using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class UserManagerExtentions
    {
        public static User FindByName(this UserManager<User> manager, string username)
        {
            User user = null;
            var task = System.Threading.Tasks.Task.Run(async () => { user = await manager.FindByNameAsync(username); });
            if (task.Exception != null)
                throw task.Exception;
            return user;
        }

        public static void AddToRoles(this UserManager<User> manager, User user, IEnumerable<string> roles)
        {
            var task = System.Threading.Tasks.Task.Run(async () => { await manager.AddToRolesAsync(user, roles); });
            task.Wait();
            if (task.Exception != null)
                throw task.Exception;
        }
    }
}
