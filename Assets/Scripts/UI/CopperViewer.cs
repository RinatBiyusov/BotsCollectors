public class CopperViewer : ResourceViewer
{
    private readonly string _nameResource =  "Copper: ";
    
    private void Start() =>
        Text.text = _nameResource + 0;
    
    private void OnEnable()
    {
        WareHouse.CopperAmountChanged += Show;
    }

    private void OnDisable()
    {
        WareHouse.CopperAmountChanged -= Show;
    }
    
    private void Show(int amount) =>
        Text.text = _nameResource + amount;
}