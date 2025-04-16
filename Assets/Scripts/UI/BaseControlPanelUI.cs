using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseControlPanelUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    public event Action PlayButtonClicked;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
    
    private void OnButtonClick() =>
        PlayButtonClicked?.Invoke();
}