using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.Responses
{
    public class GenericResponseNoData
    {
        public bool Success => !Errors.Any();
        public string? Message { get; set; }
        public bool NotFounded { get; set; } = false;
        public string[] Errors { get; set; } = [];
    }
}
