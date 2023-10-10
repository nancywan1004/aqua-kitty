using System.Collections;
using System.Collections.Generic;
using CoreRuntime.Weapons;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float movementSpeed = 1.2f;

    public Animator animator;
    private Rigidbody2D _rigidbody;
    private PlayerState playerState;
    private Vector3 mousePos;
    private float rotateY;
    private float holdDownStartTime;
    private float holdDownTime;
    private float moveStartTime;
    private float moveHoldTime;
    
    private Vector3 webZipTargetPosition;
    private float webZipSpeed;
    private Vector2 webZipStart;
    private Vector2 webZipEnd;
    private WeaponInventory _inventory;
    [SerializeField] private UI_Inventory<Weapon> _uiInventory;
    [SerializeField] private List<Weapon> _inventoryItems;
    [SerializeField] private WeaponController weaponController;

    private GlobalInputActions _inputActions;

    public enum PlayerState
    {
        Normal,
        WebZipping,
        Attached,
        Dizzy
    }

    private void Awake()
    {
        _inventory = new WeaponInventory(_inventoryItems);
        _uiInventory.InitInventory(_inventory);
        weaponController.InitInventory(_inventory);
        _inputActions = new GlobalInputActions();
        _inputActions.Player.SwitchWeapon.performed += context =>
        {
            _inventory.SwitchInventoryItem();
        };
        _inputActions.Player.Bubbleshoot.performed += context =>
        {
            weaponController.Fire(mousePos);
        };
        _inputActions.Player.Bubbleshoot.canceled += context =>
        {
            if (_inventory.SelectedItem.type is ItemType.GrappleGun)
            {
                var grappleGunItem = _inventory.SelectedItem as GrappleGun;
                StartCoroutine(grappleGunItem.HandleWebZipping());
            }
        };
        _inputActions.Player.Grapple.started += context =>
        {
            HandleWebZipStart();
        };
        _inputActions.Player.Grapple.canceled += context =>
        {
            HandleWebZipping();
        };
        playerState = PlayerState.Normal;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = false;
    }
    
    public void OnEnable()
    {
        EnableInputActions();
    }

    public void OnDisable()
    {
        DisableInputActions();
    }

    public void EnableInputActions()
    {
        _inputActions.Enable();
    }

    public void DisableInputActions()
    {
        _inputActions.Disable();
    }
    
    private void Update()
    {
        var move = _inputActions.Player.Move.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(move);
        switch (playerState)
        {
            case PlayerState.Normal:
                animator.Play("Cat_Swim");
                HandleMovement();
                break;
            case PlayerState.WebZipping:
                break;
            case PlayerState.Attached:
                HandleRelativeRotation();
                break;
            case PlayerState.Dizzy:
                animator.Play("Cat_Dizzy");
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerState = PlayerState.Dizzy;
            StartCoroutine(BackToNormal());
        }
    }

    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(2.0f);
        playerState = PlayerState.Normal;
    }

    private void FixedUpdate()
     {
        switch (playerState)
        {
            case PlayerState.Normal:
                _rigidbody.velocity = new Vector2(0, 0);
                break;
        }
     }

    private void HandleRelativeRotation()
    {
        rotateY = 0f;
        if (mousePos.x > transform.position.x)
        {
            rotateY = 180f;
        }
        transform.localRotation = Quaternion.Euler(0, rotateY, 0);
    }

    private void HandleMovement() {
        rotateY = 0f;
        if (mousePos.x > transform.position.x)
        {
            rotateY = 180f;
        }
        transform.rotation = Quaternion.Euler(0, rotateY, 0);
        Vector3 moveVector = (mousePos - transform.position).normalized;
        transform.position += moveVector * Time.deltaTime * movementSpeed;
    }

    private void HandleWebZipStart() {

        GrappleSliderController.Instance.HideSlider();
        
        GrappleSliderController.Instance.InitSlider(holdDownStartTime);
        SoundManager.PlaySound("grapple");
        webZipTargetPosition = mousePos;

        webZipSpeed = 2.0f;
        playerState = PlayerState.WebZipping;

    }

    private void HandleWebZipping()
    {
        GrappleSliderController.Instance.HideSlider();

        float travelDistance = Vector2.Distance(transform.position, webZipTargetPosition);
        if (webZipSpeed >= travelDistance)
        {
           // Debug.Log("Hold for too long!");
            transform.position = Vector2.MoveTowards(
                transform.position,
                webZipTargetPosition,
                webZipSpeed * (travelDistance / webZipSpeed)
          );
        } else
        {
            transform.position = Vector2.MoveTowards(
            transform.position,
            webZipTargetPosition,
            webZipSpeed
        );
        }

        playerState = PlayerState.Normal;
    }
}
