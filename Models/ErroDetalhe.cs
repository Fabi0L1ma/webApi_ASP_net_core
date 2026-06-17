using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class ErroDetalhe
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Trace { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
