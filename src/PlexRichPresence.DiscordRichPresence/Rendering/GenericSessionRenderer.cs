using DiscordRPC;
using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class GenericSessionRenderer : IPlexSessionRenderer
{
    public virtual bool IsRenderingAllowed(PlexSession session) => true;

    public RichPresence RenderSession(PlexSession session)
    {
        var endTimeStamp = GetSessionEndTime(session);
        var richPresence = RenderSessionInternal(session);
        
        if (session.PlayerState == PlexPlayerState.Playing)
        {
            richPresence.WithTimestamps(new Timestamps
            {
                Start = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(session.ViewOffset / 1000d)),
                End = endTimeStamp
            });
        }

        return richPresence;
    }

    protected virtual RichPresence RenderSessionInternal(PlexSession session)
    {
        var endTimeStamp = GetSessionEndTime(session);
        
        return new RichPresence
        {
            Details = session.MediaGrandParentTitle + " - " + session.MediaParentTitle,
            State = session.MediaTitle,
            Timestamps = new Timestamps
            {
                End = endTimeStamp
            }
        };
    }

    protected char GetPlayerStateIcon(PlexSession session)
    {
        return session.PlayerState switch
        {
            PlexPlayerState.Buffering => '⟲',
            PlexPlayerState.Paused => '⏸',
            PlexPlayerState.Playing => '▶',
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected static DateTime GetSessionEndTime(PlexSession session) => DateTime.UtcNow.AddSeconds((session.Duration - session.ViewOffset) / 1000);
}