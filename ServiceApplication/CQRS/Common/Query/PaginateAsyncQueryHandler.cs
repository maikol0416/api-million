using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Util.Common;

namespace ServiceApplication.CQRS
{

    public record PaginateAsyncQuery<ENT, DTO>(Paginate<DTO> paginado) : IRequest<Paginate<DTO>>
    where ENT : class, new()
    where DTO : class, new();

    public class PaginateAsyncQueryHandler<ENT, DTO> : IRequestHandler<PaginateAsyncQuery<ENT, DTO>, Paginate<DTO>>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public PaginateAsyncQueryHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<Paginate<DTO>> Handle(PaginateAsyncQuery<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.Paginate(request.paginado);
        }
    }
}

