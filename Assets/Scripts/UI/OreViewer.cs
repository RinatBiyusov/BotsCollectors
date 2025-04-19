using System;
using TMPro;
using UnityEngine;

public class OreViewer : MonoBehaviour
{
    [SerializeField] private OreEventDispatcher _oreEventDispatcher;

    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] private OreType _type;

    private void Start()
    {
        _text.text = _type + ": " + 0;
    }

    private void OnEnable()
    {
        _oreEventDispatcher.Changed += OreEventDispatcherOnChanged;
    }

    private void OnDisable()
    {
        _oreEventDispatcher.Changed -= OreEventDispatcherOnChanged;
    }

    private void OreEventDispatcherOnChanged(OreType type, int amount)
    {
        if (_type == type)
            _text.text = _type + ": " + amount;
    }
}