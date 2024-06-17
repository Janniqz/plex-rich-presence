using DiscordRPC;
using PlexRichPresence.Core;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class SeriesSessionRenderer : GenericSessionRenderer
{
    public override RichPresence RenderSession(PlexSession session)
    {
        var (playerState, endTimeStamp) = RenderPlayerState(session);
        return new RichPresence
        {
            Details = $"⏏ {session.MediaTitle}",
            State = $"{playerState} {session.MediaGrandParentTitle}",
            Timestamps = new Timestamps
            {
                End = endTimeStamp
            }
        };
    }
}