using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public partial class Inventory : MonoBehaviour
    {
        public GameObject cellPrefab;

        private void Awake()
        {
            Initialize();
            GenerateInventoryCell();
        }

        private void GenerateInventoryCell()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var obj = Instantiate(cellPrefab, transform);
                    var cell = obj.GetComponent<InventoryCell>();
                    Grid[x, y] = cell;
                    cell.Initialize(this, x, y);
                }
            }
        }
    }
}
