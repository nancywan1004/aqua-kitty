using System;
using UnityEngine;

namespace CoreRuntime.Weapons
{
    public class Weapon : InventoryItem
    {
        public virtual void Shoot(Transform firePoint)
        {
            
        }
        public Action<GameObject> OnShotFired;

        public Weapon()
        {
        }
    }
}