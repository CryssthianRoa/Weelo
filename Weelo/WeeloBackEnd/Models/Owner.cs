using System;
using System.Collections.Generic;

namespace WeeloBackEnd.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public int IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
