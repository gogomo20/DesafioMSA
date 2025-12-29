using DesafioMSA.Domain.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.Shared
{
    public class BaseListQueryRequest
    {
        public int Size { get; set
            {
                if(value > 500)
                {
                    Size = 500;
                    return;
                }
                Size = value;
            } } = 10;
        public int Page { get; set; } = 1;
        public bool Ascending { get; set; } = true;
        public string OrderBy { get; set; } = "Id";
        public ListQueryDto<TRequest> ToDto<TRequest>()
        {
            return new ListQueryDto<TRequest>
            {
                Size = Size,
                Page = Page,
                OrderBy = OrderBy,
                Ascending = Ascending,
            };
        }
    }
}
