# Arcasharp.UserAgents

Arcasharp.UserAgents is a C# library that provides a collection of top user agents and methods to retrieve them.
This package is based on the data provided by [top-user-agents](https://github.com/microlinkhq/top-user-agents).

## Getting Started

To get started with Arcasharp.UserAgents, follow these steps:

1. Install the package via NuGet:
    ```sh
    dotnet add package Arcasharp.UserAgents
    ```

2. Add the necessary using directive in your code:
    ```csharp
    using Arcasharp.UserAgents;
    ```

## Examples

### Pick any user-agent

Returns any of the top user-agents, at random.

```csharp
string userAgent = UserAgents.PickAny();
Console.WriteLine(userAgent);
```

### Get all user-agents

Returns all known top user-agents.

```csharp
ReadOnlyCollection<string> userAgents = UserAgents.GetTopUserAgents();
foreach (var userAgent in userAgents)
{
    Console.WriteLine(userAgent);
}
```

### Get the top user-agent

Returns the top user-agent.

```csharp
string topUserAgent = UserAgents.GetTopUserAgent();
Console.WriteLine(topUserAgent);
```

## Special thanks

Special thanks to [Kiko Beats](https://github.com/Kikobeats) and [top-user-agents contributors](https://github.com/microlinkhq/top-user-agents/graphs/contributors).