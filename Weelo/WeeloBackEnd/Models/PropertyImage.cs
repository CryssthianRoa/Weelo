using System;
using System.Collections.Generic;

namespace WeeloBackEnd.Models
{
    public partial class PropertyImage
    {
        public PropertyImage()
        {
            this.IdPropertyNavigation = null;
        }

        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; } = null!;
        public bool Enabled { get; set; }

        public virtual Property IdPropertyNavigation { get; set; } = null!;
    }
}
