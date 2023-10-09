using UnityEngine;

namespace CoreRuntime.Weapons
{
    [CreateAssetMenu(fileName = "GrappleGun", menuName = "AquaKitty/ScriptableObjects/Weapons/GrappleGun")]
    public class GrappleGun : Weapon
    {
        public override void Shoot(Transform firePoint)
        {
            Debug.Log("GrappleGun fired!");
        }
    }
}