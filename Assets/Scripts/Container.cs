using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

        private bool[,] gridAvailability;

        public void Initialize(Depot depot)
        {
            this.depot = depot;

            canvas = GetComponentInParent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(GridSize.x * DepotUtility.GRID_UNIT_SIZE, GridSize.y * DepotUtility.GRID_UNIT_SIZE);
            gridAvailability = new bool[GridSize.x, GridSize.y];

            CreateGrid();
            InitializeItems();
            UpdateGridAvailability();
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

        private void InitializeItems()
        {
            foreach (var item in items)
            {
                item.Initialize(this);
            }
        }
        
        private void UpdateGridAvailability()
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    gridAvailability[x, y] = true;
                }
            }

            foreach (var item in items)
            {
                for (int x = 0; x < item.Size.x; x++)
                {
                    for (int y = 0; y < item.Size.y; y++)
                    {
                        gridAvailability[item.Position.x + x, item.Position.y + y] = false;
                    }
                }
            }
            DebugGridAvailability();
        }

        private void DebugGridAvailability()
        {
            string log = name;
            log += Environment.NewLine;
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    log += $"{(gridAvailability[x, y] ? 1 : 0)} ";
                }
                log += Environment.NewLine;
            }
            Debug.LogError(log);
        }
    }
}