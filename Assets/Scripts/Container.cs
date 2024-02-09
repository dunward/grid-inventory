using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public class Container : MonoBehaviour
    {
        private Depot depot;

        private Canvas canvas;
        public Canvas Canvas
        {
            get => canvas;
        }
        private RectTransform rectTransform;

        [SerializeField]
        private Transform gridAreaTransform;

        public Coord2 GridSize;

        [SerializeField]
        private List<Item> items = new List<Item>();

        public void Initialize(Depot depot)
        {
            this.depot = depot;

            canvas = GetComponentInParent<Canvas>();
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
                    var gridComponent = grid.GetComponent<Grid>();
                    gridComponent.Initialize(new Coord2(x, y));
                }
            }
        }
    }
}