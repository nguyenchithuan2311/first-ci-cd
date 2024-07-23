using ClassLibrary1.Shared;
using MediatR;

namespace ClassLibrary1.Query;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;