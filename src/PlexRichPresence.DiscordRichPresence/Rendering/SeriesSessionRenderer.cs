using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;
using PlexRichPresence.Tools.Helpers;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class SeriesSessionRenderer : GenericSessionRenderer
{
    private readonly IThumbnailService _thumbnailService;
    
    public SeriesSessionRenderer(IThumbnailService thumbnailService) => _thumbnailService = thumbnailService;

    protected override RichPresence RenderSessionInternal(PlexSession session)
    {
        var playerState = GetPlayerStateIcon(session);
        var thumbnail = _thumbnailService.GetThumbnailURL(session);
        
        var richPresence = new RichPresence
        {
            Details = StringHelper.LimitStringByteSize($"{playerState} {session.MediaTitle} (S{session.MediaSeasonNo}E{session.MediaEpisodeNo})", 128),
            State = StringHelper.LimitStringByteSize(session.MediaGrandParentTitle, 128),
            Assets = new Assets
            {
                LargeImageKey = thumbnail ?? "icon",
                SmallImageKey = thumbnail != null ? "icon-round" : null
            },
            Type = ActivityType.Watching,
            StatusDisplay = StatusDisplayType.State
        };

        return richPresence;
    }
}