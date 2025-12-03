using Ironwall.Dotnet.Libraries.Streaming.Serivces;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// 듀얼 버퍼 시스템 테스트
/// TDD Plan: Phase 4 - 듀얼 버퍼 시스템
/// </summary>
public class DualBufferTests
{
    #region B4.1.1 - LastFrameCache 기본 테스트

    [Fact]
    public void LastFrameCache_WhenCreated_ShouldHaveNoFrame()
    {
        // Arrange & Act
        var cache = new LastFrameCache();

        // Assert
        Assert.False(cache.HasFrame);
        Assert.Null(cache.GetLastFrame());
    }

    [Fact]
    public void LastFrameCache_CacheFrame_ShouldStoreFrame()
    {
        // Arrange
        var cache = new LastFrameCache();
        var frameData = new byte[] { 1, 2, 3, 4, 5 };

        // Act
        cache.CacheFrame(frameData, 1920, 1080);

        // Assert
        Assert.True(cache.HasFrame);
        Assert.Equal(1920, cache.Width);
        Assert.Equal(1080, cache.Height);
    }

    [Fact]
    public void LastFrameCache_GetLastFrame_ShouldReturnCachedFrame()
    {
        // Arrange
        var cache = new LastFrameCache();
        var frameData = new byte[] { 1, 2, 3, 4, 5 };
        cache.CacheFrame(frameData, 1920, 1080);

        // Act
        var result = cache.GetLastFrame();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(frameData.Length, result.Length);
    }

    [Fact]
    public void LastFrameCache_CacheFrame_ShouldUpdateTimestamp()
    {
        // Arrange
        var cache = new LastFrameCache();
        var frameData = new byte[] { 1, 2, 3, 4, 5 };
        var before = DateTime.Now;

        // Act
        cache.CacheFrame(frameData, 1920, 1080);

        // Assert
        var after = DateTime.Now;
        Assert.InRange(cache.LastUpdated, before, after);
    }

    [Fact]
    public void LastFrameCache_CacheFrame_ShouldReplaceOldFrame()
    {
        // Arrange
        var cache = new LastFrameCache();
        var frame1 = new byte[] { 1, 2, 3 };
        var frame2 = new byte[] { 4, 5, 6, 7, 8 };

        // Act
        cache.CacheFrame(frame1, 1280, 720);
        cache.CacheFrame(frame2, 1920, 1080);

        // Assert
        var result = cache.GetLastFrame();
        Assert.Equal(frame2.Length, result?.Length);
        Assert.Equal(1920, cache.Width);
        Assert.Equal(1080, cache.Height);
    }

    [Fact]
    public void LastFrameCache_Clear_ShouldRemoveFrame()
    {
        // Arrange
        var cache = new LastFrameCache();
        cache.CacheFrame(new byte[] { 1, 2, 3 }, 1920, 1080);

        // Act
        cache.Clear();

        // Assert
        Assert.False(cache.HasFrame);
        Assert.Null(cache.GetLastFrame());
    }

    [Fact]
    public void LastFrameCache_IsExpired_WhenNotExpired_ShouldReturnFalse()
    {
        // Arrange
        var cache = new LastFrameCache();
        cache.CacheFrame(new byte[] { 1, 2, 3 }, 1920, 1080);

        // Act
        var isExpired = cache.IsExpired(TimeSpan.FromSeconds(30));

        // Assert
        Assert.False(isExpired);
    }

    [Fact]
    public void LastFrameCache_IsExpired_WhenNoFrame_ShouldReturnTrue()
    {
        // Arrange
        var cache = new LastFrameCache();

        // Act
        var isExpired = cache.IsExpired(TimeSpan.FromSeconds(30));

        // Assert
        Assert.True(isExpired);
    }

    #endregion

    #region B4.2.1 - FrameBuffer 상태 테스트

    [Fact]
    public void LastFrameCache_FrameAge_ShouldReturnCorrectAge()
    {
        // Arrange
        var cache = new LastFrameCache();
        cache.CacheFrame(new byte[] { 1, 2, 3 }, 1920, 1080);

        // Act
        System.Threading.Thread.Sleep(100);
        var age = cache.FrameAge;

        // Assert
        Assert.True(age.TotalMilliseconds >= 100);
    }

    [Fact]
    public void LastFrameCache_FrameCount_ShouldIncrementOnCache()
    {
        // Arrange
        var cache = new LastFrameCache();

        // Act
        cache.CacheFrame(new byte[] { 1 }, 1920, 1080);
        cache.CacheFrame(new byte[] { 2 }, 1920, 1080);
        cache.CacheFrame(new byte[] { 3 }, 1920, 1080);

        // Assert
        Assert.Equal(3, cache.FrameCount);
    }

    [Fact]
    public void LastFrameCache_GetStatus_ShouldReturnCorrectStatus()
    {
        // Arrange
        var cache = new LastFrameCache();
        cache.CacheFrame(new byte[] { 1, 2, 3, 4 }, 1920, 1080);

        // Act
        var status = cache.GetStatus();

        // Assert
        Assert.True(status.HasFrame);
        Assert.Equal(1920, status.Width);
        Assert.Equal(1080, status.Height);
        Assert.Equal(4, status.FrameSizeBytes);
        Assert.Equal(1, status.FrameCount);
    }

    #endregion
}
