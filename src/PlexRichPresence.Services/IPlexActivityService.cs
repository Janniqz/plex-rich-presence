﻿using PlexRichPresence.Core;

namespace PlexRichPresence.Services;

public interface IPlexActivityService
{
    IAsyncEnumerable<PlexSession> GetSessions(bool isOwner, string userId, string serverIp, int serverPort, string userToken);

    void Disconnect();
}