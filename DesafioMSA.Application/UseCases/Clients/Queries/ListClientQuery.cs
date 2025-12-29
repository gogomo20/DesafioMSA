using DesafioMSA.Application.MyMediator.Interfaces;
using DesafioMSA.Application.Responses;
using DesafioMSA.Application.Shared;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Repositories.Views;
using DesafioMSA.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.UseCases.Clients.Queries
{
    public class ListClientQuery : BaseListQueryRequest, IRequest<ListQueryResponse<ICollection<ClientView>>>
    {
        public string? FantasyNameFilter { get; set; }
        public string? Cnpj { get; set; }
        public class ListClientQueryHandle : IRequestHandler<ListClientQuery, ListQueryResponse<ICollection<ClientView>>>
        {
            private readonly IClientRepository _repository;
            public ListClientQueryHandle(IClientRepository repository)
            {
                _repository = repository;
            }
            public async Task<ListQueryResponse<ICollection<ClientView>>> Handle(ListClientQuery query, CancellationToken cancellation)
            {
                try
                {
                    var queryDto = query.ToDto<ClientView>();
                    if (!string.IsNullOrEmpty(query.FantasyNameFilter))
                        queryDto.Where(x => x.FantasyName.Contains(query.FantasyNameFilter));
                    if (!string.IsNullOrEmpty(query.Cnpj))
                        queryDto.Where(x => x.Cnpj.Contains(query.Cnpj));
                    var consulta = await _repository.GetPagedAsync(queryDto);
                    return new ListQueryResponse<ICollection<ClientView>>
                    {
                        Items = consulta.Data,
                        Page = consulta.Page,
                        Size = query.Size,
                        TotalPages = (int)Math.Ceiling(
                                (double)consulta.TotalRegisters / (double)query.Size
                            ),
                        TotalRegisters = consulta.TotalRegisters,
                        Message = "Consulta realizada com sucesso!"
                    };
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
