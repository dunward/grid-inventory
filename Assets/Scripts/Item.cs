using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Item")]
public class Item : ScriptableObject
{
    public int Width;
    public int Height;
    // public int Width
    // {
    //     get;
    //     private set;
    // }

    // public int Height
    // {
    //     get;
    //     private set;
    // }

    public ItemRotate rotate;
    public Sprite sprite;

    // public Item(int width, int height)
    // {
    //     Width = width;
    //     Height = height;

    //     rotate = ItemRotate.Portrait;
    // }
}
