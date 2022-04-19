using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    [CreateAssetMenu(menuName = "Create New Item")]
    public class Item : ScriptableObject
    {
        public Coord2 size;
        
        public int Width
        {
            get => size.x;
        }
        public int Height
        {
            get => size.y;
        }

        public ItemRotate rotate;
        public Sprite sprite;
    }
}
