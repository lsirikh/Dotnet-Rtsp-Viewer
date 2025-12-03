using Ironwall.Dotnet.Libraries.Streaming.Base.Models;
using Ironwall.Dotnet.Libraries.Streaming.Models;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// StreamingOptions 기본값 테스트
/// TDD Plan: Phase 1.1 - StreamingOptions 기본값 변경
/// </summary>
public class StreamingOptionsTests
{
    #region B1.1.1 - NetworkCaching 기본값 테스트

    [Fact]
    public void StreamingOptions_DefaultNetworkCaching_ShouldBe150Ms()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.Equal(150, options.NetworkCaching);
    }

    #endregion

    #region B1.1.3 - FrameBufferSize 기본값 테스트

    [Fact]
    public void StreamingOptions_DefaultFrameBufferSize_ShouldBe250KB()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.Equal(250000, options.FrameBufferSize);
    }

    #endregion

    #region B1.1.5 - AllowFrameSkip 기본값 테스트

    [Fact]
    public void StreamingOptions_DefaultAllowFrameSkip_ShouldBeFalse()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.False(options.AllowFrameSkip);
    }

    #endregion

    #region B1.1.7 - ClockJitterMs 기본값 테스트 (신규 프로퍼티)

    [Fact]
    public void StreamingOptions_DefaultClockJitterMs_ShouldBe500()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.Equal(500, options.ClockJitterMs);
    }

    #endregion

    #region B1.1.9 - EnableClockSync 기본값 테스트 (신규 프로퍼티)

    [Fact]
    public void StreamingOptions_DefaultEnableClockSync_ShouldBeTrue()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.True(options.EnableClockSync);
    }

    #endregion

    #region B1.2.1 - CreateFastConnect 프로파일 테스트

    [Fact]
    public void StreamingOptions_CreateFastConnect_NetworkCachingShouldBe100()
    {
        // Arrange & Act
        var options = StreamingOptions.CreateFastConnect();

        // Assert
        Assert.Equal(100, options.NetworkCaching);
    }

    [Fact]
    public void StreamingOptions_CreateFastConnect_FrameBufferSizeShouldBe150KB()
    {
        // Arrange & Act
        var options = StreamingOptions.CreateFastConnect();

        // Assert
        Assert.Equal(150000, options.FrameBufferSize);
    }

    [Fact]
    public void StreamingOptions_CreateFastConnect_ConnectionTimeoutShouldBe5()
    {
        // Arrange & Act
        var options = StreamingOptions.CreateFastConnect();

        // Assert
        Assert.Equal(5, options.ConnectionTimeoutSeconds);
    }

    #endregion

    #region B1.2.3 - CreateStable 프로파일 테스트

    [Fact]
    public void StreamingOptions_CreateStable_MaxReconnectAttemptsShouldBe5()
    {
        // Arrange & Act
        var options = StreamingOptions.CreateStable();

        // Assert
        Assert.Equal(5, options.MaxReconnectAttempts);
    }

    [Fact]
    public void StreamingOptions_CreateStable_NetworkCachingShouldBe300()
    {
        // Arrange & Act
        var options = StreamingOptions.CreateStable();

        // Assert
        Assert.Equal(300, options.NetworkCaching);
    }

    [Fact]
    public void StreamingOptions_CreateStable_ExponentialBackoffShouldBeTrue()
    {
        // Arrange & Act
        var options = StreamingOptions.CreateStable();

        // Assert
        Assert.True(options.ExponentialBackoff);
    }

    #endregion

    #region B1.5.1 - 재연결 정책 테스트

    [Fact]
    public void StreamingOptions_DefaultMaxReconnectAttempts_ShouldBe5()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.Equal(5, options.MaxReconnectAttempts);
    }

    [Fact]
    public void StreamingOptions_DefaultReconnectDelaySeconds_ShouldBe2()
    {
        // Arrange & Act
        var options = new StreamingOptions();

        // Assert
        Assert.Equal(2, options.ReconnectDelaySeconds);
    }

    #endregion

    #region B1.2.5 - Clone 메서드 테스트

    [Fact]
    public void StreamingOptions_Clone_ShouldCopyClockJitterMs()
    {
        // Arrange
        var original = new StreamingOptions { ClockJitterMs = 999 };

        // Act
        var cloned = original.Clone();

        // Assert
        Assert.Equal(999, cloned.ClockJitterMs);
    }

    [Fact]
    public void StreamingOptions_Clone_ShouldCopyEnableClockSync()
    {
        // Arrange
        var original = new StreamingOptions { EnableClockSync = false };

        // Act
        var cloned = original.Clone();

        // Assert
        Assert.False(cloned.EnableClockSync);
    }

    #endregion
}
