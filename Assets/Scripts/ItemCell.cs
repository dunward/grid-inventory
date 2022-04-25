using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Oxygenist
{
    public class ItemCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Image _backgroundImage;
        [SerializeField]
        private Image _iconImage;
        private GameObject dragObject;

        private InventoryCell _inventoryCell;
        private Coord2 _position;
        private Item _item;

        
        public void Initialize(Item item, Coord2 position, InventoryCell inventoryCell)
        {
            _iconImage.sprite = item.sprite;
            _position = position;

            _inventoryCell = inventoryCell;
            _item = item;

            ResetPosition();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _backgroundImage.raycastTarget = false;

            dragObject = CreateShadowItem();
        }

        public void OnDrag(PointerEventData eventData)
        {
            dragObject.transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _backgroundImage.raycastTarget = true;

            var inventoryCell = eventData.pointerCurrentRaycast.gameObject?.GetComponent<InventoryCell>();

            if (inventoryCell != null)
            {
                var flag = inventoryCell.MoveItem(_item, _position);
                if (flag) UpdateCellPosition(inventoryCell);                
            }

            ResetPosition();
            Destroy(dragObject);
        }

        private GameObject CreateShadowItem()
        {
            var obj = Instantiate(gameObject, transform.parent);
            obj.GetComponent<CanvasGroup>().alpha = 0.5f;

            return obj;
        }

        private void UpdateCellPosition(InventoryCell cell)
        {
            if (cell != null) _inventoryCell = cell;
        }

        private void ResetPosition()
        {
            transform.position = _inventoryCell.transform.position;
            _position = _inventoryCell.inventoryPosition;
        }
    }
}
