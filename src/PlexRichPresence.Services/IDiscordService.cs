using PlexRichPresence.Core;

namespace PlexRichPresence.Services;

public interface IDiscordService
{
    void SetDiscordPresenceToPlexSession(PlexSession session);
    void StopRichPresence();
}