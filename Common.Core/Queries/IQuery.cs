using MediatR;
namespace Common.Core.Queries;
public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : notnull { }