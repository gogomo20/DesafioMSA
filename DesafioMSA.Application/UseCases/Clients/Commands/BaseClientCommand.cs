using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.UseCases.Clients.Commands
{
    public abstract class BaseClientCommand 
    {
        public string? FantasyName { get; set; }
        public string? Cnpj { get; set; }

    }
}
