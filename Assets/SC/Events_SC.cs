using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events_SC
{
    public static event Action OnLevelWin;
    public static event Action OnLevelChange;

    public static void TriggerWin()
    {
        OnLevelWin?.Invoke();
        OnLevelChange?.Invoke();
    }
        public static void TriggerChangeLevel()
    {
        OnLevelChange?.Invoke();
    }
}
