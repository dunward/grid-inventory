using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private Coord2 position;
        private RectTransform rectTransform;

        public void Initialize(Coord2 position)
        {
            this.position = position;
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(position.x * DepotUtility.GRID_UNIT_SIZE, -position.y * DepotUtility.GRID_UNIT_SIZE, 0);
        }
    }
}