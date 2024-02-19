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

        private float scaleFactor = 0f;

        public void Initialize(Container container)
        {
            this.container = container;
            rectTransform.anchoredPosition = new Vector2(position.x * DepotUtility.GRID_UNIT_SIZE, position.y * DepotUtility.GRID_UNIT_SIZE);
            scaleFactor = InventoryCanvas.Instance.GetCanvas().scaleFactor;
        }
        
        public void UpdatePosition(Coord2 position)
        {
            this.position = position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragTransform = CreateShadowItem();
        }

        public void OnDrag(PointerEventData eventData)
        {
            dragTransform.anchoredPosition += eventData.delta / scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Destroy(dragTransform.gameObject);
        }

        private RectTransform CreateShadowItem()
        {
            var obj = Instantiate(gameObject, transform.parent);
            obj.GetComponent<CanvasGroup>().alpha = 0.5f;

            return obj.GetComponent<RectTransform>();
        }
    }
}
