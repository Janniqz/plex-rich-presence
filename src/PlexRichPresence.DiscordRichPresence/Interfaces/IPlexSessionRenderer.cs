using DiscordRPC;
using PlexRichPresence.Core;

namespace PlexRichPresence.DiscordRichPresence.Interfaces;

public interface IPlexSessionRenderer
{
    bool IsRenderingAllowed(PlexSession session);
    RichPresence RenderSession(PlexSession session);
}