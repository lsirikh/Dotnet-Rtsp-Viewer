using Ironwall.Dotnet.Libraries.Streaming.Models;

namespace Ironwall.Dotnet.Libraries.Streaming.Tests;

/// <summary>
/// PlaybackState 상태 머신 테스트
/// TDD Plan: Phase 5 - 상태 머신 개선
/// </summary>
public class PlaybackStateTests
{
    #region B5.1.1-B5.1.4 - 새 상태 존재 확인

    [Fact]
    public void PlaybackState_Prewarming_ShouldExist()
    {
        // Arrange & Act
        var state = PlaybackState.Prewarming;

        // Assert
        Assert.Equal(11, (int)state);
    }

    [Fact]
    public void PlaybackState_Ready_ShouldExist()
    {
        // Arrange & Act
        var state = PlaybackState.Ready;

        // Assert
        Assert.Equal(12, (int)state);
    }

    [Fact]
    public void PlaybackState_Degraded_ShouldExist()
    {
        // Arrange & Act
        var state = PlaybackState.Degraded;

        // Assert
        Assert.Equal(13, (int)state);
    }

    #endregion

    #region B5.2.1-B5.2.3 - 상태 전이 테스트

    [Fact]
    public void PlaybackStateTransition_IsValidTransition_FromPrewarmingToReady_ShouldBeTrue()
    {
        // Arrange & Act
        var isValid = PlaybackStateTransition.IsValidTransition(
            PlaybackState.Prewarming,
            PlaybackState.Ready);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void PlaybackStateTransition_IsValidTransition_FromReadyToPlaying_ShouldBeTrue()
    {
        // Arrange & Act
        var isValid = PlaybackStateTransition.IsValidTransition(
            PlaybackState.Ready,
            PlaybackState.Playing);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void PlaybackStateTransition_IsValidTransition_FromPlayingToDegraded_ShouldBeTrue()
    {
        // Arrange & Act
        var isValid = PlaybackStateTransition.IsValidTransition(
            PlaybackState.Playing,
            PlaybackState.Degraded);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void PlaybackStateTransition_IsValidTransition_FromDegradedToPlaying_ShouldBeTrue()
    {
        // Arrange & Act
        var isValid = PlaybackStateTransition.IsValidTransition(
            PlaybackState.Degraded,
            PlaybackState.Playing);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void PlaybackStateTransition_IsValidTransition_FromNoneToPlaying_ShouldBeFalse()
    {
        // Arrange & Act - Direct jump from None to Playing is invalid
        var isValid = PlaybackStateTransition.IsValidTransition(
            PlaybackState.None,
            PlaybackState.Playing);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void PlaybackStateTransition_GetAllowedTransitions_FromPlaying_ShouldIncludeMultipleStates()
    {
        // Arrange & Act
        var allowedStates = PlaybackStateTransition.GetAllowedTransitions(PlaybackState.Playing);

        // Assert
        Assert.Contains(PlaybackState.Paused, allowedStates);
        Assert.Contains(PlaybackState.Buffering, allowedStates);
        Assert.Contains(PlaybackState.Degraded, allowedStates);
        Assert.Contains(PlaybackState.Stopped, allowedStates);
        Assert.Contains(PlaybackState.Error, allowedStates);
    }

    [Fact]
    public void PlaybackStateTransition_IsPlayingOrSimilar_ForPlaying_ShouldReturnTrue()
    {
        // Arrange & Act
        var result = PlaybackStateTransition.IsPlayingOrSimilar(PlaybackState.Playing);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PlaybackStateTransition_IsPlayingOrSimilar_ForDegraded_ShouldReturnTrue()
    {
        // Arrange & Act
        var result = PlaybackStateTransition.IsPlayingOrSimilar(PlaybackState.Degraded);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PlaybackStateTransition_IsPlayingOrSimilar_ForBuffering_ShouldReturnFalse()
    {
        // Arrange & Act
        var result = PlaybackStateTransition.IsPlayingOrSimilar(PlaybackState.Buffering);

        // Assert
        Assert.False(result);
    }

    #endregion
}
