using System;
using UnityEngine;

public class ResourcesZone : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private int _maxAmountSpawnedResources;

    private int _currentAmountSpawnedResources;
}