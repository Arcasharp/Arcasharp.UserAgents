using System.Collections.ObjectModel;

namespace Arcasharp.UserAgents;

public static partial class UserAgents
{
    /// <summary>
    /// Returns any of the top user agents
    /// </summary>
    /// <returns>A user-agent</returns>
    public static string PickAny()
    {
        var index = ThreadSafeRandom.Next(0, TopUserAgentsCount);
        return TopUserAgents[index];
    }
    
    /// <summary>
    /// Returns the top user agent
    /// </summary>
    /// <returns>The top user-agent</returns>
    public static string GetTopUserAgent()
    {
        return TopUserAgents[0];
    }
    
    /// <summary>
    /// Returns all known top user agents
    /// </summary>
    /// <returns>A collection of user agents</returns>
    public static ReadOnlyCollection<string> GetTopUserAgents() 
        => TopUserAgentsCollection;
}