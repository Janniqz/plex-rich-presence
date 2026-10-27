using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;
using PlexRichPresence.Tools.Helpers;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class MovieSessionRenderer : GenericSessionRenderer
{
    private readonly IThumbnailService _thumbnailService;
    
    public MovieSessionRenderer(IThumbnailService thumbnailService) => _thumbnailService = thumbnailService;

    protected override RichPresence RenderSessionInternal(PlexSession session)
    {
        var playerState = GetPlayerStateIcon(session);
        var thumbnail = _thumbnailService.GetThumbnailURL(session);
        
        var richPresence = new RichPresence
        {
            Details = StringHelper.LimitStringByteSize($"{session.MediaTitle} ({session.MediaRelease})", 128),
            State = StringHelper.LimitStringByteSize($"{playerState} {session.PlayerState}", 128),
            Assets = new Assets
            {
                LargeImageKey = thumbnail ?? "icon",
                SmallImageKey = thumbnail != null ? "icon-round" : null,
            },
            Type = ActivityType.Watching,
            StatusDisplay = StatusDisplayType.Details
        };

        return richPresence;
    }
}