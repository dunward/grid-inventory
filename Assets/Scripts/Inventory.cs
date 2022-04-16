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
        public Size size;
        // public int width;
        // public int height;

        private InventoryCell[,] _grid;

        public InventoryCell[,] Grid
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

        private int[,] _validGrid; // 0 = Valid

        public void Initialize()
        {
            Grid = new InventoryCell[Width, Height];
            _validGrid = new int[Width, Height];
        }

        public void AddItem(Item item)
        {
            var pos = GetItemInventoryPosition(item);

            if (pos != null)
            {
                Grid[pos.x, pos.y].AddItem(item);
                UpdateValidGrid(pos, item);
            }
            else
            {
                Debug.LogError("Inventory is full");
            }
        }

        public Size GetItemInventoryPosition(Item item)
        {
            for (int y = 0; y < Height - (item.Height - 1); y++)
            {
                for (int x = 0; x < Width - (item.Width - 1); x++)
                {
                    if (_validGrid[x, y] == 0)
                    {
                        if (CheckItemSize(item, x, y))
                        {
                            return new Size(x, y);
                        }
                    }
                }
            }

            return null; // inventory is full
        }

        private bool CheckItemSize(Item item, int x, int y)
        {
            var (maxX, maxY) = (x + item.Width, y + item.Height);

            for (int _y = y; _y < maxY; _y++)
            {
                for (int _x = x; _x < maxX; _x++)
                {
                    if (_validGrid[_x, _y] != 0) return false;
                }
            }

            return true;
        }

        private void OnGridChanged()
        {
            
        }

        private void UpdateValidGrid(Size position, Item item)
        {
            var (maxX, maxY) = (position.x + item.Width, position.y + item.Height);
            for (int y = position.y; y < maxY; y++)
            {
                for (int x = position.x; x < maxX; x++)
                {
                    _validGrid[x, y] = 1;
                }
            }
        }

#if UNITY_EDITOR
        public void ShowValidGrid()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (_validGrid[x, y] != 0)
                        Grid[x, y].GetComponent<Image>().color = Color.red;
                }
            }
        }
#endif
    }
}
