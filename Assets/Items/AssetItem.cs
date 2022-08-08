using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;

    public Sprite UIIcon => _uiIcon;

    [SerializeField] string _name;
    [SerializeField] Sprite _uiIcon;
}
