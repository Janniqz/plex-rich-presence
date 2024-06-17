using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;

namespace PlexRichPresence.DiscordRichPresence.Thumbnail;

public abstract class ThumbnailServiceBase : IThumbnailService
{
    private string? _currentThumbnailURL;

    public string? GetThumbnailURL(PlexSession session)
    {
        if (NeedsUpdate(session))
            _currentThumbnailURL = GetThumbnailURL_Internal(session).Result;
        return _currentThumbnailURL;
    }
    
    protected abstract Task<string?> GetThumbnailURL_Internal(PlexSession session);

    protected abstract bool NeedsUpdate(PlexSession session);
}