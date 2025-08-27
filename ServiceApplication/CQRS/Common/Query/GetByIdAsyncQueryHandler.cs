using System;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record GetByIdAsyncQuery<ENT, DTO>(string Id) : IRequest<DTO>
        where ENT : class, new()
        where DTO : class, new();

    public class GetByIdAsyncQueryHandler<ENT, DTO> : IRequestHandler<GetByIdAsyncQuery<ENT, DTO>, DTO>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public GetByIdAsyncQueryHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<DTO> Handle(GetByIdAsyncQuery<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.GetById(request.Id);
        }
    }

}

