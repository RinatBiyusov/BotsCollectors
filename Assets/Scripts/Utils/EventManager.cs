using System;
using UnityEngine;

public static class EventManager
{
    public static event Action ResourceTransferred;

    public static void PickResource() => 
        ResourceTransferred?.Invoke();
}