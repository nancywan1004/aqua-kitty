using System;
using System.Collections;
using UnityEngine;

namespace CoreRuntime.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        private Weapon _weaponItem;
        private Sprite _weaponSprite;
        
        [SerializeField] private GameObject popsPrefab;
        [SerializeField] private Transform firePoint;
        private GameObject pops;
        private WeaponInventory _inventory;

        public void SetInventory(WeaponInventory inventory)
        {
            _inventory = inventory;
            _inventory.OnSwitchInventoryItem += (item) => SetWeapon(_inventory.GetItemList()[item]);
            SetWeapon(_inventory.SelectedItem);
        }
        
        // private void OnDisable()
        // {
        //     _inventory.OnSwitchInventoryItem -= (item) => SetWeapon(_inventory.GetItemList()[item]);
        // }
        
        private void SetWeapon(Weapon weaponItem)
        {
            _weaponItem = weaponItem;
            GetComponent<SpriteRenderer>().sprite = weaponItem.sprite;
            _weaponItem.FirePoint = firePoint;
            if (_weaponItem.type is ItemType.BubbleGun)
            {
                BubbleBarController.Instance.MaxBubble = _weaponItem.ammunition;
                var bubbleGunItem = (BubbleGun) _weaponItem;
                bubbleGunItem.popsPrefab = popsPrefab;
                bubbleGunItem.OnShotFired += (bubble) => StartCoroutine(bubbleGunItem.DestroyBubble(bubble));
            }
        }

        public void Fire (Vector3 mousPos)
        {
            if (_weaponItem.type is ItemType.GrappleGun)
            {
                StartCoroutine(_weaponItem.Shoot(mousPos));
            } else if (_weaponItem.type is ItemType.BubbleGun)
            {
                StartCoroutine(_weaponItem.Shoot());
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
}