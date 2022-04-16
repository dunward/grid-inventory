using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Size
{
    public int x;
    public int y;

    public Size(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"x : {x}, y : {y}";
    }
}
