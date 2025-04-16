using TMPro;
using UnityEngine;

public class ResourceViewer : MonoBehaviour
{
    [SerializeField] private WareHouseResources _wareHouse;
    
    protected TextMeshProUGUI Text;
    
    protected WareHouseResources WareHouse => _wareHouse;
    
    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }
}