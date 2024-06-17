using DiscordRPC;
using PlexRichPresence.Core;

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
            PlexPlayerState.Buffering => ("⟲", DateTime.Now.ToUniversalTime()),
            PlexPlayerState.Paused => ("⏸", DateTime.Now.ToUniversalTime()),
            PlexPlayerState.Playing => ("▶", DateTime.Now.AddSeconds(ComputeSessionRemainingTime(session)).ToUniversalTime()),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static long ComputeSessionRemainingTime(PlexSession session) =>
        (session.Duration - session.ViewOffset) / 1000;
}