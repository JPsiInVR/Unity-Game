using System;
using UnityEngine;

public class PauseController : Singleton<PauseController>
{
    public event Action OnPauseToggled;
    public bool IsGamePaused { get; private set; } = false;
    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;
        Time.timeScale = IsGamePaused ? 0 : 1;
        Screen.sleepTimeout = IsGamePaused ? SleepTimeout.SystemSetting : SleepTimeout.NeverSleep;
        OnPauseToggled?.Invoke();
    }
}
