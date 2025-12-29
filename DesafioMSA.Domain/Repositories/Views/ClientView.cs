using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Domain.Repositories.Views
{
    public class ClientView
    {
        public long Id { get; set; }
        public required string FantasyName { get; set; }
        public required string Cnpj { get; set; }
        public bool Active { get; set; }

    }
}
