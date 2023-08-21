using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject popsPrefab;
    private GameObject pops;
    private InventoryItem _weaponItem;
    private Sprite _weaponSprite;
    
    public void SetWeapon(InventoryItem weaponItem)
    {
        _weaponItem = weaponItem;
        GetComponent<SpriteRenderer>().sprite = _weaponItem.sprite;
    }

    public void Shoot ()
    {
        if (BubbleBarController.Instance.GetCurrBubble() > 0)
        {
            // Spawn bullets
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            float oxygenSpeed = 1f;
            BubbleBarController.Instance.ConsumeBubble(oxygenSpeed);
            StartCoroutine(DestroyBubble(bullet));
        }
    }
    
    IEnumerator DestroyBubble(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        if (bullet != null)
        {
            pops = Instantiate(popsPrefab, bullet.gameObject.transform.position, Quaternion.identity);
            pops.GetComponent<ParticleSystem>().Play();
            //Debug.Log("childCount is: " + bullet.transform.childCount);
            if (bullet.transform.childCount > 0)
            {
                // Debug.Log("the child is: " + bullet.transform.GetChild(0).transform);
                for (float i = bullet.transform.childCount; i > 0; i--)
                {
                    GarbageSpawner.Instance.RemoveGarbage();
                }
            }
            //SoundManager.PlaySound("bubblePop");
            Destroy(bullet);
            Destroy(pops, 3f);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= gunPos.x;
        mousePos.y -= gunPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}
