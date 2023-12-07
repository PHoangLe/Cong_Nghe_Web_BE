using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ApiResponse<T>
    {
        public int statusCode { get; set; } = 200;
        public T? data { get; set; }
        public string? message { get; set; } = "OK";
        public string? errorMessage { get; set; }
    }
}
