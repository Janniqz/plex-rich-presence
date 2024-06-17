using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class IdleSessionRenderer : IPlexSessionRenderer
{
    public RichPresence RenderSession(PlexSession session)
    {
        return new RichPresence
        {
            State = "Idle",
            Assets = new Assets
            {
                LargeImageKey = "icon"
            }
        };
    }
}