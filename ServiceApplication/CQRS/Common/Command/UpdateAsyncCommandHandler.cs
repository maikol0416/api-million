using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ServiceApplication.CQRS
{
    public record UpdateAsyncCommand<ENT, DTO>(DTO Dto) : IRequest<DTO>
        where ENT : class, new()
        where DTO : class, new();

    public class UpdateAsyncCommandHandler<ENT, DTO> : IRequestHandler<UpdateAsyncCommand<ENT, DTO>, DTO>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public UpdateAsyncCommandHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<DTO> Handle(UpdateAsyncCommand<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.UpdateModel(request.Dto);
        }
    }
}

