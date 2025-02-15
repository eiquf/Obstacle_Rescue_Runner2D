using System;

public static class EventBus
{
    public static event Action<bool> OnGameStopped;

    public static void RaiseGameStopped(bool isStopped) => OnGameStopped?.Invoke(isStopped);
}
