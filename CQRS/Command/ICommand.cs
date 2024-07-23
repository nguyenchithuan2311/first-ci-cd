using ClassLibrary1.Shared;

namespace ClassLibrary1.Command;
using MediatR;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;