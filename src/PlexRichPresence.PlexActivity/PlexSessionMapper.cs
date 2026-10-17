using Plex.ServerApi.PlexModels.Media;
using Plex.ServerApi.PlexModels.Server.Sessions;
using PlexRichPresence.Core;

namespace PlexRichPresence.PlexActivity;

public class PlexSessionMapper
{
    public PlexSession Map(SessionMetadata metadata, string plexServerHost, string plexToken)
    {
        var mediaType = GetMediaType(metadata.Type);
        return new(
            metadata.Title,
            metadata.ParentTitle,
            metadata.ParentGuid,
            metadata.GrandparentTitle,
            mediaType == PlexMediaType.Episode ? metadata.ParentIndex : null,
            mediaType == PlexMediaType.Episode ? metadata.Index : null,
            GetPlayerState(metadata.Player?.State ?? "buffering"),
            mediaType,
            metadata.Duration,
            metadata.ViewOffset,
            metadata.Thumb is null ? null : $"{plexServerHost}{metadata.Thumb[1..]}?X-Plex-Token={plexToken}"
        );
    }

    public PlexSession Map(Metadata metadata, string state, long viewOffset, string plexServerHost, string plexToken)
    {
        var mediaType = GetMediaType(metadata.Type);
        return new(
            metadata.Title,
            metadata.ParentTitle,
            metadata.ParentGuid,
            metadata.GrandparentTitle,
            mediaType == PlexMediaType.Episode ? metadata.ParentIndex : null,
            mediaType == PlexMediaType.Episode ? metadata.Index : null,
            GetPlayerState(state),
            mediaType,
            metadata.Duration,
            viewOffset,
            metadata.Thumb is null ? null : $"{plexServerHost}{metadata.Thumb[1..]}?X-Plex-Token={plexToken}"
        );
    }


    private static PlexMediaType GetMediaType(string type) => type switch
    {
        "movie" => PlexMediaType.Movie,
        "episode" => PlexMediaType.Episode,
        "track" => PlexMediaType.Track,
        _ => PlexMediaType.Unknown
    };

    private static PlexPlayerState GetPlayerState(string state) => state switch
    {
        "buffering" => PlexPlayerState.Buffering,
        "paused" => PlexPlayerState.Paused,
        _ => PlexPlayerState.Playing
    };
}