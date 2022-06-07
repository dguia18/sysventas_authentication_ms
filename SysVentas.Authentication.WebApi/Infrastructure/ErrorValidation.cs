using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
namespace SysVentas.Authentication.WebApi.Infrastructure
{
    public class ErrorValidation
    {
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationFailure> Errors { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
