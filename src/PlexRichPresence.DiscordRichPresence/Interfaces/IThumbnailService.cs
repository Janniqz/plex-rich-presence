using PlexRichPresence.Core;

namespace PlexRichPresence.DiscordRichPresence.Interfaces;

public interface IThumbnailService
{
    public string? GetThumbnailURL(PlexSession session);
}