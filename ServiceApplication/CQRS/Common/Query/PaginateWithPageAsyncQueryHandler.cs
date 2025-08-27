using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Util.Common;

namespace ServiceApplication.CQRS
{
    public record PaginateWithPageAsyncQuery<ENT, DTO>(int page, int pages) : IRequest<Paginate<DTO>>
   where ENT : class, new()
   where DTO : class, new();

    public class PaginateWithPageAsyncQueryHandler<ENT, DTO> : IRequestHandler<PaginateWithPageAsyncQuery<ENT, DTO>, Paginate<DTO>>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public PaginateWithPageAsyncQueryHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<Paginate<DTO>> Handle(PaginateWithPageAsyncQuery<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.Paginate(request.page, request.pages);
        }
    }
}

