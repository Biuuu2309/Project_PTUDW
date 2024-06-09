using System.Text.Json.Serialization;

namespace Project_UDW.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public object Data { get; set; } 
    }
}
