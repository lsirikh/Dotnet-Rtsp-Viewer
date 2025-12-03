using Ironwall.Dotnet.Libraries.Streaming.Base.Models;
using Ironwall.Dotnet.Libraries.Streaming.Models;
using Ironwall.Dotnet.Libraries.Streaming.Serivces;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// StreamPrewarmer 테스트
/// TDD Plan: Phase 2 - Pre-Connection 시스템
/// </summary>
public class StreamPrewarmerTests
{
    #region B2.1.1 - IStreamPrewarmer 인터페이스 테스트

    [Fact]
    public void IStreamPrewarmer_Interface_ShouldExist()
    {
        // Arrange & Act
        var type = typeof(IStreamPrewarmer);

        // Assert
        Assert.NotNull(type);
        Assert.True(type.IsInterface);
    }

    [Fact]
    public void IStreamPrewarmer_ShouldHavePrewarmAsyncMethod()
    {
        // Arrange
        var type = typeof(IStreamPrewarmer);

        // Act
        var method = type.GetMethod("PrewarmAsync");

        // Assert
        Assert.NotNull(method);
    }

    [Fact]
    public void IStreamPrewarmer_ShouldHaveTryGetPrewarmedContextMethod()
    {
        // Arrange
        var type = typeof(IStreamPrewarmer);

        // Act
        var method = type.GetMethod("TryGetPrewarmedContext");

        // Assert
        Assert.NotNull(method);
    }

    [Fact]
    public void IStreamPrewarmer_ShouldHaveCleanupStalePrewarmsMethod()
    {
        // Arrange
        var type = typeof(IStreamPrewarmer);

        // Act
        var method = type.GetMethod("CleanupStalePrewarms");

        // Assert
        Assert.NotNull(method);
    }

    #endregion

    #region B2.2.1 - PrewarmedContext 테스트

    [Fact]
    public void PrewarmedContext_WhenCreated_ShouldSetCreatedAt()
    {
        // Arrange
        var before = DateTime.Now;

        // Act
        var context = new PrewarmedContext("test");

        // Assert
        var after = DateTime.Now;
        Assert.InRange(context.CreatedAt, before, after);
    }

    [Fact]
    public void PrewarmedContext_WhenCreated_ShouldSetContextId()
    {
        // Arrange & Act
        var context = new PrewarmedContext("test-context-id");

        // Assert
        Assert.Equal("test-context-id", context.ContextId);
    }

    [Fact]
    public void PrewarmedContext_IsExpired_WhenNotExpired_ShouldReturnFalse()
    {
        // Arrange
        var context = new PrewarmedContext("test");

        // Act
        var isExpired = context.IsExpired(TimeSpan.FromSeconds(30));

        // Assert
        Assert.False(isExpired);
    }

    [Fact]
    public void PrewarmedContext_IsExpired_WhenExpired_ShouldReturnTrue()
    {
        // Arrange
        var context = new PrewarmedContext("test")
        {
            CreatedAt = DateTime.Now.AddSeconds(-31)
        };

        // Act
        var isExpired = context.IsExpired(TimeSpan.FromSeconds(30));

        // Assert
        Assert.True(isExpired);
    }

    #endregion

    #region B2.3.1-B2.3.8 - StreamPrewarmer 서비스 테스트

    [Fact]
    public async Task PrewarmAsync_WhenCalled_ShouldStoreContext()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer();
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };

        // Act
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);

        // Assert
        Assert.Equal(1, prewarmer.PrewarmedCount);
    }

    [Fact]
    public async Task PrewarmAsync_WithMultipleContexts_ShouldStoreAll()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer();
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };

        // Act
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);
        await prewarmer.PrewarmAsync("ctx2", connectionInfo);
        await prewarmer.PrewarmAsync("ctx3", connectionInfo);

        // Assert
        Assert.Equal(3, prewarmer.PrewarmedCount);
    }

    [Fact]
    public async Task TryGetPrewarmedContext_WhenExists_ShouldReturnTrueAndContext()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer();
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);

        // Act
        var result = prewarmer.TryGetPrewarmedContext("ctx1", out var context);

        // Assert
        Assert.True(result);
        Assert.NotNull(context);
        Assert.Equal("ctx1", context.ContextId);
    }

    [Fact]
    public async Task TryGetPrewarmedContext_WhenExists_ShouldRemoveFromCache()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer();
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);

        // Act
        prewarmer.TryGetPrewarmedContext("ctx1", out _);

        // Assert
        Assert.Equal(0, prewarmer.PrewarmedCount);
    }

    [Fact]
    public void TryGetPrewarmedContext_WhenNotExists_ShouldReturnFalse()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer();

        // Act
        var result = prewarmer.TryGetPrewarmedContext("nonexistent", out var context);

        // Assert
        Assert.False(result);
        Assert.Null(context);
    }

    [Fact]
    public async Task PrewarmAsync_WhenMaxReached_ShouldRemoveOldest()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer(maxPrewarmedContexts: 3);
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };

        // Act - Add 4 contexts (max is 3)
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);
        await Task.Delay(10); // Ensure different timestamps
        await prewarmer.PrewarmAsync("ctx2", connectionInfo);
        await Task.Delay(10);
        await prewarmer.PrewarmAsync("ctx3", connectionInfo);
        await Task.Delay(10);
        await prewarmer.PrewarmAsync("ctx4", connectionInfo);

        // Assert
        Assert.Equal(3, prewarmer.PrewarmedCount);
        // ctx1 (oldest) should be removed
        Assert.False(prewarmer.TryGetPrewarmedContext("ctx1", out _));
        Assert.True(prewarmer.TryGetPrewarmedContext("ctx2", out _));
    }

    [Fact]
    public async Task CleanupStalePrewarms_WithExpiredItems_ShouldRemoveThem()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer(staleTimeoutSeconds: 1);
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);

        // Wait for expiration
        await Task.Delay(1100);

        // Act
        var removedCount = prewarmer.CleanupStalePrewarms();

        // Assert
        Assert.Equal(1, removedCount);
        Assert.Equal(0, prewarmer.PrewarmedCount);
    }

    [Fact]
    public async Task CleanupStalePrewarms_WithValidItems_ShouldNotRemoveThem()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer(staleTimeoutSeconds: 30);
        var connectionInfo = new RtspConnectionInfo { Url = "rtsp://test" };
        await prewarmer.PrewarmAsync("ctx1", connectionInfo);

        // Act
        var removedCount = prewarmer.CleanupStalePrewarms();

        // Assert
        Assert.Equal(0, removedCount);
        Assert.Equal(1, prewarmer.PrewarmedCount);
    }

    [Fact]
    public async Task PrewarmAsync_WithSameContextId_ShouldUpdateExisting()
    {
        // Arrange
        var prewarmer = new StreamPrewarmer();
        var connectionInfo1 = new RtspConnectionInfo { Url = "rtsp://test1" };
        var connectionInfo2 = new RtspConnectionInfo { Url = "rtsp://test2" };

        // Act
        await prewarmer.PrewarmAsync("ctx1", connectionInfo1);
        await prewarmer.PrewarmAsync("ctx1", connectionInfo2);

        // Assert
        Assert.Equal(1, prewarmer.PrewarmedCount);
        prewarmer.TryGetPrewarmedContext("ctx1", out var context);
        Assert.Equal("rtsp://test2", context?.ConnectionInfo?.Url);
    }

    #endregion
}
