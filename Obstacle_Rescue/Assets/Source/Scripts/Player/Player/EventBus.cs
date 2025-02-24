using System;

public static class EventBus
{
    public static event Action<bool> OnGameStopped;
    public static event Action OnTrapActivated;

    public static void RaiseGameStopped(bool isStopped) => OnGameStopped?.Invoke(isStopped);
    public static void TrapActivated() => OnTrapActivated?.Invoke();
}
