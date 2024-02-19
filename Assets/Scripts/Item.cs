using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Oxygenist
{
    public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Container container;

        [SerializeField]
        private Coord2 size;
        public Coord2 Size
        {
            get => size;
        }

        [SerializeField]
        private Coord2 position;
        public Coord2 Position
        {
            get => position;
        }
        
        private RectTransform rectTransform;
        private RectTransform dragTransform;

        public void Initialize(Container container)
        {
            this.container = container;
            rectTransform = GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(position.x * DepotUtility.GRID_UNIT_SIZE, position.y * DepotUtility.GRID_UNIT_SIZE);
        }
        
        public void UpdatePosition(Coord2 position)
        {
            this.position = position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragTransform = CreateShadowItem();
            dragTransform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            dragTransform.anchoredPosition += eventData.delta / InventoryCanvas.Instance.ScaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.LogError(eventData.pointerCurrentRaycast.gameObject, eventData.pointerCurrentRaycast.gameObject);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<Grid>(out var grid))
                {
                    var gridPosition = grid.Position;
                    if (container.IsAvailable(gridPosition, size))
                    {
                        // container.MoveItem(this, gridPosition);
                        rectTransform.anchoredPosition = new Vector2(gridPosition.x * DepotUtility.GRID_UNIT_SIZE, -gridPosition.y * DepotUtility.GRID_UNIT_SIZE);
                        position = gridPosition;
                        container.MoveItem();
                    }
                    else
                    {
                        rectTransform.anchoredPosition = new Vector2(position.x * DepotUtility.GRID_UNIT_SIZE, -position.y * DepotUtility.GRID_UNIT_SIZE);
                    }
                    Debug.LogError("this is that grid", grid.gameObject);
                }
            }
            Destroy(dragTransform.gameObject);
        }

        private RectTransform CreateShadowItem()
        {
            var obj = Instantiate(gameObject, transform.parent);
            obj.GetComponent<CanvasGroup>().alpha = 0.5f;

            return obj.GetComponent<RectTransform>();
        }
    }
}
