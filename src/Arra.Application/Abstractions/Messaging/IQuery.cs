using Arra.SharedKernel;
using MediatR;

namespace Arra.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
