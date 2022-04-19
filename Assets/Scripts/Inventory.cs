using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Oxygenist
{
    public partial class Inventory : MonoBehaviour
    {
        public Coord2 size;
        // public int width;
        // public int height;

        private InventoryCell[,] _grid;

        public InventoryCell[,] grid
        {
            get => _grid;
            set
            {
                _grid = value;
                OnGridChanged();
            }
        }

        public int Width
        {
            get => size.x;
        }

        public int Height
        {
            get => size.y;
        }

        private bool[,] inValidGrid; // 0 = Valid

        public void Initialize()
        {
            // this.width = width;
            // this.height = height;

            grid = new InventoryCell[Width, Height];
            inValidGrid = new bool[Width, Height];
            // var validGrid = new int[width][];
            // for (int i = 0; i < width; i++)
            // {
            //     validGrid[i] = new int[height];
            // }

            // this.validGrid = validGrid;
            // ShowValidGrid();
        }

        public void AddItem(Item item)
        {
            var pos = GetItemInventoryPosition(item);

            if (pos != null) // find C# 7.0 pattern
            {
                grid[pos.x, pos.y].AddItem(item); // valid grid update here
                UpdateValidGrid(pos, item.size, true);
            }
            else
            {
                Debug.LogError("Inventory is full");
            }

            // ShowValidGrid();
        }

        public Coord2 GetItemInventoryPosition(Item item)
        {
            // !! SHOULD OPTIMIZE : so slow --
            for (int y = 0; y < Height - (item.Height - 1); y++)
            {
                for (int x = 0; x < Width - (item.Width - 1); x++)
                {
                    if (!inValidGrid[x, y])
                    {
                        if (CheckItemSize(inValidGrid, item.size, x, y)) // O(N^2 * nlogn)
                        {
                            return new Coord2(x, y);
                        }
                    }
                }
            }

            return null; // inventory is full
        }

        public bool MoveItem(Coord2 position, Coord2 newPosition, Coord2 itemSize)
        {
            // Create Temp Grid
            var temp = inValidGrid;
            UpdateValidGrid(temp, position, itemSize, false);

            if (CheckItemSize(temp, itemSize, newPosition.x, newPosition.y))
            {
                UpdateValidGrid(position, itemSize, false);
                UpdateValidGrid(newPosition, itemSize, true);

                return true;
            }

            return false;
        }

        private bool CheckItemSize(bool[,] grid, Coord2 size, int x, int y)
        {
            var (maxX, maxY) = (x + size.x, y + size.y);

            if (maxX > Width || maxY > Height) return false;

            for (int _y = y; _y < maxY; _y++)
            {
                for (int _x = x; _x < maxX; _x++)
                {
                    if (grid[_x, _y]) return false;
                }
            }

            return true;
        }

        private void OnGridChanged() // deprecate
        {
            
        }
        
        private void UpdateValidGrid(Coord2 position, Coord2 size, bool flag)
        {
            UpdateValidGrid(inValidGrid, position, size, flag);
        }

        private void UpdateValidGrid(bool[,] grid, Coord2 position, Coord2 size, bool flag)
        {
            var (maxX, maxY) = (position.x + size.x, position.y + size.y);
            for (int y = position.y; y < maxY; y++)
            {
                for (int x = position.x; x < maxX; x++)
                {
                    grid[x, y] = flag;
                }
            }

            ShowValidGrid(); // test mode
        }

#if UNITY_EDITOR
        public void ShowValidGrid()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (inValidGrid[x, y])
                        grid[x, y].GetComponent<Image>().color = Color.red;
                    else
                        grid[x, y].GetComponent<Image>().color = Color.white;
                }
            }
        }
#endif
    }
}
