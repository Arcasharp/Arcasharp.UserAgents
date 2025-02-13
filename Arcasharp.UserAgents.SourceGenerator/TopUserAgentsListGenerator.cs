using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Arcasharp.UserAgents.SourceGenerator;

[Generator]
public class TopUerAgentsListGenerator : IIncrementalGenerator
{
    private const string Domain = "raw.githubusercontent.com";
    private const string Url = "/microlinkhq/top-user-agents/refs/heads/master/src/desktop.json";
    private static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new UriBuilder("https", Domain).Uri
    };
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(AddTopUserAgentsList);
    }

    private static void AddTopUserAgentsList(IncrementalGeneratorPostInitializationContext context)
    {
        var userAgents = ReadTopUserAgents();
        if (!userAgents.Any())
        {
            throw new InvalidOperationException("Failed to read user agents");
        }
        var source = GenerateSource(userAgents);
        context.AddSource("UserAgents+TopUserAgents.g.cs", source);
    }

    private static string[] ReadTopUserAgents()
    {
        var response = HttpClient
            .GetAsync(Url)
            .GetAwaiter()
            .GetResult();

        response.EnsureSuccessStatusCode();
        
        using var content = response.Content
            .ReadAsStreamAsync()
            .GetAwaiter()
            .GetResult();
        
        var deserialize = new DataContractJsonSerializer(typeof(string[]));
        return (string[]) deserialize.ReadObject(content);
    }
    
    /// <summary>
    /// Attempt to sanitize the user agent string in case someone tries to inject code by highjacking the repository
    /// </summary>
    private static string SanitizeUserAgent(string userAgent)
    {
        return SymbolDisplay.FormatLiteral(userAgent, quote: true);
    }

    private static string GenerateSource(string[] topUserAgents)
    {
        var userAgents = new StringBuilder();
        
        foreach (var userAgent in topUserAgents)
        {
            userAgents.Append($"         {SanitizeUserAgent(userAgent)},\n");
        }

        // Remove the last comma and newline
        userAgents.Remove(userAgents.Length - 2, 2);
        
        /* language=cs */
        var source = $$"""
                         using global::System.Collections.ObjectModel;
                         using global::System.Text;

                         namespace Arcasharp.UserAgents;

                         public static partial class UserAgents
                         {
                             private static readonly string[] TopUserAgents = new string[] {
                         {{userAgents}}
                             };
                             
                             private static readonly ReadOnlyCollection<string> TopUserAgentsCollection = new(TopUserAgents);
                                 
                             private const int TopUserAgentsCount = {{topUserAgents.Length}};
                         }
                         """;
        return source;
    }
}