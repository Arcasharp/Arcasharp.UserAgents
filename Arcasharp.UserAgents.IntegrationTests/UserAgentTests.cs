namespace Arcasharp.UserAgents.IntegrationTests;

public class UserAgentTests
{
    [Fact]
    public void Read_user_agent_returns_valid_value()
    {
        // Arrange
        
        // Act
        var topUserAgents = UserAgents.GetTopUserAgents();
        
        // Assert
        Assert.All(topUserAgents, Assert.NotNull);
        Assert.All(topUserAgents, Assert.NotEmpty);
    }
}