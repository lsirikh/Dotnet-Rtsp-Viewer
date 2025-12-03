using Ironwall.Dotnet.Libraries.Streaming.Serivces;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// 적응형 버퍼링 테스트
/// TDD Plan: Phase 3 - 적응형 버퍼링
/// </summary>
public class AdaptiveBufferTests
{
    #region B3.1.1 - NetworkMetrics 클래스 테스트

    [Fact]
    public void NetworkMetrics_WhenCreated_ShouldHaveDefaultValues()
    {
        // Arrange & Act
        var metrics = new NetworkMetrics();

        // Assert
        Assert.Equal(0, metrics.AverageJitterMs);
        Assert.Equal(0, metrics.PacketLossRate);
        Assert.Equal(0, metrics.SampleCount);
    }

    [Fact]
    public void NetworkMetrics_AddJitterSample_ShouldUpdateAverage()
    {
        // Arrange
        var metrics = new NetworkMetrics();

        // Act
        metrics.AddJitterSample(100);
        metrics.AddJitterSample(200);
        metrics.AddJitterSample(300);

        // Assert
        Assert.Equal(200, metrics.AverageJitterMs); // (100 + 200 + 300) / 3
        Assert.Equal(3, metrics.SampleCount);
    }

    [Fact]
    public void NetworkMetrics_Reset_ShouldClearAllValues()
    {
        // Arrange
        var metrics = new NetworkMetrics();
        metrics.AddJitterSample(100);
        metrics.AddJitterSample(200);

        // Act
        metrics.Reset();

        // Assert
        Assert.Equal(0, metrics.AverageJitterMs);
        Assert.Equal(0, metrics.SampleCount);
    }

    [Fact]
    public void NetworkMetrics_AddJitterSample_ShouldCapSampleCount()
    {
        // Arrange
        var metrics = new NetworkMetrics(maxSamples: 5);

        // Act - Add more than max samples
        for (int i = 0; i < 10; i++)
        {
            metrics.AddJitterSample(100);
        }

        // Assert - Should only keep last 5 samples
        Assert.Equal(5, metrics.SampleCount);
    }

    [Fact]
    public void NetworkMetrics_IsNetworkStable_WhenLowJitter_ShouldReturnTrue()
    {
        // Arrange
        var metrics = new NetworkMetrics();
        metrics.AddJitterSample(10);
        metrics.AddJitterSample(15);
        metrics.AddJitterSample(12);

        // Act
        var isStable = metrics.IsNetworkStable(threshold: 50);

        // Assert
        Assert.True(isStable);
    }

    [Fact]
    public void NetworkMetrics_IsNetworkStable_WhenHighJitter_ShouldReturnFalse()
    {
        // Arrange
        var metrics = new NetworkMetrics();
        metrics.AddJitterSample(100);
        metrics.AddJitterSample(150);
        metrics.AddJitterSample(120);

        // Act
        var isStable = metrics.IsNetworkStable(threshold: 50);

        // Assert
        Assert.False(isStable);
    }

    #endregion

    #region B3.2.1-B3.2.3 - AdaptiveBufferManager 테스트

    [Fact]
    public void AdaptiveBufferManager_WhenCreated_ShouldHaveDefaultCaching()
    {
        // Arrange & Act
        var manager = new AdaptiveBufferManager(baseCaching: 150);

        // Assert
        Assert.Equal(150, manager.CurrentCaching);
    }

    [Fact]
    public void AdaptiveBufferManager_WithHighJitter_ShouldIncreaseCaching()
    {
        // Arrange
        var manager = new AdaptiveBufferManager(baseCaching: 150, minCaching: 100, maxCaching: 500);

        // Act - Simulate high jitter
        manager.UpdateMetrics(jitterMs: 100); // High jitter
        manager.UpdateMetrics(jitterMs: 120);
        manager.UpdateMetrics(jitterMs: 110);
        var newCaching = manager.CalculateOptimalCaching();

        // Assert - Caching should increase
        Assert.True(newCaching > 150);
    }

    [Fact]
    public void AdaptiveBufferManager_WithLowJitter_ShouldMaintainOrDecreaseCaching()
    {
        // Arrange
        var manager = new AdaptiveBufferManager(baseCaching: 300, minCaching: 100, maxCaching: 500);

        // Act - Simulate low jitter
        manager.UpdateMetrics(jitterMs: 10);
        manager.UpdateMetrics(jitterMs: 15);
        manager.UpdateMetrics(jitterMs: 12);
        var newCaching = manager.CalculateOptimalCaching();

        // Assert - Caching should decrease or stay same
        Assert.True(newCaching <= 300);
    }

    [Fact]
    public void AdaptiveBufferManager_ShouldNotExceedMaxCaching()
    {
        // Arrange
        var manager = new AdaptiveBufferManager(baseCaching: 150, minCaching: 100, maxCaching: 500);

        // Act - Simulate very high jitter
        for (int i = 0; i < 20; i++)
        {
            manager.UpdateMetrics(jitterMs: 500);
        }
        var newCaching = manager.CalculateOptimalCaching();

        // Assert
        Assert.True(newCaching <= 500);
    }

    [Fact]
    public void AdaptiveBufferManager_ShouldNotGoBelowMinCaching()
    {
        // Arrange
        var manager = new AdaptiveBufferManager(baseCaching: 150, minCaching: 100, maxCaching: 500);

        // Act - Simulate very low jitter
        for (int i = 0; i < 20; i++)
        {
            manager.UpdateMetrics(jitterMs: 1);
        }
        var newCaching = manager.CalculateOptimalCaching();

        // Assert
        Assert.True(newCaching >= 100);
    }

    [Fact]
    public void AdaptiveBufferManager_Reset_ShouldReturnToBaseCaching()
    {
        // Arrange
        var manager = new AdaptiveBufferManager(baseCaching: 150, minCaching: 100, maxCaching: 500);
        manager.UpdateMetrics(jitterMs: 200);
        manager.CalculateOptimalCaching();

        // Act
        manager.Reset();

        // Assert
        Assert.Equal(150, manager.CurrentCaching);
    }

    [Fact]
    public void AdaptiveBufferManager_IsAdaptationNeeded_WithSignificantChange_ShouldReturnTrue()
    {
        // Arrange
        var manager = new AdaptiveBufferManager(baseCaching: 150, minCaching: 100, maxCaching: 500);

        // Act - Simulate jitter change
        manager.UpdateMetrics(jitterMs: 200);
        manager.UpdateMetrics(jitterMs: 220);
        manager.UpdateMetrics(jitterMs: 210);

        // Assert
        Assert.True(manager.IsAdaptationNeeded());
    }

    #endregion
}
