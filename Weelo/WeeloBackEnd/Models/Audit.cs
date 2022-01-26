using System;
using System.Collections.Generic;

namespace WeeloBackEnd.Models
{
    public partial class Audit
    {
        public int IdAudit { get; set; }
        public int IdProperty { get; set; }
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; } = null!;
        public string? Error { get; set; }

        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
