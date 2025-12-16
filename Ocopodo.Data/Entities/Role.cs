using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();
    }
}
