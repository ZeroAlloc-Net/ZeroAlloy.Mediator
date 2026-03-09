using ZMediator;
using System.Runtime.CompilerServices;

// === Request/Response ===
var pong = await Mediator.Send(new Ping("Hello ZMediator!"), CancellationToken.None);
Console.WriteLine(pong);

// === Notification ===
await Mediator.Publish(new UserCreated(42, "Alice"), CancellationToken.None);

// === Streaming ===
Console.Write("Counting: ");
await foreach (var n in Mediator.CreateStream(new CountTo(5), CancellationToken.None))
{
    Console.Write($"{n} ");
}
Console.WriteLine();

// === Request types ===
public readonly record struct Ping(string Message) : IRequest<string>;
public readonly record struct UserCreated(int Id, string Name) : INotification;
public readonly record struct CountTo(int Max) : IStreamRequest<int>;

// === Handlers ===
public class PingHandler : IRequestHandler<Ping, string>
{
    public ValueTask<string> Handle(Ping request, CancellationToken ct)
        => ValueTask.FromResult($"Pong: {request.Message}");
}

public class UserCreatedLogger : INotificationHandler<UserCreated>
{
    public ValueTask Handle(UserCreated notification, CancellationToken ct)
    {
        Console.WriteLine($"User created: {notification.Name} (ID: {notification.Id})");
        return ValueTask.CompletedTask;
    }
}

public class CountToHandler : IStreamRequestHandler<CountTo, int>
{
    public async IAsyncEnumerable<int> Handle(
        CountTo request,
        [EnumeratorCancellation] CancellationToken ct)
    {
        for (var i = 1; i <= request.Max; i++)
            yield return i;
    }
}
