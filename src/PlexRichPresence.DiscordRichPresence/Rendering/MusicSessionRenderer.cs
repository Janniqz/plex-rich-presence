using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class MusicSessionRenderer : GenericSessionRenderer
{
    private readonly IThumbnailService _thumbnailService;
    
    public MusicSessionRenderer(IThumbnailService thumbnailService) => _thumbnailService = thumbnailService;

    public override RichPresence RenderSession(PlexSession session)
    {
        var (playerState, endTimeStamp) = RenderPlayerState(session);
        var thumbnail = _thumbnailService.GetThumbnailURL(session);

        var richPresence = new RichPresence
        {
            Details = $"♫ {session.MediaTitle}",
            State = $"{playerState} {session.MediaGrandParentTitle}",
            Assets = new Assets
            {
                LargeImageKey = thumbnail ?? "icon",
                SmallImageKey = thumbnail != null ? "icon-round" : null,
            },
            Type = ActivityType.Listening,
        };

        // TODO This is duplicated in all Renderers, consider moving it to a base class
        if (session.PlayerState == PlexPlayerState.Playing)
        {
            richPresence.WithTimestamps(new Timestamps
            {
                Start = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(session.ViewOffset / 1000d)),
                End = endTimeStamp
            });
        }

        return richPresence;
    }
}