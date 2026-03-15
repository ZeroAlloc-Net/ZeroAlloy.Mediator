namespace ZeroAlloc.Mediator;

public interface IRequest<TResponse>;

public interface IRequest : IRequest<Unit>;
