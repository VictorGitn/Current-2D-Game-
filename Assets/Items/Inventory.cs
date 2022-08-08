using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<AssetItem> Items;
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private ItemsEjector _ejector;
    private void OnEnable()
    {
        Render(Items);
    }

    public void Render(List<AssetItem> Items)
    {   
        foreach(Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        Items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_draggingParent);
            cell.Render(item);

            cell.Ejecting += () => Destroy(cell.gameObject);
            cell.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
        });
    }
}
