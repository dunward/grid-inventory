using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oxygenist
{
    public class InventoryCell : MonoBehaviour
    {
        public Coord2 inventoryPosition;
        
        private RectTransform _rectTransform;
        private Inventory _inventory;

        public void Initialize(Inventory inventory, int x, int y)
        {
            _rectTransform = GetComponent<RectTransform>();
            _inventory = inventory;

            inventoryPosition = new Coord2(x, y);

            _rectTransform.anchoredPosition = new Vector2(100 * x, 100 * -y);
        }

        public void AddItem(Item item)
        {
            var obj = Resources.Load($"ItemCell{item.Width}x{item.Height}") as GameObject;
            var _itemCell = Instantiate(obj, _inventory.transform).GetComponent<ItemCell>();
            _itemCell.Initialize(item, inventoryPosition, this);
            _itemCell.transform.position = transform.position;
        }

        public bool MoveItem(Item item, Coord2 prevPosition)
        {
            var z= _inventory.MoveItem(prevPosition, inventoryPosition, item.size);
            Debug.LogError(z);
            return z;
        }
    }
}
