using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServiceApplication.CQRS
{

    public record SearchAsyncQuery<ENT, DTO>(string property, string value) : IRequest<DTO>
    where ENT : class, new()
    where DTO : class, new();

    public class SearchAsyncQueryHandler<ENT, DTO> : IRequestHandler<SearchAsyncQuery<ENT, DTO>, DTO>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public SearchAsyncQueryHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<DTO> Handle(SearchAsyncQuery<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.SearchModel(request.property, request.value);
        }
    }
}

