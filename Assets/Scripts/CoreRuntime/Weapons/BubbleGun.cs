using System.Collections;
using UnityEngine;

namespace CoreRuntime.Weapons
{
    [CreateAssetMenu(fileName = "BubbleGun", menuName = "AquaKitty/ScriptableObjects/Weapons/BubbleGun")]
    public class BubbleGun : Weapon
    {
        public GameObject popsPrefab;
        private GameObject _pops;
        
        public BubbleGun()
        {
        }
        
        public override void Shoot(Transform firePoint)
        {
            if (BubbleBarController.Instance.GetCurrBubble() > 0)
            {
                // Spawn bubbles
                GameObject bubble = Instantiate(ammunitionPrefab, firePoint.transform.position, firePoint.transform.rotation);
                float oxygenSpeed = 1f;
                BubbleBarController.Instance.ConsumeBubble(oxygenSpeed);
                OnShotFired?.Invoke(bubble);
            }
        }
        
        public IEnumerator DestroyBubble(GameObject bubble)
        {
            yield return new WaitForSeconds(3f);
            if (bubble != null)
            {
                _pops = Instantiate(popsPrefab, bubble.gameObject.transform.position, Quaternion.identity);
                _pops.GetComponent<ParticleSystem>().Play();
                //Debug.Log("childCount is: " + bubble.transform.childCount);
                if (bubble.transform.childCount > 0)
                {
                    // Debug.Log("the child is: " + bubble.transform.GetChild(0).transform);
                    for (float i = bubble.transform.childCount; i > 0; i--)
                    {
                        GarbageSpawner.Instance.RemoveGarbage();
                    }
                }
                Destroy(bubble);
                Destroy(_pops, 3f);
            }
        }
    }
}