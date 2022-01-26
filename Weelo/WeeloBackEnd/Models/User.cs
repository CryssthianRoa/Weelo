using System;
using System.Collections.Generic;

namespace WeeloBackEnd.Models
{
    public partial class User
    {
        public User()
        {
            Audits = new HashSet<Audit>();
            UserRoles = new HashSet<UserRole>();
        }

        public int IdUser { get; set; }
        public string User1 { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Enable { get; set; }

        public virtual ICollection<Audit> Audits { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
