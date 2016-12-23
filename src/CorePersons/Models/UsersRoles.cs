using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorePersons.Models
{
    public class UsersRoles
    {
        public List<SelectListItem> userRoles;
        public UsersRoles()
        {
            userRoles = new List<SelectListItem>();
        }
        public List<SelectListItem> getRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();
            foreach (var Data in roles)
            {
                userRoles.Add(new SelectListItem()
                {
                    Value = Data.Id,
                    Text = Data.Name
                });
            }
            return userRoles;
        }
    }
}
