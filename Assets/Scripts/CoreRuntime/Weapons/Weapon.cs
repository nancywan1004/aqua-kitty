using System;
using System.Collections;
using UnityEngine;

namespace CoreRuntime.Weapons
{
    public class Weapon : InventoryItem
    {
        public Transform FirePoint { get; set; }
        public virtual IEnumerator Shoot()
        {
            yield break;
        }

        public virtual IEnumerator Shoot(Vector3 mousePos)
        {
            yield break;
        }
        public Action<GameObject> OnShotFired;

        public Weapon()
        {
        }
    }
}