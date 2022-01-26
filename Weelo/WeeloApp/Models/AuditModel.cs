namespace WeeloApp.Models
{
    public class AuditModel
    {
        public int IdAudit { get; set; }
        public int IdProperty { get; set; }
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; } = null!;
        public string? Error { get; set; }

        //public virtual Property IdPropertyNavigation { get; set; } = null!;
        //public virtual Role IdRoleNavigation { get; set; } = null!;
        //public virtual User IdUserNavigation { get; set; } = null!;
    }
}
