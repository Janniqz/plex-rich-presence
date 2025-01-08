using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class GenericSessionRenderer : IPlexSessionRenderer
{
    public virtual RichPresence RenderSession(PlexSession session)
    {
        var (_, endTimeStamps) = RenderPlayerState(session);
        return new RichPresence
        {
            Details = session.MediaGrandParentTitle + " - " + session.MediaParentTitle,
            State = session.MediaTitle,
            Timestamps = new Timestamps
            {
                End = endTimeStamps
            }
        };
    }

    protected (string playerState, DateTime endTimeStamp) RenderPlayerState(PlexSession session)
    {
        return session.PlayerState switch
        {
            PlexPlayerState.Buffering => ("⟲", DateTime.UtcNow.AddSeconds(ComputeSessionRemainingTime(session))),
            PlexPlayerState.Paused => ("⏸", DateTime.UtcNow.AddSeconds(ComputeSessionRemainingTime(session))),
            PlexPlayerState.Playing => ("▶", DateTime.UtcNow.AddSeconds(ComputeSessionRemainingTime(session))),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static long ComputeSessionRemainingTime(PlexSession session) =>
        (session.Duration - session.ViewOffset) / 1000;
}