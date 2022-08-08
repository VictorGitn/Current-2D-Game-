using UnityEngine;
using UnityEngine.UI;

public interface IItem
{
    string Name { get; }
    Sprite UIIcon { get; }
}
