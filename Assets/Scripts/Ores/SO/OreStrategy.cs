using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "OreType")]
public class OreStrategy : ScriptableObject
{
    public OreType OreType;

    public void Process(WarehouseOre warehouse, OreType oreType) =>
        warehouse.AddOre(oreType);
}