using System.Text;
using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;
using PlexRichPresence.Tools.Helpers;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class MusicSessionRenderer : GenericSessionRenderer
{
    private readonly IThumbnailService _thumbnailService;

    public override bool IsRenderingAllowed(PlexSession session) => session.PlayerState == PlexPlayerState.Playing;

    public MusicSessionRenderer(IThumbnailService thumbnailService) => _thumbnailService = thumbnailService;

    protected override RichPresence RenderSessionInternal(PlexSession session)
    {
        var thumbnail = _thumbnailService.GetThumbnailURL(session);
        var richPresence = new RichPresence
        {
            Details = FormatRichPresenceString(session.MediaTitle),
            State = FormatRichPresenceString(session.MediaGrandParentTitle),
            Assets = new Assets
            {
                LargeImageKey = thumbnail ?? "icon",
                SmallImageKey = thumbnail != null ? "icon-round" : null,
            },
            Type = ActivityType.Listening, 
            StatusDisplay = StatusDisplayType.State
        };

        return richPresence;
    }
}