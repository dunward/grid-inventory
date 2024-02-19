using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public class Grid : MonoBehaviour
    {
        private Container container;

        [SerializeField]
        private Coord2 position;
        public Coord2 Position
        {
            get => position;
        }
        
        private RectTransform rectTransform;

        public void Initialize(Container container, Coord2 position)
        {
            this.container = container;
            this.position = position;
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(position.x * DepotUtility.GRID_UNIT_SIZE, -position.y * DepotUtility.GRID_UNIT_SIZE, 0);
        }
    }
}