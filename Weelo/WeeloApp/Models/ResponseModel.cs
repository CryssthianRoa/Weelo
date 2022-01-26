using Newtonsoft.Json.Linq;

namespace WeeloApp.Models
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public JArray? Value { get; set; }
    }
}
