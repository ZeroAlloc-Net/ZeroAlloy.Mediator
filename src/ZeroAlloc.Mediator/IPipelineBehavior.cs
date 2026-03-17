namespace ZeroAlloc.Mediator;

/// <summary>
/// Marker interface for ZeroAlloc.Mediator pipeline behaviors.
/// Extend this (or use <see cref="ZeroAlloc.Pipeline.IPipelineBehavior"/> directly)
/// and decorate with <see cref="PipelineBehaviorAttribute"/>.
/// </summary>
public interface IPipelineBehavior : ZeroAlloc.Pipeline.IPipelineBehavior;
