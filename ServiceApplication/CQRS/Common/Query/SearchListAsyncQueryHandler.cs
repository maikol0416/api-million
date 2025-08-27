using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServiceApplication.CQRS
{

    public record SearchListAsyncQuery<ENT, DTO>(string property, string value) : IRequest<List<DTO>>
    where ENT : class, new()
    where DTO : class, new();

    public class SearchListAsyncQueryHandler<ENT, DTO> : IRequestHandler<SearchListAsyncQuery<ENT, DTO>, List<DTO>>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public SearchListAsyncQueryHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<DTO>> Handle(SearchListAsyncQuery<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.SearchListModel(request.property, request.value);
        }
    }
}

