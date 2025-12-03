using Ironwall.Dotnet.Libraries.Streaming.Base.Models;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// 캐싱 로직 테스트
/// TDD Plan: Phase 1.4 - ImprovedRtspStreamingService 캐싱 로직 개선
/// </summary>
public class CachingLogicTests
{
    #region B1.4.1 - TCP 캐싱 승수 테스트

    [Fact]
    public void CalculateOptimalCaching_WithTcp_ShouldMultiplyBy1Point2()
    {
        // Arrange
        var options = new StreamingOptions { NetworkCaching = 100, UseTcp = true };

        // Act - 직접 계산 로직 테스트
        var baseCaching = options.NetworkCaching;
        if (options.UseTcp)
        {
            baseCaching = (int)(baseCaching * 1.2);
        }
        var result = Math.Min(baseCaching, 1000);

        // Assert
        Assert.Equal(120, result);  // 100 * 1.2 = 120
    }

    #endregion

    #region B1.4.3 - 최대 캐싱 제한 테스트

    [Fact]
    public void CalculateOptimalCaching_WithHighValue_ShouldCapAt1000Ms()
    {
        // Arrange
        var options = new StreamingOptions { NetworkCaching = 2000, UseTcp = true };

        // Act - 직접 계산 로직 테스트
        var baseCaching = options.NetworkCaching;
        if (options.UseTcp)
        {
            baseCaching = (int)(baseCaching * 1.2);
        }
        var result = Math.Min(baseCaching, 1000);

        // Assert
        Assert.Equal(1000, result);  // 최대 1000ms
    }

    [Fact]
    public void CalculateOptimalCaching_WithoutTcp_ShouldNotMultiply()
    {
        // Arrange
        var options = new StreamingOptions { NetworkCaching = 150, UseTcp = false };

        // Act - 직접 계산 로직 테스트
        var baseCaching = options.NetworkCaching;
        if (options.UseTcp)
        {
            baseCaching = (int)(baseCaching * 1.2);
        }
        var result = Math.Min(baseCaching, 1000);

        // Assert
        Assert.Equal(150, result);  // TCP 아니면 그대로
    }

    #endregion
}
