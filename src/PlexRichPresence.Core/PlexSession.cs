namespace PlexRichPresence.Core;

public record PlexSession(
    string MediaTitle,
    string MediaParentTitle,
    string MediaParentGUID,
    string MediaGrandParentTitle,
    PlexPlayerState PlayerState,
    PlexMediaType MediaType,
    long Duration,
    long ViewOffset,
    string? LocalThumbnail
)
{
    public PlexSession() : this(
        "Idle",
        string.Empty,
        string.Empty,
        string.Empty,
        PlexPlayerState.Idle,
        PlexMediaType.Idle,
        default,
        default,
        string.Empty
    ) { }
}