using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oxygenist
{
    public class Depot : MonoBehaviour
    {
        [SerializeField]
        private Container[] containers;

        private void Awake()
        {
            foreach (var container in containers)
            {
                container.Initialize();
            }
        }
    }
}