using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServiceApplication.CQRS
{

    public record ToListAsyncQuery<ENT, DTO>() : IRequest<List<DTO>>
    where ENT : class, new()
    where DTO : class, new();

    public class ToListAsyncQueryHandler<ENT, DTO> : IRequestHandler<ToListAsyncQuery<ENT, DTO>, List<DTO>>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public ToListAsyncQueryHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<DTO>> Handle(ToListAsyncQuery<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.TolistModel();
        }
    }
}

