using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ServiceApplication.CQRS
{
    public record DeleteAsyncCommand<ENT, DTO>(string id) : IRequest<bool>
        where ENT : class, new()
        where DTO : class, new();

    public class DeleteAsyncCommandHandler<ENT, DTO> : IRequestHandler<DeleteAsyncCommand<ENT, DTO>, bool>
        where ENT : class, new()
        where DTO : class, new()
    {
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

        public DeleteAsyncCommandHandler(IBaseServiceApplication<ENT, DTO> implementation)
        {
            _implementation = implementation;
        }

        public async Task<bool> Handle(DeleteAsyncCommand<ENT, DTO> request, CancellationToken cancellationToken)
        {
            return await _implementation.DeleteModel(request.id);
        }
    }
}

