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
        
        public override void Shoot()
        {
            if (BubbleBarController.Instance.GetCurrBubble() > 0)
            {
                // Spawn bubbles
                GameObject bubble = Instantiate(ammunitionPrefab, FirePoint.position, FirePoint.rotation);
                int oxygenSpeed = 1;
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
                var childBubbleCount = bubble.transform.childCount;
                while (childBubbleCount > 0)
                {
                    GarbageSpawner.Instance.RemoveGarbage();
                    childBubbleCount--;
                }
                Destroy(bubble);
                Destroy(_pops, 3f);
            }
        }
    }
}