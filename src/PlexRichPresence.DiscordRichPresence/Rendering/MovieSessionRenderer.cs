using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class MovieSessionRenderer : GenericSessionRenderer
{
    private readonly IThumbnailService _thumbnailService;
    
    public MovieSessionRenderer(IThumbnailService thumbnailService) => _thumbnailService = thumbnailService;

    public override RichPresence RenderSession(PlexSession session)
    {
        var (playerState, endTimeStamp) = RenderPlayerState(session);
        var thumbnail = _thumbnailService.GetThumbnailURL(session);
        return new RichPresence
        {
            Details = session.MediaTitle,
            State = playerState.Length < 2 ? playerState + '\x2800' : playerState,
            Timestamps = new Timestamps
            {
                End = endTimeStamp
            },
            Assets = new Assets
            {
                LargeImageKey = thumbnail ?? "icon"
            }
        };
    }
}