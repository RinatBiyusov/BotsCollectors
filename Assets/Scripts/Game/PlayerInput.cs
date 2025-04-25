using System;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : MonoBehaviour
{
    public event Action LeftButtonClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftButtonClicked?.Invoke();
    }
}