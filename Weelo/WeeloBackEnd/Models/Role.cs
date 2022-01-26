using System;
using System.Collections.Generic;

namespace WeeloBackEnd.Models
{
    public partial class Role
    {
        public Role()
        {
            Audits = new HashSet<Audit>();
            UserRoles = new HashSet<UserRole>();
        }

        public int IdRole { get; set; }
        public string Role1 { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Audit> Audits { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
