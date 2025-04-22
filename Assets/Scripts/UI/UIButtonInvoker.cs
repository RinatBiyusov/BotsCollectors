using UnityEngine;
using UnityEngine.UI;

public class UIButtonInvoker : MonoBehaviour
{
    private Button _button;
    private ICommand _command;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Bind(ICommand command) =>
        _command = command;
    
    private void OnButtonClick() =>
        _command?.Execute();
}