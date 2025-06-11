using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events_SC
{
    public static event Action OnLevelWin;

    public static void TriggerWin()
    {
        OnLevelWin?.Invoke();
    }
}
