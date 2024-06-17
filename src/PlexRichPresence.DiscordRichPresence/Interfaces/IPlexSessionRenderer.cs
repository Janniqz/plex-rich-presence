using DiscordRPC;
using PlexRichPresence.Core;

namespace PlexRichPresence.DiscordRichPresence.Interfaces;

public interface IPlexSessionRenderer
{
    RichPresence RenderSession(PlexSession session);
}