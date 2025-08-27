using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ServiceApplication.CQRS
{
	public record CreateAsyncCommand<ENT, DTO>(DTO Dto) : IRequest<DTO>
		where ENT : class, new()
		where DTO : class, new();

	public class CreateAsyncCommandHandler<ENT, DTO> : IRequestHandler<CreateAsyncCommand<ENT, DTO>, DTO>
        where ENT : class, new()
        where DTO : class, new()
	{
        protected readonly IBaseServiceApplication<ENT, DTO> _implementation;

		public CreateAsyncCommandHandler(IBaseServiceApplication<ENT, DTO> implementation)
		{
			_implementation = implementation;
		}

		public async Task<DTO> Handle(CreateAsyncCommand<ENT, DTO> request, CancellationToken cancellationToken)
		{
			return await _implementation.CreateModel(request.Dto);
		}
	}
}

