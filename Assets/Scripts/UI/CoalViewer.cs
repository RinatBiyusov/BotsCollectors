using System;

public class CoalViewer : ResourceViewer
{
    private readonly string _nameResource =  "Coal: ";
    
    private void Start() =>
        Text.text = _nameResource + 0;

    private void OnEnable()
    {
        WareHouse.CoalAmountChanged += Show;
    }

    private void OnDisable()
    {
        WareHouse.CoalAmountChanged -= Show;
    }

    private void Show(int amount) =>
        Text.text = _nameResource + amount;
}