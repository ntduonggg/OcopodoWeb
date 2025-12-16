using Microsoft.AspNetCore.Identity;
using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FullName { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();
    }
}
