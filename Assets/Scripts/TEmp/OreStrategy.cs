using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "OreType")]
public class OresStrategy : ScriptableObject
{
    [SerializeField] private OreType _oreType;
}