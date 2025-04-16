public class GoldViewer : ResourceViewer
{
    private readonly string _nameResource =  "Cold: ";
    
    private void Start() =>
        Text.text = _nameResource + 0;
    
    private void OnEnable()
    {
        WareHouse.GoldAmountChanged += Show;
    }

    private void OnDisable()
    {
        WareHouse.GoldAmountChanged -= Show;
    }

    private void Show(int amount) =>
        Text.text = _nameResource + amount;
}