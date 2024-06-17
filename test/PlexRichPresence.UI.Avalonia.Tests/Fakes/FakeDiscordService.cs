using System.Collections.Generic;
using PlexRichPresence.Core;
using PlexRichPresence.Services;

namespace PlexRichPresence.UI.Avalonia.Tests.Fakes;

public class FakeDiscordService : IDiscordService
{
    public List<PlexSession> Sessions { get; } = new();

    public void SetDiscordPresenceToPlexSession(PlexSession session) => Sessions.Add(session);

    public void StopRichPresence() { }
}