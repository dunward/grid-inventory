using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    [System.Serializable]
    public class Coord2
    {
        public int x;
        public int y;

        public Coord2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"x : {x}, y : {y}";
        }
    }
}
