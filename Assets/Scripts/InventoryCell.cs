using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Vector2 inventoryPosition;
    
    private RectTransform _rectTransform;
    private Inventory _inventory;

    public void Initialize(Inventory inventory, int x, int y)
    {
        _rectTransform = GetComponent<RectTransform>();
        _inventory = inventory;

        inventoryPosition = new Vector2(x, y);

        _rectTransform.anchoredPosition = new Vector2(100 * x, 100 * -y);
    }

    public void AddItem(Item item)
    {
        var obj = Resources.Load($"ItemCell{item.Width}x{item.Height}") as GameObject;
        var cell = Instantiate(obj, transform).GetComponent<ItemCell>();
        cell.Initialize(item);
    }

    public void Test(int A)
    {
        // Debug.LogError($"Mouse Event = {A} (0 is enter, 1 is Exit) grid position : {inventoryPosition}");
    }
}
