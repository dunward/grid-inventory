using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public class Container : MonoBehaviour
    {
        private RectTransform rectTransform;

        [SerializeField]
        private Transform gridAreaTransform;

        public Coord2 GridSize;

        public void Initialize()
        {
            rectTransform = GetComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2(GridSize.x * DepotUtility.GRID_UNIT_SIZE, GridSize.y * DepotUtility.GRID_UNIT_SIZE);
            CreateGrid();
        }

        private void CreateGrid()
        {
            var gridObject = Resources.Load<GameObject>("Grid");
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    var grid = Instantiate(gridObject, gridAreaTransform);
                    grid.GetComponent<RectTransform>().anchoredPosition = new Vector3(x * DepotUtility.GRID_UNIT_SIZE, -y * DepotUtility.GRID_UNIT_SIZE, 0);
                }
            }
        }
    }
}