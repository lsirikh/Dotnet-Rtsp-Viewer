using Ironwall.Dotnet.Libraries.Streaming.Models;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// StreamingSetupModel 테스트
/// TDD Plan: Phase 1.3 - LibVLC 초기화 옵션 개선
/// </summary>
public class StreamingSetupModelTests
{
    #region B1.3.1 - ClockJitterMs 프로퍼티 테스트

    [Fact]
    public void StreamingSetupModel_DefaultClockJitterMs_ShouldBe500()
    {
        // Arrange & Act
        var model = new StreamingSetupModel();

        // Assert
        Assert.Equal(500, model.ClockJitterMs);
    }

    #endregion

    #region B1.3.2 - EnableClockSync 프로퍼티 테스트

    [Fact]
    public void StreamingSetupModel_DefaultEnableClockSync_ShouldBeTrue()
    {
        // Arrange & Act
        var model = new StreamingSetupModel();

        // Assert
        Assert.True(model.EnableClockSync);
    }

    #endregion

    #region B1.5.5 - TimeoutSeconds 테스트

    [Fact]
    public void StreamingSetupModel_DefaultTimeoutSeconds_ShouldBe15()
    {
        // Arrange & Act
        var model = new StreamingSetupModel();

        // Assert
        Assert.Equal(15, model.TimeoutSeconds);
    }

    #endregion
}
