using System;
using System.Collections.Generic;

namespace WeeloBackEnd.Models
{
    public partial class Property
    {
        public Property()
        {
            PropertyImages = new HashSet<PropertyImage>();
            PropertyTraces = new HashSet<PropertyTrace>();
        }

        public int IdProperty { get; set; }
        public int IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = null!;
        public int Year { get; set; }

        public virtual Owner IdOwnerNavigation { get; set; } = null!;
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}
