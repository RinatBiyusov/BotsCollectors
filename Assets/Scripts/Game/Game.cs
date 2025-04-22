using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static event Action GameStarted;

    private void Start()
    {
        GameStarted?.Invoke();
    }
}