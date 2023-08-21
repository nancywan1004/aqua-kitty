using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float movementSpeed = 1.2f;

    //public GameObject webZipPrefab;

    public Animator animator;
    private Rigidbody2D _rigidbody;
    //private GameObject webZip;
    private PlayerState playerState;
    private Vector3 mousePos;
    private float rotateY;
    private GameObject pops;
    private float holdDownStartTime;
    private float holdDownTime;
    private float moveStartTime;
    private float moveHoldTime;

    private Vector3 webZipDir;
    private Vector3 webZipTargetPosition;
    private float webZipSpeed;
    //private SpriteRenderer webZipSpriteRenderer;
    private Vector2 webZipStart;
    private Vector2 webZipEnd;
    private Inventory _inventory;
    [SerializeField] private UI_Inventory _uiInventory;
    [SerializeField] private List<InventoryItem> _inventoryItems;
    [SerializeField] private WeaponController weaponController;

    private GlobalInputActions _inputActions;

    private enum PlayerState
    {
        Normal,
        WebZipping,
        WebZippingSliding,
        Attached,
        Dizzy
    }

    private void Awake()
    {
        _inputActions = new GlobalInputActions();
        _inputActions.Player.Bubbleshoot.performed += context =>
        {
            weaponController.Shoot();
        };
        _inputActions.Player.Grapple.started += context =>
        {
            HandleWebZipStart();
        };
        // _inputActions.Player.Grapple.performed += context =>
        // {
        //     webZip.transform.position = firePoint.transform.position;
        //     webZipSpriteRenderer = webZip.GetComponent<SpriteRenderer>();
        //     webZipStart = new Vector2(webZip.GetComponent<SpriteRenderer>().size.x, 0);
        //     webZipEnd = new Vector2(webZip.GetComponent<SpriteRenderer>().size.x,
        //         Vector3.Distance(firePoint.transform.position, webZipTargetPosition));
        //
        //     webZipSpriteRenderer.size = webZipEnd;
        // };
        _inputActions.Player.Grapple.canceled += context =>
        {
            HandleWebZipping();
        };
        _inputActions.Player.SwitchWeapon.performed += context =>
        {
            _inventory.SwitchInventoryItem();
        };
        // _inputActions.Player.MoveLeft.started += context =>
        // {
        //     rotateY = 0f;
        //     transform.rotation = Quaternion.Euler(0, rotateY, 0);
        //     moveStartTime = Time.time;
        // };
        // _inputActions.Player.MoveLeft.performed += context =>
        // {
        //     moveHoldTime = Time.time - moveStartTime;
        //     transform.position = Vector2.MoveTowards(transform.position,-transform.right * moveHoldTime * movementSpeed, moveHoldTime);
        // };
        // _inputActions.Player.MoveRight.started += context =>
        // {
        //     rotateY = 180f;
        //     transform.rotation = Quaternion.Euler(0, rotateY, 0);
        //     moveStartTime = Time.time;
        // };
        // _inputActions.Player.MoveRight.performed += context =>
        // {
        //     moveHoldTime = Time.time - moveStartTime;
        //     transform.position = Vector2.MoveTowards(transform.position,-transform.right * moveHoldTime * movementSpeed, moveHoldTime);
        // };
        playerState = PlayerState.Normal;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = false;
        _inventory = new Inventory(_inventoryItems);
        _uiInventory.SetInventory(_inventory);
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
    
    // Update is called once per frame
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
            case PlayerState.WebZippingSliding:
                HandleWebZippingSliding();
                break;
            case PlayerState.Attached:
                HandleRelativeRotation();
                break;
            case PlayerState.Dizzy:
                // if (webZip != null)
                // {
                //     webZip.gameObject.SetActive(false);
                // }
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

        holdDownStartTime = Time.time;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);

        //Debug.Log(hit.collider);
        // if (hit.collider != null && hit.collider.CompareTag("Helper"))
        // {
            GrappleSliderController.Instance.InitSlider(holdDownStartTime);
            SoundManager.PlaySound("grapple");
            webZipTargetPosition = mousePos;
            webZipDir = (webZipTargetPosition - transform.position).normalized;

            //spawnWebZip(firePoint.transform.position, firePoint.transform.rotation);
            //Vector3 webDir = (webZipTargetPosition - firePoint.transform.position).normalized;
            //webZip.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(webDir.y, webDir.x) * Mathf.Rad2Deg - 90);

            webZipSpeed = 20.0f;
            playerState = PlayerState.WebZipping;
        //}

    }

    private void HandleWebZipping()
    {
        //Debug.Log("HandleWebZipping status");
        GrappleSliderController.Instance.HideSlider();
        holdDownTime = Time.time - holdDownStartTime;
        //webZipSpriteRenderer.size = Vector2.Lerp(webZipStart, webZipEnd, holdDownTime);

        float travelDistance = Vector2.Distance(transform.position, webZipTargetPosition);
        if (webZipSpeed * holdDownTime >= travelDistance)
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
            webZipSpeed * holdDownTime
        );
        }

        playerState = PlayerState.WebZippingSliding;

    }

    private void HandleWebZippingSliding()
    {
        //Debug.Log("HandleWebZippingSliding status");
        // webZipDir = new Vector3(webZipDir.x, webZipDir.y, 0);
        webZipSpeed -= webZipSpeed * Time.deltaTime * 15.0f;
        transform.position += webZipDir * webZipSpeed * Time.deltaTime;
        //Debug.Log("webZipSpeed is: " + webZipSpeed);
        if (webZipSpeed <= 0.8f)
        {
            //webZip.gameObject.SetActive(false);
            playerState = PlayerState.Normal;
        }
    }

    // private void spawnWebZip(Vector2 position, Quaternion rotation)
    // {
    //     if (webZip == null)
    //     {
    //         webZip = Instantiate(webZipPrefab, position, rotation);
    //     }
    //     else
    //     {
    //         webZip.transform.position = position;
    //         webZip.transform.rotation = rotation;
    //         webZip.gameObject.SetActive(true);
    //     }
    // }

}
