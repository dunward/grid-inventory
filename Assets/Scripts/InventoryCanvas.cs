using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    public static InventoryCanvas Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Canvas GetCanvas()
    {
        return GetComponent<Canvas>();
    }
}
