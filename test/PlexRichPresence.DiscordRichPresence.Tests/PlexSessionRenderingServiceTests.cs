using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Rendering;
using PlexRichPresence.Services;

namespace PlexRichPresence.DiscordRichPresence.Tests;

public class PlexSessionRenderingServiceTests
{
    public static TheoryData<PlexSession, bool, string?, string?>
        RenderingTheoryData =>
        new()
        {
            {
                new PlexSession
                {
                    MediaTitle = "Test Movie", MediaType = PlexMediaType.Movie, PlayerState = PlexPlayerState.Buffering
                },
                true, "⟲ Buffering", "Test Movie"
            },
            {
                new PlexSession
                {
                    MediaTitle = "Test Movie", MediaType = PlexMediaType.Movie, PlayerState = PlexPlayerState.Paused
                },
                true, "⏸ Paused", "Test Movie"
            },
            {
                new PlexSession
                {
                    MediaTitle = "Test Movie", MediaType = PlexMediaType.Movie, PlayerState = PlexPlayerState.Playing,
                    Duration = 20_000, ViewOffset = 10_000
                },
                true, "▶ Playing", "Test Movie"
            },
            {
                new PlexSession
                {
                    MediaTitle = "Test Title", MediaParentTitle = "Test Parent Title",
                    MediaGrandParentTitle = "Test Grand Parent Title", MediaType = PlexMediaType.Unknown,
                    PlayerState = PlexPlayerState.Paused
                },
                true, "Test Title", "Test Grand Parent Title - Test Parent Title"
            },
            {
                new PlexSession
                {
                    MediaTitle = "Test Title", MediaParentTitle = "Test Parent Title",
                    MediaGrandParentTitle = "Test Grand Parent Title", MediaType = PlexMediaType.Track,
                    PlayerState = PlexPlayerState.Paused
                },
                true, "⏸ Test Grand Parent Title", "♫ Test Title"
            },
            {
                new PlexSession
                {
                    MediaTitle = "Test Title", MediaParentTitle = "Test Parent Title",
                    MediaGrandParentTitle = "Test Grand Parent Title", MediaType = PlexMediaType.Episode,
                    PlayerState = PlexPlayerState.Paused
                },
                true, "⏸ Test Grand Parent Title", "⏏ Test Title"
            },
            {
                new PlexSession
                {
                    MediaTitle = "Idle",
                    MediaParentTitle = string.Empty,
                    MediaGrandParentTitle = string.Empty,
                    MediaType = PlexMediaType.Idle,
                    PlayerState = PlexPlayerState.Idle
                },
                false, null, null
            }
        };


    [Theory]
    [MemberData(nameof(RenderingTheoryData))]
    public void RenderPlayerState(PlexSession session, bool presenceExists, string? expectedState, string? expectedDetail)
    {
        // Given
        var plexSessionRenderingService = new PlexSessionRenderingService(new PlexSessionRendererStore(new Mock<IWebClientService>().Object), new Mock<ILogger<PlexSessionRenderingService>>().Object);

        // When
        var presence = plexSessionRenderingService.RenderSession(session);
        
        // Then
        if (!presenceExists)
        {
            presence.Should().BeNull();
        }
        else
        {
            presence.Should().NotBeNull();
            presence!.State.Should().Be(expectedState);
            presence.Details.Should().Be(expectedDetail);
        }
    }
}