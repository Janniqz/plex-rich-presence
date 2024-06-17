using PlexRichPresence.Core;
using PlexRichPresence.Services;

namespace PlexRichPresence.DiscordRichPresence.Thumbnail;

public class VideoThumbnailService : ThumbnailServiceBase
{
    public VideoThumbnailService(IWebClientService webClientService) { }

    protected override Task<string?> GetThumbnailURL_Internal(PlexSession session)
    {
        throw new NotImplementedException();
    }

    protected override bool NeedsUpdate(PlexSession session)
    {
        throw new NotImplementedException();
    }
}