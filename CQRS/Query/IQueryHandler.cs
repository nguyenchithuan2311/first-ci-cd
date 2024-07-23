using ClassLibrary1.Shared;
using MediatR;

namespace ClassLibrary1.Query;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;