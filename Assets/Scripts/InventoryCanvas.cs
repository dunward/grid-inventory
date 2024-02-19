using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    public static InventoryCanvas Instance { get; private set; }
    private Canvas canvas;

    private void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
    }

    public float ScaleFactor
    {
        get => canvas.scaleFactor;
    }
}
