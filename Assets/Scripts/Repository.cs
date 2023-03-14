using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public class Repository : MonoBehaviour
    {
        [SerializeField]
        private Inventory[] inventories;

        private void Awake()
        {
            inventories = GetComponentsInChildren<Inventory>();
        }
    }
}