using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.Responses
{
    public class ListQueryResponse<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalRegisters { get; set; }
        public int TotalPages { get; set; }
        public required T Items { get; set; }
        public string? Message { get; set; }
    }
}
